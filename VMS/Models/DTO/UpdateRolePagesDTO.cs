namespace VMS.Models.DTO
{
    public class UpdateRolePagesDTO
    {
        
        public int RoleId { get; set; }
        public List<int> PageIds { get; set; }
        public int Status { get; set; }
        public int UpdatedBy { get; set; }
    }
}
