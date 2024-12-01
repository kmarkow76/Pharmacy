using Microsoft.EntityFrameworkCore;
using Pharmacy.DAL.Interfaces;
using Pharmacy.Domain.ModelsDb;

namespace Pharmacy.DAL.Storage;

public class UserStorage : IBaseStorage<UserDb>
{
    public readonly ApplicationDbContext _context;

    public UserStorage(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Add(UserDb user) 
    {
        await _context.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(UserDb user)
    {
        _context.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<UserDb> Get(Guid id)
    {
        return await _context.UserDb.FirstOrDefaultAsync(x=>x.Id == id);
    }

    public IQueryable<UserDb> GetAll()
    {
        return _context.UserDb;
    }

    public async Task<UserDb> Update(UserDb user)
    {
        _context.UserDb.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }
}