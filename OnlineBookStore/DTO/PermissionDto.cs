namespace OnlineBookStore.DTO
{
    public class PermissionDto
    {
        public int PermissionId { get; set; }
        public string Name { get; set; }
        public string Module { get; set; }
        public string Action { get; set; }
        public string Resource { get; set; }
        public bool IsActive { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }

    }
}
