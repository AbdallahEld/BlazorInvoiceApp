using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.DTOS;

namespace BlazorInvoiceApp.Repository
{
    public interface IInvoiceRepository : IGenericOwnedRepository<Invoice , InvoiceDTO>
    {
    }
}
