using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.DTO.Company;
using SlothEnterprise.ProductApplication.ServiceLocator;

namespace SlothEnterprise.ProductApplication
{
    public class ProductApplicationService
    {
        private readonly ProcessorServiceLocator locator;

        public ProductApplicationService(
            ISelectInvoiceService selectInvoiceService, 
            IConfidentialInvoiceService confidentialInvoiceWebService, 
            IBusinessLoansService businessLoansService)
        {
            // should be passed with DI directly inside processors
            locator = new ProcessorServiceLocator(selectInvoiceService, confidentialInvoiceWebService, businessLoansService);
        }
                
        public int SubmitApplicationFor(ISellerApplication application)
        {
            var processor = locator.GetService(application);
            return processor.ProcessProduct();
        }
    }
}
