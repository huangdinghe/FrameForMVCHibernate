using System.Collections.Generic;
using System.Web.Mvc;
using NHibernate.Criterion;
using com.fxm.MVCHibernate.Domain;
using com.fxm.MVCHibernate.Service;
using com.fxm.MVCHibernate.Core;

namespace MvcApp.Controllers
{
    public class CarLineStationController : UcController
    {
        #region 列表
        public ActionResult Index(int pageIndex = 1)
        {
            int count = 0;//记录数
            IList<ICriterion> listQuery = new List<ICriterion>();
            IList<Order> listOrder = new List<Order>() { new Order("CarLine", true), new Order("ArrivalTime", true) };
            IList<CarLineStation> list = Container.Instance.Resolve<IServiceCarLineStation>().Qry(listQuery, listOrder, pageIndex, PagerHelper.PageSize, out count);
            //转换为PageList集合，用于分页控件显示不同的页码
            PageList<CarLineStation> pageList = list.ToPageList<CarLineStation>(pageIndex, PagerHelper.PageSize, count);
            //用pageList集合填充页面
            return View(pageList);
        }
        #endregion
        #region 明细
        public ActionResult Details(int id)
        {
            //根据id获取实体
            CarLineStation entity = Container.Instance.Resolve<IServiceCarLineStation>().GetEntity(id);

            return View(entity);
        }
        #endregion
        #region 新增
        public ActionResult Create()
        {
            CarLineStation entity = new CarLineStation();

            InitItems(entity);

            return View(entity);
        }
        [HttpPost]
        public ActionResult Create(CarLineStation entity)
        {
            try
            {
                Container.Instance.Resolve<IServiceCarLineStation>().Add(entity);

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
            Container.Instance.Resolve<IServiceCarLineStation>().Del(id);
            return RedirectToAction("Index");
        }
        #endregion
        #region 修改
        public ActionResult Edit(int id)
        {
            CarLineStation entity = Container.Instance.Resolve<IServiceCarLineStation>().GetEntity(id);

            InitItems(entity);
            return View(entity);
        }
        [HttpPost]
        public ActionResult Edit(CarLineStation entity)
        {
            if (ModelState.IsValid)
            {
                Container.Instance.Resolve<IServiceCarLineStation>().Upt(entity);

                return RedirectToAction("Index");
            }

            InitItems(entity);
            return View(entity);
        }
        #endregion
        private void InitItems(CarLineStation entity)
        {
            //班车下拉列表
            var carLineItems = new List<SelectListItem>();
            IList<CarLine> allCarLine = Container.Instance.Resolve<IServiceCarLine>().GetAll();
            foreach (var m in allCarLine)
            {
                var item = new SelectListItem
                {
                    Text =m.Car.Name+"=="+m.Name+((m.Shift==1)?"[早班]":"[晚班]"),
                    Value = m.ID.ToString(),
                    Selected = (entity.CarLine != null && entity.CarLine.ID == m.ID)
                };
                carLineItems.Add(item);
            }
            ViewBag.carLineItems = carLineItems;
        }
    }
}
