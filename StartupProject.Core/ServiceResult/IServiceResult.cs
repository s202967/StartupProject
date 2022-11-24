using System.Collections.Generic;

namespace StartupProject.Core.ServiceResult
{
    public interface IServiceResult
    {
        bool Status { get; set; }
        List<string> Message { get; set; }
        string MessageType { get; set; }
    }
}
