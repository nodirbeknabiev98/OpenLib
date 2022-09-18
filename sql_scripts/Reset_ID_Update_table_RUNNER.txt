@echo off
@echo Platform/Framework: ASP.NET Web-Forms, C#
@echo App: OpenLib Library Management System
@echo Cheking for expired web-links in table: password_reset_tbl_expired
@echo Updating table...
@echo Result:
SqlCmd -S LAPTOP-FL2SQGK1\SQLEXPRESS -d openLibDB -E -i "C:\BACKUPS\C#\ElibraryManagement-v1\sql_scripts\Reset_ID_Update_table_sqljob.sql"
PAUSE