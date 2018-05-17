using Bug_Tracker.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.DAO
{
    class BugDAO : GenericDAO<Bug>
    {

        private SqlConnection conn = new DBConnection().GetConnection();

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Bug> GetAll()
        {
            throw new NotImplementedException();
        }

        public Bug GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Bug t)
        {
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();

            try
            {
                SqlCommand sql = new SqlCommand(null, conn);
                sql.Transaction = trans;
                sql.CommandText = "INSERT INTO tbl_bug VALUES(@projectname, @classname, @methodname, @startline, @endline, @codeauthor, @status); SELECT SCOPE_IDENTITY()";
                sql.Prepare();
                sql.Parameters.AddWithValue("@projectname", t.ProjectName);
                sql.Parameters.AddWithValue("@classname", t.ClassName);
                sql.Parameters.AddWithValue("@methodname", t.MethodName);
                sql.Parameters.AddWithValue("@startline", t.StartLine);
                sql.Parameters.AddWithValue("@endline", t.EndLine);
                sql.Parameters.AddWithValue("@codeauthor", t.ProgrammerId);
                sql.Parameters.AddWithValue("@status", t.Status);

                sql.ExecuteNonQuery();

                t.BugId = Convert.ToInt32(sql.ExecuteScalar());

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

        public void Update(Bug t)
        {
            throw new NotImplementedException();
        }
    }
}
