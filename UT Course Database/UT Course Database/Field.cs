using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UT_Course_Database
{
    public class Field
    {
        public Field()
        {

        }

        public Field(string name, string abbr)
        {
            this.name = name;
            this.abbr = abbr;
        }

        public string name { get; set; }
        public string abbr { get; set; }

        public string getNameUpper()
        {
            return name.ToUpper();
        }
    }
}
