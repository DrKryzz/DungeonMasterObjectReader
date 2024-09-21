using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.DTOs
{
	public class missilegraphicinfo
	{



		public byte GraphicNum; //316+byte0 = graphic of object flying start
		public byte GraphicOffset;
		public byte Width; //half the width of graphic (all same sizes...)
		public byte Height; //height of graphic
		
		public byte Spell_Renamed; //if 1, its a spell graphic, else its a projectile item
		public byte Byte5;

		/*	'0x01 - animate it, somehow reversing the image a bit while its moving?*/
		/*	'0x02 - has no 'coming towards you' type graphic*/
		/*	'0x10 - has a side view*/

		public byte b4_spell()
		{
			return App.CByte(App.CShort(Spell_Renamed & 0x1) / 0x1);
		} //End function
		public void setb4_spell(ref short num)
		{
			
			Spell_Renamed = (byte)(Spell_Renamed & 0xFE);
			if (num == 1)
			{
				Spell_Renamed = (byte)(Spell_Renamed + 0x1);
			}
		} //End Sub
		public byte b5_animate()
		{
			return App.CByte(App.CShort(Byte5 & 0x1) / 0x1);
		} //End function
		public void setb5_animate(ref short num)
		{
			Byte5 = (byte)(Byte5 & 0xFE);
			if (num == 1)
			{
				Byte5 = (byte)(Byte5 + 0x1);
			}
		} //End Sub
		public byte b5_frontview()
		{
			var ans = App.CShort((Byte5 & 0x2) / 0x2);
			if (ans == 0)
			{
				return 1;
			}
			else
			{
				return 0;
			}
		} //End function
		public void setb5_frontview(ref short num)
		{
			
			Byte5 = (byte)(Byte5 & 0xFD);
			if (num == 0)
			{
				Byte5 = (byte)(Byte5 + 0x2);
			}
		} //End Sub
		public byte b5_sideview()
		{
			return App.CByte(App.CShort(Byte5 & 0x10) / 0x10);
		} //End function
		public void setb5_sideview(ref short num)
		{
			Byte5 = (byte)(Byte5 & 0xEF);
			if (num == 1)
			{
				Byte5 = (byte)(Byte5 + 0x10);
			}
		} //End Sub

		/*	'Invalid_string_refer_to_original_code*/
		/*	'*/
		/*	'    Octet 2 : cet octet semble servir pour un système d'indexation :*/
		/*	'          - Pour les missiles de type "arme" qui disposent d'une vue de face, de dos, et de*/
		/*	'            profil, la valeur de cet octet progresse de 18 en 18 (voir différence de valeur*/
		/*	'            entre les octets 1560, 1566, 1572, 1578). On peut donc supposer que le programme*/
		/*	'            stocke quelque part 6 images de différentes tailles pour la vue de face, 6 pour*/
		/*	'            la vue de dos et 6 pour la vue de profil... (cette hypothèse est renforcée par*/
		/*	'            le fait que dans le jeu, si on lance une flèche par exemple, elle passe par 6*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'            différentes!)*/
		/*	'          - Pour le shuriken, qui ne dispose que d'une vue de face et une de profil, la*/
		/*	'            différence entre les valeurs des octets 1614 et 1620 est de 12 => ça collerait,*/
		/*	'            6 images pour la vue de face + 6 pour la vue de profil...*/
		/*	'          - Pour les sortilèges par contre... les octets 1578 et 1584 ont la même valeur, de*/
		/*	'            même que les octets 1620, 1626, 1632, 1638 => cela laisserait donc supposer que*/
		/*	'            le programme ne stocke pas d'images de différentes tailles pour ces missiles...*/
		/*	'            mais ceci pourrait s'expliquer par le fait que la taille d'un missile de type*/
		/*	'            "magique" dépend aussi de la puissance du sort!*/
		/*	'*/
		/*	'    Octet 3 : largeur de l'image du missile divisée par 2 (toutes les images des missiles ont*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'          Remarque : si le missile dispose de plusieurs images (face, dos, profil), elles*/
		/*	'          doivent toutes avoir les mêmes dimensions.*/
		/*	'*/
		/*	'    Octet 4 : hauteur de l'image du missile, en pixels.*/
		/*	'*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'          octet indique en fait si la taille de l'image doit varier en fonction d'un "facteur de*/
		/*	'          puissance "! Exemple amusant : changer la valeur 00 de l'octet 1563 en 01. Créer un "*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'          la flèche est minuscule :))). Lancer ensuite la flèche avec un arc, puis une arbalète,*/
		/*	'          puis avec l'arbalète "speedbow"...*/
		/*	'*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'                s 'il existe également une vue de dos (voir bit 7), alors on prend l'image*/
		/*	'                qui suit celle de la vue de dos pour la vue de profil, sinon, on prend*/
		/*	'                l 'image qui suit celle de la vue de face.*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'                missile.l 'image qui suit l'image de la vue de face sera donc utilisée pour*/
		/*	'                cette vue de dos.*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'                par exemple, on aura une alternance vue de face/vue de dos, avec un effet*/
		/*	'                de "miroir" qui alternera tête de la hache en haut/en bas, donnant*/
		/*	'                l 'impression qu'elle tourne sur elle même en vol (ou si on voit la hache de*/
		/*	'                profil alternance effet "miroir" horizontal/vertical sur la vue de profil).*/
	} //End Class
}
