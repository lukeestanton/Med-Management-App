using System;

namespace MedManagementLibrary;

public class Insurance
{
    public int ID { get; set; }
    public string Name { get; set; }
    public decimal CoveragePercentage { get; set; }

    public Insurance() {
        Name = "No Insurance";
        CoveragePercentage = 0;
    }
}
