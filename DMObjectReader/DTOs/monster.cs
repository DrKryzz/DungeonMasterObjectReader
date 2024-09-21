using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.DTOs
{
    public class monster
    {
        public byte IdNum;
        public byte attacksound;
        public int Word2;
        public int Word4;
        public byte Speed;
        public byte MinSpeed;
        public byte Armor_Renamed;
        public byte BaseHealth;
        public byte quickness;
        public byte Poison;
        public byte power;
        public byte Unused1;
        public int word14;
        public int word16;
        public int word18;
        public int word20;
        public byte byte22;
        public byte byte23;
        public byte damagetype;
        public byte Unused2;


        /*	'byte1: 0x01 tested for itself?*/
        /*	'byte2: if non-zero, play sound number for attack*/
        /*	'word3:*/
        /*	'0x0003: 0 = 1/4 space, 1 = 1/2 space, 2 = 1 space (dragon)*/
        /*	'0x0004: attacks no matter if facing or not (always faces)*/
        /*	'0x0008: can go through walls*/
        /*	'0x0010: dumb (won't hit a live character sometimes)*/
        /*	'0x0020: levitates*/
        /*	'0x0040: if set, monster only affected by disrupt/vorpal blades*/
        /*	'        if 0, disrupting automisses*/
        /*	'        if 0, can hit with normal weapons*/
        /*	'0x0080: immune to missiles (except dispell)*/
        /*	'0x0100: short... doors close more on top of them*/
        /*	'0x0200: drop items when die*/
        /*	'0x0400: absorbs arrows, ninja stars, etc*/
        /*	'0x0800: can see invisible*/
        /*	'0x1000: can see in dark*/
        /*	'0x2000: invulnerable?*/

        public byte w2_space()
        {
            return (byte)(Word2 & 0x3);
        } //End function
        public byte w2_unknown1()
        {
            return App.CByte(App.CShort(Word2 & 0x4) / 0x4);
        } //End function
        public byte w2_walkthroughwalls()
        {
            return App.CByte(App.CShort(Word2 & 0x8) / 0x8);
        } //End function
        public byte w2_dumb()
        {
            return App.CByte(App.CShort(Word2 & 0x10) / 0x10);
        } //End function
        public byte w2_levitates()
        {
            return App.CByte(App.CShort(Word2 & 0x20) / 0x20);
        } //End function
        public byte w2_nonmaterial()
        {
            return App.CByte(App.CShort(Word2 & 0x40) / 0x40);
        } //End function
        public byte w2_immunetomissiles()
        {
            return App.CByte(App.CShort(Word2 & 0x80) / 0x80);
        } //End function
        public byte w2_short()
        {
            return App.CByte(App.CShort(Word2 & 0x100) / 0x100);
        } //End function
        public byte w2_dropsitems()
        {
            return App.CByte(App.CShort(Word2 & 0x200) / 0x200);
        } //End function
        public byte w2_absorbsmissiles()
        {
            return App.CByte(App.CShort(Word2 & 0x400) / 0x400);
        } //End function
        public byte w2_seeinvisible()
        {
            return App.CByte(App.CShort(Word2 & 0x800) / 0x800);
        } //End function
        public byte w2_seedarkness()
        {
            return App.CByte(App.CShort(Word2 & 0x1000) / 0x1000);
        } //End function
        public byte w2_invulnerable()
        {
            return App.CByte(App.CShort(Word2 & 0x2000) / 0x2000);
        } //End function

        public void set_w2_space(ref int num)
        {
            if (num > 3)
            {
                num = 3;
            }
            if (num < 0)
            {
                num = 0;
            }
            Word2 = App.CByte(App.CShort(Word2 & 0xFFFC) + num);
        } //End Sub
        public void set_w2_unknown1(ref int num)
        {

            Word2 = (Word2 & (0xFFFF - 0x4));
            if (num == 1)
            {
                Word2 = Word2 + 0x4;
            }
        } //End Sub
        public void set_w2_walkthroughwalls(ref int num)
        {
            Word2 = (Word2 &(0xFFFF - 0x8));
            if (num == 1)
            {
                Word2 = Word2 + 0x8;
            }
        } //End Sub
        public void set_w2_dumb(ref int num)
        {
            Word2 = (Word2 &(0xFFFF - 0x10));
            if (num == 1)
            {
                Word2 = Word2 + 0x10;
            }
        } //End Sub
        public void set_w2_levitates(ref int num)
        {
            
Word2 = (Word2 & (0xFFFF - 0x20));
            if (num == 1)
            {
                Word2 = Word2 + 0x20;
            }
        } //End Sub
        public void set_w2_nonmaterial(ref int num)
        {
            Word2 = (Word2 & (0xFFFF - 0x40));
            if (num == 1)
            {
                Word2 = Word2 + 0x40;
            }
        } //End Sub
        public void set_w2_immunetomissiles(ref int num)
        {
            
Word2 = (Word2 & (0xFFFF - 0x80));
            if (num == 1)
            {
                Word2 = Word2 + 0x80;
            }
        } //End Sub
        public void set_w2_short(ref int num)
        {
            
Word2 = (Word2 & (0xFFFF - 0x100));
            if (num == 1)
            {
                Word2 = Word2 + 0x100;
            }
        } //End Sub
        public void set_w2_dropsitems(ref int num)
        {
            
Word2 = (Word2 & (0xFFFF - 0x200));
            if (num == 1)
            {
                Word2 = Word2 + 0x200;
            }
        } //End Sub
        public void set_w2_absorbsmissiles(ref int num)
        {
         
Word2 = (Word2 & (0xFFFF - 0x400));
            if (num == 1)
            {
                Word2 = Word2 + 0x400;
            }
        } //End Sub
        public void set_w2_seeinvisible(ref int num)
        {
         
Word2 = (Word2 & (0xFFFF - 0x800));
            if (num == 1)
            {
                Word2 = Word2 + 0x800;
            }
        } //End Sub
        public void set_w2_seedarkness(ref int num)
        {
            
Word2 = (Word2 & (0xFFFF - 0x1000));
            if (num == 1)
            {
                Word2 = Word2 + 0x1000;
            }
        } //End Sub
        public void set_w2_invulnerable(ref int num)
        {
            
Word2 = (Word2 & (0xFFFF - 0x2000));
            if (num == 1)
            {
                Word2 = Word2 + 0x2000;
            }
        } //End Sub

        /*	'word5: This has alot to do with a monsters graphics i think...*/
        /*	'0x0003: something to do with scaling?*/
        /*	'0x0004: making a mirror image for something?*/
        /*	'0x0008: side image exists*/
        /*	'0x0010: back image exists*/
        /*	'0x0020: attack graphic exists*/
        /*	'0x0040: 0*/
        /*	'0x0080: ??? __ these 2 come in pairs, both set or not.*/
        /*	'0x0100: ??? ^^*/
        /*	'0x0200: mirrored attack image*/
        /*	'0x0400: if 0, mirror attack image while mid-attack*/
        /*	'0x0800: 0*/
        /*	'0x3000: shiftgroup x*/
        /*	'0xC000: shiftgroup y*/

        public byte w4_frontgraphics()
        {
            return App.CByte(Word4 & 0x3);
        } //End function
        public byte w4_mirrorimage()
        {
            return App.CByte((Word4 & 0x4) / 0x4);
        } //End function
        public byte w4_sideimage()
        {
            return App.CByte((Word4 & 0x8) / 0x8);
        } //End function
        public byte w4_backimage()
        {
            return App.CByte((Word4 & 0x10) / 0x10);
        } //End function
        public byte w4_attackimage()
        {
            return App.CByte((Word4 & 0x20) / 0x20);
        } //End function
        public byte w4_unknown1()
        {
            return App.CByte((Word4 & 0x40) / 0x40);
        } //End function
        public byte w4_unknown2()
        {
            return App.CByte((Word4 & 0x80) / 0x80);
        } //End function
        public byte w4_unknown3()
        {
            return App.CByte((Word4 & 0x100) / 0x100);
        } //End function
        public byte w4_mirroredattack()
        {
            return App.CByte((Word4 & 0x200) / 0x200);
        } //End function
        public byte w4_dontmirrorwhileattacking()
        {
            return App.CByte((Word4 & 0x400) / 0x400);
        } //End function
        public byte w4_unknown4()
        {
            return App.CByte((Word4 & 0x800) / 0x800);
        } //End function
        public byte w4_xshiftgroup()
        {
            return App.CByte((Word4 & 0x3000) / 0x1000);
        } //End function
        public byte w4_yshiftgroup()
        {
            return App.CByte((Word4 & 0xC000) / 0x4000);
        } //End function

        public void set_w4_frontgraphics(ref int num)
        {

Word4 = (Word4 & (0xFFFF - 0x3));
            Word4 = Word4 + App.CShort(num & 0x3);
        } //End Sub
        public void set_w4_mirrorimage(ref int num)
        {

Word4 = (Word4 & (0xFFFF - 0x4));
            if (num == 1)
            {
                Word4 = Word4 + 0x4;
            }
        } //End Sub
        public void set_w4_sideimage(ref int num)
        {
            
Word4 = (Word4 & (0xFFFF - 0x8));
            if (num == 1)
            {
                Word4 = Word4 + 0x8;
            }
        } //End Sub
        public void set_w4_backimage(ref int num)
        {
            
Word4 = (Word4 & (0xFFFF - 0x10));
            if (num == 1)
            {
                Word4 = Word4 + 0x10;
            }
        } //End Sub
        public void set_w4_attackimage(ref int num)
        {
            
Word4 = (Word4 & (0xFFFF - 0x20));
            if (num == 1)
            {
                Word4 = Word4 + 0x20;
            }
        } //End Sub
        public void set_w4_unknown1(ref int num)
        {
            
Word4 = (Word4 & (0xFFFF - 0x40));
            if (num == 1)
            {
                Word4 = Word4 + 0x40;
            }
        } //End Sub
        public void set_w4_unknown2(ref int num)
        {
            
Word4 = (Word4 & (0xFFFF - 0x80));
            if (num == 1)
            {
                Word4 = Word4 + 0x80;
            }
        } //End Sub
        public void set_w4_unknown3(ref int num)
        {
            
Word4 = (Word4 & (0xFFFF - 0x100));
            if (num == 1)
            {
                Word4 = Word4 + 0x100;
            }
        } //End Sub
        public void set_w4_mirroredattack(ref int num)
        {
            
Word4 = (Word4 & (0xFFFF - 0x200));
            if (num == 1)
            {
                Word4 = Word4 + 0x200;
            }
        } //End Sub
        public void set_w4_dontmirrorwhileattacking(ref int num)
        {
            
Word4 = (Word4 & (0xFFFF - 0x400));
            if (num == 1)
            {
                Word4 = Word4 + 0x400;
            }
        } //End Sub
        public void set_w4_unknown4(ref int num)
        {
            
Word4 = (Word4 & (0xFFFF - 0x800));
            if (num == 1)
            {
                Word4 = Word4 + 0x800;
            }
        } //End Sub
        public void set_w4_xshiftgroup(ref int num)
        {
            
Word4 = (Word4 & (0xFFFF - 0x3000));
            Word4 = Word4 + (num * 0x1000);
        } //End Sub
        public void set_w4_yshiftgroup(ref int num)
        {
            
Word4 = (Word4 & (0xFFFF - 0xC000));
            Word4 = Word4 + (num * 0x4000);
        } //End Sub
        /*    'byte7: 255 - self = ticks per move (lower the faster)*/
        /*    'byte8: minimum time to do something?*/
        /*    'byte9: resistance to projectiles?*/
        /*    'byte10: base HP*/
        /*    'byte11: quickness of monster (hit prob)*/
        /*    'byte12: poisonousness of attack*/
        /*    'byte13: power of spell casted and attack*/
        /*    'byte14: unused*/
        /*    'word14:*/
        /*    '0x000F: how far monster can see, reduced by 1/2 for each darkness level*/
        /*    '0x0F00: turn towards player if this close?*/
        /*    '0xF000: compared to distance of prey?*/
        /*    '0xE000: 2-15, casts spells... something to do w/ timing as well?*/
        public byte w14_sightrange()
        {
            return App.CByte(word14 & 0xF);
        } //End function
        public byte w14_unused1()
        {
            return App.CByte((word14 & 0xF0) / 0x10);
        } //End function
        public byte w14_awarenessrange()
        {
            return App.CByte((word14 & 0xF00) / 0x100);
        } //End function
        public byte w14_spellrange()
        {
            return App.CByte((word14 & 0xF000) / 0x1000);
        } //End function
        public void set_w14_sightrange(ref int num)
        {
            
            if (num > 15)
            {
                num = 15;
            }
            if (num < 0)
            {
                num = 0;
            }
            word14 = App.CShort(word14 & 0xFFF0) + (num * 0x1);
        } //End Sub
        public void set_w14_unused1(ref int num)
        {
            if (num > 15)
            {
                num = 15;
            }
            if (num < 0)
            {
                num = 0;
            }
            word14 = App.CShort(word14 & 0xFF0F) + (num * 0x10);
        } //End Sub
        public void set_w14_awarenessrange(ref int num)
        {
            if (num > 15)
            {
                num = 15;
            }
            if (num < 0)
            {
                num = 0;
            }
            word14 = App.CShort(word14 & 0xF0FF) + (num * 0x100);
        } //End Sub
        public void set_w14_spellrange(ref int num)
        {
         
            if (num > 15)
            {
                num = 15;
            }
            if (num < 0)
            {
                num = 0;
            }
            num = num * 0x1000;
            word14 = App.CShort(word14 & 0xFFF) + num;
        } //End Sub
        /*    'word16:*/
        /*    '0x000F: unused*/
        /*    '0x00F0: bravery, how immune to warcry/calm etc*/
        /*    '0x0F00: amount of skill characters gain when damaged/damage monster*/
        /*    '0xF000: <10 ? then it gets teleported, otherwise not.*/
        public byte w16_unused1()
        {
            return App.CByte((word16 & 0xF) / 0x1);
        } //End function
        public byte w16_bravery()
        {
            return App.CByte((word16 & 0xF0) / 0x10);
        } //End function
        public byte w16_skill()
        {
            return App.CByte((word16 & 0xF00) / 0x100);
        } //End function
        public byte w16_teleportation()
        {
            return App.CByte((word16 & 0xF000) / 0x1000);
        } //End function
        public void set_w16_unused1(ref int num)
        {

            if (num > 15)
            {
                num = 15;
            }
            if (num < 0)
            {
                num = 0;
            }
            word16 = App.CShort(word16 & 0xFFF0) + (num * 0x1);
        } //End Sub
        public void set_w16_bravery(ref int num)
        {
            if (num > 15)
            {
                num = 15;
            }
            if (num < 0)
            {
                num = 0;
            }
            word16 = App.CShort(word16 & 0xFF0F) + (num * 0x10);
        } //End Sub
        public void set_w16_skill(ref int num)
        {
            if (num > 15)
            {
                num = 15;
            }
            if (num < 0)
            {
                num = 0;
            }
            word16 = App.CShort(word16 & 0xF0FF) + (num * 0x100);
        } //End Sub
        public void set_w16_teleportation(ref int num)
        {
            if (num > 15)
            {
                num = 15;
            }
            if (num < 0)
            {
                num = 0;
            }
            num = num * 0x1000;
            word16 = App.CShort(word16 & 0xFFF) + num;
        } //End Sub
        /*    'word18:*/
        /*    '0x000F: unused*/
        /*    '0x00F0: resistance to magic (15 = immune)*/
        /*    '0x0F00: resistance to poison (15 = immune)*/
        /*    '0xF000: unused*/
        public byte w18_unused1()
        {
            return App.CByte((word18 & 0xF) / 0x1);
        } //End function
        public byte w18_magicresistance()
        {
            return App.CByte((word18 & 0xF0) / 0x10);
        } //End function
        public byte w18_poisonresistance()
        {
            return App.CByte((word18 & 0xF00) / 0x100);
        } //End function
        public byte w18_unused2()
        {
            return App.CByte((word18 & 0xF000) / 0x1000);
        } //End function
        public void set_w18_unused1(ref int num)
        {
            if (num > 15)
            {
                num = 15;
            }
            if (num < 0)
            {
                num = 0;
            }
            word18 = App.CShort(word18 & 0xFFF0) + (num * 0x1);
        } //End Sub
        public void set_w18_magicresistance(ref int num)
        {
            if (num > 15)
            {
                num = 15;
            }
            if (num < 0)
            {
                num = 0;
            }
            word18 = App.CShort(word18 & 0xFF0F) + (num * 0x10);
        } //End Sub
        public void set_w18_poisonresistance(ref int num)
        {
            if (num > 15)
            {
                num = 15;
            }
            if (num < 0)
            {
                num = 0;
            }
            word18 = App.CShort(word18 & 0xF0FF) + (num * 0x100);
        } //End Sub
        public void set_w18_unused2(ref int num)
        {
            if (num > 15)
            {
                num = 15;
            }
            if (num < 0)
            {
                num = 0;
            }
            num = num * 0x1000;
            word18 = App.CShort(word18 & 0xFFF) + num;
        } //End Sub
        /*    'word20:*/
        /*    '0x000F: time to show attack graphic*/
        /*    '0x00F0: speed of movment when fleeing, time between status update*/
        /*    '0x0F00: time between status update while attacking*/
        public byte w20_attacklength()
        {
            return App.CByte((word20 & 0xF) / 0x1);
        } //End function
        public byte w20_statusupdate()
        {
            return App.CByte((word20 & 0xF0) / 0x10);
        } //End function
        public byte w20_attackstatusupdate()
        {
            return App.CByte((word20 & 0xF00) / 0x100);
        } //End function
        public byte w20_unused1()
        {
            return App.CByte((word20 & 0xF000) / 0x1000);
        } //End function
        public void set_w20_attacklength(ref int num)
        {
            if (num > 15)
            {
                num = 15;
            }
            if (num < 0)
            {
                num = 0;
            }
            word20 = App.CShort(word20 & 0xFFF0) + (num * 0x1);
        } //End Sub
        public void set_w20_statusupdate(ref int num)
        {
            if (num > 15)
            {
                num = 15;
            }
            if (num < 0)
            {
                num = 0;
            }
            word20 = App.CShort(word20 & 0xFF0F) + (num * 0x10);
        } //End Sub
        public void set_w20_attackstatusupdate(ref int num)
        {
            if (num > 15)
            {
                num = 15;
            }
            if (num < 0)
            {
                num = 0;
            }
            word20 = App.CShort(word20 & 0xF0FF) + (num * 0x100);
        } //End Sub
        public void set_w20_unused1(ref int num)
        {
            if (num > 15)
            {
                num = 15;
            }
            if (num < 0)
            {
                num = 0;
            }
            num = num * 0x1000;
            word20 = App.CShort(word20 & 0xFFF) + num;
        } //End Sub
        /*    'word22: Hit location mask #'s (4 of them)*/
        /*    '  Basically, its kind of random which NIBBLE it uses*/
        /*    '  It looks up the chart of "hit location masks" with a somewhat*/
        /*    '  random nibble.  That chart shows what items actually help when*/
        /*    '  it comes to armor....  I don't understand the use, its very*/
        /*    '  complicated...  I think it has something to do with certain monsters*/
        /*    '  having a better chance of "injuring" your arms, feet, chest, etc*/
        /*    'byte25: something to do with damage (0 = no damage)*/
        /*    'if 1, affectd by anti-fire*/
        /*    'if 2, halves the effects of armor*/
        /*    'if 4, has x8000 flag (deals piercing damage?),*/
        /*    'if 6, = (115-wisdom)/64 * the damage*/
        /*    'if 5, affected by anti-magic*/
        /*    'byte26: unused*/
        /*    'To sum it up, this is a complete pile of shit thrown together*/
    } //End Class
}
