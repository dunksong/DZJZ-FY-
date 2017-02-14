
using System;
namespace EDRS.Model
{
	/// <summary>
	/// 案件基本信息表
	/// </summary>	
	public partial class TYYW_GG_AJJBXX
    {
        #region Model
        private string _shr;
        private DateTime? _shsj;
        private decimal _zjs=0;
        private string _djj;
        private decimal _zys = 0;
        private string _pz;

		/// <summary>
		/// 审核人
		/// </summary>
        public string SHR
		{
            set { _shr = value; }
            get { return _shr; }
		}
		
		/// <summary>
		/// 审核时间
		/// </summary>
        public DateTime? SHSJ
		{
            set { _shsj = value; }
            get { return _shsj; }
		}
		/// <summary>
        /// 总卷数
		/// </summary>
        public decimal ZJS
		{
            set { _zjs = value; }
            get { return _zjs; }
		}
        /// <summary>
        /// 第几页
        /// </summary>
        public string DJJ
        {
            set { _djj = value; }
            get { return _djj; }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public decimal ZYS
        {
            set { _zys = value; }
            get { return _zys; }
        }
      

		#endregion Model

	}
}

