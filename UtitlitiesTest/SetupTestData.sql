
CREATE TABLE public."Test"
(
    id integer NOT NULL,
    name character varying,
    PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
);


ALTER TABLE public."Test"
    OWNER to postgres;



INSERT INTO public."Test" (id, name) VALUES (1, 'test1');
INSERT INTO public."Test" (id, name) VALUES (2, 'test2');
INSERT INTO public."Test" (id, name) VALUES (3, 'test3');
INSERT INTO public."Test" (id, name) VALUES (4, 'test4');
INSERT INTO public."Test" (id, name) VALUES (5, 'test5');
