-- Table: public.AuditLog

-- DROP TABLE public."AuditLog";

CREATE TABLE public."AuditLog"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "Description" text COLLATE pg_catalog."default" NOT NULL,
    "UserId" integer NOT NULL,
    "DateTimeCreated" timestamp with time zone NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT "AuditLog_pkey" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE public."AuditLog"
    OWNER to postgres;