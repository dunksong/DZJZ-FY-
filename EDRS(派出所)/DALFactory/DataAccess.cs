using System;
using System.Reflection;
using System.Configuration;
namespace EDRS.DALFactory
{
	/// <summary>
    /// Abstract Factory pattern to create the DAL。
    /// 如果在这里创建对象报错，请检查web.config里是否修改了<add key="DAL" value="Maticsoft.SQLServerDAL" />。
	/// </summary>
	public sealed class DataAccess 
	{
        private static readonly string AssemblyPath = ConfigurationManager.AppSettings["DAL"];        
		public DataAccess()
		{ }

        #region CreateObject 

		//不使用缓存
        private static object CreateObjectNoCache(string AssemblyPath,string classNamespace)
		{		
			try
			{
				object objType = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);	
				return objType;
			}
			catch//(System.Exception ex)
			{
				//string str=ex.Message;// 记录错误日志
				return null;
			}			
			
        }
		//使用缓存
		private static object CreateObject(string AssemblyPath,string classNamespace)
		{			
			object objType = DataCache.GetCache(classNamespace);
			if (objType == null)
			{
				try
				{
					objType = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);					
					DataCache.SetCache(classNamespace, objType);// 写入缓存
				}
				catch//(System.Exception ex)
				{
					//string str=ex.Message;// 记录错误日志
				}
			}
			return objType;
		}
        #endregion

        #region 泛型生成
        ///// <summary>
        ///// 创建数据层接口。
        ///// </summary>
        //public static t Create(string ClassName)
        //{

        //    string ClassNamespace = AssemblyPath +"."+ ClassName;
        //    object objType = CreateObject(AssemblyPath, ClassNamespace);
        //    return (t)objType;
        //}
        #endregion
        /// <summary>
        /// 创建XT_QX_ANDY数据层接口。按钮管理
        /// </summary>
        public static EDRS.IDAL.IXT_QX_ANDY CreateXT_QX_ANDY()
        {

            string ClassNamespace = AssemblyPath + ".XT_QX_ANDY";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (EDRS.IDAL.IXT_QX_ANDY)objType;
        }


        /// <summary>
        /// 创建XT_QX_JSANQX数据层接口。角色按钮权限
        /// </summary>
        public static EDRS.IDAL.IXT_QX_JSANQX CreateXT_QX_JSANQX()
        {

            string ClassNamespace = AssemblyPath + ".XT_QX_JSANQX";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (EDRS.IDAL.IXT_QX_JSANQX)objType;
        }

        /// <summary>
        /// 创建YX_DZJZ_FMDYMB数据层接口。电子卷宗封面打印模板
        /// </summary>
        public static EDRS.IDAL.IYX_DZJZ_FMDYMB CreateYX_DZJZ_FMDYMB()
        {

            string ClassNamespace = AssemblyPath + ".YX_DZJZ_FMDYMB";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (EDRS.IDAL.IYX_DZJZ_FMDYMB)objType;
        }
        /// <summary>
        /// 创建YX_DZJZ_FMDY数据层接口。电子卷宗封面打印
        /// </summary>
        public static EDRS.IDAL.IYX_DZJZ_FMDY CreateYX_DZJZ_FMDY()
        {

            string ClassNamespace = AssemblyPath + ".YX_DZJZ_FMDY";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (EDRS.IDAL.IYX_DZJZ_FMDY)objType;
        }

        /// <summary>
        /// 创建XT_DZJZ_SSLB数据层接口。所属类别
        /// </summary>
        public static EDRS.IDAL.IXT_DZJZ_SSLB CreateXT_DZJZ_SSLB()
        {

            string ClassNamespace = AssemblyPath + ".XT_DZJZ_SSLB";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (EDRS.IDAL.IXT_DZJZ_SSLB)objType;
        }
        /// <summary>
        /// 创建YX_DZJZ_LSYJXZSQ数据层接口
        /// </summary>
        public static EDRS.IDAL.IXT_DZJZ_ZZCS CreateXT_DZJZ_ZZCS()
        {

            string ClassNamespace = AssemblyPath + ".XT_DZJZ_ZZCS";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (EDRS.IDAL.IXT_DZJZ_ZZCS)objType;
        }

        /// <summary>
        /// 创建YX_DZJZ_LSYJXZSQ数据层接口
        /// </summary>
        public static EDRS.IDAL.IYX_DZJZ_LSYJXZSQ CreateYX_DZJZ_LSYJXZSQ()
        {

            string ClassNamespace = AssemblyPath + ".YX_DZJZ_LSYJXZSQ";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (EDRS.IDAL.IYX_DZJZ_LSYJXZSQ)objType;
        }

        /// <summary>
        /// 创建IXT_DM_YWBM数据层接口。业务类型
        /// </summary>
        public static EDRS.IDAL.IXT_DM_YWBM CreateXT_DM_YWBM()
        {
            string ClassNamespace = AssemblyPath + ".XT_DM_YWBM";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (EDRS.IDAL.IXT_DM_YWBM)objType;
        }

        /// <summary>
        /// 创建IYX_DZJZ_LSAJWJFP数据层接口。律师案件文件分配
        /// </summary>
        public static EDRS.IDAL.IYX_DZJZ_LSAJWJFP CreateYX_DZJZ_LSAJWJFP()
        {
            string ClassNamespace = AssemblyPath + ".YX_DZJZ_LSAJWJFP";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (EDRS.IDAL.IYX_DZJZ_LSAJWJFP)objType;
        }

        /// <summary>
        /// 创建IDataStatistics数据层接口。统计
        /// </summary>
        public static EDRS.IDAL.IDataStatistics CreateDataStatistics()
        {
            string ClassNamespace = AssemblyPath + ".DataStatistics";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (EDRS.IDAL.IDataStatistics)objType;
        }

        /// <summary>
        /// 创建IXT_DM_AJLBBM数据层接口。案件类别
        /// </summary>
        public static EDRS.IDAL.IXT_DM_AJLBBM CreateXT_DM_AJLBBM()
        {
            string ClassNamespace = AssemblyPath + ".XT_DM_AJLBBM";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (EDRS.IDAL.IXT_DM_AJLBBM)objType;
        }

        /// <summary>
        /// 创建IXY_DZJZ_MBPZB数据层接口。功能定义表
        /// </summary>
        public static EDRS.IDAL.IXY_DZJZ_MBPZB CreateXY_DZJZ_MBPZB()
        {
            string ClassNamespace = AssemblyPath + ".XY_DZJZ_MBPZB";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (EDRS.IDAL.IXY_DZJZ_MBPZB)objType;
        }

        /// <summary>
        /// 创建IXT_DM_QX数据层接口。功能定义表
        /// </summary>
        public static EDRS.IDAL.IXT_DM_QX CreateXT_DM_QX()
        {

            string ClassNamespace = AssemblyPath + ".XT_DM_QX";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (EDRS.IDAL.IXT_DM_QX)objType;
        }
        /// <summary>
        /// 创建IEDRS_Report数据层接口。工作量统计
        /// </summary>
        public static EDRS.IDAL.IEDRS_Report CreateEDRS_Report()
        {
            string ClassNamespace = AssemblyPath + ".EDRS_Report";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (EDRS.IDAL.IEDRS_Report)objType;
        }
        /// <summary>
        /// 创建IXT_DM_QX数据层接口。功能定义表
        /// </summary>
        public static EDRS.IDAL.IXT_QX_RYJZQXFP CreateXT_QX_RYJZQXFP()
        {
            string ClassNamespace = AssemblyPath + ".XT_QX_RYJZQXFP";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (EDRS.IDAL.IXT_QX_RYJZQXFP)objType;
        }

        /// <summary>
        /// 创建XT_QX_RYGNFP数据层接口。
        /// </summary>
        public static EDRS.IDAL.IXT_QX_RYGNFP CreateXT_QX_RYGNFP()
        {

            string ClassNamespace = AssemblyPath + ".XT_QX_RYGNFP";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (EDRS.IDAL.IXT_QX_RYGNFP)objType;
        }

        /// <summary>
        /// 创建XT_QX_GNFL数据层接口。
        /// </summary>
        public static EDRS.IDAL.IXT_QX_GNFL CreateXT_QX_GNFL()
        {

            string ClassNamespace = AssemblyPath + ".XT_QX_GNFL";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (EDRS.IDAL.IXT_QX_GNFL)objType;
        }


        /// <summary>
        /// 创建TYYW_GG_AJJBXX数据层接口。案件基本信息表
        /// </summary>
        public static EDRS.IDAL.ITYYW_GG_AJJBXX CreateTYYW_GG_AJJBXX()
        {

            string ClassNamespace = AssemblyPath + ".TYYW_GG_AJJBXX";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (EDRS.IDAL.ITYYW_GG_AJJBXX)objType;
        }


        /// <summary>
        /// 创建YX_DZJZ_JZMLWJ数据层接口。卷宗目录文件
        /// </summary>
        public static EDRS.IDAL.IYX_DZJZ_JZMLWJ CreateYX_DZJZ_JZMLWJ()
        {

            string ClassNamespace = AssemblyPath + ".YX_DZJZ_JZMLWJ";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (EDRS.IDAL.IYX_DZJZ_JZMLWJ)objType;
        }


        /// <summary>
        /// 创建YX_DZJZ_JZML数据层接口。卷宗目录
        /// </summary>
        public static EDRS.IDAL.IYX_DZJZ_JZML CreateYX_DZJZ_JZML()
        {

            string ClassNamespace = AssemblyPath + ".YX_DZJZ_JZML";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (EDRS.IDAL.IYX_DZJZ_JZML)objType;
        }


        /// <summary>
        /// 创建YX_DZJZ_JZJBXX数据层接口。卷宗基本信息表
        /// </summary>
        public static EDRS.IDAL.IYX_DZJZ_JZJBXX CreateYX_DZJZ_JZJBXX()
        {

            string ClassNamespace = AssemblyPath + ".YX_DZJZ_JZJBXX";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (EDRS.IDAL.IYX_DZJZ_JZJBXX)objType;
        }


        /// <summary>
        /// 创建XT_DZJZ_JZMBML数据层接口。卷宗目录模板明细表
        /// </summary>
        public static EDRS.IDAL.IXT_DZJZ_JZMBML CreateXT_DZJZ_JZMBML()
        {

            string ClassNamespace = AssemblyPath + ".XT_DZJZ_JZMBML";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (EDRS.IDAL.IXT_DZJZ_JZMBML)objType;
        }


        /// <summary>
        /// 创建XT_DZJZ_JZMB数据层接口。卷宗目录模板主表
        /// </summary>
        public static EDRS.IDAL.IXT_DZJZ_JZMB CreateXT_DZJZ_JZMB()
        {

            string ClassNamespace = AssemblyPath + ".XT_DZJZ_JZMB";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (EDRS.IDAL.IXT_DZJZ_JZMB)objType;
        }

		/// <summary>
		/// 创建YX_DZJZ_WJSQDY数据层接口。卷宗文件申请打印表(一个案件一个律师当前阅卷
		/// </summary>
		public static EDRS.IDAL.IYX_DZJZ_WJSQDY CreateYX_DZJZ_WJSQDY()
		{

			string ClassNamespace = AssemblyPath +".YX_DZJZ_WJSQDY";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (EDRS.IDAL.IYX_DZJZ_WJSQDY)objType;
		}


		/// <summary>
		/// 创建YX_DZJZ_LSYJSQ数据层接口。阅卷申请表
		/// </summary>
		public static EDRS.IDAL.IYX_DZJZ_LSYJSQ CreateYX_DZJZ_LSYJSQ()
		{

			string ClassNamespace = AssemblyPath +".YX_DZJZ_LSYJSQ";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (EDRS.IDAL.IYX_DZJZ_LSYJSQ)objType;
		}


		/// <summary>
		/// 创建YX_DZJZ_LSGL数据层接口。律师管理表
		/// </summary>
		public static EDRS.IDAL.IYX_DZJZ_LSGL CreateYX_DZJZ_LSGL()
		{

			string ClassNamespace = AssemblyPath +".YX_DZJZ_LSGL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (EDRS.IDAL.IYX_DZJZ_LSGL)objType;
		}

        /// <summary>
        /// 创建YX_DZJZ_LSZZWJ数据层接口。律师管理表
        /// </summary>
        public static EDRS.IDAL.IYX_DZJZ_LSZZWJ CreateYX_DZJZ_LSZZWJ()
        {

            string ClassNamespace = AssemblyPath + ".YX_DZJZ_LSZZWJ";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (EDRS.IDAL.IYX_DZJZ_LSZZWJ)objType;
        }

		/// <summary>
		/// 创建YX_DZJZ_LSAJBD数据层接口。律师阅卷绑定表(一个
		/// </summary>
		public static EDRS.IDAL.IYX_DZJZ_LSAJBD CreateYX_DZJZ_LSAJBD()
		{

			string ClassNamespace = AssemblyPath +".YX_DZJZ_LSAJBD";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (EDRS.IDAL.IYX_DZJZ_LSAJBD)objType;
		}


		/// <summary>
		/// 创建YX_DZJZ_JZWJCC数据层接口。卷宗文件存储表（单页
		/// </summary>
		public static EDRS.IDAL.IYX_DZJZ_JZWJCC CreateYX_DZJZ_JZWJCC()
		{

			string ClassNamespace = AssemblyPath +".YX_DZJZ_JZWJCC";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (EDRS.IDAL.IYX_DZJZ_JZWJCC)objType;
		}


		/// <summary>
		/// 创建YX_DZJZ_JZRZJL数据层接口。卷宗日志记录表
		/// </summary>
		public static EDRS.IDAL.IYX_DZJZ_JZRZJL CreateYX_DZJZ_JZRZJL()
		{

			string ClassNamespace = AssemblyPath +".YX_DZJZ_JZRZJL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (EDRS.IDAL.IYX_DZJZ_JZRZJL)objType;
		}


		/// <summary>
		/// 创建YX_DZJZ_JZAJXX数据层接口。卷宗案件信息表
		/// </summary>
		public static EDRS.IDAL.IYX_DZJZ_JZAJXX CreateYX_DZJZ_JZAJXX()
		{

			string ClassNamespace = AssemblyPath +".YX_DZJZ_JZAJXX";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (EDRS.IDAL.IYX_DZJZ_JZAJXX)objType;
		}


		/// <summary>
		/// 创建YX_DZJZ_DYSQD数据层接口。打印申请单
		/// </summary>
		public static EDRS.IDAL.IYX_DZJZ_DYSQD CreateYX_DZJZ_DYSQD()
		{

			string ClassNamespace = AssemblyPath +".YX_DZJZ_DYSQD";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (EDRS.IDAL.IYX_DZJZ_DYSQD)objType;
		}


		/// <summary>
		/// 创建YX_AG_YJAP数据层接口。阅卷安排
		/// </summary>
		public static EDRS.IDAL.IYX_AG_YJAP CreateYX_AG_YJAP()
		{

			string ClassNamespace = AssemblyPath +".YX_AG_YJAP";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (EDRS.IDAL.IYX_AG_YJAP)objType;
		}


		/// <summary>
		/// 创建XY_DZJZ_XTPZ数据层接口。系统配置表
		/// </summary>
		public static EDRS.IDAL.IXY_DZJZ_XTPZ CreateXY_DZJZ_XTPZ()
		{

			string ClassNamespace = AssemblyPath +".XY_DZJZ_XTPZ";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (EDRS.IDAL.IXY_DZJZ_XTPZ)objType;
		}


		/// <summary>
		/// 创建XY_DZJZ_SBDJ数据层接口。扫描设备登记表
		/// </summary>
		public static EDRS.IDAL.IXY_DZJZ_SBDJ CreateXY_DZJZ_SBDJ()
		{

			string ClassNamespace = AssemblyPath +".XY_DZJZ_SBDJ";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (EDRS.IDAL.IXY_DZJZ_SBDJ)objType;
		}


		/// <summary>
		/// 创建XT_ZZJG_RYBM数据层接口。人员编码
		/// </summary>
		public static EDRS.IDAL.IXT_ZZJG_RYBM CreateXT_ZZJG_RYBM()
		{

			string ClassNamespace = AssemblyPath +".XT_ZZJG_RYBM";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (EDRS.IDAL.IXT_ZZJG_RYBM)objType;
		}


		/// <summary>
		/// 创建XT_ZZJG_DWBM数据层接口。单位编码
		/// </summary>
		public static EDRS.IDAL.IXT_ZZJG_DWBM CreateXT_ZZJG_DWBM()
		{

			string ClassNamespace = AssemblyPath +".XT_ZZJG_DWBM";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (EDRS.IDAL.IXT_ZZJG_DWBM)objType;
		}


		/// <summary>
		/// 创建XT_ZZJG_BMBM数据层接口。部门编码
		/// </summary>
		public static EDRS.IDAL.IXT_ZZJG_BMBM CreateXT_ZZJG_BMBM()
		{

			string ClassNamespace = AssemblyPath +".XT_ZZJG_BMBM";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (EDRS.IDAL.IXT_ZZJG_BMBM)objType;
		}


		/// <summary>
		/// 创建XT_QX_RYJSFP数据层接口。人员角色分配
		/// </summary>
		public static EDRS.IDAL.IXT_QX_RYJSFP CreateXT_QX_RYJSFP()
		{

			string ClassNamespace = AssemblyPath +".XT_QX_RYJSFP";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (EDRS.IDAL.IXT_QX_RYJSFP)objType;
		}


		/// <summary>
		/// 创建XT_QX_JSGNFP数据层接口。角色功能授权表
		/// </summary>
		public static EDRS.IDAL.IXT_QX_JSGNFP CreateXT_QX_JSGNFP()
		{

			string ClassNamespace = AssemblyPath +".XT_QX_JSGNFP";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (EDRS.IDAL.IXT_QX_JSGNFP)objType;
		}


		/// <summary>
		/// 创建XT_QX_JSBM数据层接口。角色编码
		/// </summary>
		public static EDRS.IDAL.IXT_QX_JSBM CreateXT_QX_JSBM()
		{

			string ClassNamespace = AssemblyPath +".XT_QX_JSBM";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (EDRS.IDAL.IXT_QX_JSBM)objType;
		}


		/// <summary>
		/// 创建XT_QX_GNDY数据层接口。功能定义表
		/// </summary>
		public static EDRS.IDAL.IXT_QX_GNDY CreateXT_QX_GNDY()
		{

			string ClassNamespace = AssemblyPath +".XT_QX_GNDY";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (EDRS.IDAL.IXT_QX_GNDY)objType;
		}
      
}
}