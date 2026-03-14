namespace Tyuiu.Courses.Programming.Application.Dtos
{
	public class Pages
	{
		public const int PageSize = 10;
		private const int StackSize = 10;

		public int Total;

		public int Previous => Math.Max(1, Current - 1);
		public int Current { get; set; }
		public int Next => Math.Min(Current + 1, Total);

		public int StartPreviousStack => Math.Max(1, Current - StackSize);
		public int EndNextStack => Math.Min(Total, Current + StackSize);

		public Pages(int page, int itemsCount, int pageSize)
		{
			Total = (int)Math.Ceiling(itemsCount / (double)pageSize);
			Current = Math.Max(1, Math.Min(page, Total));
		}
	}
}
