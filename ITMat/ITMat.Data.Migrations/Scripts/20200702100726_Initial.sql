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
    employee_id         int             references employee(id),
    datefrom            date            not null,
    dateto              date            not null,
    status_id           int             references loanstatus(id) default 1,
    recipient_id        int             references employee(id) null,
    note                nvarchar(max)   not null
);

create table loanline
(
    id                  int             primary key identity(1, 1),
    loan_id             int             references loan(id) on delete cascade,
    item_id             int             references item(id),
    pickedup            datetime        null,
    returned            datetime        null
);

create table comment
(
    id                  int             primary key identity(1, 1),
    username            varchar(50)     not null,
    createdtime         datetime        not null default getdate(),
    [text]              nvarchar(max)   not null
)

create table employee_comment
(
    comment_id          int             primary key references comment(id) on delete cascade,
    employee_id         int             references employee(id),
    index ix_employee_comment_eid nonclustered (employee_id)
)

go;

insert into loanstatus (id, [name])
values  (1, 'Active'),
        (2, 'Cancelled');

insert into employeestatus (id, [name])
values  (1, 'Active'),
        (2, 'Blacklisted'),
        (3, 'Inactive');

