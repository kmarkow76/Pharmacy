using Microsoft.EntityFrameworkCore;
using Pharmacy.DAL.Interfaces;
using Pharmacy.Domain.ModelsDb;

namespace Pharmacy.DAL.Storage;

public class CartStorage: IBaseStorage<CartDb>
{
    public readonly ApplicationDbContext _context;

    public CartStorage(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Add(CartDb cart)
    {
        await _context.AddAsync(cart);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(CartDb cart)
    {
        _context.Remove(cart);
        await _context.SaveChangesAsync();
    }

    public async Task<CartDb> Get(Guid id)
    {
        return await _context.CartDb.FirstOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<CartDb> GetAll()
    {
        return _context.CartDb;
    }

    public async Task<CartDb> Update(CartDb cart)
    {
        _context.CartDb.Update(cart);
        await _context.SaveChangesAsync();
        return cart;
    }
    
}