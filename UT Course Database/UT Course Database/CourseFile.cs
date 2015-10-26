using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UT_Course_Database
{
    class CourseFile
    {
        public CourseFile(string text, string abbr, int index)
        {
            this.text = text;
            this.abbr = abbr;
            this.index = index;
        }

        public string text { get; set; }
        public string abbr { get; set; }
        public int index { get; set; }
    }
}
