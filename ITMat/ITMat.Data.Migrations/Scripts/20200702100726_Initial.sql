CREATE TABLE Employee
(
    Id                  INT             PRIMARY KEY IDENTITY(1, 1),
    MANR                VARCHAR(6)      NOT NULL,
    [Name]              NVARCHAR(250)   NOT NULL,
    Blacklisted         BIT             NOT NULL DEFAULT 0
)

CREATE TABLE ItemType
(
    Id                  INT             PRIMARY KEY IDENTITY(1, 1),
    [Name]              NVARCHAR(100)   NOT NULL
)

CREATE TABLE Item 
(
    Id                  INT             PRIMARY KEY IDENTITY(1, 1),
    TypeRefId           INT             REFERENCES ItemType(Id),
    Discarded           BIT             NOT NULL DEFAULT 0
)

CREATE TABLE UniqueItem
(
    Id                  INT             PRIMARY KEY REFERENCES Item(Id),
    [UniqueIdentifier]  VARCHAR(250)    NOT NULL
)

CREATE TABLE MiscItem
(
    Id                  INT             PRIMARY KEY REFERENCES Item(Id)
)

CREATE TABLE LoanStatus
(
    Id                  INT             PRIMARY KEY IDENTITY(1, 1),
    [Name]              NVARCHAR(20)    NOT NULL
)

CREATE TABLE Loan
(
    Id                  INT             PRIMARY KEY IDENTITY(1, 1),
    EmployeeRefId       INT             REFERENCES Employee(Id),
    DateFrom            DATE            NOT NULL,
    DateTo              DATE            NOT NULL,
    StatusRefId         INT             REFERENCES LoanStatus(Id),
    RecipientRefId      INT             REFERENCES Employee(Id) NULL,
    Note                NVARCHAR(MAX)   NOT NULL
)

CREATE TABLE LoanLine
(
    Id                  INT             PRIMARY KEY IDENTITY(1, 1),
    LoanRefId           INT             REFERENCES Loan(Id) ON DELETE CASCADE,
    ItemRefId           INT             REFERENCES Item(Id),
    PickedUp            DATETIME        NULL,
    Returned            DATETIME        NULL
)