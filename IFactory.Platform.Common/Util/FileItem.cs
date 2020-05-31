using System;
using System.IO;

namespace IFactory.Platform.Common.Util
{
    public class FileItem
    {
        private string fileName;
        private string mimeType;
        private byte[] content;
        private FileInfo fileInfo;

        public FileItem(FileInfo fileInfo)
        {
            if (fileInfo == null || !fileInfo.Exists)
                throw new ArgumentException("fileInfo is null or not exists!");
            this.fileInfo = fileInfo;
        }

        public FileItem(string filePath)
          : this(new FileInfo(filePath))
        {
        }

        public FileItem(string fileName, byte[] content)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName");
            if (content == null || content.Length == 0)
                throw new ArgumentNullException("content");
            this.fileName = fileName;
            this.content = content;
        }

        public FileItem(string fileName, byte[] content, string mimeType)
          : this(fileName, content)
        {
            if (string.IsNullOrEmpty(mimeType))
                throw new ArgumentNullException("mimeType");
            this.mimeType = mimeType;
        }

        public string GetFileName()
        {
            if (fileName == null && fileInfo != null && fileInfo.Exists)
                fileName = fileInfo.FullName;
            return fileName;
        }

        public string GetMimeType()
        {
            if (mimeType == null)
                mimeType = WebApiUtils.GetMimeType(GetContent());
            return mimeType;
        }

        public byte[] GetContent()
        {
            if (content == null && fileInfo != null && fileInfo.Exists)
            {
                using (Stream stream = (Stream)fileInfo.OpenRead())
                {
                    content = new byte[stream.Length];
                    stream.Read(content, 0, content.Length);
                }
            }
            return content;
        }
    }
}
