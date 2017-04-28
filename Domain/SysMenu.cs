using Castle.ActiveRecord;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace com.fxm.MVCHibernate.Domain
{
    /// <summary>
    /// 系统功能
    /// </summary>
    [ActiveRecord]
    public class SysMenu : BaseEntity<SysMenu>
    {
        /// <summary>
        /// 系统功能
        /// </summary>
        [Property(NotNull = true, Length = 20)]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(10, ErrorMessage = "不能超过10个字符")]
        [Display(Name = "系统功能")]
        public string Name { get; set; }

        /// <summary>
        /// 类名
        /// </summary>
        [Property(Length = 200)]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(200, ErrorMessage = "不能超过200个字符")]
        [Display(Name = "类名")]
        public string LoadClassName { get; set; }

        /// <summary>
        /// 控制器名
        /// </summary>
        [Property(Length = 50)]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(50, ErrorMessage = "不能超过50个字符")]
        [Display(Name = "控制器名")]
        public string ControllerName { get; set; }

        /// <summary>
        /// Action名
        /// </summary>
        [Property(Length = 200)]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(50, ErrorMessage = "不能超过50个字符")]
        [Display(Name = "Action名")]
        public string ActionName { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        [Property]
        [Display(Name = "排序码")]
        public int SortCode { get; set; }

        /// <summary>
        /// 父功能
        /// </summary>
        [BelongsTo(Column = "ParentId", NotNull = false, Cascade = CascadeEnum.None, Lazy = FetchWhen.OnInvoke)]
        public SysMenu ParentMenu { get; set; }

        /// <summary>
        /// 可以操作当前功能的角色
        /// </summary>
        [HasAndBelongsToMany(typeof(SysRole), Table = "Role_Menu", ColumnKey = "MenuId", ColumnRef = "RoleId", Cascade = ManyRelationCascadeEnum.None, Inverse = false, Lazy = false)]
        public IList<SysRole> SysRoleList { get; set; }
    }
}
