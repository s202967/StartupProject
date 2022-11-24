using Microsoft.AspNetCore.Http;
using System.IO;

namespace StartupProject.Client.Extension
{
    /// <summary>
    /// File related extensions
    /// </summary>
    public static class FileExtension
    {
        /// <summary>
        /// Gets the download binary array
        /// </summary>
        /// <param name="file">File</param>
        /// <returns>Download binary array</returns>
        public static byte[] ReadBytes(this IFormFile file)
        {
            using (var fileStream = file.OpenReadStream())
            {
                using (var ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    return fileBytes;
                }
            }
        }
    }
}
