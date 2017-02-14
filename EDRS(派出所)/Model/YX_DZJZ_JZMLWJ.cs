/**  版本信息模板在安装目录下，可自行修改。
* YX_DZJZ_JZMLWJ.cs
*
* 功 能： N/A
* 类 名： YX_DZJZ_JZMLWJ
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2007/1/2 9:21:19   N/A    初版
*
*/
using System;
namespace EDRS.Model
{
	/// <summary>
	/// 卷宗目录文件
	/// </summary>
	[Serializable]
	public partial class YX_DZJZ_JZMLWJ
	{
		public YX_DZJZ_JZMLWJ()
		{}
		#region Model
		private string _jzbh;
		private string _mlbh;
		private string _wjxh;
		private string _sfsc="N";
		private DateTime _cjsj= DateTime.Now;
        private DateTime _zhxgsj = DateTime.Now;
		private decimal? _fqdwbm;
		private string _fql;
		private string _dwbm;
		private string _bmsah;
		private string _wjlj;
		private string _wjmc;
		private string _wjxsmc;
		private string _wjhz=".pdf";
		private decimal? _wjksy;
		private decimal? _wjjsy;
		private string _wjbzxx;
		private string _wjyzbz;
		private decimal? _wjsxh;
		private string _wjzdy;
		private string _sslbbm;
		private string _sslbmc;
		/// <summary>
		/// 卷宗编号
		/// </summary>
		public string JZBH
		{
			set{ _jzbh=value;}
			get{return _jzbh;}
		}
		/// <summary>
		/// 目录编号
		/// </summary>
		public string MLBH
		{
			set{ _mlbh=value;}
			get{return _mlbh;}
		}
		/// <summary>
		/// 文件序号(文件唯一标识)
		/// </summary>
		public string WJXH
		{
			set{ _wjxh=value;}
			get{return _wjxh;}
		}
		/// <summary>
		/// 是否删除
		/// </summary>
		public string SFSC
		{
			set{ _sfsc=value;}
			get{return _sfsc;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CJSJ
		{
			set{ _cjsj=value;}
			get{return _cjsj;}
		}
		/// <summary>
		/// 最后修改时间
		/// </summary>
		public DateTime ZHXGSJ
		{
			set{ _zhxgsj=value;}
			get{return _zhxgsj;}
		}
		/// <summary>
		/// 分区单位编码
		/// </summary>
		public decimal? FQDWBM
		{
			set{ _fqdwbm=value;}
			get{return _fqdwbm;}
		}
		/// <summary>
		/// 分区列
		/// </summary>
		public string FQL
		{
			set{ _fql=value;}
			get{return _fql;}
		}
		/// <summary>
		/// 单位编码
		/// </summary>
		public string DWBM
		{
			set{ _dwbm=value;}
			get{return _dwbm;}
		}
		/// <summary>
		/// 部门受案号
		/// </summary>
		public string BMSAH
		{
			set{ _bmsah=value;}
			get{return _bmsah;}
		}
		/// <summary>
		/// 文件路径
		/// </summary>
		public string WJLJ
		{
			set{ _wjlj=value;}
			get{return _wjlj;}
		}
		/// <summary>
		/// 文件名称
		/// </summary>
		public string WJMC
		{
			set{ _wjmc=value;}
			get{return _wjmc;}
		}
		/// <summary>
		/// 文件显示名称
		/// </summary>
		public string WJXSMC
		{
			set{ _wjxsmc=value;}
			get{return _wjxsmc;}
		}
		/// <summary>
		/// 文件后缀
		/// </summary>
		public string WJHZ
		{
			set{ _wjhz=value;}
			get{return _wjhz;}
		}
		/// <summary>
		/// 文件开始页
		/// </summary>
		public decimal? WJKSY
		{
			set{ _wjksy=value;}
			get{return _wjksy;}
		}
		/// <summary>
		/// 文件结束页
		/// </summary>
		public decimal? WJJSY
		{
			set{ _wjjsy=value;}
			get{return _wjjsy;}
		}
		/// <summary>
		/// 文件备注信息
		/// </summary>
		public string WJBZXX
		{
			set{ _wjbzxx=value;}
			get{return _wjbzxx;}
		}
		/// <summary>
		/// 文件上传验证标识(MD5值或者其它可以判定文件已上传的标识)
		/// </summary>
		public string WJYZBZ
		{
			set{ _wjyzbz=value;}
			get{return _wjyzbz;}
		}
		/// <summary>
		/// 文件顺序号
		/// </summary>
		public decimal? WJSXH
		{
			set{ _wjsxh=value;}
			get{return _wjsxh;}
		}
		/// <summary>
		/// 文件自定义
		/// </summary>
		public string WJZDY
		{
			set{ _wjzdy=value;}
			get{return _wjzdy;}
		}
		/// <summary>
		/// 所属类别编码
		/// </summary>
		public string SSLBBM
		{
			set{ _sslbbm=value;}
			get{return _sslbbm;}
		}
		/// <summary>
		/// 所属类别名称
		/// </summary>
		public string SSLBMC
		{
			set{ _sslbmc=value;}
			get{return _sslbmc;}
		}
		#endregion Model

	}
}

