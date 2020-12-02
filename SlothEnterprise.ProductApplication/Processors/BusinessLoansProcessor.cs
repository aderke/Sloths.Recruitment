using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.DTO.Company;
using SlothEnterprise.ProductApplication.DTO.Product;
using SlothEnterprise.ProductApplication.ServiceLocator;

namespace SlothEnterprise.ProductApplication.Processors
{
   public class BusinessLoansProcessor : IProductProcessor
   {
        private readonly ISellerApplication application;
        // should be initialized with DI via ctor
        private readonly IBusinessLoansService service;      

        public BusinessLoansProcessor(ISellerApplication application, IBusinessLoansService service)
        {
            this.application = application;
            this.service = service;
        }

        public int ProcessProduct()
        {
            var loans = application.Product as BusinessLoans;
            var result = service.SubmitApplicationFor(
                new CompanyDataRequest
                {
                    CompanyFounded = application.CompanyData.Founded,
                    CompanyNumber = application.CompanyData.Number,
                    CompanyName = application.CompanyData.Name,
                    DirectorName = application.CompanyData.DirectorName
                }, 
                new LoansRequest
                {
                    InterestRatePerAnnum = loans.InterestRatePerAnnum,
                    LoanAmount = loans.LoanAmount
                }
            );
            return (result.Success) ? result.ApplicationId ?? -1 : -1;
        }
    }
}
