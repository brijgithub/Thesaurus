using System;
using ADODotNetDAL;
using InterfaceDAL;

namespace Factory
{
    public static class ThesaurusFactory
    {
        public static string ConnString = string.Empty;
        public static IThesaurus Create(string Type)
        {
            IThesaurus obj = null;
            try
            {
                if (Type == "ADO")
                {
                    obj = new SynonymDAL(ConnString);

                }
            }
            catch(Exception ex)
            {
                string ss = ex.ToString();
            }
            return obj;
        }


    }
}
