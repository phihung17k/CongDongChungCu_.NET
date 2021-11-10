using CDCC.Data.Common;
using CDCC.Data.Models.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;

namespace CDCC.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly CongDongChungCuContext context;


        private DbSet<T> entities;


        public Repository(CongDongChungCuContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                IEnumerable<T> result = entities.AsEnumerable();
                return result;
            } catch (SqlException)
            {
                throw;
            }
        }


        public PagingResult<T> GetAllPaging(PagingRequest request)
        {
            try
            {
                List<T> items = entities.Skip((request.currentPage - 1) * request.pageSize).Take(request.pageSize).ToList();
                if (items.Count > 0)
                {
                    return new PagingResult<T>(items, entities.Count(), request.currentPage, request.pageSize);
                }
                else throw new NullReferenceException("Not found");
            }
            catch (SqlException)
            {
                throw;
            }
        }
        public T Get(int id)
        {
            try
            {
                T entity = entities.Find(id);
                return entity == null ? throw new NullReferenceException("Not found the given identity") : entity;
            } catch (SqlException)
            {
                throw;
            }
        }

        public bool Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity is null");
            }
            entities.Add(entity);
            return context.SaveChanges() > 0;
        }
        public int InsertExample(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("Entity is null");
                }
                entities.Add(entity);
                bool result = context.SaveChanges() > 0;
                if (result)
                {
                    var idProperty = entity.GetType().GetProperty("Id").GetValue(entity, null);
                    return (int)idProperty;
                }
            } catch (SqlException)
            {
                throw;
            }
            return 0;
        }

        public bool Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("Entity is null");
                }
                return context.SaveChanges() > 0;
            } catch (SqlException)
            {
                throw;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                T entity = entities.Find(id);
                if (entity == null) throw new NullReferenceException("Not found the given identity");
                entities.Remove(entity);
                return context.SaveChanges() > 0;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        //////////////////////////////////       Phương Thức Async _ Await       //////////////////////////////////

        //GET ALL
        public async Task<List<T>> Get_All()
        {
            var listEntity = await entities.ToListAsync();
            return listEntity;
        }

        //GET BY ID
        public async Task<T> Get_By_Id(int id)
        {
            //await ở đây có nghĩa là trong PT bất đồng bộ phải 
            T query = await entities.FindAsync(id);
            return query;
        }


        //INSERT PRODUCT
        public async Task<int> Insert_(T obj)
        {
            Boolean check = false;
            if (obj == null)
            {
                throw new ArgumentNullException("entity");
            }
            //
            await entities.AddAsync(obj);

            check = await context.SaveChangesAsync() > 0;

            //
            context.Entry<T>(obj).Reload();
            //

            if (check)
            {
               
                var idProperty =  obj.GetType().GetProperty("Id").GetValue(obj, null);
                return (int)idProperty;
            }
            //
            return 0;
        }


        //UPDATE
        //ở đây muốn update thì phải chỉ nhờ hàm Get để EF theo dõi và set State cho entity đó
        //EF core sẽ theo dõi Entity đó nếu có thay đổi dữ liệu thì sẽ set State cho entity đó là Modified SaveChangesAsync == lệnh [Update]
        //UPDATE PRODUCT
        public async Task<bool> Update_(T obj)
        {
            Boolean check = false;
            //
            if (obj == null)
            {
                throw new ArgumentNullException("entity");
            }
            //
            check = await context.SaveChangesAsync() > 0;
            //
            return check;
        }


        //DELETE
        public async Task<bool> Delete_(T obj)
        {
            Boolean check = false;

            if (obj == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Remove(obj);

            check = await context.SaveChangesAsync() > 0;

            return check;
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return entities.Where(expression);

        }


    }
}
