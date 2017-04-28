using Castle.ActiveRecord;
using System;
using System.ComponentModel.DataAnnotations;

namespace com.fxm.MVCHibernate.Domain
{
    /// <summary>
    /// 班车
    /// </summary>
    [ActiveRecord]
    public class Car : BaseEntity<Car>
    {
        /// <summary>
        /// 班车  
        /// </summary>
        [Property(NotNull = true, Length = 50)]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(50, ErrorMessage = "不能超过50个字符")]
        [Display(Name = "班车")]
        public string Name { get; set; }

        /// <summary>
        /// 车牌  
        /// </summary>
        [Property(NotNull = true, Length = 20)]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(20, ErrorMessage = "不能超过20个字符")]
        [Display(Name = "车牌")]
        public string PlateNumber { get; set; }

        /// <summary>
        /// 座位数  
        /// </summary>
        [Property(NotNull = true,Default="0")]
        [Display(Name = "座位数")]
        public int SeatCount { get; set; }

        /// <summary>
        /// 司机
        /// </summary>
        [BelongsTo(Column = "DriverId", NotNull = false, Cascade = CascadeEnum.None, Lazy = FetchWhen.OnInvoke)]
        public SysUser Driver { get; set; }

        /// <summary>
        /// 状态
        ///     0:正常
        ///     1:停运
        /// </summary>
        [Property(NotNull = true, Default = "0")]
        [Display(Name = "状态")]
        public int Status { get; set; }
    }
}
