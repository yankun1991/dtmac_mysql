using System;
using System.Data;
using System.Collections.Generic;
using DTcms.Common;

namespace DTcms.Web.Plugin.FeedbackPlus.BLL
{
	/// <summary>
	/// ��������
	/// </summary>
    public partial class feedbackplus
    {
        private readonly DTcms.Model.siteconfig siteConfig = new DTcms.BLL.siteconfig().loadConfig(); //���վ��������Ϣ
        private readonly DAL.feedbackplus dal;
        public feedbackplus()
        {
            dal = new DAL.feedbackplus(siteConfig.sysdatabaseprefix);
        }

        #region  Method
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Model.feedbackplus model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// �޸�һ������
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            dal.UpdateField(id, strValue);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(Model.feedbackplus model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool Delete(int id)
        {
            return dal.Delete(id);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.feedbackplus GetModel(int id)
        {
            return dal.GetModel(id);
        }
        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public DataSet GetList(int Top, string strWhere)
        {
            return dal.GetList(Top, strWhere);
        }

        /// <summary>
        /// ��ò�ѯ��ҳ����
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        #endregion  Method
    }

    /// <summary>
    /// �����ļ�
    /// </summary>
    public partial class install
    {
        private readonly DAL.install dal;

        public install()
        {
            dal = new DAL.install();
        }

        /// <summary>
        ///  ��ȡ�����ļ�
        /// </summary>
        public Model.install loadConfig(string config_path)
        {
            string cacheName = "gs_cache_feedbackplus_config";
            Model.install model = CacheHelper.Get<Model.install>(cacheName);
            if (model == null)
            {
                CacheHelper.Insert(cacheName, dal.loadConfig(Utils.GetMapPath(config_path)), Utils.GetMapPath(config_path));
                model = CacheHelper.Get<Model.install>(cacheName);
            }
            return model;
        }

        /// <summary>
        ///  ���������ļ�
        /// </summary>
        public Model.install saveConifg(Model.install model, string config_path)
        {
            return dal.saveConifg(model, Utils.GetMapPath(config_path));
        }
    }
}

