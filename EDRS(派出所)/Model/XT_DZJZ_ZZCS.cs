/**  版本信息模板在安装目录下，可自行修改。
* XT_DZJZ_ZZCS.cs
*
* 功 能： N/A
* 类 名： XT_DZJZ_JZMB
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2007/1/2 9:21:20   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*/
using System;
namespace EDRS.Model
{
	/// <summary>
	/// 卷宗目录模板主表
	/// </summary>
	[Serializable]
	public partial class XT_DZJZ_ZZCS
	{
        public XT_DZJZ_ZZCS()
		{}
		#region Model
        private string _csbh;        
        private string _cskey;        
        private string _csvalue;        
        private DateTime _dysj;        
        private string _fzbs;

        /// <summary>
        /// 参数编号
        /// </summary>
        public string CSBH
        {
            get { return _csbh; }
            set { _csbh = value; }
        }
        /// <summary>
        /// 参数键
        /// </summary>
        public string CSKEY
        {
            get { return _cskey; }
            set { _cskey = value; }
        }
        /// <summary>
        /// 参数值
        /// </summary>
        public string CSVALUE
        {
            get { return _csvalue; }
            set { _csvalue = value; }
        }
        /// <summary>
        /// 参数创建时间
        /// </summary>
        public DateTime DYSJ
        {
            get { return _dysj; }
            set { _dysj = value; }
        }
        /// <summary>
        /// 分组编号
        /// </summary>
        public string FZBS
        {
            get { return _fzbs; }
            set { _fzbs = value; }
        }


		#endregion Model

	}
}

