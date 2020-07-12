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

insert into loan ([datefrom], [dateto], [employee_id], [recipient_id], [status_id], [note])
values  ('2020-07-09', '2020-12-31', 1, null, 2, 'Det første udlån.'),
        ('2019-07-01', '2019-07-31', 3, null, 1, 'Gammelt udlån.'),
        ('2020-07-01', '2020-07-31', 2, null, 3, 'Annulleret.'),
        ('2020-07-02', '2020-08-01', 1, 2, 1, 'Med recipient!');

insert into comment ([username], [text])
values  ('admin-sg', 'Første comment på lån'),
        ('username', 'Ny comment på samme udlån');

insert into loan_comment ([comment_id], [loan_id])
values  (4, 1),
        (5, 1);