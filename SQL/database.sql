create table t_message
(
    message_id              int auto_increment,
    message_login_sender    varchar(30)                         not null,
    message_login_recipient varchar(30)                         not null,
    message_text            text                                not null,
    message_date            timestamp default CURRENT_TIMESTAMP not null on update CURRENT_TIMESTAMP,
    constraint t_message_message_id_uindex
        unique (message_id)
);

create table t_registration
(
    registration_id       int auto_increment,
    registration_login    varchar(30) not null,
    registration_password varchar(30) not null,
    constraint t_registration_registration_id_uindex
        unique (registration_id)
);

create table t_system_message
(
    system_message_id   int auto_increment,
    system_message_type varchar(15) not null,
    system_message_text text        not null,
    constraint t_system_message_system_message_id_uindex
        unique (system_message_id)
);

