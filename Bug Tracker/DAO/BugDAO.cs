using Bug_Tracker.Model;
using System;
using System.Collections;
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

        /// <summary>
        /// get all bugs with related code and image
        /// </summary>
        /// <returns>List<string></returns>
        public List<Bug> getAllBugs()
        {
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            List<Bug> bugList = new List<Bug>();
            Bug bug = null;
            Code code = null;
            Image image = null;

            try
            {
                SqlCommand sql = new SqlCommand(null, conn);
                sql.Transaction = trans;
                sql.CommandText = "SELECT * FROM tbl_bug b JOIN tbl_code c ON b.bug_id = c.bug_id JOIN tbl_image i ON b.bug_id = i.bug_id WHERE bug_status = 0;";
                sql.Prepare();

                using (SqlDataReader reader = sql.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        bug = new Bug();
                        code = new Code();
                        image = new Image();

                        bug.BugId = Convert.ToInt32(reader["bug_id"]);
                        bug.ProjectName = Convert.ToString(reader["project_name"]);
                        bug.ClassName = Convert.ToString(reader["class_name"]);
                        bug.MethodName = Convert.ToString(reader["method_name"]);
                        bug.StartLine = Convert.ToInt32(reader["start_line"]);
                        bug.EndLine = Convert.ToInt32(reader["end_line"]);
                        bug.ProgrammerId = Convert.ToInt32(reader["code_author"]);
                        bug.Status = Convert.ToString(reader["bug_status"]);

                        code.CodeId = Convert.ToInt32(reader["code_id"]);
                        code.CodeFilePath = Convert.ToString(reader["code_file_path"]);
                        code.CodeFileName = Convert.ToString(reader["code_file_name"]);
                        code.ProgrammingLanguage = Convert.ToString(reader["programming_language"]);
                        code.BugId = Convert.ToInt32(reader["bug_id"]);

                        image.ImageId = Convert.ToInt32(reader["image_id"]);
                        image.ImagePath = Convert.ToString(reader["image_path"]);
                        image.ImageName = Convert.ToString(reader["image_name"]);
                        image.BugId = Convert.ToInt32(reader["bug_id"]);

                        bug.Images = image;
                        bug.Codes = code;
                        bugList.Add(bug);
                    }
                }

                trans.Commit();
            }
            catch (SqlException ex)
            {
                trans.Rollback();
                throw ex;
            } catch(NullReferenceException ex)
            {
                trans.Rollback();
                throw new NullReferenceException(ex.Message);
            }

            return bugList;
        }
    }
}
