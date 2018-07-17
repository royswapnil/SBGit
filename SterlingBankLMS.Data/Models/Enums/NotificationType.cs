using System.ComponentModel;

namespace SterlingBankLMS.Data.Models.Enums
{
    public enum NotificationType
    {
        [Description("New Account")]
        NewAccount = 1,
        [Description("User Profile Updated")]
        UserProfileEdited,
        [Description("Pending User Profile Change")]
        PendingUserProfileChange,
        [Description("Forgot Password")]
        ForgotPassword,
        [Description("Course Assignment")]
        CourseAssignment,
        [Description("Course Inactivity")]
        CourseInactivity,
        [Description("Weekly Update")]
        WeeklyUpdate,
        [Description("New Discussion Reply")]
        NewDiscussionReply,
        [Description("New Moderate Comment")]
        NewModerateComment,
        [Description("Pending Moderate Comment")]
        PendingModerateComments,
        [Description("Examination Assignment")]
        ExamAssignment,
        [Description("Examination Inactivity")]
        ExamInactivity,
        [Description("Examination Due Date Prompt")]
        ExamDueDatePrompt,
        [Description("Survey Assignment")]
        SurveyAssignment,
        [Description("Training Assignment")]
        TrainingAssignment,
        [Description("Upcoming Training Period")]
        UpcomingTrainingPeriod,
        [Description("New Training Request")]
        NewTrainingRequest,
        [Description("Training Request Pending Admin Approval")]
        PendingTrainingRequests,
        [Description("Training Request Pending Line Manager Approval")]
        TrainingRequestPendingAdminApproval,
        [Description("Training Request Approved By Line Manager")]
        TrainingRequestApprovedByLineManager,
        [Description("Training Request Approved By Admin")]
        TrainingRequestApprovedByAdmin,
        [Description("Training Request Rejected by Line Manager")]
        TrainingRequestRejectedByLineManager,
        [Description("Training Request Rejected by Admin")]
        TrainingRequestRejectedByAdmin,
        [Description("New Training Video Added")]
        NewTrainingVideo,
        [Description("New Admin Support Ticket")]
        NewAdminSupportTicket,
        [Description("New IT Escalated Tickets")]
        NewITSupportTicket,
        [Description("Support Ticket Closed")]
        SupportTicketClosed,
        [Description("Un Read Tickets")]
        UnOpenedTicket,
        [Description("Training Budget Depletion 50%")]
        TrainingBudgetDepletion50percent,
        [Description("Training Budget Depletion 90%")]
        TrainingBudgetDepletion90percent,
        [Description("Training Budget Depletion 100%")]
        TrainingBudgetDepletion100percent,
        [Description("Ticket Reply")]
        TicketReply,
        [Description("New Message")]
        NormalMessage,
        [Description("Group Budget Update")]
        BudgetChange,
        [Description("Employee Update")]
        EmployeeUpdate,
        [Description("Course Bulk Update Message Start")]
        CourseBulkUpdateStart,
        [Description("Course Bulk Update Message End")]
        CourseBulkUpdateEnd


    }
}
