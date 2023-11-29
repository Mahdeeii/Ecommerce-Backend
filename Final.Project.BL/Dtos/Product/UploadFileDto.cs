namespace Final.Project.BL
{
    public class UploadFileDto
{
        public bool IsSuccess { get; set; }
        public string Massage { get; set; }
        public string URL { get; set; }
        public UploadFileDto(bool issuccess, string massage, string url="")
        {
            IsSuccess = issuccess;
            Massage = massage;
            URL = url;
        }
    }
}
