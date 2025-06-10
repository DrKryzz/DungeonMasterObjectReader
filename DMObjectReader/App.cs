using DMObjectReader.Editors;
using DMObjectReader.Helpers;
using DMObjectReader.MiscClasses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMObjectReader
{
    public class App
    {
        public const short BI_RGB = 0;
        public const int CBM_INIT = 0x4;
        public const short DIB_RGB_COLORS = 0;
        public const int SRCCOPY = 0xCC0020;
        public int counter = 0;
        public MemoryStream itemdata;
        public int BitsInBuffer;
        public int LZWByteIndex;
        public int BitBuffer;




        /*TODO ADD ALL REFERENCES*/
        public ProgressWindow dlg_progresswindow = new ProgressWindow();
        public Tgraphics graphics;
        public Tmapfile mapfile;

        public ImageList JewelIL = new ImageList();
        private string[] CSBWinObjNames = new string[180];

        //editors
        public ItemTypeEditor dlg_itemtypeeditor; // = new ItemTypeEditor(this);
        public ObjectListForm dlg_objectlist; // = new ObjectListForm(this);
        /*TODO ADD ALL REFERENCES*/

        public App()
        {
            dlg_progresswindow = new ProgressWindow();
            graphics = new Tgraphics(this);


            InitCSBWinObjNames();
            mapfile = new Tmapfile();

            // Initialize editors before the main window is created
            // Initialize when called otherwise it will be null
            //dlg_itemtypeeditor = new ItemTypeEditor(this);

            MainWindow window = new MainWindow(this);
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.Run(window);

            
        }

        /*LOST OF CODE MISSING HERE NEED MORE LATER*/

        public void InitCSBWinObjNames()
        {
            int i;
            i = 0; //hoppsan här stod det 1, men det skulle vara 0, detta för att vb är 1 baserat ibland
            CSBWinObjNames[i] = "OPEN SCROLL";
            i = i + 1;
            CSBWinObjNames[i] = "CHEST";
            i = i + 1;
            CSBWinObjNames[i] = "MON POTION(STAMINA)";
            i = i + 1;
            CSBWinObjNames[i] = "UM POTION";
            i = i + 1;
            CSBWinObjNames[i] = "DES POTION";
            i = i + 1;
            CSBWinObjNames[i] = "VEN POTION(POISON)";
            i = i + 1;
            CSBWinObjNames[i] = "SAR POTION";
            i = i + 1;
            CSBWinObjNames[i] = "ZO POTION";
            i = i + 1;
            CSBWinObjNames[i] = "ROS POTION(DEXTERITY)";
            i = i + 1;
            CSBWinObjNames[i] = "KU POTION(STRENGTH)";
            i = i + 1;
            CSBWinObjNames[i] = "DANE POTION(WISDOM)";
            i = i + 1;
            CSBWinObjNames[i] = "NETA POTION(VITALITY";
            i = i + 1;
            CSBWinObjNames[i] = "ANTIVENIN(CURE POISON)";
            i = i + 1;
            CSBWinObjNames[i] = "MON POTION(STAMINA)";
            i = i + 1;
            CSBWinObjNames[i] = "YA POTION(MAGIC SHIELD)";
            i = i + 1;
            CSBWinObjNames[i] = "EE POTION(MANA)";
            i = i + 1;
            CSBWinObjNames[i] = "VI POTION(HEALTH)";
            i = i + 1;
            CSBWinObjNames[i] = "WATER FLASK";
            i = i + 1;
            CSBWinObjNames[i] = "KATH BOMB";
            i = i + 1;
            CSBWinObjNames[i] = "PEW BOMB";
            i = i + 1;
            CSBWinObjNames[i] = "RA BOMB";
            i = i + 1;
            CSBWinObjNames[i] = "FUL BOMB";
            i = i + 1;
            CSBWinObjNames[i] = "EMPTY FLASK";
            i = i + 1;
            CSBWinObjNames[i] = "EYE OF TIME";
            i = i + 1;
            CSBWinObjNames[i] = "STORMRING";
            i = i + 1;
            CSBWinObjNames[i] = "TORCH";
            i = i + 1;
            CSBWinObjNames[i] = "FLAMITT";
            i = i + 1;
            CSBWinObjNames[i] = "STAFF OF CLAWS";
            i = i + 1;
            CSBWinObjNames[i] = "STORM";
            i = i + 1;
            CSBWinObjNames[i] = "RA BLADE";
            i = i + 1;
            CSBWinObjNames[i] = "THE FIRESTAFF";
            i = i + 1;
            CSBWinObjNames[i] = "DAGGER";
            i = i + 1;
            CSBWinObjNames[i] = "FALCHION";
            i = i + 1;
            CSBWinObjNames[i] = "SWORD";
            i = i + 1;
            CSBWinObjNames[i] = "RAPIER";
            i = i + 1;
            CSBWinObjNames[i] = "BITER";
            i = i + 1;
            CSBWinObjNames[i] = "SAMURAI SWORD";
            i = i + 1;
            CSBWinObjNames[i] = "SIDE SPLITTER";
            i = i + 1;
            CSBWinObjNames[i] = "DIAMOND EDGE";
            i = i + 1;
            CSBWinObjNames[i] = "VORPAL BLADE";
            i = i + 1;
            CSBWinObjNames[i] = "DRAGON FANG";
            i = i + 1;
            CSBWinObjNames[i] = "AXE";
            i = i + 1;
            CSBWinObjNames[i] = "EXECUTIONER";
            i = i + 1;
            CSBWinObjNames[i] = "MACE";
            i = i + 1;
            CSBWinObjNames[i] = "MACE OF ORDER";
            i = i + 1;
            CSBWinObjNames[i] = "MORNINGSTAR";
            i = i + 1;
            CSBWinObjNames[i] = "CLUB";
            i = i + 1;
            CSBWinObjNames[i] = "STONE CLUB";
            i = i + 1;
            CSBWinObjNames[i] = "CLAW BOW";
            i = i + 1;
            CSBWinObjNames[i] = "CROSSBOW";
            i = i + 1;
            CSBWinObjNames[i] = "ARROW";
            i = i + 1;
            CSBWinObjNames[i] = "SLAYER";
            i = i + 1;
            CSBWinObjNames[i] = "SLING";
            i = i + 1;
            CSBWinObjNames[i] = "ROCK";
            i = i + 1;
            CSBWinObjNames[i] = "POISON DART";
            i = i + 1;
            CSBWinObjNames[i] = "THROWING STAR";
            i = i + 1;
            CSBWinObjNames[i] = "STICK";
            i = i + 1;
            CSBWinObjNames[i] = "STAFF";
            i = i + 1;
            CSBWinObjNames[i] = "WAND";
            i = i + 1;
            CSBWinObjNames[i] = "TEOWAND";
            i = i + 1;
            CSBWinObjNames[i] = "YEW STAFF";
            i = i + 1;
            CSBWinObjNames[i] = "STAFF OF IRRA";
            i = i + 1;
            CSBWinObjNames[i] = "CROSS OF NETA";
            i = i + 1;
            CSBWinObjNames[i] = "SERPENT STAFF";
            i = i + 1;
            CSBWinObjNames[i] = "DRAGON SPIT";
            i = i + 1;
            CSBWinObjNames[i] = "SCEPTRE OF LYF";
            i = i + 1;
            CSBWinObjNames[i] = "HORN OF FEAR";
            i = i + 1;
            CSBWinObjNames[i] = "SPEEDBOW";
            i = i + 1;
            CSBWinObjNames[i] = "THE FIRESTAFF+";
            i = i + 1;
            CSBWinObjNames[i] = "CAPE";
            i = i + 1;
            CSBWinObjNames[i] = "CLOAK OF NIGHT";
            i = i + 1;
            CSBWinObjNames[i] = "TATTERED PANTS";
            i = i + 1;
            CSBWinObjNames[i] = "SANDALS";
            i = i + 1;
            CSBWinObjNames[i] = "LEATHER BOOTS";
            i = i + 1;
            CSBWinObjNames[i] = "TATTERED SHIRT";
            i = i + 1;
            CSBWinObjNames[i] = "ROBE";
            i = i + 1;
            CSBWinObjNames[i] = "FINE ROBE(TOP)";
            i = i + 1;
            CSBWinObjNames[i] = "FINE ROBE(BOTTOM)";
            i = i + 1;
            CSBWinObjNames[i] = "KIRTLE";
            i = i + 1;
            CSBWinObjNames[i] = "SILK SHIRT";
            i = i + 1;
            CSBWinObjNames[i] = "TABARD";
            i = i + 1;
            CSBWinObjNames[i] = "GUNNA";
            i = i + 1;
            CSBWinObjNames[i] = "ELVEN DOUBLET";
            i = i + 1;
            CSBWinObjNames[i] = "ELVEN HUKE";
            i = i + 1;
            CSBWinObjNames[i] = "ELVEN BOOTS";
            i = i + 1;
            CSBWinObjNames[i] = "LEATHER JERKIN";
            i = i + 1;
            CSBWinObjNames[i] = "LEATHER PANTS";
            i = i + 1;
            CSBWinObjNames[i] = "SUEDE BOOTS";
            i = i + 1;
            CSBWinObjNames[i] = "BLUE PANTS";
            i = i + 1;
            CSBWinObjNames[i] = "TUNIC";
            i = i + 1;
            CSBWinObjNames[i] = "GHI";
            i = i + 1;
            CSBWinObjNames[i] = "GHI TROUSERS";
            i = i + 1;
            CSBWinObjNames[i] = "CALISTA";
            i = i + 1;
            CSBWinObjNames[i] = "CROWN OF NERRA";
            i = i + 1;
            CSBWinObjNames[i] = "BEZERKER HELM";
            i = i + 1;
            CSBWinObjNames[i] = "HELMET";
            i = i + 1;
            CSBWinObjNames[i] = "BASINET";
            i = i + 1;
            CSBWinObjNames[i] = "NETA SHIELD";
            i = i + 1;
            CSBWinObjNames[i] = "CRYSTAL SHIELD";
            i = i + 1;
            CSBWinObjNames[i] = "WOODEN SHIELD";
            i = i + 1;
            CSBWinObjNames[i] = "SMALL SHIELD";
            i = i + 1;
            CSBWinObjNames[i] = "MAIL AKETON";
            i = i + 1;
            CSBWinObjNames[i] = "LEG MAIL";
            i = i + 1;
            CSBWinObjNames[i] = "MITHRAL AKETON";
            i = i + 1;
            CSBWinObjNames[i] = "MITHRAL MAIL";
            i = i + 1;
            CSBWinObjNames[i] = "CASQUE'N COIF";
            i = i + 1;
            CSBWinObjNames[i] = "HOSEN";
            i = i + 1;
            CSBWinObjNames[i] = "ARMET";
            i = i + 1;
            CSBWinObjNames[i] = "TORSO PLATE";
            i = i + 1;
            CSBWinObjNames[i] = "LEG PLATE";
            i = i + 1;
            CSBWinObjNames[i] = "FOOT PLATE";
            i = i + 1;
            CSBWinObjNames[i] = "SAR SHIELD";
            i = i + 1;
            CSBWinObjNames[i] = "HELM OF RA";
            i = i + 1;
            CSBWinObjNames[i] = "PLATE OF RA";
            i = i + 1;
            CSBWinObjNames[i] = "POLEYN OF RA";
            i = i + 1;
            CSBWinObjNames[i] = "GREAVE OF RA";
            i = i + 1;
            CSBWinObjNames[i] = "SHIELD OF RA";
            i = i + 1;
            CSBWinObjNames[i] = "DRAGON HELM";
            i = i + 1;
            CSBWinObjNames[i] = "DRAGON PLATE";
            i = i + 1;
            CSBWinObjNames[i] = "DRAGON POLEYN";
            i = i + 1;
            CSBWinObjNames[i] = "DRAGON GREAVE";
            i = i + 1;
            CSBWinObjNames[i] = "DRAGON SHIELD";
            i = i + 1;
            CSBWinObjNames[i] = "DEXHELM";
            i = i + 1;
            CSBWinObjNames[i] = "FLAMEBAIN";
            i = i + 1;
            CSBWinObjNames[i] = "POWERTOWERS";
            i = i + 1;
            CSBWinObjNames[i] = "BOOTS OF SPEED";
            i = i + 1;
            CSBWinObjNames[i] = "HALTER";
            i = i + 1;
            CSBWinObjNames[i] = "COMPASS";
            i = i + 1;
            CSBWinObjNames[i] = "WATERSKIN";
            i = i + 1;
            CSBWinObjNames[i] = "JEWEL SYMAL";
            i = i + 1;
            CSBWinObjNames[i] = "ILLUMULET";
            i = i + 1;
            CSBWinObjNames[i] = "ASHES";
            i = i + 1;
            CSBWinObjNames[i] = "BONES(PARTY)";
            i = i + 1;
            CSBWinObjNames[i] = "SAR COIN";
            i = i + 1;
            CSBWinObjNames[i] = "SILVER COIN";
            i = i + 1;
            CSBWinObjNames[i] = "GOR COIN";
            i = i + 1;
            CSBWinObjNames[i] = "IRON KEY";
            i = i + 1;
            CSBWinObjNames[i] = "KEY OF B";
            i = i + 1;
            CSBWinObjNames[i] = "SOLID KEY";
            i = i + 1;
            CSBWinObjNames[i] = "SQUARE KEY";
            i = i + 1;
            CSBWinObjNames[i] = "TOURQUOISE KEY";
            i = i + 1;
            CSBWinObjNames[i] = "CROSS KEY";
            i = i + 1;
            CSBWinObjNames[i] = "ONYX KEY";
            i = i + 1;
            CSBWinObjNames[i] = "SKELETON KEY";
            i = i + 1;
            CSBWinObjNames[i] = "GOLD KEY";
            i = i + 1;
            CSBWinObjNames[i] = "WINGED KEY";
            i = i + 1;
            CSBWinObjNames[i] = "TOPAZ KEY";
            i = i + 1;
            CSBWinObjNames[i] = "SAPPHIRE KEY";
            i = i + 1;
            CSBWinObjNames[i] = "EMERALD KEY";
            i = i + 1;
            CSBWinObjNames[i] = "RUBY KEY";
            i = i + 1;
            CSBWinObjNames[i] = "RA KEY";
            i = i + 1;
            CSBWinObjNames[i] = "MASTER KEY";
            i = i + 1;
            CSBWinObjNames[i] = "BOULDER";
            i = i + 1;
            CSBWinObjNames[i] = "BLUE GEM";
            i = i + 1;
            CSBWinObjNames[i] = "ORANGE GEM";
            i = i + 1;
            CSBWinObjNames[i] = "GREEN GEM";
            i = i + 1;
            CSBWinObjNames[i] = "APPLE";
            i = i + 1;
            CSBWinObjNames[i] = "CORN";
            i = i + 1;
            CSBWinObjNames[i] = "BREAD";
            i = i + 1;
            CSBWinObjNames[i] = "CHEESE";
            i = i + 1;
            CSBWinObjNames[i] = "SCREAMER SLICE";
            i = i + 1;
            CSBWinObjNames[i] = "WORM ROUND";
            i = i + 1;
            CSBWinObjNames[i] = "SHANK";
            i = i + 1;
            CSBWinObjNames[i] = "DRAGON STEAK";
            i = i + 1;
            CSBWinObjNames[i] = "GEM OF AGES";
            i = i + 1;
            CSBWinObjNames[i] = "EKKHARD CROSS";
            i = i + 1;
            CSBWinObjNames[i] = "MOONSTONE";
            i = i + 1;
            CSBWinObjNames[i] = "THE HELLION";
            i = i + 1;
            CSBWinObjNames[i] = "PENDANT FERAL";
            i = i + 1;
            CSBWinObjNames[i] = "MAGIC BOX(BLUE)";
            i = i + 1;
            CSBWinObjNames[i] = "MAGIC BOX(GREEN)";
            i = i + 1;
            CSBWinObjNames[i] = "MIRROR OF DAWN";
            i = i + 1;
            CSBWinObjNames[i] = "ROPE";
            i = i + 1;
            CSBWinObjNames[i] = "RABBIT'S FOOT";
        i = i + 1;
            CSBWinObjNames[i] = "CORBUM";
            i = i + 1;
            CSBWinObjNames[i] = "CHOKER";
            i = i + 1;
            CSBWinObjNames[i] = "LOCK PICKS";
            i = i + 1;
            CSBWinObjNames[i] = "MAGNIFIER";
            i = i + 1;
            CSBWinObjNames[i] = "ZOKATHRA SPELL";
            i = i + 1;
            CSBWinObjNames[i] = "BONES";
        } //End Sub

        public int GetNextLZWCode(int n)
        {
            while (BitsInBuffer < n && LZWByteIndex < itemdata.Length) // If there are enough bits in the buffer to make a code
            {
                itemdata.Position = LZWByteIndex;
                BitBuffer += (itemdata.ReadByte() * (int)Math.Pow(2, BitsInBuffer)); // Add the next character to the left of the BitBuffer (hence the shift to the left)
                BitsInBuffer += 8; // 8 bits were added to the buffer
                LZWByteIndex++; // Go to the next input character
            }

            if (BitsInBuffer >= n) // If there are enough bits in the buffer to make a code
            {
                int result = BitBuffer & ((1 << n) - 1); // Add to the list of codes the code made with the BitsInCode most significant bits in the buffer
                BitBuffer >>= n; // Remove the output bits from the buffer by shifting the buffer to the right
                BitsInBuffer -= n; // The number of bits in the buffer is decreased
                return result;
            }
            else
            {
                return -1;
            }
        }

        //vb to chsarp ai generated
        public string LZWUncompress_Stro()
        {
            var Dictionary = new SortedDictionary<int, string>();
            int i, k, d, x;
            string j, l, LZWString, LZWChar;
            int BitsInCode, OldCode, NewCode;
            string a, b, c;

            // Initialize LZW bit buffer
            BitBuffer = 0;           // The BitBuffer is empty
            BitsInBuffer = 0;        // So there are no bits stored in the buffer
            LZWByteIndex = 0;        // Start uncompressing LZW data at the first character in string
            BitsInCode = 9;
            d = 0;

            string result = "";
            if (itemdata.Length > 1)       // We need at least one 9 bits code, which means at least two bytes
            {
                // Create and populate the dictionary for the LZW algorithm
                for (i = 0; i <= 255; i++)
                {
                    Dictionary.Add(Dictionary.Count, ((char)i).ToString());
                }
                Dictionary.Add(256, "Flush Dictionary");
                d = Dictionary.Count;
                OldCode = GetNextLZWCode(BitsInCode);                // Get the first code
                result = Dictionary[OldCode];            // Output the string (which is a character) associated with the first code
                LZWChar = Dictionary[OldCode];              // Get the string associated with the first code

                NewCode = GetNextLZWCode(BitsInCode);
                while (NewCode != -1)
                {
                    if (NewCode != 256)                  // If the code is not the special value 256
                    {
                        if (Dictionary.ContainsKey(NewCode))      // If the current code is already in the dictionary
                        {
                            LZWString = Dictionary[NewCode];    // Get the string associated with that current code
                        }
                        else
                        {
                            LZWString = Dictionary[OldCode];    // Get the string associated with the previously read code
                            LZWString = LZWString + LZWChar;     // Add the latest input character read to the string
                        }
                        result += LZWString;   // Output the string
                        LZWChar = LZWString.Substring(0, 1);            // Get the first character of the string
                        Dictionary.Add(d, (Dictionary[OldCode] + LZWChar));  // Add the new string to the dictionary
                        d++;
                        if (BitsInCode < 12)
                        {
                            if (d == Math.Pow(2, BitsInCode))   // If there are not anymore enough bits in a code to store the code's value d == 2^BitsInCode
                            {
                                BitsInCode++;     // Add a bit to the code length
                            }
                        }

                        OldCode = NewCode;               // Go on to next code
                    }
                    else
                    {
                        Dictionary.Clear();
                        for (i = 0; i <= 255; i++)
                        {
                            Dictionary.Add(Dictionary.Count, ((char)i).ToString());
                        }
                        Dictionary.Add(256, "Flush Dictionary");
                        d = Dictionary.Count;
                        BitsInCode = 9;
                    }
                    NewCode = GetNextLZWCode(BitsInCode);
                }

                i = 0; //used to be 1 could this be our culprit?

                //store result in d:\temp\dmobjectreader\
                string thepath;
                thepath = "d:\\temp\\dmobjectreader\\precode.dat";
                //System.IO.File.WriteAllText(thepath, result);

                while (i < result.Length)
                {
                    j = result.Substring(i, 1);
                    //System.IO.File.WriteAllText(thepath, j);
                    if (char.ConvertToUtf32(j, 0) == 144)
                    {
                        Console.WriteLine("found 0x90 byte");
                        k = char.ConvertToUtf32(result.Substring(i + 1, 1), 0);
                        if (k == 0)
                        {
                            Console.WriteLine("item in lzw if k=0 statement");
                            // Remove the '00h' byte
                            //string left = result.Substring(0, i+1); //used to be i
                            //int left_lentgh = left.Length;
                            //Left
                            //System.IO.File.WriteAllText(thepath, left);

                            //string right = result.Substring(i + 2); //used to be i+2
                            //int right_length = right.Length;
                            //right
                            //System.IO.File.WriteAllText(thepath, right);

                            result = result.Substring(0, i+1) + result.Substring(i + 2); //used to be i respective i+2
                            int result_length = result.Length;
                            i++;

                            //total
                            //System.IO.File.WriteAllText(thepath, result);
                        }
                        else
                        {
                            Console.WriteLine($"item in lzw else statement (k={k})");
                            l = result.Substring(i - 1, 1);

                            a = result.Substring(0, i); //used to be i - 1

                            //b = new string(l[0], k - 1);
                            b = "";
                            for (x = 0; x < k - 1; x++)  //some kind of filler
                            {
                                b = b + l;
                            }
                        
                            if (b.Length != k - 1)
                            {
                                Console.WriteLine($"lenb(b) != k-1 ({b.Length} != {k - 1})");
                            }
                            c = result.Substring(i + 2); //used to be i + 2

                            result = a + b + c;
                            i += k - 1;
                        }
                    }
                    else
                    {
                        i++;
                    }
                }
                int test = result.Length;
            }
            return result;
        }

        //public MemoryStream LZWUncompress()
        //{
        //    string s = LZWUncompress_Stro();
        //    var w = new MemoryStream();
        //    int x;
        //    var loopTo = s.Length - 1;
        //    for (x = 0; x <= loopTo; x++)
        //        w.WriteByte((byte)Strings.AscW(s[x]));
        //    // Move file pointer to begin of file!
        //    w.Position = 0L;
        //    return w;
        //}

        public MemoryStream LZWUncompress()
        {
            string s = LZWUncompress_Stro();
            MemoryStream w = new MemoryStream();
            for (int x = 0; x < s.Length; x++)
            {
                w.WriteByte((byte)s[x]);
            }
            // Move file pointer to begin of file!
            w.Position = 0;
            return w;
        }

        public static byte[] gread_bytes(int length, Stream fnum)
        {
            return new BinaryReader(fnum).ReadBytes(length);
        }

        public static byte gread_byte(Stream fnum)
        {
            return new BinaryReader(fnum).ReadByte();
        }

        /// <summary>
        /// Reads 2 bytes as a Word and returns the result as an int
        /// </summary>
        /// <param name="fnum"></param>
        /// <returns></returns>
        public static int gread_int(Stream fnum)
        {
            //because of the little endian big endian i dm we read the least significant bit first?
            //could also read as [word] 2 bytes = 1 word and split in bit-shift
            byte b1;
            byte b2;
            b2 = gread_byte(fnum);
            b1 = gread_byte(fnum);

            int result = b2;
            result *= 256;
            result += b1;

            return result;
        }

        public static void dprint(string a) { }

        public static string ZeroStr(int nr, int width)
        {
            return nr.ToString().PadLeft(width, '0');
        }

        public static Bitmap CreatePic(int cx, int cy, Color[] pal256, byte[] pixels)
        {
            Bitmap pic = new Bitmap(cx, cy, PixelFormat.Format8bppIndexed);
            ColorPalette pal = pic.Palette;
            for (int c = 0; c < 256; c++) pal.Entries[c] = pal256[c];
            pic.Palette = pal;

            BitmapData bd = pic.LockBits(new Rectangle(0, 0, cx, cy), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            try
            {
                Marshal.Copy(pixels, 0, bd.Scan0, Math.Min(bd.Stride * cy, pixels.Length));
            }
            finally
            {
                pic.UnlockBits(bd);
            }

            return pic;
        }


        public static string CStr(params string[] input)
        {
            return string.Join("", input);
        }

        /// <summary>
        /// Takes a char or string and returns the int representation of that number
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int AscW(char input)
        {
            //var first = Microsoft.VisualBasic.Strings.AscW(input);
            var second = Convert.ToInt32(input);
            return second;
        }
        public static int AscW(string input, int num)
        {
            //var first = Microsoft.VisualBasic.Strings.AscW(input);
            var second = Convert.ToInt32(input[num]);
            return second;
        }

        public static int AscW(string input)
        {
            //var first = Microsoft.VisualBasic.Strings.AscW(input);
            //if (input.Length == 0)
            //    return 0;
            var second = Convert.ToInt32(input[0]);
            return second;
        }
        public static string Left(string input, int nCount)
        {
            return input.Substring(0, nCount);
        }

        public static string Mid(string input, int index, int nCount)
        {
            return input.Substring(index, nCount);
        }

        public static string Right(string input, int nCount)
        {
            return input.Substring(input.Length - nCount, nCount);
        }
        public static char Chr(int input)
        {
            return (char)input;
        }
        public static int Len(string input)
        {
            return input.Length;
        }
        public static string Str(byte input)
        {
            return input.ToString();
        }
        public static string Str(short input)
        {
            return input.ToString();
        }
        public static string Str(int input)
        {
            return input.ToString();
        }
        public static string Trim(string input)
        {
            return input.Trim();
        }
        public static byte CByte(int val)
        {
            return (byte)val;
        }
        public static ushort CUShort(int val)
        {
            return (ushort)val;
        }
        public static short CShort(int val)
        {
            return (byte)val;
        }

        public static int CInt(byte val)
        {
            return (int)val;
        }
        public static int CInt(long val)
        {
            return (int)val;
        }

        public static string ZeroHex(short num, int count)
        {
            int i;
            string zerohex = num.ToString("X").Trim();
            i = zerohex.Length;
            i = count - zerohex.Length;
            if (i > 0)
            {
                zerohex = new string('0', i) + zerohex;
            }
            return zerohex;
        }
        public static string ZeroHex(int num, int count)
        {
            int i;
            string zerohex = num.ToString("X").Trim();
            i = zerohex.Length;
            i = count - zerohex.Length;
            if (i > 0)
            {
                zerohex = new string('0', i) + zerohex;
            }
            return zerohex;
        }
        public static string ZeroHex(int num, short count)
        {
            int i;
            string zerohex = num.ToString("X").Trim();
            i = zerohex.Length;
            i = count - zerohex.Length;
            if (i > 0)
            {
                zerohex = new string('0', i) + zerohex;
            }
            return zerohex;
        }
        public static string ZeroHex(short num, short count)
        {
            short i;
            string zerohex = num.ToString("X").Trim();
            i = (short)zerohex.Length;
            i = (short)(count - zerohex.Length);
            if (i > 0)
            {
                zerohex = new string('0', i) + zerohex;
            }
            return zerohex;
        }

        internal void MsgBox(string message)
        {
            MessageBox.Show(message, "DMObjectReader", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }//end of class

    public static class ArrayExtender
    {
        public static T[] ToOneDimensionArray<T>(this T[,] array2D, int row)
        {
            if (row < 0 || row >= array2D.GetLength(0))
            {
                throw new ArgumentOutOfRangeException(nameof(row), "Row index is out of range.");
            }

            int numberOfColumns = array2D.GetLength(1);
            T[] result = new T[numberOfColumns];

            for (int col = 0; col < numberOfColumns; col++)
            {
                result[col] = array2D[row, col];
            }

            return result;
        }

        public static T[] Join<T>(this T[]a, T[] b)
        {
            int totalLength = a.Length + b.Length;
            T[] result = new T[totalLength];
            int currentPosition = 0;
            Array.Copy(a, 0, result, currentPosition, a.Length);
            currentPosition += a.Length;
            Array.Copy(b, 0, result, currentPosition, b.Length);

            return result;
        }

        //// Method to join multiple arrays using params
        //public static T[] Join<T>(params T[][] arrays)
        //{
        //    // Calculate total length of the resulting array
        //    int totalLength = arrays.Sum(arr => arr.Length);

        //    // Create a new array with the total length
        //    T[] result = new T[totalLength];

        //    // Track the current position in the result array
        //    int currentPosition = 0;

        //    // Copy each array into the result array
        //    foreach (T[] array in arrays)
        //    {
        //        Array.Copy(array, 0, result, currentPosition, array.Length);
        //        currentPosition += array.Length;
        //    }

        //    return result;
        //}
    }

    public static class ArrayUt<T> where T : new()
    {
        public static T[] NewArray1(int size1)
        {
            size1++;
            T[] res = new T[size1];
            for (int x = 0; x < size1; x++)
                res[x] = new T();

            return res;
        }
        public static T[,] NewArray2(int size1, int size2)
        {
            size1++;
            size2++;
            T[,] res = new T[size1, size2];
            for (int x = 0; x < size1; x++)
                for (int y = 0; y < size2; y++)
                    res[x, y] = new T();

            return res;
        }
        public static T[][] NewJagArray2(int size1, int size2)
        {
            size1++;
            size2++;
            T[][] res = new T[size1][];
            for (int x = 0; x < size1; x++)
            {
                res[x] = new T[size2];
                for (int y = 0; y < size2; y++)
                {
                    res[x][y] = new T();
                }
            }
            return res;
        }
    }

}
