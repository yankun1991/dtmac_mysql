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
    /// ���ݷ�����:�û�
    /// </summary>
    public partial class users
    {
        private string databaseprefix; //���ݿ����ǰ׺
        public users(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region ��������================================
        /// <summary>
        /// �õ����ID
        /// </summary>
        private int GetMaxId(MySqlConnection conn, MySqlTransaction trans)
        {
            string strSql = "select id from " + databaseprefix + "users order by id desc limit 1";
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
            strSql.Append("select count(1) from " + databaseprefix + "users");
            strSql.Append(" where id=@id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            return DbHelperMySql.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// ����û����Ƿ����
        /// </summary>
        public bool Exists(string user_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "users");
            strSql.Append(" where user_name=@user_name ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@user_name", MySqlDbType.VarChar,100)};
            parameters[0].Value = user_name;
            return DbHelperMySql.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// ���ͬһIPע����(Сʱ)���Ƿ����
        /// </summary>
        public bool Exists(string reg_ip, int regctrl)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "users");
            strSql.Append(" where reg_ip=@reg_ip and TIMESTAMPDIFF(HOUR,reg_time,now())<@regctrl ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@reg_ip", MySqlDbType.VarChar,30),
                    new MySqlParameter("@regctrl", MySqlDbType.Int32,4)};
            parameters[0].Value = reg_ip;
            parameters[1].Value = regctrl;
            return DbHelperMySql.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Model.users model)
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
                        strSql.Append("insert into " + databaseprefix + "users(");
                        strSql.Append("group_id,user_name,salt,password,mobile,email,avatar,nick_name,sex,birthday,telphone,area,address,qq,msn,amount,point,exp,status,reg_time,reg_ip)");
                        strSql.Append(" values (");
                        strSql.Append("@group_id,@user_name,@salt,@password,@mobile,@email,@avatar,@nick_name,@sex,@birthday,@telphone,@area,@address,@qq,@msn,@amount,@point,@exp,@status,@reg_time,@reg_ip)");
                        MySqlParameter[] parameters = {
					            new MySqlParameter("@group_id", MySqlDbType.Int32,4),
					            new MySqlParameter("@user_name", MySqlDbType.VarChar,100),
					            new MySqlParameter("@salt", MySqlDbType.VarChar,20),
					            new MySqlParameter("@password", MySqlDbType.VarChar,100),
					            new MySqlParameter("@mobile", MySqlDbType.VarChar,20),
					            new MySqlParameter("@email", MySqlDbType.VarChar,50),
					            new MySqlParameter("@avatar", MySqlDbType.VarChar,255),
					            new MySqlParameter("@nick_name", MySqlDbType.VarChar,100),
					            new MySqlParameter("@sex", MySqlDbType.VarChar,20),
					            new MySqlParameter("@birthday", MySqlDbType.Date),
					            new MySqlParameter("@telphone", MySqlDbType.VarChar,50),
					            new MySqlParameter("@area", MySqlDbType.VarChar,255),
					            new MySqlParameter("@address", MySqlDbType.VarChar,255),
					            new MySqlParameter("@qq", MySqlDbType.VarChar,20),
					            new MySqlParameter("@msn", MySqlDbType.VarChar,100),
					            new MySqlParameter("@amount", MySqlDbType.Decimal,5),
					            new MySqlParameter("@point", MySqlDbType.Int32,4),
					            new MySqlParameter("@exp", MySqlDbType.Int32,4),
					            new MySqlParameter("@status", MySqlDbType.Int32,4),
					            new MySqlParameter("@reg_time", MySqlDbType.Date),
					            new MySqlParameter("@reg_ip", MySqlDbType.VarChar,20)};
                        parameters[0].Value = model.group_id;
                        parameters[1].Value = model.user_name;
                        parameters[2].Value = model.salt;
                        parameters[3].Value = model.password;
                        parameters[4].Value = model.mobile;
                        parameters[5].Value = model.email;
                        parameters[6].Value = model.avatar;
                        parameters[7].Value = model.nick_name;
                        parameters[8].Value = model.sex;
                        if (model.birthday != null)
                        {
                            parameters[9].Value = model.birthday;
                        }
                        else
                        {
                            parameters[9].Value = DBNull.Value;
                        }
                        parameters[10].Value = model.telphone;
                        parameters[11].Value = model.area;
                        parameters[12].Value = model.address;
                        parameters[13].Value = model.qq;
                        parameters[14].Value = model.msn;
                        parameters[15].Value = model.amount;
                        parameters[16].Value = model.point;
                        parameters[17].Value = model.exp;
                        parameters[18].Value = model.status;
                        parameters[19].Value = model.reg_time;
                        parameters[20].Value = model.reg_ip;
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
        public bool Update(Model.users model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "users set ");
            strSql.Append("group_id=@group_id,");
            strSql.Append("user_name=@user_name,");
            strSql.Append("salt=@salt,");
            strSql.Append("password=@password,");
            strSql.Append("mobile=@mobile,");
            strSql.Append("email=@email,");
            strSql.Append("avatar=@avatar,");
            strSql.Append("nick_name=@nick_name,");
            strSql.Append("sex=@sex,");
            strSql.Append("birthday=@birthday,");
            strSql.Append("telphone=@telphone,");
            strSql.Append("area=@area,");
            strSql.Append("address=@address,");
            strSql.Append("qq=@qq,");
            strSql.Append("msn=@msn,");
            strSql.Append("amount=@amount,");
            strSql.Append("point=@point,");
            strSql.Append("exp=@exp,");
            strSql.Append("status=@status,");
            strSql.Append("reg_time=@reg_time,");
            strSql.Append("reg_ip=@reg_ip");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@group_id", MySqlDbType.Int32,4),
					new MySqlParameter("@user_name", MySqlDbType.VarChar,100),
					new MySqlParameter("@salt", MySqlDbType.VarChar,20),
					new MySqlParameter("@password", MySqlDbType.VarChar,100),
					new MySqlParameter("@mobile", MySqlDbType.VarChar,20),
					new MySqlParameter("@email", MySqlDbType.VarChar,50),
					new MySqlParameter("@avatar", MySqlDbType.VarChar,255),
					new MySqlParameter("@nick_name", MySqlDbType.VarChar,100),
					new MySqlParameter("@sex", MySqlDbType.VarChar,20),
					new MySqlParameter("@birthday", MySqlDbType.Date),
					new MySqlParameter("@telphone", MySqlDbType.VarChar,50),
					new MySqlParameter("@area", MySqlDbType.VarChar,255),
					new MySqlParameter("@address", MySqlDbType.VarChar,255),
					new MySqlParameter("@qq", MySqlDbType.VarChar,20),
					new MySqlParameter("@msn", MySqlDbType.VarChar,100),
					new MySqlParameter("@amount", MySqlDbType.Decimal,5),
					new MySqlParameter("@point", MySqlDbType.Int32,4),
					new MySqlParameter("@exp", MySqlDbType.Int32,4),
					new MySqlParameter("@status", MySqlDbType.Int32,4),
					new MySqlParameter("@reg_time", MySqlDbType.Date),
					new MySqlParameter("@reg_ip", MySqlDbType.VarChar,20),
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = model.group_id;
            parameters[1].Value = model.user_name;
            parameters[2].Value = model.salt;
            parameters[3].Value = model.password;
            parameters[4].Value = model.mobile;
            parameters[5].Value = model.email;
            parameters[6].Value = model.avatar;
            parameters[7].Value = model.nick_name;
            parameters[8].Value = model.sex;
            if (model.birthday != null)
            {
                parameters[9].Value = model.birthday;
            }
            else
            {
                parameters[9].Value = DBNull.Value;
            }
            parameters[10].Value = model.telphone;
            parameters[11].Value = model.area;
            parameters[12].Value = model.address;
            parameters[13].Value = model.qq;
            parameters[14].Value = model.msn;
            parameters[15].Value = model.amount;
            parameters[16].Value = model.point;
            parameters[17].Value = model.exp;
            parameters[18].Value = model.status;
            parameters[19].Value = model.reg_time;
            parameters[20].Value = model.reg_ip;
            parameters[21].Value = model.id;

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
            //��ȡ�û�������
            Model.users model = GetModel(id);
            if (model == null)
            {
                return false;
            }

            Hashtable sqllist = new Hashtable();
            //ɾ�����ּ�¼
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("delete from " + databaseprefix + "user_point_log ");
            strSql1.Append(" where user_id=@id");
            MySqlParameter[] parameters1 = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters1[0].Value = id;
            sqllist.Add(strSql1.ToString(), parameters1);

            //ɾ������¼
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from " + databaseprefix + "user_amount_log ");
            strSql2.Append(" where user_id=@id");
            MySqlParameter[] parameters2 = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters2[0].Value = id;
            sqllist.Add(strSql2.ToString(), parameters2);

            //ɾ�����������¼
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("delete from " + databaseprefix + "user_attach_log");
            strSql3.Append(" where user_id=@id");
            MySqlParameter[] parameters3 = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters3[0].Value = id;
            sqllist.Add(strSql3.ToString(), parameters3);

            //ɾ������Ϣ
            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("delete from " + databaseprefix + "user_message ");
            strSql4.Append(" where post_user_name=@post_user_name or accept_user_name=@accept_user_name");
            MySqlParameter[] parameters4 = {
					new MySqlParameter("@post_user_name", MySqlDbType.VarChar,100),
                    new MySqlParameter("@accept_user_name", MySqlDbType.VarChar,100)};
            parameters4[0].Value = model.user_name;
            parameters4[1].Value = model.user_name;
            sqllist.Add(strSql4.ToString(), parameters4);

            //ɾ��������
            StringBuilder strSql5 = new StringBuilder();
            strSql5.Append("delete from " + databaseprefix + "user_code ");
            strSql5.Append(" where user_id=@id");
            MySqlParameter[] parameters5 = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters5[0].Value = id;
            sqllist.Add(strSql5.ToString(), parameters5);

            //ɾ����¼��־
            StringBuilder strSql6 = new StringBuilder();
            strSql6.Append("delete from " + databaseprefix + "user_login_log ");
            strSql6.Append(" where user_id=@id");
            MySqlParameter[] parameters6 = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters6[0].Value = id;
            sqllist.Add(strSql6.ToString(), parameters6);

            //ɾ��OAuth��Ȩ�û���Ϣ
            StringBuilder strSql8 = new StringBuilder();
            strSql8.Append("delete from " + databaseprefix + "user_oauth ");
            strSql8.Append(" where user_id=@id");
            MySqlParameter[] parameters8 = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters8[0].Value = id;
            sqllist.Add(strSql8.ToString(), parameters8);

            //ɾ���û���ֵ��
            StringBuilder strSql9 = new StringBuilder();
            strSql9.Append("delete from " + databaseprefix + "user_recharge ");
            strSql9.Append(" where user_id=@id");
            MySqlParameter[] parameters9 = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters9[0].Value = id;
            sqllist.Add(strSql9.ToString(), parameters9);

            //ɾ���û�����
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "users ");
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
        public Model.users GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,group_id,user_name,salt,password,mobile,email,avatar,nick_name,sex,birthday,telphone,area,address,qq,msn,amount,point,exp,status,reg_time,reg_ip");
            strSql.Append(" from " + databaseprefix + "users");
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
        /// �����û������뷵��һ��ʵ��
        /// </summary>
        public Model.users GetModel(string user_name, string password, int emaillogin, int mobilelogin)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,group_id,user_name,salt,password,mobile,email,avatar,nick_name,sex,birthday,telphone,area,address,qq,msn,amount,point,exp,status,reg_time,reg_ip");
            strSql.Append(" from " + databaseprefix + "users");
            strSql.Append(" where (user_name=@user_name ");
            if (emaillogin == 1)
            {
                strSql.Append(" or email=@user_name");
            }
            if (mobilelogin == 1)
            {
                strSql.Append(" or mobile=@user_name");
            }
            strSql.Append(") and password=@password and status<3");
            strSql.Append(" limit 1");

            MySqlParameter[] parameters = {
					    new MySqlParameter("@user_name", MySqlDbType.VarChar,100),
                        new MySqlParameter("@password", MySqlDbType.VarChar,100)};
            parameters[0].Value = user_name;
            parameters[1].Value = password;

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
        /// �����û�������һ��ʵ��
        /// </summary>
        public Model.users GetModel(string user_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,group_id,user_name,salt,password,mobile,email,avatar,nick_name,sex,birthday,telphone,area,address,qq,msn,amount,point,exp,status,reg_time,reg_ip");
            strSql.Append(" from " + databaseprefix + "users");
            strSql.Append(" where user_name=@user_name and status<3 limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@user_name", MySqlDbType.VarChar,100)};
            parameters[0].Value = user_name;

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
            
            strSql.Append(" id,group_id,user_name,salt,password,mobile,email,avatar,nick_name,sex,birthday,telphone,area,address,qq,msn,amount,point,exp,status,reg_time,reg_ip");
            strSql.Append(" FROM " + databaseprefix + "users ");
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
            strSql.Append("select * FROM " + databaseprefix + "users");
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
        /// ���Email�Ƿ����
        /// </summary>
        public bool ExistsEmail(string email)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "users");
            strSql.Append(" where email=@email ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@email", MySqlDbType.VarChar,100)};
            parameters[0].Value = email;
            return DbHelperMySql.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// ����ֻ������Ƿ����
        /// </summary>
        public bool ExistsMobile(string mobile)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "users");
            strSql.Append(" where mobile=@mobile ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@mobile", MySqlDbType.VarChar,20)};
            parameters[0].Value = mobile;
            return DbHelperMySql.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// �����û���ȡ��Salt
        /// </summary>
        public string GetSalt(string user_name)
        {
            //�����û���ȡ��Salt
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select salt from " + databaseprefix + "users");
            strSql.Append(" where user_name=@user_name limit 1");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@user_name", MySqlDbType.VarChar,100)};
            parameters[0].Value = user_name;
            string salt = Convert.ToString(DbHelperMySql.GetSingle(strSql.ToString(), parameters));
            if (!string.IsNullOrEmpty(salt))
            {
                return salt;
            }
            //�������ֻ���ȡ��Salt
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("select salt from " + databaseprefix + "users");
            strSql1.Append(" where mobile=@mobile limit 1");
            MySqlParameter[] parameters1 = {
                    new MySqlParameter("@mobile", MySqlDbType.VarChar,20)};
            parameters1[0].Value = user_name;
            salt = Convert.ToString(DbHelperMySql.GetSingle(strSql1.ToString(), parameters1));
            if (!string.IsNullOrEmpty(salt))
            {
                return salt;
            }
            //����������ȡ��Salt
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select salt from " + databaseprefix + "users");
            strSql2.Append(" where email=@email limit 1");
            MySqlParameter[] parameters2 = {
                    new MySqlParameter("@email", MySqlDbType.VarChar,50)};
            parameters2[0].Value = user_name;
            salt = Convert.ToString(DbHelperMySql.GetSingle(strSql2.ToString(), parameters2));
            if (!string.IsNullOrEmpty(salt))
            {
                return salt;
            }
            return string.Empty;
        }

        /// <summary>
        /// �޸�һ������
        /// </summary>
        public int UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "users set " + strValue);
            strSql.Append(" where id=" + id);
            return DbHelperMySql.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.users DataRowToModel(DataRow row)
        {
            Model.users model = new Model.users();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["group_id"] != null && row["group_id"].ToString() != "")
                {
                    model.group_id = int.Parse(row["group_id"].ToString());
                }
                if (row["user_name"] != null)
                {
                    model.user_name = row["user_name"].ToString();
                }
                if (row["salt"] != null)
                {
                    model.salt = row["salt"].ToString();
                }
                if (row["password"] != null)
                {
                    model.password = row["password"].ToString();
                }
                if (row["mobile"] != null)
                {
                    model.mobile = row["mobile"].ToString();
                }
                if (row["email"] != null)
                {
                    model.email = row["email"].ToString();
                }
                if (row["avatar"] != null)
                {
                    model.avatar = row["avatar"].ToString();
                }
                if (row["nick_name"] != null)
                {
                    model.nick_name = row["nick_name"].ToString();
                }
                if (row["sex"] != null)
                {
                    model.sex = row["sex"].ToString();
                }
                if (row["birthday"] != null && row["birthday"].ToString() != "")
                {
                    model.birthday = DateTime.Parse(row["birthday"].ToString());
                }
                if (row["telphone"] != null)
                {
                    model.telphone = row["telphone"].ToString();
                }
                if (row["area"] != null)
                {
                    model.area = row["area"].ToString();
                }
                if (row["address"] != null)
                {
                    model.address = row["address"].ToString();
                }
                if (row["qq"] != null)
                {
                    model.qq = row["qq"].ToString();
                }
                if (row["msn"] != null)
                {
                    model.msn = row["msn"].ToString();
                }
                if (row["amount"] != null && row["amount"].ToString() != "")
                {
                    model.amount = decimal.Parse(row["amount"].ToString());
                }
                if (row["point"] != null && row["point"].ToString() != "")
                {
                    model.point = int.Parse(row["point"].ToString());
                }
                if (row["exp"] != null && row["exp"].ToString() != "")
                {
                    model.exp = int.Parse(row["exp"].ToString());
                }
                if (row["status"] != null && row["status"].ToString() != "")
                {
                    model.status = int.Parse(row["status"].ToString());
                }
                if (row["reg_time"] != null && row["reg_time"].ToString() != "")
                {
                    model.reg_time = DateTime.Parse(row["reg_time"].ToString());
                }
                if (row["reg_ip"] != null)
                {
                    model.reg_ip = row["reg_ip"].ToString();
                }
            }
            return model;
        }
        #endregion

    }
}