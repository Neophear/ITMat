insert into employee (manr, [name], status_id)
values  ('370929', 'Stiig Andreas Gade', 1),
        ('123456', 'Jens Jensen', 2),
        ('456789', 'Rikke Rikkesen', 3)
go;

insert into comment ([username], [text])
values  ('admin-sg', 'Dette er den første comment!'),
        ('admin-lg', 'Og her er comment #2'),
        ('admin-sg', 'Og endnu en comment. Denne er nummer 3');

insert into employee_comment (comment_id, employee_id)
values  (1, 1),
        (2, 1),
        (3, 2);
go;

insert into itemtype ([name])
values  ('Bærbar'),
        ('Skærm'),
        ('Stationær'),
        ('Mobiltelefon');
go;

--Insert items
declare @cnt int = 1;

while @cnt <= 50
begin
    insert into item (identifier, model, type_id)
    values  ('LAPTOP' + right('000'+cast(@cnt as varchar(3)),3), 'Dell Latitude', 1),
            ('MONITOR' + right('000'+cast(@cnt as varchar(3)),3), 'HP 32"', 2),
            ('PC' + right('000'+cast(@cnt as varchar(3)),3), 'HP 7250', 3),
            ('PHONE' + right('000'+cast(@cnt as varchar(3)),3), 'Samsung Galaxy 10', 4)

    set @cnt = @cnt + 1;
end;

go;

insert into genericitem ([name])
values  ('Mus'),        --1
        ('Tastatur'),   --2
        ('Strømkabel'), --3
        ('Kabel, VGA'), --4
        ('Kabel, DVI'), --5
        ('Kabel, HDMI');--6

insert into loan ([datefrom], [dateto], [employee_id], [recipient_id], [note])
values  ('2019-07-02', '2019-08-01', 1, null, 'Afsluttet'),
        ('2020-07-09', '2020-12-31', 1, null, 'Det andet udlån.'),
        ('2019-07-01', '2019-07-31', 3, null, 'Gammelt udlån.'),
        ('2020-07-01', '2020-07-31', 2, null, 'Endnu et.'),
        ('2020-07-02', '2020-08-01', 1, 2, 'Med recipient!');

insert into loanlineitem (loan_id, item_id, pickedup, returned)
values  (1, 1, '2019-07-02 08:32:41', '2019-08-07 12:31:07'),
        (2, 1, '2020-07-09 11:15:01', null),
        (2, 5, '2020-07-09 11:15:02', null),
        (2, 9, '2020-07-09 11:15:04', null),
        (3, 3, null, null),
        (3, 2, null, null),
        (4, 10, null, null),
        (5, 4, null, null),
        (5, 8, null, null);

insert into loanlinegenericitem (loan_id, genericitem_id)
values  (1, 1),
        (1, 1),
        (1, 1),
        (2, 4),
        (2, 1),
        (2, 2);

insert into comment ([username], [text])
values  ('admin-sg', 'Første comment på lån'),
        ('username', 'Ny comment på samme udlån');

insert into loan_comment ([comment_id], [loan_id])
values  (4, 1),
        (5, 1);