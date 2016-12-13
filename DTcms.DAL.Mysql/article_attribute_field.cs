using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL.Mysql
{
    /// <summary>
    /// ��չ�������ݷ�����:article_attribute_field
    /// </summary>
    public partial class article_attribute_field
    {
        private string databaseprefix; //���ݿ����ǰ׺
        public article_attribute_field(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region ��������========================
        /// <summary>
        /// �õ����ID
        /// </summary>
        private int GetMaxId(MySqlConnection conn, MySqlTransaction trans)
        {
            string strSql = "select id from " + databaseprefix + "article_attribute_field order by id desc limit 1";
            object obj = DbHelperMySql.GetSingle(conn, trans, strSql);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "article_attribute_field");
            strSql.Append(" where id=@id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            return DbHelperMySql.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// ��ѯ�Ƿ������
        /// </summary>
        public bool Exists(string column_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "article_attribute_field");
            strSql.Append(" where name=@name ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@name", MySqlDbType.VarChar,100)};
            parameters[0].Value = column_name;

            if (DbHelperMySql.Exists(strSql.ToString(), parameters) || DbHelperMySql.ExitColumnName(databaseprefix + "article", column_name))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Model.article_attribute_field model)
        {
            int newId;
            using (MySqlConnection conn = new MySqlConnection(DbHelperMySql.connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("insert into " + databaseprefix + "article_attribute_field(");
                        strSql.Append("name,title,control_type,data_type,data_length,data_place,item_option,default_value,is_required,is_password,is_html,editor_type,valid_tip_msg,valid_error_msg,valid_pattern,sort_id,is_sys)");
                        strSql.Append(" values (");
                        strSql.Append("@name,@title,@control_type,@data_type,@data_length,@data_place,@item_option,@default_value,@is_required,@is_password,@is_html,@editor_type,@valid_tip_msg,@valid_error_msg,@valid_pattern,@sort_id,@is_sys)");
                        MySqlParameter[] parameters = {
					            new MySqlParameter("@name", MySqlDbType.VarChar,100),
					            new MySqlParameter("@title", MySqlDbType.VarChar,100),
					            new MySqlParameter("@control_type", MySqlDbType.VarChar,50),
					            new MySqlParameter("@data_type", MySqlDbType.VarChar,50),
					            new MySqlParameter("@data_length", MySqlDbType.Int32,4),
					            new MySqlParameter("@data_place", MySqlDbType.Int32,4),
					            new MySqlParameter("@item_option", MySqlDbType.VarChar),
					            new MySqlParameter("@default_value", MySqlDbType.VarChar),
					            new MySqlParameter("@is_required", MySqlDbType.Int32,4),
					            new MySqlParameter("@is_password", MySqlDbType.Int32,4),
					            new MySqlParameter("@is_html", MySqlDbType.Int32,4),
					            new MySqlParameter("@editor_type", MySqlDbType.Int32,4),
					            new MySqlParameter("@valid_tip_msg", MySqlDbType.VarChar,255),
					            new MySqlParameter("@valid_error_msg", MySqlDbType.VarChar,255),
					            new MySqlParameter("@valid_pattern", MySqlDbType.VarChar,500),
					            new MySqlParameter("@sort_id", MySqlDbType.Int32,4),
					            new MySqlParameter("@is_sys", MySqlDbType.Int32,4)};
                        parameters[0].Value = model.name;
                        parameters[1].Value = model.title;
                        parameters[2].Value = model.control_type;
                        parameters[3].Value = model.data_type;
                        parameters[4].Value = model.data_length;
                        parameters[5].Value = model.data_place;
                        parameters[6].Value = model.item_option;
                        parameters[7].Value = model.default_value;
                        parameters[8].Value = model.is_required;
                        parameters[9].Value = model.is_password;
                        parameters[10].Value = model.is_html;
                        parameters[11].Value = model.editor_type;
                        parameters[12].Value = model.valid_tip_msg;
                        parameters[13].Value = model.valid_error_msg;
                        parameters[14].Value = model.valid_pattern;
                        parameters[15].Value = model.sort_id;
                        parameters[16].Value = model.is_sys;

                        DbHelperMySql.ExecuteSql(conn, trans, strSql.ToString(), parameters);
                        //ȡ���²����ID
                        newId = GetMaxId(conn, trans);
                        //������չ�ֶα���һ��
                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("alter table " + databaseprefix + "article_attribute_value add " + model.name + " " + model.data_type);
                        MySqlParameter[] parameters2 = { };
                        DbHelperMySql.ExecuteSql(conn, trans, strSql2.ToString(), parameters2);
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return -1;
                    }
                }
            }
            return newId;
        }

        /// <summary>
        /// �޸�һ������
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "article_attribute_field set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperMySql.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(Model.article_attribute_field model)
        {
            using (MySqlConnection conn = new MySqlConnection(DbHelperMySql.connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update " + databaseprefix + "article_attribute_field set ");
                        strSql.Append("name=@name,");
                        strSql.Append("title=@title,");
                        strSql.Append("control_type=@control_type,");
                        strSql.Append("data_type=@data_type,");
                        strSql.Append("data_length=@data_length,");
                        strSql.Append("data_place=@data_place,");
                        strSql.Append("item_option=@item_option,");
                        strSql.Append("default_value=@default_value,");
                        strSql.Append("is_required=@is_required,");
                        strSql.Append("is_password=@is_password,");
                        strSql.Append("is_html=@is_html,");
                        strSql.Append("editor_type=@editor_type,");
                        strSql.Append("valid_tip_msg=@valid_tip_msg,");
                        strSql.Append("valid_error_msg=@valid_error_msg,");
                        strSql.Append("valid_pattern=@valid_pattern,");
                        strSql.Append("sort_id=@sort_id,");
                        strSql.Append("is_sys=@is_sys");
                        strSql.Append(" where id=@id");
                        MySqlParameter[] parameters = {
					            new MySqlParameter("@name", MySqlDbType.VarChar,100),
					            new MySqlParameter("@title", MySqlDbType.VarChar,100),
					            new MySqlParameter("@control_type", MySqlDbType.VarChar,50),
					            new MySqlParameter("@data_type", MySqlDbType.VarChar,50),
					            new MySqlParameter("@data_length", MySqlDbType.Int32,4),
					            new MySqlParameter("@data_place", MySqlDbType.Int32,4),
					            new MySqlParameter("@item_option", MySqlDbType.VarChar),
					            new MySqlParameter("@default_value", MySqlDbType.VarChar),
					            new MySqlParameter("@is_required", MySqlDbType.Int32,4),
					            new MySqlParameter("@is_password", MySqlDbType.Int32,4),
					            new MySqlParameter("@is_html", MySqlDbType.Int32,4),
					            new MySqlParameter("@editor_type", MySqlDbType.Int32,4),
					            new MySqlParameter("@valid_tip_msg", MySqlDbType.VarChar,255),
					            new MySqlParameter("@valid_error_msg", MySqlDbType.VarChar,255),
					            new MySqlParameter("@valid_pattern", MySqlDbType.VarChar,500),
					            new MySqlParameter("@sort_id", MySqlDbType.Int32,4),
					            new MySqlParameter("@is_sys", MySqlDbType.Int32,4),
                                new MySqlParameter("@id", MySqlDbType.Int32,4)};
                        parameters[0].Value = model.name;
                        parameters[1].Value = model.title;
                        parameters[2].Value = model.control_type;
                        parameters[3].Value = model.data_type;
                        parameters[4].Value = model.data_length;
                        parameters[5].Value = model.data_place;
                        parameters[6].Value = model.item_option;
                        parameters[7].Value = model.default_value;
                        parameters[8].Value = model.is_required;
                        parameters[9].Value = model.is_password;
                        parameters[10].Value = model.is_html;
                        parameters[11].Value = model.editor_type;
                        parameters[12].Value = model.valid_tip_msg;
                        parameters[13].Value = model.valid_error_msg;
                        parameters[14].Value = model.valid_pattern;
                        parameters[15].Value = model.sort_id;
                        parameters[16].Value = model.is_sys;
                        parameters[17].Value = model.id;
                        DbHelperMySql.ExecuteSql(conn, trans, strSql.ToString(), parameters);

                        //�޸���չ�ֶα���һ��
                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("alter table " + databaseprefix + "article_attribute_value alter column " + model.name + " " + model.data_type);
                        DbHelperMySql.ExecuteSql(conn, trans, strSql2.ToString());
                        //û�д���ȷ������
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool Delete(int id)
        {
            //ȡ��Model��Ϣ
            Model.article_attribute_field model = GetModel(id);
            //��ʼɾ��
            using (MySqlConnection conn = new MySqlConnection(DbHelperMySql.connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //���ҹ�����Ƶ��ID���õ����Ա�ʹ��
                        StringBuilder strSql1 = new StringBuilder();
                        strSql1.Append("select channel_id,field_id from " + databaseprefix + "channel_field ");
                        strSql1.Append(" where field_id=@field_id");
                        MySqlParameter[] parameters1 = {
					            new MySqlParameter("@field_id", MySqlDbType.Int32,4)};
                        parameters1[0].Value = id;
                        DataTable dt = DbHelperMySql.Query(conn, trans, strSql1.ToString(), parameters1).Tables[0];

                        //ɾ��Ƶ���������ֶα�
                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("delete from " + databaseprefix + "channel_field");
                        strSql2.Append(" where field_id=@field_id");
                        MySqlParameter[] parameters2 = {
					            new MySqlParameter("@field_id", MySqlDbType.Int32,4)};
                        parameters2[0].Value = id;
                        DbHelperMySql.ExecuteSql(conn, trans, strSql2.ToString(), parameters2);

                        //�ؽ���ӦƵ������ͼ
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                Model.channel modelt = new DAL.Mysql.channel(databaseprefix).GetModel(conn, trans, int.Parse(dr["channel_id"].ToString()));
                                if (modelt != null)
                                {
                                    new DAL.Mysql.channel(databaseprefix).RehabChannelViews(conn, trans, modelt, modelt.name);
                                }
                            }
                        }

                        //ɾ������
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("delete from " + databaseprefix + "article_attribute_field ");
                        strSql.Append(" where id=@id");
                        MySqlParameter[] parameters = {
					            new MySqlParameter("@id", MySqlDbType.Int32,4)};
                        parameters[0].Value = id;
                        DbHelperMySql.ExecuteSql(conn, trans, strSql.ToString(), parameters);
                        
                        //ɾ����չ�ֶα���һ��
                        DbHelperMySql.ExecuteSql(conn, trans, "alter table " + databaseprefix + "article_attribute_value drop column " + model.name);

                        //û�д���ȷ������
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.article_attribute_field GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,name,title,control_type,data_type,data_length,data_place,item_option,default_value,is_required,is_password,is_html,editor_type,valid_tip_msg,valid_error_msg,valid_pattern,sort_id,is_sys");
            strSql.Append(" from " + databaseprefix + "article_attribute_field ");
            strSql.Append(" where id=@id");
            strSql.Append(" limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            Model.article_attribute_field model = new Model.article_attribute_field();
            DataSet ds = DbHelperMySql.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.name = ds.Tables[0].Rows[0]["name"].ToString();
                model.title = ds.Tables[0].Rows[0]["title"].ToString();
                model.control_type = ds.Tables[0].Rows[0]["control_type"].ToString();
                model.data_type = ds.Tables[0].Rows[0]["data_type"].ToString();
                if (ds.Tables[0].Rows[0]["data_length"].ToString() != "")
                {
                    model.data_length = int.Parse(ds.Tables[0].Rows[0]["data_length"].ToString());
                }
                if (ds.Tables[0].Rows[0]["data_place"].ToString() != "")
                {
                    model.data_place = int.Parse(ds.Tables[0].Rows[0]["data_place"].ToString());
                }
                model.item_option = ds.Tables[0].Rows[0]["item_option"].ToString();
                model.default_value = ds.Tables[0].Rows[0]["default_value"].ToString();
                if (ds.Tables[0].Rows[0]["is_required"].ToString() != "")
                {
                    model.is_required = int.Parse(ds.Tables[0].Rows[0]["is_required"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_password"].ToString() != "")
                {
                    model.is_password = int.Parse(ds.Tables[0].Rows[0]["is_password"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_html"].ToString() != "")
                {
                    model.is_html = int.Parse(ds.Tables[0].Rows[0]["is_html"].ToString());
                }
                if (ds.Tables[0].Rows[0]["editor_type"].ToString() != "")
                {
                    model.editor_type = int.Parse(ds.Tables[0].Rows[0]["editor_type"].ToString());
                }
                model.valid_tip_msg = ds.Tables[0].Rows[0]["valid_tip_msg"].ToString();
                model.valid_error_msg = ds.Tables[0].Rows[0]["valid_error_msg"].ToString();
                model.valid_pattern = ds.Tables[0].Rows[0]["valid_pattern"].ToString();
                if (ds.Tables[0].Rows[0]["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(ds.Tables[0].Rows[0]["sort_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_sys"].ToString() != "")
                {
                    model.is_sys = int.Parse(ds.Tables[0].Rows[0]["is_sys"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ���Ƶ����Ӧ������
        /// </summary>
        public DataSet GetList(int channel_id, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select dt_article_attribute_field.* ");
            strSql.Append(" FROM " + databaseprefix + "article_attribute_field INNER JOIN " + databaseprefix + "channel_field ON " + databaseprefix + "article_attribute_field.id = " + databaseprefix + "channel_field.field_id");
            strSql.Append(" where channel_id=" + channel_id);
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            strSql.Append(" order by sort_id asc," + databaseprefix + "article_attribute_field.id desc");
            return DbHelperMySql.Query(strSql.ToString());
        }

        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            
            strSql.Append(" id,name,title,control_type,data_type,data_length,data_place,item_option,default_value,is_required,is_password,is_html,editor_type,valid_tip_msg,valid_error_msg,valid_pattern,sort_id,is_sys ");
            strSql.Append(" FROM " + databaseprefix + "article_attribute_field ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            if (Top > 0)
            {
                strSql.Append(" limit " + Top.ToString());
            }
            return DbHelperMySql.Query(strSql.ToString());
        }

        /// <summary>
        /// ��ò�ѯ��ҳ����
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "article_attribute_field");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperMySql.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperMySql.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        #endregion  Method

        #region ��չ����========================
        /// <summary>
        /// ��ȡ��չ�ֶζԳ�ֵ
        /// </summary>
        public Dictionary<string, string> GetFields(int channel_id, int article_id, string strWhere)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            DataTable dt = GetList(channel_id, strWhere).Tables[0];
            if (dt.Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append(dr["name"].ToString() + ",");
                }
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select " + Utils.DelLastComma(sb.ToString()) + " from " + databaseprefix + "article_attribute_value ");
                strSql.Append(" where article_id=@article_id ");
                strSql.Append(" limit 1");
                MySqlParameter[] parameters = {
					    new MySqlParameter("@article_id", MySqlDbType.Int32,4)};
                parameters[0].Value = article_id;

                DataSet ds = DbHelperMySql.Query(strSql.ToString(), parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (ds.Tables[0].Rows[0][dr["name"].ToString()] != null)
                        {
                            dic.Add(dr["name"].ToString(), ds.Tables[0].Rows[0][dr["name"].ToString()].ToString());
                        }
                        else
                        {
                            dic.Add(dr["name"].ToString(), "");
                        }
                    }
                }
            }
            return dic;
        }
        #endregion
    }
}