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
        public DateTime LastPayTime { get; set; } = DateTime.MinValue;
        public double Percentage { get; set; } = 0;
        public double Remain { get; set; } = 0.0;
        public bool NearToPay { get; set; } 

        public string? DeleteURL { get; set; }
        public string? UpdateURL { get; set; }
        public string? InstallmentsURL { get; set; }

        public string? ErrorMessage { get; set; }
        protected override async Task OnParametersSetAsync()
        {
            try
            {
                Installments = await installmentServices.GetInstallments(Loan.ID);
                foreach (var item in Installments)
                {
                    if (!item.PayOrNo)
                    {
                        LastPayTime = item.PayTime;
                        break;
                    }
                }

                foreach (var item in Installments)
                {
                    if (item.PayOrNo)
                    {
                        Remain += item.Amount;
                    }
                }

                Remain = (Loan.RecursiveAmount - Remain);

                LoanAmount = Loan.LoanAmount;
                RecursiveAmount = Loan.RecursiveAmount.ToString("00.00");
                InstallmentCount = Installments.Count;
                Percentage = Loan.Percentage;

                DeleteURL = $"/DeleteLoan/{UserID}/{Username}/{Password}/{Loan.ID}";
                UpdateURL = $"/UpdateLoan/{UserID}/{Username}/{Password}/{Loan.ID}";
                InstallmentsURL = $"/InstallmentsPage/{UserID}/{Username}/{Password}/{Loan.ID}";

                NearToPay = (LastPayTime - DateTime.Now).Days < 7;  // this is for if you have less than week the text of last pay time going to red !a

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public void GetToInstallments()
        {
            navigate.NavigateTo(InstallmentsURL);
        }
    }
}
