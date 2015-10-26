// Author: Tung To

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
