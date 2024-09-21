using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.DTOs
{
	public class file558
	{
		public int itemlen; //should be 4720 in uncompressed mode
		private App app;
		public file558(App _app)
		{
			app = _app;
		}
		private int[] word7246 = new int[133]; //132 orig

		private drawlocgroup[,] creaturegroups = ArrayUt<drawlocgroup>.NewArray2(4, 12); //3, 11 orig
		/*	'-- 1st index --*/
		/*	'1 = group 0*/
		/*	'2 = group 1*/
		/*	'3 = group 2*/
		/*	'-- 2nd index --*/
		/*	'1 = D3 Center*/
		/*	'2 = D3 Left*/
		/*	'3 = D3 Right*/
		/*	'4 = D2 Center*/
		/*	'5 = D2 Left*/
		/*	'6 = D2 Right*/
		/*	'7 = D1 Center*/
		/*	'8 = D1 Left*/
		/*	'9 = D1 Right*/
		/*	'10= D0 Left*/
		/*	'11= D0 Right*/
		/*	' D0 Center = Player's location*/

		private byte[] byte6652 = new byte[25]; //24 orig
		private byte[] d2monsterpalettes = new byte[17]; //16 orig
		private byte[] d3monsterpalettes = new byte[17]; //16 orig
		private int[,] palettes = new int[14, 8]; //13, 7 orig



		private monstergraphicinfo[] MonsterGraphicInfos = ArrayUt<monstergraphicinfo>.NewArray1(27);

		private drawlocgroup[,] flooritemgroups = ArrayUt<drawlocgroup>.NewArray2(3, 10);
		/*	'-- 1st index --*/
		/*	'1 = group 0*/
		/*	'2 = group 1*/
		/*	'3 = group 2*/
		/*	'-- 2nd index --*/
		/*	'1 = D3 Center*/
		/*	'2 = D3 Left*/
		/*	'3 = D3 Right*/
		/*	'4 = D2 Center*/
		/*	'5 = D2 Left*/
		/*	'6 = D2 Right*/
		/*	'7 = D1 Center*/
		/*	'8 = D1 Left*/
		/*	'9 = D1 Right*/
		/*	'10= D0 Center*/
		/*	' D0 Right and Left are always invisible*/

		private byte[] byte5790 = new byte[47]; //46 orig
		private byte[] d2flooritempalettes = new byte[17]; //16 orig
		private byte[] d3flooritempalettes = new byte[17]; //16 orig
		private byte[] byte5712 = new byte[25]; //24 orig

		private missilegraphicinfo[] missileGraphicInfos = ArrayUt<missilegraphicinfo>.NewArray1(14);
		private itemgraphicinfo[] itemGraphicInfos = ArrayUt<itemgraphicinfo>.NewArray1(85);
		private decorationinfo[] buttonlocs = ArrayUt<decorationinfo>.NewArray1(4);
		/*	'1 = D3 center*/
		/*	'2 = D3 left*/
		/*	'3 = D2 center*/
		/*	'4 = d1 center*/
		/*	'note: x1, x2, y1, y2 are from top/left of view port*/
		/*	' the rest are invisible anyway....*/


		private decorationinfo[,] doordecorationinfos = ArrayUt<decorationinfo>.NewArray2(5, 4); //4, 3 orig
		/*	'-- 1st index --*/
		/*	'1 = group0*/
		/*	'2 = group1*/
		/*	'3 = group2*/
		/*	'4 = group3*/
		/*	'-- 2nd index --*/
		/*	'1 = D3*/
		/*	'2 = D2*/
		/*	'3 = D1*/
		/*	'note: x1, x2, y1, y2 are from top/left of the DOOR GRAPHIC*/


		private decorationinfo[,] floordecorationinfos = ArrayUt<decorationinfo>.NewArray2(4, 10); //3, 9 orig
		/*	'-- 1st index --*/
		/*	'1 = group0*/
		/*	'2 = group1*/
		/*	'3 = group2*/
		/*	'-- 2nd index --*/
		/*	'1 = D3 Left*/
		/*	'2 = D3 Center*/
		/*	'3 = D3 Right*/
		/*	'4 = D2 Left*/
		/*	'5 = D2 Center*/
		/*	'6 = D2 Right*/
		/*	'7 = D1 Left*/
		/*	'8 = D1 Center*/
		/*	'9 = D1 Right*/


		private decorationinfo[,] walldecorationinfos = ArrayUt<decorationinfo>.NewArray2(9, 14); //8,13
		/*	'-- 1st index --*/
		/*	' group number*/
		/*	' -- 2nd index --*/
		/*	'1 = D3 leftside wall*/
		/*	'2 = D3 rightside wall*/
		/*	'3 = D3 left wall*/
		/*	'4 = D3 center wall*/
		/*	'5 = D3 right wall*/
		/*	'6 = D2 leftside wall*/
		/*	'7 = D2 rightside wall*/
		/*	'8 = D2 left wall*/
		/*	'9 = D2 center wall*/
		/*	'10= D2 right wall*/
		/*	'11= D1 leftside wall*/
		/*	'12= D1 rightside wall*/
		/*	'13= D1 center wall*/

		/*	'Private Byte4213(1686) As Byte ' rest of the file*/

		private byte[] illegibletextcutofflocations = new byte[17]; //16 orig
		/*	'  i8  Byte4213[16];  //      Illegible text cutoff locations*/
		/*	'[0] D3 side cutoff if 1 line*/
		/*	'[1] D3 side cutoff if 1 line*/
		/*	'[2] D3 side cutoff if 1 line*/
		/*	'[3] D3 front cutoff if 1 line*/
		/*	'[4] D3 front cutoff if 1 line*/
		/*	'[5] D3 front cutoff if 1 line*/
		/*	'[6] D2 side cutoff if 1 line*/
		/*	'[7] D2 side cutoff if 1 line*/
		/*	'[8] D2 side cutoff if 1 line*/
		/*	'[9] D2 front cutoff if 1 line*/
		/*	'[10] D2 front cutoff if 1 line*/
		/*	'[11] D2 front cutoff if 1 line*/
		/*	'[12] D1 side cutoff if 1 line*/
		/*	'[13] D1 side cutoff if 1 line*/
		/*	'[14] D1 side cutoff if 1 line*/
		/*	' ???  Note D1 front is not illegible text, it is legible.*/
		private byte[] legibletextlocations = new byte[5]; //4 orig

		/*	'  i8  Byte4196[4];   //      Y locations of where to put legible text*/
		/*	' [0] = first line, [1] = second line, etc.*/
		private byte[] byte4192 = new byte[5]; //4 orig
		/*	'  i8  Byte4192[4];   //      ??? uses graphic #86 to mask something for text?*/
		private byte[] d2doordecorationpalettes = new byte[17]; //editable 16 orig
		/*	'  i8  Byte4188[16];  //      D2 Custom Palette for door decorations*/
		private byte[] d3doordecorationpalettes = new byte[17]; //editable 16 orig
		/*	'  i8  Byte4172[16];  //      D3 custom palette for door decorations*/
		private byte[] d2walldecorationpalettes = new byte[17]; //editable 16 orig
		/*	'  i8  Byte4156[16];  //      D2 Custom Palette for wall decorations*/
		private byte[] d3walldecorationpalettes = new byte[17]; //editable 16 orig
		/*	'  i8  Byte4140[16];  //      D3 Custom Palette for wall decorations*/
		private byte[] buttondecorationnumbers = new byte[3]; //not editable, never will be. 2 orig
		/*	'  i8  Byte4124[2];   //      ??? Some button placements? (0x0000)*/
		private byte[] doordecorationnumbers = new byte[13]; //editable 12 orig
		/*	'  i8  Byte4122[12];  //      Door decoration group numbers*/
		private byte[] floordecorationnumbers = new byte[11]; //editable 10 orig
		/*	'  i8  Byte4110[10];  //      Floor decoration group numbers*/
		private byte[] walldecorationnumbers = new byte[61]; //editable 60 orig
		/*	'  i8  Byte4100[60];  //      Wall decoration group numbers*/
		private int watergraphicnum;
		/*	'  i16 Word4040[1];   //      graphic number of 'water' giving wall decoration*/
		private byte[] alcovegraphicnums = new byte[5]; //4 orig
		/*	'  i8  Byte4038[4];   //      ... These are wall graphics that define*/
		/*	'                     //          alcoves.  The numbers here are 1, 2, and 3*/
		/*	'                     //          in CSB.  Square Alcove, VI Altar, Ornate Alcove.*/
		private byte[] floordecorationoffsets = new byte[11]; //editable 10 orig
		/*	'  i8  Byte4034[10];  //      some sort of floor decoration offset for which graphic to display*/
		private byte[] illegibletextgroups = new byte[13]; //12 orig
		/*	'  i8  Byte4024[12];  //      12 bytes. one byte for each 'distance' from player.*/
		/*	' defines the groups for Byte4213.*/
		/*	' group = byte4024[distanceFromPlayer] * ;*/
		/*	' Then use Byte4213[group] as the base for cutoff.*/
		private int word4012;
		/*	'  i16 Word4012;      //      unused, set to 3217 during runtime.*/

		private animationinfo[] animationinfos = ArrayUt<animationinfo>.NewArray1(12);
		/*	' START UNKNOWN GRAPHIC ITEMS:*/
		/*	' Structure for all is:*/

		/*	'[0] + 63 = graphic address*/
		/*	' [1] + graphicrandom(2)? = some offset, maybe where to 'lock' the graphic*/
		/*	' [2] = some sort of static palette info, maybe transparent pixel*/
		/*	' [3] & 0x7F + 69 = graphicnumber*/
		/*	'if [3] = 255, a2 = null*/
		/*	' [4] = width*/
		/*	' [5] = height*/
		/*	' [6] = src offset x*/
		/*	' [7] = src offset y*/

		/*	'bytes4010 to 3922 = teleporter graphics?*/
		/*	'  i8  Byte4010[8];   //      ... corresponds to byte3074*/
		/*	'  i8  Byte4002[8];   //      ...*/
		/*	'  i8  Byte3994[8];   //      ...*/
		/*	'  i8  Byte3986[8];   //      ...*/
		/*	'  i8  Byte3978[8];   //      ...*/
		/*	'  i8  Byte3970[8];   //      ...*/
		/*	'  i8  Byte3962[8];   //      ...*/
		/*	'  i8  Byte3954[8];   //      ...*/
		/*	'  i8  Byte3946[8];   //      ...*/
		/*	'  i8  Byte3938[8];   //      ...*/
		/*	'  i8  Byte3930[8];   //      ...*/
		/*	'  i8  Byte3922[8];   //      ...*/


		private graphicrect[] graphicrects = ArrayUt<graphicrect>.NewArray1(172); //171 orig
		/*	' door graphic information*/
		/*	'  These are 8 byte rect positions, but the positions are in bytes, not words.*/
		/*	'  Thus the structure is of*/
		/*	'    { byte x1, y1, x2, y2, width, height, offsetx, offsety }*/

		/*	'  i8  Byte3914[80];  //      door stuff. 10 groups of 8  D1R*/
		/*	' group 0 = closed*/
		/*	' group 1-3 = opens up/down*/
		/*	' 1 = 1/4th way down*/
		/*	' 2 = 1/2 way down*/
		/*	' 3 = 3/4ths way down*/
		/*	' group 2-9 = 'graphics' as door is opening sideways*/
		/*	' 2 = left side 1/4th way closed*/
		/*	' 3 = left side 1/2 way closed*/
		/*	' 4 = left side 3/4ths way closed*/
		/*	' 5 = right side 1/4th way closed*/
		/*	' 6 = right side 1/2 way closed*/
		/*	' 7 = right side 3/4ths way closed*/
		/*	'  i8  Byte3834[80];  //      D1, 'tested' to see if canseethroughwalls*/
		/*	'  i8  Byte3754[80];  //      D1L*/
		/*	'  i8  Byte3674[80];  //      D2R*/
		/*	'  i8  Byte3594[80];  //      D2*/
		/*	'  i8  Byte3514[80];  //      D2L*/
		/*	'  i8  Byte3434[80];  //      D3R*/
		/*	'  i8  Byte3354[80];  //      D3*/
		/*	'  i8  Byte3274[80];  //      D3L*/

		/*	'Wall graphic information:*/

		/*	' bitmap[0] = Door Left or Right Frame (Front 1),,*/
		/*	' bitmap[1] = Door Left Frame (Front 1),,*/
		/*	' bitmap[2] = Door Left Frame (Front 2),,*/
		/*	' bitmap[3] = Door Left Frame (Front 3),,*/
		/*	' bitmap[4] = Door Left Frame (Left Side 3),,*/
		/*	' bitmap[5] = Door Top Frame (Front 1),,*/
		/*	' bitmap[6] = Door Top Frame (Front 2),,*/
		/*	' bitmap[7] = Wall (Right Side 0),,*/
		/*	' bitmap[8] = Wall (Left Side 0),,*/
		/*	' bitmap[9] = Wall (Front and Sides 1),,*/
		/*	' bitmap[10] = Wall (Front and Sides 2),,*/
		/*	' bitmap[11] = Wall (Front and Sides 3),,*/
		/*	' bitmap[12] = Wall (Far Left Side 3),,*/

		/*	'  i8  Byte3194[8];   // RectPos  ... D1R bitmap[5]*/
		/*	'  i8  Byte3186[8];   // RectPos  ... D1 bitmap[5]*/
		/*	'  i8  Byte3178[8];   // RectPos  ... D1L bitmap[5]*/
		/*	'  i8  Byte3170[8];   // RectPos  ... D2R bitmap[6]*/
		/*	'  i8  Byte3162[8];   // RectPos  ... D2 bitmap[6]*/
		/*	'  i8  Byte3154[8];   // RectPos  ... D2L bitmap[6]*/
		/*	'  i8  Byte3146[8];   // RectPos  ... D0 bitmap[0] if can' see through walls*/
		/*	' otherwise its d0 bitmap[0] with offsetx - uByte2534[0]*/
		/*	'  i8  Byte3138[4];   // RectPos  ... D0 for Pointer2142 = bitmap[1] mirrored*/
		/*	'  i8  Byte3134;      //      ... part of the rect...*/
		/*	'  i8  Byte3133;      //      ... part of the rect...*/
		/*	'  i8  Byte3132[2];   //      ... part of the rect...*/

		/*	'  i8  Byte3130[8];   // RectPos  ... D1 bitmap[1]*/
		/*	'  i8  Byte3122[8];   //      ... D2 for Pointer1848 = ?????*/
		/*	'  i8  Byte3114[8];   // RectPos  ... D2 bitmap[2]*/
		/*	'  i8  Byte3106[8];   //      ... D3 for Pointer1848 = ?????*/
		/*	'  i8  Byte3098[8];   // RectPos  ... D3 bitmap[3]*/
		/*	'  i8  Byte3090[8];   //      ... D3R for Pointer1848 = ?????*/
		/*	'  i8  Byte3082[8];   // RectPos  ... D3L bitmap[4]*/

		/*	'  i8  Byte3074[8];   // RectPos  ... D3 bitmap[11] ** AND ALOT OF OTHER STUFF ***/
		/*	'  i8  Byte3066[8];   // RectPos  ... D3L bitmap[11] and teleporter?*/
		/*	'  i8  Byte3058[8];   // RectPos  ... D3R bitmap[11] and teleporter?*/
		/*	'  i8  Byte3050[8];   // RectPos  ... D2 bitmap[10] ** and alot of other stuff ***/
		/*	'  i8  Byte3042[8];   // RectPos  ... D2L bitmap[10] and teleporter?*/
		/*	'  i8  Byte3034[8];   // RectPos  ... D2R bitmap[10] and teleporter?*/
		/*	'  ui8 uByte3026[8];   // RectPos  ...D1 bitmap[9] and teleporter*/
		/*	'  i8  Byte3018[8];   // RectPos  ... D1L bitmap[9] and teleporter?*/
		/*	'  i8  Byte3010[8];   // RectPos     ... D1R bitmap[9] and teleporter?*/
		/*	'  i8  Byte3002[8];   //      ... D0 teleporter*/
		/*	'  ui8 uByte2994[8];   // RectPos  ... D0L bitmap[8] and teleporter?*/
		/*	'  i8  Byte2986[8];   // RectPos  ...D0R bitmap[7] and teleporter?*/
		/*	'  i8  Byte2978[8];   // RectPos  ... D?? mirriored bitmap[12]*/
		/*	'  i8  Byte2970[8];   // RectPos  ... D?? bitmap[12]*/
		/*	'  i8  Byte2962[4];   // byte RectPos     ... D?? mirrored bitmap[10]*/
		/*	'  i8  Byte2958[4];   // byte Rectpos ... D?? mirrored bitmap[11]*/

		/*	'  i8  Byte2954[8];   //byte RectPos      ... D0R ceiling pit*/
		/*	'  i8  Byte2946[8];   //byte RectPos      ... D0 ceiling pit*/
		/*	'  i8  Byte2938[8];   //byte RectPos      ... D0L ceiling pit*/
		/*	'  i8  Byte2930[8];   //byte RectPos      ... D1R ceilign pit*/
		/*	'  i8  Byte2922[8];   //byte RectPos      ... D1 ceiling pit*/
		/*	'  i8  Byte2914[8];   //byte RectPos      ... D1L ceiling pit*/
		/*	'  i8  Byte2906[8];   //byte RectPos      ... D2R ceiling pit*/
		/*	'  i8  Byte2898[8];   //byte RectPos      ... D2 ceiling pit*/
		/*	'  i8  Byte2890[8];   //byte Rectpos      ... D2L ceiling pit*/

		/*	'  i8  Byte2882[8];   //RectPos (byte)      ... D0R floor pit*/
		/*	'  i8  Byte2874[8];   //RectPos (byte)      ... D0 floor pit*/
		/*	'  i8  Byte2866[8];   //RectPos (byte)      ... D0L floor pit*/
		/*	'  i8  Byte2858[8];   //RectPos (byte)      ... D1R floor pit*/
		/*	'  i8  Byte2850[8];   //RectPos (byte)      ... D1 floor pit*/
		/*	'  i8  Byte2842[8];   //RectPos (byte)      ... D1L floor pit*/
		/*	'  i8  Byte2834[8];   //RectPos (byte)      ... D2R floor pit*/
		/*	'  i8  Byte2826[8];   //RectPos (byte)      ... D2 floor pit*/
		/*	'  i8  Byte2818[8];   //RectPos (byte)      ... D2L floor pit*/
		/*	'  i8  Byte2810[8];   //RectPos (byte)      ... D3R floor pit*/
		/*	'  i8  Byte2802[8];   //RectPos (byte)      ... D3 floor pit*/
		/*	'  i8  Byte2794[8];   //RectPos (byte)      ... D3L floor pit*/

		/*	'  i8  Byte2786[8];   //RectPos (byte)      ... D0R stairs (these are left-right facing?)*/
		/*	'  i8  Byte2778[8];   //RectPos (byte)      ... D0L stairs*/
		/*	'  i8  Byte2770[8];   //RectPos (byte)      ... D1R stairs*/
		/*	'  i8  Byte2762[8];   //RectPos (byte)      ... DlL stairs (depends on facing)*/
		/*	'  i8  Byte2754[8];   //RectPos (byte)      ... D1R stairs (depends on facing)*/
		/*	'  i8  Byte2746[8];   //RectPos (byte)      ... D1L stairs (depends on facing)*/
		/*	'  i8  Byte2738[8];   //RectPos (byte)      ... D2R stairs*/
		/*	'  i8  Byte2730[8];   //RectPos (byte)      ... D2L stairs*/

		/*	'  i8  Byte2722[8];   //RectPos (byte)      ... D0 stairs (for front facing?)*/
		/*	'  i8  Byte2714[8];   //RectPos (byte)      ... D0 stairs*/
		/*	'  i8  Byte2706[8];   //RectPos (byte)      ... D1R stairs*/
		/*	'  i8  Byte2698[8];   //RectPos (byte)      ... D1 Stairs*/
		/*	'  i8  Byte2690[8];   //RectPos (byte)      ... D1L stairs*/
		/*	'  i8  Byte2682[8];   //RectPos (byte)      ... D2R stairs*/
		/*	'  i8  Byte2674[8];   //RectPos (byte)      ... D2 stairs*/
		/*	'  i8  Byte2666[8];   //RectPos (byte)      ... D2L stairs*/
		/*	'  i8  Byte2658[8];   //RectPos (byte)      ... D3R stairs*/
		/*	'  i8  Byte2650[8];   //RectPos (byte)      ... D3 stairs*/
		/*	'  i8  Byte2642[8];   //RectPos (byte)      ... D3L stairs*/

		/*	'  i8  Byte2634[8];   //RectPos (byte)      ... D0 stairs*/
		/*	'  i8  Byte2626[8];   //RectPos (byte)      ... D0 stairs*/
		/*	'  i8  Byte2618[8];   //RectPos (byte)      ... D1R stairs*/
		/*	'  i8  Byte2610[8];   //RectPos (byte)      ... D1 stairs*/
		/*	'  i8  Byte2602[8];   //RectPos (byte)      ... D1L stairs*/
		/*	'  i8  Byte2594[8];   //RectPos (byte)      ... D2R stairs*/
		/*	'  i8  Byte2586[8];   //RectPos (byte)      ... D2 stairs*/
		/*	'  i8  Byte2578[8];   //RectPos (byte)      ... D2L stairs*/
		/*	'  i8  Byte2570[8];   //RectPos (byte)      ... D3R stairs*/
		/*	'  i8  Byte2562[8];   //RectPos (byte)      ... D3 stairs*/
		/*	'  i8  Byte2554[8];   //RectPos (byte)      ... D3L stairs*/


		private byte[] restoffile = new byte[21];//orig 20

		/*	'  i8  Byte2546[4];   // where to draw character portraits ?*/

		/*	'  ui8 uByte2542[4];  //Rectpos (byte) ... see through walls rect*/

		/*	'  i16 Word2538[2];   //      ... D1 see through walls?*/
		/*	'  ui8 uByte2534[4];   //      ... rectpos? (part of above (bytes 4-7)*/

		/*	'  //i16 Word2532[1];   //*/
		/*	'  i16 Word2530[2];   // ... rect for drawing overlays (i.e. inside poison cloud)*/


		/*	'------------------------------*/
		/*	'Graphic 0x228 (558) bit study:*/

		/*	'  i16 Word7246[12];  // Swapped when read 4 groups of 3. D3 D2 D1 D0?*/
		/*	' or as bytes, 4 groups of 6.*/
		/*	' lightning bolt draw locations of some sort*/
		/*	'  structure = { int x, int y, int scale };*/
		/*	' uses Byte156 as palette*/

		/*	' CLOUD DRAWING STUFF: ALL words swapped when read.*/
		/*	' If DB15.type() > 100 then use this group*/
		/*	'  i16 Word7222[30];  // Swapped when read 10 groups of 3. D3r D3l D3c D2l D2r D2c D1l D1r D1c D0 ?*/
		/*	' or as bytes, 10 groups of 6.*/
		/*	'  structure = { int x, int y, int scale };*/

		/*	'  i16 Word7162[60];  // Swapped when read 15 groups of 4...*/
		/*	' each group of 4 contains x,y for facing and x,y for 'not facing?'*/
		/*	'  structure = { int x, int y, int x2, int y2 };*/
		/*	'  x2,y2 = coordinates when not facing?*/

		/*	' else if DB15.b7() is true use this group:*/
		/*	'  i16 Word7042[30];  // Swapped when read, 15 groups of 2.*/
		/*	'  structure = { int x, int y};*/
		/*	'*/
		/*	' CLOUD DRAWING STUFF END*/

		/*	' CREATURE GROUPS START*/
		/*	'  ITEM110 s6982[3]; // Three groups of 110*/
		/*	' CREATURE GROUPS END*/

		/*	'  i8  Byte6652[24];  // +/- small integers to shift image*/
		/*	' 3 8-byte groups, group 0, 1, and 2.*/
		/*	'  i8  Byte6628[16];    // D2 creature palette modifier*/
		/*	'  i8  Byte6612[16];    // d3 creature palette modifier*/
		/*	'  i16 Word6596[13][7]; // These are the custom palettes for monsters color 9 and 10.*/

		/*	' MONSTERGRAPHICINFO START*/
		/*	'  ITEM12 Item6414[27];//      ...*/
		/*	' MONSTERGRAPHICINFO END*/

		/*	' FLOORITEMGROUPS START*/
		/*	'  VIEWPORTPOS viewportPosition[3]; //s6090*/
		/*	'                     //      ... 100-byte entries each with 10-byte entries*/
		/*	'                     // Nominal positions in viewport for*/
		/*	'                     //  [0] Missiles ??*/
		/*	'                     //  [1] A sword on floor pointed here*/
		/*	'                     //  [2] ???*/
		/*	'                     // Each 100-byte entry has 10 entries, one for each*/
		/*	'                     // relative room number (9=room party is in).  And each*/
		/*	'                     // 10-byte entry has 5 2-byte entries, one for each relative*/
		/*	'                     // position within the room and one for ??? (object in niche?).*/
		/*	'                     //   [0] = far left*/
		/*	'                     //   [1] = far right*/
		/*	'                     //   [2] = near right*/
		/*	'                     //   [3] = near left*/
		/*	'                     //   [4] = niche??*/
		/*	' FLOORITEMGROUPS END*/

		/*	'  i8  Byte5790[32];  //  ??? Each entry points to byte6652*/
		/*	'(+- small integers for graphics).  This seems to be some*/
		/*	'sort of 'identifier' for which one to use.*/

		/*	'  i8  Byte5758[6];   //  Explosion sizes? Maybe cloud sizes... not sure.*/
		/*	'32 = full size, 16 = half, 8 = 1/4th, etc.*/
		/*	'  i8  Byte5752[8];   //  Another type of scaling, 32 = full size.*/
		/*	' Appears to scale projectiles in the air that are NOT spells*/
		/*	' [x] = 'distance' from player.*/
		/*	' Seems logical, you can see 3 rooms, each room has 2 locations.*/
		/*	' 3x2 = 6... byte5752[6] and [7] = ?*/
		/*	'  i8  Byte5744[16];  // D2 Floor item graphic pallette modifier?*/
		/*	'  i8  Byte5728[16];  // D3 Floor item graphic pallette modifier*/
		/*	'  i8  Byte5712[16];  // palette for graphics 351-359.*/
		/*	' this is the palette for when u get hit by dispel or are in a poison cloud.*/
		/*	' I think =]  Its either that or this is the special palette for light fireballs.  UNlikely.*/
		/*	'  i8  Byte5696[8];   // 4 groups of width/height combonations.*/
		/*	' there seems to be 14 pre-made graphics for each, 4/6/8/10.../32 / 32 scale.*/
		/*	' ? = fireball?*/
		/*	' ? = antimaterial?*/
		/*	' ? = lightning?*/
		/*	' ? = poison?*/

		/*	' MISSILEGRAPHICINFOS START*/
		/*	'  i8  Byte5688[84];  // You must littleEndian these.. (14) 6-byte entries*/
		/*	' MISSILEGRAPHICINFOS END 'derivedgraphic sizes start at 282 and add the offset*/

		/*	' ITEMGRAPHICINFOS START*/
		/*	'  ITEM6 s5604[85];   //      ... (85) 6-byte entries*/
		/*	' ITEMGRAPHICINFOS END*/

		/*	' BUTTONLOCS START*/
		/*	'  i8  Byte5094[6];   //      ...*/
		/*	'  i8  Byte5088[12];  //      ...*/
		/*	'  i8  Byte5076[6];   //      ...*/
		/*	' BUTTONLOCS END*/

		/*	' DOORDECORATIONINFOS START*/
		/*	'  i8  Byte5070[12];  //      ... 6-byte RectPos for doors?*/
		/*	'  i8  Byte5058[60];  //      ... 18-byte entries????*/
		/*	' DOORDECORATIONINFOS END*/

		/*	' FLOORDECORATIONFINFOS START*/
		/*	'  i8  Byte4998[162]; // 6-byte entries     ...*/
		/*	' FLOORDECORATIONFINFOS END*/

		/*	' WALLDECORATIONINFOS START*/
		/*	'  i8  Byte4836[60];  //      ...*/
		/*	'  i8  Byte4776[564]; //      ... 78-byte entries?*/
		/*	' WALLDECORATIONINFOS END*/

		/*	'  i8  Byte4213[16];  //      Illegible text cutoff locations*/
		/*	'[0] D3 side cutoff if 1 line*/
		/*	'[1] D3 side cutoff if 1 line*/
		/*	'[2] D3 side cutoff if 1 line*/
		/*	'[3] D3 front cutoff if 1 line*/
		/*	'[4] D3 front cutoff if 1 line*/
		/*	'[5] D3 front cutoff if 1 line*/
		/*	'[6] D2 side cutoff if 1 line*/
		/*	'[7] D2 side cutoff if 1 line*/
		/*	'[8] D2 side cutoff if 1 line*/
		/*	'[9] D2 front cutoff if 1 line*/
		/*	'[10] D2 front cutoff if 1 line*/
		/*	'[11] D2 front cutoff if 1 line*/
		/*	'[12] D1 side cutoff if 1 line*/
		/*	'[13] D1 side cutoff if 1 line*/
		/*	'[14] D1 side cutoff if 1 line*/
		/*	' ???  Note D1 front is not illegible text, it is legible.*/
		/*	'  i8  Byte4196[4];   //      Y locations of where to put legible text*/
		/*	' [0] = first line, [1] = second line, etc.*/
		/*	'  i8  Byte4192[4];   //      ??? uses graphic #86 to mask something for text?*/
		/*	'  i8  Byte4188[16];  //      D2 Custom Palette for door decorations*/
		/*	'  i8  Byte4172[16];  //      D3 custom palette for door decorations*/
		/*	'  i8  Byte4156[16];  //      D2 Custom Palette for wall decorations*/
		/*	'  i8  Byte4140[16];  //      D3 Custom Palette for wall decorations*/
		/*	'  i8  Byte4124[2];   //      ??? Some button placements? (0x0000)*/
		/*	'  i8  Byte4122[12];  //      Door decoration group numbers*/
		/*	'  i8  Byte4110[10];  //      Floor decoration group numbers*/
		/*	'  i8  Byte4100[60];  //      Wall decoration group numbers*/
		/*	'  i16 Word4040[1];   //      graphic number of 'water' giving wall decoration*/
		/*	'  i8  Byte4038[4];   //      ... These are wall graphics that define*/
		/*	'                     //          alcoves.  The numbers here are 1, 2, and 3*/
		/*	'                     //          in CSB.  Square Alcove, VI Altar, Ornate Alcove.*/
		/*	'  i8  Byte4034[10];  //      some sort of floor decoration offset for which graphic to display*/
		/*	'  i8  Byte4024[12];  //      12 bytes. one byte for each 'distance' from player.*/
		/*	' defines the groups for Byte4213.*/
		/*	' group = byte4024[distanceFromPlayer] * ;*/
		/*	' Then use Byte4213[group] as the base for cutoff.*/
		/*	'  i16 Word4012;      //      unused, set to 3217 during runtime.*/


		/*	' START UNKNOWN GRAPHIC ITEMS:*/
		/*	' Structure for all is:*/

		/*	'[0] + 63 = graphic address*/
		/*	' [1] + graphicrandom(2)? = some offset, maybe where to 'lock' the graphic*/
		/*	' [2] = some sort of static palette info, maybe transparent pixel*/
		/*	' [3] & 0x7F + 69 = graphicnumber*/
		/*	'if [3] = 255, a2 = null*/
		/*	' [4] = width*/
		/*	' [5] = height*/
		/*	' [6] = src offset x*/
		/*	' [7] = src offset y*/

		/*	'bytes4010 to 3922 = teleporter graphics?*/
		/*	'  i8  Byte4010[8];   //      ... corresponds to byte3074*/
		/*	'  i8  Byte4002[8];   //      ...*/
		/*	'  i8  Byte3994[8];   //      ...*/
		/*	'  i8  Byte3986[8];   //      ...*/
		/*	'  i8  Byte3978[8];   //      ...*/
		/*	'  i8  Byte3970[8];   //      ...*/
		/*	'  i8  Byte3962[8];   //      ...*/
		/*	'  i8  Byte3954[8];   //      ...*/
		/*	'  i8  Byte3946[8];   //      ...*/
		/*	'  i8  Byte3938[8];   //      ...*/
		/*	'  i8  Byte3930[8];   //      ...*/
		/*	'  i8  Byte3922[8];   //      ...*/


		/*	' door graphic information*/
		/*	'  i8  Byte3914[80];  //      door stuff. 10 groups of 8  D1R*/
		/*	' group 0 = closed*/
		/*	' group 1-3 = opens up/down*/
		/*	' 1 = 1/4th way down*/
		/*	' 2 = 1/2 way down*/
		/*	' 3 = 3/4ths way down*/
		/*	' group 2-9 = 'graphics' as door is opening sideways*/
		/*	' 2 = left side 1/4th way closed*/
		/*	' 3 = left side 1/2 way closed*/
		/*	' 4 = left side 3/4ths way closed*/
		/*	' 5 = right side 1/4th way closed*/
		/*	' 6 = right side 1/2 way closed*/
		/*	' 7 = right side 3/4ths way closed*/
		/*	'  i8  Byte3834[80];  //      D1, 'tested' to see if canseethroughwalls*/
		/*	'  i8  Byte3754[80];  //      D1L*/
		/*	'  i8  Byte3674[80];  //      D2R*/
		/*	'  i8  Byte3594[80];  //      D2*/
		/*	'  i8  Byte3514[80];  //      D2L*/
		/*	'  i8  Byte3434[80];  //      D3R*/
		/*	'  i8  Byte3354[80];  //      D3*/
		/*	'  i8  Byte3274[80];  //      D3L*/

		/*	' END UNKNOWN GRAPHIC ITEMS:*/

		/*	'Wall graphic information:*/
		/*	'  These are 8 byte rect positions, but the positions are in bytes, not words.*/
		/*	'  Thus the structure is of*/
		/*	'    { byte x1, y1, x2, y2, width, height, offsetx, offsety }*/

		/*	' bitmap[0] = Door Left or Right Frame (Front 1),,*/
		/*	' bitmap[1] = Door Left Frame (Front 1),,*/
		/*	' bitmap[2] = Door Left Frame (Front 2),,*/
		/*	' bitmap[3] = Door Left Frame (Front 3),,*/
		/*	' bitmap[4] = Door Left Frame (Left Side 3),,*/
		/*	' bitmap[5] = Door Top Frame (Front 1),,*/
		/*	' bitmap[6] = Door Top Frame (Front 2),,*/
		/*	' bitmap[7] = Wall (Right Side 0),,*/
		/*	' bitmap[8] = Wall (Left Side 0),,*/
		/*	' bitmap[9] = Wall (Front and Sides 1),,*/
		/*	' bitmap[10] = Wall (Front and Sides 2),,*/
		/*	' bitmap[11] = Wall (Front and Sides 3),,*/
		/*	' bitmap[12] = Wall (Far Left Side 3),,*/

		/*	'  i8  Byte3194[8];   // RectPos  ... D1R bitmap[5]*/
		/*	'  i8  Byte3186[8];   // RectPos  ... D1 bitmap[5]*/
		/*	'  i8  Byte3178[8];   // RectPos  ... D1L bitmap[5]*/
		/*	'  i8  Byte3170[8];   // RectPos  ... D2R bitmap[6]*/
		/*	'  i8  Byte3162[8];   // RectPos  ... D2 bitmap[6]*/
		/*	'  i8  Byte3154[8];   // RectPos  ... D2L bitmap[6]*/
		/*	'  i8  Byte3146[8];   // RectPos  ... D0 bitmap[0] if can' see through walls*/
		/*	' otherwise its d0 bitmap[0] with offsetx - uByte2534[0]*/
		/*	'  i8  Byte3138[4];   // RectPos  ... D0 for Pointer2142 = bitmap[1] mirrored*/
		/*	'  i8  Byte3134;      //      ... part of the rect...*/
		/*	'  i8  Byte3133;      //      ... part of the rect...*/
		/*	'  i8  Byte3132[2];   //      ... part of the rect...*/

		/*	'  i8  Byte3130[8];   // RectPos  ... D1 bitmap[1]*/
		/*	'  i8  Byte3122[8];   //      ... D2 for Pointer1848 = ?????*/
		/*	'  i8  Byte3114[8];   // RectPos  ... D2 bitmap[2]*/
		/*	'  i8  Byte3106[8];   //      ... D3 for Pointer1848 = ?????*/
		/*	'  i8  Byte3098[8];   // RectPos  ... D3 bitmap[3]*/
		/*	'  i8  Byte3090[8];   //      ... D3R for Pointer1848 = ?????*/
		/*	'  i8  Byte3082[8];   // RectPos  ... D3L bitmap[4]*/

		/*	'  i8  Byte3074[8];   // RectPos  ... D3 bitmap[11] ** AND ALOT OF OTHER STUFF ***/
		/*	'  i8  Byte3066[8];   // RectPos  ... D3L bitmap[11] and teleporter?*/
		/*	'  i8  Byte3058[8];   // RectPos  ... D3R bitmap[11] and teleporter?*/
		/*	'  i8  Byte3050[8];   // RectPos  ... D2 bitmap[10] ** and alot of other stuff ***/
		/*	'  i8  Byte3042[8];   // RectPos  ... D2L bitmap[10] and teleporter?*/
		/*	'  i8  Byte3034[8];   // RectPos  ... D2R bitmap[10] and teleporter?*/
		/*	'  ui8 uByte3026[8];   // RectPos  ...D1 bitmap[9] and teleporter*/
		/*	'  i8  Byte3018[8];   // RectPos  ... D1L bitmap[9] and teleporter?*/
		/*	'  i8  Byte3010[8];   // RectPos     ... D1R bitmap[9] and teleporter?*/
		/*	'  i8  Byte3002[8];   //      ... D0 teleporter*/
		/*	'  ui8 uByte2994[8];   // RectPos  ... D0L bitmap[8] and teleporter?*/
		/*	'  i8  Byte2986[8];   // RectPos  ...D0R bitmap[7] and teleporter?*/
		/*	'  i8  Byte2978[8];   // RectPos  ... D?? mirriored bitmap[12]*/
		/*	'  i8  Byte2970[8];   // RectPos  ... D?? bitmap[12]*/
		/*	'  i8  Byte2962[4];   // byte RectPos     ... D?? mirrored bitmap[10]*/
		/*	'  i8  Byte2958[4];   // byte Rectpos ... D?? mirrored bitmap[11]*/

		/*	'  i8  Byte2954[8];   //byte RectPos      ... D0R ceiling pit*/
		/*	'  i8  Byte2946[8];   //byte RectPos      ... D0 ceiling pit*/
		/*	'  i8  Byte2938[8];   //byte RectPos      ... D0L ceiling pit*/
		/*	'  i8  Byte2930[8];   //byte RectPos      ... D1R ceilign pit*/
		/*	'  i8  Byte2922[8];   //byte RectPos      ... D1 ceiling pit*/
		/*	'  i8  Byte2914[8];   //byte RectPos      ... D1L ceiling pit*/
		/*	'  i8  Byte2906[8];   //byte RectPos      ... D2R ceiling pit*/
		/*	'  i8  Byte2898[8];   //byte RectPos      ... D2 ceiling pit*/
		/*	'  i8  Byte2890[8];   //byte Rectpos      ... D2L ceiling pit*/

		/*	'  i8  Byte2882[8];   //RectPos (byte)      ... D0R floor pit*/
		/*	'  i8  Byte2874[8];   //RectPos (byte)      ... D0 floor pit*/
		/*	'  i8  Byte2866[8];   //RectPos (byte)      ... D0L floor pit*/
		/*	'  i8  Byte2858[8];   //RectPos (byte)      ... D1R floor pit*/
		/*	'  i8  Byte2850[8];   //RectPos (byte)      ... D1 floor pit*/
		/*	'  i8  Byte2842[8];   //RectPos (byte)      ... D1L floor pit*/
		/*	'  i8  Byte2834[8];   //RectPos (byte)      ... D2R floor pit*/
		/*	'  i8  Byte2826[8];   //RectPos (byte)      ... D2 floor pit*/
		/*	'  i8  Byte2818[8];   //RectPos (byte)      ... D2L floor pit*/
		/*	'  i8  Byte2810[8];   //RectPos (byte)      ... D3R floor pit*/
		/*	'  i8  Byte2802[8];   //RectPos (byte)      ... D3 floor pit*/
		/*	'  i8  Byte2794[8];   //RectPos (byte)      ... D3L floor pit*/

		/*	'  i8  Byte2786[8];   //RectPos (byte)      ... D0R stairs (these are left-right facing?)*/
		/*	'  i8  Byte2778[8];   //RectPos (byte)      ... D0L stairs*/
		/*	'  i8  Byte2770[8];   //RectPos (byte)      ... D1R stairs*/
		/*	'  i8  Byte2762[8];   //RectPos (byte)      ... DlL stairs (depends on facing)*/
		/*	'  i8  Byte2754[8];   //RectPos (byte)      ... D1R stairs (depends on facing)*/
		/*	'  i8  Byte2746[8];   //RectPos (byte)      ... D1L stairs (depends on facing)*/
		/*	'  i8  Byte2738[8];   //RectPos (byte)      ... D2R stairs*/
		/*	'  i8  Byte2730[8];   //RectPos (byte)      ... D2L stairs*/

		/*	'  i8  Byte2722[8];   //RectPos (byte)      ... D0 stairs (for front facing?)*/
		/*	'  i8  Byte2714[8];   //RectPos (byte)      ... D0 stairs*/
		/*	'  i8  Byte2706[8];   //RectPos (byte)      ... D1R stairs*/
		/*	'  i8  Byte2698[8];   //RectPos (byte)      ... D1 Stairs*/
		/*	'  i8  Byte2690[8];   //RectPos (byte)      ... D1L stairs*/
		/*	'  i8  Byte2682[8];   //RectPos (byte)      ... D2R stairs*/
		/*	'  i8  Byte2674[8];   //RectPos (byte)      ... D2 stairs*/
		/*	'  i8  Byte2666[8];   //RectPos (byte)      ... D2L stairs*/
		/*	'  i8  Byte2658[8];   //RectPos (byte)      ... D3R stairs*/
		/*	'  i8  Byte2650[8];   //RectPos (byte)      ... D3 stairs*/
		/*	'  i8  Byte2642[8];   //RectPos (byte)      ... D3L stairs*/

		/*	'  i8  Byte2634[8];   //RectPos (byte)      ... D0 stairs*/
		/*	'  i8  Byte2626[8];   //RectPos (byte)      ... D0 stairs*/
		/*	'  i8  Byte2618[8];   //RectPos (byte)      ... D1R stairs*/
		/*	'  i8  Byte2610[8];   //RectPos (byte)      ... D1 stairs*/
		/*	'  i8  Byte2602[8];   //RectPos (byte)      ... D1L stairs*/
		/*	'  i8  Byte2594[8];   //RectPos (byte)      ... D2R stairs*/
		/*	'  i8  Byte2586[8];   //RectPos (byte)      ... D2 stairs*/
		/*	'  i8  Byte2578[8];   //RectPos (byte)      ... D2L stairs*/
		/*	'  i8  Byte2570[8];   //RectPos (byte)      ... D3R stairs*/
		/*	'  i8  Byte2562[8];   //RectPos (byte)      ... D3 stairs*/
		/*	'  i8  Byte2554[8];   //RectPos (byte)      ... D3L stairs*/

		/*	'  i8  Byte2546[4];   // where to draw character portraits ?*/

		/*	'  ui8 uByte2542[4];  //Rectpos (byte) ... see through walls rect*/

		/*	'  i16 Word2538[2];   //      ... D1 see through walls?*/
		/*	'  ui8 uByte2534[4];   //      ... rectpos? (part of above (bytes 4-7)*/

		/*	'  //i16 Word2532[1];   //*/
		/*	'  i16 Word2530[2];   // ... rect for drawing overlays (i.e. inside poison cloud)*/


		public MemoryStream itemdata;
		public int itemloc; //Now 0 based


		public Color palettetocolor(int c)
		{

			int r, g, b;
			/*        'For j = 1 To 7*/
			if (c != 0 && c <= 0x777)
			{
				r = App.CShort(c & 0xF00) / 0x100;
				g = App.CShort(c & 0xF0) / 0x10;
				b = App.CShort(c & 0xF) / 0x1;
				if (r > 7)
				{
					r = 7;
				}
				if (g > 7)
				{
					g = 7;
				}
				if (b > 7)
				{
					b = 7;
				}
				r = r * 36;
				g = g * 36;
				b = b * 36;
				/*            'MsgBox "building RGB code: " + Str(r) + ", " + Str(g) + ", " + Str(b)*/
				return Color.FromArgb(r, g, b);
				/*            'MsgBox "Building color before: " + Hex(palettes(i, j)) + " after: " + Hex(palettetocolor)*/
			}
			else
			{
				return Color.Black;
			}
			/*        'Next*/
		} //End function

		public int palette(short X, short Y)
		{
			return palettes[X, Y];
		} //End function
		public void setpallete(short X, short Y, int c)
		{
			palettes[X, Y] = c;
		} //End Sub

        public drawlocgroup creaturegroup(short X, short Y)
        {
/*		'creaturegroup = creaturegroups(X, Y)*/
				short i;
				short j;

			var ans_creaturegroup = new drawlocgroup();

			for( i = 1;i<= 5; i++) 
			{ 
				for(j = 1;j<= 2; j++) 
				{ 
					ans_creaturegroup.setloc(i, j, creaturegroups[X, Y].loc_Renamed(i, j));
				} //end Next 
			} //end Next	

			return ans_creaturegroup;
		} //End function
	public void setcreaturegroup(short X, short Y, ref drawlocgroup g)
	{
		/*		'Set creaturegroup = creaturegroups(X, Y)*/
		/*		'creaturegroups(X, Y) = g*/
        short i;
		short j;

		for(i = 1; i <= 5; i++) 
		{ 
			for(j = 1;j<= 2; j++) 
			{ 
				creaturegroups[X, Y].setloc(i, j, g.loc_Renamed(i, j));
			} //end Next 
		} //end Next 
	} //End Sub
	public drawlocgroup FloorItemGroup(short X, short Y)
	{
        short i;
		short j;

		var ans_FloorItemGroup = new drawlocgroup();
		for( i = 1;i <= 5; i++) 
		{ 
			for( j = 1;j <= 2; j++) 
			{ 
				ans_FloorItemGroup.setloc(i, j, flooritemgroups[X, Y].loc_Renamed(i, j));
			} //end Next 
		} //end Next 

		return ans_FloorItemGroup;
	} //End function
	public void setflooritemgroup(short X, short Y, ref drawlocgroup g)
	{
        short i;
		short j;

		for( i = 1; i<= 5; i++) 
		{ 
			for(j = 1; j<= 2; j++) 
			{ 
				flooritemgroups[X, Y].setloc(i, j, g.loc_Renamed(i, j));
			} //end Next 
		} //end Next 
	} //End Sub
	public decorationinfo doordecorationinfo(short X, short Y)
	{
		return doordecorationinfos[X, Y];
	} //End function
	public decorationinfo floordecorationinfo(short X, short Y)
	{
		return floordecorationinfos[X, Y];
	} //End function
	public decorationinfo walldecorationinfo(short X, short Y)
	{
		return walldecorationinfos[X, Y];
	} //End function
	public decorationinfo buttonloc(short X)
	{
		return buttonlocs[X];
	} //End function
	public monstergraphicinfo monstergraphicinfo(short X)
	{
		return MonsterGraphicInfos[X];
	} //End function
	public missilegraphicinfo missilegraphicinfo(short X)
	{
		return missileGraphicInfos[X];
	} //End function
	public itemgraphicinfo itemgraphicinfo(short X)
	{
		return itemGraphicInfos[X];
	} //End function


	public byte d2flooritempalette(short X)
	{
		return d2flooritempalettes[X];
	} //End function

	public void setd2flooritempalette(short X, byte val_Renamed)
	{
		d2flooritempalettes[X] = val_Renamed;
	} //End Sub
	public byte d3flooritempalette(short X)
	{
		return d3flooritempalettes[X];
	} //End function

	public void setd3flooritempalette(short X, byte val_Renamed)
	{
		d3flooritempalettes[X] = val_Renamed;
	} //End Sub
	public byte d2doordecorationpalette(short X)
	{
		return d2doordecorationpalettes[X];
	} //End function

public void setd2doordecorationpalette(short X, byte val_Renamed)
{
    d2doordecorationpalettes[X] = val_Renamed;
} //End Sub
public byte d3doordecorationpalette(short X)
{
    return d3doordecorationpalettes[X];
} //End function

public void setd3doordecorationpalette(short X, byte val_Renamed)
{
    d3doordecorationpalettes[X] = val_Renamed;
} //End Sub

public byte d2monsterpalette(short X)
{
    return d2monsterpalettes[X];
} //End function

public void setd2monsterpalette(short X, byte val_Renamed)
{
    d2monsterpalettes[X] = val_Renamed;
} //End Sub
public byte d3monsterpalette(short X)
{
    return d3monsterpalettes[X];
} //End function

public void setd3monsterpalette(short X, byte val_Renamed)
{
    d3monsterpalettes[X] = val_Renamed;
} //End Sub
public byte d2walldecorationpalette(short X)
{
    return d2walldecorationpalettes[X];
} //End function

public void setd2walldecorationpalette(short X, byte val_Renamed)
{
    d2walldecorationpalettes[X] = val_Renamed;
} //End Sub
public byte d3walldecorationpalette(short X)
{
    return d3walldecorationpalettes[X];
} //End function

public void setd3walldecorationpalette(short X, byte val_Renamed)
{
    d3walldecorationpalettes[X] = val_Renamed;
} //End Sub
public byte doordecorationnumber(short X)
{
    return doordecorationnumbers[X];
} //End function

public void setdoordecorationnumber(short X, byte val_Renamed)
{
    doordecorationnumbers[X] = val_Renamed;
} //End Sub
public byte floordecorationnumber(short X)
{
    return floordecorationnumbers[X];
} //End function

public void setfloordecorationnumber(short X, byte val_Renamed)
{
    floordecorationnumbers[X] = val_Renamed;
} //End Sub
public byte walldecorationnumber(short X)
{
    return walldecorationnumbers[X];
} //End function

public void setwalldecorationnumber(short X, byte val_Renamed)
{
    walldecorationnumbers[X] = val_Renamed;
} //End Sub
public byte floordecorationoffset(short X)
{
    return floordecorationoffsets[X];
} //End function

public void setfloordecorationoffset(short X, byte val_Renamed)
{
    floordecorationoffsets[X] = val_Renamed;
} //End Sub
public animationinfo animationinfo(short X)
{
    return animationinfos[X];
} //End function
public graphicrect graphicrect(short X)
{
    return graphicrects[X];
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
	ans_read_int = (ans_read_int * 256);
	ans_read_int = (ans_read_int + b1);

			return ans_read_int;
    } //End function
    public void write_byte(byte num)
	{
        itemdata.Position = itemloc;
		itemdata.WriteByte(num);
		itemloc = itemloc + 1;
	} //End Sub
public void write_int(ref int num)
{
        byte b1;
		byte b2;
		int l1;
		l1 = num & 0xFF00;
		b1 = (byte)(l1 / 0x100);
		b2 = (byte)(num & 0xFF);
		write_byte(b1);
		write_byte(b2);
    } //End Sub
    public void read()
	{
        short i;
		short j;
		itemloc = 0;
		for (i = 1; i <= 132; i++)
		{
			word7246[i] = read_int();
		} //end Next
		for (i = 1; i <= 3; i++)
		{
			for (j = 1; j <= 11; j++)
			{
				creaturegroups[i, j].setloc(1, 1, read_byte());
				creaturegroups[i, j].setloc(1, 2, read_byte());
				creaturegroups[i, j].setloc(2, 1, read_byte());
				creaturegroups[i, j].setloc(2, 2, read_byte());
				creaturegroups[i, j].setloc(3, 1, read_byte());
				creaturegroups[i, j].setloc(3, 2, read_byte());
				creaturegroups[i, j].setloc(4, 1, read_byte());
				creaturegroups[i, j].setloc(4, 2, read_byte());
				creaturegroups[i, j].setloc(5, 1, read_byte());
				creaturegroups[i, j].setloc(5, 2, read_byte());
			} //end Next
		} //end Next
		for ( i = 1; i <= 24; i++)
		{
			byte6652[i] = read_byte();
		} //end Next
		for ( i = 1; i <= 16; i++)
		{
			d2monsterpalettes[i] = read_byte();
		} //end Next
		for ( i = 1; i <= 16; i++)
		{
			d3monsterpalettes[i] = read_byte();
		} //end Next
		for ( i = 1; i <= 13; i++)
		{
			for (j = 1; j <= 7; j++)
			{
				palettes[i, j] = read_int();
			} //end Next
		} //end Next
		for ( i = 1; i <= 27; i++)
		{
			MonsterGraphicInfos[i].byte0 = read_byte();
			MonsterGraphicInfos[i].graphicnum = read_byte();
			MonsterGraphicInfos[i].byte2 = read_byte();
			MonsterGraphicInfos[i].byte3 = read_byte();
			MonsterGraphicInfos[i].widthfront = read_byte();
			MonsterGraphicInfos[i].heightfront = read_byte();
			MonsterGraphicInfos[i].widthside = read_byte();
			MonsterGraphicInfos[i].heightside = read_byte();
			MonsterGraphicInfos[i].widthattack = read_byte();
			MonsterGraphicInfos[i].heightattack = read_byte();
			MonsterGraphicInfos[i].byte10 = read_byte();
			MonsterGraphicInfos[i].byte11 = read_byte();
		} //end Next
		for (i = 1; i <= 3; i++)
		{
			for (j = 1; j <= 10; j++)
			{
				flooritemgroups[i, j].setloc(1, 1, read_byte());
				flooritemgroups[i, j].setloc(1, 2, read_byte());
				flooritemgroups[i, j].setloc(2, 1, read_byte());
				flooritemgroups[i, j].setloc(2, 2, read_byte());
				flooritemgroups[i, j].setloc(3, 1, read_byte());
				flooritemgroups[i, j].setloc(3, 2, read_byte());
				flooritemgroups[i, j].setloc(4, 1, read_byte());
				flooritemgroups[i, j].setloc(4, 2, read_byte());
				flooritemgroups[i, j].setloc(5, 1, read_byte());
				flooritemgroups[i, j].setloc(5, 2, read_byte());
			} //end Next
		} //end Next
		for (i = 1; i <= 46; i++)
		{
			byte5790[i] = read_byte();
		} //end Next
		for (i = 1; i <= 16; i++)
		{
			d2flooritempalettes[i] = read_byte();
		} //end Next
		for (i = 1; i <= 16; i++)
		{
			d3flooritempalettes[i] = read_byte();
		} //end Next
		for (i = 1; i <= 24; i++)
		{
			byte5712[i] = read_byte();
		} //end Next
			for (i = 1; i <= 14; i++)
			{
				missileGraphicInfos[i].GraphicNum = read_byte();
				missileGraphicInfos[i].GraphicOffset = read_byte();
				missileGraphicInfos[i].Width = read_byte();
				missileGraphicInfos[i].Height = read_byte();
				missileGraphicInfos[i].Spell_Renamed = read_byte();
				missileGraphicInfos[i].Byte5 = read_byte();
			} //end Next
			for ( i = 1; i <= 85; i++)
			{
				itemGraphicInfos[i].GraphicNum = read_byte();
				itemGraphicInfos[i].GraphicOffset = read_byte();
				itemGraphicInfos[i].Width = read_byte();
				itemGraphicInfos[i].Height = read_byte();
				itemGraphicInfos[i].Byte4 = read_byte();
				itemGraphicInfos[i].FloorItemGroup = read_byte();
			} //end Next
			for ( i = 1; i <= 4; i++)
			{
				buttonlocs[i].x1 = read_byte();
				buttonlocs[i].x2 = read_byte();
				buttonlocs[i].y1 = read_byte();
				buttonlocs[i].y2 = read_byte();
				buttonlocs[i].Width = read_byte();
				buttonlocs[i].Height = read_byte();
			} //end Next
			for ( i = 1; i <= 4; i++)
			{
				for ( j = 1; j <= 3; j++)
				{
					doordecorationinfos[i, j].x1 = read_byte();
					doordecorationinfos[i, j].x2 = read_byte();
					doordecorationinfos[i, j].y1 = read_byte();
					doordecorationinfos[i, j].y2 = read_byte();
					doordecorationinfos[i, j].Width = read_byte();
					doordecorationinfos[i, j].Height = read_byte();
				} //end Next
			} //end Next
			for ( i = 1; i <= 3; i++)
			{
				for ( j = 1; j <= 9; j++)
				{
					floordecorationinfos[i, j].x1 = read_byte();
					floordecorationinfos[i, j].x2 = read_byte();
					floordecorationinfos[i, j].y1 = read_byte();
					floordecorationinfos[i, j].y2 = read_byte();
					floordecorationinfos[i, j].Width = read_byte();
					floordecorationinfos[i, j].Height = read_byte();
				} //end Next
			} //end Next
			for ( i = 1; i <= 8; i++)
			{
				for ( j = 1; j <= 13; j++)
				{
					walldecorationinfos[i, j].x1 = read_byte();
					walldecorationinfos[i, j].x2 = read_byte();
					walldecorationinfos[i, j].y1 = read_byte();
					walldecorationinfos[i, j].y2 = read_byte();
					walldecorationinfos[i, j].Width = read_byte();
					walldecorationinfos[i, j].Height = read_byte();
				} //end Next
			} //end Next
			/*        '    For i = 1 To 1686*/
			/*        '        Byte4213[i] = read_byte*/
			/*        '    Next*/
			for ( i = 1; i <= 16; i++)
			{
				illegibletextcutofflocations[i] = read_byte();
			} //end Next
			for ( i = 1; i <= 4; i++)
			{
				legibletextlocations[i] = read_byte();
			} //end Next
			for ( i = 1; i <= 4; i++)
			{
				byte4192[i] = read_byte();
			} //end Next
			for ( i = 1; i <= 16; i++)
			{
				d2doordecorationpalettes[i] = read_byte();
			} //end Next
			for ( i = 1; i <= 16; i++)
			{
				d3doordecorationpalettes[i] = read_byte();
			} //end Next
			for ( i = 1; i <= 16; i++)
			{
				d2walldecorationpalettes[i] = read_byte();
			} //end Next
			for ( i = 1; i <= 16; i++)
			{
				d3walldecorationpalettes[i] = read_byte();
			} //end Next
			for ( i = 1; i <= 2; i++)
			{
				buttondecorationnumbers[i] = read_byte();
			} //end Next
			for ( i = 1; i <= 12; i++)
			{
				doordecorationnumbers[i] = read_byte();
			} //end Next
			for ( i = 1; i <= 10; i++)
			{
				floordecorationnumbers[i] = read_byte();
			} //end Next
			for ( i = 1; i <= 60; i++)
			{
				walldecorationnumbers[i] = read_byte();
			} //end Next
			watergraphicnum = read_int();
			for ( i = 1; i <= 4; i++)
			{
				alcovegraphicnums[i] = read_byte();
			} //end Next
			for ( i = 1; i <= 10; i++)
			{
				floordecorationoffsets[i] = read_byte();
			} //end Next
			for ( i = 1; i <= 12; i++)
			{
				illegibletextgroups[i] = read_byte();
			} //end Next
			word4012 = read_int();
			for ( i = 1; i <= 12; i++)
			{
				animationinfos[i].graphicaddress = read_byte();
				animationinfos[i].graphicrand = read_byte();
				animationinfos[i].TransparentPixel = read_byte();
				animationinfos[i].GraphicNum = read_byte();
				animationinfos[i].width = read_byte();
				animationinfos[i].height = read_byte();
				animationinfos[i].srcx = read_byte();
				animationinfos[i].srcy = read_byte();
			} //end Next
			for ( i = 1; i <= 171; i++)
			{
				graphicrects[i].x1 = read_byte();
				graphicrects[i].x2 = read_byte();
				graphicrects[i].y1 = read_byte();
				graphicrects[i].y2 = read_byte();
				graphicrects[i].width = read_byte();
				graphicrects[i].height = read_byte();
				graphicrects[i].srcx = read_byte();
				graphicrects[i].srcy = read_byte();
			} //end Next
			for ( i = 1; i <= 20; i++)
			{
				restoffile[i] = read_byte();
			} //end Next
			initGraphicRectData();
    } //End Sub

    public void update()
	{
        short i;
		short j;
		itemdata = new MemoryStream();
		itemloc = 0;
		for ( i = 1; i <= 132; i++)
		{
			write_int(ref word7246[i]);
		} //end Next

		for ( i = 1; i <= 3; i++)
		{
			for (j = 1; j <= 11; j++)
			{

        write_byte(creaturegroups[i, j].loc_Renamed(1, 1));
        write_byte(creaturegroups[i, j].loc_Renamed(1, 2));
        write_byte(creaturegroups[i, j].loc_Renamed(2, 1));
        write_byte(creaturegroups[i, j].loc_Renamed(2, 2));
        write_byte(creaturegroups[i, j].loc_Renamed(3, 1));
        write_byte(creaturegroups[i, j].loc_Renamed(3, 2));
        write_byte(creaturegroups[i, j].loc_Renamed(4, 1));
        write_byte(creaturegroups[i, j].loc_Renamed(4, 2));
        write_byte(creaturegroups[i, j].loc_Renamed(5, 1));
        write_byte(creaturegroups[i, j].loc_Renamed(5, 2));
    } //end Next
} //end Next
for ( i = 1; i <= 24; i++)
{

    write_byte(byte6652[i]);
} //end Next
for ( i = 1; i <= 16; i++)
{

    write_byte(d2monsterpalettes[i]);
} //end Next
for ( i = 1; i <= 16; i++)
{

    write_byte(d3monsterpalettes[i]);
} //end Next
for ( i = 1; i <= 13; i++)
{
    for (j = 1; j <= 7; j++)
    {

        write_int(ref palettes[i, j]);
    } //end Next
} //end Next
for ( i = 1; i <= 27; i++)
{

    write_byte((MonsterGraphicInfos[i].byte0));

    write_byte((MonsterGraphicInfos[i].graphicnum));

    write_byte((MonsterGraphicInfos[i].byte2));

    write_byte((MonsterGraphicInfos[i].byte3));

    write_byte((MonsterGraphicInfos[i].widthfront));

    write_byte((MonsterGraphicInfos[i].heightfront));

    write_byte((MonsterGraphicInfos[i].widthside));

    write_byte((MonsterGraphicInfos[i].heightside));

    write_byte((MonsterGraphicInfos[i].widthattack));

    write_byte((MonsterGraphicInfos[i].heightattack));

    write_byte((MonsterGraphicInfos[i].byte10));

    write_byte((MonsterGraphicInfos[i].byte11));
} //end Next
for ( i = 1; i <= 3; i++)
{
    for (j = 1; j <= 10; j++)
    {

        write_byte(flooritemgroups[i, j].loc_Renamed(1, 1));

        write_byte(flooritemgroups[i, j].loc_Renamed(1, 2));

        write_byte(flooritemgroups[i, j].loc_Renamed(2, 1));

        write_byte(flooritemgroups[i, j].loc_Renamed(2, 2));

        write_byte(flooritemgroups[i, j].loc_Renamed(3, 1));

        write_byte(flooritemgroups[i, j].loc_Renamed(3, 2));

        write_byte(flooritemgroups[i, j].loc_Renamed(4, 1));

        write_byte(flooritemgroups[i, j].loc_Renamed(4, 2));

        write_byte(flooritemgroups[i, j].loc_Renamed(5, 1));

        write_byte(flooritemgroups[i, j].loc_Renamed(5, 2));
    } //end Next
} //end Next
for ( i = 1; i <= 46; i++)
{

    write_byte(byte5790[i]);
} //end Next
for ( i = 1; i <= 16; i++)
{

    write_byte(d2flooritempalettes[i]);
} //end Next
for ( i = 1; i <= 16; i++)
{

    write_byte(d3flooritempalettes[i]);
} //end Next
for ( i = 1; i <= 24; i++)
{

    write_byte(byte5712[i]);
} //end Next
for ( i = 1; i <= 14; i++)
{

    write_byte((missileGraphicInfos[i].GraphicNum));

    write_byte((missileGraphicInfos[i].GraphicOffset));

    write_byte((missileGraphicInfos[i].Width));

    write_byte((missileGraphicInfos[i].Height));

    write_byte((missileGraphicInfos[i].Spell_Renamed));

    write_byte((missileGraphicInfos[i].Byte5));
} //end Next
for ( i = 1; i <= 85; i++)
{

    write_byte((itemGraphicInfos[i].GraphicNum));

    write_byte((itemGraphicInfos[i].GraphicOffset));

    write_byte((itemGraphicInfos[i].Width));

    write_byte((itemGraphicInfos[i].Height));

    write_byte((itemGraphicInfos[i].Byte4));

    write_byte((itemGraphicInfos[i].FloorItemGroup));
} //end Next
for ( i = 1; i <= 4; i++)
{

    write_byte((buttonlocs[i].x1));

    write_byte((buttonlocs[i].x2));

    write_byte((buttonlocs[i].y1));

    write_byte((buttonlocs[i].y2));

    write_byte((buttonlocs[i].Width));

    write_byte((buttonlocs[i].Height));
} //end Next
for ( i = 1; i <= 4; i++)
{
    for (j = 1; j <= 3; j++)
    {

        write_byte((doordecorationinfos[i, j].x1));

        write_byte((doordecorationinfos[i, j].x2));

        write_byte((doordecorationinfos[i, j].y1));

        write_byte((doordecorationinfos[i, j].y2));

        write_byte((doordecorationinfos[i, j].Width));

        write_byte((doordecorationinfos[i, j].Height));
    } //end Next
} //end Next
for ( i = 1; i <= 3; i++)
{
    for (j = 1; j <= 9; j++)
    {

        write_byte((floordecorationinfos[i, j].x1));

        write_byte((floordecorationinfos[i, j].x2));

        write_byte((floordecorationinfos[i, j].y1));

        write_byte((floordecorationinfos[i, j].y2));

        write_byte((floordecorationinfos[i, j].Width));

        write_byte((floordecorationinfos[i, j].Height));
    } //end Next
} //end Next
for ( i = 1; i <= 8; i++)
{
    for (j = 1; j <= 13; j++)
    {

        write_byte((walldecorationinfos[i, j].x1));

        write_byte((walldecorationinfos[i, j].x2));

        write_byte((walldecorationinfos[i, j].y1));

        write_byte((walldecorationinfos[i, j].y2));

        write_byte((walldecorationinfos[i, j].Width));

        write_byte((walldecorationinfos[i, j].Height));
    } //end Next
} //end Next
/*        '    For i = 1 To 1686*/
/*        '        write_byte Byte4213[i]*/
/*        '    Next*/
for ( i = 1; i <= 16; i++)
{

    write_byte(illegibletextcutofflocations[i]);
} //end Next
for ( i = 1; i <= 4; i++)
{

    write_byte(legibletextlocations[i]);
} //end Next
for ( i = 1; i <= 4; i++)
{

    write_byte(byte4192[i]);
} //end Next
for ( i = 1; i <= 16; i++)
{

    write_byte(d2doordecorationpalettes[i]);
} //end Next
for ( i = 1; i <= 16; i++)
{

    write_byte(d3doordecorationpalettes[i]);
} //end Next
for ( i = 1; i <= 16; i++)
{

    write_byte(d2walldecorationpalettes[i]);
} //end Next
for ( i = 1; i <= 16; i++)
{

    write_byte(d3walldecorationpalettes[i]);
} //end Next
for ( i = 1; i <= 2; i++)
{

    write_byte(buttondecorationnumbers[i]);
} //end Next
for ( i = 1; i <= 12; i++)
{

    write_byte(doordecorationnumbers[i]);
} //end Next
for ( i = 1; i <= 10; i++)
{

    write_byte(floordecorationnumbers[i]);
} //end Next
for ( i = 1; i <= 60; i++)
{

    write_byte(walldecorationnumbers[i]);
} //end Next
write_int(ref watergraphicnum);
for ( i = 1; i <= 4; i++)
{

    write_byte(alcovegraphicnums[i]);
} //end Next
for ( i = 1; i <= 10; i++)
{

    write_byte(floordecorationoffsets[i]);
} //end Next
for ( i = 1; i <= 12; i++)
{

    write_byte(illegibletextgroups[i]);
} //end Next
write_int(ref word4012);
for ( i = 1; i <= 12; i++)
{

    write_byte((animationinfos[i].graphicaddress));

    write_byte((animationinfos[i].graphicrand));

    write_byte((animationinfos[i].TransparentPixel));

    write_byte((animationinfos[i].GraphicNum));

    write_byte((animationinfos[i].width));

    write_byte((animationinfos[i].height));

    write_byte((animationinfos[i].srcx));

    write_byte((animationinfos[i].srcy));
} //end Next
for ( i = 1; i <= 171; i++)
{

    write_byte((graphicrects[i].x1));

    write_byte((graphicrects[i].x2));

    write_byte((graphicrects[i].y1));

    write_byte((graphicrects[i].y2));

    write_byte((graphicrects[i].width));

    write_byte((graphicrects[i].height));

    write_byte((graphicrects[i].srcx));

    write_byte((graphicrects[i].srcy));
} //end Next
for ( i = 1; i <= 20; i++)
{

    write_byte(restoffile[i]);
} //end Next
/*        'graphics.setitemsize 559, LenB(itemdata)*/
/*        'graphics.setitemstr 559, itemdata*/


app.graphics.setgitemsize(558, (int)itemdata.Length);

app.graphics.setgitemdata(558, itemdata);
    } //End Sub
		public void initGraphicRectData()
		{
			int i;
			short yyyy;
			yyyy = 250;

			i = 1;
			graphicrects[i].name = "D1R Door closed";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1R Door 1/4 open up/down";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1R Door 1/2 open up/down";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1R Door 3/4 open up/down";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1R Door 1/4 closed left";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1R Door 1/2 closed left";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1R Door 3/4 closed left";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1R Door 1/4 closed right";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1R Door 1/2 closed right";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1R Door 3/4 closed right";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1 Door closed";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1 Door 1/4 open up/down";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1 Door 1/2 open up/down";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1 Door 3/4 open up/down";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1 Door 1/4 closed left";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1 Door 1/2 closed left";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1 Door 3/4 closed left";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1 Door 1/4 closed right";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1 Door 1/2 closed right";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1 Door 3/4 closed right";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1L Door closed";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1L Door 1/4 open up/down";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1L Door 1/2 open up/down";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1L Door 3/4 open up/down";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1L Door 1/4 closed left";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1L Door 1/2 closed left";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1L Door 3/4 closed left";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1L Door 1/4 closed right";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1L Door 1/2 closed right";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D1L Door 3/4 closed right";
			graphicrects[i].displayedGraphic = 117;
			i = i + 1;
			graphicrects[i].name = "D2R Door closed";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2R Door 1/4 open up/down";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2R Door 1/2 open up/down";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2R Door 3/4 open up/down";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2R Door 1/4 closed left";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2R Door 1/2 closed left";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2R Door 3/4 closed left";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2R Door 1/4 closed right";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2R Door 1/2 closed right";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2R Door 3/4 closed right";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2 Door closed";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2 Door 1/4 open up/down";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2 Door 1/2 open up/down";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2 Door 3/4 open up/down";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2 Door 1/4 closed left";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2 Door 1/2 closed left";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2 Door 3/4 closed left";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2 Door 1/4 closed right";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2 Door 1/2 closed right";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2 Door 3/4 closed right";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2L Door closed";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2L Door 1/4 open up/down";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2L Door 1/2 open up/down";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2L Door 3/4 open up/down";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2L Door 1/4 closed left";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2L Door 1/2 closed left";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2L Door 3/4 closed left";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2L Door 1/4 closed right";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2L Door 1/2 closed right";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D2L Door 3/4 closed right";
			graphicrects[i].displayedGraphic = 116;
			i = i + 1;
			graphicrects[i].name = "D3R Door closed";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3R Door 1/4 open up/down";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3R Door 1/2 open up/down";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3R Door 3/4 open up/down";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3R Door 1/4 closed left";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3R Door 1/2 closed left";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3R Door 3/4 closed left";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3R Door 1/4 closed right";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3R Door 1/2 closed right";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3R Door 3/4 closed right";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3 Door closed";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3 Door 1/4 open up/down";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3 Door 1/2 open up/down";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3 Door 3/4 open up/down";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3 Door 1/4 closed left";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3 Door 1/2 closed left";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3 Door 3/4 closed left";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3 Door 1/4 closed right";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3 Door 1/2 closed right";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3 Door 3/4 closed right";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3L Door closed";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3L Door 1/4 open up/down";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3L Door 1/2 open up/down";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3L Door 3/4 open up/down";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3L Door 1/4 closed left";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3L Door 1/2 closed left";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3L Door 3/4 closed left";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3L Door 1/4 closed right";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3L Door 1/2 closed right";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D3L Door 3/4 closed right";
			graphicrects[i].displayedGraphic = 115;
			i = i + 1;
			graphicrects[i].name = "D1R Door top frame";
			graphicrects[i].displayedGraphic = 83;
			i = i + 1;
			graphicrects[i].name = "D1 Door top frame";
			graphicrects[i].displayedGraphic = 83;
			i = i + 1;
			graphicrects[i].name = "D1L Door top frame";
			graphicrects[i].displayedGraphic = 83;
			i = i + 1;
			graphicrects[i].name = "D2R Door top frame";
			graphicrects[i].displayedGraphic = 84;
			i = i + 1;
			graphicrects[i].name = "D2 Door top frame";
			graphicrects[i].displayedGraphic = 84;
			i = i + 1;
			graphicrects[i].name = "D2L Door top frame";
			graphicrects[i].displayedGraphic = 84;
			i = i + 1;
			graphicrects[i].name = "D1Side Door (inside)";
			graphicrects[i].displayedGraphic = 78;
			i = i + 1;
			graphicrects[i].name = "D1 Door right frame";
			graphicrects[i].displayedGraphic = 79;
			graphicrects[i].mirrored = 1;
			i = i + 1;
			graphicrects[i].name = "D1 Door left frame";
			graphicrects[i].displayedGraphic = 79;
			i = i + 1;
			graphicrects[i].name = "D2 Door right frame";
			graphicrects[i].displayedGraphic = 80;
			graphicrects[i].mirrored = 1;
			i = i + 1;
			graphicrects[i].name = "D2 Door left frame";
			graphicrects[i].displayedGraphic = 80;
			i = i + 1;
			graphicrects[i].name = "D3 Door right frame";
			graphicrects[i].displayedGraphic = 81;
			graphicrects[i].mirrored = 1;
			i = i + 1;
			graphicrects[i].name = "D3 Door left frame";
			graphicrects[i].displayedGraphic = 81;
			i = i + 1;
			graphicrects[i].name = "D3R Door left frame";
			graphicrects[i].displayedGraphic = 82;
			i = i + 1;
			graphicrects[i].name = "D3L Door right frame";
			graphicrects[i].displayedGraphic = 82;
			graphicrects[i].mirrored = 1;
			i = i + 1;
			graphicrects[i].name = "D3 Wall* (F&S)";
			graphicrects[i].displayedGraphic = 89;
			i = i + 1;
			graphicrects[i].name = "D3L Wall* (F&S)";
			graphicrects[i].displayedGraphic = 89;
			i = i + 1;
			graphicrects[i].name = "D3R Wall* (F&S)";
			graphicrects[i].displayedGraphic = 89;
			i = i + 1;
			graphicrects[i].name = "D2 Wall* (F&S)";
			graphicrects[i].displayedGraphic = 88;
			i = i + 1;
			graphicrects[i].name = "D2L Wall* (F&S)";
			graphicrects[i].displayedGraphic = 88;
			i = i + 1;
			graphicrects[i].name = "D2R Wall* (F&S)";
			graphicrects[i].displayedGraphic = 88;
			i = i + 1;
			graphicrects[i].name = "D1 Wall* (F&S)";
			graphicrects[i].displayedGraphic = 87;
			i = i + 1;
			graphicrects[i].name = "D1L Wall* (F&S)";
			graphicrects[i].displayedGraphic = 87;
			i = i + 1;
			graphicrects[i].name = "D1R Wall* (F&S)";
			graphicrects[i].displayedGraphic = 87;
			i = i + 1;
			graphicrects[i].name = "Viewport Rect";
			graphicrects[i].displayedGraphic = 0;
			graphicrects[i].description = "This rect represents the entire area that displays the view.  Editing will cause unknown results!";
			i = i + 1;
			graphicrects[i].name = "D0L Wall";
			graphicrects[i].displayedGraphic = 86;
			i = i + 1;
			graphicrects[i].name = "D0R Wall";
			graphicrects[i].displayedGraphic = 85;
			i = i + 1;
			graphicrects[i].name = "D3R left wall";
			graphicrects[i].displayedGraphic = 90;
			i = i + 1;
			graphicrects[i].name = "D3L right wall";
			graphicrects[i].displayedGraphic = 90;
			graphicrects[i].mirrored = 1;
			i = i + 1;
			graphicrects[i].name = "Temporary viewport bounds";
			graphicrects[i].displayedGraphic = 0;
			graphicrects[i].description = "Not a standard graphic rect.  This happens to have two rects, width/height are x1/x2 of the second rect, and srcx/srcy are the y1/y2 of the second rect.";
			i = i + 1;
			graphicrects[i].name = "D0R Ceiling Pit";
			graphicrects[i].displayedGraphic = 68;
			graphicrects[i].mirrored = 1;
			i = i + 1;
			graphicrects[i].name = "D0 Ceiling Pit";
			graphicrects[i].displayedGraphic = 69;
			i = i + 1;
			graphicrects[i].name = "D0L Ceiling Pit";
			graphicrects[i].displayedGraphic = 68;
			i = i + 1;
			graphicrects[i].name = "D1R Ceiling Pit";
			graphicrects[i].displayedGraphic = 66;
			graphicrects[i].mirrored = 1;
			i = i + 1;
			graphicrects[i].name = "D1 Ceiling Pit";
			graphicrects[i].displayedGraphic = 67;
			i = i + 1;
			graphicrects[i].name = "D1L Ceiling Pit";
			graphicrects[i].displayedGraphic = 66;
			i = i + 1;
			graphicrects[i].name = "D2R Ceiling Pit";
			graphicrects[i].displayedGraphic = 64;
			graphicrects[i].mirrored = 1;
			i = i + 1;
			graphicrects[i].name = "D2 Ceiling Pit";
			graphicrects[i].displayedGraphic = 65;
			i = i + 1;
			graphicrects[i].name = "D2L Ceiling Pit";
			graphicrects[i].displayedGraphic = 64;
			i = i + 1;
			graphicrects[i].name = "D0R Floor Pit";
			graphicrects[i].displayedGraphic = 56;
			graphicrects[i].mirrored = 1;
			i = i + 1;
			graphicrects[i].name = "D0 Floor Pit";
			graphicrects[i].displayedGraphic = 57;
			i = i + 1;
			graphicrects[i].name = "D0L Floor Pit";
			graphicrects[i].displayedGraphic = 56;
			i = i + 1;
			graphicrects[i].name = "D1R Floor Pit";
			graphicrects[i].displayedGraphic = 54;
			graphicrects[i].mirrored = 1;
			i = i + 1;
			graphicrects[i].name = "D1 Floor Pit";
			graphicrects[i].displayedGraphic = 55;
			i = i + 1;
			graphicrects[i].name = "D1L Floor Pit";
			graphicrects[i].displayedGraphic = 54;
			i = i + 1;
			graphicrects[i].name = "D2R Floor Pit";
			graphicrects[i].displayedGraphic = 52;
			graphicrects[i].mirrored = 1;
			i = i + 1;
			graphicrects[i].name = "D2 Floor Pit";
			graphicrects[i].displayedGraphic = 53;
			i = i + 1;
			graphicrects[i].name = "D2L Floor Pit";
			graphicrects[i].displayedGraphic = 52;
			i = i + 1;
			graphicrects[i].name = "D3R Floor Pit";
			graphicrects[i].displayedGraphic = 50;
			graphicrects[i].mirrored = 1;
			i = i + 1;
			graphicrects[i].name = "D3 Floor Pit";
			graphicrects[i].displayedGraphic = 51;
			i = i + 1;
			graphicrects[i].name = "D3L Floor Pit";
			graphicrects[i].displayedGraphic = 50;
			i = i + 1;
			graphicrects[i].name = "D0R stairs";
			graphicrects[i].displayedGraphic = 108;
			graphicrects[i].mirrored = 1;
			i = i + 1;
			graphicrects[i].name = "D0L stairs";
			graphicrects[i].displayedGraphic = 108;
			i = i + 1;
			graphicrects[i].name = "D1R stairs down L/R";
			graphicrects[i].displayedGraphic = 107;
			graphicrects[i].mirrored = 1;
			i = i + 1;
			graphicrects[i].name = "D1L stairs down L/R";
			graphicrects[i].displayedGraphic = 107;
			i = i + 1;
			graphicrects[i].name = "D1R stairs up L/R";
			graphicrects[i].displayedGraphic = 106;
			graphicrects[i].mirrored = 1;
			i = i + 1;
			graphicrects[i].name = "D1L stairs up L/R";
			graphicrects[i].displayedGraphic = 106;
			i = i + 1;
			graphicrects[i].name = "D2R stairs L/R";
			graphicrects[i].displayedGraphic = 105;
			graphicrects[i].mirrored = 1;
			i = i + 1;
			graphicrects[i].name = "D2L stairs L/R";
			graphicrects[i].displayedGraphic = 105;
			i = i + 1;
			graphicrects[i].name = "D0 stairs down";
			graphicrects[i].displayedGraphic = 104;
			graphicrects[i].mirrored = 1;
			i = i + 1;
			graphicrects[i].name = "D0 stairs down";
			graphicrects[i].displayedGraphic = 104;
			i = i + 1;
			graphicrects[i].name = "D1R stairs down";
			graphicrects[i].displayedGraphic = 102;
			graphicrects[i].mirrored = 1;
			i = i + 1;
			graphicrects[i].name = "D1 stairs down";
			graphicrects[i].displayedGraphic = 103;
			i = i + 1;
			graphicrects[i].name = "D1L stairs down";
			graphicrects[i].displayedGraphic = 102;
			i = i + 1;
			graphicrects[i].name = "D2R stairs down";
			graphicrects[i].displayedGraphic = 100;
			graphicrects[i].mirrored = 1;
			i = i + 1;
			graphicrects[i].name = "D2 stairs down";
			graphicrects[i].displayedGraphic = 101;
			i = i + 1;
			graphicrects[i].name = "D2L stairs down";
			graphicrects[i].displayedGraphic = 100;
			i = i + 1;
			graphicrects[i].name = "D3R stairs down";
			graphicrects[i].displayedGraphic = 98;
			graphicrects[i].mirrored = 1;
			i = i + 1;
			graphicrects[i].name = "D3 stairs down";
			graphicrects[i].displayedGraphic = 99;
			i = i + 1;
			graphicrects[i].name = "D3L stairs down";
			graphicrects[i].displayedGraphic = 98;
			i = i + 1;
			graphicrects[i].name = "D0 stairs up";
			graphicrects[i].displayedGraphic = 97;
			graphicrects[i].mirrored = 1;
			i = i + 1;
			graphicrects[i].name = "D0 stairs up";
			graphicrects[i].displayedGraphic = 97;
			i = i + 1;
			graphicrects[i].name = "D1R stairs up";
			graphicrects[i].displayedGraphic = 95;
			graphicrects[i].mirrored = 1;
			i = i + 1;
			graphicrects[i].name = "D1 stairs up";
			graphicrects[i].displayedGraphic = 96;
			i = i + 1;
			graphicrects[i].name = "D1L stairs up";
			graphicrects[i].displayedGraphic = 95;
			i = i + 1;
			graphicrects[i].name = "D2R stairs up";
			graphicrects[i].displayedGraphic = 93;
			graphicrects[i].mirrored = 1;
			i = i + 1;
			graphicrects[i].name = "D2 stairs up";
			graphicrects[i].displayedGraphic = 94;
			i = i + 1;
			graphicrects[i].name = "D2L stairs up";
			graphicrects[i].displayedGraphic = 93;
			i = i + 1;
			graphicrects[i].name = "D3R stairs up";
			graphicrects[i].displayedGraphic = 91;
			graphicrects[i].mirrored = 1;
			i = i + 1;
			graphicrects[i].name = "D3 stairs up";
			graphicrects[i].displayedGraphic = 92;
			i = i + 1;
			graphicrects[i].name = "D3L stairs up";
			graphicrects[i].displayedGraphic = 91;
		}//end sub
	}//end class
}
