using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.Helpers
{
    public class Tmapfile
    {
        public string File = "Data\\dm.map";
        public int numitems;
        private string[] names = new string[801]; //orig 800
        private string[] types = new string[801]; //orig 800
        public void Read()
        {
            string row;
            string[] strs;
            int i;

            using (FileStream fs = new FileStream(File, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    List<string> thelines = System.IO.File.ReadAllLines(File).ToList();

                    //skip first big endian file thingy
                    for(i = 1; i < thelines.Count; i++)
                    {
                        strs = thelines[i].Split(new char[] { ',' }, 6);
                        types[i-1] = strs[1]; //i -1 because we skip first
                        names[i-1] = "[" + strs[3] + "] " + strs[4]; //i -1 because we skip first
                    }

                    //i = 0; //skip first line
                    //while (!sr.EndOfStream && i <= 800)
                    //{
                    //    row = sr.ReadLine();
                    //    strs = row.Split(new char[] { ',' }, 6);
                    //    types[i] = strs[1];
                    //    names[i] = "[" + strs[3] + "] " + strs[4];
                    //    i++;
                    //}
                }
            }
            numitems = i; // set the number of items read
        }
        public string getname(int i)
        {
            if (i < 0 || i >= numitems ) 
            {
                return "OOR";
            } 
            else
            {
                return names[i];
            }
        } //End function
        public string gettyperenamed(int i)
        {
            if (i < 0 || i >= numitems ) 
            {
                return "OOR";
            } 
                else
            {
                return types[i];
            }
        } //End function
    } //End Class
}
