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
