CREATE PROCEDURE Sp_AssignSurvey
	
	@LearningGroupId int, @AssignedIds nvarchar(max), @UserId int,@OrganizationId int
AS
BEGIN
	
	SET NOCOUNT ON;
	
	CREATE  Table #AssignedItems(
	ID int, UserId int ,C1 int, C2 int
	)

	BEGIN TRY
	
	BEGIN TRANSACTION;
	
	insert into #AssignedItems
	exec SP_GetColumns @AssignedIds,',','',''
    
	
	DECLARE @SurveyId as Bigint;
	DECLARE @AssignedSurveyCursor as CURSOR;
 
	SET @AssignedSurveyCursor = CURSOR FAST_FORWARD FOR
	Select distinct UserId from #AssignedItems;
	OPEN @AssignedSurveyCursor;
	FETCH NEXT FROM @AssignedSurveyCursor INTO @SurveyId;
	WHILE @@FETCH_STATUS = 0

	BEGIN
		
		--- check if exam has already been assigned to users
		 if (select count(id) from LearningGroupSurvey where SurveyId = @SurveyId) = 0
			BEGIN
				insert into LearningGroupSurvey (SurveyId,LearningGroupId,CreatedById,LastModifiedById,ModifiedDate,CreatedDate,IsDeleted)
				select @SurveyId,@LearningGroupId,@UserId,@UserId,GETDATE(),GETDATE(),0
			
			END
			
				--- insert into User Examination to assign to users
				insert into UserSurvey(Surveyid,UserId,IsFinished,CreatedById,
				LastModifiedById,ModifiedDate,CreatedDate,IsDeleted)
				
				select @SurveyId,U.UserId,0,@UserId,@UserId,GETDATE(),GETDATE(),0
				 from dbo.GetUsersByLearningGroupID(@LearningGroupId,@OrganizationId) U
				 left join UserSurvey UE on U.UserId = Ue.UserId
				 where UE.UserId is null

     FETCH NEXT FROM @AssignedSurveyCursor INTO @SurveyId;

	END

	CLOSE @AssignedSurveyCursor;
	DEALLOCATE @AssignedSurveyCursor;

			if @@trancount > 0
			BEGIN
				insert into MessageQueue(IsProccessed,NotificationType,ActionId,Comments,AdditionalUsers,OrganizationId,
				CreatedById,LastModifiedById,ModifiedDate,CreatedDate,IsDeleted)
					select 0,NotificationType,@LearningGroupId,null,null,@OrganizationId,@UserId,@UserId,getdate(),GETDATE(),0
					from NotificationTypes where NotificationAction = 'Survey Assignment';
			END
				
	drop table #AssignedItems
	COMMIT TRANSACTION;

	END TRY
	BEGIN CATCH
		declare @ErrorMessage nvarchar(max), 
			 @ErrorSeverity int, 
			 @ErrorState int;
			 select @ErrorMessage = ERROR_MESSAGE() + ' Line ' + cast(ERROR_LINE() as nvarchar(5)), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();

			if @@trancount > 0
				rollback transaction;

				drop table #AssignedItems
				raiserror (@ErrorMessage, @ErrorSeverity, @ErrorState);
	END CATCH; 

	END
	GO


--Here
CREATE PROCEDURE Sp_AssignTraining
	
	@LearningGroupId int, @AssignedIds nvarchar(max), @UserId int,@OrganizationId int
AS
BEGIN
	
	SET NOCOUNT ON;
	
	CREATE  Table #AssignedItems(
	ID int, UserId int ,C1 int, C2 int
	)

	BEGIN TRY
	
	BEGIN TRANSACTION;
	
	insert into #AssignedItems
	exec SP_GetColumns @AssignedIds,',','',''
    
	
	DECLARE @TrainingId as Bigint;
	DECLARE @AssignedTrainingCursor as CURSOR;
 
	SET @AssignedTrainingCursor = CURSOR FAST_FORWARD FOR
	Select distinct UserId from #AssignedItems;
	OPEN @AssignedTrainingCursor;
	FETCH NEXT FROM @AssignedTrainingCursor INTO @TrainingId;
	WHILE @@FETCH_STATUS = 0

	BEGIN
		
		--- check if exam has already been assigned to users
		 if (select count(id) from LearningGroupTraining where TrainingId = @TrainingId) = 0
			BEGIN
				insert into LearningGroupTraining (TrainingId,LearningGroupId,CreatedById,LastModifiedById,ModifiedDate,CreatedDate,IsDeleted)
				select @TrainingId,@LearningGroupId,@UserId,@UserId,GETDATE(),GETDATE(),0
			
			END
			
				--- insert into User Examination to assign to users
				insert into UserTraining(UserId,TrainingId,IsRequested,OrganizationId, CreatedById,
				LastModifiedById,ModifiedDate,CreatedDate,IsDeleted)
				
				select U.UserId,@TrainingId,0,@OrganizationId,@UserId,@UserId,GETDATE(),GETDATE(),0
				 from dbo.GetUsersByLearningGroupID(@LearningGroupId,@OrganizationId) U
				 left join UserTraining UE on U.UserId = Ue.UserId
				 where UE.UserId is null

     FETCH NEXT FROM @AssignedTrainingCursor INTO @TrainingId;

	END

	CLOSE @AssignedTrainingCursor;
	DEALLOCATE @AssignedTrainingCursor;

			if @@trancount > 0
			BEGIN
				insert into MessageQueue(IsProccessed,NotificationType,ActionId,Comments,AdditionalUsers,OrganizationId,
				CreatedById,LastModifiedById,ModifiedDate,CreatedDate,IsDeleted)
					select 0,NotificationType,@LearningGroupId,null,null,@OrganizationId,@UserId,@UserId,getdate(),GETDATE(),0
					from NotificationTypes where NotificationAction = 'Training Assignment';
			END
				
	drop table #AssignedItems
	COMMIT TRANSACTION;

	END TRY
	BEGIN CATCH
		declare @ErrorMessage nvarchar(max), 
			 @ErrorSeverity int, 
			 @ErrorState int;
			 select @ErrorMessage = ERROR_MESSAGE() + ' Line ' + cast(ERROR_LINE() as nvarchar(5)), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();

			if @@trancount > 0
				rollback transaction;

				drop table #AssignedItems
				raiserror (@ErrorMessage, @ErrorSeverity, @ErrorState);
	END CATCH; 

	END
	GO



--Here
CREATE PROCEDURE Sp_AssignExamination
	
	@LearningGroupId int, @AssignedIds nvarchar(max), @UserId int,@OrganizationId int
AS
BEGIN
	
	SET NOCOUNT ON;

	CREATE  Table #AssignedItems(
	ID int, UserId int ,C1 int, C2 int
	)
	
	BEGIN TRY
	
	BEGIN TRANSACTION;
	

	insert into #AssignedItems
	exec SP_GetColumns @AssignedIds,',','',''
    
	
	DECLARE @ExamId as Bigint;
	DECLARE @AssignedExamCursor as CURSOR;
 
	SET @AssignedExamCursor = CURSOR FAST_FORWARD FOR
	Select distinct UserId from #AssignedItems;
	OPEN @AssignedExamCursor;
	FETCH NEXT FROM @AssignedExamCursor INTO @ExamId;
	WHILE @@FETCH_STATUS = 0

	BEGIN
		
		--- check if exam has already been assigned to users
		 if (select count(id) from LearningGroupExam where ExaminationId = @ExamId) = 0
			BEGIN
				insert into LearningGroupExam (ExaminationId,LearningGroupId,CreatedById,LastModifiedById,ModifiedDate,CreatedDate,IsDeleted)
				select @ExamId,@LearningGroupId,@UserId,@UserId,GETDATE(),GETDATE(),0
			
			END
			
				--- insert into User Examination to assign to users
				insert into UserExamination(UserId,Attempt,ExaminationId,CreatedById,
				LastModifiedById,ModifiedDate,CreatedDate,IsDeleted,ExtraAttempt,LearningGroupId)
				
				select U.UserId,1,@ExamId,@UserId,@UserId,GETDATE(),GETDATE(),0,0,@LearningGroupId
				 from dbo.GetUsersByLearningGroupID(@LearningGroupId,@OrganizationId) U
				 left join UserExamination UE on U.UserId = Ue.UserId
				 where UE.UserId is null

     FETCH NEXT FROM @AssignedExamCursor INTO @ExamId;

	END

	CLOSE @AssignedExamCursor;
	DEALLOCATE @AssignedExamCursor;

			if @@trancount > 0
			BEGIN
				insert into MessageQueue(IsProccessed,NotificationType,ActionId,Comments,AdditionalUsers,OrganizationId,
				CreatedById,LastModifiedById,ModifiedDate,CreatedDate,IsDeleted)
					select 0,NotificationType,@LearningGroupId,null,null,@OrganizationId,@UserId,@UserId,getdate(),GETDATE(),0
					from NotificationTypes where NotificationAction = 'Examination Assignment';
			END
				
	drop table #AssignedItems
	COMMIT TRANSACTION;

	END TRY
	BEGIN CATCH
		declare @ErrorMessage nvarchar(max), 
			 @ErrorSeverity int, 
			 @ErrorState int;
			 select @ErrorMessage = ERROR_MESSAGE() + ' Line ' + cast(ERROR_LINE() as nvarchar(5)), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();

			if @@trancount > 0
				rollback transaction;

				drop table #AssignedItems
				raiserror (@ErrorMessage, @ErrorSeverity, @ErrorState);
	END CATCH; 
	END
	GO