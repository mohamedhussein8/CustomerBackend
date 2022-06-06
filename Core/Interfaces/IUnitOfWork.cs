﻿using Core.Entities;

namespace Core.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IBaseRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;


        Task Commit();
    }
}
