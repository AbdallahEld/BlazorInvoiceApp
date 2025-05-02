using AutoMapper;
using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.DTOS;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorInvoiceApp.Repository
{
    public class InvoiceRepository : GenericOwnedRepository<Invoice , InvoiceDTO>, IInvoiceRepository
    {
        public InvoiceRepository(ApplicationDbContext context,IMapper mapper) : base(context,mapper)
        {
            
        }

        public async Task DeleteWithLineItems(ClaimsPrincipal user, string invoiceId)
        {
            string? userId = getMyUserId(user);
            if (userId is not null)
            {
                var lineItems = await _context.invoiceLineItems.Where(e => e.InvoiceId == invoiceId && e.UserId == userId).ToListAsync();
                foreach (var lineItem in lineItems)
                {
                    _context.invoiceLineItems.Remove(lineItem);
                }
                Invoice? invoice = await _context.Invoices.Where(e => e.Id == invoiceId && e.UserId == userId).FirstOrDefaultAsync();
                if (invoice != null)
                {
                    _context.Invoices.Remove(invoice);
                }
            }
            return;
        }

        public async Task<List<InvoiceDTO>> GetAllMineFlat(ClaimsPrincipal user)
        {
            string? userId = getMyUserId(user);
            var q = from i in _context.Invoices.Where(i => i.UserId == userId).Include(i => i.InvoiceTerms).Include(i => i.Customer).Include(i => i.InvoiceLineItems)
                    select new InvoiceDTO
                    {
                        Id = i.Id,
                        CreateDate = i.CreateDate,
                        InvoiceNumber = i.InvoiceNumber,
                        Description = i.Description,
                        CustomerId = i.CustomerId,
                        CustomerName = i.Customer!.Name,
                        InvoiceTermsId = i.InvoiceTermsId,
                        InvoiceTermsName = i.InvoiceTerms!.Name,
                        Paid = i.Paid,
                        Credit = i.Credit,
                        TaxRate = i.TaxRate,
                        UserId = i.UserId,
                        InvoiceTotal = i.InvoiceLineItems.Sum(e => e.TotalPrice)
                    };
            List<InvoiceDTO> invoices = await q.ToListAsync();
            return invoices;
        }
    }
}
