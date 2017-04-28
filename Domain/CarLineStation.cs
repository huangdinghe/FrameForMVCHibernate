using Castle.ActiveRecord;
using System.ComponentModel.DataAnnotations;

namespace com.fxm.MVCHibernate.Domain
{
    /// <summary>
    /// 停靠站
    /// </summary>
    [ActiveRecord]
    public class CarLineStation : BaseEntity<CarLineStation>
    {
        /// <summary>
        /// 线路
        /// </summary>
        [BelongsTo("CarLineId", Lazy = FetchWhen.OnInvoke, NotNull = true)]
        [Display(Name = "线路")]
        public CarLine CarLine { get; set; }

        /// <summary>
        /// 站名  
        /// </summary>
        [Property(NotNull = true, Length = 50)]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(50, ErrorMessage = "不能超过50个字符")]
        [Display(Name = "站名")]
        public string Name { get; set; }

        /// <summary>
        /// 到站时间  
        /// </summary>
        [Property(NotNull = true, Length = 10)]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(10, ErrorMessage = "不能超过10个字符")]
        [Display(Name = "到站时间")]
        public string ArrivalTime { get; set; }
    }
}
