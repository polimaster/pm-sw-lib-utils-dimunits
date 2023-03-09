using System;
using System.Collections.Generic;

namespace Polimaster.Utils.DimUnits {
    public static class TexasConverter {

        public static DateTime BYTE_TO_TIME(byte x1, byte x2, byte x3){
            var int64LiValue = 630507456000000000 + ((long)x1 + 256 * x2 + 256 * 256 * x3) * 10000000 * 60;
            return new DateTime(int64LiValue);
        }

        /**/public static DateTime BYTE_TO_TIME(byte x1, byte x2, byte x3, byte x4) 
        {
            long date = ((x4 * 256 + x3) * 256 + x2) * 256 + x1;
            return new DateTime(1999, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMinutes(date);
        }

        /*
          public static DateTime BYTE_TO_TIME(byte x1, byte x2, byte x3, byte x4)// 630507456000000000
        {
            //var int64LiValue = 125596224000000000+((long)x1 + 256 * x2 + 256 * 256 * x3 + 256 * 256 * 256 * x4) * 10000000;
            //return new DateTime(int64LiValue);
            long date = (( x4 * 256 +  x3) * 256 +  x2) * 256 + x1;
            return new DateTime(1999, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMinutes(date);

        }

         */

        public static byte[] TIME_TO_4BYTE(DateTime dt)
        {
            var date = (long)(dt - new DateTime(1999, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
          var x1 = (byte)date;
          var x2 = (byte)(date >> 8);
          var x3 = (byte)(date >> 16);
          var x4 = (byte)(date >> 24);
          return new[] {x4, x3, x2, x1 };
        }

        public static byte[] TIME_TO_BYTE(DateTime dt) {
            var ticks = (dt.Ticks - 630507456000000000) / 60 / 10000000;
            var x1 = (byte)ticks;
            var x2 = (byte)(ticks >> 8);
            var x3 = (byte)(ticks >> 16);
            return new[]{x3, x2, x1};
        }

        public static double BYTE_TO_DOUBLE(IList<byte> buff, int beg) {
            var value = new[] { buff[beg + 1], buff[beg], buff[beg + 3], buff[beg + 2] };
            if (value[0] == 0 && value[1] == 0 && value[2] == 0 && value[3] == 0) return 0;
            var mant = ((value[1] & 0x7f) + (double)value[2] / 0x100 + (double)value[3] / 0x10000) / 0x80 + 0x1;
            if (value[0] != 0) mant *= Math.Pow(2, value[0] - 0x80);
            if (value[1] > 0x80) mant = -mant;
            return Math.Round(mant, 14);
        }

        public static void DOUBLE_TO_BYTE(double d, IList<byte> buff, int beg = 4){
            //            BOOL PMPagerFunc::FlToArB(double Value, BYTE *ArB)
            byte i, zn1, zn2;
            var x = new byte[beg];

            var numValue = d;
            if(numValue == 0) return; // fabs (Num_Value)
            if(numValue < 0){
                numValue *= -1.0;
                zn1 = 0x80;
            } else zn1 = 0;
            if(numValue > 1) zn2 = 0;
            else zn2 = 1;
            byte k1 = 0x80;
            double k2 = 1;
            byte step = 0;
            byte pos1 = 2;
            byte posb = 1;
            while(numValue >= 2){
                step++;
                numValue = numValue/2;
            }
            while(numValue < 1){
                step++;
                numValue = numValue*2;
            }
            numValue = numValue - 1;
            for(i = 1; i <= 24; i++){
                if(numValue > k2){
                    x[pos1 - 1] = (byte) (x[pos1 - 1] + k1);
                    numValue = numValue - k2;
                }
                if(numValue == k2){
                    x[pos1 - 1] = (byte) (x[pos1 - 1] + k1);
                    goto lab1;
                }
                k1 = (byte) (k1/2);
                k2 = k2/2;
                posb++;
                if(posb != 9) continue;
                posb = 1;
                pos1++;
                k1 = 0x80;
            }
            lab1:
            if(zn2 == 1)
                x[0] = (byte) (0x80 - step);
            else x[0] = (byte) (0x80 | step);
            x[1] = (byte) (x[1] | zn1);
            buff[0] = x[1];
            buff[1] = x[0];
            buff[2] = x[3];
            buff[3] = x[2];
        }
    }
}
