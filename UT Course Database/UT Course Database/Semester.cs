// Author: Tung To

using System.Collections.Generic;

namespace UT_Course_Database
{
    class Semester
    {

        public Semester(string sem, string year)
        {
            semester = sem;
            this.year = year;
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
            this.year = year;
            this.list = list;
            this.notes = notes;
        }

        public string semester { get; set; }
        public string year { get; set; }
        public List<Course> list { get; set; }
        public string[] notes { get; set; }

    }
}
