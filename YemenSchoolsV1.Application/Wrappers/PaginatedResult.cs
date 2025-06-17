namespace YemenSchoolsV1.Application.Wrappers
{
	public class PaginatedResult<T>
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public int TotalPages { get; set; }
		public int TotalRecords { get; set; }
		public bool HasPreviousPage => PageNumber > 1;
		public bool HasNextPage => PageNumber < TotalPages;
		public bool Succeeded { get; set; }
		public List<string>? Messages { get; set; } = [];

		public List<T> Data { get; set; }



		public static PaginatedResult<T> Success(List<T> data, int totalRecords, int pageNumber, int pageSize)
		{
			return new(data, totalRecords, pageNumber, pageSize, true);
		}
		//public static PaginatedResult<T> Failure(List<T> data, int totalRecords, int pageNumber, int pageSize, List<string> messages)
		//{
		//	return new(data, totalRecords, pageNumber, pageSize, false, messages);
		//}

		private PaginatedResult(List<T> data, int totalRecords, int pageNumber, int pageSize, bool succeeded, List<string> messages = null!)
		{
			Data = data;
			TotalRecords = totalRecords;
			PageNumber = pageNumber;
			PageSize = pageSize;
			TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
			Succeeded = succeeded;
			Messages = messages;
		}
	}
}
