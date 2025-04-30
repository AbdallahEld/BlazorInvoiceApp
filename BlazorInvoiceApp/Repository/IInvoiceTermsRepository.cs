using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.DTOS;

namespace BlazorInvoiceApp.Repository
{
    public interface IInvoiceTermsRepository : IGenericOwnedRepository<InvoiceTerms , InvoiceTermsDTO>
    {
    }
}
