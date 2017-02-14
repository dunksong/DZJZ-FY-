using System;
using System.Runtime.Serialization;
namespace EDRS.Model
{
	/// <summary>
    /// 律师案件文件分配
	/// </summary>
	[Serializable]
    [DataContract]
	public partial class YX_DZJZ_LSAJWJFP
	{
        public YX_DZJZ_LSAJWJFP()
		{}
		#region Model
        private string _fpbm;
        private string _yjxh;
        private string _wjxh;
        private DateTime _addtime = DateTime.Now;
		private string _sfsc="N";
        
		/// <summary>
        /// 分配编码
		/// </summary>
        [DataMember(IsRequired = false)] //设置反序列化非必须的
        public string FPBM
		{
            set { _fpbm = value; }
            get { return _fpbm; }
		}
		/// <summary>
        /// 阅卷序号
		/// </summary>
        [DataMember(IsRequired = false)] //设置反序列化非必须的
        public string YJXH
		{
            set { _yjxh = value; }
            get { return _yjxh; }
		}
		/// <summary>
        /// 文件序号
		/// </summary>    
        [DataMember(IsRequired = false)] //设置反序列化非必须的
        public string WJXH
		{
            set { _wjxh = value; }
            get { return _wjxh; }
		}
        /// <summary>
        /// 添加时间
        /// </summary>
        [DataMember(IsRequired = false)] //设置反序列化非必须的
        public DateTime ADDTIME
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
		/// <summary>
		/// 是否删除
		/// </summary>
        [DataMember(IsRequired=false)] //设置反序列化非必须的
		public string SFSC
		{
			set{ _sfsc=value;}
			get{return _sfsc;}
		}
		
		#endregion Model

	}
}

