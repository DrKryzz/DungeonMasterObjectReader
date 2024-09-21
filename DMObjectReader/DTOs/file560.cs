using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMObjectReader.DTOs
{
    public class file560
    {

        public MemoryStream itemdata;
        public int itemloc; //Now 0 based
        private attack[] Attacks = ArrayUt<attack>.NewArray1(44);
        private combo[] Combos = ArrayUt<combo>.NewArray1(44);
        private spell[] Spells = ArrayUt<spell>.NewArray1(25);
        private short[] rects = new short[25]; //orig 24
        private byte[] Runes = new byte[25]; //orig 24
        private byte[] RuneMultipliers = new byte[7]; //orig 6
        private byte[] bytes20194 = new byte[17]; //orig 16
        private short byte19218;

        private App app;
        public file560(App _app)
        {
            app = _app;
            foreach (var c in Combos)
            {
                c.f560 = this;
            }

        }
        public byte read_byte()
        {
            itemdata.Position = itemloc;
            byte ans = new BinaryReader(itemdata).ReadByte();
            itemloc = itemloc + 1;

            return ans;
        } //End function

        public int read_int()
        {
            byte b1;
            byte b2;
            b2 = read_byte();
            b1 = read_byte();
            int ans_read_int = b2;
            ans_read_int = ans_read_int * 256;
            ans_read_int = ans_read_int + b1;

            return ans_read_int;
        } //End function

        public void write_byte(byte num)
        {
            itemdata.Position = itemloc;
            itemdata.WriteByte(num);
            itemloc = itemloc + 1;
        } //End Sub
        public void write_int(int num)
        {

            byte b1;
            byte b2;
            int l1;
            l1 = num & 0xFF00;
            b1 = App.CByte(l1 / 0x100);
            b2 = App.CByte(num & 0xFF);
            write_byte(b1);
            write_byte(b2);
        } //End Sub

        public attack attack(short num)
        {
            return Attacks[num];
        } //End function
        public combo Combo(short num)
        {
            return Combos[num];
        } //End function
        public spell Spell(short num)
        {
            return Spells[num];
        } //End function
        public short rect(short num)
        {
            return rects[num];
        } //End function
        public byte rune(short num)
        {
            return Runes[num];
        } //End function
        public void setrune(short num, byte val_Renamed)
        {
            Runes[num] = val_Renamed;
        } //End Sub
        public byte runemultiplier(short num)
        {
            return RuneMultipliers[num];
        } //End function
        public void setrunemultiplier(short num, byte val_Renamed)
        {
            RuneMultipliers[num] = val_Renamed;
        } //End Sub
        public byte byte20194(short num)
        {
            return bytes20194[num];
        } //End function
        public void setbyte20194(short num, byte v)
        {
            bytes20194[num] = v;
        } //End Sub
        public void update()
        {


            short i;
            short atknum;
            short var;

            itemdata = new MemoryStream();
            itemloc = 0;
            var = 0;
            for (atknum = 1; atknum <= 44; atknum++)
            {
                var = (short)(var + 1 + App.Len(Attacks[atknum].name));
            } //end Next 

            if (var > 299)
            {
                MessageBox.Show("Error: Attacks total name length too large!" + App.Chr(13) + App.Chr(13) + "Total characters: " + App.Str(var) + "/300");
                return;
            }

            for (i = 1; i <= 24; i++)
            {
                write_int(rects[i]);
            } //end Next 

            for (i = 1; i <= 16; i++)
            {
                write_byte(bytes20194[i]);
            } //end Next 

            for (i = 1; i <= 44; i++)
            {
                write_byte((Attacks[i].Exp));
            } //end Next 
            for (i = 1; i <= 44; i++)
            {
                write_byte((Attacks[i].Skill));
            } //end Next 
            for (i = 1; i <= 44; i++)
            {
                if (Attacks[i].Defense < 0)
                {
                    Attacks[i].Defense = (short)(Attacks[i].Defense + 256);
                }
                write_byte(App.CByte(Attacks[i].Defense));
            } //end Next 
            for (i = 1; i <= 44; i++)
            {
                write_byte((Attacks[i].Stamina));
            } //end Next 
            for (i = 1; i <= 44; i++)
            {
                write_byte(App.CByte(Attacks[i].HitProb));
            } //end Next 
            for (i = 1; i <= 44; i++)
            {
                write_byte((Attacks[i].Damage));
            } //end Next 
            for (i = 1; i <= 44; i++)
            {
                write_byte((Attacks[i].Fatigue));
            } //end Next 

            var = 0;
            for (atknum = 1; atknum <= 44; atknum++)
            {
                for (i = 1; i <= App.Len(Attacks[atknum].name); i++)
                {
                    var = (short)(var + 1);
                    write_byte(App.CByte(App.AscW(App.Mid(Attacks[atknum].name, i, 1))));
                } //end Next 
                write_byte(0);
                var = (short)(var + 1);
            } //end Next 
            for (; var <= 299; var++) //var = var
            {
                write_byte(0);
            } //end Next 


            for (i = 1; i <= 44; i++)
            {
                write_byte((Combos[i].attack1));
                write_byte((Combos[i].attack2));
                write_byte((Combos[i].attack3));
                write_byte((Combos[i].byte4));
                write_byte((Combos[i].byte5));
                write_byte((Combos[i].byte6));
                write_byte((Combos[i].unused1));
                write_byte((Combos[i].unused2));
            } //end Next 

            write_int(byte19218);

            for (i = 1; i <= 25; i++)
            {
                write_byte((Spells[i].Rune1));
                write_byte((Spells[i].Rune2));
                write_byte((Spells[i].Rune3));
                write_byte((Spells[i].Rune4));
                write_byte((Spells[i].Difficulty));
                write_byte((Spells[i].Skill));
                write_int((Spells[i].word6));
            } //end Next 

            for (i = 1; i <= 6; i++)
            {
                write_byte(RuneMultipliers[i]);
            } //end Next 

            for (i = 1; i <= 24; i++)
            {
                write_byte(Runes[i]);
            } //end Next 


            app.graphics.setgitemsize(560, (int)itemdata.Length);

            app.graphics.setgitemdata(560, itemdata);
        } //End Sub
        public void read()
        {

            short i;
            short atknum;
            short var;
            int l;

            itemloc = 0;

            for (i = 1; i <= 24; i++)
            {
                l = read_int();
                rects[i] = (short)l;
            } //end Next 

            for (i = 1; i <= 16; i++)
            {
                bytes20194[i] = read_byte();
            } //end Next 

            for (i = 1; i <= 44; i++)
            {
                Attacks[i].Exp = read_byte();
            } //end Next 
            for (i = 1; i <= 44; i++)
            {
                Attacks[i].Skill = read_byte();
            } //end Next 
            for (i = 1; i <= 44; i++)
            {
                Attacks[i].Defense = read_byte();
                if (Attacks[i].Defense > 128)
                {
                    Attacks[i].Defense = (short)(Attacks[i].Defense - 256);
                }
            } //end Next 
            for (i = 1; i <= 44; i++)
            {
                Attacks[i].Stamina = read_byte();
            } //end Next 
            for (i = 1; i <= 44; i++)
            {
                Attacks[i].HitProb = read_byte();
            } //end Next 
            for (i = 1; i <= 44; i++)
            {
                Attacks[i].Damage = read_byte();
            } //end Next 
            for (i = 1; i <= 44; i++)
            {
                Attacks[i].Fatigue = read_byte();
            } //end Next 

            for (i = 1; i <= 44; i++)
            {
                Attacks[i].name = "";
            } //end Next 
            atknum = 1;
            for (i = 1; i <= 300; i++)
            {
                var = read_byte();
                if (var == 0)
                {
                    atknum = (short)(atknum + 1);
                }
                else
                {
                    if (atknum <= 44)
                    {
                        Attacks[atknum].name = Attacks[atknum].name + App.Chr(var);
                    }
                }
            } //end Next 

            for (i = 1; i <= 44; i++)
            {
                Combos[i].attack1 = read_byte();
                Combos[i].attack2 = read_byte();
                Combos[i].attack3 = read_byte();
                Combos[i].byte4 = read_byte();
                Combos[i].byte5 = read_byte();
                Combos[i].byte6 = read_byte();
                Combos[i].unused1 = read_byte();
                Combos[i].unused2 = read_byte();
            } //end Next 

            byte19218 = (short)read_int();

            for (i = 1; i <= 25; i++)
            {
                Spells[i].Rune1 = read_byte();
                Spells[i].Rune2 = read_byte();
                Spells[i].Rune3 = read_byte();
                Spells[i].Rune4 = read_byte();
                Spells[i].Difficulty = read_byte();
                Spells[i].Skill = read_byte();
                Spells[i].word6 = read_int();
            } //end Next 

            for (i = 1; i <= 6; i++)
            {
                RuneMultipliers[i] = read_byte();
            } //end Next 

            for (i = 1; i <= 24; i++)
            {
                Runes[i] = read_byte();
            } //end Next 

        } //End Sub

    } //End Class
}
