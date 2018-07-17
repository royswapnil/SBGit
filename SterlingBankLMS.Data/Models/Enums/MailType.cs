using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Models.Enums
{
    public enum MailType
    {
        NewAccount = 1,
        ForgotPassword,
        NewSupportTicketReceived,
        NewSupportTicketSent,
        TicketReply,
        NormalMessage,
        BudgetChange,
        BudgetExpenditureUpdate,
        TrainingRequestStatus,
        TrainingAssignment,
        UserCreated,
        EmployeeCreated,
        EmployeeUpdated
    }
}
