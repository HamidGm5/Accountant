using Accountant.Web.Services.Contract;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Accountant.Web.Pages.Suggestion
{
    public class SuggestionBase : ComponentBase
    {
        [Parameter]
        public string Username { get; set; }
        [Parameter]
        public string Password { get; set; }

        [Inject]
        public IUserServices UserServices { get; set; }
        [Inject]
        public IIncomeServices incomeServices { get; set; }
        [Inject]
        public IJSRuntime JS { get; set; }
        [Inject]
        public NavigationManager navigation { get; set; }

        public bool UrlOk { get; set; }

        public double IncomeInYear { get; set; }
        public double Tax { get; set; }             // Tax is the common
        public SuggestionModel EcoSuggestion { get; set; } = new SuggestionModel();
        public SuggestionModel NormalSuggestion { get; set; } = new SuggestionModel();
        public SuggestionModel HighSuggestion { get; set; } = new SuggestionModel();

        public ICollection<SuggestionModel> Models { get; set; } = new List<SuggestionModel>();
        public ICollection<SuggestionModel> ModelsForMonth { get; set; } = new List<SuggestionModel>();

        public double NetIncomeInMonth { get; set; }
        public double TaxInMonth { get; set; }

        public SuggestionModel EcoSuggestionInMonth { get; set; } = new SuggestionModel();
        public SuggestionModel NormalSuggestionInMonth { get; set; } = new SuggestionModel();
        public SuggestionModel HighSuggestionInMonth { get; set; } = new SuggestionModel();

        public string ErrorMessage { get; set; }


        protected async override Task OnParametersSetAsync()
        {
            try
            {
                if (await UserServices.Login(Username, Password) != null)
                {
                    UrlOk = true;
                }
                else
                {
                    ErrorMessage = "Your username and password is not match !";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public async Task Calculate_Click()
        {
            try
            {
                if (IncomeInYear > 0)
                {
                    Models.Clear(); // for prevent of iteration
                    ModelsForMonth.Clear();

                    Tax = TaxCalculate();
                    TaxInMonth = (Tax / 12);
                    NetIncomeInMonth = (IncomeInYear / 12) - TaxInMonth;

                    EcoCalculate();
                    NormalCalculate();
                    HighCalculate();

                    Models.Add(EcoSuggestion);
                    Models.Add(NormalSuggestion);
                    Models.Add(HighSuggestion);

                    EcoInMonth();
                    NormalInMonth();
                    HighInMonth();

                    ModelsForMonth.Add(EcoSuggestionInMonth);
                    ModelsForMonth.Add(NormalSuggestionInMonth);
                    ModelsForMonth.Add(HighSuggestionInMonth);

                }
                else
                {
                    await JS.InvokeVoidAsync("alert", "Enter your Salary Per Year !");
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        private double TaxCalculate()           // Tax in US country without state tax ,
                                                // reference is https://www.irs.gov/filing/federal-income-tax-rates-and-brackets
        {
            if (IncomeInYear < 11000)
            {
                return 0.1 * IncomeInYear; // 10%
            }
            else if (IncomeInYear > 11000 && IncomeInYear < 44725)
            {
                return 0.12 * IncomeInYear; // 12%
            }
            else if (IncomeInYear > 44725 && IncomeInYear < 95375)
            {
                return 0.22 * IncomeInYear; // 22%
            }
            else if (IncomeInYear > 95375 && IncomeInYear < 182100)
            {
                return 0.24 * IncomeInYear; // 24%

            }
            else if (IncomeInYear > 182100 && IncomeInYear < 231250)
            {
                return 0.32 * IncomeInYear; // 32%

            }
            else if (IncomeInYear > 231250 && IncomeInYear < 578125)
            {
                return 0.35 * IncomeInYear; // 35%

            }
            else if (IncomeInYear > 578125)
            {
                return 0.37 * IncomeInYear; // 37%

            }
            else
            {
                return 0;
            }
        }

        public void EcoCalculate()
        {
            double netIncome = IncomeInYear - Tax;  // use common variable for netIncome
            double TempRemain = netIncome;

            EcoSuggestion.Name = "Eco";
            EcoSuggestion.Tax = Tax;
            EcoSuggestion.Edible = 0.15 * netIncome;
            TempRemain -= EcoSuggestion.Edible;
            EcoSuggestion.Recreation = 0.05 * netIncome; //20%
            TempRemain -= EcoSuggestion.Recreation;
            EcoSuggestion.HomeRent = 0.40 * netIncome;    //60%
            TempRemain -= EcoSuggestion.HomeRent;
            EcoSuggestion.Saving = TempRemain;  // 40%

        }
        public void NormalCalculate()
        {
            double netIncome = IncomeInYear - Tax;
            double TempRemain = netIncome;

            NormalSuggestion.Name = "Normal";
            NormalSuggestion.Tax = Tax;
            NormalSuggestion.Edible = 0.25 * netIncome;
            TempRemain -= NormalSuggestion.Edible;
            NormalSuggestion.Recreation = 0.10 * netIncome; // 40%
            TempRemain -= NormalSuggestion.Recreation;
            NormalSuggestion.HomeRent = 0.45 * netIncome; // 75%
            TempRemain -= NormalSuggestion.HomeRent;
            NormalSuggestion.Saving = TempRemain;   // 25%
        }
        public void HighCalculate()
        {
            double netIncome = IncomeInYear - Tax;
            var TempRemain = netIncome;

            HighSuggestion.Name = "High";
            HighSuggestion.Tax = Tax;
            HighSuggestion.Edible = 0.30 * netIncome;
            TempRemain -= HighSuggestion.Edible;
            HighSuggestion.Recreation = 0.15 * netIncome;    // 45%
            TempRemain -= HighSuggestion.Recreation;
            HighSuggestion.HomeRent = 0.50 * netIncome;   // 95%
            TempRemain -= HighSuggestion.HomeRent;
            HighSuggestion.Saving = TempRemain;         //5%
        }


        public void EcoInMonth()
        {
            double TempRemain = NetIncomeInMonth;

            EcoSuggestionInMonth.Name = "Eco";
            EcoSuggestionInMonth.Tax = TaxInMonth;

            EcoSuggestionInMonth.Edible = 0.15 * NetIncomeInMonth;
            TempRemain -= EcoSuggestionInMonth.Edible;

            EcoSuggestionInMonth.Recreation = 0.05 * NetIncomeInMonth; //20%
            TempRemain -= EcoSuggestionInMonth.Recreation;

            EcoSuggestionInMonth.HomeRent = 0.40 * NetIncomeInMonth;    //60%
            TempRemain -= EcoSuggestionInMonth.HomeRent;

            EcoSuggestionInMonth.Saving = TempRemain;  // 40%


        }
        public void NormalInMonth()
        {
            double TempRemain = NetIncomeInMonth;

            NormalSuggestionInMonth.Name = "Normal";
            NormalSuggestionInMonth.Tax = TaxInMonth;

            NormalSuggestionInMonth.Edible = 0.25 * NetIncomeInMonth;
            TempRemain -= NormalSuggestionInMonth.Edible;

            NormalSuggestionInMonth.Recreation = 0.10 * NetIncomeInMonth; // 40%
            TempRemain -= NormalSuggestionInMonth.Recreation;

            NormalSuggestionInMonth.HomeRent = 0.45 * NetIncomeInMonth; // 75%
            TempRemain -= NormalSuggestionInMonth.HomeRent;

            NormalSuggestionInMonth.Saving = TempRemain;   // 25%
        }
        public void HighInMonth()
        {
            var TempRemain = NetIncomeInMonth;

            HighSuggestionInMonth.Name = "High";
            HighSuggestionInMonth.Tax = TaxInMonth;

            HighSuggestionInMonth.Edible = 0.30 * NetIncomeInMonth;
            TempRemain -= HighSuggestionInMonth.Edible;

            HighSuggestionInMonth.Recreation = 0.15 * NetIncomeInMonth;    // 45%
            TempRemain -= HighSuggestionInMonth.Recreation;

            HighSuggestionInMonth.HomeRent = 0.50 * NetIncomeInMonth;   // 95%
            TempRemain -= HighSuggestionInMonth.HomeRent;

            HighSuggestionInMonth.Saving = TempRemain;         //5%
        }
    }
}
