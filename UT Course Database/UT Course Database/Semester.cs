// Author: Tung To

using System;
using System.Collections.Generic;

namespace UT_Course_Database
{
    public class Semester : IComparable<Semester>
    {

        public Semester(string sem, string year)
        {
            switch (sem)
            {
                case "Fall": semester = (int)SemesterOrder.Fall; break;
                case "Winter": semester = (int)SemesterOrder.Winter; break;
                case "Spring": semester = (int)SemesterOrder.Spring; break;
                case "Summer": semester = (int)SemesterOrder.Summer; break;
            }
            this.year = int.Parse(year);
            list = new List<Course>();
            notes = new List<string>();
        }

        public Semester(string sem, string year, List<Course> list, List<string> notes)
        {
            switch (sem)
            {
                case "Fall": semester = (int)SemesterOrder.Fall; break;
                case "Winter": semester = (int)SemesterOrder.Winter; break;
                case "Spring": semester = (int)SemesterOrder.Spring; break;
                case "Summer": semester = (int)SemesterOrder.Summer; break;
            }
            this.year = int.Parse(year);
            this.list = list;
            this.notes = notes;
        }

        public int semester { get; set; }
        public int year { get; set; }
        public List<Course> list { get; set; }
        public List<string> notes { get; set; }

        public override string ToString()
        {
            string sem = "";
            switch (semester)
            {
                case (int)SemesterOrder.Fall: sem = "Fall"; break;
                case (int)SemesterOrder.Winter: sem = "Wntr"; break;
                case (int)SemesterOrder.Spring: sem = "Spr"; break;
                case (int)SemesterOrder.Summer: sem = "Sum"; break;
            }
            if (year > 0)
                return sem + " " + year;
            else
                return sem + " " + (-1*year) + "BCE";
        }

        public int CompareTo(Semester s)
        {
            if(year==s.year)
            {
                return semester.CompareTo(s.semester);
            }

            return year.CompareTo(s.year);
        }

        public enum SemesterOrder { Fall, Winter, Spring, Summer}
    }
}
