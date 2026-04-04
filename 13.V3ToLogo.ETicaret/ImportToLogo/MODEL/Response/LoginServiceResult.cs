namespace ImportToLogo.MODEL.Response
{
    public class LoginServiceResult
    {
        public int ReturnCode { get; set; } 
        public string Description { get; set; }
        public int FirmNr { get; set; }
        public int PeriodNr { get; set; }
        public string SessionId { get; set; }
        public string UserName { get; set; }
    }
}
