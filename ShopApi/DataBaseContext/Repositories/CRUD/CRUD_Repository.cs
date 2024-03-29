﻿namespace ShopApi.DataBaseContext.Repositories.CRUD;

public class CRUD_Repository<T> : BaseRepository, ICRUD_REpository<T> where T : BaseEntity
{
    protected CRUD_Repository(DataBaseContext context) : base(context)
    {
        
    }
    
    public virtual async Task<IEnumerable<T>> GetAll()
    {
        var result = Context.Set<T>().ToListAsync();

        return await result;
    }

    public virtual async Task<T> GetById(Guid id)
    {
        var result = await Context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        if (result is null) throw new NullReferenceException();

        return result;
    }

    public virtual async Task<T> Add(T? entity)
    {
        if (entity is null) throw new NullReferenceException();
        var result = Context.AddAsync<T>(entity);
        await Context.SaveChangesAsync();

        return result.Result.Entity;
    }

    public virtual async Task<T> Update(T entity)
    {
        var result = Context.Update(entity).Entity;
        await Context.SaveChangesAsync();

        return result;
    }

    public virtual async Task<T> Remove(Guid id)
    {
        var entity = await Context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        var result = Context.Set<T>().Remove(entity ?? throw new InvalidOperationException());
        await Context.SaveChangesAsync();

        return result.Entity;
    }
}