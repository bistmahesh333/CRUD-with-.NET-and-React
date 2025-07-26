namespace API.Models
{
    public class memberATT
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public int? AddressId { get; set; }
        public string AddressName { get; set; }
        public int? MemberTypeId { get; set; }
        public string TypeName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Dob { get; set; }
        public DateTime? EntryDate { get; set; }
    }
}
