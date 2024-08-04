using Accountant.API.Entities;
using Accountant.API.Repository.Interfaces;
using Accountant.Model.Dto;
using System.Collections.ObjectModel;

namespace Accountant.API.Business
{
    public class InstallmentBusiness
    {
        private readonly IinstallmentRepository _repository;

        public InstallmentBusiness(IinstallmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<Installment>> AddInstallments(LoanDto loan)
        {
            try
            {
                var installmnetsValue = await Calculate(loan);
                ICollection<Installment> installments = new Collection<Installment>();
                for (var i = 0; i < loan.PeriodPerMonth; i++)
                {

                    //installments.Add()
                }

                return installments;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ICollection<Installment>> UpdateInstallment(LoanDto loan)
        {

            var installments = await _repository.GetInstallmentsByLoanID(loan.ID);
            foreach (var installment in installments)
            {

            }
            return installments;
        }

        public async Task<double> Calculate(LoanDto loan)
        {
            return 0.0;
        }
    }
}
