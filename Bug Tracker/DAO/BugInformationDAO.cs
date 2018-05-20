using Bug_Tracker.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.DAO
{
    class BugInformationDAO : GenericDAO<BugInformation>
    {
        private SqlConnection conn = new DBConnection().GetConnection();

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<BugInformation> GetAll()
        {
            throw new NotImplementedException();
        }

        public BugInformation GetById(int id)
        {
            conn.Open();
            BugInformation p = null;

            try
            {
                SqlCommand sql = new SqlCommand(null, conn);
                sql.CommandText = "SELECT * FROM tbl_bug_information WHERE bug_id=@bug_id;";
                sql.Prepare();
                sql.Parameters.AddWithValue("@bug_id", id);
                using (SqlDataReader reader = sql.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        p = new BugInformation
                        {
                            InformationId = Convert.ToInt32(reader["bug_information_id"]),
                            Cause = reader["cause"].ToString(),
                            Symtons = reader["symptons"].ToString(),
                            BugId = Convert.ToInt32(reader["bug_id"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return p;
        }

        public void Insert(BugInformation t)
        {
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();

            try
            {
                SqlCommand sql = new SqlCommand(null, conn);
                sql.Transaction = trans;
                sql.CommandText = "INSERT INTO tbl_bug_information VALUES(@symptons, @cause, @bug_id)";
                sql.Prepare();
                sql.Parameters.AddWithValue("@symptons", t.Symtons);
                sql.Parameters.AddWithValue("@cause", t.Cause);
                sql.Parameters.AddWithValue("@bug_id", t.BugId);

                sql.ExecuteNonQuery();

                trans.Commit();
            }
            catch (SqlException ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public void Update(BugInformation t)
        {
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();

            try
            {
                SqlCommand sql = new SqlCommand(null, conn);
                sql.Transaction = trans;
                sql.CommandText = "UPDATE tbl_bug_information SET symptons = @symptons, cause = @cause WHERE bug_id = @bug_id";
                sql.Prepare();
                sql.Parameters.AddWithValue("@symptons", t.Symtons);
                sql.Parameters.AddWithValue("@cause", t.Cause);
                sql.Parameters.AddWithValue("@bug_id", t.BugId);

                sql.ExecuteNonQuery();

                trans.Commit();
            }
            catch (SqlException ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
