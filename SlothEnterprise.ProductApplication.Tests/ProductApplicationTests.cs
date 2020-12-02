using FluentAssertions;
using Moq;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.DTO.Company;
using SlothEnterprise.ProductApplication.DTO.Product;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class ProductApplicationTests
    {
        private readonly ProductApplicationService _sut;
        private readonly Mock<ISelectInvoiceService> _selectInvoiceServiceMock = new Mock<ISelectInvoiceService>();
        private readonly Mock<IConfidentialInvoiceService> _confidentialInvoiceServiceMock = new Mock<IConfidentialInvoiceService>();
        private readonly Mock<IBusinessLoansService> _businessLoansServiceMock = new Mock<IBusinessLoansService>();
        private readonly ISellerApplication _sellerApplication;
        private readonly Mock<IApplicationResult> _result = new Mock<IApplicationResult>();

        public ProductApplicationTests()
        {
            _sut = new ProductApplicationService(_selectInvoiceServiceMock.Object, _confidentialInvoiceServiceMock.Object, _businessLoansServiceMock.Object);
            _result.SetupProperty(p => p.ApplicationId, 1);
            _result.SetupProperty(p => p.Success, true);
            var sellerApplicationMock = new Mock<ISellerApplication>();
            sellerApplicationMock.SetupProperty(p => p.Product, new ConfidentialInvoiceDiscount());
            sellerApplicationMock.SetupProperty(p => p.CompanyData, new SellerCompanyData());
            _sellerApplication = sellerApplicationMock.Object;

            _confidentialInvoiceServiceMock.Setup(m => m.SubmitApplicationFor(It.IsAny<CompanyDataRequest>(), It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(_result.Object);
            _selectInvoiceServiceMock.Setup(m => m.SubmitApplicationFor(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(It.IsAny<int>);
            _businessLoansServiceMock.Setup(m => m.SubmitApplicationFor(It.IsAny<CompanyDataRequest>(), It.IsAny<LoansRequest>())).Returns(_result.Object);
        }

        [Fact]
        public void ProductApplicationService_SubmitApplicationFor_WhenCalledWithConfidentialInvoiceDiscount_ShouldReturnOne()
        {            
            var result = _sut.SubmitApplicationFor(_sellerApplication);
            
            result.Should().Be(1);
        }

        [Fact]
        public void ProductApplicationService_SubmitApplicationFor_WhenCalledWithSelectiveInvoiceDiscount_ShouldReturnOne()
        {
             var sut = new ProductApplicationService(_selectInvoiceServiceMock.Object, _confidentialInvoiceServiceMock.Object, _businessLoansServiceMock.Object);
            _result.SetupProperty(p => p.ApplicationId, 1);
            _result.SetupProperty(p => p.Success, true);
            var sellerApplicationMock = new Mock<ISellerApplication>();
            sellerApplicationMock.SetupProperty(p => p.Product, new SelectInvoiceDiscount());
            sellerApplicationMock.SetupProperty(p => p.CompanyData, new SellerCompanyData());
            var sellerApplication = sellerApplicationMock.Object;
            _selectInvoiceServiceMock.Setup(m => m.SubmitApplicationFor(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(1);

            var result = sut.SubmitApplicationFor(sellerApplication);

            result.Should().Be(1);
        }

        [Fact]
        public void ProductApplicationService_SubmitApplicationFor_WhenCalledWithBusinessLoans_ShouldReturnOne()
        {
            var sut = new ProductApplicationService(_selectInvoiceServiceMock.Object, _confidentialInvoiceServiceMock.Object, _businessLoansServiceMock.Object);
            _result.SetupProperty(p => p.ApplicationId, 1);
            _result.SetupProperty(p => p.Success, true);
            var sellerApplicationMock = new Mock<ISellerApplication>();
            sellerApplicationMock.SetupProperty(p => p.Product, new BusinessLoans());
            sellerApplicationMock.SetupProperty(p => p.CompanyData, new SellerCompanyData());
            var sellerApplication = sellerApplicationMock.Object;
            _businessLoansServiceMock.Setup(m => m.SubmitApplicationFor(It.IsAny<CompanyDataRequest>(), It.IsAny<LoansRequest>())).Returns(_result.Object);

            var result = sut.SubmitApplicationFor(sellerApplication);

            result.Should().Be(1);
        }
    }
}