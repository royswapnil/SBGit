using System.ComponentModel;

namespace SterlingBankLMS.Data.Models.Enums
{
    public enum TrainingApprovalStatus
    {
        [Description("Pending Line Manager Approval")]
        PendingLineManagerApproval = 1,
        [Description("Pending Admin Approval")]
        PendingAdminApproval,
        [Description("Rejected by Line Manager")]
        RejectedByLineManager,
        [Description("Rejected by Admin")]
        RejectedByAdmin,
        [Description("Approved")]
        Approved
    }
}
