using System.Collections.Generic;
using System.Threading.Tasks;
﻿using CDCC.Data.Common;



namespace CDCC.Data.Repository
{
    public interface IRepository<T> where T : class
    {

        IEnumerable<T> GetAll();
        public PagingResult<T> GetAllPaging(PagingRequest request);
        T Get(int id);
        bool Insert(T entity);
        int InsertExample(T entity);
        bool Update(T entity);
        bool Delete(int id);


        ///////////////////////////////////////////////////
        Task<List<T>> Get_All();
        //Pt get by id
        Task<T> Get_By_Id(int id);

        Task<int> Insert_(T obj);

        Task<bool> Update_(T obj);

        Task<bool> Delete_(T obj);



    }
}
