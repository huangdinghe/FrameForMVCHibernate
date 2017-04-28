using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using com.fxm.MVCHibernate.Core;
using com.fxm.MVCHibernate.Domain;
using com.fxm.MVCHibernate.Service;
using NHibernate.Criterion;

namespace MvcApp.Controllers
{
    public class OrderSeatRecController : UcController
    {
        #region 乘客订座
        #region 班车列表
        public ActionResult CarListForOrder()
        {
            IList<ICriterion> qryWhere = new List<ICriterion>() { Expression.Eq("Status", 0) };
            IList<Car> carList = Container.Instance.Resolve<IServiceCar>().Qry(qryWhere);

            IList<CarLine> carLineList = Container.Instance.Resolve<IServiceCarLine>().GetAll();
            IList<CarLineStation> carLineStationList = Container.Instance.Resolve<IServiceCarLineStation>().GetAll();
            ViewBag.carLineList = carLineList;
            ViewBag.carLineStationList = carLineStationList;
            if (AppHelper.IsMobileBrowser)
            {
                return View("CarListForOrderMobile", carList);
            }
            else
            {
                return View("CarListForOrder", carList);
            }
        }
        #endregion

        #region 指定班车的线路列表
        public ActionResult LineListForOrder(int carId)
        {
            IList<ICriterion> qryWhere = new List<ICriterion>() { Expression.Eq("Car.ID", carId) };
            IList<CarLine> lineList = Container.Instance.Resolve<IServiceCarLine>().Qry(qryWhere);

            if (AppHelper.IsMobileBrowser)
            {
                return View("LineListForOrderMobile", lineList);
            }
            else
            {
                return View("LineListForOrder", lineList);
            }
        }
        #endregion

        #region 指定日期与线路的订座
        public ActionResult OrderSeat(DateTime date, int lineId, string msg = "")
        {
            CarLine entity = Container.Instance.Resolve<IServiceCarLine>().GetEntity(lineId);
            //已订座信息
            ViewBag.hisOrderSeatRec = Container.Instance.Resolve<IServiceOrderSeatRec>().Qry(new List<ICriterion>() { 
                Expression.Eq("CarDate", date),
                Expression.Eq("CarLine.ID", lineId)
            });
            ViewBag.carDate = date;
            ViewBag.msg = msg;

            if (AppHelper.IsMobileBrowser)
            {
                return View("OrderSeatMobile", entity);
            }
            else
            {
                return View("OrderSeat", entity);
            }
        }
        #endregion

        #region 退订
        public ActionResult UnOrder(int id, int carId, DateTime carDate, DateTime StartTime)
        {
            TimeSpan d = StartTime.Subtract(DateTime.Now);
            int mins = d.Hours * 60 + d.Minutes;
            if (mins > 10)
            {
                Container.Instance.Resolve<IServiceOrderSeatRec>().Del(id);
                return RedirectToAction("OrderSeat", new { date = carDate, lineId = id, msg = "退订成功！" });
            }
            else if (mins <= 0)
            {
                return RedirectToAction("OrderSeat", new { date = carDate, lineId = id, msg = "已发车，不能退订！" });
            }
            else
            {
                return RedirectToAction("OrderSeat", new { date = carDate, lineId = id, msg = "离发车时间不足10分钟，不能退订！" });
            }
        }
        #endregion

        #region 订座
        public ActionResult Order(int id, int seatNumber, int carId, DateTime carDate, string carLineStartTime, int shift)
        {
            IList<OrderSeatRec> hisOrderSeatRec = Container.Instance.Resolve<IServiceOrderSeatRec>().Qry(new List<ICriterion>() { Expression.Eq("OrderPerson", AppHelper.LoginedUser), Expression.Eq("CarDate", carDate), Expression.Eq("Shift", shift) });
            if (hisOrderSeatRec == null || hisOrderSeatRec.Count == 0)
            {
                //发车时间
                DateTime startTime = Convert.ToDateTime(carDate.ToString("yyyy-MM-dd") + " " + carLineStartTime);
                //订座时间
                DateTime orderTime = DateTime.Now;
                TimeSpan d = startTime.Subtract(orderTime);
                int mins = d.Hours * 60 + d.Minutes;
                if (mins > 1440)
                {
                    return RedirectToAction("OrderSeat", new { date = carDate, lineId = id, msg = "离发车时间超过24小时，不能预订！" });
                }
                else if (mins <= 0)
                {
                    return RedirectToAction("OrderSeat", new { date = carDate, lineId = id, msg = "已发车，不能预订！" });
                }
                else
                {
                    Car urCar = Container.Instance.Resolve<IServiceCar>().GetEntity(carId);
                    OrderSeatRec entity = new OrderSeatRec()
                    {
                        Car = urCar,
                        CarLine = Container.Instance.Resolve<IServiceCarLine>().GetEntity(id),
                        DriverName = urCar.Driver.Name,
                        DriverTel = urCar.Driver.Tel,
                        CarDate = carDate,
                        Shift = shift,
                        OrderPerson = AppHelper.LoginedUser,
                        OrderTime = orderTime,
                        StartTime = startTime,
                        SeatNumber = seatNumber
                    };
                    Container.Instance.Resolve<IServiceOrderSeatRec>().Add(entity);
                    return RedirectToAction("OrderSeat", new { date = carDate, lineId = id, msg="" });
                }
            }
            else
            {
                return RedirectToAction("OrderSeat", new { date = carDate, lineId = id,msg = "您今天已经订过座了，不能再预订！您可以退订后重新订座" });
            }
        }
        #endregion

        #region 个人订座记录
        public ActionResult QryPersonal(int pageIndex = 1)
        {
            int count = 0;//记录数
            IList<ICriterion> listQuery = new List<ICriterion>() { Expression.Eq("OrderPerson", AppHelper.LoginedUser) };
            IList<Order> listOrder = new List<Order>() { new Order("ID", false) };
            IList<OrderSeatRec> list = Container.Instance.Resolve<IServiceOrderSeatRec>().Qry(listQuery, listOrder, pageIndex, PagerHelper.PageSize, out count);
            //转换为PageList集合，用于分页控件显示不同的页码
            PageList<OrderSeatRec> pageList = list.ToPageList<OrderSeatRec>(pageIndex, PagerHelper.PageSize, count);
            //用pageList集合填充页面
            if (AppHelper.IsMobileBrowser)
            {
                return View("QryPersonalMobile", pageList);
            }
            else
            {
                return View("QryPersonal", pageList);
            }
        }
        #endregion
        #endregion

        #region 司机行车记录
        #region 司机个人行车任务
        public ActionResult OrderSeatDriver(int pageIndex = 1, string msg = "")
        {
            IList<ICriterion> listQuery = new List<ICriterion>();
            IList<CarLine> list = Container.Instance.Resolve<IServiceCarLine>().Qry(listQuery).Where(m=>m.Car.Driver.ID==AppHelper.LoginedUser.ID).ToList();
            //已订座记录
            List<int> carIdArr = new List<int>();
            foreach (var item in list)
            {
                carIdArr.Add(item.Car.ID);
            }
            ViewBag.hisOrderSeatRec = Container.Instance.Resolve<IServiceOrderSeatRec>().Qry(new List<ICriterion>() { 
                Expression.Or(Expression.Eq("CarDate", DateTime.Today),Expression.Eq("CarDate", DateTime.Today.AddDays(1)))
                ,Expression.In("Car.ID",carIdArr.ToArray())
            });
            if (AppHelper.IsMobileBrowser)
            {
                return View("OrderSeatDriverMobile", list);
            }
            else
            {
                return View("OrderSeatDriver", list);
            }

        }
        #endregion

        #region 司机个人行车记录
        public ActionResult QryPersonalDriver(int pageIndex = 1)
        {
            IList<ICriterion> listQuery = new List<ICriterion>();
            IList<CarLine> list = Container.Instance.Resolve<IServiceCarLine>().Qry(listQuery).Where(m => m.Car.Driver.ID == AppHelper.LoginedUser.ID).ToList();
            //已订座记录
            List<int> carIdArr = new List<int>();
            foreach (var item in list)
            {
                carIdArr.Add(item.Car.ID);
            }
            ViewBag.hisOrderSeatRec = Container.Instance.Resolve<IServiceOrderSeatRec>().Qry(new List<ICriterion>() { 
                Expression.In("Car.ID",carIdArr.ToArray())
            });
            if (AppHelper.IsMobileBrowser)
            {
                return View("QryPersonalDriverMobile", list);
            }
            else
            {
                return View("QryPersonalDriver", list);
            }
        }
        #endregion
        #endregion

        #region 订座记录管理
        #region 全部订座记录
        public ActionResult Index(int pageIndex = 1, string JobNumber = "", string CarId="", string StartDate = "", string EndDate = "")
        {
            int count = 0;//记录数
            IList<ICriterion> listQuery = new List<ICriterion>();
            if (JobNumber.Length > 0)
            {
                listQuery.Add(Expression.Eq("OrderPerson.LoginCode", JobNumber));
            }
            if (CarId.Length > 0 && CarId != "0")
            {
                listQuery.Add(Expression.Eq("Car.ID", CarId));
            }
            if (StartDate.Length > 0)
            {
                listQuery.Add(Expression.Ge("StartTime", Convert.ToDateTime(StartDate)));
            }
            if (EndDate.Length > 0)
            {
                listQuery.Add(Expression.Lt("StartTime", Convert.ToDateTime(EndDate)));
            }
            //班车下拉列表
            var carItems = new List<SelectListItem>();
            var option = new SelectListItem
            {
                Text = "(全部)",
                Value = "0",
            };
            carItems.Add(option);
            IList<Car> allCar = Container.Instance.Resolve<IServiceCar>().GetAll();
            foreach (var m in allCar)
            {
                var item = new SelectListItem
                {
                    Text = m.Name,
                    Value = m.ID.ToString(),
                };
                carItems.Add(item);
            }
            ViewBag.carItems = carItems;
            IList<Order> listOrder = new List<Order>() { new Order("ID", false) };
            IList<OrderSeatRec> list = Container.Instance.Resolve<IServiceOrderSeatRec>().Qry(listQuery, listOrder, pageIndex, PagerHelper.PageSize, out count);
            //转换为PageList集合，用于分页控件显示不同的页码
            PageList<OrderSeatRec> pageList = list.ToPageList<OrderSeatRec>(pageIndex, PagerHelper.PageSize, count);
            //用pageList集合填充页面
            return View(pageList);
        }
        #endregion

        #region 删除订座记录
        public ActionResult Delete(int id)
        {
            Container.Instance.Resolve<IServiceCarLineStation>().Del(id);
            return RedirectToAction("Index");
        }
        #endregion
        #endregion

        #region 乘客可订座列表--废弃
        public ActionResult OrderSeat_old(int pageIndex = 1, string msg = "")
        {
            ViewBag.msg = msg;
            //班车日期 20点后订次日
            DateTime carDate = DateTime.Now.Hour < 20 ? DateTime.Today : DateTime.Today.AddDays(1);
            //该日已订座记录
            ViewBag.hisOrderSeatRec = Container.Instance.Resolve<IServiceOrderSeatRec>().Qry(new List<ICriterion>() { 
                Expression.Eq("CarDate", carDate)
            });
            ViewBag.carDate = carDate;
            //待订列表
            int count = 0;//记录数
            IList<ICriterion> listQuery = new List<ICriterion>() { Expression.Eq(GetFieldNameByDay(carDate), 1) };
            IList<Order> listOrder = new List<Order>() { new Order("Car.ID", true), new Order("ID", true) };
            IList<CarLine> list = Container.Instance.Resolve<IServiceCarLine>().Qry(listQuery, listOrder, pageIndex, PagerHelper.PageSize, out count);
            //转换为PageList集合，用于分页控件显示不同的页码
            PageList<CarLine> pageList = list.ToPageList<CarLine>(pageIndex, PagerHelper.PageSize, count);
            //用pageList集合填充页面
            if (AppHelper.IsMobileBrowser)
            {
                return View("OrderSeatMobile", pageList);
            }
            else
            {
                return View("OrderSeat", pageList);
            }
        }
        #endregion

        #region 根据日期确定师傅发车的字段名
        private string GetFieldNameByDay(DateTime datet)
        {
            string[] weeks = new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            string name = weeks[Convert.ToInt16(datet.DayOfWeek)];

            //大休日
            DateTime bigResetDate = Convert.ToDateTime(System.Configuration.ConfigurationManager.AppSettings["BigResetDate"]);
            bool isOdd = Convert.ToBoolean((datet.Subtract(bigResetDate).Days / 7) % 2);
            string index = isOdd ? "1" : "2";

            return name + index;
        }
        #endregion
    }
}
