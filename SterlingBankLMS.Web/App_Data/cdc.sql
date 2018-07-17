GO

CREATE FUNCTION [dbo].[SplitStringIntoRows] (@CommaDelimitedString   varchar(1000))
RETURNS   @Result TABLE (Column1   VARCHAR(100))
AS
BEGIN
        DECLARE @IntPos INT
        WHILE (CHARINDEX(',',    @CommaDelimitedString, 0) > 0)
        BEGIN
              SET @IntPos =   CHARINDEX(',',    @CommaDelimitedString, 0)      
              INSERT INTO   @Result (Column1)
              --LTRIM and RTRIM to ensure blank spaces are   removed
              SELECT RTRIM(LTRIM(SUBSTRING(@CommaDelimitedString,   0, @IntPos)))   
              SET @CommaDelimitedString = STUFF(@CommaDelimitedString,   1, @IntPos,   '') 
        END
        INSERT INTO   @Result (Column1)
        SELECT RTRIM(LTRIM(@CommaDelimitedString))--LTRIM and RTRIM to ensure blank spaces are removed
        RETURN 
END

 GO

EXEC sys.sp_cdc_enable_db

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'Permission', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'AspNetRolesPermission', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'AspNetUserRoles', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'AspNetUsers', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'AdBanner', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'Advert', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'Branch', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'Certificate', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'Course', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'CourseLike', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'CourseReview', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'Department', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'DepartmentBudget', 
@role_name = NULL


EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'Examination', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'ExaminationAttempt', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'ExaminationQuestion', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'ExaminationQuestionOption', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'ExaminationQuizResponse', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'FAQ', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'Grade', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'Group', 
@role_name = NULL


EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'LearningArea', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'LearningGroup', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'LearningGroupCourse', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'LearningGroupExam', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'LearningGroupSurvey', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'LearningGroupTraining', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'LearningGroupTag', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'Lesson', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'LessonProgress', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'LineManager', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'MailExemptedUsers', 
@role_name = NULL



EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'Module', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'Notification', 
@role_name = NULL


EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'Organization', 
@role_name = NULL


EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'QuizQuestion', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'QuizQuestionOption', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'QuizResponse', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'Region', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'Report', 
@role_name = NULL


EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'Survey', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'SurveyQuestion', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'SurveyQuestionOptions', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'SurveyQuestionResponse', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'SurveyTemplate', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'Ticket', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'Training', 
@role_name = NULL


EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'TrainingBudgetExpenditure', 
@role_name = NULL


EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'TrainingPeriod', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'TrainingRequest', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'TrainingVideo', 
@role_name = NULL


EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'UserCertificate', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'UserCourse', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'UserLessonQuiz', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'UserSurvey', 
@role_name = NULL

EXEC sys.sp_cdc_enable_table 
@source_schema = 'dbo', 
@source_name = 'UserTraining', 
@role_name = NULL


GO


CREATE PROCEDURE [dbo].[Sp_lms_cdc]
(
    @xml xml,
	@fromdate datetime,
	@todate datetime,
	@operation varchar(10),
	@name varchar(Max),
	@pageNo int = 0,
    @pageSize int
)
AS

BEGIN

	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	DECLARE @Names VARCHAR(8000) 
    SELECT Id into #tmpusers FROM AspNetUsers WHERE concat(FIRSTname,' ', Lastname) like '%'+@name+'%' or @name is null

	if (@operation = '0')
	BEGIN 
		SET @operation = '1,2,3,4';
	END
	if (@operation = '3')
	BEGIN 
		SET @operation = '3,4';
	END
	if (@operation = '1')
	BEGIN 
		SET @operation = '1,3,4';
	END

	if (@FROMDATE is null and @TODATE is not null)
	BEGIN 
		SET @FROMDATE = @TODATE;
	END
	if (@TODATE is null and @FROMDATE is not null)
	BEGIN 
		SET @TODATE = @FROMDATE;
	END
	--DECLARE @xmlstr XML;
	--SET @xmlstr = CAST(@xml AS XML);

	SELECT IDENTITY(int, 1, 1) AS RowID,
	t.value('(tablename/text())[1]','nvarchar(120)')AS tablename ,
	t.value('(cdctablename/text())[1]','nvarchar(120)')AS cdctablename into #tmpxmldata
	FROM
	@xml.nodes('ArrayOfArrayOfEntities/ArrayOfEntities/entities')AS TempTable(t) where t.value('(tablename/text())[1]','nvarchar(120)') in 
	(SELECT tb.name AS tablename
	FROM sys.tables tb
	INNER JOIN sys.schemas s on s.schema_id = tb.schema_id
	WHERE tb.is_tracked_by_cdc = 1 and COL_LENGTH(tb.name, 'LastModifiedById') is not null)

	--SELECT IDENTITY(int, 1, 1) AS RowID, tb.name AS tablename, CONCAT('cdc.dbo_',tb.name,'_CT') AS cdctablename into #tmpxmldata
	--FROM sys.tables tb
	--INNER JOIN sys.schemas s on s.schema_id = tb.schema_id
	--WHERE tb.is_tracked_by_cdc = 1 and COL_LENGTH(tb.name, 'LastModifiedById') is not null

	DECLARE @r int =1
	DECLARE @t int =1
	SELECT @t=count(1) from #tmpxmldata 

	while (@r<=@t)
	begin
	DECLARE @TableName varchar(50)
	DECLARE @cdcTableName varchar(50)
	DECLARE @CaptureInstnce as varchar(200)
	SELECT @TableName = tablename FROM #tmpxmldata where RowID = @r
	SELECT @cdcTableName = cdctablename FROM #tmpxmldata where RowID = @r
	
	SELECT @CaptureInstnce = b.capture_instance FROM sys.tables a, cdc.change_tables b
						   where a.object_id = b.source_object_id and name = @TableName and 
						   is_tracked_by_cdc = 1

	DECLARE @strQuery nVarchar(Max)

	SET @strQuery = 'SELECT IDENTITY(int, 1, 1) AS RowID,sys.fn_cdc_map_lsn_to_time(__$start_lsn) AS ''ChangedDateTime'', * ,
    ( SELECT    Co.column_name + '',''
        FROM      cdc.captured_columns Co
                INNER JOIN cdc.change_tables CT ON Co.[object_id] = CT.[object_id]
        WHERE     capture_instance = '''+@CaptureInstnce+'''
                AND sys.fn_cdc_is_bit_set(Co.column_ordinal,
                                            CC.__$update_mask) = 1
    FOR
        XML PATH('''')
    ) AS changedcolumns
	into tmpdatawithchangedcolumns 
	FROM '+@cdcTableName+' CC WHERE __$operation 
	in(' + CAST(@operation as nvarchar(20)) +') '

	DECLARE @strQuery1 nVarchar(Max)
	
	SET @strQuery1 = ' and sys.fn_cdc_map_lsn_to_time(__$start_lsn) BETWEEN ''' + CONVERT(VARCHAR(10),@FROMDATE, 101) + ''' and ''' + CONVERT(VARCHAR(10),DATEADD(DD,1,@TODATE), 101) + ''' order by __$operation desc'

	DECLARE @strQuery2 nVarchar(Max)
	
	SET @strQuery2 = ' order by __$operation desc'
	
	if (@FROMDATE is null and @TODATE is null)
	begin
		EXEC(@strQuery + @strQuery2)
	end
	else
	begin
		EXEC(@strQuery + @strQuery1)
	end
	
	DECLARE @th int
	SELECT @th=count(1) from tmpdatawithchangedcolumns where isdeleted = 1

	if ((@operation = '1,3,4' or @operation = '1,2,3,4') and  @th > 0)
	begin 

	update tmpdatawithchangedcolumns set changedcolumns =
	 (SELECT    column_name + ','
        FROM  information_schema.columns
                
        WHERE   table_name = @TableName
    FOR
        XML PATH(''))

	end

	

	SELECT IDENTITY(int, 1, 1) AS RowIDdd, @TableName as Tablename, * into #tmpsplitted from
	tmpdatawithchangedcolumns x cross apply 
    dbo.SplitStringIntoRows(SUBSTRING(x.changedcolumns,1,len(x.changedcolumns) - 1)) Y;	

	drop table tmpdatawithchangedcolumns

	/*ITERATION THROUGH COLUMNS*/
	DECLARE @temp TABLE
	(
		RowIDDd int,
		isdeleted bit,
		ChangedDateTime datetime,
		tablename	varchar(50),
		Id	int,
		OperationId varbinary(MAX),
		UpdatedData varchar(MAX),
		operation int,
		[Column Name] varchar(MAX)
	)

	DECLARE @row int =1
	DECLARE @tot int =1
	DECLARE @col varchar(MAX)
	DECLARE @q varchar(MAX)
	SELECT @tot=count(1) from #tmpsplitted 

	while (@row<=@tot)
	begin
		SELECT @col=Column1 from #tmpsplitted where RowIDdd=@row
		if (@col != 'LastModifiedByDate' and @col != 'LastModifiedById')
		begin
		SET @q='select RowIDDd,isdeleted,ChangedDateTime, tablename,LastModifiedById as Id,__$start_lsn as ''OperationId'',['+@col +'] as ''Updated Data'' , __$operation as ''Operation'',Column1 as ''Column Name'' FROM #tmpsplitted where RowIDDd = '+convert(varchar(10),@row)
		end
		INSERT into @temp
		EXEC(@q)
		SET @row=@row+1
	end

	DROP table #tmpsplitted
	SET @r=@r+1
	END	
	--select * from @temp


SELECT  CAST(ROW_NUMBER() OVER(ORDER BY ChangedDateTime DESC) AS int) AS SeqId,
	CAST(RANK() OVER (ORDER BY ChangedDateTime DESC) AS int) AS Trn#,a.ChangedDateTime,concat(b.FIRSTname,' ', b.Lastname) as Name,
	 a.Tablename,a.[Column Name] as ColumnName, Operation,isdeleted, a.BeforeUpdate, a.AfterUpdate into #tmpfinallisttable from
     (SELECT distinct RowIDDd,Id,OperationId,isdeleted,ChangedDateTime,tablename,'INSERT' 'Operation',NULL 'BeforeUpdate',UpdatedData 'AfterUpdate',[Column Name] from @temp a
	 where operation=2 and ([Column Name] like '%Name%' or [Column Name] in ('Title','CreatedById'))
	 union all
	 SELECT distinct a.RowIDDd,b.Id,a.OperationId,b.isdeleted,a.ChangedDateTime,a.tablename,'UPDATE' 'Operation',a.UpdatedData 'BeforeUpdate',b.UpdatedData 'AfterUpdate',a.[Column Name] from @temp a
	 inner join @temp b on a.OperationId=b.OperationId and a.[Column Name]=b.[Column Name]
	 where a.operation=3  and b.operation=4 and b.isdeleted = 0
	 union all
	 SELECT distinct a.RowIDDd,b.Id,a.OperationId,b.isdeleted,a.ChangedDateTime,a.tablename,'DELETE' 'Operation',a.UpdatedData 'BeforeUpdate',NULL 'AfterUpdate',a.[Column Name] from @temp a
	 inner join @temp b on a.OperationId=b.OperationId and a.[Column Name]=b.[Column Name]
	 where a.operation=3  and b.operation=4 and b.isdeleted = 1 and (@operation='1,3,4' or @operation='1,2,3,4')
		and (a.[Column Name] like '%Name%' or a.[Column Name] in ('Title','CreatedById'))
	 union all
     SELECT distinct c.RowIDDd,c.Id,c.OperationId,c.isdeleted,c.ChangedDateTime,c.tablename,'DELETE' 'Operation',c.UpdatedData 'BeforeUpdate',NULL 'AfterUpdate',c.[Column Name] from @temp c
	 where operation=1
	 and (c.[Column Name] like '%Name%' or c.[Column Name] in ('Title','CreatedById'))) a inner join ASPNetUsers b on a.Id = b.Id where a.Id in (Select Id from #tmpusers) or @name is null

	 
	 update a set a.AfterUpdate = concat(b.FIRSTname,' ', b.Lastname) from #tmpfinallisttable a inner join AspNetUsers b on a.afterupdate = b.Id where 
	 columnname = 'CREATEDBYID' and a.afterupdate is not null
	 
	 update a set a.BeforeUpdate = concat(b.FIRSTname,' ', b.Lastname) from #tmpfinallisttable a inner join AspNetUsers b on a.BeforeUpdate = b.Id where 
	 columnname = 'CREATEDBYID' and a.BeforeUpdate is not null

	 declare @y int
	 select @y = count(*) from #tmpfinallisttable 
	 select @y as TotalRecords, * from #tmpfinallisttable order by SeqId OFFSET (@pageSize * @pageNo) ROWS FETCH NEXT @pageSize ROWS ONLY;
	 Drop table #tmpfinallisttable
	 DROP table #tmpusers
END
GO


