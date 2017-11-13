using System;
using System.Collections;
using System.Collections.Generic;

namespace InterfaceDAL
{
    public interface IThesaurus
    {
        void AddSynonyms(IEnumerable<string> synonyms);

        IEnumerable<string> GetSynonyms(string word);

        IEnumerable<string>  GetWords();


    }
}
