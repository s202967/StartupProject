namespace StartupProject.Services.Common
{
    public class AppSettings
    {
        public string FileBasePath { get; set; }

        public string FileRootFolderName { get; set; }

        public string FileBaseUrlPrefix
        {
            get
            {
                return $"/{FileRootFolderName}";
            }
        }

        public bool AwsSesEnabled { get; set; }
    }
}
