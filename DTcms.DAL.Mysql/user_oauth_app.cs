using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL.Mysql
{
    /// <summary>
    /// ���ݷ�����:OAuthӦ��
    /// </summary>
    public partial class user_oauth_app
    {
        private string databaseprefix; //���ݿ����ǰ׺
        public user_oauth_app(string _databaseprefix)
        { 
            databaseprefix = _databaseprefix; 
        }

        #region ��������================================
        /// <summary>
        /// �õ����ID
        /// </summary>
        private int GetMaxId(MySqlConnection conn, MySqlTransaction trans)
        {
            string strSql = "select id from " + databaseprefix + "user_oauth_app order by id desc limit 1";
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
            strSql.Append("select count(1) from " + databaseprefix + "user_oauth_app");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;
            return DbHelperMySql.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Model.user_oauth_app model)
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
                        strSql.Append("insert into " + databaseprefix + "user_oauth_app(");
                        strSql.Append("title,img_url,app_id,app_key,remark,sort_id,is_lock,api_path)");
                        strSql.Append(" values (");
                        strSql.Append("@title,@img_url,@app_id,@app_key,@remark,@sort_id,@is_lock,@api_path)");
                        MySqlParameter[] parameters = {
					            new MySqlParameter("@title", MySqlDbType.VarChar,100),
					            new MySqlParameter("@img_url", MySqlDbType.VarChar,255),
					            new MySqlParameter("@app_id", MySqlDbType.VarChar,100),
					            new MySqlParameter("@app_key", MySqlDbType.VarChar,500),
					            new MySqlParameter("@remark", MySqlDbType.VarChar,500),
					            new MySqlParameter("@sort_id", MySqlDbType.Int32,4),
					            new MySqlParameter("@is_lock",  MySqlDbType.Int32,4),
					            new MySqlParameter("@api_path", MySqlDbType.VarChar,255)};
                        parameters[0].Value = model.title;
                        parameters[1].Value = model.img_url;
                        parameters[2].Value = model.app_id;
                        parameters[3].Value = model.app_key;
                        parameters[4].Value = model.remark;
                        parameters[5].Value = model.sort_id;
                        parameters[6].Value = model.is_lock;
                        parameters[7].Value = model.api_path;
                        DbHelperMySql.ExecuteSql(conn, trans, strSql.ToString(), parameters);
                        //ȡ���²����ID
                        newId = GetMaxId(conn, trans);
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
        /// ����һ������
        /// </summary>
        public bool Update(Model.user_oauth_app model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "user_oauth_app set ");
            strSql.Append("title=@title,");
            strSql.Append("img_url=@img_url,");
            strSql.Append("app_id=@app_id,");
            strSql.Append("app_key=@app_key,");
            strSql.Append("remark=@remark,");
            strSql.Append("sort_id=@sort_id,");
            strSql.Append("is_lock=@is_lock,");
            strSql.Append("api_path=@api_path");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@title", MySqlDbType.VarChar,100),
					new MySqlParameter("@img_url", MySqlDbType.VarChar,255),
					new MySqlParameter("@app_id", MySqlDbType.VarChar,100),
					new MySqlParameter("@app_key", MySqlDbType.VarChar,500),
					new MySqlParameter("@remark", MySqlDbType.VarChar,500),
					new MySqlParameter("@sort_id", MySqlDbType.Int32,4),
					new MySqlParameter("@is_lock",  MySqlDbType.Int32,4),
					new MySqlParameter("@api_path", MySqlDbType.VarChar,255),
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = model.title;
            parameters[1].Value = model.img_url;
            parameters[2].Value = model.app_id;
            parameters[3].Value = model.app_key;
            parameters[4].Value = model.remark;
            parameters[5].Value = model.sort_id;
            parameters[6].Value = model.is_lock;
            parameters[7].Value = model.api_path;
            parameters[8].Value = model.id;

            int rows = DbHelperMySql.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "user_oauth_app ");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            int rows = DbHelperMySql.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.user_oauth_app GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,title,img_url,app_id,app_key,remark,sort_id,is_lock,api_path");
            strSql.Append(" from " + databaseprefix + "user_oauth_app");
            strSql.Append(" where id=@id limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            DataSet ds = DbHelperMySql.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ���ݽӿ�Ŀ¼����һ��ʵ��
        /// </summary>
        public Model.user_oauth_app GetModel(string api_path)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,title,img_url,app_id,app_key,remark,sort_id,is_lock,api_path");
            strSql.Append(" from " + databaseprefix + "user_oauth_app");
            strSql.Append(" where api_path=@api_path limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@api_path", MySqlDbType.VarChar,100)};
            parameters[0].Value = api_path;

            DataSet ds = DbHelperMySql.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            
            strSql.Append(" id,title,img_url,app_id,app_key,remark,sort_id,is_lock,api_path ");
            strSql.Append(" FROM " + databaseprefix + "user_oauth_app ");
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
            strSql.Append("select * FROM " + databaseprefix + "user_oauth_app");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperMySql.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperMySql.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        #endregion

        #region ��չ����================================
        /// <summary>
        /// �޸�һ������
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "user_oauth_app set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperMySql.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// ������ת��Ϊʵ��
        /// </summary>
        public Model.user_oauth_app DataRowToModel(DataRow row)
        {
            Model.user_oauth_app model = new Model.user_oauth_app();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["title"] != null)
                {
                    model.title = row["title"].ToString();
                }
                if (row["img_url"] != null)
                {
                    model.img_url = row["img_url"].ToString();
                }
                if (row["app_id"] != null)
                {
                    model.app_id = row["app_id"].ToString();
                }
                if (row["app_key"] != null)
                {
                    model.app_key = row["app_key"].ToString();
                }
                if (row["remark"] != null)
                {
                    model.remark = row["remark"].ToString();
                }
                if (row["sort_id"] != null && row["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(row["sort_id"].ToString());
                }
                if (row["is_lock"] != null && row["is_lock"].ToString() != "")
                {
                    model.is_lock = int.Parse(row["is_lock"].ToString());
                }
                if (row["api_path"] != null)
                {
                    model.api_path = row["api_path"].ToString();
                }
            }
            return model;
        }
        #endregion
    }
}