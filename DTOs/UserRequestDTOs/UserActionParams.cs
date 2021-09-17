
namespace UserManagement.DTOs
{
    /// <summary>
    /// Contains query properties for searching, filtering and getting users for a specific page.
    /// </summary>
    public class UserActionParams
    {
        public string StateName { get; set; }
        public string SearchWord { get; set; }
        public int Page { get; set; } = 1;
    }
}
