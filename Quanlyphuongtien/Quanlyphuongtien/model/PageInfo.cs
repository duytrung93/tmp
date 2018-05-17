using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanlyphuongtien
{
    public class PageInfo
    {
        public ushort CurentPage { get; set; }
        public ushort RecordOfPage { get; set; }
        public ushort TotalPage { get; set; }
        public object Data { get; set; }
        public PageInfo() { }
        public PageInfo(ushort iPage, ushort iRecordOfPage, List<VehicleLog> iData)
        {
            int vEndRecord = iData.Count - iRecordOfPage * iPage;
            if (vEndRecord > iRecordOfPage)
                this.Data = iData.Skip(iRecordOfPage * iPage).Take(iRecordOfPage).ToList();
            else if (vEndRecord > 0)
                this.Data = iData.Skip(iRecordOfPage * iPage).Take(vEndRecord).ToList();
            this.CurentPage = iPage;
            this.RecordOfPage = iRecordOfPage;
            if (iData.Count % iRecordOfPage > 0)
                this.TotalPage = (ushort)(iData.Count / iRecordOfPage + 1);
            else
                this.TotalPage = (ushort)(iData.Count / iRecordOfPage);
        }

    }
}
