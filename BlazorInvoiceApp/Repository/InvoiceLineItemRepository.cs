using AutoMapper;
using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.DTOS;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorInvoiceApp.Repository
{
    public class InvoiceLineItemRepository : GenericOwnedRepository<InvoiceLineItem , InvoiceLineItemDTO>, IInvoiceLineItemRepository
    {
        public InvoiceLineItemRepository(ApplicationDbContext context, IMapper mapper) : base (context,mapper)
        {
            
        }

        public async Task<List<InvoiceLineItemDTO>> GetAllByInvoiceId(ClaimsPrincipal user, string invoiceId)
        {
            return await GetQuery(user , l => l.InvoiceId == invoiceId , null!);
        }

        //public async Task<List<InvoiceLineItem>> GetAllByInvoiceId(ClaimsPrincipal user, string invoiceId)
        //{
        //    string? userId = getMyUserId(user);
        //    if (userId is not null)
        //    {
        //        List<InvoiceLineItem> result = await _context.invoiceLineItems.Where(e => e.InvoiceId == invoiceId && e.UserId == userId).ToListAsync();
        //        return result;
        //    }
        //    return new List<InvoiceLineItem>();
        //}
    }
}
