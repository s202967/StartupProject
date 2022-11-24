using System.Collections.Generic;

namespace StartupProject.Core.ServiceResult
{
    public class ServiceResult : IServiceResult
    {
        public string MessageType { get; set; }

        public List<string> Message { get; set; }

        public bool Status { get; set; }

        public ServiceResult(bool status, List<string> message = null, string messageType = null)
        {
            if (string.IsNullOrEmpty(messageType))
                MessageType = Core.ServiceResult.MessageType.Success;
            else
                MessageType = messageType;
            if (message == null)
                message = new List<string>();
            Message = message;
            Status = status;
        }

        public ServiceResult(bool status, List<string> message)
        {
            Status = status;
            Message = message;
        }
    }
}

namespace StartupProject.Core.ServiceResult
{
    public class ServiceResult<T> : ServiceResult
    {
        private T ResposeData { get; set; }
        public T Data
        {
            get => ResposeData;
            set => ResposeData = value;
        }

        public ServiceResult(bool status, List<string> message = null, string messageType = null)
            : base(status, message, messageType)
        {
        }
    }
}
