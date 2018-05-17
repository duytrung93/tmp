using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace Quanlyphuongtien
{
    public class LogData
    {
        public List<VehicleLog> Log { get; set; }
        public List<VehiclePause> Pause { get; set; }
        public LogData() { }
        public LogData(string json)
        {
            LogData logData = JsonConvert.DeserializeObject<LogData>(json);
            if (logData != null)
            {
                this.Log = logData.Log;
                this.Pause = logData.Pause;
            }
        }

    }

    public class VehicleLog
    {
        public string Dtime { get; set; }
        public uint Lat { get; set; }
        public uint Lon { get; set; }
        public string Speed { get; set; }
        public float Direction { get; set; }
        public uint Distance { get; set; }
        public ushort State { get; set; }
        public string OtherState { get; set; }
        public string Address { get; set; }
        public string Timedata { get; set; }
    }
    public class VehiclePause
    {
        public uint Vehicle_id { get; set; }
        public uint Stt { get; set; }
        public string Plate { get; set; }
        public string DriverName { get; set; }
        public string GPLX { get; set; }
        public string TransportType { get; set; }
        public string Time { get; set; }
        public string TimePause { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public bool Active { get; set; }
        public string BeginPause { get; set; }
        public string EndPause { get; set; }
        public uint TotalTime { get; set; }
        public bool CheckState { get; set; }
        public string RF_ID { get; set; }
        public uint TotalRecord { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string isOK { get; set; }
        public string SensorState { get; set; }
    }

}
