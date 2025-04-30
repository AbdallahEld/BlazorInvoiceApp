using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.DTOS;

namespace BlazorInvoiceApp.Repository
{
    public interface IInvoiceLineItemRepository : IGenericOwnedRepository<InvoiceLineItem , InvoiceLineItemDTO>
    {

    }
}
