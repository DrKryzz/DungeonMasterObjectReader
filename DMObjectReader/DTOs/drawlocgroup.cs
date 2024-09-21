using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.DTOs
{
	public class drawlocgroup
	{
		private byte[,] locs = new byte[6,3]; //5,2 original -> increase both with 1
		/*	'-- 1st index --*/
		/*	'1 = back left*/
		/*	'2 = back right*/
		/*	'3 = front right'*/
		/*	'4 = front left*/
		/*	'5 = center or niche*/

		/*	'-- 2nd index --*/
		/*	'1 = x coordinate of middle of monster/item*/
		/*	'2 = y coordinate of bottom of monster/item*/

		public byte loc_Renamed(short i, short j)
		{
			return locs[i, j];
		} //End function

		public void setloc(short i, short j, byte b)
		{
			locs[i, j] = b;
		} //End Sub
	} //End Class
}
