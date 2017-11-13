using System;
using InterfaceDAL;
using System.Collections;
using System.Collections.Generic;

namespace CommonDAL
{
    public abstract class AbstractDAL:IThesaurus
    {

        protected string ConnectionString = string.Empty;

        public AbstractDAL( string _ConnectionString)
        {
            ConnectionString = _ConnectionString;
        }

        public virtual void AddSynonyms(IEnumerable<string> strSynonym)
        {
            
        }

        public virtual IEnumerable<string> GetSynonyms(string word)
        {
            throw new NotImplementedException();
        }

        public  virtual IEnumerable<string> GetWords()
        {
            throw new NotImplementedException();
        }

       
    }
}
