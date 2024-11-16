using System;

namespace MedManagementLibrary
{
    public class Treatment
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Treatment()
        {
            Name = string.Empty;
            Price = 0;
        }
    }
}

