using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using com.fxm.MVCHibernate.Domain;
using com.fxm.MVCHibernate.Service;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;

namespace MvcApp
{
    public partial class FrmInitDB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           this.btnCreate.Click += new EventHandler(DelegatebtnCreate_Click);
        }
        private void DelegatebtnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ActiveRecordStarter.IsInitialized)
                {
                    //如果ActiveRecordStarter框架没有初始化
                    IConfigurationSource source = System.Configuration.ConfigurationManager.GetSection("activerecord") as IConfigurationSource;
                    ActiveRecordStarter.Initialize(typeof(SysUser).Assembly, source);
                }
                Response.Write("正在创建数据库...<br />");
                ActiveRecordStarter.CreateSchema();
                Response.Write("创建数据库完成<br />");
                Response.Write("正在初始化数据...<br />");

                InitSysMenu();
                Response.Write("----系统菜单 初始化完毕！<br />");

                InitSysRole();
                Response.Write("----系统角色 初始化完毕！<br />");

                InitSysUser();
                Response.Write("----系统用户 初始化完毕！<br />");

                InitCar();
                Response.Write("----班车 初始化完毕！<br />");

                InitCarLine();
                Response.Write("----班车线路 初始化完毕！<br />");

                InitCarLineStation();
                Response.Write("----班车停靠站 初始化完毕！<br />");

                Response.Write("初始化数据完成<br />");
            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.Message);
            }

        }

        #region 初始化系统菜单
        /// <summary>
        /// 初始化 系统菜单
        /// </summary>
        private void InitSysMenu()
        {
            #region 管理员 菜单
            //id=1
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().Add(new SysMenu()
            {
                Name = "系统管理",
                SortCode = 10
            });
            //id=2
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().Add(new SysMenu()
            {
                Name = "菜单管理",
                ControllerName = "SysMenu",
                ActionName = "Index",
                LoadClassName = "MvcApp.Controllers.SysMenuController",
                ParentMenu = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().GetEntity(1),
                SortCode = 10
            });
            //id=3
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().Add(new SysMenu()
            {
                Name = "用户管理",
                ControllerName = "SysUser",
                ActionName = "Index",
                LoadClassName = "MvcApp.Controllers.SysUserController",
                ParentMenu = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().GetEntity(1),
                SortCode = 20
            });
            //id=4
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().Add(new SysMenu()
            {
                Name = "角色管理",
                ControllerName = "SysRole",
                ActionName = "Index",
                LoadClassName = "MvcApp.Controllers.SysRoleController",
                ParentMenu = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().GetEntity(1),
                SortCode = 30
            });
            //id=5
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().Add(new SysMenu()
            {
                Name = "更改密码",
                ControllerName = "SysUser",
                ActionName = "ChangePassword",
                LoadClassName = "MvcApp.Controllers.SysUserController",
                ParentMenu = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().GetEntity(1),
                SortCode = 40
            });
            //id=6
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().Add(new SysMenu()
            {
                Name = "业务管理",
                SortCode = 20
            });
            //id=7
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().Add(new SysMenu()
            {
                Name = "班车管理",
                ControllerName = "Car",
                ActionName = "Index",
                LoadClassName = "MvcApp.Controllers.CarController",
                ParentMenu = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().GetEntity(6),
                SortCode = 10
            });
            //id=8
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().Add(new SysMenu()
            {
                Name = "线路管理",
                ControllerName = "CarLine",
                ActionName = "Index",
                LoadClassName = "MvcApp.Controllers.CarLineController",
                ParentMenu = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().GetEntity(6),
                SortCode = 20
            });
            //id=9
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().Add(new SysMenu()
            {
                Name = "停靠站管理",
                ControllerName = "CarLineStation",
                ActionName = "Index",
                LoadClassName = "MvcApp.Controllers.CarLineStationController",
                ParentMenu = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().GetEntity(6),
                SortCode = 30
            });
            //id=10
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().Add(new SysMenu()
            {
                Name = "订座记录管理",
                ControllerName = "OrderSeatRec",
                ActionName = "Index",
                LoadClassName = "MvcApp.Controllers.OrderSeatRecController",
                ParentMenu = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().GetEntity(6),
                SortCode = 40
            });
            #endregion

            #region 乘客 菜单
            //id=11
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().Add(new SysMenu()
            {
                Name = "功能菜单",
                SortCode = 10
            });
            //id=12
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().Add(new SysMenu()
            {
                Name = "订座",
                ControllerName = "OrderSeatRec",
                ActionName = "CarListForOrder",
                LoadClassName = "MvcApp.Controllers.OrderSeatRecController",
                ParentMenu = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().GetEntity(11),
                SortCode = 10
            });
            //id=13
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().Add(new SysMenu()
            {
                Name = "我的乘车记录",
                ControllerName = "OrderSeatRec",
                ActionName = "QryPersonal",
                LoadClassName = "MvcApp.Controllers.OrderSeatRecController",
                ParentMenu = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().GetEntity(11),
                SortCode = 20
            });
            //id=14
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().Add(new SysMenu()
            {
                Name = "更改密码",
                ControllerName = "SysUser",
                ActionName = "ChangePassword",
                LoadClassName = "MvcApp.Controllers.SysUserController",
                ParentMenu = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().GetEntity(11),
                SortCode = 30
            });
            #endregion

            #region 司机 菜单
            //id=15
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().Add(new SysMenu()
            {
                Name = "功能菜单",
                SortCode = 10
            });
            //id=16
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().Add(new SysMenu()
            {
                Name = "我的行车任务",
                ControllerName = "OrderSeatRec",
                ActionName = "OrderSeatDriver",
                LoadClassName = "MvcApp.Controllers.OrderSeatRecController",
                ParentMenu = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().GetEntity(15),
                SortCode = 10
            });
            //id=17
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().Add(new SysMenu()
            {
                Name = "我的行车记录",
                ControllerName = "OrderSeatRec",
                ActionName = "QryPersonalDriver",
                LoadClassName = "MvcApp.Controllers.OrderSeatRecController",
                ParentMenu = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().GetEntity(15),
                SortCode = 10
            });
            #endregion
        }
        #endregion

        #region 初始化角色
        /// <summary>
        /// 初始化 角色
        /// </summary>
        private void InitSysRole()
        {
            //id=1
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysRole>().Add(new SysRole()
            {
                Name = "系统管理员",
                SysMenuList = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().Qry(new List<ICriterion>() {Expression.Le("ID",10) })
            });
            //id=2
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysRole>().Add(new SysRole()
            {
                Name = "乘客",
                SysMenuList = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().Qry(new List<ICriterion>() { Expression.Gt("ID", 10), Expression.Le("ID", 14) })
            });
            //id=3
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysRole>().Add(new SysRole()
            {
                Name = "司机",
                SysMenuList = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysMenu>().Qry(new List<ICriterion>() { Expression.Gt("ID", 14) })
            });
        }
        #endregion

        #region 初始化用户
        private void InitSysUser()
        {
            //id=1
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysUser>().Add(new SysUser()
            {
                LoginCode = "admin",
                Password = AppHelper.EncodeMd5("123"),
                Status = 0,
                Name = "系统管理员",
                SysRoleList = new List<SysRole>() { com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysRole>().GetEntity(1) }
            });
            //id=2
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysUser>().Add(new SysUser()
            {
                LoginCode = "HK00001",
                Password = AppHelper.EncodeMd5("123"),
                Status = 0,
                Name = "王俊太",
                SysRoleList = new List<SysRole>() { com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysRole>().GetEntity(2) }
            });
            //id=3
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysUser>().Add(new SysUser()
            {
                LoginCode = "HK00002",
                Password = AppHelper.EncodeMd5("123"),
                Status = 0,
                Name = "董世芳",
                SysRoleList = new List<SysRole>() { com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysRole>().GetEntity(2) }
            });
            //id=4
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysUser>().Add(new SysUser()
            {
                LoginCode = "13594233686",
                Password = AppHelper.EncodeMd5("123"),
                Status = 0,
                Name = "甘师傅",
                Tel = "13594233686",
                SysRoleList = new List<SysRole>() { com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysRole>().GetEntity(3) }
            });
            //id=5
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysUser>().Add(new SysUser()
            {
                LoginCode = "13594386527",
                Password = AppHelper.EncodeMd5("123"),
                Status = 0,
                Name = "张师傅",
                Tel = "13594386527",
                SysRoleList = new List<SysRole>() { com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysRole>().GetEntity(3) }
            });
            //id=6
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysUser>().Add(new SysUser()
            {
                LoginCode = "13340353316",
                Password = AppHelper.EncodeMd5("123"),
                Status = 0,
                Name = "汤师傅",
                Tel = "13340353316",
                SysRoleList = new List<SysRole>() { com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysRole>().GetEntity(3) }
            });
            //id=7
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysUser>().Add(new SysUser()
            {
                LoginCode = "13509420036",
                Password = AppHelper.EncodeMd5("123"),
                Status = 0,
                Name = "秦师傅",
                Tel = "13509420036",
                SysRoleList = new List<SysRole>() { com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysRole>().GetEntity(3) }
            });
        }
        #endregion

        #region 初始化班车
        private void InitCar()
        {
            //id=1
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCar>().Add(new Car()
            {
                Name = "1号交通车",
                PlateNumber = "渝A 78622",
                SeatCount = 53,
                Driver=com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysUser>().GetEntity(4),
                Status = 0
            });
            //id=2
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCar>().Add(new Car()
            {
                Name = "2号交通车",
                PlateNumber = "渝A 25531",
                SeatCount = 49,
                Driver = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysUser>().GetEntity(5),
                Status = 0
            });
            //id=3
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCar>().Add(new Car()
            {
                Name = "3号交通车",
                PlateNumber = "渝A 68092",
                SeatCount = 49,
                Driver = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysUser>().GetEntity(6),
                Status = 0
            });
            //id=4
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCar>().Add(new Car()
            {
                Name = "4号交通车",
                PlateNumber = "渝B P5192",
                SeatCount = 46,
                Driver = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysUser>().GetEntity(7),
                Status = 0
            });
        }
        #endregion

        #region 初始化班车线路
        private void InitCarLine()
        {
            #region 1号车线路1
            //id=1
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLine>().Add(new CarLine()
            {
                Name = "路线1",
                Car = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCar>().GetEntity(1),
                Remark = "周一至周末上班(学堂湾-新厂区)",
                CarLineStartTime="07:20",
                Shift=1,
                Monday1 = 1,
                Monday2 = 1,
                Tuesday1 = 1,
                Tuesday2 = 1,
                Wednesday1 = 1,
                Wednesday2 = 1,
                Thursday1 = 1,
                Thursday2 = 1,
                Friday1 = 1,
                Friday2 = 1,
                Saturday1 = 0,
                Saturday2 = 1,
                Sunday1 = 0,
                Sunday2 = 0,
            });
            #endregion

            #region 1号车线路2
            //id=2
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLine>().Add(new CarLine()
            {
                Name = "路线2",
                Car = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCar>().GetEntity(1),
                Remark = "周一至周末下班(新厂区-学堂湾)",
                CarLineStartTime = "18:00",
                Shift = 2,
                Monday1 = 1,
                Monday2 = 1,
                Tuesday1 = 1,
                Tuesday2 = 1,
                Wednesday1 = 1,
                Wednesday2 = 1,
                Thursday1 = 1,
                Thursday2 = 1,
                Friday1 = 1,
                Friday2 = 1,
                Saturday1 = 0,
                Saturday2 = 1,
                Sunday1 = 0,
                Sunday2 = 0,
            });
            #endregion

            #region 1号车线路3
            //id=3
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLine>().Add(new CarLine()
            {
                Name = "路线3",
                Car = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCar>().GetEntity(1),
                Remark = "大休周六至工地上班(公租房-新厂区)",
                CarLineStartTime = "07:30",
                Shift = 1,
                Monday1 = 0,
                Monday2 = 0,
                Tuesday1 = 0,
                Tuesday2 = 0,
                Wednesday1 = 0,
                Wednesday2 = 0,
                Thursday1 = 0,
                Thursday2 = 0,
                Friday1 = 0,
                Friday2 = 0,
                Saturday1 = 1,
                Saturday2 = 0,
                Sunday1 = 0,
                Sunday2 = 0,
            });
            #endregion

            #region 1号车线路4
            //id=4
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLine>().Add(new CarLine()
            {
                Name = "路线4",
                Car = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCar>().GetEntity(1),
                Remark = "大休周六工地下班(新厂区-公租房)",
                CarLineStartTime = "18:00",
                Shift = 2,
                Monday1 = 0,
                Monday2 = 0,
                Tuesday1 = 0,
                Tuesday2 = 0,
                Wednesday1 = 0,
                Wednesday2 = 0,
                Thursday1 = 0,
                Thursday2 = 0,
                Friday1 = 0,
                Friday2 = 0,
                Saturday1 = 1,
                Saturday2 = 0,
                Sunday1 = 0,
                Sunday2 = 0,
            });
            #endregion

            #region 2号车线路1-1
            //id=5
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLine>().Add(new CarLine()
            {
                Name = "路线1-1",
                Car = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCar>().GetEntity(2),
                Remark = "周一至周末上班(公租房-新厂区)",
                CarLineStartTime="07:15",
                Shift = 1,
                Monday1 = 1,
                Monday2 = 1,
                Tuesday1 = 1,
                Tuesday2 = 1,
                Wednesday1 = 1,
                Wednesday2 = 1,
                Thursday1 = 1,
                Thursday2 = 1,
                Friday1 = 1,
                Friday2 = 1,
                Saturday1 = 0,
                Saturday2 = 1,
                Sunday1 = 0,
                Sunday2 = 0,
            });
            #endregion

            #region 2号车线路1-2
            //id=6
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLine>().Add(new CarLine()
            {
                Name = "路线1-2",
                Car = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCar>().GetEntity(2),
                Remark = "周一至周末上班(公租房-新厂区)",
                CarLineStartTime = "07:25",
                Shift = 1,
                Monday1 = 1,
                Monday2 = 1,
                Tuesday1 = 1,
                Tuesday2 = 1,
                Wednesday1 = 1,
                Wednesday2 = 1,
                Thursday1 = 1,
                Thursday2 = 1,
                Friday1 = 1,
                Friday2 = 1,
                Saturday1 = 0,
                Saturday2 = 1,
                Sunday1 = 0,
                Sunday2 = 0,
            });
            #endregion

            #region 2号车线路1-3
            //id=7
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLine>().Add(new CarLine()
            {
                Name = "路线1-3",
                Car = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCar>().GetEntity(2),
                Remark = "周一至周末上班(公租房-新厂区)",
                CarLineStartTime = "07:35",
                Shift = 1,
                Monday1 = 1,
                Monday2 = 1,
                Tuesday1 = 1,
                Tuesday2 = 1,
                Wednesday1 = 1,
                Wednesday2 = 1,
                Thursday1 = 1,
                Thursday2 = 1,
                Friday1 = 1,
                Friday2 = 1,
                Saturday1 = 0,
                Saturday2 = 1,
                Sunday1 = 0,
                Sunday2 = 0,
            });
            #endregion

            #region 2号车线路1-4
            //id=8
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLine>().Add(new CarLine()
            {
                Name = "路线1-4",
                Car = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCar>().GetEntity(2),
                Remark = "周一至周末上班(公租房-新厂区)",
                CarLineStartTime = "07:45",
                Shift = 1,
                Monday1 = 1,
                Monday2 = 1,
                Tuesday1 = 1,
                Tuesday2 = 1,
                Wednesday1 = 1,
                Wednesday2 = 1,
                Thursday1 = 1,
                Thursday2 = 1,
                Friday1 = 1,
                Friday2 = 1,
                Saturday1 = 0,
                Saturday2 = 1,
                Sunday1 = 0,
                Sunday2 = 0,
            });
            #endregion

            #region 2号车线路2
            //id=9
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLine>().Add(new CarLine()
            {
                Name = "路线2",
                Car = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCar>().GetEntity(2),
                Remark = "周一至周末下班(惠科-公租房)",
                CarLineStartTime = "18:00",
                Shift = 2,
                Monday1 = 1,
                Monday2 = 1,
                Tuesday1 = 1,
                Tuesday2 = 1,
                Wednesday1 = 1,
                Wednesday2 = 1,
                Thursday1 = 1,
                Thursday2 = 1,
                Friday1 = 1,
                Friday2 = 1,
                Saturday1 = 0,
                Saturday2 = 1,
                Sunday1 = 0,
                Sunday2 = 0,
            });
            #endregion

            #region 3号车线路1
            //id=10
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLine>().Add(new CarLine()
            {
                Name = "路线1",
                Car = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCar>().GetEntity(3),
                Remark = "周末下班(惠科-观音桥)",
                CarLineStartTime = "17:50",
                Shift = 2,
                Monday1 = 0,
                Monday2 = 0,
                Tuesday1 = 0,
                Tuesday2 = 0,
                Wednesday1 = 0,
                Wednesday2 = 0,
                Thursday1 = 0,
                Thursday2 = 0,
                Friday1 = 0,
                Friday2 = 0,
                Saturday1 = 1,
                Saturday2 = 0,
                Sunday1 = 0,
                Sunday2 = 0,
            });
            #endregion

            #region 4号车线路1
            //id=11
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLine>().Add(new CarLine()
            {
                Name = "路线1",
                Car = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCar>().GetEntity(4),
                Remark = "周一至周末上班(学堂湾-新厂区)",
                CarLineStartTime = "07:10",
                Shift = 1,
                Monday1 = 1,
                Monday2 = 1,
                Tuesday1 = 1,
                Tuesday2 = 1,
                Wednesday1 = 1,
                Wednesday2 = 1,
                Thursday1 = 1,
                Thursday2 = 1,
                Friday1 = 1,
                Friday2 = 1,
                Saturday1 = 0,
                Saturday2 = 1,
                Sunday1 = 0,
                Sunday2 = 0,
            });
            #endregion

            #region 4号车线路2
            //id=12
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLine>().Add(new CarLine()
            {
                Name = "路线2",
                Car = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCar>().GetEntity(4),
                Remark = "周一至周末下班(新厂区-学堂湾)",
                CarLineStartTime = "17:50",
                Shift = 2,
                Monday1 = 1,
                Monday2 = 1,
                Tuesday1 = 1,
                Tuesday2 = 1,
                Wednesday1 = 1,
                Wednesday2 = 1,
                Thursday1 = 1,
                Thursday2 = 1,
                Friday1 = 1,
                Friday2 = 1,
                Saturday1 = 0,
                Saturday2 = 1,
                Sunday1 = 0,
                Sunday2 = 0,
            });
            #endregion

            #region 4号车线路3
            //id=13
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLine>().Add(new CarLine()
            {
                Name = "路线3",
                Car = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCar>().GetEntity(4),
                Remark = "周末外出(大休周六周日、小休周日)(公租房-解放碑)",
                CarLineStartTime = "09:00",
                Shift = 1,
                Monday1 = 0,
                Monday2 = 0,
                Tuesday1 = 0,
                Tuesday2 = 0,
                Wednesday1 = 0,
                Wednesday2 = 0,
                Thursday1 = 0,
                Thursday2 = 0,
                Friday1 = 0,
                Friday2 = 0,
                Saturday1 = 1,
                Saturday2 = 0,
                Sunday1 = 1,
                Sunday2 = 1,
            });
            #endregion
        }
        #endregion

        #region 初始化停靠站
        private void InitCarLineStation()
        {
            CarLine CarLine = new CarLine();
            #region 1号车1号线
            CarLine = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLine>().GetEntity(1);
            //id=1
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "学堂湾轻轨站",
                ArrivalTime = "07:20"
            });
            //id=2
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "大山村轻轨站",
                ArrivalTime = "07:25"
            });
            //id=3
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "界石",
                ArrivalTime = "07:45"
            });
            //id=4
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "惠科",
                ArrivalTime = "07:50"
            });
            //id=5
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "新厂区",
                ArrivalTime = "07:55"
            });
            #endregion

            #region 1号车2号线
            CarLine = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLine>().GetEntity(2);
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "新厂区",
                ArrivalTime = "18:00"
            });
            //id=7
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "公租房",
                ArrivalTime = "18:05"
            });
            //id=8
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "惠科",
                ArrivalTime = "18:10"
            });
            //id=9
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "界石",
                ArrivalTime = "18:15"
            });
            //id=10
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "大山村",
                ArrivalTime = "18:30"
            });
            //id=11
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "学堂湾",
                ArrivalTime = "18:35"
            });
           #endregion

            #region 1号车3号线
            CarLine = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLine>().GetEntity(3);
            //id=12
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "公租房",
                ArrivalTime = "07:30"
            });
            //id=13
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "惠科",
                ArrivalTime = "07:35"
            });
            //id=14
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "新厂区",
                ArrivalTime = "07:40"
            });
            #endregion

            #region 1号车4号线
            CarLine = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLine>().GetEntity(4);
            //id=15
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "新厂区",
                ArrivalTime = "18:00"
            });
            //id=16
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "惠科",
                ArrivalTime = "18:05"
            });
            //id=16
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "公租房",
                ArrivalTime = "18:10"
            });
            #endregion

            #region 2号车1-1号线
            CarLine = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLine>().GetEntity(5);
            //id=17
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "公租房",
                ArrivalTime = "07:15"
            });
            //id=18
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "惠科",
                ArrivalTime = "07:20"
            });
            #endregion

            #region 2号车1-2号线
            CarLine = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLine>().GetEntity(6);
            //id=18
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "公租房",
                ArrivalTime = "07:25"
            });
            //id=19
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "惠科",
                ArrivalTime = "07:30"
            });
            #endregion

            #region 2号车1-3号线
            CarLine = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLine>().GetEntity(7);
            //id=20
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "公租房",
                ArrivalTime = "07:35"
            });
            //id=21
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "惠科",
                ArrivalTime = "07:40"
            });
            #endregion

            #region 2号车1-4号线
            CarLine = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLine>().GetEntity(8);
            //id=22
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "公租房",
                ArrivalTime = "07:45"
            });
            //id=23
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "惠科",
                ArrivalTime = "07:50"
            });
            #endregion

            #region 2号车2号线
            CarLine = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLine>().GetEntity(9);
            //id=23
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "惠科",
                ArrivalTime = "18:00"
            });
            //id=24
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "公租房",
                ArrivalTime = "18:05"
            });
            //id=25
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "惠科",
                ArrivalTime = "18:10"
            });
            //id=26
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "公租房",
                ArrivalTime = "18:15"
            });
            #endregion

            #region 3号车1号线
            CarLine = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLine>().GetEntity(10);
            //id=27
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "惠科",
                ArrivalTime = "17:50"
            });
            //id=28
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "四公里",
                ArrivalTime = "18:30"
            });
            //id=29
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "观音桥",
                ArrivalTime = "18:45"
            });
            #endregion

            #region 4号车1号线
            CarLine = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLine>().GetEntity(11);
            //id=33
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "学堂湾轻轨站",
                ArrivalTime = "07:10"
            });
            //id=34
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "大山村轻轨站",
                ArrivalTime = "07:15"
            });
            //id=35
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "界石",
                ArrivalTime = "07:35"
            });
            //id=36
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "惠科",
                ArrivalTime = "07:40"
            });
            //id=37
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "新厂区",
                ArrivalTime = "07:45"
            });
            #endregion

            #region 4号车2号线
            CarLine = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLine>().GetEntity(12);
            //id=38
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "新厂区",
                ArrivalTime = "17:50"
            });
            //id=39
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "公租房",
                ArrivalTime = "17:55"
            });
            //id=40
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "HK",
                ArrivalTime = "18:00"
            });
            //id=41
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "界石",
                ArrivalTime = "18:05"
            });
            //id=42
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "大山村",
                ArrivalTime = "18:20"
            });
            //id=43
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "学堂湾",
                ArrivalTime = "18:25"
            });
            #endregion

            #region 4号车3号线
            CarLine = com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLine>().GetEntity(13);
            //id=44
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "公租房",
                ArrivalTime = "09:00"
            });
            //id=45
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "巴南万达",
                ArrivalTime = "09:30"
            });
            //id=46
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "南坪万达",
                ArrivalTime = "10:00"
            });
            //id=47
            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceCarLineStation>().Add(new CarLineStation()
            {
                CarLine = CarLine,
                Name = "解放碑",
                ArrivalTime = "10:30"
            });
            #endregion
        }
        #endregion
    }
}