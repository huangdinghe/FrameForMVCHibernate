using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace com.fxm.MVCHibernate.Domain
{
    /// <summary>
    /// 订座记录
    /// </summary>
    [ActiveRecord]
    public class OrderSeatRec : BaseEntity<OrderSeatRec>
    {
        /// <summary>
        /// 班车
        /// </summary>
        [BelongsTo("CarId", Lazy = FetchWhen.OnInvoke, NotNull = true)]
        [Display(Name = "班车")]
        public Car Car { get; set; }
        /// <summary>
        /// 司机
        /// </summary>
        [Property(NotNull = true)]
        [Display(Name = "司机")]
        public string DriverName { get; set; }
        /// <summary>
        /// 司机电话
        /// </summary>
        [Property(NotNull = true)]
        [Display(Name = "司机电话")]
        public string DriverTel { get; set; }

        /// <summary>
        /// 线路
        /// </summary>
        [BelongsTo("CarLineId", Lazy = FetchWhen.OnInvoke, NotNull = true)]
        [Display(Name = "线路")]
        public CarLine CarLine { get; set; }

        /// <summary>
        /// 班车日期
        /// </summary>
        [Property(NotNull = true)]
        [Display(Name = "班车日期")]
        public DateTime CarDate { get; set; }

        /// <summary>
        /// 订座人
        /// </summary>
        [BelongsTo("OrderUserId", Lazy = FetchWhen.OnInvoke, NotNull = true)]
        [Display(Name = "订座人")]
        public SysUser OrderPerson { get; set; }

        /// <summary>
        /// 订座时间
        /// </summary>
        [Property(NotNull = true)]
        [Display(Name = "订座时间")]
        public DateTime OrderTime { get; set; }

        /// <summary>
        /// 发车时间
        /// </summary>
        [Property(NotNull = true)]
        [Display(Name = "发车时间")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 班别
        ///     1:早班
        ///     2：晚班
        /// </summary>
        [Property(NotNull = true)]
        [Display(Name = "班别")]
        public int Shift { get; set; }
        
        /// <summary>
        /// 座号
        /// </summary>
        [Property(NotNull = true, Default = "0")]
        [Display(Name = "座号")]
        public int SeatNumber { get; set; }
    }
}
