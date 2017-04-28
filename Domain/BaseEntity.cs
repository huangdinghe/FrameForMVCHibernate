using Castle.ActiveRecord;
using System.ComponentModel.DataAnnotations;

namespace com.fxm.MVCHibernate.Domain
{
    public class BaseEntity<T> : ActiveRecordBase
         where T : class
    {
        /// <summary>
        /// 主键字段Id
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Native)]
        [Display(AutoGenerateField = false)]
        public virtual int ID { get; set; }
    }
}
