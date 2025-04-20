
namespace PocWebDevBackend.Service.Toast
{
    public class ToastService
    {
        public string Message { get; set; }
        public MessageType Type { get; set; }

        public enum MessageType
        {
            Success,
            Error,
            Wrarning,
            Info,
        }
    }
}
