using System;
using System.Data;
namespace EDRS.IDAL
{
	/// <summary>
	/// �ӿڲ�IYX_DZJZ_LSYJXZSQ ��ժҪ˵����
	/// </summary>
	public interface IYX_DZJZ_LSYJXZSQ
	{
		#region  ��Ա����
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		bool Exists(string SQBM);
		/// <summary>
		/// ����һ������
		/// </summary>
		bool Add(EDRS.Model.YX_DZJZ_LSYJXZSQ model);
		/// <summary>
		/// ���Ӷ�������
		/// </summary>
		bool AddList(System.Collections.Generic.List<EDRS.Model.YX_DZJZ_LSYJXZSQ> models);
		/// <summary>
		/// ����һ������
		/// </summary>
		bool Update(EDRS.Model.YX_DZJZ_LSYJXZSQ model);
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		bool Delete(string SQBM);
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		bool DeleteList(string SQBMlist);
        /// <summary>
        /// ɾ����������
        /// </summary>
        bool DeleteListBySFSC(string SQBMlist);
		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		EDRS.Model.YX_DZJZ_LSYJXZSQ GetModel(string SQBM);
		/// <summary>
		/// ��������б�
		/// </summary>
		DataSet GetList(string strWhere,params object[] objValues);
		/// <summary>
		/// ��������б�
		/// </summary>
		int GetRecordCount(string strWhere,params object[] objValues);
		/// <summary>
		/// ��ҳ��ȡ�����б�
		/// </summary>
		DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex,params object[] objValues);
		/// <summary>
		/// ���ݷ�ҳ��������б�
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  ��Ա����
	}
}
