using StartupProject.Core.BaseEntity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartupProject.Core.Security.Identity
{
    [Table("RefreshToken")]
    public class RefreshToken : Entity<long>
    {
        public RefreshToken(string userName, string token, string obfuscatedToken, string clientIpAddress)
        {
            UserName = userName;
            Token = token;
            ObfuscatedToken = obfuscatedToken;
            ClientIpAddress = clientIpAddress;
        }

        public string UserName { get; set; }

        [Column("RefreshToken")]
        public string Token { get; set; }

        public string ObfuscatedToken { get; set; }

        public string ClientIpAddress { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
