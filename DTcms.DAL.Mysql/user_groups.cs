using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL.Mysql
{
    /// <summary>
    ///  ���ݷ�����:�û����
    /// </summary>
    public partial class user_groups
    {
        private string databaseprefix; //���ݿ����ǰ׺
        public user_groups(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region ��������===============================
        /// <summary>
        /// �õ����ID
        /// </summary>
        private int GetMaxId(MySqlConnection conn, MySqlTransaction trans)
        {
            string strSql = "select id from " + databaseprefix + "user_groups order by id desc limit 1";
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
            strSql.Append("select count(1) from " + databaseprefix + "user_groups");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            return DbHelperMySql.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Model.user_groups model)
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
                        strSql.Append("insert into " + databaseprefix + "user_groups(");
                        strSql.Append("title,grade,upgrade_exp,amount,point,discount,is_default,is_upgrade,is_lock)");
                        strSql.Append(" values (");
                        strSql.Append("@title,@grade,@upgrade_exp,@amount,@point,@discount,@is_default,@is_upgrade,@is_lock)");
                        MySqlParameter[] parameters = {
					            new MySqlParameter("@title", MySqlDbType.VarChar,100),
					            new MySqlParameter("@grade", MySqlDbType.Int32,4),
					            new MySqlParameter("@upgrade_exp", MySqlDbType.Int32,4),
					            new MySqlParameter("@amount", MySqlDbType.Decimal,5),
					            new MySqlParameter("@point", MySqlDbType.Int32,4),
					            new MySqlParameter("@discount", MySqlDbType.Int32,4),
					            new MySqlParameter("@is_default", MySqlDbType.Int32,4),
					            new MySqlParameter("@is_upgrade", MySqlDbType.Int32,4),
					            new MySqlParameter("@is_lock", MySqlDbType.Int32,4)};
                        parameters[0].Value = model.title;
                        parameters[1].Value = model.grade;
                        parameters[2].Value = model.upgrade_exp;
                        parameters[3].Value = model.amount;
                        parameters[4].Value = model.point;
                        parameters[5].Value = model.discount;
                        parameters[6].Value = model.is_default;
                        parameters[7].Value = model.is_upgrade;
                        parameters[8].Value = model.is_lock;
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
        public bool Update(Model.user_groups model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "user_groups set ");
            strSql.Append("title=@title,");
            strSql.Append("grade=@grade,");
            strSql.Append("upgrade_exp=@upgrade_exp,");
            strSql.Append("amount=@amount,");
            strSql.Append("point=@point,");
            strSql.Append("discount=@discount,");
            strSql.Append("is_default=@is_default,");
            strSql.Append("is_upgrade=@is_upgrade,");
            strSql.Append("is_lock=@is_lock");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@title", MySqlDbType.VarChar,100),
					new MySqlParameter("@grade", MySqlDbType.Int32,4),
					new MySqlParameter("@upgrade_exp", MySqlDbType.Int32,4),
					new MySqlParameter("@amount", MySqlDbType.Decimal,5),
					new MySqlParameter("@point", MySqlDbType.Int32,4),
					new MySqlParameter("@discount", MySqlDbType.Int32,4),
					new MySqlParameter("@is_default", MySqlDbType.Int32,4),
					new MySqlParameter("@is_upgrade", MySqlDbType.Int32,4),
					new MySqlParameter("@is_lock", MySqlDbType.Int32,4),
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = model.title;
            parameters[1].Value = model.grade;
            parameters[2].Value = model.upgrade_exp;
            parameters[3].Value = model.amount;
            parameters[4].Value = model.point;
            parameters[5].Value = model.discount;
            parameters[6].Value = model.is_default;
            parameters[7].Value = model.is_upgrade;
            parameters[8].Value = model.is_lock;
            parameters[9].Value = model.id;

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
            Hashtable sqllist = new Hashtable();
            //ɾ����Ա��۸�
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("delete from " + databaseprefix + "user_group_price ");
            strSql1.Append(" where group_id=@group_id ");
            MySqlParameter[] parameters1 = {
                    new MySqlParameter("@group_id", MySqlDbType.Int32,4)};
            parameters1[0].Value = id;
            sqllist.Add(strSql1.ToString(), parameters1);

            //ɾ������
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "user_groups ");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;
            sqllist.Add(strSql.ToString(), parameters);

            bool result = DbHelperMySql.ExecuteSqlTran(sqllist);
            if (result)
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
        public Model.user_groups GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,title,grade,upgrade_exp,amount,point,discount,is_default,is_upgrade,is_lock");
            strSql.Append(" from " + databaseprefix + "user_groups");
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
            
            strSql.Append(" id,title,grade,upgrade_exp,amount,point,discount,is_default,is_upgrade,is_lock ");
            strSql.Append(" FROM " + databaseprefix + "user_groups ");
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
        #endregion

        #region ��չ����===============================
        /// <summary>
        /// ���ر�������
        /// </summary>
        public string GetTitle(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select title from " + databaseprefix + "user_groups");
            strSql.Append(" where id=" + id + " limit 1");
            string title = Convert.ToString(DbHelperMySql.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(title))
            {
                return "";
            }
            return title;
        }

        /// <summary>
        /// ��ȡ��Ա���ۿ�
        /// </summary>
        public int GetDiscount(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select discount from " + databaseprefix + "user_groups");
            strSql.Append(" where id=" + id + " limit 1");
            string str = Convert.ToString(DbHelperMySql.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            return Convert.ToInt32(str);
        }

        /// <summary>
        /// ȡ��Ĭ�����ʵ��
        /// </summary>
        public Model.user_groups GetDefault()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,title,grade,upgrade_exp,amount,point,discount,is_default,is_upgrade,is_lock");
            strSql.Append(" from " + databaseprefix + "user_groups");
            strSql.Append(" where is_lock=0 order by is_default desc,id asc limit 1");

            DataSet ds = DbHelperMySql.Query(strSql.ToString());
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
        /// ���ݾ���ֵ�������������ʵ��
        /// </summary>
        public Model.user_groups GetUpgrade(int group_id, int exp)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,title,grade,upgrade_exp,amount,point,discount,is_default,is_upgrade,is_lock");
            strSql.Append(" from " + databaseprefix + "user_groups");
            strSql.Append(" where is_lock=0 and is_upgrade=1 and grade>(select grade from " + databaseprefix + "user_groups where id=" + group_id + ") and upgrade_exp<=" + exp);
            strSql.Append(" order by grade asc limit 1");
            DataSet ds = DbHelperMySql.Query(strSql.ToString());
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
        /// ������ת��Ϊʵ��
        /// </summary>
        public Model.user_groups DataRowToModel(DataRow row)
        {
            Model.user_groups model = new Model.user_groups();
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
                if (row["grade"] != null && row["grade"].ToString() != "")
                {
                    model.grade = int.Parse(row["grade"].ToString());
                }
                if (row["upgrade_exp"] != null && row["upgrade_exp"].ToString() != "")
                {
                    model.upgrade_exp = int.Parse(row["upgrade_exp"].ToString());
                }
                if (row["amount"] != null && row["amount"].ToString() != "")
                {
                    model.amount = decimal.Parse(row["amount"].ToString());
                }
                if (row["point"] != null && row["point"].ToString() != "")
                {
                    model.point = int.Parse(row["point"].ToString());
                }
                if (row["discount"] != null && row["discount"].ToString() != "")
                {
                    model.discount = int.Parse(row["discount"].ToString());
                }
                if (row["is_default"] != null && row["is_default"].ToString() != "")
                {
                    model.is_default = int.Parse(row["is_default"].ToString());
                }
                if (row["is_upgrade"] != null && row["is_upgrade"].ToString() != "")
                {
                    model.is_upgrade = int.Parse(row["is_upgrade"].ToString());
                }
                if (row["is_lock"] != null && row["is_lock"].ToString() != "")
                {
                    model.is_lock = int.Parse(row["is_lock"].ToString());
                }
            }
            return model;
        }
        #endregion
    }
}