using Castle.ActiveRecord;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace com.fxm.MVCHibernate.Domain
{
    /// <summary>
    /// 用户
    /// </summary>
    [ActiveRecord]
    public class SysUser : BaseEntity<SysUser>
    {
        /// <summary>
        /// 姓名  
        /// </summary>
        [Property(NotNull = true, Length = 30)]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(30, ErrorMessage = "不能超过30个字符")]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 登录账号  
        /// </summary>
        [Property(NotNull = true, Length = 30)]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(30, ErrorMessage = "不能超过30个字符")]
        [Display(Name = "登录账号")]
        public string LoginCode { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [Property(NotNull = true, Length = 32)]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(32, ErrorMessage = "不能超过32个字符")]
        [Display(Name = "登录密码")]
        public string Password { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [Property(NotNull = false, Length = 30)]
        [StringLength(30, ErrorMessage = "不能超过30个字符")]
        [Display(Name = "电话")]
        public string Tel { get; set; }

        /// <summary>
        /// 状态
        ///     0:正常
        ///     1:禁用
        /// </summary>
        [Property(NotNull = true, Default = "0")]
        [Display(Name = "状态")]
        public int Status { get; set; }

        /// <summary>
        /// 当前用户所属角色
        /// </summary>
        [HasAndBelongsToMany(typeof(SysRole), Table = "User_Role", ColumnKey = "UserId", ColumnRef = "RoleId", Cascade = ManyRelationCascadeEnum.None, Lazy = false)]
        public IList<SysRole> SysRoleList { get; set; }
    }
}
