using DotNetsTask.Repo;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace DotNetsTask.Repo
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity FindById(object id);

        void Insert(TEntity entity);

        void InsertList(List<TEntity> entity);

        void InsertCollection(List<TEntity> entityCollection);

        void DeleteCollection(List<TEntity> entityCollection);
        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task Delete(object id);

        // void Delete(object id);
       // PagedListResult<TEntity> Search(SearchQuery<TEntity> searchQuery, out int totalCount);

        Task InsertAsync(TEntity entity);

        Task<List<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(int id);

        Task UpdateAsync(TEntity entity);

        RepositoryQuery<TEntity> Query();

        void ChangeEntityCollectionState<T>(ICollection<T> entityCollection, ObjectState state) where T : class;

        void ChangeEntityState(TEntity entity, ObjectState state);

        void UpdateWithoutAttach(TEntity entity);

        IEnumerable<TEntity> Get<TResult>(Expression<Func<TEntity, bool>> filter = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                             Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                              bool trackingEnabled = false) where TResult : class;

       // collageerpdbContext GetContext();

        string GetOpenConnection();

        void SaveChanges();

        void Delete(List<TEntity> entity);



       // public List<T> ExecuteStoredProcedure<T>(string storedProcedureName, SqlParameter[] parameters) where T : class;
        void Dispose();
       

    }
}