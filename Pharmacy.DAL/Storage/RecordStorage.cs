using Microsoft.EntityFrameworkCore;
using Pharmacy.DAL.Interfaces;
using Pharmacy.Domain.ModelsDb;

namespace Pharmacy.DAL.Storage;

public class RecordStorage: IBaseStorage<RecordDb>
{
    public readonly ApplicationDbContext _context;

    public RecordStorage(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Add(RecordDb record)
    {
        await _context.AddAsync(record);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(RecordDb record)
    {
        _context.Remove(record);
        await _context.SaveChangesAsync();
    }

    public async Task<RecordDb> Get(Guid id)
    {
        return await _context.RecordDb.FirstOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<RecordDb> GetAll()
    {
        return _context.RecordDb;
    }

    public async Task<RecordDb> Update(RecordDb record)
    {
        _context.RecordDb.Update(record);
        await _context.SaveChangesAsync();
        return record;
    }
}