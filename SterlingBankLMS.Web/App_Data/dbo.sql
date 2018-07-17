IF object_id('[dbo].[User_AspNetUsers]') IS NULL
BEGIN
	EXECUTE ('ALTER TABLE [dbo].[User] ADD CONSTRAINT [User_AspNetUsers] FOREIGN KEY ([ApplicationUserId]) REFERENCES [dbo].[AspNetUsers] ([Id])') 
END


IF object_id('[dbo].[Sp_GetCoursesList]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[Sp_GetCoursesList]
    @organizationId [int],
    @Search [nvarchar](150),
    @pageNo [int] = 0,
    @pageSize [int]
AS
BEGIN
    SET NOCOUNT ON;
    
    					DECLARE @Total INT = (SELECT Count(id)
    			   FROM   Course
    			   WHERE  OrganizationId = @organizationId
    					  AND IsDeleted = 0)
    
    			SET @Search = (SELECT Ltrim(Rtrim(@Search)))
    
    			SELECT DISTINCT a.Id,
    							a.[Name],
    							c.[Name]    AS LearningAreaName,
    							a.[Description],
    							a.DueDate,
    							--a.ExamRetakeCount,
    							a.ImageUrl,
    							--a.PassGrade,
    							a.Overview,
    							a.Objectives,
								a.IsPublished,
    							a.HasCertificate,
    							Count(b.Id) AS ModuleCount,
    							@Total      TotalRecords
    			FROM   Course a
    				   LEFT JOIN Module b
    						  ON a.Id = b.CourseId
    							 AND b.IsDeleted = 0
    				   INNER JOIN LearningArea c
    						   ON a.LearningAreaId = c.Id
    			WHERE  a.OrganizationId = @OrganizationID
    				   AND a.IsDeleted = 0
    				   AND ( ( @Search IS NULL )
    						  OR ( @Search IS NOT NULL
    							   AND ( a.[Name] LIKE ''%'' + @Search + ''%''
    									  OR c.[Name] LIKE ''%'' + @Search + ''%'' ) ) )
    			GROUP  BY a.id,
    					  a.[Name],
    					  c.[Name],
    					  a.[Description],
    					  a.DueDate,
    					  --a.ExamRetakeCount,
    					  a.ImageUrl,
    					  --a.PassGrade,
    					  a.Overview,
    					  a.Objectives,
						  a.IsPublished,
    							a.HasCertificate
    			ORDER  BY a.Id desc
    			OFFSET (@pageSize * @pageNo) ROWS FETCH NEXT @pageSize ROWS ONLY
END')
END


IF object_id('[dbo].[Sp_GetEmployeeCertificates]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[Sp_GetEmployeeCertificates]
			@id [int],
			@organizationId [int]
		AS
		BEGIN
    						SELECT c.id,
							       c.[Name],
								   c.ExpiryDate,
								   a.FirstName,
								   a.LastName,
								   a.StaffId,
								   c.CertificateApprovalStatus,
								   c.DateObtained,
								   c.TemplateUrl,
								   a.Id as UserId,
								   d.[Name] as deptName,
								   c.[Description]
    						FROM  UserCertificate c
    							   JOIN AspNetUsers a
    								 ON a.Id = c.UserId
								   JOIN Department d
								     ON a.DepartmentId=d.Id
    						WHERE  a.id = @id
    							   AND a.OrganizationId = @organizationId
		END')
END


IF object_id('[dbo].[SP_GetEmployeeDetails]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[SP_GetEmployeeDetails]
					@employeeId [int],
					@organizationId [int]
				AS
				BEGIN
    								SELECT a.StaffId,
    									   a.Id,
    									   a.UserName,
    									   a.PictureUrl,
    									   a.Email,
    									   a.PhoneNumber,
    									   a.DateOfEmployment,
    									   a.FirstName,
    									   a.LastName,
    									   a.Gender,
    									   d.[Name] department,
    									   a.Functions,
    									   a.DateOfBirth,
										   gr.Name as Grade,
    									   b.[NAME] Branch,
										   a.LineManagerFirstName,
										   a.LineManagerLastName,
										   a.LIneManagerStaffId
    								FROM   AspNetUsers a
    									   LEFT JOIN Department d
    											  ON a.departmentId = d.Id
    									   LEFT JOIN branch b
    											  ON b.id = a.BranchId
										   LEFT JOIN Grade gr
										          ON gr.Id=a.GradeId
    									   LEFT JOIN [Group] g
    											  ON g.Id = a.GroupId
    								WHERE  a.id = @employeeId and a.OrganizationId = @organizationId 
 

				END')
END


IF object_id('[dbo].[SP_GetEmployeeList]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[SP_GetEmployeeList]
							@pageNo [int] = 1,
							@pageSize [int],
							@organizationId [int],
							@keyword [nvarchar](150),
							@group [int]
						AS
						BEGIN
							SET NOCOUNT ON;
    
    										  SELECT a.Id,
    												 StaffId,
    												 FirstName,
    												 LastName,
    												 g.[NAME]    [Group],
    												 b.[NAME]    Branch,

    												 Count(a.id) OVER () TotalCount
    										  FROM   AspnetUsers a
    												 LEFT JOIN [Group] g
    														ON a.GroupId = g.Id
    												 LEFT JOIN branch b
    														ON b.id = a.BranchId
    										  WHERE  a.OrganizationId = @organizationId
    												 AND (  @keyword IS NULL 
    														OR a.FirstName LIKE ''%'' + @keyword + ''%''
    														OR a.LastName LIKE ''%'' + @keyword + ''%''
    														OR a.UserName LIKE ''%'' + @keyword + ''%''
    														OR a.Email LIKE ''%'' +  @keyword+ ''%'' )
    												 AND (@group IS NULL
    												  OR a.GroupId = @group)
    										  ORDER  BY a.id desc
    										  OFFSET (@pageSize * (@pageNo-1)) ROWS FETCH NEXT @pageSize ROWS ONLY
						END')
END


IF object_id('[dbo].[Sp_GetEmployeeTrainingRecords]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[Sp_GetEmployeeTrainingRecords]
    @id [int],
    @organizationId [int]
AS
BEGIN
    				  SELECT a.Id as UserId,
					         t.[Name] as TrainingName,
					         t.Id as TrainingId,
					         a.FirstName,
							 a.LastName,
							 a.StaffId,
							 d.[Name] as DepartmentName,
					         t.AmountPerStaff,
							 t.[Description],
							 t.[Location],
							 t.Venue,
							 t.StartPeriod,
							 t.EndPeriod,
    						 ut.Id as UserTrainingId
    				  FROM   [User] u
    						 JOIN UserTraining ut
    						   ON u.id = ut.UserId
    						 JOIN Training t
    						   ON ut.TrainingId = t.Id
    						 JOIN AspNetUsers a
    						   ON a.Id = u.ApplicationUserId
							 JOIN Department d
							   ON d.Id=a.DepartmentId
    				  WHERE  a.id = @id
    						 AND a.OrganizationId = @organizationId
END')
END


IF object_id('[dbo].[SP_GetTrainingNames]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[SP_GetTrainingNames]
    @organizationId [int]
AS
BEGIN
    		     SELECT distinct Id,[Name]  from Training  
    
    	          WHERE OrganizationId = @OrganizationId  
    
    	          order by  [Name];
END')
END


IF object_id('[dbo].[SP_GetRegionNames]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[SP_GetRegionNames]
    @organizationId [int]
AS
BEGIN
    		     select distinct Id,[Name]  from Region  
    
    	where OrganizationId = @OrganizationId  
    
    	order by  [Name];
END')
END


IF object_id('[dbo].[SP_GetNames]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[SP_GetNames]
    @organizationId [int]
AS
BEGIN
select distinct Id,FirstName,LastName  from AspNetUsers  
    
    	where OrganizationId = @OrganizationId  
    
    	order by  FirstName;
END')
END


IF object_id('[dbo].[SP_GetGroupNames]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[SP_GetGroupNames]
    @organizationId [int]
AS
BEGIN
 select distinct Id,[Name]  from [Group]  
    
    	where OrganizationId = @OrganizationId  
    
    	order by  [Name];
END')
END

IF object_id('[dbo].[SP_GetGradeNames]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[SP_GetGradeNames]
    @organizationId [int]
AS
BEGIN
 select distinct Id,[Name]  from Grade  
    
    	where OrganizationId = @OrganizationId  
    
    	order by  Id;
END')
END


IF object_id('[dbo].[SP_GetDepartmentBudget]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[SP_GetDepartmentBudget]
    @OrganizationId [int],
    @Search [nvarchar](150),
    @pageNo [int] = 0,
    @pageSize [int]
AS
BEGIN
        declare @Total int = (select count(id) from DepartmentBudget where OrganizationId = 1 and IsDeleted = 0)
    
    	set @Search = (select Ltrim(Rtrim(@Search)))
    
    	select distinct 
		              b.Id,
					  g.Id as GroupId,
					  r.Id as RegionId,
					  g.[Name] as GroupName,
					  r.[Name] as RegionName,
					  b.Budget,
					  b.AmountSpent,
					  b.[Year],
					  @Total TotalRecords  
        from DepartmentBudget b
    	inner join [Group] g on g.Id = b.GroupId
		inner join Region r on r.Id=b.RegionId
    
    	where b.OrganizationId = @OrganizationId and b.IsDeleted = 0 and ((@Search IS NULL) or (@Search is not null and (g.[Name] like ''%''+@Search+''%''  )) or b.[Year] like ''%''+@Search+''%'' )
    
    	order by g.[Name]
    
    	OFFSET (@pageSize * @pageNo) ROWS FETCH NEXT @pageSize ROWS ONLY;
END')
END


IF object_id('[dbo].[SP_GetTrainingList]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[SP_GetTrainingList]
    @OrganizationId [int],
    @Search [nvarchar](150),
    @pageNo [int] = 0,
    @pageSize [int]
AS
BEGIN
        declare @Total int = (select count(Id) from Training where OrganizationId = 1 and IsDeleted = 0)

	set @Search = (select Ltrim(Rtrim(@Search)))

	select distinct Id,[Name] ,[Description],StartPeriod,[Location],
	
	Vendor,Venue,TrainingCategory,TrainingType,CreatedDate,@Total TotalRecords  from Training 

	where OrganizationId = @OrganizationId and IsDeleted = 0 and ((@Search IS NULL) or (@Search is not null and ([Name] like ''%''+@Search+''%'' or [Name] like ''%''+@Search+''%'' ))  )

	order by Id desc

	OFFSET (@pageSize * @pageNo) ROWS FETCH NEXT @pageSize ROWS ONLY;
END')
END


IF object_id('[dbo].[SP_GetTrainingVideo]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[SP_GetTrainingVideo]
    @OrganizationId [int],
    @Search [nvarchar](150),
    @pageNo [int] = 0,
    @pageSize [int]
AS
BEGIN
     declare @Total int = (select count(Id) from TrainingVideo where OrganizationId = 1 and IsDeleted = 0)

	set @Search = (select Ltrim(Rtrim(@Search)))

	select distinct Id,TrainingVideoUrl ,TrainingVideoName,ImageUrl,CreatedDate,@Total TotalRecords  from TrainingVideo 

	where OrganizationId = @OrganizationId and IsDeleted = 0 and ((@Search IS NULL) or (@Search is not null and (TrainingVideoName like ''%''+@Search+''%'' or TrainingVideoName like ''%''+@Search+''%'' ))  )

	order by TrainingVideoName

	OFFSET (@pageSize * @pageNo) ROWS FETCH NEXT @pageSize ROWS ONLY;
END')
END


IF object_id('[dbo].[SP_GetTrainingRequestList]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[SP_GetTrainingRequestList]
    @OrganizationId [int],
    @Search [nvarchar](150),
    @pageNo [int] = 0,
    @pageSize [int]
AS
BEGIN
     DECLARE @Total INT = (SELECT Count(id)
    			   FROM   TrainingRequest
    			   WHERE  OrganizationId = @organizationId
    					  AND IsDeleted = 0)
				   select distinct
				      x.Id, 
					   a.FirstName, 
					   a.LastName,
					   a.StaffId,
					   a.Gender,
					   d.Id as DepartmentId,
					   d.[Name] as DepartmentName,
					   g.[Name] as GradeName,
					   gr.[Name] as GroupName, 
					   br.[Name] as BranchName, 
					   r.[Name] as RegionName,
					   z.ApplicationUserId, 
					   x.TrainingApprovalStatus,
					   x.CreatedDate,
					   x.ReasonForRequest,
					   y.[Name] as TrainingName, 
					   y.Id as TrainingId,
					   y.AmountPerStaff,
					   y.BudgetExpended ,
					   @Total  TotalRecords
				   from 
				   TrainingRequest x 
				                       inner join Training y on x.TrainingId=y.Id
									   inner join [User] z on x.UserId = z.Id
									   inner join AspNetUsers a on a.Id = z.ApplicationUserId
									   inner join Department d on a.DepartmentId=d.Id
	                                   inner join Region r on a.RegionId=r.Id
				        			   inner join [Group] gr on a.GroupId=gr.Id
									   inner join Branch br on a.BranchId=br.Id
									   inner join Grade g on a.GradeId=g.Id 

									   WHERE  x.OrganizationId = @OrganizationID 
    				   AND x.IsDeleted = 0
    				   AND ( ( @Search IS NULL )
    						  OR ( @Search IS NOT NULL
    							   AND ( a.FirstName LIKE ''%'' + @Search + ''%''
    									  OR a.LastName LIKE ''%'' + @Search + ''%'' OR gr.[Name] LIKE ''%''+@Search+''%'' OR d.[Name] LIKE ''%''+@Search+''%'' OR a.StaffId LIKE ''%''+@Search+''%'') ))

										  ORDER  BY x.TrainingApprovalStatus, x.CreatedDate desc
    			OFFSET (@pageSize * @pageNo) ROWS FETCH NEXT @pageSize ROWS ONLY;
END')
END


IF object_id('[dbo].[SP_GetTicketList]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[SP_GetTicketList]
    @OrganizationId [int],
    @Search [nvarchar](150),
    @pageNo [int] = 0,
    @pageSize [int]
AS
BEGIN
     DECLARE @Total INT = (SELECT Count(id)
    			   FROM   Ticket
    			   WHERE  OrganizationId = @organizationId
    					  AND IsDeleted = 0)
				  select distinct
				       x.Id as TicketId, 
					   x.ModifiedDate,
					   x.TicketDescription,
					   x.TicketStatus,
					   x.TicketTitle,
					   x.FilePath,
					   x.CreatedDate,
					   a.FirstName, 
					   a.LastName,
					   a.StaffId,
					   a.Gender,
					   d.[Name] as DepartmentName,
					   g.Name,
					   gr.[Name] as GroupName, 
					   br.[Name] as BranchName, 
					   r.[Name] as RegionName,
					   z.ApplicationUserId, 
					   @Total  TotalRecords
				   from 
				   Ticket x 
									   inner join [User] z on x.UserId = z.Id
									   inner join AspNetUsers a on a.Id = z.ApplicationUserId
									   inner join Department d on a.DepartmentId=d.Id
	                                   inner join Region r on a.RegionId=r.Id
				        			   inner join [Group] gr on a.GroupId=gr.Id
									   inner join Branch br on a.BranchId=br.Id
									   inner join Grade g on a.GradeId=g.Id 

									   WHERE  x.OrganizationId = @OrganizationID 
    				   AND x.IsDeleted = 0
    				   AND ( ( @Search IS NULL )
    						  OR ( @Search IS NOT NULL
    							   AND ( a.FirstName LIKE ''%'' + @Search + ''%''
    									  OR a.LastName LIKE ''%'' + @Search + ''%'' OR x.TicketTitle LIKE ''%''+@Search+''%'' OR d.[Name] LIKE ''%''+@Search+''%'' OR a.StaffId LIKE ''%''+@Search+''%'') ))

										  ORDER  BY x.CreatedDate desc
    			OFFSET (@pageSize * @pageNo) ROWS FETCH NEXT @pageSize ROWS ONLY;
END')
END


IF object_id('[dbo].[Sp_GetEmployeeAssignedCourses]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[Sp_GetEmployeeAssignedCourses] @userId           [INT],
																		  @organizationId   [INT],
																		  @availabilityType [INT],
																		  @deleted			[BIT]
					AS
					  BEGIN
						  DECLARE @table TABLE
							(
							   Id              INT,
							   LearningGroupId INT,
							   UserId          INT,
							   RoleId          INT,
							   TagType         INT,
							   TagValue           INT
							);

						  INSERT INTO @table
						  SELECT tag.Id,
								 tag.LearningGroupId,
								 aspu.Id,
								 CASE
								   WHEN tag.TagType = 4 THEN r.RoleId
								   ELSE 0
								 END       RoleId,
								 tag.TagType,
								 tag.TagValue
						  FROM   LearningGroupTag tag
								 LEFT JOIN AspNetUsers  aspu  
										ON ( ( tag.TagType = 1
											   AND tag.TagValue = aspU.GroupId )
											  OR ( tag.TagType = 2
												   AND tag.TagValue = aspU.Gender )
											  OR ( tag.TagType = 3
												   AND tag.TagValue = aspU.GradeId )
											  OR ( tag.TagType = 6
												   AND tag.TagValue = aspU.RegionId )
											  OR ( tag.TagType = 7
												   AND tag.TagValue = aspU.DepartmentId ) )
										   AND aspU.Id = @userId
						--roles
								LEFT JOIN (SELECT ApplicationUserId, ur.RoleId FROM [User] u JOIN AspNetUserRoles ur ON ur.UserId=u.Id) r
									ON aspU.id = r.ApplicationUserId
									   AND tag.TagType = 4
									   AND tag.TagValue = r.RoleId

						   WHERE  tag.TagType != 5;

						  DELETE FROM @table
						  WHERE  LearningGroupId IN (SELECT LearningGroupId
													 FROM   @table
													 WHERE  UserId IS NULL
															 OR RoleId IS NULL)

						  -- Individual
						  INSERT INTO @table
						  SELECT a.id,
								 a.LearningGroupId,
								 a.TagValue,
								 NULL RoleId,
								 a.TagType,
								 a.TagValue
						  FROM   LearningGroupTag a
						  WHERE  a.tagtype = 5
								 AND a.TagValue = @userId

						  SELECT DISTINCT c.Id,
										  c.NAME,
										  LEFT(c.ShortDescription, 200) + ''...''        [ShortDescription],
										  c.ImageUrl,
										  (select AVG(rating) from CourseReview cr where c.Id = cr.CourseId) AverageRating,
										  b.[Availability],
										  b.LearningGroupId,
										   CONVERT(NVARCHAR(15), c.DueDate, 106) DueDate
						  FROM   @table a
								 INNER JOIN LearningGroupCourse b
										 ON a.LearningGroupId = b.LearningGroupId
								 INNER JOIN Course c
										 ON b.CourseId = c.Id
						  WHERE ( c.DueDate IS NULL OR c.DueDate >= Sysdatetime())
								AND ( c.isdeleted = 0 OR ( c.IsDeleted = @deleted ) )
								AND ( @availabilityType IS NULL OR [Availability] = @availabilityType )
								AND c.OrganizationId=@organizationId
								AND c.IsPublished=1
								AND c.Id not In (select uc.CourseId Id from UserCourse uc where uc.UserId=@userId and uc.Completed=1 and uc.LearningGroupId=b.LearningGroupId)
								ORDER  BY [Availability]
					END')
  END


  IF object_id('[dbo].[Sp_GetUsersByLearningGroupID]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[Sp_GetUsersByLearningGroupID] 
 @LId int,
 @Search [nvarchar](150),
 @pageNo [int] = 0,
 @pageSize [int],
 @OrganizationId int
AS
BEGIN

		  DECLARE @table TABLE
							(
							   Id              INT,
							   LearningGroupId INT,
							   UserId          INT,
							   FirstName nvarchar(200),
							   LastName nvarchar(200),
							   DepartmentId INT,
							   BranchId INT,
							   RegionId INT,
							   GradeId INT,
							   GroupId INT,
							   Gender Int,
							   RoleId          INT,
							   TagType         INT,
							   TagValue           INT
							);

						  INSERT INTO @table
						  SELECT tag.Id,
								 tag.LearningGroupId,
								 aspu.Id,aspu.FirstName,aspu.LastName, aspu.DepartmentId,aspu.BranchId,aspu.RegionId,aspu.GradeId,aspu.GroupId,aspu.Gender,
								 CASE
								   WHEN tag.TagType = 4 THEN r.RoleId
								   ELSE 0
								 END       RoleId,
								 tag.TagType,
								 tag.TagValue
						  FROM   LearningGroupTag tag
								 LEFT JOIN AspNetUsers  aspu  
										ON ( ( tag.TagType = 1
											   AND tag.TagValue = aspU.GroupId )
											  OR ( tag.TagType = 2
												   AND tag.TagValue = aspU.Gender )
											  OR ( tag.TagType = 3
												   AND tag.TagValue = aspU.GradeId )
											  OR ( tag.TagType = 6
												   AND tag.TagValue = aspU.RegionId )
											  OR ( tag.TagType = 7
												   AND tag.TagValue = aspU.DepartmentId ) )
										   AND tag.LearningGroupId = @LId
						--roles
								 LEFT JOIN AspNetUserRoles r
										ON aspU.id = r.UserId
										   AND tag.TagType = 4
										   AND tag.TagValue = r.RoleId

						   WHERE  tag.TagType != 5 and aspu.OrganizationId = @OrganizationId;

						  DELETE FROM @table
						  WHERE  LearningGroupId IN (SELECT LearningGroupId
													 FROM   @table
													 WHERE  UserId IS NULL
															 OR RoleId IS NULL)

						  -- Individual
						  INSERT INTO @table
						  SELECT a.id,
								 a.LearningGroupId,
								 a.TagValue,aspu.FirstName,aspu.LastName,aspu.DepartmentId,aspu.BranchId,aspu.RegionId,aspu.GradeId,aspu.GroupId,aspu.Gender,
								 NULL RoleId,
								 a.TagType,
								 a.TagValue
						  FROM   LearningGroupTag a
						  inner join AspNetUsers aspu on a.TagValue = aspu.Id
						  WHERE  a.tagtype = 5
								 AND a.LearningGroupId = @LId and aspu.OrganizationId = @OrganizationId

					if(@pageSize is not null)
						begin
									DECLARE @Total INT = (SELECT Count(id)
    			   FROM   @table)

	select a.*,b.[Name] as Department, c.[Name] as Branch, d.[Name] as Region, e.[Name] as Grade, f.[Name] as [Group],@Total TotalRecords from @table a
	left join Department b on a.DepartmentId = b.Id
	left join Branch c on a.BranchId = c.Id
	left join Region d on a.RegionId = d.Id 
	left join grade e on a.GradeId = e.Id 
	left join [Group] f on a.GroupId = f.Id 
	where  ( ( @Search IS NULL )
    						  OR ( @Search IS NOT NULL
    							   and ((a.FirstName like ''%'' + @Search + ''%'') or (a.LastName like ''%'' + @Search + ''%'' )
								    or (b.[Name] like ''%'' + @Search + ''%'' ) or (c.[Name] like ''%'' + @Search + ''%'' ) 
									or (d.[Name] like ''%'' + @Search + ''%'' ) or (e.[Name] like ''%'' + @Search + ''%'' ))
									)
									)
ORDER BY a.id desc
OFFSET (@pageSize * @pageNo) ROWS FETCH NEXT @pageSize ROWS ONLY
						end
					
					else
						begin
								
	select a.*,b.[Name] as Department, c.[Name] as Branch, d.[Name] as Region, e.[Name] as Grade, f.[Name] as [Group],0 TotalRecords from @table a
	left join Department b on a.DepartmentId = b.Id
	left join Branch c on a.BranchId = c.Id
	left join Region d on a.RegionId = d.Id 
	left join grade e on a.GradeId = e.Id 
	left join [Group] f on a.GroupId = f.Id 
	
ORDER BY a.id desc

						end			 
	
END')
  END


    IF object_id('[dbo].[Sp_GetLearningGroupById]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[Sp_GetLearningGroupById] 
    @LearningGroupId int
AS
BEGIN
    SET NOCOUNT ON;
	SELECT m.id,m.[name],m.TagFormat, COALESCE(t1.ct, 0) CourseCount, COALESCE(t2.ct, 0) as TrainingCount , COALESCE(t3.ct, 0) as ExamCount, COALESCE(t4.ct, 0)  AS SurveyCount
	
	FROM   LearningGroup m 
LEFT   JOIN (
   SELECT LearningGroupId, count(*) AS ct
   FROM   LearningGroupCourse
   GROUP  BY LearningGroupId
   ) t1 ON t1.LearningGroupId = m.id

LEFT   JOIN (
   SELECT LearningGroupId, count(*) AS ct
   FROM   LearningGroupTraining
   GROUP  BY LearningGroupId
   ) t2 ON t2.LearningGroupId = m.id

LEFT   JOIN (
   SELECT LearningGroupId, count(*) AS ct
   FROM   LearningGroupExam
   GROUP  BY LearningGroupId
   ) t3 ON t3.LearningGroupId = m.id

   LEFT   JOIN (
   SELECT LearningGroupId, count(*) AS ct
   FROM   LearningGroupSurvey
   GROUP  BY LearningGroupId
   ) t4 ON t4.LearningGroupId = m.id

   	WHERE  m.id = @LearningGroupId

END')
  END


IF object_id('[dbo].[SP_GetEmployeeCourseProgress]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[Sp_GetEmployeeCourseProgress] @userId         [INT],
                                                     @organizationId [INT],
                                                     @deleted        [BIT]
							AS
							  BEGIN
								  WITH cte
									   AS (SELECT (SELECT Isnull(Count(lp.id), 0)
												   FROM   LessonProgress lp
												   WHERE  lp.UserCourseId = uc.Id
														  AND lp.IsDeleted = 0
														  AND lp.IsCompleted = 1) AS Completed,
												  (SELECT Isnull(Count (l.id), 0)
												   FROM   Lesson l
														  JOIN Module m
															ON m.CourseId = uc.CourseId
												   WHERE  l.ModuleId = m.id
														  AND l.IsDeleted = 0)    LessonsCounts,
												  la.[Name]                       LearningArea,
												  uc.Id,
												  uc.CourseStatus,
												  C.Name,
												  c.Id                            AS CourseId,
												  uc.CreatedDate,
												  uc.ModifiedDate
										   FROM   UserCourse uc
												  JOIN Course c
													ON c.id = uc.CourseId
												  JOIN LearningArea la
													ON la.Id = c.LearningAreaId
										   WHERE  uc.IsDeleted = 0
												  AND uc.UserId = @userId)
								  SELECT [Name],
										 Id,
										 CourseId,
										 Completed,
										 CourseStatus,
										 LessonsCounts,
										 cte.LearningArea,
										 CASE
										   WHEN LessonsCounts = 0 THEN 0
										   WHEN CourseStatus = 2 then 100.00
										   ELSE Cast(Isnull(Completed * 100.00 / LessonsCounts, 0)AS DECIMAL (18, 2))
										 END Progress
								  FROM   cte
								    ORDER BY ModifiedDate DESC, CreatedDate DESC
							  END')
END


IF object_id('[dbo].[SP_GetCourseDetails]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[Sp_GetCourseDetails] @courseId       [INT],
															@organizationId [INT],
															@deleted        [BIT]
				AS
				  BEGIN
					  SELECT c.Id,
							 CONVERT(NVARCHAR(15), c.DueDate, 106)     DueDate,
							 c.Description,
							 c.objectives,
							 c.ImageUrl,
							 ShortDescription,
							 c.Overview,
							 --c.PassGrade,
							 c.[Name],
							 --c.ExamRetakeCount,
							 c.EstimatedDuration,
							 c.HoursPerWeek,
							 --c.PassGrade,
							 c.CreatedDate,
							 (SELECT Isnull (Count(m.id), 0)
							  FROM   Module m
							  WHERE  m.CourseId = c.Id)              ModulesCount
					  FROM   Course c
					  WHERE  c.id = @courseId
							 AND c.OrganizationId = @organizationId
							 AND (c.DueDate IS NULL OR c.DueDate >= Sysdatetime())
							 AND c.IsPublished=1
				   AND ( c.isdeleted = 0 OR ( c.IsDeleted = @deleted ) )
				  END')
END


IF object_id('[dbo].[Sp_GetExaminationList]') IS NULL
BEGIN
	EXECUTE('create PROCEDURE [dbo].[Sp_GetExaminationList] 
    @organizationId [int],
    @Search [nvarchar](150),
    @pageNo [int] = 0,
    @pageSize [int]
AS
BEGIN
    SET NOCOUNT ON;
    
    			DECLARE @Total INT = (SELECT Count(id)
    			   FROM   Examination
    			   WHERE  OrganizationId = @organizationId
    					  AND IsDeleted = 0)
    
    			SET @Search = (SELECT Ltrim(Rtrim(@Search)))
    

	  

    			SELECT DISTINCT a.Id,
    							a.[Name],
								b.[Name] as Course,
								b.Id as CourseId,
    						    a.ExamRetakeCount,
							    a.HourLimit,
								MinuteLimit,
								a.ExamType,
								a.ImageUrl,
    							a.PassGrade,
								a.StartDate,
								a.EndDate,

    							Count(c.Id) AS QuestionCount,
    							@Total      TotalRecords
    			FROM   Examination a
				       left join Course b on a.CourseId = b.Id
					   left join ExaminationQuestion c on a.Id = c.ExaminationId and c.IsDeleted = 0
    				  
    			WHERE  a.OrganizationId = @organizationId
    				   AND a.IsDeleted = 0
    				   AND ( ( @Search IS NULL )
    						  OR ( @Search IS NOT NULL
    							   AND ( a.[Name] LIKE ''%'' + @Search + ''%'') ) )
    			GROUP  BY a.id,
    					  a.[Name],
    					  a.ExamRetakeCount,
								a.HourLimit,
								a.MinuteLimit,
								a.ExamType,
								a.ImageUrl,
    							a.PassGrade,
								b.[Name],b.Id,a.HourLimit,
								a.StartDate,
								a.EndDate
    			ORDER  BY a.Id desc
    			OFFSET (@pageSize * @pageNo) ROWS FETCH NEXT @pageSize ROWS ONLY
END')
END


IF Object_id('[dbo].[Sp_SearchCourses]') IS NULL
  BEGIN
      EXECUTE('CREATE PROCEDURE [dbo].[Sp_SearchCourses] (@q              NVARCHAR(150),
													  @filter       INT null,
													  @filterBy       INT NULL,
													  @organizationId INT,
													  @pageNo         INT,
													  @pageSize       INT,
													  @deleted        BIT)
			AS
			  BEGIN
				  
				  if(@pageSize is null)
				  begin
				  SELECT c.NAME,
								  c.ImageUrl,
								  c.Id,
								  la.NAME LearningArea,
								  c.createdDate,
								  CONVERT(NVARCHAR(15), c.DueDate, 106) DueDate,
								  c.ShortDescription                    [Description]
						   FROM   Course c JOIN LearningArea la
								   ON la.Id = c.LearningAreaId
					  
						   WHERE  c.OrganizationId = @organizationId
								  AND (@q IS NULL  OR c.NAME LIKE ''%'' + @q + ''%'')
								  AND ( c.isdeleted = 0 OR ( c.IsDeleted = @deleted ) )
								  AND (c.DueDate IS NULL OR c.DueDate >= Sysdatetime())
								  AND(@filterBy IS NULL OR @filterBy=1 AND @filter=la.Id )
								  AND c.IsPublished=1
				  end

				  else

				  begin

				  WITH result
					   AS (SELECT c.NAME,
								  c.ImageUrl,
								  c.Id,
								  la.NAME LearningArea,
								  c.createdDate,
								  CONVERT(NVARCHAR(15), c.DueDate, 106) DueDate,
								  (select AVG(rating) from CourseReview cr where c.Id = cr.CourseId) AverageRating,
								  c.ShortDescription                    [Description]
						   FROM   Course c JOIN LearningArea la
								   ON la.Id = c.LearningAreaId
					  
						   WHERE  c.OrganizationId = @organizationId
								  AND (@q IS NULL  OR c.NAME LIKE ''%'' + @q + ''%'')
								  AND ( c.isdeleted = 0 OR ( c.IsDeleted = @deleted ) )
								  AND (c.DueDate IS NULL OR c.DueDate >= Sysdatetime())
								  AND(@filterBy IS NULL OR @filterBy=1 AND @filter=la.Id )
								  AND c.IsPublished=1),
					   tcounter
					   AS (SELECT Count(*) OVER() AS Total
						   FROM   result)
				  SELECT result.*,
						 Isnull((SELECT Count(total)
								 FROM   tcounter), 0) TotalCount
				  FROM   result
				  ORDER  BY result.createdDate
				  OFFSET (@pageSize * (@pageNo-1)) ROWS FETCH NEXT @pageSize ROWS ONLY

				  end
			  END')
  END 


IF object_id('[dbo].[SP_GetAllUsers]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[SP_GetAllUsers]
    @OrganizationId [int],
    @Search [nvarchar](150),
    @pageNo [int] = 0,
    @pageSize [int]
AS
BEGIN
    DECLARE @Total INT = (SELECT Count(id)
    			   FROM   AspNetUsers
    			   WHERE  OrganizationId = @organizationId)
				  select distinct
						  u.ApplicationUserId as UserId,
						  x.Email,
						  x.FirstName,
						  x.LastName,
						  x.StaffId,
						  x.DateOfEmployment,
						  z.[Name] as RoleName,
						  z.IsActive,
					   @Total  TotalRecords
				  from [User] u inner join AspNetUsers x on u.ApplicationUserId=x.Id inner join AspNetUserRoles y on x.Id=y.UserId inner join AspNetRoles z on y.RoleId=z.Id
						  WHERE  x.OrganizationId = @OrganizationID 
    				   AND ( ( @Search IS NULL )
    						  OR ( @Search IS NOT NULL
    							   AND ( x.FirstName LIKE ''%'' + @Search + ''%''
    									  OR x.LastName LIKE ''%'' + @Search + ''%'' OR z.[Name] LIKE ''%''+@Search+''%'' OR x.StaffId LIKE ''%''+@Search+''%'') ))

										  ORDER  BY x.FirstName 
    			OFFSET (@pageSize * @pageNo) ROWS FETCH NEXT @pageSize ROWS ONLY;
END')
END


IF object_id('[dbo].[Sp_GetModuleCourse]') IS NULL
 BEGIN
 EXECUTE('CREATE PROCEDURE Sp_GetModuleCourse (@id INT, @orgId INT)
			AS
			  BEGIN
				  				  SELECT c.Id,
						 c.DueDate
				  FROM   Course c
				  WHERE  c.Id = @id
						 AND c.OrganizationId = @orgId
						 AND c.IsDeleted = 0
  END')
  END


IF object_id('[dbo].[Sp_GetLesson]') IS NULL
 BEGIN
 EXECUTE('CREATE PROCEDURE Sp_GetLesson (@id INT, @orgId INT)
			AS
			BEGIN
			  SELECT l.ContentUrl,
				   l.HtmlContent,
				   l.IsExternalContent,
				   l.[Name],
				   l.LessonContentType,
				   l.Id,
				   l.[Description],
				   l.MimeType,
				   l.SortOrder,
				   l.QuizRetakeCount,
				   c.Id CourseId,
				   m.Id ModuleId,
				   l.QuestionNo
			FROM   Course c
				   JOIN Module m
					 ON m.CourseId = c.Id
				   JOIN Lesson l
					 ON l.ModuleId = m.Id
			WHERE  l.Id = @id
				   AND c.OrganizationId = @orgId
				   AND l.IsDeleted = 0 
  END')
  END


IF object_id('[dbo].[Sp_getlessonquizzes]') IS NULL
 BEGIN
 EXECUTE('CREATE PROCEDURE Sp_GetLessonQuizzes (@lessonId INT,
											    @organizationId INT)
		AS
		  BEGIN
			  SELECT q.Id,
					 q.AnswerType,
					 q.Question,
					 q.SortOrder
			  FROM   QuizQuestion q
			  WHERE  q.LessonId = @lessonId
					 AND q.OrganizationId = @organizationId
		  END')
  END


IF object_id('[dbo].[Sp_GetLastQuizResponse]') IS NULL
 BEGIN
 EXECUTE('CREATE PROCEDURE [dbo].[Sp_GetLastQuizResponse] (@lessonId INT,
                                                @userId   INT)
				AS
				  BEGIN
					  SELECT TOP (1) qr.Id,
									 qr.Value,
									 qr.IsAnswer,
									 qr.QuizId,
									 qq.SortOrder
					  FROM   QuizResponse qr
							 JOIN QuizQuestion qq
							   ON qq.Id = qr.QuizId
							 JOIN Lesson l
							   ON l.Id = qq.LessonId
							 JOIN UserLessonQuiz ulq
							   ON ulq.LessonId = l.Id
							 JOIN UserCourse uc
							   ON ulq.UserCourseId = uc.Id
					  WHERE  l.id = @lessonId
							 AND qr.IsDeleted = 0
							 AND uc.UserId = @userId
							 AND ulq.IsFinished=0
					  ORDER BY qr.CreatedDate desc
					  END')
  END


  IF object_id('[dbo].[Sp_GetLessonInmoduleIncourse]') IS NULL
 BEGIN
 EXECUTE('CREATE PROCEDURE Sp_GetLessonInmoduleIncourse (@courseId INT,
												   @moduleId INT,
												   @lessonId INT,
												   @orgId    INT)
		AS
		  BEGIN
			  SELECT l.*
			  FROM   Module m
					 JOIN course c
					   ON m.courseId = c.id
					 JOIN Lesson l
					   ON l.ModuleId = m.Id
			  WHERE  m.id = @moduleId
					 AND m.CourseId = c.Id
					 AND c.OrganizationId = @orgId
					 AND c.id = @courseId
					 AND l.id = @lessonId
					 AND c.IsDeleted = 0
		  END')
  END

  IF object_id('[dbo].[Sp_GetUsersByRole]') IS NULL
BEGIN
	EXECUTE(' CREATE PROCEDURE [dbo].[Sp_GetUsersByRole]
	-- Add the parameters for the stored procedure here
	@RoleName nvarchar(250)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

  
  Select a.FirstName,a.LastName,a.Email,a.Id UserId from 
  AspNetUsers a 
  inner join AspNetUserRoles b on a.Id = b.UserId
  inner join AspNetRoles c on b.RoleId = c.Id
  where c.[Name] = @RoleName
END')
END


IF object_id('[dbo].[SP_GetTicketDetails]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[SP_GetTicketDetails]
    @OrganizationId [int]
AS
BEGIN
    select distinct  
	           a.Email,
               a.FirstName,
			   a.LastName, 
			   d.[Name] as TicketIssuerDepartment,
			   t.Id as TicketId,
			   t.TicketTitle,
			   t.TicketDescription,
			   t.TicketStatus, 
			   t.CreatedDate,
			   t.IsDeleted
from Ticket t 
            inner join [User] u on u.Id=t.UserId 
			inner join AspNetUsers a on a.Id=u.ApplicationUserId
            left join Department d on d.Id=a.DepartmentId
			where t.OrganizationId=@OrganizationId
			ORDER  BY t.CreatedDate desc;
END')
END


IF object_id('[dbo].[Sp_GetSessionCompletedLesson]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE Sp_GetSessionCompletedLesson (@userCourseId INT)
					AS
					  BEGIN
						  SELECT (SELECT DISTINCT ISNULL(Count(lp.id),0)
								  FROM   LessonProgress lp
								  WHERE  lp.UserCourseId = uc.Id
										 AND lp.IsDeleted = 0
										 AND lp.IsCompleted = 1) AS CompletedLesson,
								 (SELECT DISTINCT ISNULL(Count (l.id),0)
								  FROM   Lesson l
										 JOIN Module m
										   ON m.CourseId = uc.CourseId
								  WHERE  l.ModuleId = m.id
										 AND l.IsDeleted = 0)    TotalLesson
						  FROM   UserCourse uc
						  WHERE  uc.id = @UserCourseId
					  END')
END
IF object_id('[dbo].[SP_GetBudgetExpenditure]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[SP_GetBudgetExpenditure]
    @groupId [int],
	@regionId [int],
	@OrganizationId [int],
    @Search [nvarchar](150),
    @pageNo [int] = 0,
    @pageSize [int]
AS
BEGIN
    
	           DECLARE @Total INT = (SELECT Count(id)
    			   FROM   TrainingBudgetExpenditure
    			   WHERE  OrganizationId = @organizationId)
				  select distinct
						  tb.AmountSpent,
						  tb.Id,
						  tb.DepartmentBudgetId,
						  tb.GroupId,
						  g.[Name] as GroupName,
						  r.[Name] as RegionName,
						  tb.NumberOfParticipants,
						  tb.RegionId,
						  tb.TrainingId,
						  t.[Name] as TrainingName,
						  t.[Location],
						  t.Venue,
						  d.[Year],
						  t.StartPeriod,
					   @Total  TotalRecords
				  from TrainingBudgetExpenditure tb
				   inner join Training t on t.Id=tb.TrainingId 
				   inner join DepartmentBudget d on d.Id=tb.DepartmentBudgetId
				   inner join [Group] g on g.Id=tb.GroupId
				   inner join Region r on r.Id=tb.RegionId
						  WHERE  tb.OrganizationId = @OrganizationId and tb.GroupId=@groupId and tb.RegionId=@regionId
    				   AND ( ( @Search IS NULL )
    						  OR ( @Search IS NOT NULL
    							   AND ( t.[Name] LIKE ''%'' + @Search + ''%''
    									  OR t.[Location] LIKE ''%'' + @Search + ''%'' ) ))

										  ORDER  BY tb.Id 
    			OFFSET (@pageSize * @pageNo) ROWS FETCH NEXT @pageSize ROWS ONLY;
END')
END


IF object_id('[dbo].[SP_GetEmployeeDetailsUpdate]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[SP_GetEmployeeDetailsUpdate]
    @id [int],
	@organizationId [int]
AS
BEGIN
   
	                                SELECT a.StaffId,
    									   a.Id,
    									   a.Email,
    									   a.PhoneNumber Phone,
    									   a.DateOfEmployment,
										   a.DateOfBirth,
    									   a.FirstName,
    									   a.LastName,
    									   a.Gender,
										   asr.RoleId,
    									   d.Id Department,
    									   a.Functions [Function],
    									   a.DateOfBirth,
										   a.[Address],
										   gr.Id as Grade,
    									   b.Id Branch,
										   r.Id Region,
										   g.Id [Group],
										   a.LineManagerStaffId
    								FROM   AspNetUsers a
    									   LEFT JOIN Department d
    											  ON a.departmentId = d.Id
    									   LEFT JOIN branch b
    											  ON b.id = a.BranchId
										   LEFT JOIN Grade gr
										          ON gr.Id=a.GradeId
    									   LEFT JOIN [Group] g
    											  ON g.Id = a.GroupId
										   LEFT JOIN Region r
										          ON r.Id=a.RegionId
										   LEFT JOIN AspNetUserRoles asr
										          ON asr.UserId=a.Id
    								WHERE  a.id = @id and a.OrganizationId = @organizationId ;
END')
END

IF object_id('[dbo].[Sp_GetQuizSummaryScore]') IS NULL
BEGIN
	EXECUTE('CREATE  PROCEDURE [dbo].[Sp_GetQuizSummaryScore] (@userCourseId INT,
														@lessonId     INT)
		AS
		  BEGIN
			  WITH quizsummary
				   AS (SELECT (SELECT Count(id)
							   FROM   QuizQuestion qq
							   WHERE  qq.LessonId = l.Id)   Questions,
							  (SELECT Count(*)
							   FROM   QuizResponse qr
							   WHERE  qr.UserLessonQuizId = ulq.Id
									  AND qr.IsDeleted = 0) Answers,
							  (SELECT SUM(qq.[Weight])
							   FROM   QuizResponse qr
									  JOIN QuizQuestion qq
										ON qq.id = qr.QuizId
							   WHERE  qr.UserLessonQuizId = ulq.id
									  AND qr.IsDeleted = 0
									  AND qr.IsAnswer = 1)  CorrectAnswers,
							  l.QuestionNo,
							  l.PassMark,
							  ulq.Id,
							  l.QuizRetakeCount
					   FROM   UserLessonQuiz ulq
							  JOIN Lesson l
								ON l.id = ulq.LessonId
								   AND ulq.UserCourseId = @userCourseId
								   AND l.Id = @lessonId)
			  SELECT TOP (1) Count(*)
							   OVER()             AS TotalAttempts,
							 Max(CorrectAnswers) over()  MaxScore,
							 Avg(questions)       Questions,
							 Sum(QuestionNo)
							   OVER(partition BY id) QuestionNo,
							 Sum(PassMark)
							   OVER ( partition BY id) PassMark,
							 Sum(QuizRetakeCount) OVER ( partition BY id) Retake
			  FROM   quizsummary
			  GROUP  BY Id,
						QuestionNo,
						PassMark,
						QuizRetakeCount,
						CorrectAnswers
		  END')
END


IF object_id('[dbo].[Sp_ValidateCLaCourse]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE Sp_ValidateCLaCourse (@courseId INT,
													  @moduleId INT =NULL,
													  @lessonId INT=NULL,
													  @orgId INT)
				AS
				  BEGIN
									SELECT DISTINCT c.Id
									FROM   Course c
					   JOIN Module m
						 ON m.courseId = c.id
					   JOIN Lesson l
						 ON l.ModuleId = m.id
				WHERE  c.id = @courseId
					   AND ( @moduleId IS NULL OR m.Id = @moduleId )
					   AND ( @lessonId IS NULL OR l.Id = @lessonId )
					   AND c.isdeleted = 0
					   AND c.ispublished = 1
					   AND (c.organizationId = @orgId )
					   AND (c.DueDate IS NULL or c.DueDate >= SYSDATETIME())
					   END')
END


IF object_id('[dbo].[Sp_GetUserCourseCompletedLessons]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE Sp_GetUserCourseCompletedLessons (@userCourseId INT)
				AS
				  BEGIN
					  SELECT LessonId Id, ModuleId
					  FROM   LessonProgress lp
					  WHERE UserCourseId = @userCourseId
							 AND lp.IsDeleted = 0
							 AND lp.IsCompleted = 1
					  ORDER  BY lp.ModuleId
				  END')
END

IF object_id('[dbo].[Sp_HasTakenSurveyorReview]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE Sp_HasTakenSurveyorReview(@courseId INT,
																@userId   INT,
																@orgId    INT)
						AS
						  BEGIN
							  SELECT (SELECT CASE
											   WHEN Count (cr.id) > 0 THEN CAST(1 AS BIT)
											   ELSE CAST(0 AS BIT)
											 END
									  FROM   CourseReview cr
											 JOIN Course c
											   ON c.id = @courseId
									  WHERE  cr.CourseId = @courseId
											 AND cr.UserId = @userId
											 AND c.OrganizationId = @orgId
											 AND cr.IsDeleted = 0) HastakenReview,
									 (SELECT TOP 1 CASE
													 WHEN Count(us.Id) > 0 THEN CAST(1 AS BIT)
													 ELSE CAST(0 AS BIT)
												   END
									  FROM   UserSurvey us
											 JOIN Survey s
											   ON s.Id = us.SurveyId
											 JOIN Course c
											   ON c.Id = @courseId
									  WHERE  s.CourseId = @courseId
											 AND us.UserId = @userId
											 AND c.OrganizationId = @orgId
											 AND us.IsDeleted = 0) HasTakenSurvey,
											 (SELECT TOP 1 CASE
													 WHEN Count(s.Id) > 0 THEN CAST(1 AS BIT)
													 ELSE CAST(0 AS BIT)
												   END
									  FROM   Survey s
											 JOIN Course c
											   ON c.Id = @courseId
									  WHERE  s.CourseId = @courseId
											 
											 AND c.OrganizationId = @orgId
											 AND s.IsDeleted = 0) HasSurvey
						  END')
END

IF object_id('[dbo].[SP_GetTableBranch]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[SP_GetTableBranch]
    @OrganizationId [int],
    @Search [nvarchar](150),
    @pageNo [int] = 0,
    @pageSize [int]
AS
BEGIN
DECLARE @Total INT = (SELECT Count(id)
    			   FROM   Branch
    			   WHERE  OrganizationId = @organizationId)
    select distinct
				         Id as BranchId,
						 [Name] as BranchName,
					   @Total  TotalRecords
				  from Branch
						  WHERE  OrganizationId = @OrganizationID AND IsDeleted=0
    				   AND ( ( @Search IS NULL )
    						  OR ( @Search IS NOT NULL
    							   AND ( [Name] LIKE ''%'' + @Search + ''%'' )))

										  ORDER  BY Id 
    			OFFSET (@pageSize * @pageNo) ROWS FETCH NEXT @pageSize ROWS ONLY;
END')
END



IF object_id('[dbo].[SP_GetTableDepartment]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[SP_GetTableDepartment]
    @GroupId [int],
	@OrganizationId [int],
    @Search [nvarchar](150),
    @pageNo [int] = 0,
    @pageSize [int]
AS
BEGIN
DECLARE @Total INT = (SELECT Count(id)
    			   FROM   Department
    			   WHERE  OrganizationId = @organizationId)
    select distinct
				         d.Id as DepartmentId,
						 d.[Name] as DeptName,
						 g.[Name] as GroupName,
					   @Total  TotalRecords
				  from Department d inner join [Group] g on g.Id= d.GroupId
						  WHERE  d.OrganizationId = @OrganizationID AND d.GroupId=@GroupId AND d.IsDeleted=0
    				   AND ( ( @Search IS NULL )
    						  OR ( @Search IS NOT NULL
    							   AND ( d.[Name] LIKE ''%'' + @Search + ''%'' )))

										  ORDER  BY d.Id 
    			OFFSET (@pageSize * @pageNo) ROWS FETCH NEXT @pageSize ROWS ONLY;;
END')
END

IF object_id('[dbo].[SP_GetTableDepartmentList]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[SP_GetTableDepartmentList]
    @OrganizationId [int],
    @Search [nvarchar](150),
    @pageNo [int] = 0,
    @pageSize [int]
AS
BEGIN
DECLARE @Total INT = (SELECT Count(id)
    			   FROM   Department
    			   WHERE  OrganizationId = @organizationId)
select distinct
				         d.Id as DepartmentId,
						 d.[Name] as DeptName,
						 g.[Name] as GroupName,
					   @Total  TotalRecords
				  from Department d inner join [Group] g on d.GroupId=g.Id
						  WHERE  d.OrganizationId = @OrganizationID  AND d.IsDeleted=0
    				   AND ( ( @Search IS NULL )
    						  OR ( @Search IS NOT NULL
    							   AND ( d.[Name] LIKE ''%'' + @Search + ''%'' )))

										  ORDER  BY d.Id 
    			OFFSET (@pageSize * @pageNo) ROWS FETCH NEXT @pageSize ROWS ONLY;
END')
END

IF object_id('[dbo].[SP_GetTableGroup]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[SP_GetTableGroup]
    @OrganizationId [int],
    @Search [nvarchar](150),
    @pageNo [int] = 0,
    @pageSize [int]
AS
BEGIN
DECLARE @Total INT = (SELECT Count(id)
    			   FROM   [Group]
    			   WHERE  OrganizationId = @organizationId)
 select distinct
				         Id as GroupId,
						 [Name] as GroupName,
						 GroupHeadFirstName,
						 GroupHeadLastName,
						 GroupHeadStaffId,
					   @Total  TotalRecords
				  from [Group]
						  WHERE  OrganizationId = @OrganizationID AND IsDeleted=0
    				   AND ( ( @Search IS NULL )
    						  OR ( @Search IS NOT NULL
    							   AND ( [Name] LIKE ''%'' + @Search + ''%'' )))

										  ORDER  BY Id 
    			OFFSET (@pageSize * @pageNo) ROWS FETCH NEXT @pageSize ROWS ONLY;
END')
END

IF object_id('[dbo].[SP_GetTableRegion]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[SP_GetTableRegion]
    @OrganizationId [int],
    @Search [nvarchar](150),
    @pageNo [int] = 0,
    @pageSize [int]
AS
BEGIN
DECLARE @Total INT = (SELECT Count(id)
    			   FROM   Region
    			   WHERE  OrganizationId = @organizationId)
 select distinct
				         Id as RegionId,
						 [Name] as RegionName,
					   @Total  TotalRecords
				  from Region
						  WHERE  OrganizationId = @OrganizationID AND IsDeleted=0
    				   AND ( ( @Search IS NULL )
    						  OR ( @Search IS NOT NULL
    							   AND ( [Name] LIKE ''%'' + @Search + ''%'' )))

										  ORDER  BY Id 
    			OFFSET (@pageSize * @pageNo) ROWS FETCH NEXT @pageSize ROWS ONLY;
END')
END


IF object_id('[dbo].[SP_GetUserOrgTrainingByMonth]') IS NULL
BEGIN
EXEC('CREATE PROCEDURE [dbo].[SP_GetUserOrgTrainingByMonth]
	
	@userID int,@OrganizationId int, @startDate datetime,@endDate datetime,@pageSize int , @pageNo int --SP_GetUserOrgTrainingByMonth 1,1,null,null,10,1
AS
BEGIN

	  if(@pageSize is null)

		BEGIN

			select  a.id, a.[EndPeriod],a.[Description],a.[Location],a.[Name],a.[StartPeriod], a.[TrainingCategory], a.[TrainingType],a.[Vendor],a.[Venue],
			case when b.id is null  then cast(0 as bit) else cast(1 as bit) end Ongoing,
			case when c.id is null  then cast(0 as bit) else cast(1 as bit) end Requested, a.PeriodFormat
	
			from 
			training a 
			left join UserTraining b on a.Id = b.TrainingId and b.IsDeleted = 0 and b.UserId = @userID
			left join TrainingRequest c on a.Id = c.TrainingId and c.IsDeleted = 0 and c.UserId = @userID 

			WHERE ((@startDate is not null and @endDate is not null) and (a.[EndPeriod] >= @startDate)
			AND (a.[StartPeriod] <= @endDate and a.IsDeleted = 0)) or (1 = 1) and (a.OrganizationId = @OrganizationId)

		END
	
	ELSE
		BEGIN
			
			declare @totalRecord int = (select  count(a.id) from training a WHERE a.IsDeleted = 0 and (a.OrganizationId = @OrganizationId))

			select  a.id, a.[EndPeriod],a.[Description],a.[Location],a.[Name],a.[StartPeriod], a.[TrainingCategory], a.[TrainingType],a.[Vendor],a.[Venue],
			case when b.id is null  then cast(0 as bit) else cast(1 as bit) end Ongoing,
			case when c.id is null  then cast(0 as bit) else cast(1 as bit) end Requested, a.PeriodFormat,@totalRecord TotalCount

			from 
			training a 
			left join UserTraining b on a.Id = b.TrainingId and b.IsDeleted = 0 and b.UserId = @userID
			left join TrainingRequest c on a.Id = c.TrainingId and c.IsDeleted = 0 and c.UserId = @userID 

			WHERE ((@startDate is not null and @endDate is not null) and (a.[EndPeriod] >= @startDate)
			AND (a.[StartPeriod] <= @endDate and a.IsDeleted = 0)) or (1 = 1) and (a.OrganizationId = @OrganizationId)
			ORDER  BY a.Id
				  OFFSET (@pageSize * (@pageNo-1)) ROWS FETCH NEXT @pageSize ROWS ONLY
		END

 END')
 END

 IF object_id('[dbo].[SP_GetTrainingRequestPendingLineManagerApproval]') IS NULL
BEGIN
EXEC('CREATE PROCEDURE SP_GetTrainingRequestPendingLineManagerApproval
	
	@LineManagerId int
AS
BEGIN

	Select a.Id,a.TrainingId,b.[Name] as TrainingName,a.CreatedDate , Cast(1 as bit) as IsApproval ,TrainingApprovalStatus,c.FirstName,c.LastName
	from TrainingRequest a
	 inner join Training b on a.TrainingId = b.Id 
	 inner join AspNetUsers c on a.UserId = c.Id 
where a.IsDeleted = 0 
and b.IsDeleted = 0 
and c.LineManagerId = @LineManagerId
and a.TrainingApprovalStatus = 1

END')


END

 IF object_id('[dbo].[SP_GetTrainingRequest]') IS NULL
BEGIN
EXEC('Create PROCEDURE [dbo].[SP_GetTrainingRequest]
	
	@id int
AS
BEGIN

	Select a.Id,a.TrainingId,
	b.[Name] as TrainingName,b.AmountPerStaff,a.ReasonForRequest,a.lineManagerComment,a.linemanageractionDate,a.AdminComment,a.AdminActionDate, a.CreatedDate,
	TrainingApprovalStatus,c.FirstName,c.LastName,c.Gender,c.StaffId,
	 Isnull(d.FirstName + '' '' + d.LastName,h.FirstName + '' '' + h.LastName) as LineManagerName,d.StaffId as LineManagerStaffId,
	 e.[Name] as BranchName,f.[Name] as GradeName,g.[Name] as DepartmentName

	from TrainingRequest a
	 inner join Training b on a.TrainingId = b.Id 
	 inner join AspNetUsers c on a.UserId = c.Id 
	 left join AspNetUsers d on a.lineManagerActionById = d.Id
	 left join AspNetUsers h on c.lineManagerId = h.id
	 left join Branch e on c.BranchId = e.id
	 left join Grade f on c.GradeId = f.id
	 left join Department g on c.DepartmentId = g.Id

where a.IsDeleted = 0 
and b.IsDeleted = 0 
and a.id = @id

END
')
END


 IF object_id('[dbo].[UploadLearningArea]') IS NULL
BEGIN
EXEC('
create table UploadLearningArea(
ProcessId int, Id int, [Name] nvarchar(200),SavedID int
)


create table UploadCourse(

ProcessId int, Id int, [Name] nvarchar(200), [Description] nvarchar(max), ShortDescription nvarchar(200),
ImageUrl nvarchar(500),DueDate datetime,LearningAreaId int,LearningArea nvarchar(200), Overview nvarchar(max),Objectives nvarchar(max),
EstimatedDuration int,HoursPerWeek int,IsPublished bit,HasCertificate bit,SavedID int
)

create table UploadModule(
ProcessId int, Id int, Course int, [Name] nvarchar(200),SavedID int
)


create table UploadLesson(
ProcessId int, Id int, Module int, [Name] nvarchar(200), [Description] nvarchar(max),ContentUrl nvarchar(200),
IsExternalContent bit,HtmlContent nvarchar(max),LessonContentType nvarchar(100),ContentType int,QuizRetakeCount int,Gradeable bit,SavedID int
)



create table UploadQuizQuestion(
ProcessId int, Id int, Lesson int, Question nvarchar(500), [Description] nvarchar(max),QuizAnswerType nvarchar(100),AnswerType int,
IsMultipleChoice bit,[Weight] int,SavedID int
)


create table UploadQuizQuestionOption(
ProcessId int, Id int, Question int, Title nvarchar(500), [Value] int,IsAnswer bit,SavedID int
)
')

END



 IF object_id('[dbo].[SP_ProcessCourseBulkUpload]') IS NULL
BEGIN
EXEC('CREATE PROCEDURE [dbo].[SP_ProcessCourseBulkUpload] 
	@ProcessID int, @UserID int, @OrganizationID int
AS
BEGIN
	SET NOCOUNT ON;

    ---delete learning areas that exists 
		

		BEGIN TRY

			BEGIN TRANSACTION;

			

		delete from UploadLearningArea 
		where ProcessId = @ProcessID and
		 [Name] in (select [Name] from UploadLearningArea 
		 where ProcessId = @ProcessID
		 group by [Name] having COUNT(id) > 1)
	

	delete a from UploadLearningArea a 
	inner join LearningArea b on a.[Name] = b.[Name]
	where a.ProcessId = @ProcessID
	

	---delete from course that name exists
	delete from UploadCourse 
		where ProcessId = @ProcessID and
		 [Name] in (select [Name] from UploadCourse 
		 where ProcessId = @ProcessID
		 group by [Name] having COUNT(id) > 1)

	delete a from UploadCourse a 
	inner join Course b on a.[Name] = b.[Name]
	where a.ProcessId = @ProcessID

	--delete from course where learning area is deleted

	delete a from UploadCourse a left join
	UploadLearningArea b on a.LearningAreaId = b.Id
	where a.ProcessId = @ProcessID and a.LearningAreaId is null and a.LearningAreaId != 0

	
	--- Insert learning areas

	Insert into LearningArea ([Name],OrganizationId,CreatedById,LastModifiedById,CreatedDate,ModifiedDate,IsDeleted)
	select [Name],@OrganizationID,@UserID,@UserID,getdate(),GETDATE(),0 from UploadLearningArea
    where ProcessId = @ProcessID

	----- Update learning area upload with saved table id


	update a 
	SET a.SavedID = b.Id
	from UploadLearningArea a
	inner join learningarea  b  ON a.[Name] = b.[Name] 
	WHERE a.ProcessID = @ProcessID


	---- insert course with learning area id
	

	insert into Course (LearningAreaId, [Name], [Description],ShortDescription,ImageUrl,DueDate,EstimatedDuration,
	HoursPerWeek, Overview,Objectives,IsPublished,HasCertificate,OrganizationId,CreatedById,LastModifiedById,ModifiedDate,
	CreatedDate,IsDeleted) 

	select case when a.LearningAreaId != 0 then b.SavedID else c.Id end as NewLearningAreaID,
	  a.[Name],a.[Description],a.ShortDescription,a.ImageUrl,a.DueDate, a.EstimatedDuration,
	a.HoursPerWeek, a.Overview,a.Objectives,a.IsPublished,a.HasCertificate,
	@OrganizationID,@UserID,@UserID,GETDATE(),GETDATE(),0
	 from UploadCourse a 
	left join UploadLearningArea b on (a.LearningAreaId != 0 and a.LearningAreaId = b.Id)
	left join LearningArea c on (a.learningArea is not null and c.[Name] = a.LearningArea)
	

	update  a 
	SET a.SavedID = b.Id
	from UploadCourse a
	inner join Course b  ON a.[Name] = b.[Name] 
	WHERE a.ProcessID = @ProcessID


	
	---delete from module that name exists for module

	delete a from UploadModule  a
	inner join UploadCourse b on a.Course = b.Id
		where a.ProcessId = @ProcessID and
		 a.[Name] in 
		 (
		 select z.[Name] from UploadModule z
		 where z.ProcessId = @ProcessID and z.Course = a.Course
		 group by z.[Name] having COUNT(z.id) > 1
		 )


	delete a from UploadModule a 
	inner join UploadCourse b on a.Course = b.Id
	inner join Course c on c.Id = b.SavedId
	inner join Module d on a.[Name] = d.[Name]
	where a.ProcessId = @ProcessID 


	
	--delete from module where course is deleted

	delete a from UploadModule a left join
	UploadCourse b on a.Course = b.Id
	where a.ProcessId = @ProcessID and a.Course is null 

	
	---- insert module with course id

	insert into Module ([Name],SortOrder,CourseId,CreatedById,LastModifiedById,ModifiedDate,
	CreatedDate,IsDeleted) 
	select a.[Name],
	  row_number() over (order by (select NULL)) + (select Isnull(Max(SortOrder),0) from Module where CourseId = b.SavedID),
	  b.SavedID,@UserID,@UserID,GETDATE(),GETDATE(),0
	from UploadModule a
	inner join UploadCourse b on a.Course = b.id
	where a.ProcessId = @ProcessID

	

	update a
	SET a.SavedID = b.Id 
	from UploadModule a 
	inner join Module b  ON a.[Name] = b.[Name]
	WHERE a.ProcessID = @ProcessID

	

	---delete from lesson that name exists for lesson

	delete a from UploadLesson  a
	inner join UploadModule b on a.Module = b.Id
		where a.ProcessId = @ProcessID and
		 a.[Name] in 
		 (
		 select z.[Name] from UploadLesson z
		 where z.ProcessId = @ProcessID and z.Module = a.Module
		 group by z.[Name] having COUNT(z.id) > 1
		 )

	delete a from UploadLesson a 
	inner join UploadModule b on a.Module = b.Id
	inner join Module c on c.Id = b.SavedId
	inner join Lesson d on a.[Name] = d.[Name]
	where a.ProcessId = @ProcessID

	--delete from lesson where module is deleted

	delete a from UploadLesson a left join
	UploadModule b on a.Module = b.Id
	where a.ProcessId = @ProcessID and a.Module is null 
	
	
	---- insert lesson with module id

	insert into Lesson ([Name],[Description],ContentUrl,IsExternalContent,HTMLContent,MimeType,IsGradeableContent,LessonContentType,
	QuizRetakeCount, SortOrder,ModuleId,OrganizationId,CreatedById,LastModifiedById,ModifiedDate,
	CreatedDate,IsDeleted) 
	select a.[Name],a.[Description],ContentUrl,IsExternalContent,HTMLContent,null,Gradeable,ContentType,
	QuizRetakeCount,
	  row_number() over (order by (select NULL)) + (select ISNULL(Max(SortOrder),0) from Lesson where ModuleId = b.SavedID),
	  b.SavedID,@OrganizationID,@UserID,@UserID,GETDATE(),GETDATE(),0
	from UploadLesson a
	inner join UploadModule b on a.Module = b.id
	where a.ProcessId = @ProcessID

	update a 
	SET a.SavedID = b.Id
	from UploadLesson a
	inner join Lesson b  ON a.[Name] = b.[Name] 
	WHERE a.ProcessID = @ProcessID



	---delete from quiz question that name exists for quiz

	delete a from UploadQuizQuestion  a
	inner join UploadLesson b on a.Lesson = b.Id
		where a.ProcessId = @ProcessID and
		 a.Question in 
		 (
		 select z.Question from UploadQuizQuestion z
		 where z.ProcessId = @ProcessID and z.Lesson = a.Lesson
		 group by z.Question having COUNT(z.id) > 1
		 )


	delete a from UploadQuizQuestion a 
	inner join UploadLesson b on a.Lesson = b.Id
	inner join Lesson c on c.Id = b.SavedId
	inner join QuizQuestion d on a.Question = d.Question
	where a.ProcessId = @ProcessID 

		--delete from lesson where module is deleted

	delete a from UploadQuizQuestion a left join
	UploadLesson b on a.Lesson = b.Id
	where a.ProcessId = @ProcessID and a.Lesson is null 

		---- insert quiz question with lesson id

	insert into QuizQuestion(Question,AnswerType,SortOrder,LessonId,IsMultipleChoice,[Weight],
	OrganizationId,CreatedById,LastModifiedById,ModifiedDate,CreatedDate,IsDeleted) 
	select Question,AnswerType,
	row_number() over (order by (select NULL)) + (select ISNULL(Max(SortOrder),0) from QuizQuestion where LessonId = b.SavedID),
	b.SavedID,IsMultipleChoice,[Weight],@OrganizationID,@UserID,@UserID,GETDATE(),GETDATE(),0
	from UploadQuizQuestion a
	inner join UploadLesson b on a.Lesson = b.id
	where a.ProcessId = @ProcessID

	update  a 
	SET a.SavedID = b.Id
	from UploadQuizQuestion a
	inner join QuizQuestion b ON a.Question = b.Question 
	WHERE a.ProcessID = @ProcessID
	


	---delete from quiz question that name exists for quiz

	delete a from UploadQuizQuestionOption  a
	inner join UploadQuizQuestion b on a.Question = b.Id
		where a.ProcessId = @ProcessID and
		 a.Title in 
		 (
		 select z.Title from UploadQuizQuestionOption z
		 where z.ProcessId = @ProcessID and z.Question = a.Question
		 group by z.Title having COUNT(z.id) > 1
		 )

	delete a from UploadQuizQuestionOption a 
	inner join UploadQuizQuestion b on a.Question = b.Id
	inner join QuizQuestion c on c.Id = b.SavedId
	inner join QuizQuestionOption d on a.Title = d.Title
	where a.ProcessId = @ProcessID

	delete a from UploadQuizQuestionOption a left join
	UploadQuizQuestion b on a.Question = b.Id
	where a.ProcessId = @ProcessID and a.Question is null 


		---- insert quiz question option with question id
		
	

	insert into QuizQuestionOption(QuizId,Title,[Value],IsAnswer,
	OrganizationId,CreatedById,LastModifiedById,ModifiedDate,CreatedDate,IsDeleted) 
	select b.SavedID,Title,[Value],IsAnswer,@OrganizationID,@UserID,@UserID,GETDATE(),GETDATE(),0
	from UploadQuizQuestionOption a
	inner join UploadQuizQuestion b on a.Question = b.id
	where a.ProcessId = @ProcessID


	delete from	UploadLearningArea WHERE ProcessID = @ProcessID
	delete from	UploadCourse WHERE ProcessID = @ProcessID
	delete from	UploadModule WHERE ProcessID = @ProcessID
	delete from	UploadLesson WHERE ProcessID = @ProcessID
	delete from	UploadQuizQuestion WHERE ProcessID = @ProcessID

	delete from	UploadQuizQuestionOption WHERE ProcessID = @ProcessID

			COMMIT TRANSACTION;


		END TRY

		BEGIN CATCH
			 declare @ErrorMessage nvarchar(max), 
			 @ErrorSeverity int, 
			 @ErrorState int;
			 select @ErrorMessage = ERROR_MESSAGE() + '' Line '' + cast(ERROR_LINE() as nvarchar(5)), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();

			if @@trancount > 0
				rollback transaction;

				delete from	UploadLearningArea WHERE ProcessID = @ProcessID
				delete from	UploadCourse WHERE ProcessID = @ProcessID
				delete from	UploadModule WHERE ProcessID = @ProcessID
				delete from	UploadLesson WHERE ProcessID = @ProcessID
				delete from	UploadQuizQuestion WHERE ProcessID = @ProcessID
				delete from	UploadQuizQuestionOption WHERE ProcessID = @ProcessID
				raiserror (@ErrorMessage, @ErrorSeverity, @ErrorState);
		END CATCH
		




END
')
END


 IF object_id('[dbo].[Sp_GetLearningGroupList]') IS NULL
BEGIN
EXEC('Create PROCEDURE [dbo].[Sp_GetLearningGroupList] 
    @organizationId [int],
    @Search [nvarchar](150),
    @pageNo [int] = 0,
    @pageSize [int]
AS
BEGIN
    SET NOCOUNT ON;
    
		DECLARE @Total INT = (SELECT Count(id)
    			   FROM   LearningGroup
    			   WHERE  OrganizationId = @organizationId
    					  AND IsDeleted = 0)
   

			SELECT m.id,m.[name],m.TagFormat, COALESCE(t1.ct, 0) CourseCount, COALESCE(t2.ct, 0) as TrainingCount , COALESCE(t3.ct, 0) as ExamCount, COALESCE(t4.ct, 0)  AS SurveyCount, @Total TotalRecords
FROM   LearningGroup m 
LEFT   JOIN (
   SELECT LearningGroupId, count(*) AS ct
   FROM   LearningGroupCourse
   GROUP  BY LearningGroupId
   ) t1 ON t1.LearningGroupId = m.id

LEFT   JOIN (
   SELECT LearningGroupId, count(*) AS ct
   FROM   LearningGroupTraining
   GROUP  BY LearningGroupId
   ) t2 ON t2.LearningGroupId = m.id

LEFT   JOIN (
   SELECT LearningGroupId, count(*) AS ct
   FROM   LearningGroupExam
   GROUP  BY LearningGroupId
   ) t3 ON t3.LearningGroupId = m.id
   LEFT   JOIN (
   SELECT LearningGroupId, count(*) AS ct
   FROM   LearningGroupSurvey
   GROUP  BY LearningGroupId
   ) t4 ON t4.LearningGroupId = m.id

   	WHERE  m.OrganizationId = @OrganizationID
    				   AND m.IsDeleted = 0
    				   AND ( ( @Search IS NULL )
    						  OR ( @Search IS NOT NULL
    							   AND ( m.[Name] LIKE ''%'' + @Search + ''%'' ) ) )

ORDER BY m.id desc
OFFSET (@pageSize * @pageNo) ROWS FETCH NEXT @pageSize ROWS ONLY

END')

END


-- IF object_id('[dbo].[Sp_GetExamandUserAttemptCounts]') IS NULL
--BEGIN
--EXEC('CREATE PROCEDURE Sp_GetExamandUserAttemptCounts (@usercourseId INT,
--                                                        @orgId        INT)
--	AS
--	  BEGIN
--		  SELECT TOP (1) e.Id,
--									 e.Name,
--									 e.Description,
--									 e.PassGrade,
--									 e.EndDate,
--									 e.ExamRetakeCount,
--									 e.HourLimit,
--									 e.MinuteLimit,
--									 e.startDate,
--									 e.EndDate,
--									 ISNULL((SELECT Count(id)
--											 FROM   ExaminationAttempt ea
--											 WHERE  ea.ExaminationId = e.id
--													AND ea.UserCourseId = uc.Id
--													AND ea.Completed = 1
--													AND ea.IsDeleted = 0), 0) AttemptCounts
--					  FROM   Examination e
--							 LEFT JOIN UserCourse uc
--									ON uc.Id = @usercourseId
--					  WHERE  e.OrganizationId = @orgId
--							 AND e.IsDeleted = 0
--							 AND e.CourseId = uc.CourseId
--					  ORDER  BY e.CreatedDate DESC
--	  END')

--END

-- IF object_id('[dbo].[Sp_GetLastExamResponse]') IS NULL
--BEGIN
--EXEC('CREATE PROCEDURE Sp_GetLastExamResponse (@examId INT,
--                                                 @userId INT)
--	AS
--	  BEGIN
--		  		SELECT TOP (1) eqr.ExaminationQuestionId,
--							   eqr.Value
--				FROM   Examination e
--					   JOIN ExaminationAttempt ea
--						 ON e.Id = ea.ExaminationId
--					   JOIN ExaminationQuizResponse eqr
--						 ON eqr.ExaminationAttemptId = ea.Id
--					   JOIN UserCourse uc
--						 ON uc.Id = ea.UserCourseId
--				WHERE  uc.UserId = @userId
--					   AND e.Id = @examId
--					   AND e.EndDate >= Sysutcdatetime()
--					   AND ea.Completed = 0
--				ORDER  BY eqr.CreatedDate DESC 
--	  END')

--END


 IF object_id('[dbo].[SP_GetColumns]') IS NULL
BEGIN
EXEC('CREATE PROCEDURE [dbo].[SP_GetColumns]
    @List nvarchar(2000),
    @SplitOn nvarchar(5),
	@C1 varchar(30),
	@C2 varchar(30)
AS
BEGIN  

DECLARE @RtnValue table
(

    Id int identity(1,1),
    USERID INT,
	C1 varchar(30),
	C2 varchar(30)

)

	While (Charindex(@SplitOn,@List)>0)
	Begin
	Insert Into @RtnValue (USERID,C1,C2)
	Select	ltrim(rtrim(Substring(@List,1,Charindex(@SplitOn,@List)-1))),@C1,@C2
	Set @List =Substring(@List,Charindex(@SplitOn,@List)+len(@SplitOn),len(@List))
	End
		Insert Into @RtnValue (USERID,C1,C2)
		Select Value = ltrim(rtrim(@List)),@C1,@C2
    
	select * from @RtnValue
END')

END

 IF object_id('[dbo].[Sp_GetUsersByRoleOrg]') IS NULL
BEGIN
EXEC(' CREATE PROCEDURE [dbo].[Sp_GetUsersByRoleOrg]
	-- Add the parameters for the stored procedure here
	@RoleName nvarchar(250),
	@OrgId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

  
  Select a.FirstName,a.LastName,a.Email,a.Id UserId from 
  AspNetUsers a 
  inner join AspNetUserRoles b on a.Id = b.UserId
  inner join AspNetRoles c on b.RoleId = c.Id
  inner join Organization o on o.id = a.OrganizationId
  where c.[Name] = @RoleName
END')

END

--IF object_id('[dbo].[SP_UserCourseProgress]') IS NULL
--BEGIN
--	EXECUTE('CREATE PROCEDURE [dbo].[SP_UserCourseProgress]
--    @OrganizationId [int],
--    @Search [nvarchar](150),
--    @pageNo [int] = 0,
--    @pageSize [int]
--AS
--BEGIN
--DECLARE @Total INT = (SELECT Count(id)
--    			   FROM   UserCourse)

--	SET @Search = (SELECT Ltrim(Rtrim(@Search)));
   
--	  WITH userprogress
--			  AS (SELECT (SELECT Isnull(Count(l.id), 0)
--			  FROM   LessonProgress l
--			  WHERE  l.UserCourseId = u.Id
--					 AND l.IsDeleted = 0
--					 AND l.IsCompleted = 1) AS Completed,

--			(SELECT Isnull(Count (le.id), 0)
--			 FROM   Lesson le
--					JOIN Module m ON m.CourseId = u.CourseId
--			 WHERE  le.ModuleId = m.id
--				    AND le.IsDeleted = 0)  As  LessonsCounts,

--					u.Id as UserCourseId,
--					 a.FirstName,
--					 a.LastName,
--					 c.[Name] as CourseName,
--					 u.StartDate,
--					 u.Score as ExaminationScore,
--					 u.TotalExamAttempts,
--					 g.[Name] as GroupName,
--					  @Total as TotalRecords
--	        from UserCourse u 
--	                 inner join Course c on c.Id=u.CourseId 
--					 inner join AspNetUsers a on a.Id=u.UserId
--					 inner join [Group] g on g.Id=a.GroupId
--	 WHERE c.OrganizationId=@OrganizationId AND u.IsDeleted=0
--    				   AND ( ( @Search IS NULL )
--    						  OR ( @Search IS NOT NULL
--    							   AND ( a.[FirstName] LIKE ''%'' + @Search + ''%''  OR a.LastName LIKE ''%'' + @Search + ''%'' OR g.[Name] LIKE ''%'' + @Search + ''%''OR c.[Name] LIKE ''%'' + @Search + ''%'')))

--										  ORDER  BY a.[FirstName] 
--    			OFFSET (@pageSize * @pageNo) ROWS FETCH NEXT @pageSize ROWS ONLY)

--				select
--				UserCourseId, 
--				FirstName, 
--				LastName, 
--				CourseName, 
--				StartDate, 
--				ExaminationScore, 
--				TotalExamAttempts,
--				Completed, 
--				LessonsCounts,
--				TotalRecords,
--				CASE 
--				   WHEN LessonsCounts = 0 THEN 100.0
--				   ELSE Cast(Isnull(Completed * 100.00 / LessonsCounts, 0)AS DECIMAL (18, 2))
--				END as CourseProgress
--				from userprogress
--END')
--END

--IF object_id('[dbo].[SP_UserExaminationProgress]') IS NULL
--BEGIN
--	EXECUTE('CREATE PROCEDURE [dbo].[SP_UserExaminationProgress]
--    @OrganizationId [int],
--    @Search [nvarchar](150),
--    @pageNo [int] = 0,
--    @pageSize [int]
--AS
--BEGIN
--DECLARE @Total INT = (SELECT Count(id)
--    			   FROM   UserExamination)

--	SET @Search = (SELECT Ltrim(Rtrim(@Search)));
   
--	WITH userex
--			  AS (SELECT (SELECT Isnull(Count(ea.id), 0)
--			  FROM   ExaminationAttempt ea
--			  WHERE  ea.UserId = e.UserId
--					 AND ea.IsDeleted = 0) AS Attempt,

--					 e.Id as UserExamId,
--					 a.FirstName,
--					 a.LastName,
--					 ex.[Name] as ExamName,
--					 u.Score as AverageScore,
--					 u.Score as HighestScore,
--					 ex.PassGrade,
--					 e.IsPassed,
--					 ex.ExamRetakeCount,
--					 e.CreatedDate,
--					 g.[Name] as GroupName,
--					  @Total as TotalRecords
--	        from UserExamination e 
--			inner join AspNetUsers a on a.Id=e.UserId 
--	        inner join Examination ex on ex.Id=e.ExaminationId
--			inner join UserCourse u on u.Id=e.UserCourseId
--			inner join [Group] g on g.Id=a.GroupId
--	 WHERE ex.OrganizationId=@OrganizationId AND ex.IsDeleted=0
--    				   AND ( ( @Search IS NULL )
--    						  OR ( @Search IS NOT NULL
--    							   AND ( a.[FirstName] LIKE ''%'' + @Search + ''%''  OR a.LastName LIKE ''%'' + @Search + ''%'' OR g.[Name] LIKE ''%'' + @Search + ''%''OR ex.[Name] LIKE ''%'' + @Search + ''%'')))

--										  ORDER  BY a.[FirstName] 
--    			OFFSET (@pageSize * @pageNo) ROWS FETCH NEXT @pageSize ROWS ONLY)
	

--	select
--				UserExamId, 
--				FirstName, 
--				LastName, 
--				ExamName, 
--				CreatedDate, 
--				AverageScore,
--				HighestScore, 
--				PassGrade,
--				IsPassed,
--				Attempt, 
--				TotalRecords
--				from userex
--END')
--END

 IF object_id('[dbo].[SP_CLONECOURSE]') IS NULL
BEGIN
EXEC('CREATE PROCEDURE SP_CLONECOURSE 
	-- Add the parameters for the stored procedure here
	@CourseId int, @Name nvarchar(300),@userId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY 

			BEGIN TRANSACTION;

			insert into Course ([Name],EstimatedDuration,HoursPerWeek,[Description],ShortDescription,ImageUrl,DueDate,LearningAreaId,
			Overview,Objectives,IsPublished,HasCertificate,OrganizationId,CreatedById,LastModifiedById,ModifiedDate,CreatedDate,IsDeleted)

			Select @Name,EstimatedDuration,HoursPerWeek,[Description],ShortDescription,ImageUrl,DueDate,LearningAreaId,
			Overview,Objectives,IsPublished,HasCertificate,OrganizationId,@userId,@userId,getdate(),getdate(),0
			from course where id = @CourseId

			declare @newCourseId int = (select scope_identity())

			DECLARE @OldModuleId int

			----- LOOP THROUGH MODULES
			DECLARE @ModuleCursor as CURSOR;
 
			SET @ModuleCursor = CURSOR FAST_FORWARD FOR
			Select id from module where courseid = @CourseId and IsDeleted = 0;
			OPEN @ModuleCursor;
			FETCH NEXT FROM @ModuleCursor INTO @OldModuleId;
			WHILE @@FETCH_STATUS = 0

			BEGIN
		
				insert into Module ([Name],SortOrder,CourseId,CreatedById,LastModifiedById,ModifiedDate,CreatedDate,IsDeleted)
			
				select [Name],SortOrder,@newCourseId,@userId,@userId,getdate(),getdate(),IsDeleted
				from module where id = @OldModuleId

				declare @newModuleId int = (select scope_identity())

				----- LOOP THROUGH MODULE LESSONS AND INSERT LESSON

				DECLARE @OldLessonId int
				DECLARE @LessonCursor as CURSOR;
 
				SET @LessonCursor = CURSOR FAST_FORWARD FOR
				Select id from lesson where ModuleId = @OldModuleId and IsDeleted = 0;
				OPEN @LessonCursor;
				FETCH NEXT FROM @LessonCursor INTO @OldLessonId;
				WHILE @@FETCH_STATUS = 0

				BEGIN
				
				insert into Lesson([Name],[Description],ContentUrl,IsExternalContent,HtmlContent,MimeType,IsGradeableContent,
				LessonContentType,QuizRetakeCount,SortOrder,ModuleId,OrganizationId,CreatedById,LastModifiedById,ModifiedDate,CreatedDate,IsDeleted)

				select L.[Name],[Description],ContentUrl,IsExternalContent,HtmlContent,MimeType,IsGradeableContent,
				LessonContentType,QuizRetakeCount,L.SortOrder,@newModuleId,OrganizationId,@userId,@userId,GETDATE(),getdate(),L.IsDeleted
				from Lesson l where id = @OldLessonId

				declare @newLessonId int = (select scope_identity())

				----- LOOP THROUGH MODULE LESSONS THAT ARE QUIZ AND INSERT QUESTIONS

				if(select lessoncontenttype from Lesson where id = @OldLessonId) = 4

				BEGIN

				DECLARE @OldQuestionId int
				DECLARE @QuestionCursor as CURSOR;
 
				SET @QuestionCursor = CURSOR FAST_FORWARD FOR
				Select id from QuizQuestion where LessonId = @OldLessonId and IsDeleted = 0;
				OPEN @QuestionCursor;
				FETCH NEXT FROM @QuestionCursor INTO @OldQuestionId;
				WHILE @@FETCH_STATUS = 0

				BEGIN
				
				insert into QuizQuestion(Question,AnswerType,SortOrder,LessonId,IsMultipleChoice,[Weight],
				OrganizationId,CreatedById,LastModifiedById,ModifiedDate,CreatedDate,IsDeleted)

				Select Question,AnswerType,SortOrder,@newLessonId,IsMultipleChoice,[Weight],
				OrganizationId,@userId,@userId,GETDATE(),GETDATE(),0
				from QuizQuestion where id = @OldQuestionId

				declare @newQuestionId int = (select scope_identity())
				----- LOOP THROUGH QUESTIONS AND INSERT OPTIONS

				DECLARE @OldOptionId int
				DECLARE @OptionCursor as CURSOR;
 
				SET @OptionCursor = CURSOR FAST_FORWARD FOR
				Select id from QuizQuestionOption where QuizId = @OldQuestionId and IsDeleted = 0;
				OPEN @OptionCursor;
				FETCH NEXT FROM @OptionCursor INTO @OldOptionId;
				WHILE @@FETCH_STATUS = 0

				BEGIN
				
				insert into QuizQuestionOption(QuizId,Title,[Value],IsAnswer,OrganizationId,CreatedById,LastModifiedById,ModifiedDate,CreatedDate,IsDeleted)
				select @newQuestionId,Title,[Value],IsAnswer,OrganizationId,@userId,@userId,GETDATE(),GETDATE(),IsDeleted
				from QuizQuestionOption where id = @OldOptionId

				FETCH NEXT FROM @OptionCursor INTO @OldOptionId;
			END

		CLOSE @OptionCursor;
		DEALLOCATE @OptionCursor;


				FETCH NEXT FROM @QuestionCursor INTO @OldQuestionId;
			END

		CLOSE @QuestionCursor;
		DEALLOCATE @QuestionCursor;

				END


				FETCH NEXT FROM @LessonCursor INTO @OldLessonId;
			END

		CLOSE @LessonCursor;
		DEALLOCATE @LessonCursor;


				FETCH NEXT FROM @ModuleCursor INTO @OldModuleId;
			END

		CLOSE @ModuleCursor;
		DEALLOCATE @ModuleCursor;


			COMMIT TRANSACTION;


		END TRY

		BEGIN CATCH
			 declare @ErrorMessage nvarchar(max), 
			 @ErrorSeverity int, 
			 @ErrorState int;
			 select @ErrorMessage = ERROR_MESSAGE() + '' Line '' + cast(ERROR_LINE() as nvarchar(5)), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();

			if @@trancount > 0
				rollback transaction;
				raiserror (@ErrorMessage, @ErrorSeverity, @ErrorState);
		END CATCH

END')

END

 IF object_id('[dbo].[SP_CLONEEXAM]') IS NULL
BEGIN
EXEC('CREATE PROCEDURE SP_CLONEEXAM  
	-- Add the parameters for the stored procedure here
	@ExamId int, @Name nvarchar(300),@userId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY 

			BEGIN TRANSACTION;

			insert into Examination(CourseId,ImageUrl,ExamType,PassGrade,ExamRetakeCount,[Name],[Description],HourLimit,MinuteLimit,StartDate,EndDate,
			OrganizationId,CreatedById,LastModifiedById,ModifiedDate,CreatedDate,IsDeleted)

			Select CourseId,ImageUrl,ExamType,PassGrade,ExamRetakeCount,@Name,[Description],HourLimit,MinuteLimit,StartDate,EndDate,
			OrganizationId,@userId,@userId,getdate(),getdate(),0
			from Examination where id = @ExamId

			declare @newExamId int = (select scope_identity())

			----- LOOP THROUGH Examination Questions

				DECLARE @OldQuestionId int
				DECLARE @QuestionCursor as CURSOR;
 
				SET @QuestionCursor = CURSOR FAST_FORWARD FOR
				Select id from ExaminationQuestion where ExaminationId=  @ExamId and IsDeleted = 0;
				OPEN @QuestionCursor;
				FETCH NEXT FROM @QuestionCursor INTO @OldQuestionId;
				WHILE @@FETCH_STATUS = 0

				BEGIN
				
				insert into ExaminationQuestion(Question,AnswerType,SortOrder,ExaminationId,IsMultipleChoice,[Weight],
				CreatedById,LastModifiedById,ModifiedDate,CreatedDate,IsDeleted)

				Select Question,AnswerType,SortOrder,@newExamId,IsMultipleChoice,[Weight],
				@userId,@userId,GETDATE(),GETDATE(),0
				from ExaminationQuestion where id = @OldQuestionId

				declare @newQuestionId int = (select scope_identity())
				----- LOOP THROUGH QUESTIONS AND INSERT OPTIONS

				DECLARE @OldOptionId int
				DECLARE @OptionCursor as CURSOR;
 
				SET @OptionCursor = CURSOR FAST_FORWARD FOR
				Select id from ExaminationQuestionOption where ExaminationQuizId = @OldQuestionId and IsDeleted = 0;
				OPEN @OptionCursor;
				FETCH NEXT FROM @OptionCursor INTO @OldOptionId;
				WHILE @@FETCH_STATUS = 0

				BEGIN
				
				insert into ExaminationQuestionOption(ExaminationQuizId,Title,[Value],IsAnswer,CreatedById,LastModifiedById,ModifiedDate,CreatedDate,IsDeleted)
				select @newQuestionId,Title,[Value],IsAnswer,@userId,@userId,GETDATE(),GETDATE(),IsDeleted
				from ExaminationQuestionOption where id = @OldOptionId

				FETCH NEXT FROM @OptionCursor INTO @OldOptionId;
			END

		CLOSE @OptionCursor;
		DEALLOCATE @OptionCursor;


				FETCH NEXT FROM @QuestionCursor INTO @OldQuestionId;
			END

		CLOSE @QuestionCursor;
		DEALLOCATE @QuestionCursor;

			


			COMMIT TRANSACTION;


		END TRY

		BEGIN CATCH
			 declare @ErrorMessage nvarchar(max), 
			 @ErrorSeverity int, 
			 @ErrorState int;
			 select @ErrorMessage = ERROR_MESSAGE() + '' Line '' + cast(ERROR_LINE() as nvarchar(5)), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();

			if @@trancount > 0
				rollback transaction;
				raiserror (@ErrorMessage, @ErrorSeverity, @ErrorState);
		END CATCH

END')

END

 IF object_id('[dbo].[GetUsersByLearningGroupID]') IS NULL
BEGIN
EXEC('CREATE FUNCTION GetUsersByLearningGroupID 
(
	-- Add the parameters for the function here
	@LearningGroupId int,@OrganizationId int
)
RETURNS 

@USERS TABLE 
(	Id INT,
	LearningGroupId INT,
	UserId INT,
	FirstName nvarchar(200),
	LastName nvarchar(200),
	DepartmentId INT,
	BranchId INT,
	RegionId INT,
	GradeId INT,
	GroupId INT,
	Gender Int,
	RoleId INT,
	TagType INT,
	TagValue INT
)
AS
BEGIN
	  INSERT INTO @USERS
						  SELECT tag.Id,
								 tag.LearningGroupId,
								 aspu.Id,aspu.FirstName,aspu.LastName, aspu.DepartmentId,aspu.BranchId,aspu.RegionId,aspu.GradeId,aspu.GroupId,aspu.Gender,
								 CASE
								   WHEN tag.TagType = 4 THEN r.RoleId
								   ELSE 0
								 END       RoleId,
								 tag.TagType,
								 tag.TagValue
						  FROM   LearningGroupTag tag
								 LEFT JOIN AspNetUsers  aspu  
										ON ( ( tag.TagType = 1
											   AND tag.TagValue = aspU.GroupId )
											  OR ( tag.TagType = 2
												   AND tag.TagValue = aspU.Gender )
											  OR ( tag.TagType = 3
												   AND tag.TagValue = aspU.GradeId )
											  OR ( tag.TagType = 6
												   AND tag.TagValue = aspU.RegionId )
											  OR ( tag.TagType = 7
												   AND tag.TagValue = aspU.DepartmentId ) )
										   AND tag.LearningGroupId = @LearningGroupId 
						--roles
								 LEFT JOIN AspNetUserRoles r
										ON aspU.id = r.UserId
										   AND tag.TagType = 4
										   AND tag.TagValue = r.RoleId

						   WHERE  tag.TagType != 5 and aspu.OrganizationId = @OrganizationId

						  DELETE FROM @USERS
						  WHERE  LearningGroupId IN (SELECT LearningGroupId
													 FROM   @USERS
													 WHERE  UserId IS NULL
															 OR RoleId IS NULL)

						  -- Individual
						  INSERT INTO @USERS
						  SELECT a.id,
								 a.LearningGroupId,
								 a.TagValue,aspu.FirstName,aspu.LastName,aspu.DepartmentId,aspu.BranchId,aspu.RegionId,aspu.GradeId,aspu.GroupId,aspu.Gender,
								 NULL RoleId,
								 a.TagType,
								 a.TagValue
						  FROM   LearningGroupTag a
						  inner join AspNetUsers aspu on a.TagValue = aspu.Id
						  WHERE  a.tagtype = 5
								 AND a.LearningGroupId = @LearningGroupId and aspu.OrganizationId = @OrganizationId
	
	RETURN 
END')

END


 IF object_id('[dbo].[Sp_GetExamAndAttempt]') IS NULL
BEGIN
EXEC('CREATE PROCEDURE [dbo].[Sp_GetExamAndAttempt] @courseId INT,
													@userId   INT
		AS
		  BEGIN
			 SELECT ue.UserAttemptCounts,
						 e.[Name],
						 e.[Description],
						 e.ExamRetakeCount,
						 e.startdate,
						 e.EndDate
				  FROM   (SELECT uesq.Id,
								 uesq.UserId,
								 uesq.ExaminationId,
								 (SELECT Isnull(Count(ea.Id), 0)
								  FROM   ExaminationAttempt ea
								  WHERE  ea.Completed = 1
										 AND ea.UserExmaninationId = uesq.Id) UserAttemptCounts
						  FROM   UserExamination uesq) ue
						 RIGHT JOIN Examination e
								 ON( e.id = ue.ExaminationId
									 AND ue.userId = @userId )
				  WHERE  e.CourseId = @courseId
						 AND e.isdeleted = 0
		  END')

END

 IF object_id('[dbo].[Sp_GetEmployeeAssignedExams]') IS NULL
BEGIN
EXEC('CREATE PROCEDURE [dbo].[Sp_GetEmployeeAssignedExams](@userId   INT,
																@q        NVARCHAR(150),
																@pageNo   INT,
																@pageSize INT,
																@deleted  BIT)
				 AS
				  BEGIN
					  SELECT e.Id                                      ExaminationId,
							 e.[Name],
							 LEFT(e.[Description], 200) + ''...''        [Description],
							 CONVERT(NVARCHAR(15), e.EndDate, 106)      EndDate,
							 CONVERT(NVARCHAR(15), e.StartDate, 106)    StartDate,
							 (SELECT Isnull(Count(ea.Id), 0)
							  FROM   ExaminationAttempt ea
							  WHERE  ea.Completed = 1
									 AND ea.UserExmaninationId = ue.Id) UserAttemptCounts,
							 Count(ue.id)
							   OVER ()                                  TotalCount
					  FROM   UserExamination ue
							 JOIN Examination e
							   ON( e.Id = ue.ExaminationId
								   AND e.EndDate IS NULL
									OR e.EndDate >= Sysdatetime() )
					  WHERE  ue.UserId = @userId
							 AND (SELECT Isnull(Count(ea.Id), 0)
								  FROM   ExaminationAttempt ea
								  WHERE  ea.Completed = 1
										 AND ea.UserExmaninationId = ue.Id) < ( isnull(ue.Attempt,0) + isnull(ue.ExtraAttempt,0) )
							 AND ( @q IS NULL    OR e.[Name] LIKE ''%'' + @q + ''%'' )
							 AND ( e.isdeleted = 0    OR ( e.IsDeleted = @deleted ) )
							 AND ( ue.isdeleted = 0     OR ( ue.IsDeleted = @deleted ) )
					  ORDER  BY ue.CreatedDate DESC
					  OFFSET (@pageSize * (@pageNo-1)) ROWS FETCH NEXT @pageSize ROWS ONLY
				  END')
END


 IF object_id('[dbo].[Sp_GetExaminationDetails]') IS NULL
BEGIN
EXEC('CREATE PROCEDURE [dbo].[Sp_GetExaminationDetails](@id     INT,
															  @userId INT,
															  @orgId  INT)
			AS
			  BEGIN
				  SELECT e.Id                                       ExaminationId,
						 e.[Name],
						 e.[Description],
						 e.PassGrade,
						 e.ExamRetakeCount,
						 e.HourLimit,
						 e.MinuteLimit,
						 CONVERT(NVARCHAR(15), e.EndDate, 106)      EndDate,
						 CONVERT(NVARCHAR(15), e.StartDate, 106)    StartDate,
						 (SELECT Isnull(Count(ea.Id), 0)
						  FROM   ExaminationAttempt ea
						  WHERE  ea.Completed = 1
								 AND ea.UserExmaninationId = ue.Id) UserAttemptCounts,
						 Count(ue.id)
						   OVER ()                                  TotalCount
				  FROM   UserExamination ue
						 JOIN Examination e
						   ON( e.Id = ue.ExaminationId
							   AND e.EndDate IS NULL
								OR e.EndDate >= Sysdatetime() )
				  WHERE  ue.UserId = @userId
						 AND e.Id = @id
						  AND (SELECT Isnull(Count(ea.Id), 0)
								  FROM   ExaminationAttempt ea
								  WHERE  ea.Completed = 1
										 AND ea.UserExmaninationId = ue.Id) < ( isnull(ue.Attempt,0) + isnull(ue.ExtraAttempt,0) )
						 AND ( e.IsDeleted = 0 AND ue.IsDeleted = 0 )
			  END')
END

IF object_id('[dbo].Sp_validateOrganizationExamination') is null
BEGIN
EXEC('CREATE PROCEDURE [dbo].Sp_validateOrganizationExamination (@id    INT,
                                                  @orgId INT)
AS
  BEGIN
      SELECT Id as ExaminationId
      FROM   Examination
      WHERE  StartDate >= Sysdatetime()
             AND EndDate <= Sysdatetime()
             AND Id = @id
             AND OrganizationId = @orgId
             AND isdeleted = 0
  END ')
END