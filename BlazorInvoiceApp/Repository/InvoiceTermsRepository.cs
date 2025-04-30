using AutoMapper;
using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.DTOS;

namespace BlazorInvoiceApp.Repository
{
    public class InvoiceTermsRepository : GenericOwnedRepository<InvoiceTerms , InvoiceTermsDTO>, IInvoiceTermsRepository
    {
        public InvoiceTermsRepository(ApplicationDbContext context,IMapper mapper) : base (context ,mapper)
        {
            
        }
    }
}
