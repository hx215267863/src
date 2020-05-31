using Microsoft.CSharp.RuntimeBinder;
using PagedList;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace IFactory.Domain.Models
{
  public class PagedData<T> : List<T>, IPagedData
  {
    public int PageCount { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalItemCount { get; set; }

    public PagedData()
    {
    }

    public PagedData(IEnumerable<T> list, IPagedList pagedList)
    {
      this.AddRange(list);
      this.PageCount = pagedList.PageCount;
      this.PageNumber = pagedList.PageNumber;
      this.PageSize = pagedList.PageSize;
      this.TotalItemCount = pagedList.TotalItemCount;
    }

    public PagedData(IPagedList<T> pagedList)
    {
      this.AddRange((IEnumerable<T>) pagedList);
      this.PageCount = pagedList.PageCount;
      this.PageNumber = pagedList.PageNumber;
      this.PageSize = pagedList.PageSize;
      this.TotalItemCount = pagedList.TotalItemCount;
    }

    public IPagedList ToPagedList()
    {
      return (IPagedList) new StaticPagedList<T>((IEnumerable<T>) this, this.PageNumber, this.PageSize, this.TotalItemCount);
    }

    public void AddItem(object obj)
    {
      //???????
    }
  }
}
