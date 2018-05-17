using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Quanlyphuongtien
{
    public class LeftMenuOnline
    {
        public string Name { get; set; }
        public uint TotalDevice { get; set; }
        public List<VehicleOnline> Data { get; set; }

        public LeftMenuOnline()
        {
        }


    }
    public class GetLeftMenuOnline
    {
        public List<LeftMenuOnline> ListLeftMenuOnline { get; set; }
        public GetLeftMenuOnline(string json)
        {
            ListLeftMenuOnline = JsonConvert.DeserializeObject<List<LeftMenuOnline>>(json) ?? new List<LeftMenuOnline>();
        }
    }
}
