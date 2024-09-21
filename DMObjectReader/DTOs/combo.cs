using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.DTOs
{
	public class combo
	{

		public byte attack1;
		public byte attack2;
		public byte attack3;
		public byte byte4;
		public byte byte5;
		public byte byte6;
		public byte unused1;
		public byte unused2;

		public file560 f560; //initiated from file560

		public string tostring_Renamed()
		{
			string ans = "";
			if (attack1 >= 0 && attack1 <= 44)
			{
				ans = f560.attack((short)(attack1 + 1)).name;
				return ans;
			}
			else
			{
				ans = "-";
				return ans;
			}

			ans = ans + "/";

			if (attack2 >= 0 && attack3 < 44)
			{
				ans = ans + f560.attack((short)(attack2 + 1)).name;
			}
			else
			{
				ans = ans + "-";
			}

			ans = ans + "/";

			if (attack2 >= 0 && attack3 <= 44)
			{
				ans = ans + f560.attack((short)(attack3 + 1)).name;
			}
			else
			{
				ans = ans + "-";
			}
			return ans;
		} //End function
		public byte skill1()
		{
			return App.CByte(byte4 & 0x7F);
		} //End function
		public byte skill2()
		{
			return App.CByte(byte5 & 0x7F);
		} //End function
		public byte skill3()
		{
			return App.CByte(byte6 & 0x7F);
		} //End function
		public short charges1()
		{
			if ((byte4 & 0x80) > 0)
			{
				return 1;
			}
			else
			{
				return 0;
			}
		} //End function
		public short charges2()
		{
			if ((byte5 & 0x80) > 0)
			{
				return 1;
			}
			else
			{
				return 0;
			}
		} //End function
		public short charges3()
		{
			if ((byte6 & 0x80) > 0)
			{
				return 1;
			}
			else
			{
				return 0;
			}
		} //End function
		public void setskill1( short num)
		{

		if (num > 127)
			{
				num = 127;
			}
			if (num < 0)
			{
				num = 0;
			}
			byte4 = App.CByte(App.CShort(byte4 & 0x80) + num);
		} //End Sub
		public void setskill2( short num)
		{

		if (num > 127)
			{
				num = 127;
			}
			if (num < 0)
			{
				num = 0;
			}
			byte5 = App.CByte(App.CShort(byte5 & 0x80) + num);
		} //End Sub
		public void setskill3( short num)
		{
	
		if (num > 127)
			{
				num = 127;
			}
			if (num < 0)
			{
				num = 0;
			}
			byte6 = App.CByte(App.CShort(byte6 & 0x80) + num);
		} //End Sub
		public void setcharges1( short num)
		{

		if (num == 1)
			{
				byte4 = App.CByte(App.CShort(byte4 & 0x7F) + 0x80);
			}
			else
			{
				byte4 = App.CByte(byte4 & 0x7F);
			}
		} //End Sub
		public void setcharges2( short num)
		{

		if (num == 1)
			{
				byte5 = App.CByte(App.CShort(byte5 & 0x7F) + 0x80);
			}
			else
			{
				byte5 = App.CByte(byte5 & 0x7F);
			}
		} //End Sub
		public void setcharges3( short num)
		{

		if (num == 1)
			{
				byte6 = App.CByte(App.CShort(byte6 & 0x7F) + 0x80);
			}
			else
			{
				byte6 = App.CByte(byte6 & 0x7F);
			}
		} //End Sub
	} //End Class
}
