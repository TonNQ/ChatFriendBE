namespace BKConnect.BKConnectBE.Common
{
    public static class MsgNo
    {
        //Success
        public static readonly string SUCCESS = "Thành công";
        public static readonly string SUCCESS_REGISTER = "Đăng ký tài khoản thành công";

        //Error
        public static readonly string ERROR_REGISTER = "Đăng ký tài khoản thất bại";
        public static readonly string ERROR_EMAIL_HAS_USED = "Email này đã được sử dụng";

        //Error internal service
        public static readonly string ERROR_INTERNAL_SERVICE = "Một lỗi đã xảy ra trên hệ thống, thử lại sau";
    }
}