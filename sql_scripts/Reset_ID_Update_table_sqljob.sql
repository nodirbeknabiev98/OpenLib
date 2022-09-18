--USE openLibDB
--UPDATE [password_reset_tbl_expired]
--SET userSignature = prt.userSignature, member_id = prt.member_id, phone_number = prt.phone_number, reset_id = prt.reset_id, linkReq = prt.linkReq, linkExp = prt.linkExp
--FROM  password_reset_tbl prt
--WHERE prt.member_id = 'nodirbek_nabiev';
--GO

--SQL SCRIPT 
USE openLibDB
BEGIN TRANSACTION;

	BEGIN TRY
		INSERT INTO password_reset_tbl_expired(userSignature,member_id,phone_number,reset_id,linkReq,linkExp)
		SELECT userSignature,member_id,phone_number,reset_id,linkReq,linkExp
		FROM password_reset_tbl
		WHERE password_reset_tbl.linkExp < GETDATE();
	END TRY
	BEGIN CATCH 
		SELECT ERROR_NUMBER() AS ErrorNumber, ERROR_SEVERITY() AS ErrorSeverity,ERROR_STATE() AS ErrorState,ERROR_PROCEDURE() AS ErrorProcedure,ERROR_LINE() AS ErrorLine,ERROR_MESSAGE() AS ErrorMessage;
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
	END CATCH;

	BEGIN TRY
		DELETE FROM password_reset_tbl
		WHERE linkExp < GETDATE();
	END TRY
	BEGIN CATCH 
		SELECT ERROR_NUMBER() AS ErrorNumber, ERROR_SEVERITY() AS ErrorSeverity,ERROR_STATE() AS ErrorState,ERROR_PROCEDURE() AS ErrorProcedure,ERROR_LINE() AS ErrorLine,ERROR_MESSAGE() AS ErrorMessage;
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
	END CATCH;

IF @@TRANCOUNT > 0
    COMMIT TRANSACTION;




