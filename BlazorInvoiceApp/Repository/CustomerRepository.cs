using AutoMapper;
using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.DTOS;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorInvoiceApp.Repository
{
    public class CustomerRepository : GenericOwnedRepository<Customer , CustomerDTO>, ICustomerRepository
    {

        public CustomerRepository(ApplicationDbContext context, IMapper mapper) : base (context , mapper)
        {
        }

        public async Task Seed(ClaimsPrincipal user)
        {
            string? userId = getMyUserId(user);
            if (userId is not null)
            {
                int count = await _context.Customers.Where(e => e.UserId == userId).CountAsync();
                if(count == 0)
                {
                    CustomerDTO defaultCusomer = new CustomerDTO()
                    {
                        Name = "My First Customer"
                    };
                    await AddMine(user , defaultCusomer);
                }
            }
            return;
        }
    }
}
