namespace Training_Center_Management_API.Dtos.common
{
    public class PagedResultDto<T>
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public ICollection<T> Data { get; set; }
    }
}
