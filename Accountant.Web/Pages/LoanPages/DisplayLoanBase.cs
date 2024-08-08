using Accountant.Model.Dto;
using Accountant.Web.Services.Contract;
using Microsoft.AspNetCore.Components;

namespace Accountant.Web.Pages.LoanPages
{
    public class DisplayLoanBase : ComponentBase
    {
        [Parameter]
        public LoanDto Loan { get; set; }
        [Parameter]
        public int UserID { get; set; }
        [Parameter]
        public string Username { get; set; }
        [Parameter]
        public string Password { get; set; }

        [Inject]
        public IinstallmentServices installmentServices { get; set; }

        [Inject]
        public NavigationManager navigate { get; set; }

        public ICollection<InstallmentDto> Installments { get; set; }
        public string RecursiveAmount { get; set; } = "";
        public double LoanAmount { get; set; } = 0;
        public int InstallmentCount { get; set; } = 0;
        public string LastPayTime { get; set; } = "";
        public double Percentage { get; set; } = 0;

        public string DeleteURL { get; set; }
        public string UpdateURL { get; set; }

        public string ErrorMessage { get; set; }
        protected override async Task OnParametersSetAsync()
        {
            try
            {
                Installments = await installmentServices.GetInstallments(Loan.ID);
                foreach (var item in Installments)
                {
                    if (!item.PayOrNo)
                    {
                        LastPayTime = item.PayTime.ToString("MM/dd/yy");
                        break;
                    }
                }
                LoanAmount = Loan.LoanAmount;
                RecursiveAmount = Loan.RecursiveAmount.ToString("00.00");
                InstallmentCount = Installments.Count;
                Percentage = Loan.Percentage;

                DeleteURL = $"/DeleteLoan/{UserID}/{Username}/{Password}/{Loan.ID}";
                UpdateURL = $"/UpdateLoan/{UserID}/{Username}/{Password}/{Loan.ID}";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public void GetToInstallments()
        {
            navigate.NavigateTo("/");
        }
    }
}
