using System;
using System.Collections.Generic;
using System.Text;

namespace Quanlyphuongtien
{
    public interface iService
    {
        string GetLocalFilePath(string filename);
        void OpenDialer(string phoneNumber);
            
    }
}
