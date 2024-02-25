using System.Security.Cryptography;
using System.Text;

namespace InquestSpider.Resource.Core
{
    public class ResourceUrlHasher
    {
        public byte[] HashUrl(string url)
        {
            byte[] result = null;
            if (!string.IsNullOrEmpty(url))
            {
                result = SHA512.HashData(Encoding.UTF8.GetBytes(url.ToLower()));
            }
            return result;
        }
    }
}
