using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UT_Course_Database
{
    class Config
    {
        public Config (string config)
        {
            string[] k = config.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            ShowHelp = k[0].Split('=')[1] == "true" ? true : false;
            IsUnstable = k[1].Split('=')[1] == "true" ? true : false;
        }

        public bool ShowHelp { get; set; }
        public bool IsUnstable { get; set; }

        public void Save()
        {
            StreamWriter save = new StreamWriter("Resources/config.txt");

            save.WriteLine("ShowHelp=" + ShowHelp.ToString().ToLower());
            save.WriteLine("IsUnstable=" + IsUnstable.ToString().ToLower());

            save.Close();
        }
    }
}
