using System;
using CommonDAL;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ADODotNetDAL
{
    public abstract class TemplateADO : AbstractDAL
    {
        protected SqlConnection objConn = null;
        protected SqlCommand objCommand = null;

        public TemplateADO(string _ConnectionString)
            :base(_ConnectionString)
        {
        }
        private void Open()
        {
            try
            {
                objConn = new SqlConnection(ConnectionString);
                objConn.Open();
                objCommand = new SqlCommand();
                objCommand.Connection = objConn;
            }
            catch(Exception ex)
            {
                string ss = ex.ToString();
            }
        }

        private void Close()
        {
            try
            {
                objConn.Close();
            }
            catch(Exception ex)
            {
                string ss = ex.ToString();
            }
        }

        protected abstract void ExecuteCommand(string str);
        protected abstract IEnumerable<string> ExecuteCommand();
        protected abstract IEnumerable<string> ExecuteCommandForSearch(string word);
            
      
        //DESIGN PATTERN :TEMPLATE PATTERN

        private void Exceute(string str)
        {
            try
            {
                Open();
                ExecuteCommand(str);
                Close();
            }
            catch (Exception ex)
            {
                string ss = ex.ToString();
            }
        }

        private IEnumerable<string> Exceute()
        {
            IEnumerable<string> strList = null;
            try
            {
                Open();
                strList = ExecuteCommand();
                Close();
            }
            catch (Exception ex)
            {
                string ss = ex.ToString();
            }
            return strList;
        }

        private IEnumerable<string> Execute(string str)
        {
            IEnumerable<string> strList = null;
            try
            {
                Open();
                strList = ExecuteCommandForSearch(str);
                Close();
            }
            catch (Exception ex)
            {
                string ss = ex.ToString();
            }
            return strList;
        }

        public override void AddSynonyms(IEnumerable<string> strSynonym)
        {
            try
            {
                foreach (var item in strSynonym)
                {

                    Exceute(item);
                }
            }
            catch (Exception ex)
            {
                string ss = ex.ToString();
            }

        }

        public override IEnumerable<string> GetWords()
        {
             return Exceute();

        }

        public override IEnumerable<string> GetSynonyms(string word)
        {
            return Execute(word);

        }
    }

    public class SynonymDAL:TemplateADO
    {

        public SynonymDAL(string _ConnectionString)
            :base(_ConnectionString)
        {
        }

        protected override void ExecuteCommand(string str)
        {
            try
            {
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "AddSynonyms";
                objCommand.Parameters.Add("@WordSynonym", SqlDbType.VarChar).Value = str.Trim();
                objCommand.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                string ss = ex.ToString();
            }
        }

        protected override IEnumerable<string>  ExecuteCommand()
        {
            List<string> strList = new List<string>();
            try
            {

                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "GetWords";

                using (SqlDataReader dr = objCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string WordName = dr["WordNam"].ToString();
                        strList.Add(WordName);
                    }
                }
            }
            catch (Exception ex)
            {
                string ss = ex.ToString();
            }
            return strList;
        }

        protected override IEnumerable<string> ExecuteCommandForSearch(string word)
        {
            List<string> strList = new List<string>();
            try
            {
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "GetSynonyms";
                objCommand.Parameters.Add("@Word", SqlDbType.VarChar).Value = word.Trim();
                SqlDataReader dr = null;
                using (dr = objCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        strList.Add(dr["WordSynonym"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                string ss = ex.ToString();
            }
            return strList;
        }
    }
}
