using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.DTO.Company;
using SlothEnterprise.ProductApplication.DTO.Product;
using SlothEnterprise.ProductApplication.ServiceLocator;

namespace SlothEnterprise.ProductApplication.Processors
{
    public class ConfidentialInvoiceDiscountProcessor : IProductProcessor
    {
        private readonly ISellerApplication application;
        // should be initialized with DI via ctor
        private readonly IConfidentialInvoiceService service;

        private const decimal vatRate = 0.20M;

        public ConfidentialInvoiceDiscountProcessor(ISellerApplication application, IConfidentialInvoiceService service)
        {
            this.application = application;
            this.service = service;
        }

        public int ProcessProduct()
        {
            var product = (ConfidentialInvoiceDiscount) application.Product;
            var result = service.SubmitApplicationFor(
                    new CompanyDataRequest
                    {
                        CompanyFounded = application.CompanyData.Founded,
                        CompanyNumber = application.CompanyData.Number,
                        CompanyName = application.CompanyData.Name,
                        DirectorName = application.CompanyData.DirectorName
                    },
                    product.TotalLedgerNetworth, 
                    product.AdvancePercentage, 
                    vatRate
                );

            return (result.Success) ? result.ApplicationId ?? -1 : -1;
        }
    }
}
