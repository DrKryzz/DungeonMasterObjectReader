using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.LinkLabel;

namespace DMObjectReader.Editors
{
    public partial class ObjectListForm : Form
    {
        App App;
        public ObjectListForm(App _app)
        {
            InitializeComponent();
            App = _app;
        }

        public void init()
        {
            short i;

            ObjectList.Items.Clear();
            for (i = 1; i <= 180; i++) 
            {
                ObjectList.Items.Add("1").SubItems.AddRange("2,3".Split(','));
                modifieditem(i);
            }
        }

        public void modifieditem(short num) 
        {

            ObjectList.Items[num - 1].Text = App.ZeroStr(num - 1, 3);
            ObjectList.Items[num - 1].SubItems[1].Text = App.graphics.f556.objstrings[App.graphics.f559.obj(num).ItemType + 1];
            ObjectList.Items[num - 1].SubItems[2].Text = App.graphics.f556.objtype(num);
        }
    }
}
