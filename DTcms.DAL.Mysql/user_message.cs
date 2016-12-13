using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL.Mysql
{
    /// <summary>
    /// �û�����Ϣ
    /// </summary>
    public partial class user_message
    {
        private string databaseprefix; //���ݿ����ǰ׺
        public user_message(string _databaseprefix)
        {
            databaseprefix = _databaseprefix; 
        }

        #region ��������==============================
        /// <summary>
        /// �õ����ID
        /// </summary>
        private int GetMaxId(MySqlConnection conn, MySqlTransaction trans)
        {
            string strSql = "select id from " + databaseprefix + "user_message order by id desc limit 1";
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
            strSql.Append("select count(1) from " + databaseprefix + "user_message");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            return DbHelperMySql.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Model.user_message model)
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
                        strSql.Append("insert into " + databaseprefix + "user_message(");
                        strSql.Append("type,post_user_name,accept_user_name,is_read,title,content,post_time,read_time)");
                        strSql.Append(" values (");
                        strSql.Append("@type,@post_user_name,@accept_user_name,@is_read,@title,@content,@post_time,@read_time)");
                        MySqlParameter[] parameters = {
					            new MySqlParameter("@type", MySqlDbType.Int32,4),
					            new MySqlParameter("@post_user_name", MySqlDbType.VarChar,100),
					            new MySqlParameter("@accept_user_name", MySqlDbType.VarChar,100),
					            new MySqlParameter("@is_read", MySqlDbType.Int32,4),
					            new MySqlParameter("@title", MySqlDbType.VarChar,100),
					            new MySqlParameter("@content", MySqlDbType.VarChar),
					            new MySqlParameter("@post_time", MySqlDbType.Date),
					            new MySqlParameter("@read_time", MySqlDbType.Date)};
                        parameters[0].Value = model.type;
                        parameters[1].Value = model.post_user_name;
                        parameters[2].Value = model.accept_user_name;
                        parameters[3].Value = model.is_read;
                        parameters[4].Value = model.title;
                        parameters[5].Value = model.content;
                        parameters[6].Value = model.post_time;
                        if (model.read_time != null)
                        {
                            parameters[7].Value = model.read_time;
                        }
                        else
                        {
                            parameters[7].Value = DBNull.Value;
                        }
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
        public bool Update(Model.user_message model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "user_message set ");
            strSql.Append("type=@type,");
            strSql.Append("post_user_name=@post_user_name,");
            strSql.Append("accept_user_name=@accept_user_name,");
            strSql.Append("is_read=@is_read,");
            strSql.Append("title=@title,");
            strSql.Append("content=@content,");
            strSql.Append("post_time=@post_time,");
            strSql.Append("read_time=@read_time");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@type", MySqlDbType.Int32,4),
					new MySqlParameter("@post_user_name", MySqlDbType.VarChar,100),
					new MySqlParameter("@accept_user_name", MySqlDbType.VarChar,100),
					new MySqlParameter("@is_read", MySqlDbType.Int32,4),
					new MySqlParameter("@title", MySqlDbType.VarChar,100),
					new MySqlParameter("@content", MySqlDbType.LongText),
					new MySqlParameter("@post_time", MySqlDbType.Date),
					new MySqlParameter("@read_time", MySqlDbType.Date),
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = model.type;
            parameters[1].Value = model.post_user_name;
            parameters[2].Value = model.accept_user_name;
            parameters[3].Value = model.is_read;
            parameters[4].Value = model.title;
            parameters[5].Value = model.content;
            parameters[6].Value = model.post_time;
            if (model.read_time != null)
            {
                parameters[7].Value = model.read_time;
            }
            else
            {
                parameters[7].Value = DBNull.Value;
            }
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
            strSql.Append("delete from " + databaseprefix + "user_message ");
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
        /// �����û���ɾ��һ������
        /// </summary>
        public bool Delete(int id, string user_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "user_message ");
            strSql.Append(" where id=@id and (post_user_name=@post_user_name or accept_user_name=@accept_user_name)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4),
                    new MySqlParameter("@post_user_name", MySqlDbType.VarChar,100),
                    new MySqlParameter("@accept_user_name", MySqlDbType.VarChar,100)};
            parameters[0].Value = id;
            parameters[1].Value = user_name;
            parameters[2].Value = user_name;

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
        public Model.user_message GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,type,post_user_name,accept_user_name,is_read,title,content,post_time,read_time from " + databaseprefix + "user_message ");
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
        /// ���ǰ��������
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            
            strSql.Append(" id,type,post_user_name,accept_user_name,is_read,title,content,post_time,read_time ");
            strSql.Append(" FROM " + databaseprefix + "user_message ");
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
            strSql.Append("select * FROM " + databaseprefix + "user_message");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperMySql.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperMySql.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        #endregion

        #region ��չ����==============================
        /// <summary>
        /// ���ؼ�¼����
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + "user_message ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperMySql.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// �޸�һ������
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "user_message set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperMySql.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// ������ת��Ϊʵ��
        /// </summary>
        public Model.user_message DataRowToModel(DataRow row)
        {
            Model.user_message model = new Model.user_message();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["type"] != null && row["type"].ToString() != "")
                {
                    model.type = int.Parse(row["type"].ToString());
                }
                if (row["post_user_name"] != null)
                {
                    model.post_user_name = row["post_user_name"].ToString();
                }
                if (row["accept_user_name"] != null)
                {
                    model.accept_user_name = row["accept_user_name"].ToString();
                }
                if (row["is_read"] != null && row["is_read"].ToString() != "")
                {
                    model.is_read = int.Parse(row["is_read"].ToString());
                }
                if (row["title"] != null)
                {
                    model.title = row["title"].ToString();
                }
                if (row["content"] != null)
                {
                    model.content = row["content"].ToString();
                }
                if (row["post_time"] != null && row["post_time"].ToString() != "")
                {
                    model.post_time = DateTime.Parse(row["post_time"].ToString());
                }
                if (row["read_time"] != null && row["read_time"].ToString() != "")
                {
                    model.read_time = DateTime.Parse(row["read_time"].ToString());
                }
            }
            return model;
        }
        #endregion
    }
}