using com.fxm.MVCHibernate.Domain;
using com.fxm.MVCHibernate.Manager;
using com.fxm.MVCHibernate.Service;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;

namespace com.fxm.MVCHibernate.Component
{
    public class BaseComponent<T, M> :
        IBaseService<T>
        where T : BaseEntity<T>, new()
        where M : BaseManager<T>, new()
    {
        protected M manager = (M)typeof(M)
            .GetConstructor(Type.EmptyTypes)
            .Invoke(null);

        #region 增
        /// <summary>
        /// 新建实体
        /// </summary>
        public void Add(T entity)
        {
            manager.Add(entity);
        }
        #endregion

        #region 删
        /// <summary>
        /// 删除实体
        /// </summary>
        public void Del(T entity)
        {
            manager.Del(entity);
        }
        /// <summary>
        /// 根据主键删除实体
        /// </summary>
        public void Del(int ID)
        {
            manager.Del(ID);
        }
        /// <summary>
        /// 删除全部
        /// </summary>
        public void DelAll()
        {
            manager.DelAll();
        }
        #endregion

        #region 改
        /// <summary>
        /// 更新实体
        /// </summary>
        public void Upt(T entity)
        {
            manager.Upt(entity);
        }
        #endregion

        #region 查
        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <returns>ID</returns>
        public T GetEntity(int ID)
        {
            return manager.GetEntity(ID);
        }
        /// <summary>
        /// 获取所有实体
        /// </summary>
        public IList<T> GetAll()
        {
            return manager.GetAll();
        }
        /// <summary>
        /// 根据查询条件查询满足条件的实体
        /// </summary>
        public IList<T> Qry(IList<ICriterion> queryConditions)
        {
            return manager.Qry(queryConditions);
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
            return manager.Qry(queryConditions, orderList, pageIndex, pageSize, out count);
        }
        #endregion
















        //#region 增
        ///// <summary>
        ///// 新建实体
        ///// </summary>
        //public virtual void Add(T entity)
        //{
        //    manager.Add(entity);
        //}
        //#endregion

        //#region 删
        ///// <summary>
        ///// 删除实体
        ///// </summary>
        //public void Del(T t)
        //{
        //    manager.Del(t);
        //}

        ///// <summary>
        ///// 根据主键删除实体
        ///// </summary>
        //public void Del(int ID)
        //{
        //    manager.Del(ID);
        //}
        ///// <summary>
        ///// 删除全部
        ///// </summary>
        //public void DelAll()
        //{
        //    manager.DelAll();
        //}
        //#endregion

        //#region 改
        ///// <summary>
        ///// 更新实体
        ///// </summary>
        //public void Upt(T t)
        //{
        //    manager.Upt(t);
        //}
        //#endregion

        //#region 查
        ///// <summary>
        ///// 根据主键获取实体
        ///// </summary>
        //public T Get(int ID)
        //{
        //    return manager.Get(ID);
        //}
        ///// <summary>
        ///// 获取所有实体
        ///// </summary>
        //public IList<T> GetAll()
        //{
        //    return manager.GetAll();
        //}
        ///// <summary>
        ///// 根据查询条件查询满足条件的实体
        ///// </summary>
        //public IList<T> Qry(IList<ICriterion> queryConditions)
        //{
        //    return manager.Qry(queryConditions);
        //}
        ///// <summary>
        ///// 分页获取满足条件的实体
        ///// </summary>
        ///// <param name="queryConditions">查询条件</param>
        ///// <param name="orderList">排序属性列表</param>
        ///// <param name="pageIndex">页码,从1开始</param>
        ///// <param name="pageSize">每页实体数</param>
        ///// <param name="count">满足条件的实体总数</param>
        ///// <returns></returns>
        //public IList<T> Qry(IList<ICriterion> queryConditions, IList<Order> orderList, int pageIndex, int pageSize, out int count)
        //{
        //    return manager.Qry(queryConditions, orderList, pageIndex, pageSize, out count);
        //}
        //#endregion
    }
}
