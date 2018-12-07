using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Application.Services.Dto;

namespace MPACore.PhoneBook.Dto
{
    /// <summary>
    /// 分页和排序
    /// </summary>
    public class PageAbdSortInputDto : IPagedResultRequest, ISortedResultRequest
    {
        [Range(1, 500)] public int MaxResultCount { get; set; } = 10;
        [Range(0, int.MaxValue)] public int SkipCount { get; set; } = 0;
        public string Sorting { get; set; } = "Id";
    }
}
