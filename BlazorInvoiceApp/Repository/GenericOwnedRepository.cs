using AutoMapper;
using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.DTOS;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;

namespace BlazorInvoiceApp.Repository
{
    public class GenericOwnedRepository<TEntity, TDTO> : IGenericOwnedRepository<TEntity, TDTO>
        where TEntity : class, IEntity, IOwnedEntity
        where TDTO : class, IDTO, IOwnedDTO
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        protected string? getMyUserId(ClaimsPrincipal User)
        {
            Claim? userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId is not null)
            {
                return userId.Value;
            }
            else
                return null;
        }
        public GenericOwnedRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }        

        public async Task<List<TDTO>> GetAllMine(ClaimsPrincipal user)
        {
            string? userId = getMyUserId(user);
            if (userId is not null)
            {
                List<TEntity> entities = await _context.Set<TEntity>().Where(e => e.UserId == userId).ToListAsync();
                List<TDTO> dTOs = _mapper.Map<List<TDTO>>(entities);
                return dTOs;
            }
            else
                return new List<TDTO>();
        }

        public async Task<TDTO> GetMineById(ClaimsPrincipal user, string id)
        {
            string? userId = getMyUserId(user);
            if (userId is not null)
            {
                TEntity? entity = await _context.Set<TEntity>().Where(e => e.Id ==  id && e.UserId == userId).FirstOrDefaultAsync();
                TDTO? dTO = _mapper.Map<TDTO>(entity);
                return dTO;
            }
            else
                return null!;

        }

        public async Task<TDTO> UpdateMine(ClaimsPrincipal user, TDTO dto)
        {
            string? userId = getMyUserId(user);
            if (userId is not null)
            {
                TEntity? userToUpdate = await _context.Set<TEntity>().Where(e => e.Id == dto.Id && e.UserId == userId).FirstOrDefaultAsync();
                if (userToUpdate is not null)
                {
                    _mapper.Map<TDTO,TEntity >(dto , userToUpdate);
                    _context.Entry(userToUpdate).State = EntityState.Modified;
                    TDTO result = _mapper.Map<TDTO>(userToUpdate);
                    return result;
                }
                else 
                    return null!; 
            }
            else 
                return null!;
        }
        public async Task<bool> DeleteMine(ClaimsPrincipal user, string id)
        {
            string? userId = getMyUserId(user);
            if (userId is not null)
            {
                TEntity? entity = await _context.Set<TEntity>().Where(e => e.Id == id && e.UserId == userId).FirstOrDefaultAsync();
                if (entity is not null)
                {
                    _context.Remove(entity);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public async Task<string> AddMine(ClaimsPrincipal user, TDTO dto)
        {
            string? userId = getMyUserId(user);
            if (userId is not null)
            {
                dto.UserId = userId;
                dto.Id = System.Guid.NewGuid().ToString();
                TEntity entity = _mapper.Map<TEntity>(dto);
                await _context.Set<TEntity>().AddAsync(entity);
                return entity.Id;
            }
            else
            {
                return null!;
            }
        }

        protected async Task<List<TDTO>> GetQuery (ClaimsPrincipal user , Expression <Func<TEntity ,bool>>? expression, List<Expression<Func<TEntity , object>>> includes)
        {
            string? userId = getMyUserId(user);
            if (userId is not null)
            {
                IQueryable<TEntity> query = _context.Set<TEntity>().Where(e => e.UserId == userId);
                if (expression is not null)
                {
                    query = query.Where(expression);
                }
                if (includes is not null)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }
                List<TEntity> entities = await query.ToListAsync();

                List<TDTO> result = _mapper.Map<List<TDTO>>(entities);
                return result;
            }
            else
            {
                return new List<TDTO>();
            }
        }

    }
}
