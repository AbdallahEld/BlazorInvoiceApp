namespace BlazorInvoiceApp.Repository
{
    public interface IRepositoryCollection : IDisposable
    {
        ICustomerRepository Customer { get; }
        IInvoiceRepository Invoice { get; }
        IInvoiceLineItemRepository InvoiceLineItem { get; }
        IInvoiceTermsRepository InvoiceTerms { get; }

        Task<int> Save();
    }
}
