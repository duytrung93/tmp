using System;
using System.Collections.Generic;
using System.Text;

namespace Quanlyphuongtien
{
    public static class i18n
    {

        /// <summary>
        /// Đăng nhập
        /// </summary>
        public static string LoginPageBtnLoginLabel { get { return "Đăng nhập"; } }
        /// <summary>
        /// Giám sát online
        /// </summary>
        public static string TrackingPageTitle { get { return "Giám sát online"; } }
        /// <summary>
        /// Thông báo
        /// </summary>
        public static string MessageCallAPIFail { get { return "Thông báo"; } }

        public static string MessageApiCalling { get { return "Đang tải dữ liệu..."; } }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public static string TrackingPageLblVehicleOnlineSpeed { get { return "Trạng thái: "; } }
        /// <summary>
        /// Vị trí
        /// </summary>
        public static string TrackingPageLblVehicleOnlineAddress { get { return "Vị trí: "; } }
        /// <summary>
        /// Km trong ngày
        /// </summary>
        public static string TrackingPageLblVehicleOnlineTotalKM { get { return "Km trong ngày: "; } }
        /// <summary>
        /// Tổng thời gian di chuyển
        /// </summary>
        public static string TrackingPageLblVehicleOnlineTotalTimeRun { get { return "Tổng thời gian di chuyển: "; } }
        /// <summary>
        /// Tổng thời gian dừng xe bật máy
        /// </summary>
        public static string TrackingPageLblVehicleOnlineTotalTimePauseOn { get { return "Tổng thời gian dừng xe bật máy: "; } }
        /// <summary>
        /// Tìm kiếm xe
        /// </summary>
        public static string TrackingPageTxtSearchVehicleOnline { get { return "Tìm kiếm xe..."; } }
        /// <summary>
        /// Không tìm thấy biển số xe
        /// </summary>
        public static string TrackingPageLblCantFindVehicle { get { return "Không tìm thấy biển số xe"; } }
        /// <summary>
        /// Khách hàng chưa có xe trong hệ thống
        /// </summary>
        public static string TrackingPageLblNotHaveVehicle { get { return "Khách hàng chưa có xe trong hệ thống"; } }
        /// <summary>
        /// Danh sách xe
        /// </summary>
        public static string TrackingpageBtnListVehicle { get { return "Danh sách xe"; } }
        /// <summary>
        /// Bản đồ
        /// </summary>
        public static string TrackingpageBtnMap { get { return "Bản đồ"; } }
        /// <summary>
        /// Chưa chọn xe
        /// </summary>
        public static string TrackingPageInfoBoxPlate { get { return "Chưa chọn xe"; } }
        /// <summary>
        /// Ẩn
        /// </summary>
        public static string TrackingPageInfoBoxBtnHide { get { return "Ẩn"; } }
        /// <summary>
        /// Hiện
        /// </summary>
        public static string TrackingPageInfoBoxBtnShow { get { return "Hiện"; } }
        /// <summary>
        /// Thời gian
        /// </summary>
        public static string TrackingPageInfoBoxLblDtime { get { return "Thời gian: "; } }
        /// <summary>
        /// Vị trí
        /// </summary>
        public static string TrackingPageInfoBoxLblAddress { get { return "Vị trí: "; } }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public static string TrackingPageInfoBoxLblStatus { get { return "Trạng thái: "; } }
        /// <summary>
        /// Tổng km
        /// </summary>
        public static string TrackingPageInfoBoxLblTotalKM { get { return "Tổng km: "; } }
        /// <summary>
        /// Cửa xe
        /// </summary>
        public static string TrackingPageInfoBoxLblDoorState { get { return "Cửa xe: "; } }
        /// <summary>
        /// Tổng thời gian di chuyển
        /// </summary>
        public static string TrackingPageInfoBoxLblTotalTimeRun { get { return "TG di chuyển: "; } }
        /// <summary>
        /// Tổng thời dừng xe bật máy
        /// </summary>
        public static string TrackingPageInfoBoxLblTotalTimePauseOn { get { return "TG dừng bật máy: "; } }
        /// <summary>
        /// Tốc độ
        /// </summary>
        public static string TrackingPageInfoBoxLblSpeed { get { return "Tốc độ: "; } }
        /// <summary>
        /// Nhiên liệu
        /// </summary>
        public static string TrackingPageInfoBoxLblOil { get { return "Nhiên liệu: "; } }
        /// <summary>
        /// Điều hòa
        /// </summary>
        public static string TrackingPageInfoBoxLblAir { get { return "Điều hòa: "; } }

        public static string ViewLogPageBtnFormInput { get { return "Thông tin xe"; } }
        public static string ViewLogPageBtnMap { get { return "Bản dồ"; } }

        public static string ViewLogPageBtnGetLog { get { return "Xem lại lộ trình"; } }

        public static string ViewLogPageValidatePlateRequied { get { return "Chưa nhập biển số"; } }

        public static string MessageApiNoData { get { return "Không có dữ liệu hiển thị"; } }

        public static string ViewLogPagePointStart { get { return "Bắt đầu"; } }
        public static string ViewLogPagePointEnd { get { return "Kết thúc"; } }

        public static string ViewLogPageLblShowSpeed { get { return "Tốc độ"; } }

        public static string MessageInitData { get { return "Đang khởi tạo dữ liệu..."; } }

        public static string ContactPageTitle { get { return "Thông tin liên hệ"; } }
        public static string ContactPagelblAddress { get { return "Địa chỉ: "; } }
        public static string ContactPagelblphone { get { return "Số điện thoại: "; } }
        public static string ContactPagelblEmail { get { return "Email: "; } }

        public static string ContactPageValueAddress { get { return "Số 19, Tổ 29, Khu X2A, Yên Sở, Hoàng Mai, Hà Nội."; } }
        public static string ContactPageValuePhone { get { return "1900.6735"; } }
        public static string ContactPageValueEmail { get { return "bistechvina@gmail.com"; } }

        public static string ContactPagelblCompanyName { get { return "Công ty CP Công nghệ Bistech Việt Nam"; } }

        public static string AccountPageTitle { get { return "Thông tin tài khoản"; } }

        public static string AccountPageTxtUserName { get { return "Tài khoản"; } }
        public static string AccountPageTxtFullName { get { return "Họ tên"; } }
        public static string AccountPageTxtOldPassword { get { return "Mật khẩu cũ"; } }
        public static string AccountPageTxtNewPass { get { return "Mật khẩu mới"; } }
        public static string AccountPageTxtReNewPass { get { return "Nhập lại"; } }
        public static string AccountPagebtnSubmit { get { return "Cập nhật"; } }

        public static string AccountPageNewPassNotEqualRePass { get { return "Mật khẩu mới và xác nhận mật khẩu không khớp"; } }

        public static string AccountPageChangePassOK { get { return "Đổi mật khẩu thành công, vui lòng đăng nhập lại"; } }

        /// <summary>
        /// Xem lại lộ trình
        /// </summary>
        internal static string ViewLogPageTitle { get { return "Xem lại lộ trình"; } }
        /// <summary>
        /// Quản lý phương tiện
        /// </summary>
        internal static string MainPageTitle { get { return "Quản lý phương tiện"; } }
        /// <summary>
        /// Tài khoản
        /// </summary>
        internal static string LoginPageTxtUsername { get { return "Tài khoản"; } }
        /// <summary>
        /// Mật khẩu
        /// </summary>
        internal static string LoginPageTxtPassword { get { return "Mật khẩu"; } }
        /// <summary>
        /// Quên mật khẩu
        /// </summary>
        internal static string LogInPageLblForgotPass { get { return "Quên mật khẩu!"; } }
        /// <summary>
        /// Tự động đăng nhập
        /// </summary>
        internal static string LoginPageCbxAutoLogin { get { return "Tự động đăng nhập"; } }
        /// <summary>
        /// Hỗ trợ dịch vụ: 1900 6735
        /// </summary>
        internal static string LoginPageLblHotLine { get { return "Hỗ trợ dịch vụ: 1900 6735"; } }
        /// <summary>
        /// Hệ thống đang xử lý...
        /// </summary>
        internal static string MessageSystemProcessing { get { return "Hệ thống đang xử lý..."; } }
        /// <summary>
        /// Thông báo
        /// </summary>
        internal static string MessageAlert { get { return "Thông báo"; } }
        /// <summary>
        /// Đóng
        /// </summary>
        internal static string MessageClose { get { return "Đóng"; } }
        /// <summary>
        /// Đăng nhập hệ thống
        /// </summary>
        internal static string LoginPageTitle { get { return "Đăng nhập hệ thống"; } }
        /// <summary>
        /// Giám sát
        /// </summary>
        internal static string MainPageBtnTracking { get { return "Giám sát"; } }
        /// <summary>
        /// Lộ trình
        /// </summary>
        internal static string MainPageBtnRouter { get { return "Lộ trình"; } }
        /// <summary>
        /// Báo cáo
        /// </summary>
        internal static string MainPageBtnReport { get { return "Báo cáo"; } }
        /// <summary>
        /// Thông báo
        /// </summary>
        internal static string MainPageBtnAlert { get { return "Thông báo"; } }
        /// <summary>
        /// Liên hệ
        /// </summary>
        internal static string MainPageBtnContact { get { return "Liên hệ"; } }
        /// <summary>
        /// Tài khoản
        /// </summary>
        internal static string MainPageBtnUser { get { return "Tài khoản"; } }
        /// <summary>
        /// Thoát
        /// </summary>
        internal static string MainPageBtnLogout { get { return "Thoát"; } }



    }
}
