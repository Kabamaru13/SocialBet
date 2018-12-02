using System;
namespace SocialBet.Helpers
{
    public class ResultData
    {
        public Object data { get; set; }
        public Error error { get; set; }
    }

    public class Error
    {
        public int errorCode { get; set; }
        public string message { get; set; }
    }
}
