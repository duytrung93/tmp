using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace Quanlyphuongtien
{
    public class VehicleTooltip
    {
        public string Speed { get; set; }
        public string DateUpdate { get; set; }
        public string Dtime { get; set; }
        public string TotalKM { get; set; }
        public string Oil { get; set; }
        public string TimeOn { get; set; }
        public string DriverName { get; set; }
        public string GPLX { get; set; }
        public string GPLX_DateActive { get; set; }
        public string GPLX_DateExpired { get; set; }
        public string TotalOverSpeed { get; set; }
        public string TotalPause { get; set; }
        public string TimePause { get; set; }
        public string TimeRun { get; set; }
        public string TotalTimePauseOn { get; set; }
        public string TotalTimeRun { get; set; }
        public string Address { get; set; }
        public string CustomerName { get; set; }
        public string VIN { get; set; }
        public string VehicleType { get; set; }
        public string Status { get; set; }
        public string CityName { get; set; }
        public string RankName { get; set; }
        public string Phone { get; set; }
        public string Simnumber { get; set; }
        public string Imei { get; set; }
        public string DoorState { get; set; }
        public string Air { get; set; }
        public string OilLevel { get; set; }
        public string TCDBVNState { get; set; }

        public VehicleTooltip()
        {
        }

        public VehicleTooltip(string json)
        {
            VehicleTooltip obj = JsonConvert.DeserializeObject<VehicleTooltip>(json);
            this.Speed = obj.Speed;
            this.DateUpdate = obj.DateUpdate;
            this.Dtime = obj.Dtime;
            this.TotalKM = obj.TotalKM;
            this.Oil = obj.Oil;
            this.TimeOn = obj.TimeOn;
            this.DriverName = obj.DriverName;
            this.GPLX = obj.GPLX;
            this.GPLX_DateActive = obj.GPLX_DateActive;
            this.GPLX_DateExpired = obj.GPLX_DateExpired;
            this.TotalOverSpeed = obj.TotalOverSpeed;
            this.TotalPause = obj.TotalPause;
            this.TimePause = obj.TimePause;
            this.TimeRun = obj.TimeRun;
            this.TotalTimePauseOn = obj.TotalTimePauseOn;
            this.TotalTimeRun = obj.TotalTimeRun;
            this.Address = obj.Address;
            this.CustomerName = obj.CustomerName;
            this.VIN = obj.VIN;
            this.VehicleType = obj.VehicleType;
            this.Status = obj.Status;
            this.CityName = obj.CityName;
            this.RankName = obj.RankName;
            this.Phone = obj.Phone;
            this.Simnumber = obj.Simnumber;
            this.Imei = obj.Imei;
            this.DoorState = obj.DoorState;
            this.Air = obj.Air;
            this.OilLevel = obj.OilLevel;
            this.TCDBVNState = obj.TCDBVNState;
        }
    }
}
