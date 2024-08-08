using Accountant.Model.Dto;
using Microsoft.AspNetCore.Components;

namespace Accountant.Web.Pages.PaymentPages
{
    public class DisplayPeymentTransactionBase : ComponentBase
    {
        [Parameter]
        public ICollection<PaymentTransactionDto> PaymentTransactions { get; set; }

        [Parameter]
        public string Username { get; set; }

        [Parameter]
        public string Password { get; set; }
    }
}
