using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace Quanlyphuongtien
{
    public static class Utility
    {
        public static void SetEffect(BindableObject obj, string colorEffect = null, Command command = null)
        {
            if (colorEffect == null)
                colorEffect = Contanst.PrimaryColor;
            XamEffects.TouchEffect.SetColor(obj, Color.FromHex(colorEffect));
            XamEffects.EffectsConfig.SetChildrenInputTransparent(obj, true);
            if (command != null)
            {
                XamEffects.Commands.SetTap(obj, command);
            }
        }
        public static string NumberToMoney(string str)
        {
            string moneyReversed = "";

            string strNumber = str;

            int processedCount = 0;

            for (int i = (strNumber.Length - 1); i >= 0; i--)
            {
                moneyReversed += strNumber[i];

                processedCount += 1;

                if ((processedCount % 3) == 0 && processedCount < strNumber.Length)
                {
                    moneyReversed += ",";
                }
            }

            string money = "";

            for (int i = (moneyReversed.Length - 1); i >= 0; i--)
            {
                money += moneyReversed[i];
            }

            return money;
        }
        public static void MarkerRotation(Pin p, double deg, ushort miniSecond)
        {
            var anchor = p.Anchor;
            var degP = p.Rotation % 360;
            if (p.Rotation > 360)
                p.Rotation = degP;
            var degE = deg % 360;
            if (deg > 360)
                deg = degE;

            var degRemain = Math.Abs(deg - p.Rotation);
            if (degRemain <= 0)
                return;
            double timePerTick = miniSecond / degRemain;
            Device.StartTimer(TimeSpan.FromMilliseconds(timePerTick), () =>
            {
                if (degP > deg)
                {
                    p.Rotation = p.Rotation -= 1;
                    if (p.Rotation <= deg)
                        return false;
                }
                else
                {
                    p.Rotation = p.Rotation += 1;
                    if (p.Rotation >= deg)
                        return false;
                }
                return true;

            });
        }
        public static double CalculationAngle(double sLat, double sLng, double eLat, double eLng)
        {
            if (eLng == sLng)
                return -1;
            var rs = Math.Atan((eLat - sLat) / (eLng - sLng)) * 180 / 3.14;
            return (eLng < sLng ? 180 + (90 - rs) : 90 - rs);
        }

       
    }
}
