// Author: Tung To

using System;

namespace UT_Course_Database
{
    public class Course
    {

        public Course()
        {

        }

        public Course(string course, string code, string name, string description)
        {
            this.course = course;
            this.code = code;
            this.name = name;
            this.description = description;
        }

        public string course { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public string GetDivision()
        {
            string h = code.Substring(1, 1);
            int i = Int32.Parse(h);
            if (i > 1)
                return "u";
            return "l";
        }

        public int GetHours()
        {
            string h = code.Substring(0, 1);
            return Int32.Parse(h);
            
        }
    }
}
