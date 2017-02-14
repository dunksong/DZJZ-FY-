/**********************监督案件**************************/
var JdCase = {
    createNew: function () {
        var jdCase = {};
        jdCase.AJBH = "";
        jdCase.AJMC = "";
        jdCase.CBDW_BM = "";
        jdCase.CBDW_MC = "";
        jdCase.CBBM_BM = "";
        jdCase.CBBM_MC = "";
        jdCase.CBRGH = "";
        jdCase.CBR = "";
        jdCase.YSY = "";
        jdCase.ZBDW_BM = "";
        jdCase.ZBDW_MC = "";
        jdCase.ZBBM_BM = "";
        jdCase.ZBBM_MC = "";
        jdCase.ZBMJ = "";
        jdCase.TQPBAY_DMS = "";
        jdCase.TQPBAY_MCS = "";
        jdCase.TQPBWH = "";
        jdCase.AQZY = "";
        jdCase.FZXYR = "";
        jdCase.TQPBSJ = "";
        jdCase.TQPBSJ_CN = "";
        jdCase.CopyData = function (data) {
            if (!data || data == null) data = JdCase.createNew();
            jdCase.AJBH = data.AJBH;
            jdCase.AJMC = data.AJMC;
            jdCase.CBDW_BM = data.CBDW_BM;
            jdCase.CBBM_MC = data.CBDW_MC;
            jdCase.CBBM_BM = data.CBBM_BM;
            jdCase.CBBM_MC = data.CBBM_MC;
            jdCase.CBRGH = data.CBRGH;
            jdCase.CBR = data.CBR;
            jdCase.YSY = data.YSY;
            jdCase.ZBDW_BM = data.ZBDW_BM;
            jdCase.ZBBM_MC = data.ZBBM_MC;
            jdCase.ZBBM_BM = data.ZBBM_BM;
            jdCase.ZBBM_MC = data.ZBBM_MC;
            jdCase.ZBMJ = data.ZBMJ;
            jdCase.TQPBAY_DMS = data.TQPBAY_DMS;
            jdCase.TQPBWH = data.TQPBWH;
            jdCase.TQPBSJ = data.TQPBSJ;
            jdCase.TQPBSJ_CN = data.TQPBSJ_CN;
            jdCase.AQZY = data.AQZY;
            jdCase.FZXYR = data.FZXYR;
        };
        return jdCase;
    }
}


/*******************************考评质量**********************************/
var CaseQuality = {
    createNew: function () {
        var caseQuality = {};
        caseQuality.AJBH = "";
        caseQuality.KPZBBH = "";
        caseQuality.KPZBNR = "";
        caseQuality.KPFS = "";
        return caseQuality;
    }
}

/*******************************文书模板信息**********************************/
var DocTemplateModel = {
    createNew: function () {
        var docTemplateModel = {};
        docTemplateModel.WSMBBH = "";
        docTemplateModel.WSMBMC = "";
        docTemplateModel.WJMBLJ = "";
        docTemplateModel.WSJH = "";
        return docTemplateModel;
    }
}

/*******************************文书信息**********************************/
var DocInfoModel = {
    createNew: function () {
        var docInfoModel = {};
        docInfoModel.AJBH = "";
        docInfoModel.WJBH = "";
        docInfoModel.DWBM = "";
        docInfoModel.FWJBH = "";
        docInfoModel.WJMC = "";
        docInfoModel.WJLJ = "";
        docInfoModel.WJWH = "";
        docInfoModel.WJLX = "";
        docInfoModel.WJBZ = "";
        docInfoModel.STATE = "";
        docInfoModel.TagData = "";
        docInfoModel.DocTemplate = DocTemplateModel.createNew();
        return docInfoModel;
    }
}

/*******************************流程节点信息**********************************/
var LcsljdModel = {
    createNew: function () {
        var lcsljdModel = {};
        lcsljdModel.AJBH = "";
        lcsljdModel.LCSLJDBH = "";
        lcsljdModel.DWBM = "";
        lcsljdModel.LCMBBM = "";
        lcsljdModel.LCJDBM = "";
        lcsljdModel.LCJDMC = "";
        lcsljdModel.JDLX = "";
        lcsljdModel.FJDBH = "";
        lcsljdModel.JDJRSJ = "";
        lcsljdModel.JDLKSJ = "";
        lcsljdModel.JDZXZGH = "";
        lcsljdModel.JDZXZ = "";
        lcsljdModel.JDZXZT = "";
        lcsljdModel.JDJRYY = "";
        lcsljdModel.SFYXLK = "";
        lcsljdModel.LCSLBH = "";
        return lcsljdModel;
    }
}

/*******************************文件操作界面基本信息**********************************/
var CaseInformation = {
    createNew: function () {
        var caseInformation = {};
        caseInformation.Case = JdCase.createNew();
        caseInformation.Doc = DocInfoModel.createNew();
        caseInformation.LC = LcsljdModel.createNew();
        return caseInformation;
    }
}