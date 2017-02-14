using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDRS.Model
{
     [Serializable]
     public partial class YX_DZJZ_LSZZWJ
    {
        #region Model
        private string _zzwjbh;
        private string _lszh;
        private string _zzwj;
        private string _sfsc = "N";

        /// <summary>
        /// 资质文件编码
        /// </summary>
        //[DataMember(IsRequired = false)] //设置反序列化非必须的
        public string ZZWJBM
        {
            set { _zzwjbh= value; }
            get { return _zzwjbh; }
        }
        /// <summary>
        /// 律师证号
        /// </summary>
       // [DataMember(IsRequired = false)] //设置反序列化非必须的
        public string LSZH
        {
            set { _lszh = value; }
            get { return _lszh; }
        }
        /// <summary>
        /// 资质文件
        /// </summary>    
       // [DataMember(IsRequired = false)] //设置反序列化非必须的
        public string ZZWJ
        {
            set { _zzwj = value; }
            get { return _zzwj; }
        }
       
        /// <summary>
        /// 是否删除
        /// </summary>
       // [DataMember(IsRequired = false)] //设置反序列化非必须的
        public string SFSC
        {
            set { _sfsc = value; }
            get { return _sfsc; }
        }

        #endregion Model

    }
}
