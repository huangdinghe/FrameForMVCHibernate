using com.fxm.MVCHibernate.Domain;
using NHibernate.Criterion;
using System.Collections.Generic;

namespace com.fxm.MVCHibernate.Service
{
    public interface IBaseService<T> 
        where T : BaseEntity<T>, new()
    {
        #region 增
        /// <summary>
        /// 新建实体
        /// </summary>
        void Add(T entity);
        #endregion

        #region 删
        /// <summary>
        /// 删除实体
        /// </summary>
        void Del(T entity);
        /// <summary>
        /// 根据主键删除实体
        /// </summary>
        void Del(int ID);
        /// <summary>
        /// 删除全部
        /// </summary>
        void DelAll();
        #endregion

        #region 改
        /// <summary>
        /// 更新实体
        /// </summary>
        void Upt(T entity);
        #endregion

        #region 查
        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <returns>ID</returns>
        T GetEntity(int ID);
        /// <summary>
        /// 获取所有实体
        /// </summary>
        IList<T> GetAll();
        /// <summary>
        /// 根据查询条件查询满足条件的实体
        /// </summary>
        IList<T> Qry(IList<ICriterion> queryConditions);
        /// <summary>
        /// 分页获取满足条件的实体
        /// </summary>
        /// <param name="queryConditions">查询条件</param>
        /// <param name="orderList">排序属性列表</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页实体数</param>
        /// <param name="count">总实体数</param>
        /// <returns></returns>
        IList<T> Qry(IList<ICriterion> queryConditions, IList<Order> orderList, int pageIndex, int pageSize, out int count);
        #endregion
    }
}
