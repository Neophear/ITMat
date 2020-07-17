create table employeestatus
(
    id                  int             primary key,
    [name]              varchar(20)     not null
);

create table employee
(
    id                  int             primary key identity(1, 1),
    manr                varchar(6)      not null,
    [name]              nvarchar(250)   not null,
    status_id           int             references employeestatus(id) not null default 1
);

create table itemtype
(
    id                  int             primary key identity(1, 1),
    [name]              nvarchar(100)   not null
);

create table item
(
    id                  int             primary key identity(1, 1),
    identifier          varchar(250)    unique not null,
    model               varchar(250)    not null,
    type_id             int             references itemtype(id) not null,
    discarded           bit             not null default 0
);

create nonclustered index fi_item_active on item(id) where discarded = 0;
create nonclustered index ix_item_model on item(model);

create table genericitem
(
    id                  int             primary key identity(1, 1),
    [name]              varchar(50)     unique not null
);

create table loan
(
    id                  int             primary key identity(1, 1),
    datefrom            date            not null,
    dateto              date            not null,
    employee_id         int             references employee(id) not null,
    recipient_id        int             references employee(id) null,
    note                nvarchar(max)   not null,
    constraint c_loan_chkdates check (datefrom <= dateto)
);

create table loanlineitem
(
    id                  int             primary key identity(1, 1),
    loan_id             int             references loan(id) on delete cascade not null,
    item_id             int             references item(id) not null,
    pickedup            datetime        null,
    returned            datetime        null,
    constraint uq_loanlineitem unique (loan_id, item_id),
    constraint c_loanlineitem_chkdates check (returned is null or pickedup <= returned)
);

create table loanlinegenericitem
(
    id                  int             primary key identity(1, 1),
    loan_id             int             references loan(id) on delete cascade not null,
    genericitem_id      int             references genericitem(id) not null,
    pickedup            datetime        null,
    returned            datetime        null,
    index ix_loanlinegenericitem nonclustered (loan_id),
    constraint c_loanlinegenericitem_chkdates check (returned is null or pickedup <= returned)
);

create table comment
(
    id                  int             primary key identity(1, 1),
    username            varchar(50)     not null,
    createdtime         datetime        not null default getdate(),
    [text]              nvarchar(max)   not null
);

create table employee_comment
(
    comment_id          int             primary key references comment(id) on delete cascade not null,
    employee_id         int             references employee(id) not null,
    index ix_employee_comment_eid nonclustered (employee_id)
);

create table loan_comment
(
    comment_id          int             primary key references comment(id) on delete cascade not null,
    loan_id             int             references loan(id) not null,
    index ix_loan_comment_lid nonclustered (loan_id)
);

create table item_comment
(
    comment_id          int             primary key references comment(id) on delete cascade not null,
    item_id             int             references item(id) not null,
    index ix_item_comment_iid nonclustered (item_id)
);

go

create view vw_loanlineitem_occupations
as
select lli.id, loan_id, item_id, isnull(lli.pickedup, l.datefrom) as [from], isnull(lli.returned, dateadd(d, 1, l.dateto)) as [to] from loanlineitem lli inner join loan l on lli.loan_id = l.id

go

create function udf_isOverlapping(@span1Start datetime, @span1End datetime, @span2Start datetime, @span2End datetime)
returns bit
as
begin
    return iif((@span1Start <= @span2End and @span1End >= @span2Start), 1, 0);
end

go

create function udf_isLoanValid(@loanId int, @dateFrom date, @dateTo date)
returns bit
as
begin
    declare @result bit = 0;

    --When type 'date' is compared to 'datetime' the time-part is 00:00:00, so add one day
    set @dateTo = dateadd(d, 1, @dateTo)

    if not exists (
        select top 1 lli.id from loanlineitem lli
        inner join loan l on lli.loan_id = l.id
        --Only check items that are in this loan
        where lli.item_id in (select item_id from loanlineitem where loan_id = @loanId)
        --Check if this span is overlapping with other lines/loans
        --When type 'date' is compared to 'datetime' the time-part is 00:00:00, so add one day
        and (dbo.udf_isOverlapping(
                @dateFrom,
                @dateTo,
                isnull(lli.pickedup, iif(l.id = @loanId, @dateFrom, l.datefrom)),
                isnull(lli.returned, iif(l.id = @loanId, @dateTo, dateadd(d, 1, l.dateto)))
                ) = 1
            )
        )
    begin
        set @result = 1
    end

    return @result;
end;

go;

alter table loan
add constraint c_loan_datecheck check (dbo.udf_isLoanValid(id, dateFrom, dateTo) = 1);

go;

create function udf_isLineValid(@itemId int, @loanId int, @pickedup datetime, @returned datetime)
returns bit
as
begin
    declare @result bit = 0;
    declare @from date = @pickedup;
    declare @to date = @returned;
    
    --If either @from or @to is null, fill the ones with null from loan-table
    if (@from is null or @to is null)
    begin
        select  @from = isnull(@from, datefrom),
                @to = isnull(@to, dateadd(d, 1, dateto))
        from    loan
        where   id = @loanId;
    end

    --If no lines with overlap exists, this line is valid, so set result to 1
    if not exists (
        select top 1 id from vw_loanlineitem_occupations
        where item_id = @itemId
        and loan_id <> @loanId
        and dbo.udf_isOverlapping(@from, @to, [from], [to]) = 1
        )
    begin
        set @result = 1;
    end

    return @result;
end;

go;

alter table loanlineitem
add constraint c_loanlineitem_checkoverlap check (dbo.udf_isLineValid(item_id, loan_id, pickedup, returned) = 1)

go;

insert into employeestatus (id, [name])
values  (1, 'Active'),
        (2, 'Blacklisted'),
        (3, 'Inactive');

go;

create view vw_loan
as
select  l.*,
        active = case when exists(select top 1 id from loanlineitem where loan_id = l.id and (returned is null or pickedup is null)) then 1
                      when exists(select top 1 id from loanlinegenericitem where loan_id = l.id and (returned is null or pickedup is null)) then 1
                      else 0
                 end
from loan l