using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.DTOs
{
	public class animationinfo
	{
		/*	'  TAG008c98(pnt_4,*/
		/*	'            d.pViewportBMP,*/
		/*	'            A2,*/
		/*	'            TAG022d38(0),*/
		/*	'            rectpos,*/
		/*	'            sw(uA3[1] + GraphicRandom(2)),*/
		/*	'            sw(GraphicRandom(32)),*/
		/*	'            112,*/
		/*	'            uA3[2],*/
		/*	'            uA3[6],*/
		/*	'            0);*/
		public byte graphicaddress;
		/*	'offset +73 = graphic to display*/
		/*	' probably set my runtime to distinguish teleporter/fluxcage*/

		public byte graphicrand;
		/*	' ????*/
		public byte TransparentPixel;
		/*	'&7f = transparent pixel*/
		/*	'if &80 is set, then?*/
		public byte GraphicNum;
		/*	'if = 255, then a2 = NULL, no mask?*/
		/*	'else*/
		/*	'&7F = graphic num, add 69 as offset to get the MASK's graphic num*/
		/*	'a2 = viewport*/
		/*	'if &80 is set, mirror it*/

		public byte width;
		public byte height;
		public byte srcx;
		public byte srcy;
		/*	'always 64, but completely unused.*/
	} //End Class
}
