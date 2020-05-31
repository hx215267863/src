using IFactory.Platform.Common.Util;
using System.Collections.Generic;

namespace IFactory.Platform.Common.Request
{
  public abstract class BaseUploadRequest<T> : BaseRequest<T>, IUploadRequest<T>, IRequest<T>, IUploadRequest where T : BaseResponse
  {
    private IDictionary<string, FileItem> fileParameters;

    public virtual IDictionary<string, FileItem> GetFileParameters()
    {
      return fileParameters ?? (fileParameters = new Dictionary<string, FileItem>());
    }

    public void SetFileParamaters(IDictionary<string, FileItem> fileParameters)
    {
      this.fileParameters = fileParameters;
    }
  }
}
