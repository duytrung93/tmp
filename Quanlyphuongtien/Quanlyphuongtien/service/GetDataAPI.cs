using Newtonsoft.Json;
using System;
using System.Net;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Quanlyphuongtien
{
    public static class GetDataAPI
    {
        public static async Task<GetAPIResponse> GetAPI(string iAPI, object param)
        {
            string url = "";
            try
            {
                HttpClient client = new HttpClient();
                Type type = param.GetType();
                var vQueryString = "?App=1";
                foreach (PropertyInfo info in type.GetProperties())
                {
                    // Do something with the property info.
                    string propValue = info.GetValue(param, null).ToString();
                    vQueryString += String.Format("&{0}={1}", info.Name, HttpUtility.UrlEncode(propValue));

                }

                //client.PostAsync(pQueryString,)
                url = iAPI + vQueryString;


                var response = await client.GetAsync(url);

                if (response != null)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    return new GetAPIResponse
                    {
                        Status = response.StatusCode,
                        Result = result
                    };
                }
                return new GetAPIResponse
                {
                    Status = HttpStatusCode.ExpectationFailed,
                    Result = "Đã có lỗi sảy ra khi gọi API: " + url
                };
            }
            catch (Exception ex)
            {

                return new GetAPIResponse
                {
                    Status = HttpStatusCode.ExpectationFailed,
                    Result = "Không thể kết nối với máy chủ"
                };
            }

        }
        public static async Task LogAPIAsync(string pQueryString)
        {
            HttpClient client = new HttpClient();
            //client.PostAsync(pQueryString,)
            await client.GetAsync(API.APPLogapi + "?iLoginfo=" + btoa(pQueryString));
        }
        /// <summary>
        /// Mã hóa link id công việc
        /// </summary>
        /// <param name="toEncode"></param>
        /// <returns></returns>
        public static string btoa(string toEncode)
        {
            byte[] bytes = Encoding.GetEncoding(28591).GetBytes(toEncode);
            string toReturn = System.Convert.ToBase64String(bytes);
            return toReturn;
        }
    }
    public class GetAPIResponse
    {
        public HttpStatusCode Status { get; set; }
        public string Result { get; set; }
    }
    public static class API
    {
        private static string LinkAPI = "https://quanlyphuongtien.vn:9090/";
        //public static string LinkAPI = "http://113.190.153.238:6868/";
        #region TKAPI
        /// <summary>
        /// Đăng nhập hệ thống			
        /// iUserName	string	Tên đăng nhập
        /// iPassword   string  Mật khẩu
        /// </summary>
        public static string Login { get { return LinkAPI + "TKAPI/Login"; } }
        /// <summary>
        /// Lấy Menu bên trái khi người dùng đăng nhập(danh sách xe)
        /// iTockenkey	string	Chuỗi key lấy từ login
        /// </summary>
        public static string GetLeftMenuOnline { get { return LinkAPI + "TKAPI/GetLeftMenuOnline"; } }
        /// <summary>
        /// Lấy danh sách thiết bị hiển thị online trên bản đồ
        /// iListVehicleId	string	Danh sách ID xe, mỗi ID cách nhau bởi dấu "," ví dụ ",1,2,3,4,5,"
        /// iTockenkey	string	Chuỗi key lấy từ login
        /// </summary>
        public static string GetVehicleOnlineByListId { get { return LinkAPI + "TKAPI/GetVehicleOnlineByListId"; } }
        /// <summary>
        /// Lấy thông tin xe khi click vào xe			
        /// iVehicle_id	uint	ID xe cần xem dữ liệu
        /// TockenKey	string	Chuỗi key lấy từ login
        /// </summary>
        public static string GetVehicleTooltip { get { return LinkAPI + "TKAPI/GetVehicleTooltip"; } }
        /// <summary>
        /// Xem lại lộ trình
        /// FromDate: 2018-05-04T00:00:00.000
        /// ToDate: 2018-05-04T23:59:00.000
        /// VehicleId: 9294
        /// MaxSpeed: 80
        /// Plate: 29C-945.16
        /// isHC: true
        /// TockenKey: ctyquangvinh
        /// </summary>
        public static string GetVehicleLog { get { return LinkAPI + "TKAPI/GetVehicleLog"; } }
        /// <summary>
        /// Thay đổi mật khẩu		
        /// iPasswordOld	string	Mật khẩ cũ
        /// iPasswordNew	string	Mật khẩu mới
        /// iTockenkey	string	Chuỗi key lấy từ login
        /// </summary>

        public static string ChangePassword { get { return LinkAPI + "TKAPI/ChangePassword"; } }
        

        #endregion

        public static string APPLogapi = LinkAPI + "API/APPLogapi";

    }
}
