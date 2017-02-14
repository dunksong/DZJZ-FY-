using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebUI.Handler.ZZJG;

namespace WebUI
{
    public partial class ReportCenter : BasePage
    {
        public string options = "";
        DZJZ_Report bll = new DZJZ_Report();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string action = Request["action"];
                string type = Request["type"];
                DataSet ds;
                switch (action)
                {
                    case "makecase":
                        ds = GetMakeCaseData();
                        if (type == "bar")
                        {
                            InitMakeCaseBar(ds);
                        }
                        else if (type == "pie")
                        {
                            InitMakeCasePie(ds);
                        }
                        break;
                    case "caseunitreport":
                        ds = GetUnitCaseData();
                        InitUnitCaseBar(ds);
                        break;
                    case "caseByMouth":
                        ds = GetCaseByMouth();
                        InitMouthCaseBar(ds);
                        break;
                    case "business":
                        ds = GetBusinessReport();
                        InitBusinessReport(ds);
                        break;
                }
            }
        }

        private void InitBusinessReport(DataSet ds)
        {
            //stack: '总量',
            //itemStyle: { normal: { label: { show: true, position: 'insideTop'}} },
            string ajdata = @"{
                        'name': '" + ((VersionName)0).ToString()+"总数" + @"',
                        stack: '总量',
                        itemStyle: { normal: { label: { show: true, position: 'insideTop'}} },
                        'type': 'bar', 
                        'data': [";
            string isregard = @"{
                        'name': '" + "已制作数" + @"',
                        stack: '总量',
                        itemStyle: { normal: { label: { show: true, position: 'insideTop'}} },
                        'type': 'bar', 
                        'data': [";
            string jdata = @"{
                        'name': '" + "卷" + @"',
                        stack: '总量',
                        itemStyle: { normal: { label: { show: true, position: 'insideTop'}} },
                        'type': 'bar', 
                        'data': [";
            string mdata = @"{
                        'name': '" + "目录" + @"',
                        stack: '总量',
                        itemStyle: { normal: { label: { show: true, position: 'insideTop'}} },
                        'type': 'bar', 
                        'data': [";
            string wdata = @"{
                        'name': '" + "文件" + @"',
                        stack: '总量',
                        itemStyle: { normal: { label: { show: true, position: 'insideTop'}} },
                        'type': 'bar', 
                        'data': [";
            string pageData = @"{
                        'name': '" + "文件页" + @"',
                        stack: '总量',
                        itemStyle: { normal: { label: { show: true, position: 'insideTop'}} },
                        'type': 'bar', 
                        'data': [";
            //stack: '总量',
            //itemStyle: { normal: { label: { show: true, position: 'insideTop'}} },
            string cols = "";
            string columns = "['"+((VersionName)0).ToString()+"总数','已制作数','卷', '目录','文件','文件页']";
            string series = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];

                if (jdata.Substring(jdata.Length - 1, 1) != "[")
                {
                    cols += ",";
                    ajdata += ",";
                    jdata += ",";
                    isregard += ",";
                    mdata += ",";
                    wdata += ",";
                    pageData += ",";
                }
                if ((i + 1) % 2 == 0)
                {
                    cols += "'\\n" + dr["YWMC"].ToString() + "" + "'";//换行显示
                }
                else
                {
                    cols += "'" + dr["YWMC"] + "" + "'";
                }
                ajdata += Convert.ToInt32(dr["AJCOUNT"] == DBNull.Value ? 0 : dr["AJCOUNT"]);
                isregard += Convert.ToInt32(dr["ISREGARD"] == DBNull.Value ? 0 : dr["ISREGARD"]);
                jdata += Convert.ToInt32(dr["JCOUNT"] == DBNull.Value ? 0 : dr["JCOUNT"]);
                //mdata += Convert.ToInt32(dr["MCOUNT"] == DBNull.Value ? 0 : dr["MCOUNT"]);
                wdata += Convert.ToInt32(dr["WCOUNT"] == DBNull.Value ? 0 : dr["WCOUNT"]);
                pageData += Convert.ToInt32(dr["PageCount"] == DBNull.Value ? 0 : dr["PageCount"]);
            }
            ajdata += @"]
                        }";
            isregard += @"]
                        }";
            jdata += @"]
                        }";
            mdata += @"]
                        }";
            wdata += @"]
                        }";
            pageData += @"]
                        }";
            cols = "[" + cols + "]";

            series = "[" + ajdata + "," + isregard + "," + jdata + "," + mdata + "," + wdata + "," + pageData + "]";
            //            title: { text: '卷宗制作工作量统计', subtext: '按人员' }, 
            options = @"{
            calculable: true,
            tooltip: {
                trigger: 'axis',
                axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                    type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                }
            },
            legend: {
                data: " + columns + @"
            },
            xAxis: [
                {
                    type: 'category',
                    'axisLabel':{'interval':0},
                    data: " + cols + @"
                }
            ],
            yAxis: [
                {
                    type: 'value'
                }
            ],
            series: " + series + @"
        }";
        }

        private DataSet GetBusinessReport()
        {
            string strWhere = "";
            List<object> values = new List<object>();
            string pageIndex = Request["page"];
            string pageSize = Request["pagesize"];
            string unit = Request["unit"];//承办单位
            string timebegin = Request["timebegin"];//开始日期
            string timeend = Request["timeend"];//结束日期
            string count_start = Request["count_start"];
            string count_end = Request["count_end"];
            if (!string.IsNullOrEmpty(unit))
            {
                strWhere += " AND CBDW_MC LIKE '%" + unit + "%'";
            }
            if (!string.IsNullOrEmpty(timebegin))
            {
                strWhere += " and CJSJ >= to_date('" + Convert.ToDateTime(timebegin).ToShortDateString() + "','yyyy-mm-dd')";
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                strWhere += " and CJSJ <= to_date('" + Convert.ToDateTime(timeend).AddDays(1).ToShortDateString() + "','yyyy-mm-dd')";
            }
            string qxWhere = " AND DWBM = '" + UserInfo.DWBM + "' AND JSBM in ("+Jsbms+") AND BMBM in ("+Bmbms+") AND QXLX = 0";
            string havingWhere = " HAVING 1=1 ";
            if (Convert.ToInt32(count_start) > 0)
            {
                havingWhere += " AND SUM(CASE WHEN ISREGARD > 0 THEN 1 ELSE 0 END) >=" + count_start;
            }
            if (Convert.ToInt32(count_end) > 0)
            {
                havingWhere += " AND SUM(CASE WHEN ISREGARD > 0 THEN 1 ELSE 0 END) <=" + count_end;
            }
            EDRS.BLL.EDRS_Report bll = new EDRS.BLL.EDRS_Report(Request);
            int count = 0;
            DataSet ds = bll.GetCaseBusinessReport(strWhere, havingWhere, qxWhere, Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex), "", out count, values.ToArray());
            if (ds == null)
            {
                OperateLog.AddLog(OperateLog.LogType.卷宗业务情况统计Web, "查询卷宗业务情况统计图表数据失败！", UserInfo, UserRole, Request);
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.卷宗业务情况统计Web, "查询卷宗业务情况统计图表数据成功！", UserInfo, UserRole, Request);
            }
            return ds;
        }

        private void InitMouthCaseBar(DataSet ds)
        {
                        //stack: '总量',
                        //itemStyle: { normal: { label: { show: true, position: 'insideTop'}} },
            string ajdata = @"{
                        'name': '" + ((VersionName)0).ToString()+"数量" + @"',
                        'type': 'bar', 
                        'data': [";
            string jdata = @"{
                        'name': '" + "卷" + @"',
                        'type': 'bar', 
                        'data': [";
            string mdata = @"{
                        'name': '" + "目录" + @"',
                        'type': 'bar', 
                        'data': [";
            string wdata = @"{
                        'name': '" + "文件" + @"',
                        'type': 'bar', 
                        'data': [";
            string pageData = @"{
                        'name': '" + "文件页" + @"',
                        'type': 'bar', 
                        'data': [";
            //stack: '总量',
            //itemStyle: { normal: { label: { show: true, position: 'insideTop'}} },
            string cols = "";
            //string columns = "['案件数量','卷', '目录','文件','文件页']";

            string columns = "['"+((VersionName)0).ToString()+"数量','卷','文件','文件页']";
            string series = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];

                if (jdata.Substring(jdata.Length - 1, 1) != "[")
                {
                    cols += ",";
                    ajdata += ",";
                    jdata += ",";
                    mdata += ",";
                    wdata += ",";
                    pageData += ",";
                }
                if ((i + 1) % 2 == 0)
                {
                    cols += "'\\n" + dr["MM"].ToString().Replace("-","年") + "月" + "'";//换行显示
                }
                else
                {
                    cols += "'" + dr["MM"].ToString().Replace("-","年") + "月" + "'";
                }
                ajdata += Convert.ToInt32(dr["AJCOUNT"] == DBNull.Value ? 0 : dr["AJCOUNT"]);
                jdata += Convert.ToInt32(dr["JCOUNT"] == DBNull.Value ? 0 : dr["JCOUNT"]);
             //   mdata += Convert.ToInt32(dr["MCOUNT"] == DBNull.Value ? 0 : dr["MCOUNT"]);
                wdata += Convert.ToInt32(dr["WCOUNT"] == DBNull.Value ? 0 : dr["WCOUNT"]);
                pageData += Convert.ToInt32(dr["PageCount"] == DBNull.Value ? 0 : dr["PageCount"]);
            }
            ajdata += @"]
                        }";
            jdata += @"]
                        }";
            mdata += @"]
                        }";
            wdata += @"]
                        }";
            pageData += @"]
                        }";
            cols = "[" + cols + "]";

            //series = "[" + ajdata + "," + jdata + "," + mdata + "," + wdata + "," + pageData + "]";

            series = "[" + ajdata + "," + jdata + "," + wdata + "," + pageData + "]";
            //            title: { text: '卷宗制作工作量统计', subtext: '按人员' }, 
            options = @"{
            calculable: true,
            tooltip: {
                trigger: 'axis',
                axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                    type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                }
            },
            legend: {
                data: " + columns + @"
            },
            xAxis: [
                {
                    type: 'category',
                    'axisLabel':{'interval':0},
                    data: " + cols + @"
                }
            ],
            yAxis: [
                {
                    type: 'value'
                }
            ],
            series: " + series + @"
        }";
        }
        private DataSet GetCaseByMouth()
        {
            string where = "";
            List<object> values = new List<object>();
            string unit = Request["unit"];//承办单位
            string timebegin = Request["timebegin"];//开始日期
            string timeEnd = Request["timeend"];//结束日期
            if (!string.IsNullOrEmpty(unit))
            {
                where += " and CBDW_BM in (";
                where += "" + unit.Replace(";",",") +"";
                where += ")";
            }
            //strWhere += " and (select count(1) from yx_dzjz_jzml y where y.Bmsah=T1.Bmsah) > 0";//已关联
            if (!string.IsNullOrEmpty(timebegin))
            {
                where += " and SLRQ >= to_date('" + Convert.ToDateTime(timebegin).ToShortDateString() + "','yyyy-mm-dd')";
            }
            if (!string.IsNullOrEmpty(timeEnd))
            {
                where += " and SLRQ < to_date('" + Convert.ToDateTime(timeEnd).AddDays(1).ToShortDateString() + "','yyyy-mm-dd')";
            }

            //单位权限
            //List<string> unitRoleList = GetDwBm(UserRole.DWBM, UserRole.BMBM, UserRole.JSBM);

            EDRS.BLL.EDRS_Report bll = new EDRS.BLL.EDRS_Report(Request);
            string qxWhere = " AND DWBM = '" + UserInfo.DWBM + "' AND JSBM in (" + Jsbms + ") AND BMBM in ("+Bmbms+") AND QXLX = 0";
            DataSet ds = bll.GetCaseGroupMouth(where, qxWhere, "mm",values.ToArray());

            if (ds == null)
            {
                OperateLog.AddLog(OperateLog.LogType.卷宗月统计图Web, "查询卷宗月统计报表图表数据失败！", UserInfo, UserRole, Request);
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.卷宗月统计图Web, "查询卷宗月统计报表图表数据成功！", UserInfo, UserRole, Request);
            }
            return ds;
        }
        private void InitUnitCaseBar(DataSet ds)
        {
            string ajdata = @"{
                        'name': '" + ((VersionName)0).ToString() + @"',
                        'type': 'bar', 
                        stack: '总量',
                        itemStyle: { normal: { label: { show: true, position: 'insideTop'}} },
                        'data': [";
            string jdata = @"{
                        'name': '" + "卷" + @"',
                        'type': 'bar', 
                        stack: '总量',
                        itemStyle: { normal: { label: { show: true, position: 'insideTop'}} },
                        'data': [";
            string mdata = @"{
                        'name': '" + "目录" + @"',
                        'type': 'bar', 
                        stack: '总量',
                        itemStyle: { normal: { label: { show: true, position: 'insideTop'}} },
                        'data': [";
            string wdata = @"{
                        'name': '" + "文件" + @"',
                        'type': 'bar', 
                        stack: '总量',
                        itemStyle: { normal: { label: { show: true, position: 'insideTop'}} },
                        'data': [";
            string pageData = @"{
                        'name': '" + "文件页" + @"',
                        'type': 'bar', 
                        stack: '总量',
                        itemStyle: { normal: { label: { show: true, position: 'insideTop'}} },
                        'data': [";
            //stack: '总量',
            //itemStyle: { normal: { label: { show: true, position: 'insideTop'}} },
            string cols = "";
            string columns = "['"+((VersionName)0).ToString()+"','卷', '目录','文件','文件页']";
            string series = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count;i++ )
            {
                DataRow dr = ds.Tables[0].Rows[i];
                if (jdata.Substring(jdata.Length - 1, 1) != "[")
                {
                    cols += ",";
                    ajdata += ",";
                    jdata += ",";
                    mdata += ",";
                    wdata += ",";
                    pageData += ",";
                }
                if ((i + 1) % 2 == 0)
                {
                    cols += "'\\n" + dr["CBDW_MC"].ToString() + "'";//换行显示
                }
                else
                {
                    cols += "'" + dr["CBDW_MC"].ToString() + "'";
                }
                ajdata += Convert.ToInt32(dr["AJCOUNT"] == DBNull.Value ? 0 : dr["AJCOUNT"]);
                jdata += Convert.ToInt32(dr["JCOUNT"] == DBNull.Value ? 0 : dr["JCOUNT"]);
                //mdata += Convert.ToInt32(dr["MCOUNT"] == DBNull.Value ? 0 : dr["MCOUNT"]);
                wdata += Convert.ToInt32(dr["WCOUNT"] == DBNull.Value ? 0 : dr["WCOUNT"]);
                pageData += Convert.ToInt32(dr["PageCount"] == DBNull.Value ? 0 : dr["PageCount"]);
            }
            ajdata += @"]
                        }";
            jdata += @"]
                        }";
            mdata += @"]
                        }";
            wdata += @"]
                        }";
            pageData += @"]
                        }";
            cols = "[" + cols + "]";

            series = "[" + ajdata + "," + jdata + "," + mdata + "," + wdata + "," + pageData + "]";
            //            title: { text: '卷宗制作工作量统计', subtext: '按人员' }, 
            options = @"{
            calculable: true,
            tooltip: {
                trigger: 'axis',
                axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                    type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                }
            },
            legend: {
                data: " + columns + @"
            },
            xAxis: [
                {
                    type: 'category',
                    'axisLabel':{'interval':0},
                    data: " + cols + @"
                }
            ],
            yAxis: [
                {
                    type: 'value'
                }
            ],
            series: " + series + @"
        }";
        }

        private void InitMakeCasePie(DataSet ds)
        {
            string legend = "";
            string series = "";
            for ( int i= 0;i<ds.Tables[0].Rows.Count;i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                if (i != 0)
                {
                    legend += ",";
                    series += ",";
                }
                legend += "'" + dr["JZSCRXM"].ToString() + "'";
                series += "{value:" + Convert.ToInt32(dr["PageCount"] == DBNull.Value ? 0 : dr["PageCount"]) + ",name:'" + dr["JZSCRXM"].ToString() + "'}";
            }

            options = @"{
                                title : {
                                    text: '卷宗制作工作量统计',
                                    subtext: '按人员',
                                    x:'center'
                                },
                                tooltip : {
                                    trigger: 'item',
                                    formatter: '{a} <br/>{b} : {c}({d}%)'
                                },
                                legend: {
                                    orient : 'vertical',
                                    x : 'left',
                                    data:[" + legend + @"]
                                },
                                calculable : true,
                                series : [
                                    {
                                        name:'工作量(页)',
                                        type:'pie',
                                        radius : '55%',
                                        center: ['50%', '60%'],
                                        data:[
                                           " + series + @"
                                        ]
                                    }
                                ]
                            }
                    ";
        }

        private void InitMakeCaseBar(DataSet ds)
        {
            string ajdata = @"{
                        'name': '" + ((VersionName)0).ToString()+"数量" + @"',
                        'type': 'bar', 
                        stack: '总量',
                        itemStyle: { normal: { label: { show: true, position: 'insideTop'}} },
                        'data': [";
            string jdata = @"{
                        'name': '" + "卷" + @"',
                        'type': 'bar', 
                        stack: '总量',
                        itemStyle: { normal: { label: { show: true, position: 'insideTop'}} },
                        'data': [";
            string mdata = @"{
                        'name': '" + "目录" + @"',
                        'type': 'bar', 
                        stack: '总量',
                        itemStyle: { normal: { label: { show: true, position: 'insideTop'}} },
                        'data': [";
            string wdata = @"{
                        'name': '" + "文件" + @"',
                        'type': 'bar', 
                        stack: '总量',
                        itemStyle: { normal: { label: { show: true, position: 'insideTop'}} },
                        'data': [";
            string pageData = @"{
                        'name': '" + "文件页" + @"',
                        'type': 'bar', 
                        stack: '总量',
                        itemStyle: { normal: { label: { show: true, position: 'insideTop'}} },
                        'data': [";
            //stack: '总量',
            //itemStyle: { normal: { label: { show: true, position: 'insideTop'}} },
            string cols = "";
            string columns = "['"+((VersionName)0).ToString()+"数量','卷', '目录','文件','文件页']";
            string series = "";
            for(int i=0;i<ds.Tables[0].Rows.Count;i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
            
                if (jdata.Substring(jdata.Length - 1, 1) != "[")
                {
                    cols += ",";
                    ajdata += ",";
                    jdata += ",";
                    mdata += ",";
                    wdata += ",";
                    pageData += ",";
                }
                if ((i + 1) % 2 == 0)
                {
                    cols += "'\\n" + dr["JZSCRXM"].ToString() + "'";//换行显示
                }
                else
                {
                    cols += "'" + dr["JZSCRXM"].ToString() + "'";
                }
                ajdata += Convert.ToInt32(dr["AJCOUNT"] == DBNull.Value ? 0 : dr["AJCOUNT"]);
                jdata += Convert.ToInt32(dr["JCOUNT"] == DBNull.Value ? 0 : dr["JCOUNT"]);
                //mdata += Convert.ToInt32(dr["MCOUNT"] == DBNull.Value ? 0 : dr["MCOUNT"]);
                wdata += Convert.ToInt32(dr["WCOUNT"] == DBNull.Value ? 0 : dr["WCOUNT"]);
                pageData += Convert.ToInt32(dr["PageCount"] == DBNull.Value ? 0 : dr["PageCount"]);
            }
            ajdata += @"]
                        }";
            jdata += @"]
                        }";
            mdata += @"]
                        }";
            wdata += @"]
                        }";
            pageData += @"]
                        }";
            cols = "[" + cols + "]";

            series = "[" + ajdata + "," + jdata + "," + mdata + "," + wdata + "," + pageData + "]";
            //            title: { text: '卷宗制作工作量统计', subtext: '按人员' }, 
            options = @"{
            calculable: true,
            tooltip: {
                trigger: 'axis',
                axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                    type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                }
            },
            legend: {
                data: " + columns + @"
            },
            xAxis: [
                {
                    type: 'category',
                    'axisLabel':{'interval':0},
                    data: " + cols + @"
                }
            ],
            yAxis: [
                {
                    type: 'value'
                }
            ],
            series: " + series + @"
        }";
        }
        private DataSet GetUnitCaseData()
        {
            string where = "";
            List<object> values = new List<object>();
            string pageIndex = Request["page"];
            string pageSize = Request["pagesize"];
            string key = Request["key"];//部门受案号
            string unit = Request["unit"];//承办单位
            string timebegin = Request["timebegin"];//开始日期
            string timeend = Request["timeend"];//结束日期
            if (!string.IsNullOrEmpty(unit))
            {
                where += " and CBDW_MC like ";
                where += "'%" + unit + "%'";
            }
            //strWhere += " and (select count(1) from yx_dzjz_jzml y where y.Bmsah=T1.Bmsah) > 0";//已关联
            if (!string.IsNullOrEmpty(timebegin))
            {
                where += " and SLRQ >= to_date('" + Convert.ToDateTime(timebegin).ToShortDateString() + "','yyyy-mm-dd')";
            }
            if (!string.IsNullOrEmpty(timeend))
            {
                where += " and SLRQ <= to_date('" + Convert.ToDateTime(timeend).AddDays(1).ToShortDateString() + "','yyyy-mm-dd')";
            }
            
            //单位权限
            //List<string> unitRoleList = GetDwBm(UserRole.DWBM, UserRole.BMBM, UserRole.JSBM);
            string qxWhere = " AND DWBM = '" + UserInfo.DWBM + "' AND JSBM in (" + Jsbms + ") AND BMBM in ("+Bmbms+") AND QXLX = 0";
            EDRS.BLL.EDRS_Report bll = new EDRS.BLL.EDRS_Report(Request);
            int count;
            //1-8 修改存储过程
            DataSet ds = bll.GetCaseGroupByUnit(where,UserInfo.DWBM, UserInfo.GH, Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex), "", out count, values.ToArray());

            if (ds == null)
            {
                OperateLog.AddLog(OperateLog.LogType.单位卷宗统计Web, "查询单位卷宗统计图表数据失败！", UserInfo, UserRole, Request);
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.单位卷宗统计Web, "查询单位卷宗统计图表数据成功！", UserInfo, UserRole, Request);
            }
            return ds;
        }
        private DataSet GetMakeCaseData()
        {
            string pageIndex = Request["page"];
            string pageSize = Request["pagesize"];
            //where
            string b_date = Request["b_date"];
            string e_date = Request["e_date"];
            string username = Request["username"];
            string unit = Request["txt_unit"];
            string where = "";
            List<object> values = new List<object>();
            if (!string.IsNullOrEmpty(b_date))
            {
                //JZ.Cjsj（创建时间） / JZ.JZSCSJ（上传时间）
                where += " and JZ.Cjsj >= to_date('" + Convert.ToDateTime(b_date).ToShortDateString() + "','yyyy-mm-dd')";
            }
            if (!string.IsNullOrEmpty(e_date))
            {
                where += " and JZ.Cjsj <= to_date('" + Convert.ToDateTime(e_date).AddDays(1).ToShortDateString() + "','yyyy-mm-dd')";

            }
            if (!string.IsNullOrEmpty(username))
            {
                where += " and JZ.JZSCRXM LIKE ";
                where += "'%" + username + "%'";
            }
            if (!string.IsNullOrEmpty(unit))
            {
                where += " and CBDW_MC LIKE ";
                where += "'%" + unit + "%'";
            }
            //单位权限
            //List<string> unitRoleList = GetDwBm(UserRole.DWBM, UserRole.BMBM, UserRole.JSBM);
            
            string qxWhere = " AND DWBM = '" + UserInfo.DWBM + "' AND JSBM in (" + Jsbms + ") AND BMBM in (" + Bmbms + ") AND QXLX = 0";

            EDRS.BLL.EDRS_Report bll = new EDRS.BLL.EDRS_Report(Request);
            int count;
            DataSet ds = bll.GetCaseByPerson(where, qxWhere, Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex), "", out count, values.ToArray());

            if (ds == null)
            {
                OperateLog.AddLog(OperateLog.LogType.卷宗制作工作量统计Web, "查询卷宗制作工作量统计图表数据失败！", UserInfo, UserRole, Request);
            }
            else
            {
                OperateLog.AddLog(OperateLog.LogType.卷宗制作工作量统计Web, "查询卷宗制作工作量统计图表数据成功！", UserInfo, UserRole, Request);
            }
            return ds;
        }


    }
}