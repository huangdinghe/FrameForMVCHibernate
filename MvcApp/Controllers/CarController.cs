using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NHibernate.Criterion;
using com.fxm.MVCHibernate.Domain;
using com.fxm.MVCHibernate.Service;
using com.fxm.MVCHibernate.Core;

namespace MvcApp.Controllers
{
    public class CarController : UcController
    {
        #region 列表
        public ActionResult Index(int pageIndex = 1)
        {
            int count = 0;//记录数
            IList<ICriterion> listQuery = new List<ICriterion>();
            IList<Order> listOrder = new List<Order>() { new Order("ID", true) };
            IList<Car> list = Container.Instance.Resolve<IServiceCar>().Qry(listQuery, listOrder, pageIndex, PagerHelper.PageSize, out count);
            //转换为PageList集合，用于分页控件显示不同的页码
            PageList<Car> pageList = list.ToPageList<Car>(pageIndex, PagerHelper.PageSize, count);
            //用pageList集合填充页面
            return View(pageList);
        }
        #endregion

        #region 明细
        public ActionResult Details(int id)
        {
            //根据id获取实体
            Car entity = Container.Instance.Resolve<IServiceCar>().GetEntity(id);

            return View(entity);
        }
        #endregion

        #region 新增
        public ActionResult Create()
        {
            Car entity = new Car();
            entity.Status = 0;

            InitItems(entity);
            return View(entity);
        }
        [HttpPost]
        public ActionResult Create(Car entity)
        {
            try
            {
                Container.Instance.Resolve<IServiceCar>().Add(entity);

                return RedirectToAction("Index");
            }
            catch
            {
                InitItems(entity);
                return View(entity);
            }
        }
        #endregion

        #region 删除
        public ActionResult Delete(int id)
        {
            Container.Instance.Resolve<IServiceCar>().Del(id);
            return RedirectToAction("Index");
        }
        #endregion

        #region 修改
        public ActionResult Edit(int id)
        {
            Car entity = Container.Instance.Resolve<IServiceCar>().GetEntity(id);

            InitItems(entity);

            return View(entity);
        }
        [HttpPost]
        public ActionResult Edit(Car entity)
        {
            try
            {
                Container.Instance.Resolve<IServiceCar>().Upt(entity);

                return RedirectToAction("Index");
            }
            catch
            {
                InitItems(entity);
                return View(entity);
            }
        }
        #endregion

        #region 更改状态
        public ActionResult SwitchStatus(int id)
        {
            Car entity = Container.Instance.Resolve<IServiceCar>().GetEntity(id);
            entity.Status = (entity.Status == 0) ? 1 : 0;

            Container.Instance.Resolve<IServiceCar>().Upt(entity);

            return RedirectToAction("Index");
        }
        #endregion

        private void InitItems(Car entity)
        {
            //班车下拉列表
            var driverItems = new List<SelectListItem>();
            IList<SysUser> allDriver = Container.Instance.Resolve<IServiceSysUser>().GetAll();
            foreach (var m in allDriver)
            {
                foreach (var r in m.SysRoleList)
                {
                    if (r.ID == 3)
                    {
                        var item = new SelectListItem
                        {
                            Text = m.Name,
                            Value = m.ID.ToString(),
                            Selected = (entity.Driver != null && entity.Driver.ID == m.ID)
                        };
                        driverItems.Add(item);
                    }
                }
            }
            ViewBag.driverItems = driverItems;
            //状态
            ViewBag.statusItems = new List<SelectListItem>() { 
                new SelectListItem { Text = "正常", Value = "0",Selected=(entity.Status == 0) },
                new SelectListItem { Text = "停运", Value = "1", Selected = (entity.Status == 1) }
            };
        }
    }
}
