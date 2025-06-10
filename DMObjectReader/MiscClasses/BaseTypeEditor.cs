using DMObjectReader.DTOs;
using DMObjectReader.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMObjectReader.MiscClasses
{
    public abstract class BaseTypeEditor : Form
    {
        public int itemnum;
        public obj obj; // = new obj();
        public short parenttype;
        public bool subchange;
        public ObjectListForm dlg_objectlist;





        public void init() { }
    }
}
