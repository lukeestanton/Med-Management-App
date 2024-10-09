public class Physician
{
    public string Name { get; set; }
    public string LicenseNumber { get; set; }
    public DateTime GraduationDate { get; set; }
    public List<string> Specializations { get; set; }

    public Physician()
    {
        Name = string.Empty;
        LicenseNumber = string.Empty;
        GraduationDate = DateTime.MinValue;
        Specializations = new List<string>();
    }
}
