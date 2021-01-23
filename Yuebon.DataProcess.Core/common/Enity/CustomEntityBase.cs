using System;
using System.Linq;
using System.Reflection;

namespace Yuebon.DataProcess.Core.common.Enity
{
    public abstract class CustomEntityBase
    {
        #region 索引器
        /// <summary>
        /// 索引器，可以用字符串访问变量属性
        /// </summary>
        /// <param name="_propertyName">属性名称</param>
        /// <returns>属性值，如果传入的属性名称不存在，返回null</returns>
        public object this[string _propertyName]
        {
            get
            {
                var pi = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly).FirstOrDefault(p => p.Name.Equals(_propertyName));//从所有获取的属性值中找到传入的属性值
                if (null != pi && null != pi.GetMethod)
                {
                    return pi.GetValue(this);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                var pi = this.GetType().GetProperties().FirstOrDefault(p => p.Name.Equals(_propertyName));
                if (null != pi && null != pi.SetMethod)
                {
                    if (pi.PropertyType.Equals(value.GetType()))
                    {
                        pi.SetValue(this, value);
                    }
                    else
                    {
                        pi.SetValue(this, Convert.ChangeType(value, pi.PropertyType));
                    }
                }
            }
        }
        #endregion
    }
}
