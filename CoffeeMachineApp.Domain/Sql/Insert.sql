DBCC CHECKIDENT (CoffeeMachines, RESEED, 0)
DBCC CHECKIDENT (Products, RESEED, 0)
DBCC CHECKIDENT (Badges, RESEED, 0)

insert into CoffeeMachines values (50)

insert into Products values ((select top(1) MachineID from CoffeeMachines), 'Tea', 5)
insert into Products values ((select top(1) MachineID from CoffeeMachines), 'Coffee', 5)
insert into Products values ((select top(1) MachineID from CoffeeMachines), 'Chocolate', 5)

insert into Badges values ('Badge 1', (select ProductID from Products Where Name='Tea'), 2)
insert into Badges values ('Badge 2', null, null)