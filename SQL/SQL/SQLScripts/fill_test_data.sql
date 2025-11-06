USE StudentManagement;


INSERT INTO COURSES (NAME, DESCRIPTION) VALUES
('Computer Science', 'Fundamentals of programming and algorithms'),
('Mathematics', 'Advanced mathematics and calculus'),
('Physics', 'Classical and modern physics'),
('Chemistry', 'Organic and inorganic chemistry');


INSERT INTO GROUPS (COURSE_ID, NAME) VALUES
(1, 'SR-01'), 
(1, 'SR-02'),
(2, 'MT-01'), 
(2, 'MT-02'),
(3, 'PH-01'), 
(4, 'CH-01'); 


INSERT INTO STUDENTS (GROUP_ID, FIRST_NAME, LAST_NAME) VALUES

(1, 'John', 'Smith'),
(1, 'Emma', 'Johnson'),
(1, 'Michael', 'Brown'),
(1, 'Sarah', 'Davis'),
(1, 'David', 'Wilson'),
(1, 'Lisa', 'Miller'),
(1, 'James', 'Taylor'),
(1, 'Jennifer', 'Anderson'),

(2, 'Robert', 'Thomas'),
(2, 'Maria', 'Jackson'),
(2, 'William', 'White'),
(2, 'Linda', 'Harris'),
(2, 'Richard', 'Martin'),
(2, 'Barbara', 'Thompson'),
(2, 'Charles', 'Garcia'),
(2, 'Susan', 'Martinez'),
(2, 'Joseph', 'Robinson'),
(2, 'Jessica', 'Clark'),
(2, 'Thomas', 'Rodriguez'),
(2, 'Karen', 'Lewis'),


(3, 'Christopher', 'Lee'),
(3, 'Nancy', 'Walker'),
(3, 'Daniel', 'Hall'),
(3, 'Betty', 'Allen'),
(3, 'Paul', 'Young'),


(4, 'Mark', 'King'),
(5, 'Donna', 'Wright'),
(6, 'Steven', 'Scott');
