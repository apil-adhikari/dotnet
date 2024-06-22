﻿using Apil_PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Apil_PMS.Infrastructure.IRepository.ICrudService
{
    public interface ICrudService<TEntity> where TEntity : BaseEntity
    {
        int Insert(TEntity entity);

        Task<int> InsertAsync(TEntity entity);

        int Delete(TEntity entity);

        Task<int> DeleteAsync(TEntity entity);

        int Update(TEntity entity);

        Task<int> UpdateAsync(TEntity entity);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> GetAsync(int? id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
    }
}
