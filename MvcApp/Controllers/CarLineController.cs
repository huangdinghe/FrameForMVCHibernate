using System.Collections.Generic;
using System.Web.Mvc;
using NHibernate.Criterion;
using com.fxm.MVCHibernate.Domain;
using com.fxm.MVCHibernate.Service;
using com.fxm.MVCHibernate.Core;

namespace MvcApp.Controllers
{
    public class CarLineController : UcController
    {
        #region 列表
        public ActionResult Index(int pageIndex = 1)
        {
            int count = 0;//记录数
            IList<ICriterion> listQuery = new List<ICriterion>();
            IList<Order> listOrder = new List<Order>() { new Order("ID", true) };
            IList<CarLine> list = Container.Instance.Resolve<IServiceCarLine>().Qry(listQuery, listOrder, pageIndex, PagerHelper.PageSize, out count);
            //转换为PageList集合，用于分页控件显示不同的页码
            PageList<CarLine> pageList = list.ToPageList<CarLine>(pageIndex, PagerHelper.PageSize, count);
            //用pageList集合填充页面
            return View(pageList);
        }
        #endregion

        #region 明细
        public ActionResult Details(int id)
        {
            //根据id获取实体
            CarLine entity = Container.Instance.Resolve<IServiceCarLine>().GetEntity(id);

            return View(entity);
        }
        #endregion

        #region 新增
        public ActionResult Create()
        {
            CarLine entity = new CarLine();
            
            InitItems(entity);

            return View(entity);
        }
        [HttpPost]
        public ActionResult Create(CarLine entity)
        {
            try
            {
                Container.Instance.Resolve<IServiceCarLine>().Add(entity);

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
            Container.Instance.Resolve<IServiceCarLine>().Del(id);
            return RedirectToAction("Index");
        }
        #endregion

        #region 修改
        public ActionResult Edit(int id)
        {
            CarLine entity = Container.Instance.Resolve<IServiceCarLine>().GetEntity(id);

            InitItems(entity);
            return View(entity);
        }
        [HttpPost]
        public ActionResult Edit(CarLine entity)
        {
            try
            {
                Container.Instance.Resolve<IServiceCarLine>().Upt(entity);
                return RedirectToAction("Index");
            }
            catch
            {
                InitItems(entity);
                return View(entity);
            }
        }
        #endregion

        private void InitItems(CarLine entity)
        {
            //班车下拉列表
            var carItems = new List<SelectListItem>();
            IList<Car> allCar = Container.Instance.Resolve<IServiceCar>().GetAll();
            foreach (var m in allCar)
            {
                var item = new SelectListItem
                {
                    Text = m.Name,
                    Value = m.ID.ToString(),
                    Selected = (entity.Car != null && entity.Car.ID == m.ID)
                };
                carItems.Add(item);
            }
            ViewBag.carItems = carItems;
            //班别
            ViewBag.shiftItems = new List<SelectListItem>() { 
                new SelectListItem { Text = "早班", Value = "1",Selected=(entity.Shift == 1) },
                new SelectListItem { Text = "晚班", Value = "2", Selected = (entity.Shift == 2) }
            };
            ViewBag.monday1Items = new List<SelectListItem>() { 
                new SelectListItem { Text = "是", Value = "1",Selected=(entity.Monday1 == 1) },
                new SelectListItem { Text = "否", Value = "0", Selected = (entity.Monday1 == 0) }
            };
            ViewBag.tuesday1Items = new List<SelectListItem>() { 
                new SelectListItem { Text = "是", Value = "1",Selected=(entity.Tuesday1 == 1) },
                new SelectListItem { Text = "否", Value = "0", Selected = (entity.Tuesday1 == 0) }
            };
            ViewBag.wednesday1Items = new List<SelectListItem>() { 
                new SelectListItem { Text = "是", Value = "1",Selected=(entity.Wednesday1 == 1) },
                new SelectListItem { Text = "否", Value = "0", Selected = (entity.Wednesday1 == 0) }
            };
            ViewBag.thursday1Items = new List<SelectListItem>() { 
                new SelectListItem { Text = "是", Value = "1",Selected=(entity.Thursday1 == 1) },
                new SelectListItem { Text = "否", Value = "0", Selected = (entity.Thursday1 == 0) }
            };
            ViewBag.friday1Items = new List<SelectListItem>() { 
                new SelectListItem { Text = "是", Value = "1",Selected=(entity.Friday1 == 1) },
                new SelectListItem { Text = "否", Value = "0", Selected = (entity.Friday1 == 0) }
            };
            ViewBag.saturday1Items = new List<SelectListItem>() { 
                new SelectListItem { Text = "是", Value = "1",Selected=(entity.Saturday1 == 1) },
                new SelectListItem { Text = "否", Value = "0", Selected = (entity.Saturday1 == 0) }
            };
            ViewBag.sunday1Items = new List<SelectListItem>() { 
                new SelectListItem { Text = "是", Value = "1",Selected=(entity.Sunday1 == 1) },
                new SelectListItem { Text = "否", Value = "0", Selected = (entity.Sunday1 == 0) }
            };

            ViewBag.monday2Items = new List<SelectListItem>() { 
                new SelectListItem { Text = "是", Value = "1",Selected=(entity.Monday2 == 1) },
                new SelectListItem { Text = "否", Value = "0", Selected = (entity.Monday2 == 0) }
            };
            ViewBag.tuesday2Items = new List<SelectListItem>() { 
                new SelectListItem { Text = "是", Value = "1",Selected=(entity.Tuesday2 == 1) },
                new SelectListItem { Text = "否", Value = "0", Selected = (entity.Tuesday2 == 0) }
            };
            ViewBag.wednesday2Items = new List<SelectListItem>() { 
                new SelectListItem { Text = "是", Value = "1",Selected=(entity.Wednesday2 == 1) },
                new SelectListItem { Text = "否", Value = "0", Selected = (entity.Wednesday2 == 0) }
            };
            ViewBag.thursday2Items = new List<SelectListItem>() { 
                new SelectListItem { Text = "是", Value = "1",Selected=(entity.Thursday2 == 1) },
                new SelectListItem { Text = "否", Value = "0", Selected = (entity.Thursday2 == 0) }
            };
            ViewBag.friday2Items = new List<SelectListItem>() { 
                new SelectListItem { Text = "是", Value = "1",Selected=(entity.Friday2 == 1) },
                new SelectListItem { Text = "否", Value = "0", Selected = (entity.Friday2 == 0) }
            };
            ViewBag.saturday2Items = new List<SelectListItem>() { 
                new SelectListItem { Text = "是", Value = "1",Selected=(entity.Saturday2 == 1) },
                new SelectListItem { Text = "否", Value = "0", Selected = (entity.Saturday2 == 0) }
            };
            ViewBag.sunday2Items = new List<SelectListItem>() { 
                new SelectListItem { Text = "是", Value = "1",Selected=(entity.Sunday2 == 1) },
                new SelectListItem { Text = "否", Value = "0", Selected = (entity.Sunday2 == 0) }
            };
        }
    }
}
