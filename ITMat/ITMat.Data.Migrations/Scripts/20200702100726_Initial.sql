create table employeestatus
(
    id                  int             primary key,
    [name]              varchar(20)     not null
)

create table employee
(
    id                  int             primary key identity(1, 1),
    manr                varchar(6)      not null,
    [name]              nvarchar(250)   not null,
    status_id           int             references employeestatus(id) default 1
)

create table itemtype
(
    id                  int             primary key identity(1, 1),
    [name]              nvarchar(100)   not null
)

create table item
(
    id                  int             primary key identity(1, 1),
    type_id             int             references itemtype(id),
    discarded           bit             not null default 0
)

create table uniqueitem
(
    id                  int             primary key references item(id),
    [uniqueidentifier]  varchar(250)    not null
)

create table miscitem
(
    id                  int             primary key references item(id)
)

create table loanstatus
(
    id                  int             primary key,
    [name]              nvarchar(20)    not null
)

create table loan
(
    id                  int             primary key identity(1, 1),
    datefrom            date            not null,
    dateto              date            not null,
    employee_id         int             references employee(id),
    recipient_id        int             references employee(id) null,
    status_id           int             references loanstatus(id) default 1,
    note                nvarchar(max)   not null,
    constraint c_loan_chkdates check (datefrom <= dateto)
);

create nonclustered index fi_loan_active on loan(id) where status_id = 1;

create table loanitemline
(
    loan_id             int             references loan(id) on delete cascade,
    item_id             int             references item(id),
    pickedup            datetime        null,
    returned            datetime        null,
    constraint pk_loanitemline primary key (loan_id, item_id),
    constraint c_loanitemline_chkdates check (isnull(pickedup, '1970-01-01') <= isnull(returned, '1970-01-01'))
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
    comment_id          int             primary key references comment(id) on delete cascade,
    employee_id         int             references employee(id),
    index ix_employee_comment_eid nonclustered (employee_id)
);

create table loan_comment
(
    comment_id          int             primary key references comment(id) on delete cascade,
    loan_id             int             references loan(id),
    index ix_loan_comment_lid nonclustered (loan_id)
);

go;

insert into loanstatus (id, [name])
values  (1, 'Active'),
        (2, 'Finished'),
        (3, 'Cancelled');

insert into employeestatus (id, [name])
values  (1, 'Active'),
        (2, 'Blacklisted'),
        (3, 'Inactive');