using Microsoft.EntityFrameworkCore;
using Pharmacy.DAL.Interfaces;
using Pharmacy.Domain.ModelsDb;

namespace Pharmacy.DAL.Storage;

public class CategoryStorage : IBaseStorage<CategoryDb>
{
    public readonly ApplicationDbContext _context;

    public CategoryStorage(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Add(CategoryDb category)
    {
        await _context.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(CategoryDb category)
    {
        _context.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task<CategoryDb> Get(Guid id)
    {
        return await _context.CategoryDb.FirstOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<CategoryDb> GetAll()
    {
        return _context.CategoryDb;
    }

    public async Task<CategoryDb> Update(CategoryDb category)
    {
        _context.CategoryDb.Update(category);
        await _context.SaveChangesAsync();
        return category;
    }
    
    
}