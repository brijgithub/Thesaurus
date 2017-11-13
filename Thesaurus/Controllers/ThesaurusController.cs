using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Factory;
using Microsoft.AspNetCore.Mvc;
using InterfaceDAL;
using Microsoft.Extensions.Configuration;

namespace Thesaurus.Controllers
{
    [Route("api/[controller]")]
    public class ThesaurusController : Controller
    {

        IConfiguration _configuration;
        IThesaurus obj;

        public ThesaurusController(IConfiguration configuration)
        {
            _configuration = configuration;
            ThesaurusFactory.ConnString = _configuration.GetValue<string>("ConnString");
            obj = ThesaurusFactory.Create("ADO");
        }


        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return obj.GetWords();
        }

        // GET api/values/5
        [HttpGet("{word}")]
        public IEnumerable<string> Get(string word)
        {
            return obj.GetSynonyms(word);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            IThesaurus obj;
            obj = ThesaurusFactory.Create("ADO");

            IEnumerable<string> str = new List<string>() { "handsome", "pretty", "gorgeous"};

            obj.AddSynonyms(str);
        }



       
    }
}
