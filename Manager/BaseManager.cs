using Castle.ActiveRecord;
using NHibernate.Criterion;
using System.Collections.Generic;
using System.Linq;

namespace com.fxm.MVCHibernate.Manager
{
    public class BaseManager<T> 
        : ActiveRecordBase<T>
        where T : class
    {
        #region 增
        /// <summary>
        /// 新建实体
        /// </summary>
        public  void Add(T entity) 
        {
            ActiveRecordBase.Create(entity);
        }
        #endregion

        #region 删
        /// <summary>
        /// 删除实体
        /// </summary>
        public  void Del(T entity) 
        {
            ActiveRecordBase.Delete(entity);
        }
        /// <summary>
        /// 根据主键删除实体
        /// </summary>
        public  void Del(int ID) 
        {
            //1.根据Id得到实体
            T entity = GetEntity(ID);
            //2.按实体删除
            if (entity != null)
            {
                Del(entity);
            }
        }
        /// <summary>
        /// 删除全部
        /// </summary>
        public  void DelAll() 
        {
            ActiveRecordBase.DeleteAll(typeof(T));
        }
        #endregion

        #region 改
        /// <summary>
        /// 更新实体
        /// </summary>
        public  void Upt(T entity) 
        {
            ActiveRecordBase.Update(entity);
        }
        #endregion

        #region 查
        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <returns>ID</returns>
        public  T GetEntity(int ID)
        {
            return ActiveRecordBase.FindByPrimaryKey(typeof(T), ID) as T;
        }
        /// <summary>
        /// 获取所有实体
        /// </summary>
        public  IList<T> GetAll()
        {
            return ActiveRecordBase.FindAll(typeof(T)) as IList<T>;
        }
        /// <summary>
        /// 根据查询条件查询满足条件的实体
        /// </summary>
        public IList<T> Qry(IList<ICriterion> queryConditions) 
        {
            return ActiveRecordBase.FindAll(
                typeof(T)
                , queryConditions.ToArray()
                ) as IList<T>;
        }
        /// <summary>
        /// 分页获取满足条件的实体
        /// </summary>
        /// <param name="queryConditions">查询条件</param>
        /// <param name="orderList">排序属性列表</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页实体数</param>
        /// <param name="count">总实体数</param>
        /// <returns></returns>
        public IList<T> Qry(IList<ICriterion> queryConditions, IList<Order> orderList, int pageIndex, int pageSize, out int count)
        {
            if (queryConditions == null)
            {
                queryConditions = new List<ICriterion>();
            }
            //根据查询条件获取满足条件的对象总数
            count = ActiveRecordBase.Count(
                typeof(T)
                , queryConditions.ToArray());

            if (orderList == null)
            {
                orderList = new List<Order>();
            }
            //根据查询条件分页获取对象集合
            return ActiveRecordBase.SlicedFindAll(
                typeof(T)
                , (pageIndex - 1) * pageSize
                , pageSize
                , orderList.ToArray()
                , queryConditions.ToArray()
                ) as IList<T>;
        }
        #endregion
    }
}