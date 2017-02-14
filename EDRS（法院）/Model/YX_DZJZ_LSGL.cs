using System;
namespace EDRS.Model
{
	/// <summary>
	/// 律师管理表
	/// </summary>
	[Serializable]
	public partial class YX_DZJZ_LSGL
	{
		public YX_DZJZ_LSGL()
		{}
		#region Model
		private string _lszh;
        private string _lsxm;
        private string _lsdw;
        private string _lsdwmc;

        public string LSDWMC
        {
            get { return _lsdwmc; }
            set { _lsdwmc = value; }
        }
        private string _lsdwdz;
		private string _lsdwyzhm;
		private string _lslxdh;
		private string _lssj;
		private string _delxr;
		private string _delxrdh;
		private DateTime? _lszgyxsj;
		private string _sfdxzg="N";
		private string _lsxxbz;
		private string _lszzwj1;
		private string _lszzwj2;
		private string _lszzwj3;
		private string _lszzwj4;
		private DateTime? _cjsj;
		private DateTime? _zhycyjsj;
		private string _sfsc="N";
		/// <summary>
		/// 律师证号
		/// </summary>
		public string LSZH
		{
			set{ _lszh=value;}
			get{return _lszh;}
		}
		/// <summary>
		/// 律师姓名
		/// </summary>
		public string LSXM
		{
			set{ _lsxm=value;}
			get{return _lsxm;}
		}
		/// <summary>
		/// 律师所属单位
		/// </summary>
		public string LSDW
		{
			set{ _lsdw=value;}
			get{return _lsdw;}
		}
		/// <summary>
		/// 律师单位地址
		/// </summary>
		public string LSDWDZ
		{
			set{ _lsdwdz=value;}
			get{return _lsdwdz;}
		}
		/// <summary>
		/// 律师单位邮政号码
		/// </summary>
		public string LSDWYZHM
		{
			set{ _lsdwyzhm=value;}
			get{return _lsdwyzhm;}
		}
		/// <summary>
		/// 律师联系电话
		/// </summary>
		public string LSLXDH
		{
			set{ _lslxdh=value;}
			get{return _lslxdh;}
		}
		/// <summary>
		/// 律师手机
		/// </summary>
		public string LSSJ
		{
			set{ _lssj=value;}
			get{return _lssj;}
		}
		/// <summary>
		/// 第二联系人
		/// </summary>
		public string DELXR
		{
			set{ _delxr=value;}
			get{return _delxr;}
		}
		/// <summary>
		/// 第二联系人电话
		/// </summary>
		public string DELXRDH
		{
			set{ _delxrdh=value;}
			get{return _delxrdh;}
		}
		/// <summary>
		/// 律师资格有效时间
		/// </summary>
		public DateTime? LSZGYXSJ
		{
			set{ _lszgyxsj=value;}
			get{return _lszgyxsj;}
		}
		/// <summary>
		/// 是否吊销资格(Y/N)
		/// </summary>
		public string SFDXZG
		{
			set{ _sfdxzg=value;}
			get{return _sfdxzg;}
		}
		/// <summary>
		/// 律师信息备注
		/// </summary>
		public string LSXXBZ
		{
			set{ _lsxxbz=value;}
			get{return _lsxxbz;}
		}
		/// <summary>
		/// 律师资质文件1
		/// </summary>
		public string LSZZWJ1
		{
			set{ _lszzwj1=value;}
			get{return _lszzwj1;}
		}
		/// <summary>
		/// 律师资质文件2
		/// </summary>
		public string LSZZWJ2
		{
			set{ _lszzwj2=value;}
			get{return _lszzwj2;}
		}
		/// <summary>
		/// 律师资质文件3
		/// </summary>
		public string LSZZWJ3
		{
			set{ _lszzwj3=value;}
			get{return _lszzwj3;}
		}
		/// <summary>
		/// 律师资质文件4
		/// </summary>
		public string LSZZWJ4
		{
			set{ _lszzwj4=value;}
			get{return _lszzwj4;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? CJSJ
		{
			set{ _cjsj=value;}
			get{return _cjsj;}
		}
		/// <summary>
		/// 最后一次阅卷时间
		/// </summary>
		public DateTime? ZHYCYJSJ
		{
			set{ _zhycyjsj=value;}
			get{return _zhycyjsj;}
		}
		/// <summary>
		/// 是否删除
		/// </summary>
		public string SFSC
		{
			set{ _sfsc=value;}
			get{return _sfsc;}
		}
		#endregion Model

	}
}

