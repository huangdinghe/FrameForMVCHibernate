using System.Collections;
using com.fxm.MVCHibernate.Core;
using com.fxm.MVCHibernate.Domain;
using com.fxm.MVCHibernate.Service;
using NHibernate.Criterion;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace MvcApp.Controllers
{
    public class SysMenuController : UcController
    {
        #region 列表
        public ActionResult Index(int pageIndex = 1)
        {
            int count = 0;
            IList<ICriterion> listQuery = new List<ICriterion>();
            IList<Order> listOrder = new List<Order>() ;
            IList<SysMenu> list = Container.Instance.Resolve<IServiceSysMenu>().Qry(listQuery, listOrder, pageIndex, PagerHelper.PageSize, out count);
            //转换为PageList集合，用于分页控件显示不同的页码
            PageList<SysMenu> pageList = list.ToPageList<SysMenu>(pageIndex, PagerHelper.PageSize, count);
            //用pageList集合填充页面
            return View(pageList);
        }
        #endregion
    }
}
