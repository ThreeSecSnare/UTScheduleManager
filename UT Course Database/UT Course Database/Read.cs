using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UT_Course_Database
{
    class Read
    {
        public static void Initiate()
        {
            StreamReader file = new StreamReader("Resources/2014-2016/aas.txt");

            List<CourseFile> coursefilelist = new List<CourseFile>();
            string[] list = Directory.GetFiles(@"Resources/2014-2016/");
            string[] abbr = new string[list.Length];

            for (int i = 0; i < list.Length; i++)
            {
                abbr[i] = list[i].Substring(0, list[i].Length - 4).Split('/')[2].ToUpper();
            }

            int[] index = new int[list.Length];

            for(int i = 0; i < list.Length; i++)
            {
                index[i] = abbr[i].Length + 1;
            }

            for (int i = 0; i < list.Length; i++)
            {
                coursefilelist.Add(new CourseFile(list[i], abbr[i], index[i]));
            }



            foreach (CourseFile cf in coursefilelist)
            {
                file = new StreamReader(cf.text);

                string line = file.ReadLine();

                while (!file.EndOfStream)
                {
                    
                    line = line.Substring(0, line.Length - 1);
                    string[] split = line.Split(new string[] { ". " }, StringSplitOptions.None);
                    string tccn_remove = split[0].Split(new string[] { " (" }, StringSplitOptions.None)[0];
                    string abbr_remove = tccn_remove.Substring(cf.index, tccn_remove.Length - cf.index);
                    string[] codes = abbr_remove.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

                    string name = split[1];

                    file.ReadLine();
                    string description = file.ReadLine();

                    if (file.Peek() == '\r')
                    {
                        file.ReadLine();
                        description += "\n\n" + file.ReadLine();

                        string nextLine = file.ReadLine();

                        while (nextLine!=null && nextLine.StartsWith("Topic"))
                        {
                            description += "\n\n" + nextLine;
                            nextLine = file.ReadLine();
                        }

                        

                        line = nextLine;
                    }

                    else
                        line = file.ReadLine();

                    for (int i = 0; i < codes.Length; i++)
                    {
                        Form1.list.Add(new Course(cf.abbr, codes[i], name, description));
                    }
                }

                file.Close();
            }

        }


    }
}
