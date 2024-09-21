using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.DTOs
{
    public class file559
    {
        private App app;


        public MemoryStream itemdata;
        public int itemloc; //Now 0 based
        private byte[] bytes10340 = new byte[5];//orig 4
        private byte[] bytes10336 = new byte[257]; //orig 256
        private byte[] bytes10080 = new byte[65]; //orig 64
        private byte[] bytes10016 = new byte[257]; //orig 256
        private Tdoor[] Doors = ArrayUt<Tdoor>.NewArray1(4); 
        private monsterdrop[] MonsterDrops = ArrayUt<monsterdrop>.NewArray1(40);
        private byte[] soundnums = new byte[9]; //orig 8
        private monster[] Monsters = ArrayUt<monster>.NewArray1(27);
        private int[] foodvalues = new int[9]; //orig 8
        private byte[] bytes8946 = new byte[55]; //misc item weights orig 54
        public int word8892;
        private armor[] armors = ArrayUt<armor>.NewArray1(58);
        private weapon[] weapons = ArrayUt<weapon>.NewArray1(46);
        private obj[] objs = ArrayUt<obj>.NewArray1(180);
        private byte[] bytes7302 = new byte[17]; //orig 16
        private byte[] dbentrysizes = new byte[17]; //orig 16
        private int[] deltaxs = new int[5]; //orig 4
        private int[] deltays = new int[5]; //orig 4

        public file559(App _app)
        {
            app = _app;
        }
        public Tdoor door(short num)
        {
            return Doors[num];
        } //End function
        public monsterdrop monsterdrop(short num)
        {
            return MonsterDrops[num];
        } //End function
        public byte soundnum(short num)
        {
            return soundnums[num];
        } //End function
        public monster monster(short num)
        {
            return Monsters[num];
        } //End function
        public int FoodValue(short num)
        {
            return foodvalues[num];
        } //End function
        public void setfoodvalue(short num, int val_Renamed)
        {
            foodvalues[num] = val_Renamed;
        } //End Sub
        public armor Armor(short num)
        {
            return armors[num];
        } //End function
        public weapon weapon(short num)
        {
            return weapons[num];
        } //End function
        public obj obj(short num)
        {
            return objs[num];
        } //End function
        public byte dbentrysize(short num)
        {
            return dbentrysizes[num];
        } //End function
        public int deltax(short num)
        {
            return deltaxs[num];
        } //End function
        public int deltay(short num)
        {
            return deltays[num];
        } //End function
        public byte byte10340(short num)
        {
            return bytes10340[num];
        } //End function
        public byte byte10336(short num)
        {
            return bytes10336[num];
        } //End function
        public byte byte10080(short num)
        {
            return bytes10080[num];
        } //End function
        public byte byte10016(short num)
        {
            return bytes10016[num];
        } //End function
        public byte byte8946(short num)
        {
            return bytes8946[num];
        } //End function
        public byte byte7302(short num)
        {
            return bytes7302[num];
        } //End function
        public byte read_byte()
        {
            itemdata.Position = itemloc;
            var ans_read_byte = new BinaryReader(itemdata).ReadByte();
            itemloc = itemloc + 1;

            return ans_read_byte;
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
        public void update()
        {
            short i;
            itemdata = new MemoryStream();
            itemloc = 0;
            for (i = 1; i <= 4; i++)
            {
                write_byte(bytes10340[i]);
            } //end Next
            for (i = 1; i <= 256; i++)
            {
                write_byte(bytes10336[i]);
            } //end Next
            for (i = 1; i <= 64; i++)
            {
                write_byte(bytes10080[i]);
            } //end Next
            for (i = 1; i <= 256; i++)
            {
                write_byte(bytes10016[i]);
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                write_byte(Doors[i].Byte0);
                write_byte((Doors[i].Strength));
            } //end Next
            for (i = 1; i <= 40; i++)
            {
                write_int(MonsterDrops[i].word0);
            } //end Next
            for (i = 1; i <= 8; i++)
            {
                write_byte(soundnums[i]);
            } //end Next
            for (i = 1; i <= 27; i++)
            {
                write_byte((Monsters[i].IdNum));
                write_byte((Monsters[i].attacksound));
                write_int((Monsters[i].Word2));
                write_int((Monsters[i].Word4));
                write_byte((Monsters[i].Speed));
                write_byte((Monsters[i].MinSpeed));
                write_byte((Monsters[i].Armor_Renamed));
                write_byte((Monsters[i].BaseHealth));
                write_byte((Monsters[i].quickness));
                write_byte((Monsters[i].Poison));
                write_byte((Monsters[i].power));
                write_byte((Monsters[i].Unused1));
                write_int((Monsters[i].word14));
                write_int((Monsters[i].word16));
                write_int((Monsters[i].word18));
                write_int((Monsters[i].word20));
                write_byte((Monsters[i].byte22));
                write_byte((Monsters[i].byte23));
                write_byte((Monsters[i].damagetype));
                write_byte((Monsters[i].Unused2));
            } //end Next  //702
            for (i = 1; i <= 8; i++)
            {
                write_int(foodvalues[i]);
            } //end Next
            for (i = 1; i <= 54; i++)
            {
                write_byte(bytes8946[i]);
            } //end Next
            write_int(word8892);
            for (i = 1; i <= 58; i++)
            {
                write_byte((armors[i].Weight));
                write_byte((armors[i].armor_Renamed));
                write_byte((armors[i].Byte2));
                write_byte((armors[i].Unused1));
            } //end Next  //232
            for (i = 1; i <= 46; i++)
            {
                write_byte((weapons[i].Weight));
                write_byte((weapons[i].Byte1));
                write_byte((weapons[i].Damage));
                write_byte((weapons[i].Distance));
                write_int((weapons[i].Word4));
            } //end Next  //276
            for (i = 1; i <= 180; i++)
            {
                write_int((objs[i].ItemType));
                write_byte((objs[i].FloorGraphic));
                write_byte((objs[i].Combos));
                write_int((objs[i].WearLocs));
            } //end Next  //1080
            for (i = 1; i <= 16; i++)
            {
                write_byte(bytes7302[i]);
            } //end Next
            for (i = 1; i <= 16; i++)
            {
                write_byte(dbentrysizes[i]);
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                write_int(deltaxs[i]);
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                write_int(deltays[i]);
            } //end Next
            /*        'graphics.setitemsize 560, LenB(itemdata)*/
            /*        'graphics.setitemstr 560, itemdata*/


            app.graphics.setgitemsize(559, (int)itemdata.Length);

            app.graphics.setgitemdata(559, itemdata);
        } //End Sub
        public void read()
        {
            short i;
            itemloc = 0;
            for (i = 1; i <= 4; i++)
            {
                bytes10340[i] = read_byte();
            } //end Next
            for (i = 1; i <= 256; i++)
            {
                bytes10336[i] = read_byte();
            } //end Next
            for (i = 1; i <= 64; i++)
            {
                bytes10080[i] = read_byte();
            } //end Next
            for (i = 1; i <= 256; i++)
            {
                bytes10016[i] = read_byte();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                Doors[i].Byte0 = read_byte();
                Doors[i].Strength = read_byte();
            } //end Next
            for (i = 1; i <= 40; i++)
            {
                MonsterDrops[i].word0 = read_int();
            } //end Next
            for (i = 1; i <= 8; i++)
            {
                soundnums[i] = read_byte();
            } //end Next
            for (i = 1; i <= 27; i++)
            {
                Monsters[i].IdNum = read_byte();
                Monsters[i].attacksound = read_byte();
                Monsters[i].Word2 = read_int();
                Monsters[i].Word4 = read_int();
                Monsters[i].Speed = read_byte();
                Monsters[i].MinSpeed = read_byte();
                Monsters[i].Armor_Renamed = read_byte();
                Monsters[i].BaseHealth = read_byte();
                Monsters[i].quickness = read_byte();
                Monsters[i].Poison = read_byte();
                Monsters[i].power = read_byte();
                Monsters[i].Unused1 = read_byte();
                Monsters[i].word14 = read_int();
                Monsters[i].word16 = read_int();
                Monsters[i].word18 = read_int();
                Monsters[i].word20 = read_int();
                Monsters[i].byte22 = read_byte();
                Monsters[i].byte23 = read_byte();
                Monsters[i].damagetype = read_byte();
                Monsters[i].Unused2 = read_byte();
            } //end Next  //702
            for (i = 1; i <= 8; i++)
            {
                foodvalues[i] = read_int();
            } //end Next
            for (i = 1; i <= 54; i++)
            {
                bytes8946[i] = read_byte();
            } //end Next
            word8892 = read_int();
            for (i = 1; i <= 58; i++)
            {
                armors[i].Weight = read_byte();
                armors[i].armor_Renamed = read_byte();
                armors[i].Byte2 = read_byte();
                armors[i].Unused1 = read_byte();
            } //end Next  //232
            for (i = 1; i <= 46; i++)
            {
                weapons[i].Weight = read_byte();
                weapons[i].Byte1 = read_byte();
                weapons[i].Damage = read_byte();
                weapons[i].Distance = read_byte();
                weapons[i].Word4 = read_int();
            } //end Next  //276
            for (i = 1; i <= 180; i++)
            {
                objs[i].ItemType = read_int();
                objs[i].FloorGraphic = read_byte();
                objs[i].Combos = read_byte();
                objs[i].WearLocs = read_int();
            } //end Next  //1080
            for (i = 1; i <= 16; i++)
            {
                bytes7302[i] = read_byte();
            } //end Next
            for (i = 1; i <= 16; i++)
            {
                dbentrysizes[i] = read_byte();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                deltaxs[i] = read_int();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                deltays[i] = read_int();
            } //end Next
        } //End Sub
        /*    '  i8  Byte10340[4];*/
        /*    '  i8  Byte10336[256]; //       ...*/
        /*    '  i8  Byte10080[64];  //       ...*/
        /*    '  i8  Byte10016[256]; //       ...*/
        /*    '  i16 DoorCharacteristics[4]; //9760// swapped when read.*/
        /*    '  i16 MonsterDroppings[40];   //9752//*/
        /*    '  ui8 uByte9672[8];   // Sound numbers???*/
        /*    '  ITEM26 MonsterDescriptor[27];//9664*/
        /*    '  //ITEM26 Item9664[27];//*/
        /*    '  i16 FoodValue[8];//8962 // obj_Apple to obj_DragonSteak      ...*/
        /*    '                   // swapped when read*/
        /*    '  i8  Byte8946[54];   //*/
        /*    '  i16 Word8892;       //       ...*/
        /*    '  CLOTHINGDESC ClothingDesc[58]; //8890*/
        /*    '  WEAPONDESC weapons[46];//8658   // Used for Database5 objects*/
        /*    '                      // word4 swapped when read;*/
        /*    '                      // pDB5->weaponType() is index*/
        /*    '  OBJDESC ObjDesc[180];//8382*/
        /*    '            // -object Type*/
        /*    '            // -effect of objects when attacking?*/
        /*    '            // -Words 0 and 4 swapped when read*/
        /*    '            // -*/
        /*    '            // -Word4 bit 0 = consumable??*/
        /*    '            // -indexed by Object Index*/
        /*    '            //    = scroll type   +  0 //  1 Scroll type*/
        /*    '            //    = chest type    +  1 //  1 Chest types*/
        /*    '            //    = potion type   +  2 // 21 Potion types*/
        /*    '            //    = weapon type   + 23 // 46 Weapon types*/
        /*    '            //    = clothing type + 69 // 58 Clothing types*/
        /*    '            //    = food type     +127 // 53 Misc types*/
        /*    '*/
        /*    '  i8  Byte7302[16];   //  #extra entries in each 'misc' database     ...*/
        /*    '  i8  DBEntrySize[16];//7286 // Size of entries in each 'misc' database     ...*/
        /*    '                             // Always an even number! Sometimes zero.*/
        /*    '  i16 DeltaY[4]; //7270  // deltay for 4 directions*/
        /*    '  i16 DeltaX[4]; //7262    // deltax for 4 directions End of 22f*/
    }
}
