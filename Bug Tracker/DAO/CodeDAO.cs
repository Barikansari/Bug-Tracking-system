using Bug_Tracker.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.DAO
{
    class CodeDAO : GenericDAO<Code>
    {

        private SqlConnection conn = new DBConnection().GetConnection();

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Code> GetAll()
        {
            throw new NotImplementedException();
        }

        public Code GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Code t)
        {
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();

            try
            {
                SqlCommand sql = new SqlCommand(null, conn);
                sql.Transaction = trans;
                sql.CommandText = "INSERT INTO tbl_code VALUES(@filepath, @filename, @plan, @bug_id)";
                sql.Prepare();
                sql.Parameters.AddWithValue("@filepath", t.CodeFilePath);
                sql.Parameters.AddWithValue("@filename", t.CodeFileName);
                sql.Parameters.AddWithValue("@plan", t.ProgrammingLanguage);
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

        public void Update(Code t)
        {
            throw new NotImplementedException();
        }
    }
}
