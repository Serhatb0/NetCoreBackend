using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class TokenOptions
    {
        public string Audience { get; set; } 
        public string Issuer { get; set; } // İmza
        public int AccessTokenExpiration { get; set; } // Erişim Jetonunun Son Kullanma Tarihi
        public string SecurityKey { get; set; } // Güvenlik Anahtarı


    }
}
