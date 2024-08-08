using Accountant.Model.Dto;
using Accountant.Web.Services.Contract;
using Microsoft.AspNetCore.Components;
using System.Transactions;

namespace Accountant.Web.Pages.IncomePages
{
    public class DisplayIncomeTransactionBase : ComponentBase
    {
        [Parameter]
        public ICollection<IncomeTransactionDto> IncomeTransactions { get; set; }

        [Parameter]
        public string Username { get; set; }

        [Parameter]
        public string Password { get; set; }

        [Inject]
        public IIncomeServices IncomeServices { get; set; }

        public IncomeTransactionDto incomeTransaction { get; set; }
    }
}
