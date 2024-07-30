Description

This problem will help the user store a local user which contains his/her generated password for the platform that he wanted to register.

The user can generate a password to a Platform that he likes and can set the length of the password Default value:10
The user can search for the all the password of his account
The user can search for the platform and generated password by typing the password that he wrote during the generation


Basic Functions

GUI-----
4 Options:
after login
[user]
Add password
List All Passwords
Search For Password for Platform
Exit Applicatin

Project require database
Mysql 

users[ID,name,password,email,mail_system(bool)]
core[id,user_id,platform,password,generated_password,gen_pw_length]
