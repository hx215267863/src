using System.IO;
using System.Net;

namespace IFactory.UI.Controls
{
    internal class WebReadState
    {
        public WebRequest webRequest;
        public MemoryStream memoryStream;
        public Stream readStream;
        public byte[] buffer;
    }
}
