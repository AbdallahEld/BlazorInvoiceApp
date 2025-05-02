using AutoMapper;
using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.DTOS;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorInvoiceApp.Repository
{
    public class InvoiceTermsRepository : GenericOwnedRepository<InvoiceTerms , InvoiceTermsDTO>, IInvoiceTermsRepository
    {
        public InvoiceTermsRepository(ApplicationDbContext context,IMapper mapper) : base (context ,mapper)
        {
            
        }

        public async Task Seed(ClaimsPrincipal user)
        {
            string? userId = getMyUserId(user);
            if (userId == null)
            {
                int count = await _context.InvoiceTerms.Where(e => e.UserId == userId).CountAsync();
                if (count == 0)
                {
                    InvoiceTermsDTO term1 = new InvoiceTermsDTO()
                    {
                        Name = "Net 30"
                    };
                    InvoiceTermsDTO term2 = new InvoiceTermsDTO()
                    {
                        Name = "Net 60"
                    };
                    InvoiceTermsDTO term3 = new InvoiceTermsDTO()
                    {
                        Name = "Net 90"
                    };
                    await AddMine(user, term1);
                    await AddMine(user, term2);
                    await AddMine(user, term3);
                }
            }
            return;
        }
    }
}
