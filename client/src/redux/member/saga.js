// -- DROP SCHEMA "member";

// CREATE SCHEMA "member" AUTHORIZATION postgres;
// -- "member".tbl_address definition

// -- Drop table

// -- DROP TABLE "member".tbl_address;

// CREATE TABLE "member".tbl_address (
// 	address_id int4 NOT NULL,
// 	address_name varchar NULL,
// 	entry_date timestamp NULL,
// 	CONSTRAINT tbl_address_pkey PRIMARY KEY (address_id)
// );


// -- "member".tbl_member definition

// -- Drop table

// -- DROP TABLE "member".tbl_member;

// CREATE TABLE "member".tbl_member (
// 	member_id varchar NOT NULL,
// 	member_name varchar NULL,
// 	member_type_id int4 NULL,
// 	address_id int4 NULL,
// 	email varchar(10) NULL,
// 	phone varchar NULL,
// 	gender varchar NULL,
// 	dob varchar NULL,
// 	entry_date timestamp NULL,
// 	CONSTRAINT tbl_member_pkey PRIMARY KEY (member_id)
// );


// -- "member".tbl_member_type definition

// -- Drop table

// -- DROP TABLE "member".tbl_member_type;

// CREATE TABLE "member".tbl_member_type (
// 	member_type_id int4 NOT NULL,
// 	type_name varchar NULL,
// 	entry_date timestamp NULL,
// 	CONSTRAINT tbl_address_type_pkey PRIMARY KEY (member_type_id)
// );



// -- DROP FUNCTION "member".fn_get_address(int4);

// CREATE OR REPLACE FUNCTION member.fn_get_address(p_address_id integer)
//  RETURNS refcursor
//  LANGUAGE plpgsql
// AS $function$
// declare ref refcursor;
// BEGIN
//     OPEN ref FOR
//     SELECT 
//         address_id,
//         address_name,
//         entry_date
//     FROM member.tbl_address
//     WHERE p_address_id IS NULL OR address_id = p_address_id;
// return ref;
// END;
// $function$
// ;

// -- DROP FUNCTION "member".fn_get_address_member_type(int4);

// CREATE OR REPLACE FUNCTION member.fn_get_address_member_type(p_member_type_id integer)
//  RETURNS refcursor
//  LANGUAGE plpgsql
// AS $function$
// declare ref refcursor;
// BEGIN
//     OPEN ref FOR
//     SELECT 
//         member_type_id,
//         type_name,
//         entry_date
//     FROM member.tbl_address_type
//     WHERE p_member_type_id IS NULL OR member_type_id = p_member_type_id;
// return ref;
// END;
// $function$
// ;

// -- DROP FUNCTION "member".fn_get_member(varchar);

// CREATE OR REPLACE FUNCTION member.fn_get_member(p_member_id character varying)
//  RETURNS refcursor
//  LANGUAGE plpgsql
// AS $function$
// declare ref refcursor;
// BEGIN
//     OPEN ref FOR
//     SELECT 
//         m.member_id,
//         m.member_name,
//         m.address_id,
//         a.address_name,
// 		mt.member_type_id,
// 		mt.member_type_name,
//         m.email,
//         m.phone,
//         m.gender,
//         m.dob,
//         m.entry_date
//     FROM member.tbl_member m
//     LEFT JOIN member.tbl_address a 
// 	ON m.address_id = a.address_id
// 	left join member.tbl_member_type mt
// 	on mt.member_type_id = m.member_type_id
//     WHERE p_member_id IS NULL OR m.member_id = p_member_id;
// return ref;
// END;
// $function$
// ;

// -- DROP FUNCTION "member".fn_get_member_type(int4);

// CREATE OR REPLACE FUNCTION member.fn_get_member_type(p_member_type_id integer)
//  RETURNS refcursor
//  LANGUAGE plpgsql
// AS $function$
// declare ref refcursor;
// BEGIN
//     OPEN ref FOR
//     SELECT 
//         member_type_id,
//         type_name,
//         entry_date
//     FROM member.tbl_member_type
//     WHERE p_member_type_id IS NULL OR member_type_id = p_member_type_id;
// return ref;
// END;
// $function$
// ;