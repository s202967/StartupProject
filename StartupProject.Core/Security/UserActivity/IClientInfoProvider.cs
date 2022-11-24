﻿namespace StartupProject.Core.Security.UserActivity
{
    public interface IClientInfoProvider
    {
        string BrowserInfo { get; }

        string ClientIpAddress { get; }

        string ComputerName { get; }

        bool IsLocal { get; }

        string Uri { get; }

        string BaseUri { get; }
    }
}