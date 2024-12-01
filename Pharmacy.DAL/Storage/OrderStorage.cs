using Microsoft.EntityFrameworkCore;
using Pharmacy.DAL.Interfaces;
using Pharmacy.Domain.ModelsDb;

namespace Pharmacy.DAL.Storage;

public class OrderStorage: IBaseStorage<OrderDb>
{
    public readonly ApplicationDbContext _context;

    public OrderStorage(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Add(OrderDb order)
    {
        await _context.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(OrderDb order)
    {
        _context.Remove(order);
        await _context.SaveChangesAsync();
    }

    public async Task<OrderDb> Get(Guid id)
    {
        return await _context.OrderDb.FirstOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<OrderDb> GetAll()
    {
        return _context.OrderDb;
    }

    public async Task<OrderDb> Update(OrderDb order)
    {
        _context.OrderDb.Update(order);
        await _context.SaveChangesAsync();
        return order;
    }
}