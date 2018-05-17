using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace Quanlyphuongtien
{
    public class VehicleOnline
    {
        public uint Id { get; set; }
        public string IMEI { get; set; }
        public string Plate { get; set; }
        public string DateUpdate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Direction { get; set; }
        public string Speed { get; set; }
        public ushort State { get; set; }
        public string Address { get; set; }
        public float TotalKM { get; set; }
        public string TotalPause { get; set; }
        public string Acc { get; set; }
        public string isKHC { get; set; }
        public string TotalTimeRun { get; set; }
        public string TotalTimePauseOn { get; set; }
        public string ActiveState { get; set; }

        internal static string GetStateColor(VehicleOnloineState state)
        {
            switch (state)
            {
                case VehicleOnloineState.Running:
                    return Contanst.PrimaryTextColor;
                case VehicleOnloineState.PauseOn:
                    return "#38ca38";
                case VehicleOnloineState.PauseOff:
                    return "#000000";
                case VehicleOnloineState.LostConnection:
                    return "#d40000";
                case VehicleOnloineState.OverSpeed:
                    return "#a09000";
                case VehicleOnloineState.LostGPS:
                    return "#d40000";
                default:
                    return "#d40000";
            }
        }
        internal static BitmapDescriptor GetStateIcon(VehicleOnloineState state)
        {
            string imgBase64 = "";
            switch (state)
            {
                case VehicleOnloineState.Running:
                    imgBase64 = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAASHSURBVFhH7ZZrTJtlFMdfJqUtt7ctbbm1g7aMlkILtJRBL3Tcwq1TKmSEbLiI6z6gsbJpZG6TDwaQqLuERJ3OCDgTaxZZcCO4GGSDaRbIjAlEtyW4yeaIbEK5I8LfA/viBz++/WCyNzl5n/f58JzfOe/5n/MwzJPn/5aBnp4eUWtre82Bg02f7MqvHjIa879L1lo6ZDLZs16vVxTQeJqOHXMlJtZO8XgtCAr6ECEhXRAIPifrhFBwAtu31814G199LiAQQ0NDYo2m6VeG6YeIHYNQeB0C/gg5HgGffw1i0RiCmAHIZYenfb5+CecQtvwP0nm8cxtKxR3s2PEAEvENxMXdgiL+Nlj2OtSaSSQkTGJb0GfwertyOQdgmI8yIsIvw5K1gvR0P2Jjf4bBMAODcZbW40hNnUZ29hoiIy8jOfVTG+cAbnevRS67iqJCwGxeQGLiHeQ5gLw8QKO5C6PxEYqLQFm5Brm8g/sMXPxmZGds7ABKSwnANIfk5PsoLcHWd6p+igAeorwcUCqHERV13MJ5BtLS9uvk0f1rm06yLQswpP2JykpsmcnkhylzFhUVQHz8VTBMjoFzAJatVkmlFxcdjh8o4hYoFFX07x2IibGR00rodG/CbrtCe/1kLh3nAB5Pp0Yq6VgOC5NQhMx/Gp8voCJ8C80tPj3nAPn5RxNE7PF5ljVtOefxtpHxERzM31pv7kVEJNK6YV2p9Gg4BzAaT2eyrGdZIimGWHyIGk83FVsvoiRf0/ocRKLXIZWWEcgzONhwPo9zAIOhTRwRXn1PKNxJ3W4cavU0MjNmqfgWkJQ0RdK7jdDQEsqAY16tPiznHIBh9oUJBO6J0NAkakATsNtXSYLzKCtbgcMxT81pCmFhGTQX7LN6fSP3rZhhmkOEwqqbQqECGRkTcDpXyLmfpLeEXc45ZJqmaD5oyWzTVutrEQHIABPEsnk/CfhK6gG3YLMuo7DQj6KiJVhz/bT3G01HLdVF+oPGxveEgQBgVCr3DaFQA33KOLLMq7Ba57ZA0o0zSNL8ghBeCuIU9skzZ0Z5nAM4nftF4qgX7oWGGqn4biInBwSxRF1wFVqtn6bij5R+E9WBe+HIkY+jOQdgmFqdSPTOmkj0NEpKRrF3799wuVZQULBMNeGnbthHfcCF8PCjePfUV3bOAXSGTrNC0Ut6P4mS0gZ4PC+jrs5LSthHqnAiOjqdANqpDZ/HqY5vizkHeKO5L0elGqQxPEbpr0FVVQUNIhets6BSxVHxaSgLg1CrrqCt7VIB5wBxcce0sbG9f5nNc6T757F7dx69zXQRSSQoCUVOtZH5Ow2pPrjd7SbOAXy+7yVq9Rf3sy0bNPdHsWfPMMmwmxrSWVLDJaSlDiA3Zx0yWZff5WqWcg6weWCM/MSXBsMicnNB2n9sm2rItmxeUjZIngtI0b8/HBDnjw9tTmMj++4a0h6S9hdJhmt0PVtHVtYq3REeIfipC3+89IqP+///74jq6y9EtLYPltcfGD5UUztyMibmbGtkZPeLDNNeyDBvswGM/snRgcnAP1WFENWmdTwDAAAAAElFTkSuQmCC";
                    break;
                case VehicleOnloineState.PauseOn:
                    imgBase64 = "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";
                    break;
                case VehicleOnloineState.PauseOff:
                    imgBase64 = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAQSSURBVFhH7VbdS9tXGI4f+JFETfxI1Kgxxq8kJho/YmKiiU0CMQluKnHza7TTYUeNRZnOjmLwYheloBu6YkAL6YUsgt83k1op7GpXYzeD3ZVBN6lCWRlu6Oyz8x5o2R/wy8WgPzj8Tn4nnOc573ne531FonfP/y0C0WhUfHtmLvTx2PjDxsbGxw0NDU8+uj669snNWx/6fL7shJ7n/b4PunR6w28pKSlITk5GRkYGHzSnb0ql8sVnn9+9nhASkUhUnJ2V/XOSSIScnBxkZmZycHqnp6dDJpMhKSkJ+fn5Z/H4d7mCk3A4HKa0tLSr0tJSVFVVQS6Xo7i4GCqVihOqqKiAWq0GA0Y0GnMKToBt2CCVStHc3Iz6+noUFRXBaDTCZDLxucFggMVigUQsxnrsW4/gBMbHpywFBQXwuN1oampCeXk5Ojo6+NBqtZyIx+MB/eerlTW34AQeP/3BRidlSgdTP6qrq/mchl6v5wT8fj9KSkrg8fmsghPw+P06pvJ/vOyUdXV1qKysRFdX11sCtbW18Hq9KCwsRCRyr1lwAlVVxgqFQvHX4OAgwuEw5ufnsbS0hMXFRQYY4d+Gh4e5MO9EvqwTnADbsEwqlbyKxWLY39/H3t7e2/fu7i52dnawsbEBpUKJmZm7NYITsHV2qlm6/UEnPz4+BoFubm4iHo9je3sbh4eHPBoSiYSlaqVWcAJTs19YdDrd3xTmYDAIunPKexpvBNnf38+0oYXV4bILTuDBo0eKa27Pi+7ubqSmpqKsrIz7AHkCkSA3tFqtsNsd5z1DQ0WCExgaCmdbrbZfx8ZGeQYwZ+QZQJnQ3t6Ompoa9PX1UYq+ZOTkghNgG6axK/hlYmKCh9/pdHLwQCAAl8sFtoaBgQG0tLSctrW1ZSWCQBIL90+Tk5O8FjAQuJkrkvvRnL5RilpttufMmNISQUDk6rz248jICC88ZMcETIN0QEUqGAywCFiehUKhFMEJzM3NyfV6w3PKAFI9CzXMZjNYQ8I1QdXRbrdTXfhzdHRUKTiBm+FpMys0r8nre3t7wUDQ09PDhUjAZM9UiPLy8pgbqs2CExCLxWaq/ZRuR0dHuLi4wNnZGU5PT3FycoKDgwO+RunJwIUvRsbGxiaNRkNtF3fB/z7n5+fcDXNzc/n1JITA1w/WKAKvKd3Igt88V1dXuLy8xNbWFk9PEmMoNNgk+BUwB8xi/d4z6nrIjpeXl7G+vo7V1VUsLCxwLdAa08GrGzfCBYIToA1lObI4iY2ASP1kxfRbz9oxk9HEzUhVrPo+IeC06cTUrEWj0f6u09XyDsjS2so9gDyBwFmEXt5f/CaQMAK08crKinR69o7301u3p/02+z1rS+t91qqF2ZLb6XxPllDwd5snIgL/Auoa8iU3MtDNAAAAAElFTkSuQmCC";
                    break;
                case VehicleOnloineState.LostConnection:
                    imgBase64 = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAASHSURBVFhH7ZZrTJtlFMc7B4WGS1vKWpCLdNAOSltgHZcWBJwUyq0XYOM6yqB0XMtNRJhL3RQlQZeZzTkjZssWk80tGCXDmKEOISFBR/ST4YMzDslSJZsUBi2V/T2d8ZsfXz6Y7E1O3ifvm5znd85zzv85LNbT5/+Wgbq+voCu/leNrWbLBVP2CzO5SuXXGXLlOZlCUcESCIJ2NB5xfLzWKBLdOxMQgKv+/vicz8dkaCjGeTy8HxwMA4/naLJ21O0IxJXJyeDuyKjFn2jTLbkCjpgYrEqkcEr3YSkqCm6FAneFQvSGCBwXL37KYxzik7ffVU1wuYAmE49zc+GQSgG1mkyD32Lj4M7MBJ7PxkRQIM73vJzBOMBVnS51PiICKCnBVl4eHLJEoKDgid1PTMQ6bQ69Ht9FR+OStih7BwBKUu94o66qwqY2H47kZKCsDDCV4XeVCs5sAqiswg8JCXhvX6KGcYBzmpzkObkcqK/HI60WK2lpQE0NUF2NBxoN/vRm4MgRfE+1MJKTx/wRDPYNyr9VKLbR3Ax3aSk2tHmAxQI0NmKTjsEL5V3PK5OQFRGhZDwDdku79HZS8tZcRQWGqeAOSSTIourPjIyEae9e2Ckj01QDt6keTr31joJxgIGk9JgLQuG6IDAQ5Pw/zd/PD8MCAXpTs6SMAxwvrRCf4Iesq6gVvQC7fHzAJvPbvRvP0Nv7LSYoCC1s9nZTrCyOcYCPbX2qoxzOZn5ICE5RO07FxmIhPv6JfUXrEfpWFLoHBoIZa7Qy34ZdXC5P7+u7pCEZXtuvAnJy4KLi8+h02KK1h1qxhDKgZvuutYhEQsYz0MdiBRj9/e9KCeCPoiJ4yivgrKyEi3ThkdGIjcJCqKg+Mjgcp72kOpRxADuLxdZzOIvRHA7u04buulqsmhuw0XAUa4cPw0kdIKN/muDglQGrlcs4ADncRc5/fI4qfelQBdykAaukCRvNVqwRzEOjATL6J+Pzlzt1Or+dAGAZxOI7UtrkV1K/vzo74WxthautHZskQCsmE+RsNtKEont2u92HcYDWmlb+sWcjlvdz/PEzyTEGBrDZ3Q1PTw+2CWSZjiWV4Exc7vqZ4WER4wC1YWGy0+HhnnKaBxbq6uAZGoKrvx8uAnDRccwffBF6KsLXaCb4sGcojXGAa+0vqSfiJDhLE5CNWs9G528z16O2uBg5KSlICQvDKInUdZLly8dfP8g4wBdXrudOk84v0nVbS4NIcXk5SijtB9LTEU7TURwJ1IxYjGm6DT9q7sxhHGAkryjhM4nEs0EDSXNWFrIpchVdSjGkhCFR0UgWieDIz8dNurJ71eoUxgH0LFbQB6Ghv6CjAwsNZsy2teEy9f+YwYCb9J4hMFiP4ZJEunbePsq8EnojenOP8Jrb0gRQAWJwEHjFawP/GBWkh/6NxifMMh79vw7Hz46ppw6krTwgHXBREW7bOvG4uwvulhY8pCxMxcat3Dg5UrBjAF7Ht8ZvCb488UbhbJutZ85iPX1DqRyZ1BV3T7V0FH5jNjM/ju9oNE+dUwb+BsCDO4wj/0DsAAAAAElFTkSuQmCC";
                    break;
                case VehicleOnloineState.OverSpeed:
                    imgBase64 = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAASKSURBVFhH7ZZ7TNNXFMc7eZV2tFSEUR6d5eVAKG0p41Wo8sh4KTjBzJTNqJkJjkUHbpKMrAvDAhE3hi860EDRuHYjY6MREB1xNTjCRlzGonM6HgPBIe83TL47bfxzf/74Y4m/5Jt7c29y7uec+7vnHBbr+fd/i0BtbZOTpvjTrKP579Sq1ekP4uPl3artkZW5eR+8npNTwF1Xf/Lf/0i9d6/3QGmpPc6ffwEXLthCr3ek0R5arS3Uas/hjz85eXBdIK5e/ZF37NjLQ9dvsNDby0NXFxvd3Q4kNjo7HdD7Kx8dHSwcOeI+evmyScA4RFNTrcJo2IDBQW88euSPnh4B/rjvQfIkGGda98XQkAiXGlgUkTIl4wD19RzZ9x1OWFkJx+SkBHfvCmkMxtRUCEVEiLGxYKzSXns7F6bWmmTGAb44w5J2dbkDSMTMjAz9/WKax5Li0NfnQzCh1r3OTlcYGj+MZxzg3LlkWWenBx2SQl7L6Bq20DzZqpGRQIyPWwBSYDYLodPlRzMOcLamRvKD2RKBVMzOKsjjEJpnkjKsQNPTYda9mzfdUVT8roxxAG1FYUD7dU/cvh2LEyeCsGePF7Zt87AqK8sLxcWv4NYtJVpbhaisPCthHKC0NGLzmSrnZT6fCzL+n+Jw2JQPnNHc1hbMOMDx42GioiLegkTCf3a4LWxs7KxisWyta2KxE/LyHFb27+f7MA7wZeNR+b59jv8kJQmQm+sMXbUABsNGq3Q6AQ4f5iMpyQW7d9usnfo8JpJxAI2G77xzp/3fSiUH9+654skTH/rxpJibk+LxY1/8ft8NiYlsEm/hypUKb8YBCgpY3B07HIYDA7kYGAjB0pKSDk/G4mIK5uaVlAUlCAnhQKXijmu1mS6MA2g0Knbmrk2DYjEbfw3JsLCgooSUivn5NHqWKgwPS+HnxyYA3qhGo+YxDmA0ZtuoVMIHYrE9/uyTEICSABIJwJIZoykbBsPX1xbh4Z7Der1+fcpyQoLTL4GBL9J9B2FxSYGZ2WgCicbkVCh6f/NDQIAdYuNE/Uaj0Z7xCBgMBtcDB9wnpKEcPHwoo6wXQfcfhuVlOaZntuCnnz0RGuqIzEzHiUOHVJsYBzB+c0laWiZARgYfd+6kYHX1DfLe8g9sxwQVomaTB1JTnVBUxEdJ+ZsKxgHq6iLCvv3OCydPOtObT6PG421SDnVByYiNlUAufwnl5Tx83SiydEdRjANcvBgpN5t9qRERITs7BunpWaRdUCgiIRSK4e/vQoXIi7oiH2pIslSMAxQXywJbWj0xMSEjr+MQF5cOmSwWIlEQBILNdP/uGB2Vw2TyxlsH7ZgvRmo1i9fQwBsBoqjpSMO1a9moro5HVVUMjF9FoqVlK9bWIlFX7zal0eS4MR4Bi0GtdoNpdVVBLyDmmaJpjCK9iqdP5VhYlKCkxOnGuhxuMVpZGaNsa+OPTFENWF6JoJcQRflATnMFxie2ornZYayiQpawbgAWwzodi3/6NOc1vX7je2az16meHq/P6uu5+WVlNimFhSzm2/F19ea5cYrAvwLfmmz8UMulAAAAAElFTkSuQmCC";
                    break;
                case VehicleOnloineState.LostGPS:
                    imgBase64 = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAASHSURBVFhH7ZZrTJtlFMc7B4WGS1vKWpCLdNAOSltgHZcWBJwUyq0XYOM6yqB0XMtNRJhL3RQlQZeZzTkjZssWk80tGCXDmKEOISFBR/ST4YMzDslSJZsUBi2V/T2d8ZsfXz6Y7E1O3ifvm5znd85zzv85LNbT5/+Wgbq+voCu/leNrWbLBVP2CzO5SuXXGXLlOZlCUcESCIJ2NB5xfLzWKBLdOxMQgKv+/vicz8dkaCjGeTy8HxwMA4/naLJ21O0IxJXJyeDuyKjFn2jTLbkCjpgYrEqkcEr3YSkqCm6FAneFQvSGCBwXL37KYxzik7ffVU1wuYAmE49zc+GQSgG1mkyD32Lj4M7MBJ7PxkRQIM73vJzBOMBVnS51PiICKCnBVl4eHLJEoKDgid1PTMQ6bQ69Ht9FR+OStih7BwBKUu94o66qwqY2H47kZKCsDDCV4XeVCs5sAqiswg8JCXhvX6KGcYBzmpzkObkcqK/HI60WK2lpQE0NUF2NBxoN/vRm4MgRfE+1MJKTx/wRDPYNyr9VKLbR3Ax3aSk2tHmAxQI0NmKTjsEL5V3PK5OQFRGhZDwDdku79HZS8tZcRQWGqeAOSSTIourPjIyEae9e2Ckj01QDt6keTr31joJxgIGk9JgLQuG6IDAQ5Pw/zd/PD8MCAXpTs6SMAxwvrRCf4Iesq6gVvQC7fHzAJvPbvRvP0Nv7LSYoCC1s9nZTrCyOcYCPbX2qoxzOZn5ICE5RO07FxmIhPv6JfUXrEfpWFLoHBoIZa7Qy34ZdXC5P7+u7pCEZXtuvAnJy4KLi8+h02KK1h1qxhDKgZvuutYhEQsYz0MdiBRj9/e9KCeCPoiJ4yivgrKyEi3ThkdGIjcJCqKg+Mjgcp72kOpRxADuLxdZzOIvRHA7u04buulqsmhuw0XAUa4cPw0kdIKN/muDglQGrlcs4ADncRc5/fI4qfelQBdykAaukCRvNVqwRzEOjATL6J+Pzlzt1Or+dAGAZxOI7UtrkV1K/vzo74WxthautHZskQCsmE+RsNtKEont2u92HcYDWmlb+sWcjlvdz/PEzyTEGBrDZ3Q1PTw+2CWSZjiWV4Exc7vqZ4WER4wC1YWGy0+HhnnKaBxbq6uAZGoKrvx8uAnDRccwffBF6KsLXaCb4sGcojXGAa+0vqSfiJDhLE5CNWs9G528z16O2uBg5KSlICQvDKInUdZLly8dfP8g4wBdXrudOk84v0nVbS4NIcXk5SijtB9LTEU7TURwJ1IxYjGm6DT9q7sxhHGAkryjhM4nEs0EDSXNWFrIpchVdSjGkhCFR0UgWieDIz8dNurJ71eoUxgH0LFbQB6Ghv6CjAwsNZsy2teEy9f+YwYCb9J4hMFiP4ZJEunbePsq8EnojenOP8Jrb0gRQAWJwEHjFawP/GBWkh/6NxifMMh79vw7Hz46ppw6krTwgHXBREW7bOvG4uwvulhY8pCxMxcat3Dg5UrBjAF7Ht8ZvCb488UbhbJutZ85iPX1DqRyZ1BV3T7V0FH5jNjM/ju9oNE+dUwb+BsCDO4wj/0DsAAAAAElFTkSuQmCC";
                    break;
                default:
                    imgBase64 = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAASHSURBVFhH7ZZrTJtlFMc7B4WGS1vKWpCLdNAOSltgHZcWBJwUyq0XYOM6yqB0XMtNRJhL3RQlQZeZzTkjZssWk80tGCXDmKEOISFBR/ST4YMzDslSJZsUBi2V/T2d8ZsfXz6Y7E1O3ifvm5znd85zzv85LNbT5/+Wgbq+voCu/leNrWbLBVP2CzO5SuXXGXLlOZlCUcESCIJ2NB5xfLzWKBLdOxMQgKv+/vicz8dkaCjGeTy8HxwMA4/naLJ21O0IxJXJyeDuyKjFn2jTLbkCjpgYrEqkcEr3YSkqCm6FAneFQvSGCBwXL37KYxzik7ffVU1wuYAmE49zc+GQSgG1mkyD32Lj4M7MBJ7PxkRQIM73vJzBOMBVnS51PiICKCnBVl4eHLJEoKDgid1PTMQ6bQ69Ht9FR+OStih7BwBKUu94o66qwqY2H47kZKCsDDCV4XeVCs5sAqiswg8JCXhvX6KGcYBzmpzkObkcqK/HI60WK2lpQE0NUF2NBxoN/vRm4MgRfE+1MJKTx/wRDPYNyr9VKLbR3Ax3aSk2tHmAxQI0NmKTjsEL5V3PK5OQFRGhZDwDdku79HZS8tZcRQWGqeAOSSTIourPjIyEae9e2Ckj01QDt6keTr31joJxgIGk9JgLQuG6IDAQ5Pw/zd/PD8MCAXpTs6SMAxwvrRCf4Iesq6gVvQC7fHzAJvPbvRvP0Nv7LSYoCC1s9nZTrCyOcYCPbX2qoxzOZn5ICE5RO07FxmIhPv6JfUXrEfpWFLoHBoIZa7Qy34ZdXC5P7+u7pCEZXtuvAnJy4KLi8+h02KK1h1qxhDKgZvuutYhEQsYz0MdiBRj9/e9KCeCPoiJ4yivgrKyEi3ThkdGIjcJCqKg+Mjgcp72kOpRxADuLxdZzOIvRHA7u04buulqsmhuw0XAUa4cPw0kdIKN/muDglQGrlcs4ADncRc5/fI4qfelQBdykAaukCRvNVqwRzEOjATL6J+Pzlzt1Or+dAGAZxOI7UtrkV1K/vzo74WxthautHZskQCsmE+RsNtKEont2u92HcYDWmlb+sWcjlvdz/PEzyTEGBrDZ3Q1PTw+2CWSZjiWV4Exc7vqZ4WER4wC1YWGy0+HhnnKaBxbq6uAZGoKrvx8uAnDRccwffBF6KsLXaCb4sGcojXGAa+0vqSfiJDhLE5CNWs9G528z16O2uBg5KSlICQvDKInUdZLly8dfP8g4wBdXrudOk84v0nVbS4NIcXk5SijtB9LTEU7TURwJ1IxYjGm6DT9q7sxhHGAkryjhM4nEs0EDSXNWFrIpchVdSjGkhCFR0UgWieDIz8dNurJ71eoUxgH0LFbQB6Ghv6CjAwsNZsy2teEy9f+YwYCb9J4hMFiP4ZJEunbePsq8EnojenOP8Jrb0gRQAWJwEHjFawP/GBWkh/6NxifMMh79vw7Hz46ppw6krTwgHXBREW7bOvG4uwvulhY8pCxMxcat3Dg5UrBjAF7Ht8ZvCb488UbhbJutZ85iPX1DqRyZ1BV3T7V0FH5jNjM/ju9oNE+dUwb+BsCDO4wj/0DsAAAAAElFTkSuQmCC";
                    break;
            }
            byte[] Base64Stream = Convert.FromBase64String(imgBase64);
            BitmapDescriptor bitmapDescriptor = BitmapDescriptorFactory.FromStream(new MemoryStream(Base64Stream));
            return bitmapDescriptor;

        }



        internal static List<VehicleOnline> GetListVehicleOnline(string json)
        {
            return JsonConvert.DeserializeObject<List<VehicleOnline>>(json);
        }
    }
    public enum VehicleOnloineState : ushort
    {
        /// <summary>
        /// Xe đang chạy
        /// </summary>
        Running = 1,
        /// <summary>
        /// Dừng xe mở máy
        /// </summary>
        PauseOn = 2,
        /// <summary>
        /// Dừng xe tắt máy
        /// </summary>
        PauseOff = 3,
        /// <summary>
        /// Mất kết nối
        /// </summary>
        LostConnection = 4,
        /// <summary>
        /// Quá tốc độ
        /// </summary>
        OverSpeed = 5,
        /// <summary>
        /// Mất dữ liệu GPS
        /// </summary>
        LostGPS = 6
    }

}
