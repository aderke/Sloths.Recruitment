using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.DTO.Company;
using SlothEnterprise.ProductApplication.DTO.Product;
using SlothEnterprise.ProductApplication.Processors;
using System;

namespace SlothEnterprise.ProductApplication.ServiceLocator
{
    public class ProcessorServiceLocator
    {
        private readonly ISelectInvoiceService selectInvoiceService;
        private readonly IConfidentialInvoiceService confidentialInvoiceWebService;
        private readonly IBusinessLoansService businessLoansService;

        // should be assinged directly inside proccessors via DI
        public ProcessorServiceLocator(
            ISelectInvoiceService selectInvoiceService,
            IConfidentialInvoiceService confidentialInvoiceWebService,
            IBusinessLoansService businessLoansService)
        {
            this.selectInvoiceService = selectInvoiceService;
            this.confidentialInvoiceWebService = confidentialInvoiceWebService;
            this.businessLoansService = businessLoansService;
        }

        public IProductProcessor GetService(ISellerApplication application)
        {
            var product = application.Product;

            if (product is SelectInvoiceDiscount)
            {
                return new SelectInvoiceDiscountProcessor(application, selectInvoiceService);
            }
            else if (product is ConfidentialInvoiceDiscount)
            {
                return new ConfidentialInvoiceDiscountProcessor(application, confidentialInvoiceWebService);
            }
            else if (product is BusinessLoans)
            {
                return new BusinessLoansProcessor(application, businessLoansService);
            }

            throw new ArgumentException(message: "Product is not a recognized", paramName: nameof(product));
        }
    }
}
