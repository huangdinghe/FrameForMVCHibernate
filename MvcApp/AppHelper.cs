using com.fxm.MVCHibernate.Domain;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace MvcApp
{
    public class AppHelper
    {
        /// <summary>
        /// 使用MD5加密字符串
        /// </summary>
        public static string EncodeMd5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(str));

            return System.Text.Encoding.Default.GetString(result);
        }
        /// <summary>
        /// 将ID字符串转化为数组
        /// </summary>
        public static int[] StrToArray(string str)
        {
            List<int> arr = new List<int>();
            string[] arrStr = str.Split(',');
            for (int i = 0; i < arrStr.Length; i++)
            {
                try
                {
                    arr.Add(int.Parse(arrStr[i]));
                }
                catch
                {
                }
            }
            return arr.ToArray();
        }
        #region 用户所属角色的数据源
        /// <summary>
        /// 用户所属角色的数据源
        /// </summary>
        /// <param name="allItem">备选项</param>
        /// <param name="selectedItem">选中项</param>
        /// <returns></returns>
        public static List<SelectListItem> InitCheckBoxList(IList<SysRole> allItem, IList<SysRole> selectedItem)
        {
            var cblItems = new List<SelectListItem>();
            foreach (var role in allItem)
            {
                var item = new SelectListItem
                {
                    Text = role.Name,
                    Value = role.ID.ToString()
                };
                if (selectedItem != null)
                {
                    foreach (var selRole in selectedItem)
                    {
                        if (selRole.ID == role.ID)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
                cblItems.Add(item);
            }
            return cblItems;
        }
        public static List<SelectListItem> InitCheckBoxList(IList<SysMenu> allItem, IList<SysMenu> selectedItem)
        {
            var cblItems = new List<SelectListItem>();
            foreach (var menu in allItem)
            {
                var item = new SelectListItem
                {
                    Text = menu.Name,
                    Value = menu.ID.ToString()
                };
                if (selectedItem != null)
                {
                    foreach (var selMenu in selectedItem)
                    {
                        if (selMenu.ID == menu.ID)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
                cblItems.Add(item);
            }
            return cblItems;
        }
        #endregion
        /// <summary>
        /// 当前登录用户 
        /// </summary>
        public static SysUser LoginedUser
        {
            get
            {
                if (HttpContext.Current.Session["LoginedUser"] != null) return HttpContext.Current.Session["LoginedUser"] as SysUser;
                return null;
            }
            set
            {
                HttpContext.Current.Session["LoginedUser"] = value;//保存到Session中
            }
        }
        /// <summary>
        /// 登录用户的菜单权限
        /// </summary> 
        public static IList<SysMenu> Privileges
        {
            get
            {
                IList<ICriterion> listQuery = new List<ICriterion>();
                IList<SysMenu> ret = new List<SysMenu>();
                if (LoginedUser != null && LoginedUser.SysRoleList != null)//如果有用户登录并且有对应的角色
                {
                    foreach (SysRole role in LoginedUser.SysRoleList)//遍历当前用户的角色 
                    {
                        foreach (SysMenu menu in role.SysMenuList)//获取当前用户可以操作的功能 
                        {
                            if (ret.Where(o => o.ID == menu.ID).Count() < 1)//如果当前功能在权限集合中不存在
                            {
                                ret.Add(menu);//添加到集合中 
                            }
                        }
                    }
                    ret.OrderBy(d => d.SortCode);
                }
                return ret;
            }
        }
        /// <summary>
        /// 主机地址
        /// </summary>
        public static string Host
        {
            get
            {
                string ret = HttpContext.Current.Request.Url.AbsoluteUri;
                if (IsMobileBrowser)
                {
                    ret = ret.Replace("SysUser/LoginMobile", "");
                }
                else
                {
                    ret = ret.Replace("SysUser/Login", "");
                }
                return ret;
            }
        }
        /// <summary>
        /// 是否移动浏览器
        /// </summary>
        public static bool IsMobileBrowser
        {
            get
            {
                string sUserAgent = HttpContext.Current.Request.UserAgent.ToLower();
                var bIsIpad = sUserAgent.IndexOf("ipad") >= 0;
                var bIsIphoneOs = sUserAgent.IndexOf("iphone") > 0;
                var bIsMidp = sUserAgent.IndexOf("midp") > 0;
                var bIsUc7 = sUserAgent.IndexOf("rv") > 0;
                var bIsUc = sUserAgent.IndexOf("ucweb") > 0;
                var bIsAndroid = sUserAgent.IndexOf("android") > 0;
                var bIsCE = sUserAgent.IndexOf("windows ce") > 0;
                var bIsWM = sUserAgent.IndexOf("windows mobile") > 0;

                if (bIsIpad || bIsIphoneOs || bIsMidp || bIsUc7 || bIsUc || bIsAndroid || bIsCE || bIsWM)
                {
                    return true;
                }
                return false;
            }
        }

        #region 根据日期确定师傅发车的字段名
        public static string GetFieldNameByDay(DateTime datet)
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