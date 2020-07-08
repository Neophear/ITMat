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