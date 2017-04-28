using Castle.ActiveRecord;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace com.fxm.MVCHibernate.Domain
{
    /// <summary>
    /// 线路
    /// </summary>
    [ActiveRecord]
    public class CarLine : BaseEntity<CarLine>
    {
        /// <summary>
        /// 班车
        /// </summary>
        [BelongsTo("CarId", Lazy = FetchWhen.OnInvoke, NotNull = true)]
        [Display(Name = "班车")]
        public Car Car { get; set; }

        /// <summary>
        /// 线路  
        /// </summary>
        [Property(NotNull = true, Length = 50)]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(50, ErrorMessage = "不能超过50个字符")]
        [Display(Name = "线路")]
        public string Name { get; set; }

        /// <summary>
        /// 线路说明  
        /// </summary>
        [Property(NotNull = false, Length = 100)]
        [StringLength(100, ErrorMessage = "不能超过100个字符")]
        [Display(Name = "线路说明")]
        public string Remark { get; set; }
        /// <summary>
        /// 发车时间
        /// </summary>
        [Property(NotNull = false, Length = 5)]
        [StringLength(5, ErrorMessage = "不能超过5个字符")]
        [Display(Name = "发车时间")]
        public string CarLineStartTime { get; set; }
        /// <summary>
        /// 班别
        ///     1:早班
        ///     2：晚班
        /// </summary>
        [Property(NotNull = true)]
        [Display(Name = "班别")]
        public int Shift { get; set; }
        /// <summary>
        /// 大休周一
        /// </summary>
        [Property(NotNull = true,Default="0")]
        [Required(ErrorMessage = "不能为空")]
        [Display(Name = "大休周一")]
        public int Monday1 { get; set; }

        /// <summary>
        /// 小休周一 
        /// </summary>
        [Property(NotNull = true, Default = "0")]
        [Required(ErrorMessage = "不能为空")]
        [Display(Name = "小休周一")]
        public int Monday2 { get; set; }

        /// <summary>
        /// 大休周二
        /// </summary>
        [Property(NotNull = true, Default = "0")]
        [Required(ErrorMessage = "不能为空")]
        [Display(Name = "大休周二")]
        public int Tuesday1 { get; set; }

        /// <summary>
        /// 小休周二
        /// </summary>
        [Property(NotNull = true, Default = "0")]
        [Required(ErrorMessage = "不能为空")]
        [Display(Name = "小休周二")]
        public int Tuesday2 { get; set; }

        /// <summary>
        /// 大休周三
        /// </summary>
        [Property(NotNull = true, Default = "0")]
        [Required(ErrorMessage = "不能为空")]
        [Display(Name = "大休周三")]
        public int Wednesday1 { get; set; }

        /// <summary>
        /// 小休周三
        /// </summary>
        [Property(NotNull = true, Default = "0")]
        [Required(ErrorMessage = "不能为空")]
        [Display(Name = "小休周三")]
        public int Wednesday2 { get; set; }

        /// <summary>
        /// 大休周四
        /// </summary>
        [Property(NotNull = true, Default = "0")]
        [Required(ErrorMessage = "不能为空")]
        [Display(Name = "大休周四")]
        public int Thursday1 { get; set; }

        /// <summary>
        /// 小休周四
        /// </summary>
        [Property(NotNull = true, Default = "0")]
        [Required(ErrorMessage = "不能为空")]
        [Display(Name = "小休周四")]
        public int Thursday2 { get; set; }

        /// <summary>
        /// 大休周五
        /// </summary>
        [Property(NotNull = true, Default = "0")]
        [Required(ErrorMessage = "不能为空")]
        [Display(Name = "大休周五")]
        public int Friday1 { get; set; }

        /// <summary>
        /// 小休周五
        /// </summary>
        [Property(NotNull = true, Default = "0")]
        [Required(ErrorMessage = "不能为空")]
        [Display(Name = "小休周五")]
        public int Friday2 { get; set; }

        /// <summary>
        /// 大休周六
        /// </summary>
        [Property(NotNull = true, Default = "0")]
        [Required(ErrorMessage = "不能为空")]
        [Display(Name = "大休周六")]
        public int Saturday1 { get; set; }

        /// <summary>
        /// 小休周六
        /// </summary>
        [Property(NotNull = true, Default = "0")]
        [Required(ErrorMessage = "不能为空")]
        [Display(Name = "小休周六")]
        public int Saturday2 { get; set; }

        /// <summary>
        /// 大休周日
        /// </summary>
        [Property(NotNull = true, Default = "0")]
        [Required(ErrorMessage = "不能为空")]
        [Display(Name = "大休周日")]
        public int Sunday1 { get; set; }

        /// <summary>
        /// 小休周日
        /// </summary>
        [Property(NotNull = true, Default = "0")]
        [Required(ErrorMessage = "不能为空")]
        [Display(Name = "小休周日")]
        public int Sunday2 { get; set; }
        /// <summary>
        /// 停靠站
        /// </summary>
        [HasMany(typeof(CarLineStation), ColumnKey = "CarLineId", Lazy = false, Inverse = true)]
        public virtual IList<CarLineStation> CarLineStationList { get; set; }
    }
}
