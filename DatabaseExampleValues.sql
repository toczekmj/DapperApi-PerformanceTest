drop table if exists Project;
drop table if exists Card;
create table Project (
                         id text primary key,
                         name text not null
);
create table Card
(
    id         text primary key,
    content    text not null,
    color      text not null,
    projectId  text not null,
    foreign key (projectId) references Project (id)
);


insert into Project (id, name) values ('1', 'Project 1');
insert into Project (id, name) values ('2', 'Project 2');

insert into Card (id, content, color, projectId) values ('1', 'Card 1', 'red', '1');
insert into Card (id, content, color, projectId) values ('2', 'Card 2', 'blue', '1');
insert into Card (id, content, color, projectId) values ('3', 'Card 3', 'green', '1');
insert into Card (id, content, color, projectId) values ('4', 'Card 4', 'yellow', '1');
insert into Card (id, content, color, projectId) values ('5', 'Card 5', 'red', '1');
insert into Card (id, content, color, projectId) values ('6', 'Card 6', 'blue', '1');
insert into Card (id, content, color, projectId) values ('7', 'Card 7', 'green', '1');
insert into Card (id, content, color, projectId) values ('8', 'Card 8', 'yellow', '1');
insert into Card (id, content, color, projectId) values ('9', 'Card 9', 'red', '1');
insert into Card (id, content, color, projectId) values ('10', 'Card 10', 'blue', '1');
insert into Card (id, content, color, projectId) values ('11', 'Card 11', 'green', '1');
insert into Card (id, content, color, projectId) values ('12', 'Card 12', 'yellow', '1');
insert into Card (id, content, color, projectId) values ('13', 'Card 13', 'red', '1');
insert into Card (id, content, color, projectId) values ('14', 'Card 14', 'blue', '1');
insert into Card (id, content, color, projectId) values ('15', 'Card 15', 'green', '1');
insert into Card (id, content, color, projectId) values ('16', 'Card 16', 'red', '2');
insert into Card (id, content, color, projectId) values ('17', 'Card 17', 'blue', '2');
insert into Card (id, content, color, projectId) values ('18', 'Card 18', 'green', '2');
insert into Card (id, content, color, projectId) values ('19', 'Card 19', 'yellow', '2');
insert into Card (id, content, color, projectId) values ('20', 'Card 20', 'red', '2');
insert into Card (id, content, color, projectId) values ('21', 'Card 21', 'blue', '2');
insert into Card (id, content, color, projectId) values ('22', 'Card 22', 'green', '2');
insert into Card (id, content, color, projectId) values ('23', 'Card 23', 'yellow', '2');
insert into Card (id, content, color, projectId) values ('24', 'Card 24', 'red', '2');
insert into Card (id, content, color, projectId) values ('25', 'Card 25', 'blue', '2');
insert into Card (id, content, color, projectId) values ('26', 'Card 26', 'green', '2');
insert into Card (id, content, color, projectId) values ('27', 'Card 27', 'yellow', '2');
insert into Card (id, content, color, projectId) values ('28', 'Card 28', 'red', '2');
insert into Card (id, content, color, projectId) values ('29', 'Card 29', 'blue', '2');
insert into Card (id, content, color, projectId) values ('30', 'Card 30', 'green', '2');

