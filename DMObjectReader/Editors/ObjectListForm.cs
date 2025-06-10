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

        private void ObjectList_Renamed_DblClick(object sender, MouseEventArgs e)
        {
            int itemnum;
            string ItemType;

            itemnum = ObjectList.FocusedItem.Index + 1; //maybe remove 1

            ItemType = App.graphics.f556.objtype(itemnum);

            FoodEditor dlg_foodeditor;
            ArmorEditor dlg_armoreditor;
            WeaponEditor dlg_weaponeditor;
            MiscObjectEditor dlg_miscobjecteditor;
            if (ItemType == "Food")
            {
                dlg_foodeditor = new FoodEditor();
                dlg_foodeditor.itemnum = itemnum;
                dlg_foodeditor.init();
                dlg_foodeditor.parenttype = 3;
                dlg_foodeditor.dlg_objectlist = this;
                dlg_foodeditor.Show();
            }
            else if (ItemType == "Armor") {
                dlg_armoreditor = new ArmorEditor();
                dlg_armoreditor.itemnum = itemnum;
                dlg_armoreditor.init();
                dlg_armoreditor.parenttype = 3;
                dlg_armoreditor.dlg_objectlist = this;
                dlg_armoreditor.Show();
                        }
            else if (ItemType == "Weapon") {
                dlg_weaponeditor = new WeaponEditor();
                dlg_weaponeditor.itemnum = itemnum;
                dlg_weaponeditor.init();
                dlg_weaponeditor.parenttype = 3;
                dlg_weaponeditor.dlg_objectlist = this;
                dlg_weaponeditor.Show();
                        }
            else if (ItemType == "Misc") {
                dlg_miscobjecteditor = new MiscObjectEditor();
                dlg_miscobjecteditor.itemnum = itemnum;
                dlg_miscobjecteditor.init();
                dlg_miscobjecteditor.parenttype = 3;
                dlg_miscobjecteditor.dlg_objectlist = this;
                dlg_miscobjecteditor.Show();
                        }
            else
                App.MsgBox("Invalid item type");
        }
    }
}
