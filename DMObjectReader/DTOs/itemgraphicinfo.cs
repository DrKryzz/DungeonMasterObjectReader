using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.DTOs
{
	public class itemgraphicinfo
	{
		public byte GraphicNum; //360+byte0 = graphic for item on floor
		public byte GraphicOffset;
		public byte Width; //half the width
		public byte Height; //height
		public byte Byte4; //0x01 : mirrable on floor (for boxes) 0x10: has special niche graphic
		public byte FloorItemGroup; //flooritemgroup

		public byte b4_nicheview()
		{
			return App.CByte(App.CShort(Byte4 & 0x10) / 0x10);
		} //End function
		public void setb4_nicheview(ref short num)
		{
			Byte4 = (byte)(Byte4 & 0xEF);
			if (num == 1)
			{
				Byte4 = (byte)(Byte4 + 0x10);
			}
		} //End Sub

		public byte b4_mirrored()
		{
			return App.CByte(App.CShort(Byte4 & 0x1) / 0x1);
		} //End function
		public void setb4_mirrored(ref short num)
		{
			Byte4 = (byte)(Byte4 & 0xFE);
			if (num == 1)
			{
				Byte4 = (byte)(Byte4 + 0x1);
			}
		} //End Sub

		/*	'Invalid_string_refer_to_original_code*/
		/*	'    Octet 2 : cet octet semble servir pour un système d'indexation : sa valeur progresse de 2 en 2*/
		/*	'          d 'un objet au suivant (3 tailles possibles pour l'affichage des objets en fonction de la*/
		/*	'          distance => on suppose que le programme stocke quelque part les 2 images de l'objet*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'          est de 6), et les 2 boû‘es magiques (différence de 4 entre les valeurs des octets 2022 et*/
		/*	'          2028, et entre celles des octets 2118 et 2124). Ces 3 objets sont particuliers, puisque*/
		/*	'          ce sont les seuls pour lesquels il y a un effet "miroir", qui fait que leur image est*/
		/*	'          différente selon qu'ils se trouvent sur la gauche ou sur la droite du joueur (=> on*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'    Octet 3 : largeur de l'image de l'objet divisée par 2 (remarque : toutes les images des objets ont*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'    Octet 4 : hauteur de l'image de l'objet, en pixels. (remarque : la hauteur de l'image d'un objet ne*/
		/*	'          doit pas dépasser 48 pixels, sinon cette image va "déborder" de la vue du donjon*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'                      ______      |      ______*/
		/*	'                     /     /|     |     |\     \      (Remarque : ce dessin est sans*/
		/*	'                    |     | |     |     | |     |      doute superflu mais j'avais*/
		/*	'                    |     | | <---|---> | |     |      envie de le faire, alors je*/
		/*	'                    |_____|/      |      \|_____|      me suis dit "juste fais-le"*/
		/*	'                              |            et je l'ai fait)*/
		/*	'*/
		/*	'    Octet 6 : groupe de coordonnées de référence qu'il faut utiliser pour afficher l'image de cet*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'Invalid_string_refer_to_original_code*/
		/*	'          - le groupe 0 concerne 30 objets, de largeur moyenne=55 pixels et de hauteur moyenne=18*/
		/*	'          - le groupe 1 concerne 50 objets, de largeur moyenne=34 pixels et de hauteur moyenne=10*/
		/*	'          - le groupe 2 concerne  5 objets, de largeur moyenne=70 pixels et de hauteur moyenne=23*/
		/*	'          Les objets les plus "petits" sont donc généralement dans le groupe 1, les plus "gros"*/
		/*	'          dans le groupe 2, et les objets de taille "moyenne" dans le groupe 0.*/
	} //End Class
}
