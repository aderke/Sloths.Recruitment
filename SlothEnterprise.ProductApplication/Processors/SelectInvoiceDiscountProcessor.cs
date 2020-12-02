using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.DTO.Company;
using SlothEnterprise.ProductApplication.DTO.Product;
using SlothEnterprise.ProductApplication.ServiceLocator;

namespace SlothEnterprise.ProductApplication.Processors
{
    public class SelectInvoiceDiscountProcessor :  IProductProcessor
    {
        private readonly ISellerApplication application;
        // should be initialized with DI via ctor
        private readonly ISelectInvoiceService service;

        private const decimal advancePercentage = 0.80M;

        public SelectInvoiceDiscountProcessor(ISellerApplication application, ISelectInvoiceService service)
        {
            this.application = application;
            this.service = service;
        }

        public int ProcessProduct()
        {
            var product = (SelectInvoiceDiscount) application.Product;
            return service.SubmitApplicationFor(
                    application.CompanyData.Number.ToString(), 
                    product.InvoiceAmount, 
                    advancePercentage
                );
        }
    }
}
