using Accountant.API.Entities;
using Accountant.API.Repository.Interfaces;
using Accountant.Model.Dto;
using System.Collections.ObjectModel;

namespace Accountant.API.Business
{
    public class InstallmentBusiness
    {
        private readonly IinstallmentRepository _repository;
        private readonly LoanBusiness _loanBusiness;

        public InstallmentBusiness(IinstallmentRepository repository, LoanBusiness loanBusiness)
        {
            _repository = repository;
            _loanBusiness = loanBusiness;
        }

        public async Task<ICollection<Installment>> AddInstallments(LoanDto loan)
        {
            try
            {

                var installmnetsValue = await _loanBusiness.MonthlyAmount(loan);

                ICollection<Installment> installments = new Collection<Installment>();
                for (var i = 0; i < loan.PeriodPerMonth; i++)
                {
                    Installment installment = new Installment();
                    installment.PayOrNo = false;
                    installment.Amount = installmnetsValue;
                    installment.PayTime = loan.StartTime.AddMonths(i);
                    installments.Add(installment);
                }

                return installments;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ICollection<Installment>> UpdateInstallment(LoanDto loan)     // Maybe Update Count of installment , need generate
        {
            double PriceAfterUpdate = await _loanBusiness.MonthlyAmount(loan);

            var installments = new Collection<Installment>();
            for (var i = 0; i < loan.PeriodPerMonth; i++)
            {
                var Installment = new Installment();

                Installment.Amount = PriceAfterUpdate;
                Installment.PayTime = loan.StartTime.AddMonths(i);
                Installment.PayOrNo = false;

                installments.Add(Installment);
            }
            return installments;
        }
    }
}
