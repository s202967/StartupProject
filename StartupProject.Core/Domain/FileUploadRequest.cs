namespace StartupProject.Core.Domain
{
    public class FileUploadRequest
    {
        public byte[] File { get; set; }

        public string ContentType { get; set; }

        public string FileName { get; set; }

        public FileUploadRequest(byte[] byteArray, string contentType, string fileName)
        {
            File = byteArray;
            ContentType = contentType;
            FileName = fileName;
        }
    }
}
