

PRINT '     _  _                                       '
PRINT '    | || |  _            _                      '
PRINT '    | || |_| |_ ___  ___| |_ _   _ _ __ ___     '
PRINT '    |__   _| __/ _ \/ __| __| | | |  __/ _ \    '
PRINT '       |_| | ||  __/ (__| |_| |_| | | |  __/    '
PRINT '            \__\___|\___|\__|\__,_|_|  \___|    '
PRINT '                                                '
PRINT '        empower your software solutions         '
PRINT '                                                '

PRINT '******************* pre scripts *******************'

-- Start transaction for all pre scripts
BEGIN TRANSACTION;
BEGIN TRY

    DECLARE @DBEXISTS bit;
    SET @DBEXISTS = 0;

    DECLARE @DBNAME NVARCHAR(128);
    SET @DBNAME = DB_NAME();

    DECLARE @DBID INT;
    SET @DBID = DB_ID();

    IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = @DBNAME)
    BEGIN
        PRINT 'The database ' +  @DBNAME + ' does not yet exist. All pre deployment scripts will be skipped.'
        SET @DBEXISTS = 0
    END
    ELSE
    BEGIN
        PRINT 'The database ' +  @DBNAME + ' already exists. pre deployment scripts will be executed.'
        SET @DBEXISTS = 1
    END
END TRY
BEGIN CATCH
    DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorSeverity INT;
    DECLARE @ErrorState INT;

    SELECT @ErrorMessage = ERROR_MESSAGE(),
           @ErrorSeverity = ERROR_SEVERITY(),
           @ErrorState = ERROR_STATE();

	-- Rollback all transactions if any of the pre scripts failed.
    IF @@TRANCOUNT > 0  
        ROLLBACK TRANSACTION;

    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);

END CATCH;

-- Commit transaction if all pre scripts have been successfully run.
IF @@TRANCOUNT > 0  
    COMMIT TRANSACTION;  
GO