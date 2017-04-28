using Castle.ActiveRecord;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace com.fxm.MVCHibernate.Domain
{
    /// <summary>
    /// 角色
    /// </summary>
    [ActiveRecord]
    public class SysRole : BaseEntity<SysRole>
    {
        /// <summary>
        /// 角色名
        /// </summary>
        [Property(NotNull = true, Length = 20)]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(20, ErrorMessage = "不能超过20个字符")]
        [Display(Name = "角色名")]
        public string Name { get; set; }

        /// <summary>
        /// 当前角色拥有的用户
        /// </summary>
        [HasAndBelongsToMany(typeof(SysUser), Table = "User_Role", ColumnKey = "RoleId", ColumnRef = "UserId", Cascade = ManyRelationCascadeEnum.None, Lazy = false)]
        public IList<SysUser> SysUserList { get; set; }

        /// <summary>
        /// 当前角色可以操作的功能
        /// </summary>
        [HasAndBelongsToMany(typeof(SysMenu), Table = "Role_Menu", ColumnKey = "RoleId", ColumnRef = "MenuId", Lazy = false)]
        public IList<SysMenu> SysMenuList { get; set; }
    }
}
