// Author: Tung To

using System.Collections.Generic;

namespace UT_Course_Database
{
    class Semester
    {

        public Semester(string sem, string year)
        {
            semester = sem;
            this.year = int.Parse(year);
            list = new List<Course>();
            notes = new string[20];

            for (int i = 0; i < notes.Length; i++)
            {
                notes[i] = "";
            }
        }

        public Semester(string sem, string year, List<Course> list, string[] notes)
        {
            semester = sem;
            this.year = int.Parse(year);
            this.list = list;
            this.notes = notes;
        }

        public string semester { get; set; }
        public int year { get; set; }
        public List<Course> list { get; set; }
        public string[] notes { get; set; }

        public override string ToString()
        {
            string sem = "";
            switch (semester)
            {
                case "Spring": sem = "Spr"; break;
                case "Fall": sem = "Fall"; break;
                case "Summer": sem = "Sum"; break;
            }
            if (year > 0)
                return sem + " " + year;
            else
                return sem + " " + -1*year + "BCE";
        }
    }
}
