using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDRS.Model
{
    /// <summary>
    /// 卷宗文件申请打印记录
    /// </summary>
    [Serializable]
    public partial class YX_DZJZ_WJSQDYJL
    {
        #region Model
        private string _sqbh;
        private string _xh;
        private string _yjxh;
        private string _wjxh;
        private DateTime _addtime = DateTime.Now;
        private string _sfxydy = "N";
        private string _sfsc = "N";

        /// <summary>
        /// 分配编码
        /// </summary>
        //[DataMember(IsRequired = false)] //设置反序列化非必须的
        public string SQBH
        {
            set { _sqbh= value; }
            get { return _sqbh; }
        }
        /// <summary>
        /// 阅卷序号
        /// </summary>
       // [DataMember(IsRequired = false)] //设置反序列化非必须的
        public string XH
        {
            set { _xh = value; }
            get { return _xh; }
        }
        /// <summary>
        /// 阅卷序号
        /// </summary>
        public string YJXH
        {
            set { _yjxh = value; }
            get { return _yjxh; }
        }
        /// <summary>
        /// 文件序号
        /// </summary>    
       // [DataMember(IsRequired = false)] //设置反序列化非必须的
        public string WJXH
        {
            set { _wjxh = value; }
            get { return _wjxh; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        //[DataMember(IsRequired = false)] //设置反序列化非必须的
        public DateTime ADDTIME
        {
            get { return _addtime; }
            set { _addtime = value; }
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
        /// <summary>
        /// 是否确认打印
        /// </summary>
        public string SFXYDY
        {
            set { _sfxydy = value; }
            get { return _sfxydy; }
        }

        #endregion Model

    }
}
