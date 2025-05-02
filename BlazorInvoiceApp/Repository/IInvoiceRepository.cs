using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.DTOS;
using System.Security.Claims;

namespace BlazorInvoiceApp.Repository
{
    public interface IInvoiceRepository : IGenericOwnedRepository<Invoice , InvoiceDTO>
    {
        public Task DeleteWithLineItems(ClaimsPrincipal user, string invoiceId);

        public Task<List<InvoiceDTO>> GetAllMineFlat(ClaimsPrincipal user);
    }
}
