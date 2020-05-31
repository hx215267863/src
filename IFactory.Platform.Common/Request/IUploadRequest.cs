using IFactory.Platform.Common.Util;
using System.Collections.Generic;

namespace IFactory.Platform.Common.Request
{
  public interface IUploadRequest
  {
    IDictionary<string, FileItem> GetFileParameters();

    void SetFileParamaters(IDictionary<string, FileItem> fileParameters);
  }
}
