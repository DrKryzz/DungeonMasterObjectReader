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

namespace DMObjectReader.Editors
{
    public partial class ItemTypeEditor : Form
    {
        public short selecteditem;
        private string[] objstrings; // = new string[200]; //orig 200 but f556.objstrings.Length is 201
        App App;
        public ItemTypeEditor(App _app)
        {

            InitializeComponent();
            App = _app;
            objstrings = new string[App.graphics.f556.objstrings.Length];
        }

        public void init()
        {
            
            for(int i = 0; i < App.graphics.f556.objstrings.Length; i++)
            {
                objstrings[i] = App.graphics.f556.objstrings[i];
            }
            
        }
    }
}
