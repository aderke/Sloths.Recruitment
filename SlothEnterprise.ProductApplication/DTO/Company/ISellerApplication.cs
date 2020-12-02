using System;

namespace SlothEnterprise.ProductApplication.DTO.Company
{
    public interface ISellerApplication
    {
        Product.Product Product { get; set; }
        SellerCompanyData CompanyData { get; set; }
    }
    public class SellerCompanyData
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public string DirectorName { get; set; }
        public DateTime Founded { get; set; }
    }
}