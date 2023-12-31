﻿namespace EGIDTask.Models.Criteria
{
    public class BaseCriteria
    {
        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public string Lang { get; set; }
        public int? Code { get; set; }
        public int? Id { get; set; }
        public List<int> Ids { get; set; }
        public bool PagingEnabled { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
        public string FreeText { get; set; }
        public bool ForGridView { get; set; }
        public bool? IsActive { get; set; }

    }
}
