﻿namespace YemenSchoolsV1.Application.Helpers
{
	public class PaginationQuery
	{
		private const int MaxPageSize = 50;
		private int _pageSize = 10;
		public int PageNumber { get; set; } = 1;
		public int PageSize
		{
			get => _pageSize;
			set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
		}

		public PaginationQuery() { }

		public PaginationQuery(int pageNumber, int pageSize)
		{
			PageNumber = pageNumber;
			PageSize = pageSize;
		}
	}
}
