CREATE DATABASE bug_tracker;
USE bug_tracker;

/////////////////////////////////////////////////////////////////////////////////////////////
CREATE TABLE tbl_programmer (
	programmer_id INT IDENTITY,
	full_name VARCHAR(256) NOT NULL,
	username VARCHAR(256) NOT NULL UNIQUE,
	password VARCHAR(256) NOT NULL,
	CONSTRAINT pk_programmer_id PRIMARY KEY(programmer_id)
);
SP_HELP tbl_programmer;
SELECT * FROM tbl_programmer;
DELETE FROM tbl_programmer--done

/////////////////////////////////////////////////////////////////////////////////////////////
CREATE TABLE tbl_tester (
	tester_id INT IDENTITY,
	full_name VARCHAR(256) NOT NULL,
	username VARCHAR(256) NOT NULL UNIQUE,
	password VARCHAR(256) NOT NULL,
	CONSTRAINT pk_tester_id PRIMARY KEY(tester_id)
);
SP_HELP tbl_tester;

/////////////////////////////////////////////////////////////////////////////////////////////
CREATE TABLE tbl_code (
	code_id INT IDENTITY,
	code_file_path VARCHAR(256) NOT NULL,
	code_file_name VARCHAR(256) NOT NULL,
	programming_language VARCHAR(256) NOT NULL,
	CONSTRAINT pk_code_id PRIMARY KEY(code_id)
);
SP_HELP tbl_code;
ALTER TABLE tbl_code ADD bug_id INT;
ALTER TABLE tbl_code ADD CONSTRAINT fk_code_bug_id FOREIGN KEY(bug_id) REFERENCES tbl_bug(bug_id);
SELECT * FROM tbl_code;
DELETE FROM tbl_code;--done


/////////////////////////////////////////////////////////////////////////////////////////////
CREATE TABLE tbl_bug(
	bug_id INT IDENTITY,
	project_name VARCHAR(256) NOT NULL,
	class_name VARCHAR(256),
	method_name VARCHAR(256),
	start_line INT NOT NULL,
	end_line INT,
	code_author INT,
	code_id INT,
	bug_status CHAR(1) NOT NULL DEFAULT '0',
	CONSTRAINT pk_bug_id PRIMARY KEY(bug_id),
	CONSTRAINT fk_code_author FOREIGN KEY(code_author) REFERENCES tbl_programmer(programmer_id),
	CONSTRAINT fk_code_id FOREIGN KEY(code_id) REFERENCES tbl_code(code_id),
);
SP_HELP tbl_bug;
ALTER TABLE tbl_bug DROP CONSTRAINT fk_code_id;
ALTER TABLE tbl_bug DROP COLUMN code_id;
SELECT * FROM tbl_bug;
DELETE FROM tbl_bug;--done
SELECT * FROM tbl_bug;



/////////////////////////////////////////////////////////////////////////////////////////////

CREATE TABLE tbl_source_control 
	source_control_id INT IDENTITY,
	link VARCHAR(999) NOT NULL,
	start_line INT NOT NULL,
	end_line INT,
	bug_id INT,
	CONSTRAINT pk_source_control_id PRIMARY KEY(source_control_id),
	CONSTRAINT fk_bug_id FOREIGN KEY(bug_id) REFERENCES tbl_bug(bug_id)
);
SP_HELP tbl_source_control;
SELECT * FROM tbl_source_control;
DELETE FROM tbl_source_control;--done

/////////////////////////////////////////////////////////////////////////////////////////////

CREATE TABLE tbl_image (
	image_id INT IDENTITY,
	image_path VARCHAR(999) NOT NULL,
	image_name VARCHAR(256) NOT NULL,
	bug_id INT,
	CONSTRAINT pk_image_id PRIMARY KEY(image_id),
	CONSTRAINT fk_image_bug_id FOREIGN KEY(bug_id) REFERENCES tbl_bug(bug_id)
);
SP_HELP tbl_image;
SELECT * FROM tbl_image;
DELETE FROM tbl_image;--done
insert into tbl_image values('code_image', 'Screenshot (7).png', 1106);

/////////////////////////////////////////////////////////////////////////////////////////////

CREATE TABLE tbl_fixer (
	fixer_id INT IDENTITY,
	fixed_by INT,
	bug_id INT,
	fixed_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	CONSTRAINT pk_fixer_id PRIMARY KEY(fixer_id),
	CONSTRAINT fk_fixed_by FOREIGN KEY(fixed_by) REFERENCES tbl_programmer(programmer_id),
	CONSTRAINT fk_fixer_bug_id FOREIGN KEY(bug_id) REFERENCES tbl_bug(bug_id)
);

SP_HELP tbl_fixer;
DELETE FROM tbl_fixer;--done

/////////////////////////////////////////////////////////////////////////////////////////////

CREATE TABLE tbl_assign (
	assign_id INT IDENTITY,
	assign_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	deadline DATETIME,
	descriptions VARCHAR(999),
	assign_by INT,
	assign_to INT,
	bug_id INT,
	CONSTRAINT pk_assign_id PRIMARY KEY(assign_id),
	CONSTRAINT fk_assign_by FOREIGN KEY(assign_by) REFERENCES tbl_tester(tester_id),
	CONSTRAINT fk_assign_to FOREIGN KEY(assign_to) REFERENCES tbl_programmer(programmer_id)
);

SP_HELP tbl_assign;
ALTER TABLE tbl_assign DROP COLUMN deadline;
SELECT * FROM tbl_assign;
DELETE FROM tbl_assign;--done

/////////////////////////////////////////////////////////////////////////////////////////////

CREATE TABLE tbl_bug_history (
	bug_history_id INT IDENTITY,
	descriptions VARCHAR(999),
	source_control_id INT,
	CONSTRAINT pk_bug_history_id PRIMARY KEY(bug_history_id),
	CONSTRAINT fk_source_control_id FOREIGN KEY(source_control_id) REFERENCES tbl_source_control(source_control_id)
);

SP_HELP tbl_bug_history;
DELETE FROM tbl_bug_history;--done

/////////////////////////////////////////////////////////////////////////////////////////////

CREATE TABLE tbl_bug_information(
	bug_information_id INT IDENTITY,
	symptons VARCHAR(999),
	cause VARCHAR(999),
	bug_id INT,
	CONSTRAINT pk_bug_information_id PRIMARY KEY(bug_information_id),
	CONSTRAINT fk_bug_information_bug_id FOREIGN KEY(bug_id) REFERENCES tbl_bug(bug_id)
);

SP_HELP tbl_bug_information;
SELECT * FROM tbl_bug_information;
delete from tbl_bug_information where bug_information_id = 4;
DELETE FROM tbl_bug_information;--done

////////////////////////////////////////////////////////////////////////////////////////////////////

CREATE TABLE tbl_admin(
	admin_id INT IDENTITY,
	company_name VARCHAR(999) NOT NULL,
	username VARCHAR(999) UNIQUE NOT NULL,
	password VARCHAR(999) NOT NULL,
	CONSTRAINT pk_admin_id PRIMARY KEY(admin_id)
);
DELETE FROM tbl_admin;--done

///////////////////////////////////////////////////////////////////////////////////////////////////

CREATE TABLE tbl_project(
	project_id INT IDENTITY,
	project_name VARCHAR(999) NOT NULL,
	CONSTRAINT pk_project_id PRIMARY KEY(project_id)
);

ALTER TABLE tbl_project ADD admin_id INT NOT NULL;
ALTER TABLE tbl_project ADD CONSTRAINT fk_admin_id FOREIGN KEY(admin_id) REFERENCES tbl_admin (admin_id);

SELECT * FROM tbl_project;
DELETE FROM tbl_project;--done

///////////////////////////////////////////////////////////////////////////////////////////////////

CREATE TABLE tbl_project_programmer (
	project_programmer_id INT IDENTITY,
	project_id INT,
	programmer_id INT,
	CONSTRAINT pk_project_programmer_id PRIMARY KEY(project_programmer_id),
	CONSTRAINT fk_project_id FOREIGN KEY(project_id) REFERENCES tbl_project(project_id),
	CONSTRAINT fk_programmer_id FOREIGN KEY(programmer_id) REFERENCES tbl_programmer(programmer_id)
);
DELETE FROM tbl_project_programmer--done;


SP_HELP tbl_project_programmer;
SELECT * FROM tbl_project_programmer;
ALTER TABLE tbl_project_programmer ADD admin_id INT;
ALTER TABLE tbl_project_programmer ADD CONSTRAINT fk_admins_id FOREIGN KEY(admin_id) REFERENCES tbl_admin(admin_id) ON DELETE CASCADE;
DELETE FROM tbl_project_programmer;
alter table tbl_project_programmer drop CONSTRAINT fk_project_id;
ALTER TABLE tbl_project_programmer ADD CONSTRAINT fk_project_id FOREIGN KEY(project_id) REFERENCES tbl_project(project_id) ON DELETE CASCADE;
///////////////////////////////////////////////////////////////////////////////////////////////////

INSERT INTO tbl_programmer VALUES ('Nishan Dhungana', 'nishandhungana41', 'nishan');SELECT 

SELECT * FROM tbl_bug;
SELECT * FROM tbl_code;
SELECT * FROM tbl_image;

-- code
SELECT * FROM tbl_bug b
JOIN tbl_code c ON
b.bug_id = c.bug_id
JOIN tbl_image i
ON b.bug_id = i.bug_id
JOIN tbl_source_control sc
ON sc.bug_id = i.bug_id
WHERE bug_status = 0
AND b.bug_id = 102;

SELECT * FROM tbl_project;
SELECT * FROM tbl_bug;

--to get all projects
SELECT pr.project_id, pr.project_name FROM tbl_project pr
JOIN tbl_project_programmer pp
ON pr.project_id = pp.project_id
JOIN tbl_programmer pro
ON pp.programmer_id = pro.programmer_id
WHERE pro.programmer_id = 1;

SELECT pr.project_name FROM tbl_project pr JOIN tbl_project_programmer pp ON pr.project_id = pp.project_id JOIN tbl_programmer pro ON pp.programmer_id = pro.programmer_id WHERE pro.programmer_id = 1; 

select * from tbl_image;
select * from tbl_code;
SELECT * FROM tbl_bug;
delete from tbl_image where bug_id = 82;
delete from tbl_bug where bug_id = 82;
delete from tbl_code where bug_id = 82;
select * from tbl_tester
select * from tbl_source_control;
select * from tbl_assign;
select * from tbl_fixer;
select * from tbl_programmer;

--get all assigned used name
SELECT p.full_name
FROM tbl_programmer p
JOIN tbl_assign a
ON a.assign_to = p.programmer_id
AND bug_id = 98;

--who fixed a bug
SELECT f.bug_id, f.fixed_date, p.full_name
FROM tbl_programmer p
JOIN tbl_fixer f
ON p.programmer_id = f.fixed_by;

--
SELECT * FROM	tbl_assign;

--get assigned bug
SELECT * FROM tbl_programmer p
JOIN tbl_assign a
ON a.assign_to = p.programmer_id
JOIN tbl_bug b
ON b.bug_id = a.bug_id
JOIN tbl_image i
ON b.bug_id = i.bug_id
JOIN tbl_source_control sc
ON sc.bug_id = i.bug_id
JOIN tbl_code c
ON c.bug_id = b.bug_id
WHERE a.assign_to = 1
AND b.bug_status = '0'