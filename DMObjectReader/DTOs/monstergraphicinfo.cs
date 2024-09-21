using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.DTOs
{
    public class monstergraphicinfo
    {

        /*	'Invalid_string_refer_to_original_code*/
        /*	'Invalid_string_refer_to_original_code*/
        /*	'Invalid_string_refer_to_original_code*/
        /*	'Invalid_string_refer_to_original_code*/
        /*	'    Octet 05 : largeur de l'image de la créature vue de face divisée par 2 (NB : la largeur de toutes*/
        /*	'           les images des créatures est un multiple de 16). Si elle existe, l'image de la vue de*/
        /*	'           dos doit avoir exactement la même largeur que l'image de la vue de face, car c'est la*/
        /*	'           même valeur qui est utilisée.*/
        /*	'    Octet 06 : hauteur de l'image de la créature vue de face, en pixels. Si elle existe, l'image de*/
        /*	'           la vue de dos doit avoir exactement la même hauteur que l'image de la vue de face,*/
        /*	'           car c 'est la même valeur qui est utilisée.*/
        /*	'    Octet 07 : largeur de l'image de la créature vue de profil divisée par 2 (largeur=multiple de 16).*/
        /*	'    Octet 08 : hauteur de l'image de la créature vue de profil, en pixels.*/
        /*	'    Octet 09 : largeur de l'image de la créature en attaque divisée par 2 (largeur=multiple de 16).*/
        /*	'    Octet 10 : hauteur de l'image de la créature en attaque, en pixels.*/
        /*	'    Octet 11 : - Les 4 bits de gauche indiquent le groupe de coordonnées de référence qu'il faut*/
        /*	'Invalid_string_refer_to_original_code*/
        /*	'Invalid_string_refer_to_original_code*/
        /*	'            - groupe 1 => position basse (groupes de 1 ou 2 créatures seulement)*/
        /*	'Invalid_string_refer_to_original_code*/
        /*	'           - Les 4 bits de droite donnent l'index de la couleur de transparence de l'image de la*/
        /*	'             créature (valeur comprise entre 0 et 15). Cette couleur doit être la même pour*/
        /*	'             toutes les vues (face, profil, dos, attaque). Exemple : j'ouvre les images du*/
        /*	'Invalid_string_refer_to_original_code*/
        /*	'             pour le fond : c'est la couleur 13, donc les 4 bits de droite de l'octet 0843 auront*/
        /*	'             pour valeur 1101.*/
        /*	'    Octet 12 : change les couleurs 09 (orange) et 10 (rose) dans la vue du donjon, en utilisant des*/
        /*	'           couleurs provenant d'une autre palette (on va l'appeler "palette N°2" pour plus de*/
        /*	'Invalid_string_refer_to_original_code*/
        /*	'           (exemple : couleur violette du ver géant, le bleu des trolls, le rouge du dragon...).*/
        /*	'           - les 4 bits de gauche donnent l'indice de la couleur, issue de la palette N°2, qui*/
        /*	'             remplacera la couleur 10 (rose) dans la vue du donjon.*/
        /*	'           - les 4 bits de droite donnent l'indice de la couleur, issue de la palette N°2, qui*/
        /*	'             remplacera la couleur 09 (orange) dans la vue du donjon.*/
        /*	'           Ces changements de couleur fonctionnent d'une façon assez particulière, expliquée en*/
        /*	'           détail dans le fichier "Couleurs des créatures.txt"*/


        public byte byte0; // ???
        public byte graphicnum; // 446+byte1 = graphic of monster starting
        public byte byte2; // ???
        public byte byte3; // ???
        public byte widthfront; // half the width of the picture of 446+byte1 (front/back)
        public byte heightfront; // height of 446+byte1 picture (front/back)
        public byte widthside; // half the width of the picture of 446+byte1+1? (side view)
        public byte heightside; // height ""
        public byte widthattack; // half width of attacking graphic
        public byte heightattack; // height ""
        public byte byte10; // 0xF0: monster graphic group, 0x0F: transparent pixel number
        public byte byte11; // 0xF0: replace color 10 with this, 0x0F: color 9

        public byte graphicgroup()
        {
            return App.CByte(App.CShort(byte10 & 0xF0) / 0x10);
        } //End function
        public byte transparentpixel()
        {
            return App.CByte(App.CShort(byte10 & 0xF) / 0x1);
        } //End function
        public byte colora()
        {
            return App.CByte(App.CShort(byte11 & 0xF0) / 0x10);
        } //End function
        public byte colorb()
        {
            return App.CByte(App.CShort(byte11 & 0xF) / 0x1);
        } //End function

        public void setgraphicgroup(ref byte num)
        {
            
            if (num > 15)
            {
                num = 15;
            }
            else if (num < 0)
            {
                num = 0;
            }
            byte10 = App.CByte(App.CShort(byte10 & 0xF) + (num * 0x10));
        } //End Sub
        public void settransparentpixel(ref byte num)
        {
            
            if (num > 15)
            {
                num = 15;
            }
            else if (num < 0)
            {
                num = 0;
            }
            byte10 = App.CByte(App.CShort(byte10 & 0xF0) + (num * 0x1));
        } //End Sub
        public void setcolora(ref byte num)
        {
            
            if (num > 15)
            {
                num = 15;
            }
            else if (num < 0)
            {
                num = 0;
            }
            byte11 = App.CByte(App.CShort(byte11 & 0xF) + (num * 0x10));
        } //End Sub
        public void setcolorb(ref byte num)
        {
            if (num > 15)
            {
                num = 15;
            }
            else if (num < 0)
            {
                num = 0;
            }
            byte11 = App.CByte(App.CShort(byte11 & 0xF0) + (num * 0x1));
        } //End Sub
    } //End Class
}
