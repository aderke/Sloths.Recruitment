# Here are my thoughts:

#1 The interface implementation classes inside the 'Application' folder seems redundant - they will not have other 
# interface implementations, so there is no need to do SellerApplication: ISellerApplication and SellerCompanyData: ISellerCompanyData there. 

#2 It doesn't make sense to create a separate IProduct interface with only one Id field. Only one member (Id) does not help
# distinguish between different types of products. Instead we can do a base abstract Product class.

#3 The ProductApplicationService takes care of a lot. Save operations are none of his business. The goal here is to make an input service
# cleaner - get a request, choose a suitable manager for processing and return the result. That's all.

#4 Services do not provide a clear and unique way to work with results. Sometimes, when checking the result, the ApplicationId is asked,
# sometimes not. The consistency is broken.

#5 Some business values (such as AdvancePercentage) are embedded in the design of the class. Values shouldn't go there. Hard to find, to 
# predict and support them. I introduced DTO's objects and DTO should be as clean as possible. 

#6 Putting all dependencies (ISelectInvoiceService, IConfidentialInvoiceService, IBusinessLoansService) inside ProductApplicationService not needed
# and not useful at all. It should be moved to the corresponded processors via constructor injection.

#7 The entry point (contact interface) must remain intact for service clients for versioning and backward compatibility.

# PROPOSED SOLUTION - use smth between Abstract Factory (Service Locator) pattern to resolve service->product manager dependencies.
# Also I'll add tests that will cover usage for different products.
# It will be easy to support and add new product logic: add new supported type into Product DTO folder, describe it, inherit from Product, 
# add a new specific processor and do all specific work there, refgister new processor via ProcessorSerivceLocator


#                                      ProductApplicationService
#                                                 |
#                                       ProductSerivceLocator
#                 ________________________________|_______________________________________
#                 |                               |                                       |
# ConfidentialInvoiceDiscountProcessor    SelectiveInvoiceDiscountProcessor     BusinessLoansProcessor
# (ConfidentialInvoiceDiscount)            (SelectiveInvoiceDiscount)            (BusinessLoans)