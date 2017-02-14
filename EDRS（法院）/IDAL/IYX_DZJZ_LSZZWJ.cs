using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDRS.IDAL
{
    public interface IYX_DZJZ_LSZZWJ : ILogBase
    {
        #region  成员方法
        /// <summary>
        /// 添加律师资质文件列表
        /// </summary>
        /// <returns></returns>
        bool AddList(string LSZH,List<string> fileList);
        /// <summary>
        /// 删除律师资质文件列表
        /// </summary>
        /// <param name="LSZH"></param>
        /// <param name="fileList"></param>
        /// <returns></returns>
        bool DelList(string LSZH, List<string> fileList);
        /// <summary>
        /// 根据律师证号删除所有资质文件
        /// </summary>
        /// <param name="LSZH"></param>
        /// <returns></returns>
        bool DelAll(string LSZH);
        /// <summary>
        /// 根据律师证号获取律师所有资质文件
        /// </summary>
        /// <param name="LSZH"></param>
        /// <returns></returns>
        List<string> GetList(string LSZH);
        #endregion


        #region  MethodEx
        #endregion
    }
}
