namespace HospitalManagementSystem.Models
{
	public class DoctorDetailModel
	{
        public int DoctorId { get; set; }
		public string? Name { get; set; }
		public string? Field { get; set; }
		public string? Gender { get; set; }
		public DateTime? Birthday { get; set; }
		public string? EmailAddress { get; set; }
		public string? PhoneNumber { get; set; }
	}
}