using MedManagementLibrary;

public class Program
{
    static void Main(string[] args)
    {
        List<Patient> patients = new List<Patient>();
        List<Physician> physicians = new List<Physician>();
        AppointmentManager appointmentManager = new AppointmentManager();

        bool isContinue = true;

// Menu
        while (isContinue)
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Add a patient");
            Console.WriteLine("2. Add a physician");
            Console.WriteLine("3. Schedule an appointment");
            Console.WriteLine("4. Quit");

            string input = Console.ReadLine() ?? string.Empty;

            switch (input)
            {
                case "1":
                    AddPatient(patients);
                    break;

                case "2":
                    AddPhysician(physicians);
                    break;

                case "3":
                    ScheduleAppointment(patients, physicians, appointmentManager);
                    break;

                case "4":
                    isContinue = false;
                    break;

                default:
                    Console.WriteLine("That's not a valid option!");
                    break;
            }
        }
    }

// Add a new patient and store in Patient object
    static void AddPatient(List<Patient> patients)
    {
        Patient newPatient = new Patient();

        Console.WriteLine("Enter patient's name:");
        newPatient.Name = Console.ReadLine();

        Console.WriteLine("Enter patient's address:");
        newPatient.Address = Console.ReadLine();

        Console.WriteLine("Enter patient's birthdate (yyyy-mm-dd):");
        newPatient.Birthday = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Enter patient's race:");
        newPatient.Race = Console.ReadLine();

        Console.WriteLine("Enter patient's gender:");
        newPatient.Gender = Console.ReadLine();

        patients.Add(newPatient);
        Console.WriteLine("Patient added.");
    }

// Add a new physician and store in Physician object
    static void AddPhysician(List<Physician> physicians)
    {
        Physician newPhysician = new Physician();

        Console.WriteLine("Enter physician's name:");
        newPhysician.Name = Console.ReadLine();

        Console.WriteLine("Enter physician's license number:");
        newPhysician.LicenseNumber = Console.ReadLine();

        Console.WriteLine("Enter physician's graduation date (yyyy-mm-dd):");
        newPhysician.GraduationDate = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Enter physician's specializations (comma-separated):");
        newPhysician.Specializations = new List<string>(Console.ReadLine().Split(','));

        physicians.Add(newPhysician);
        Console.WriteLine("Physician added.");
    }


    static void ScheduleAppointment(List<Patient> patients, List<Physician> physicians, AppointmentManager appointmentManager)
    {
        if (patients.Count == 0 || physicians.Count == 0)
        {
            Console.WriteLine("You need at least one patient and one physician to schedule an appointment!");
            return;
        }

        Console.WriteLine("Select a patient by number:");
        for (int i = 0; i < patients.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {patients[i].Name}"); // + 1 to start the console loop from 1 but still start from 0 internally
        }
        int patientIndex = int.Parse(Console.ReadLine()) - 1; // Adjustment to start from 1 on console list

        Console.WriteLine("Select a physician by number:");
        for (int i = 0; i < physicians.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {physicians[i].Name}");
        }
        int physicianIndex = int.Parse(Console.ReadLine()) - 1;

        Console.WriteLine("Enter appointment date and time (yyyy-mm-dd HH:mm):");
        DateTime appointmentDate = DateTime.Parse(Console.ReadLine());

        appointmentManager.ScheduleAppointment(patients[patientIndex], physicians[physicianIndex], appointmentDate);
    }
}