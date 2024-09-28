using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using DotNetsTask.Data.Models;




namespace DotNetsTask.Repo
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal DotNetsTasksDbContext Context;
        internal DbSet<TEntity> DbSet;
        private readonly IConfiguration configuration;

        public Repository(DotNetsTasksDbContext context, IConfiguration configuration)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
            this.configuration = configuration;
        }

        public async Task<List<TEntity>> GetAllAsync() => await DbSet.ToListAsync();

        public async Task<TEntity> GetAsync(int id) => await DbSet.FindAsync(id);

        public async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            DbSet.Update(entity);
            await Context.SaveChangesAsync();
        }
        public virtual RepositoryQuery<TEntity> Query()
        {
            var repositoryGetFluentHelper =
                new RepositoryQuery<TEntity>(this);

            return repositoryGetFluentHelper;
        }

        public virtual TEntity FindById(object SectionId)
        {
            return DbSet.Find(SectionId);
        }
        public virtual void Insert(TEntity entity)
        {
            try
            {

                //DbSet.Attach(entity);
                //Context.Entry(entity).State = EntityState.Added;
                DbSet.Add(entity);
                Context.SaveChanges();
            }
            catch (Exception ex) //(DbEntityValidationException ex)
            {
                throw ex;
                //StringBuilder sb = new StringBuilder();

                //foreach (var failure in ex.EntityValidationErrors)
                //{
                //    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                //    foreach (var error in failure.ValidationErrors)
                //    {
                //        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                //        sb.AppendLine();
                //    }
                //}

                //throw new DbEntityValidationException("Entity Validation Failed - errors follow:\n" + sb.ToString(), ex);
            }
        }

        public async Task InsertAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            DbSet.Add(entity);
            await Context.SaveChangesAsync();
        }

        public virtual void InsertList(List<TEntity> entities)
        {
            try
            {
                //DbSet.Attach(entity);
                //Context.Entry(entity).State = EntityState.Added;
                foreach (var entity in entities)
                {
                    DbSet.Add(entity);
                }

                Context.SaveChanges();
            }
            catch (Exception ex) //(DbEntityValidationException ex)
            {
                //StringBuilder sb = new StringBuilder();

                //foreach (var failure in ex.EntityValidationErrors)
                //{
                //    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                //    foreach (var error in failure.ValidationErrors)
                //    {
                //        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                //        sb.AppendLine();
                //    }
                //}

                //throw new DbEntityValidationException("Entity Validation Failed - errors follow:\n" + sb.ToString(), ex);
            }
        }



        public virtual void Update(TEntity entity)
        {
            try
            {
                // Detach any existing entity with the same key
                var existingEntity = DbSet.Local.SingleOrDefault(e => Context.Entry(e).Entity == entity);
                if (existingEntity != null)
                {
                    Context.Entry(existingEntity).State = EntityState.Detached;
                }

                // Attach and update the entity
                DbSet.Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;
                Context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Handle and log the exception
                //Log.Error($"Error updating entity of type {typeof(TEntity)}", ex);
                throw;
            }
            catch (Exception ex)
            {
                // Handle and log other exceptions
                //Log.Error($"Error updating entity of type {typeof(TEntity)}", ex);
                throw;
            }
        }




        public virtual void UpdateWithoutAttach(TEntity entity)
        {
            try
            {
                Context.Entry(entity).State = EntityState.Modified;
                Context.SaveChanges();
            }
            catch (Exception ex) //(DbEntityValidationException ex)
            {
                //StringBuilder sb = new StringBuilder();

                //foreach (var failure in ex.EntityValidationErrors)
                //{
                //    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                //    foreach (var error in failure.ValidationErrors)
                //    {
                //        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                //        sb.AppendLine();
                //    }
                //}

                //throw new DbEntityValidationException(
                //    "Entity Validation Failed - errors follow:\n" +
                //    sb.ToString(), ex
                //);
            }
        }
        //public virtual void Delete(object id)
        //{
        //    var entity = DbSet.Find(id);
        //    Delete(entity);
        //}


        public virtual void InsertCollection(List<TEntity> entityCollection)
        {
            entityCollection.ForEach(e =>
            {
                DbSet.Add(e);
            });
            Context.SaveChanges();
        }
        public virtual void Delete(TEntity entity)
        {

            DbSet.Attach(entity);
            DbSet.Remove(entity);
            Context.SaveChanges();
        }
        public async Task Delete(object id)
        {
            try
            {
                var entity = DbSet.Find(id);
                DbSet.Remove(entity);
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public virtual void DeleteCollection(List<TEntity> entityCollection)
        {
            entityCollection.ForEach(e =>
            {
                Context.Entry(e).State = EntityState.Deleted;
            });
            Context.SaveChanges();
        }
        public virtual void Delete(List<TEntity> entity)
        {

            DbSet.RemoveRange(entity);
            Context.SaveChanges();
        }
        public DotNetsTasksDbContext GetContext()
        {
            return Context;
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public string GetOpenConnection()
        {
            return configuration["ConnectionStrings:IYAMConnection"];
           
        }

        public void ChangeEntityCollectionState<T>(ICollection<T> entityCollection, ObjectState state) where T : class
        {
            foreach (T entity in entityCollection.ToList())
            {
                Context.Entry(entity).State = ConvertState(state);
            }
        }
        public void ChangeEntityState(TEntity entity, ObjectState state)
        {

            Context.Entry(entity).State = ConvertState(state);

        }
        public IEnumerable<TEntity> Get<TResult>(Expression<Func<TEntity, bool>> filter = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                              bool trackingEnabled = false
                                            ) where TResult : class
        {
            IQueryable<TEntity> query = DbSet;

            if (include != null)
            {
                query = include(query);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return (trackingEnabled ? query : query.AsNoTracking()).AsEnumerable();
        }

        internal IQueryable<TEntity> Get(
           Expression<Func<TEntity, bool>> filter = null,
           bool trackingEnabled = false,
           Func<IQueryable<TEntity>,
               IOrderedQueryable<TEntity>> orderBy = null,
           List<Expression<Func<TEntity, object>>>
               includeProperties = null,
           int? page = null,
           int? pageSize = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (includeProperties != null)
                includeProperties.ForEach(i => { query = query.Include(i); });

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);

            return (trackingEnabled ? query : query.AsNoTracking());
        }

        //----------------------------------------------------------------        
        //public virtual PagedListResult<TEntity> Search(SearchQuery<TEntity> searchQuery)
        //{
        //    IQueryable<TEntity> sequence = DbSet;

        //    //Applying filters
        //    sequence = ManageFilters(searchQuery, sequence);

        //    //Include Properties
        //    sequence = ManageIncludeProperties(searchQuery, sequence);

        //    //Resolving Sort Criteria
        //    //This code applies the sorting criterias sent as the parameter
        //    sequence = ManageSortCriterias(searchQuery, sequence);

        //    return GetTheResult(searchQuery, sequence);
        //}

        //public virtual PagedListResult<TEntity> Search(SearchQuery<TEntity> searchQuery, out int totalCount)
        //{
        //    IQueryable<TEntity> sequence = DbSet;

        //    //Applying filters
        //    sequence = ManageFilters(searchQuery, sequence);

        //    //Include Properties
        //    sequence = ManageIncludeProperties(searchQuery, sequence);

        //    //Resolving Sort Criteria
        //    //This code applies the sorting criterias sent as the parameter
        //    sequence = ManageSortCriterias(searchQuery, sequence);

        //    return GetTheResult(searchQuery, sequence, out totalCount);
        //}

        ///// <summary>
        ///// Executes the query against the repository (database).
        ///// </summary>
        ///// <param name="searchQuery"></param>
        ///// <param name="sequence"></param>
        ///// <returns></returns>
        //protected virtual PagedListResult<TEntity> GetTheResult(SearchQuery<TEntity> searchQuery, IQueryable<TEntity> sequence)
        //{
        //    //Counting the total number of object.
        //    var resultCount = sequence.Count();

        //    var result = (searchQuery.Take > 0)
        //                        ? (sequence.Skip(searchQuery.Skip).Take(searchQuery.Take).ToList())
        //                        : (sequence.ToList());

        //    //Debug info of what the query looks like
        //    //Console.WriteLine(sequence.ToString());

        //    // Setting up the return object.
        //    bool hasNext = (searchQuery.Skip <= 0 && searchQuery.Take <= 0) ? false : (searchQuery.Skip + searchQuery.Take < resultCount);
        //    return new PagedListResult<TEntity>()
        //    {
        //        Entities = result,
        //        HasNext = hasNext,
        //        HasPrevious = (searchQuery.Skip > 0),
        //        Count = resultCount
        //    };
        //}

        ///// <summary>
        ///// Executes the query against the repository (database).
        ///// </summary>
        ///// <param name="searchQuery"></param>
        ///// <param name="sequence"></param>
        ///// <returns></returns>
        //protected virtual PagedListResult<TEntity> GetTheResult(SearchQuery<TEntity> searchQuery, IQueryable<TEntity> sequence, out int totalCount)
        //{
        //    //Counting the total number of object.
        //    totalCount = sequence.Count();

        //    var result = (searchQuery.Take > 0)
        //                        ? (sequence.Skip(searchQuery.Skip).Take(searchQuery.Take).ToList())
        //                        : (sequence.ToList());

        //    //Debug info of what the query looks like
        //    //Console.WriteLine(sequence.ToString());

        //    // Setting up the return object.
        //    bool hasNext = (searchQuery.Skip <= 0 && searchQuery.Take <= 0) ? false : (searchQuery.Skip + searchQuery.Take < totalCount);
        //    return new PagedListResult<TEntity>()
        //    {
        //        Entities = result,
        //        HasNext = hasNext,
        //        HasPrevious = (searchQuery.Skip > 0),
        //        Count = totalCount
        //    };
        //}

        ///// <summary>
        ///// Resolves and applies the sorting criteria of the SearchQuery
        ///// </summary>
        ///// <param name="searchQuery"></param>
        ///// <param name="sequence"></param>
        ///// <returns></returns>
        //protected virtual IQueryable<TEntity> ManageSortCriterias(SearchQuery<TEntity> searchQuery, IQueryable<TEntity> sequence)
        //{
        //    if (searchQuery.SortCriterias != null && searchQuery.SortCriterias.Count > 0)
        //    {
        //        var sortCriteria = searchQuery.SortCriterias[0];
        //        var orderedSequence = sortCriteria.ApplyOrdering(sequence, false);

        //        if (searchQuery.SortCriterias.Count > 1)
        //        {
        //            for (var i = 1; i < searchQuery.SortCriterias.Count; i++)
        //            {
        //                var sc = searchQuery.SortCriterias[i];
        //                orderedSequence = sc.ApplyOrdering(orderedSequence, true);
        //            }
        //        }
        //        sequence = orderedSequence;
        //    }
        //    else
        //    {
        //        try
        //        {
        //            sequence = sequence.OrderBy(x => (true));// as IOrderedQueryable<TEntity>;
        //            //sequence = ((IOrderedQueryable<TEntity>)sequence).OrderBy(x => (true));
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //    }
        //    return sequence;
        //}

        ///// <summary>
        ///// Chains the where clause to the IQueriable instance.
        ///// </summary>
        ///// <param name="searchQuery"></param>
        ///// <param name="sequence"></param>
        ///// <returns></returns>
        //protected virtual IQueryable<TEntity> ManageFilters(SearchQuery<TEntity> searchQuery, IQueryable<TEntity> sequence)
        //{
        //    if (searchQuery.Filters != null && searchQuery.Filters.Count > 0)
        //    {
        //        foreach (var filterClause in searchQuery.Filters)
        //        {
        //            sequence = sequence.Where(filterClause);
        //        }
        //    }
        //    return sequence;
        //}

        ///// <summary>
        ///// Includes the properties sent as part of the SearchQuery.
        ///// </summary>
        ///// <param name="searchQuery"></param>
        ///// <param name="sequence"></param>
        ///// <returns></returns>
        //protected virtual IQueryable<TEntity> ManageIncludeProperties(SearchQuery<TEntity> searchQuery, IQueryable<TEntity> sequence)
        //{
        //    if (!string.IsNullOrWhiteSpace(searchQuery.IncludeProperties))
        //    {
        //        var properties = searchQuery.IncludeProperties.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

        //        foreach (var includeProperty in properties)
        //        {
        //            sequence = sequence.Include(includeProperty);
        //        }
        //    }
        //    return sequence;
        //}

        private EntityState ConvertState(ObjectState state)
        {
            switch (state)
            {
                case ObjectState.Added:
                    return EntityState.Added;
                case ObjectState.Deleted:
                    return EntityState.Deleted;
                case ObjectState.Modified:
                    return EntityState.Modified;
                default:
                    return EntityState.Unchanged;
            }
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        //public List<T> ExecuteStoredProcedure<T>(string storedProcedureName, SqlParameter[] parameters) where T : class
        //{
        //    if (parameters == null)
        //    {
        //        parameters = Array.Empty<SqlParameter>(); // or use an empty array if null is acceptable
        //    }
        //    var results = Context.Set<T>()
        //        .FromSqlRaw($"EXEC {storedProcedureName} {GetParameterString(parameters)}", parameters)
        //        .ToList();

        //    return results;
        //}

        //private string GetParameterString(SqlParameter[] parameters)
        //{
        //    if (parameters == null || parameters.Length == 0)
        //        return string.Empty;

        //    return string.Join(", ", parameters.Select(p => $"@{p.ParameterName}"));
        //}

    }
}
