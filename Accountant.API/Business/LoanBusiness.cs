using Accountant.Model.Dto;

namespace Accountant.API.Business
{
    public class LoanBusiness
    {
        public async Task<double> RecursiveAmount(LoanDto loan)
        {
            double MonthlyPayment = await MonthlyAmount(loan);

            return MonthlyPayment * loan.PeriodPerMonth;
        }

        public async Task<double> MonthlyAmount(LoanDto loan)
        {
            var OrginalAmount = loan.LoanAmount;
            var Percentage = (loan.Percentage * .01) / 12;
            var BaseFormula = Math.Pow(1 + Percentage, loan.PeriodPerMonth);

            var Top = Percentage * BaseFormula;
            var Down = BaseFormula - 1;

            var MonthlyPayment = (OrginalAmount * Top) / Down;

            return MonthlyPayment;
        }
    }
}
