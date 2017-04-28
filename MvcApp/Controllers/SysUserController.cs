using System.Collections.Generic;
using System.Web.Mvc;
using System.Data;
using System.Data.OleDb;
using NHibernate.Criterion;
using com.fxm.MVCHibernate.Domain;
using com.fxm.MVCHibernate.Service;
using com.fxm.MVCHibernate.Core;
using System.Web;

namespace MvcApp.Controllers
{
    public class SysUserController : UcController
    {
        #region 列表
        public ActionResult Index(int pageIndex = 1, string JobNumber = "")
        {
            int count = 0;//记录数
            IList<ICriterion> listQuery = new List<ICriterion>();
            if (JobNumber.Length > 0)
            {
                listQuery.Add(Expression.Eq("LoginCode", JobNumber));
            }
            IList<Order> listOrder = new List<Order>() { new Order("ID", true) };
            IList<SysUser> list = Container.Instance.Resolve<IServiceSysUser>().Qry(listQuery, listOrder, pageIndex, PagerHelper.PageSize, out count);
            //转换为PageList集合，用于分页控件显示不同的页码
            PageList<SysUser> pageList = list.ToPageList<SysUser>(pageIndex, PagerHelper.PageSize, count);
            //用pageList集合填充页面
            return View(pageList);
        }
        #endregion

        #region 明细
        public ActionResult Details(int id)
        {
            //根据id获取实体
            SysUser user = Container.Instance.Resolve<IServiceSysUser>().GetEntity(id);
            //用户所属角色
            ViewBag.cblItems = AppHelper.InitCheckBoxList(Container.Instance.Resolve<IServiceSysRole>().GetAll(), user.SysRoleList);
            return View(user);
        }
        #endregion

        #region 登录
        public ViewResult Login()
        {
            if (AppHelper.IsMobileBrowser)
            {
                return View("LoginMobile");
            }
            else
            {
                return View("Login");
            }
        }
        /// <summary>
        /// 验证登录
        /// </summary>
        /// <param name="account">账户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpPost]
        public string Login(string account, string password = "123")
        {
            //访问数据库，根据用户名和密码获取用户信息
            IList<ICriterion> listQuery = new List<ICriterion>() { 
                Expression.Eq("LoginCode", account), 
                Expression.Eq("Password", AppHelper.EncodeMd5(password)), 
                Expression.Eq("Status", 0) };
            IList<SysUser> lst = Container.Instance.Resolve<IServiceSysUser>().Qry(listQuery);
            if (lst.Count > 0)
            {
                AppHelper.LoginedUser = lst[0];//保存用户登录信息
                if (AppHelper.IsMobileBrowser)
                {
                    if (account.Substring(0, 2).ToLower() == "hk")
                    {
                        return AppHelper.Host + "Home/IndexMobile";
                    }
                    else
                    {
                        return AppHelper.Host + "Home/IndexDriverMobile";
                    }
                }
                else
                {
                    return AppHelper.Host + "Home/Index";
                }
            }
            else
            {
                return "0";
            }
        }
        /// <summary>
        /// 退出登录
        /// </summary>
        public ViewResult ExitLogin()
        {
            Session.Clear();

            return View("Login");
        }
        #endregion

        #region 新增
        public ActionResult Create()
        {
            SysUser entity = new SysUser();
            entity.Status = 0;


            InitItems(entity);
            return View(entity);
        }
        [HttpPost]
        public ActionResult Create(SysUser entity, string roleIDs)
        {
            try
            {
                entity.SysRoleList = Container.Instance.Resolve<IServiceSysRole>().Qry(new List<ICriterion>() { Expression.In("ID", AppHelper.StrToArray(roleIDs)) });
                entity.Password = AppHelper.EncodeMd5(entity.Password);

                Container.Instance.Resolve<IServiceSysUser>().Add(entity);

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
            Container.Instance.Resolve<IServiceSysUser>().Del(id);
            return RedirectToAction("Index");
        }
        #endregion

        #region 修改
        public ActionResult Edit(int id)
        {
            SysUser entity = Container.Instance.Resolve<IServiceSysUser>().GetEntity(id);

            InitItems(entity);
            return View(entity);
        }
        [HttpPost]
        public ActionResult Edit(SysUser entity, string roleIDs)
        {
           try
            {
                entity.SysRoleList = Container.Instance.Resolve<IServiceSysRole>().Qry(new List<ICriterion>() { Expression.In("ID", AppHelper.StrToArray(roleIDs)) });

                Container.Instance.Resolve<IServiceSysUser>().Upt(entity);

                return RedirectToAction("Index");
            }
           catch
           {
               InitItems(entity);
               return View(entity);
           }
        }
        #endregion

        #region 更改用户状态
        public ActionResult SwitchStatus(int id)
        {
            SysUser entity = Container.Instance.Resolve<IServiceSysUser>().GetEntity(id);
            entity.Status = (entity.Status == 0) ? 1 : 0;

            Container.Instance.Resolve<IServiceSysUser>().Upt(entity);//更新当前用户

            return RedirectToAction("Index");
        }
        #endregion

        #region   修改密码
        public ActionResult ChangePassword()
        {
            if (AppHelper.IsMobileBrowser)
            {
                return View("ChangePasswordMobile", AppHelper.LoginedUser);
            }
            else
            {
                return View("ChangePassword", AppHelper.LoginedUser);
            }
        }
        [HttpPost]
        public ActionResult ChangeNewPassword(string password)
        {
            SysUser entity = Container.Instance.Resolve<IServiceSysUser>().GetEntity(AppHelper.LoginedUser.ID);

            entity.Password = AppHelper.EncodeMd5(password);
            Container.Instance.Resolve<IServiceSysUser>().Upt(entity);

            AppHelper.LoginedUser.Password = entity.Password;

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PasswordCheck(string oldPasswod = "")
        {
            return Json(AppHelper.EncodeMd5(oldPasswod) == AppHelper.LoginedUser.Password, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 导入Excel
        [HttpPost]
        public ActionResult ImportExcel()
        {
            JsonResult json = new JsonResult();
            try
            {
                HttpPostedFileBase fb = Request.Files[0];
                string fileName = fb.FileName;  //获取到文件名  xx.xls
                fb.SaveAs(Server.MapPath("~/Content/Model.xls"));

                string file = Server.MapPath("~/Content/Model.xls");
                string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + file + ";" + "Extended Properties=Excel 8.0;";
                OleDbDataAdapter odda = new OleDbDataAdapter("select * from [Sheet1$]", strConn);
                DataSet myds = new DataSet();
                odda.Fill(myds, "ExcelInfo");

                DataTable dt = myds.Tables["ExcelInfo"].DefaultView.ToTable();
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["工号"].ToString().Length > 0)
                    {
                        IList<SysUser> lst = Container.Instance.Resolve<IServiceSysUser>().Qry(new List<ICriterion>() { Expression.Eq("LoginCode", dr["工号"].ToString()) });
                        if (lst.Count > 0)
                        {
                            SysUser entity = Container.Instance.Resolve<IServiceSysUser>().GetEntity(lst[0].ID);
                            entity.LoginCode = dr["工号"].ToString();
                            entity.Password = AppHelper.EncodeMd5(dr["初始密码"].ToString());
                            entity.Status = 0;
                            entity.Name = dr["姓名"].ToString();
                            entity.SysRoleList = new List<SysRole>() { com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysRole>().GetEntity(2) };

                            Container.Instance.Resolve<IServiceSysUser>().Upt(entity);
                        }
                        else
                        {
                            com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysUser>().Add(new SysUser()
                            {
                                LoginCode = dr["工号"].ToString(),
                                Password = AppHelper.EncodeMd5(dr["初始密码"].ToString()),
                                Status = 0,
                                Name = dr["姓名"].ToString(),
                                SysRoleList = new List<SysRole>() { com.fxm.MVCHibernate.Core.Container.Instance.Resolve<IServiceSysRole>().GetEntity(2) }
                            });
                        }
                    }
                }
                json.Data = "导入成功";
            }
            catch (System.Exception ex)
            {

                json.Data = "导入失败!" + ex.Message;
            }
            return View("Index");
        }
        #endregion

        private void InitItems(SysUser entity)
        {
            //角色
            ViewBag.cblItems = AppHelper.InitCheckBoxList(Container.Instance.Resolve<IServiceSysRole>().GetAll(), entity.SysRoleList);
            //状态
            ViewBag.statusItems = new List<SelectListItem>() { 
                new SelectListItem { Text = "正常", Value = "0",Selected=(entity.Status == 0) },
                new SelectListItem { Text = "禁用", Value = "1", Selected = (entity.Status == 1) }
            };
        }
    }
}
