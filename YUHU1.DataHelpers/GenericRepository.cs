using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace YUHU1.DataHelpers
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private DbContext _ctx;
        private DbSet<T> _set;
        public GenericRepository(DbContext dbContext)
        {
            _ctx = dbContext;
            _set = _ctx.Set<T>();
        }

        //public DataResult<T> Get(Expression<Func<T, bool>> filter)
        //{
        //    bool success = true;
        //    List<string> errors = new List<string>();
        //    T entity = default(T);
        //    try
        //    {
        //        entity = _set.AsNoTracking().FirstOrDefault(filter);
        //    }
        //    catch (Exception ex)
        //    {
        //        success = false;
        //        errors.Add(ex.Message);
        //    }
        //    return new DataResult<T>(entity, new StatusResult(success, errors));
        //}

        public DataResult<T> Get(
            Expression<Func<T, bool>> filter,
            string includetable = null)
        {
            bool success = true;
            List<string> errors = new List<string>();
            IQueryable<T> directResult = _set;
            try
            {
                if (filter != null)
                {
                    directResult = directResult.Where(filter);
                }
                else
                {
                    directResult = _set;
                }

                if (includetable != null)
                {
                    string[] tableList = includetable.Split(',');
                    foreach (string table in tableList)
                    {
                        directResult = directResult.Include(table);
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
                errors.Add(ex.Message);
            }
            return new DataResult<T>(directResult.FirstOrDefault(), new StatusResult(success, errors));
        }

        public DataResult<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> order = null,
            string includetable = null)
        {
            bool success = true;
            List<string> errors = new List<string>();
            IQueryable<T> directResult = _set;
            try
            {
                if (filter != null)
                {
                    directResult = directResult.Where(filter);
                }
                else
                {
                    directResult = _set;
                }

                if (includetable != null)
                {
                    string[] tableList = includetable.Split(',');
                    foreach (string table in tableList)
                    {
                        directResult = directResult.Include(table);
                    }
                }

                if (order != null)
                {
                    directResult = order(directResult);
                }
            }
            catch (Exception ex)
            {
                success = false;
                errors.Add(ex.Message);
            }
            return new DataResult<IEnumerable<T>>(directResult.ToList(), new StatusResult(success, errors));
        }
        public DataResult<IEnumerable<T>> GetAllByPaging(
                int skip,
                int take,
                Func<IQueryable<T>, IOrderedQueryable<T>> order,
                Expression<Func<T, bool>> filter = null)
        {
            bool success = true;
            List<string> errors = new List<string>();
            IQueryable<T> directResult = null;
            List<T> result = null;
            try
            {

                if (filter != null)
                {
                    directResult = _set.Where(filter);
                }
                else
                {
                    directResult = _set;
                }

                if (order != null)
                {
                    directResult = order(directResult);
                }
                directResult = directResult.Skip(skip).Take(take);
                result = directResult.ToList();

            }
            catch (Exception ex)
            {
                success = false;
                errors.Add(ex.Message);
            }
            return new DataResult<IEnumerable<T>>(result, new StatusResult(success, errors));
        }

        public DataResult<IEnumerable<T>> GetAllByPaging(
                int skip,
                int take,
                out long totalRecords,
                Func<IQueryable<T>, IOrderedQueryable<T>> order,
                Expression<Func<T, bool>> filter = null)
        {
            bool success = true;
            List<string> errors = new List<string>();
            IQueryable<T> directResult = null;
            List<T> result = null;
            totalRecords = 0;
            try
            {
                totalRecords = _set.LongCount();
                if (filter != null)
                {
                    directResult = _set.Where(filter);
                }
                else
                {
                    directResult = _set;
                }

                if (order != null)
                {
                    directResult = order(directResult);
                }
                directResult = directResult.Skip(skip).Take(take);
                result = directResult.ToList();

            }
            catch (Exception ex)
            {
                success = false;
                errors.Add(ex.Message);
            }
            return new DataResult<IEnumerable<T>>(result, new StatusResult(success, errors));
        }

        public DataResult<int> Insert(params T[] items)
        {
            bool success = true;
            List<string> errors = new List<string>();
            int count = 0;
            try
            {
                count = items.Count();
                if (items != null && count > 0)
                {
                    _set.AddRange(items);
                }
                else
                {
                    success = false;
                    errors.Add("Item cannot be null or empty");
                }
            }
            catch (Exception ex)
            {
                success = false;
                errors.Add(ex.Message);
            }
            return new DataResult<int>(count, new StatusResult(success, errors));

        }
        public StatusResult Update(T item)
        {
            bool success = true;
            List<string> errors = new List<string>();
            try
            {
                if (item != null)
                {
                    if (!_set.Local.Contains(item))
                        _set.Attach(item);
                    _ctx.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    success = false;
                    errors.Add("Item cannot be null");
                }
            }
            catch (Exception ex)
            {
                success = false;
                errors.Add(ex.Message);
            }
            return new StatusResult(success, errors);

        }

        public DataResult<int> Delete(params T[] items)
        {
            bool success = true;
            List<string> errors = new List<string>();
            int count = 0;
            try
            {
                count = items.Count();
                if (items != null && count > 0)
                {
                    _set.RemoveRange(items);
                }
                else
                {
                    success = false;
                    errors.Add("Item cannot be null/empty");
                }
            }
            catch (Exception ex)
            {
                success = false;
                errors.Add(ex.Message);
            }
            return new DataResult<int>(count, new StatusResult(success, errors));
        }
        public DataResult<int> Delete(Expression<Func<T, bool>> filter)
        {
            bool success = true;
            List<string> errors = new List<string>();
            int count = 0;
            try
            {
                var data = _set.Where(filter);
                count = data.Count();
                _set.RemoveRange(data);
            }
            catch (Exception ex)
            {
                success = false;
                errors.Add(ex.Message);
            }
            return new DataResult<int>(count, new StatusResult(success, errors));
        }
        public DataResult<int> ExecuteSqlCommand(string query, params object[] sqlParameters)
        {
            bool success = true;
            List<string> errors = new List<string>();
            int result = 0;
            try
            {
                if (string.IsNullOrEmpty(query))
                {
                    success = false;
                    errors.Add("Query cannot be empty");
                }
                else
                {
                    if (sqlParameters != null && sqlParameters.Count() > 0)
                    {
                        result = _ctx.Database.ExecuteSqlCommand(query, sqlParameters);
                    }
                    else
                    {
                        result = _ctx.Database.ExecuteSqlCommand(query);
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
                errors.Add(ex.Message);
            }
            return new DataResult<int>(result, new StatusResult(success, errors));
        }
        public DataResult<bool> Exists(Expression<Func<T, bool>> filter)
        {
            bool success = true;
            List<string> errors = new List<string>();
            bool exists = false;
            try
            {
                exists = _set.Any(filter);
            }
            catch (Exception ex)
            {
                exists = false;
                success = false;
                errors.Add(ex.Message);
            }
            return new DataResult<bool>(exists, new StatusResult(success, errors));
        }
        public DataResult<IEnumerable<T>> SqlQuery(string query, params object[] sqlParameters)
        {
            bool success = true;
            List<string> errors = new List<string>();
            IEnumerable<T> result = null;
            try
            {
                if (string.IsNullOrEmpty(query))
                {
                    success = false;
                    errors.Add("Query cannot be empty");
                }
                else
                {
                    if (sqlParameters != null && sqlParameters.Count() > 0)
                    {
                        result = _ctx.Database.SqlQuery<T>(query, sqlParameters);
                    }
                    else
                    {
                        result = _ctx.Database.SqlQuery<T>(query);
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
                errors.Add(ex.Message);
            }
            return new DataResult<IEnumerable<T>>(result, new StatusResult(success, errors));
        }

        public Task<DataResult<IEnumerable<T>>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> order = null)
        {
            bool success = true;
            List<string> errors = new List<string>();
            IQueryable<T> result = null;
            try
            {
                if (filter != null)
                {

                    result = _set.Where(filter);
                }
                else
                {
                    result = _set;
                }
                if (order != null)
                {
                    result = order(result);
                }
            }
            catch (Exception ex)
            {
                success = false;
                errors.Add(ex.Message);
            }
            return Task.FromResult(new DataResult<IEnumerable<T>>(result, new StatusResult(success, errors)));
        }

        public Task<DataResult<IEnumerable<T>>> GetAllByPagingAsync(int skip, int take, Func<IQueryable<T>, IOrderedQueryable<T>> order, Expression<Func<T, bool>> filter = null)
        {
            bool success = true;
            List<string> errors = new List<string>();
            IQueryable<T> result = null;
            try
            {
                if (filter != null)
                {
                    result = _set.Where(filter);
                }
                else
                {
                    result = _set;
                }

                if (order != null)
                {
                    result = order(result);
                }
                result = result.Skip(skip).Take(take);
            }
            catch (Exception ex)
            {
                success = false;
                errors.Add(ex.Message);
            }
            return Task.FromResult(new DataResult<IEnumerable<T>>(result, new StatusResult(success, errors)));
        }

        public async Task<DataResult<T>> GetAsync(Expression<Func<T, bool>> filter)
        {
            bool success = true;
            List<string> errors = new List<string>();
            T entity = default(T);
            try
            {
                entity = await _set.FirstOrDefaultAsync(filter);
            }
            catch (Exception ex)
            {
                success = false;
                errors.Add(ex.Message);
            }
            return new DataResult<T>(entity, new StatusResult(success, errors));
        }

        public Task<DataResult<int>> InsertAsync(params T[] items)
        {
            bool success = true;
            List<string> errors = new List<string>();
            int count = 0;
            try
            {
                count = items.Count();
                if (items != null && count > 0)
                {
                    _set.AddRange(items);
                }
                else
                {
                    success = false;
                    errors.Add("Item cannot be null or empty");
                }
            }
            catch (Exception ex)
            {
                success = false;
                errors.Add(ex.Message);
            }
            return Task.FromResult(new DataResult<int>(count, new StatusResult(success, errors)));

        }

        public Task<StatusResult> UpdateAsync(T item)
        {
            bool success = true;
            List<string> errors = new List<string>();
            try
            {
                if (item != null)
                {
                    if (!_set.Local.Contains(item))
                        _set.Attach(item);
                    _ctx.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    success = false;
                    errors.Add("Item cannot be null");
                }
            }
            catch (Exception ex)
            {
                success = false;
                errors.Add(ex.Message);
            }
            return Task.FromResult(new StatusResult(success, errors));
        }

        public Task<DataResult<int>> DeleteAsync(params T[] items)
        {
            bool success = true;
            List<string> errors = new List<string>();
            int count = 0;
            try
            {
                count = items.Count();
                if (items != null && count > 0)
                {
                    foreach (var item in items)
                    {
                        if (!_set.Local.Contains(item))
                        {
                            _set.Attach(item);
                        }
                        _ctx.Entry<T>(item).State = EntityState.Deleted;
                    }
                }
                else
                {
                    success = false;
                    errors.Add("Item cannot be null/empty");
                }
            }
            catch (Exception ex)
            {
                success = false;
                errors.Add(ex.Message);
            }
            return Task.FromResult(new DataResult<int>(count, new StatusResult(success, errors)));
        }

        public Task<DataResult<int>> DeleteAsync(Expression<Func<T, bool>> filter)
        {
            bool success = true;
            List<string> errors = new List<string>();
            int count = 0;
            try
            {
                var data = _set.Where(filter);
                count = data.Count();
                _set.RemoveRange(data);
            }
            catch (Exception ex)
            {
                success = false;
                errors.Add(ex.Message);
            }
            return Task.FromResult(new DataResult<int>(count, new StatusResult(success, errors)));
        }

        public Task<DataResult<bool>> ExistsAsync(Expression<Func<T, bool>> filter)
        {
            bool success = true;
            List<string> errors = new List<string>();
            bool exists = false;
            try
            {
                exists = _set.Any(filter);
            }
            catch (Exception ex)
            {
                exists = false;
                success = false;
                errors.Add(ex.Message);
            }
            return Task.FromResult(new DataResult<bool>(exists, new StatusResult(success, errors)));
        }

        public Task<DataResult<int>> ExecuteSqlCommandAsync(string query, params object[] sqlParameters)
        {
            bool success = true;
            List<string> errors = new List<string>();
            int result = 0;
            try
            {
                if (string.IsNullOrEmpty(query))
                {
                    success = false;
                    errors.Add("Query cannot be empty");
                }
                else
                {
                    if (sqlParameters != null && sqlParameters.Count() > 0)
                    {
                        result = _ctx.Database.ExecuteSqlCommand(query, sqlParameters);
                    }
                    else
                    {
                        result = _ctx.Database.ExecuteSqlCommand(query);
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
                errors.Add(ex.Message);
            }
            return Task.FromResult(new DataResult<int>(result, new StatusResult(success, errors)));
        }

        public Task<DataResult<IEnumerable<T>>> SqlQueryAsync(string query, params object[] sqlParameters)
        {
            bool success = true;
            List<string> errors = new List<string>();
            IEnumerable<T> result = null;
            try
            {
                if (string.IsNullOrEmpty(query))
                {
                    success = false;
                    errors.Add("Query cannot be empty");
                }
                else
                {
                    if (sqlParameters != null && sqlParameters.Count() > 0)
                    {
                        result = _ctx.Database.SqlQuery<T>(query, sqlParameters);
                    }
                    else
                    {
                        result = _ctx.Database.SqlQuery<T>(query);
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
                errors.Add(ex.Message);
            }
            return Task.FromResult(new DataResult<IEnumerable<T>>(result, new StatusResult(success, errors)));

        }
    }
}
