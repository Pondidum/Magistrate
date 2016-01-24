namespace Magistrate
{
	public class MagistrateUser
	{
		public string Name { get; set; }
		public string Key { get; set; }

		public bool CanCreatePermissions { get; set; }
		public bool CanCreateRoles { get; set; }
		public bool CanCreateUsers { get; set; }
	}
}
