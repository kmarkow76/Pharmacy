using Microsoft.EntityFrameworkCore;
using Pharmacy.DAL.Interfaces;
using Pharmacy.Domain.ModelsDb;

namespace Pharmacy.DAL.Storage;

public class MedicineStorage : IBaseStorage<MedicineDb>
{
    public readonly ApplicationDbContext _context;

    public MedicineStorage(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Add(MedicineDb medicine)
    {
        await _context.AddAsync(medicine);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(MedicineDb medicine)
    {
        _context.Remove(medicine);
        await _context.SaveChangesAsync();
    }

    public async Task<MedicineDb> Get(Guid id)
    {
        return await _context.MedicineDb.FirstOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<MedicineDb> GetAll()
    {
        return _context.MedicineDb;
    }

    public async Task<MedicineDb> Update(MedicineDb medicine)
    {
        _context.MedicineDb.Update(medicine);
        await _context.SaveChangesAsync();
        return medicine;
    }
}