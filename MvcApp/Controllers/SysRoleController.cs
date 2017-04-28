using System.Collections.Generic;
using System.Web.Mvc;
using NHibernate.Criterion;
using com.fxm.MVCHibernate.Domain;
using com.fxm.MVCHibernate.Service;
using com.fxm.MVCHibernate.Core;

namespace MvcApp.Controllers
{
    public class SysRoleController : UcController
    {
        #region 列表
        public ActionResult Index(int pageIndex = 1, string roleName = "")
        {
            int count = 0;//记录数
            IList<ICriterion> listQuery = new List<ICriterion>() { new LikeExpression("Name", roleName, MatchMode.Anywhere) };
            IList<Order> listOrder = new List<Order>() { new Order("ID", true) };
            IList<SysRole> list = Container.Instance.Resolve<IServiceSysRole>().Qry(listQuery, listOrder, pageIndex, PagerHelper.PageSize, out count);
            //转换为PageList集合，用于分页控件显示不同的页码
            PageList<SysRole> pageList = list.ToPageList<SysRole>(pageIndex, PagerHelper.PageSize, count);
            //用pageList集合填充页面
            return View(pageList);
        }
        #endregion

        #region 新增
        public ViewResult Create()
        {
            SysRole role = new SysRole();

            return View(role);
        }
        [HttpPost]
        public ActionResult Create(SysRole entity, string FunctionIDs)
        {
            try
            {
                entity.SysMenuList = Container.Instance.Resolve<IServiceSysMenu>().Qry(new List<ICriterion>() { Expression.In("ID", AppHelper.StrToArray(FunctionIDs)) });

                Container.Instance.Resolve<IServiceSysRole>().Add(entity);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(entity);
            }
        }
        #endregion

        #region 删除
        public ActionResult Delete(int id)
        {
            Container.Instance.Resolve<IServiceSysRole>().Del(id);
            return RedirectToAction("Index");
        }
        #endregion

        #region 修改
        public ActionResult Edit(int id)
        {
            SysRole entity = Container.Instance.Resolve<IServiceSysRole>().GetEntity(id);

            string moduleIds = "";
            foreach (var m in entity.SysMenuList)
            {
                moduleIds += m.ID.ToString() + ",";
            }
            ViewBag.EditRoleModuleIDs = moduleIds;

            string userIds = "";
            foreach (var u in entity.SysUserList)
            {
                userIds += u.ID.ToString() + ",";
            }
            ViewBag.EditRoleUserIDs = userIds;

            return View(entity);
        }
        [HttpPost]
        public ActionResult Edit(SysRole entity, string moduleIDs, string userIDs)
        {
            entity.SysMenuList = Container.Instance.Resolve<IServiceSysMenu>().Qry(new List<ICriterion>() { Expression.In("ID", AppHelper.StrToArray(moduleIDs)) });
            entity.SysUserList = Container.Instance.Resolve<IServiceSysUser>().Qry(new List<ICriterion>() { Expression.In("ID", AppHelper.StrToArray(userIDs)) });
            Container.Instance.Resolve<IServiceSysRole>().Upt(entity);//更新实体

            return RedirectToAction("Index");
        }
        #endregion

        #region 明细
        public ActionResult Details(int id)
        {
            SysRole entity = Container.Instance.Resolve<IServiceSysRole>().GetEntity(id);
            string moduleIds = "";
            foreach (var m in entity.SysMenuList)
            {
                moduleIds += m.ID.ToString() + ",";
            }
            ViewBag.EditRoleModuleIDs = moduleIds;
            string userIds = "";
            foreach (var u in entity.SysUserList)
            {
                userIds += u.ID.ToString() + ",";
            }
            ViewBag.EditRoleUserIDs = userIds;

            return View(entity);
        }
        #endregion
    }
}
