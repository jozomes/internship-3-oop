using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectApp.Classes;
namespace DrugoPredavanje
{
    class Program
    {
        public static Dictionary<Project, List<Assignment>> database = new Dictionary<Project, List<Assignment>>();
        public static DateTime getFutureDateInput()
        {
            DateTime inputDate;

            do
            {
                Console.Write("Enter a future date in the format dd-MM-yyyy: ");
                string input = Console.ReadLine();

                if (DateTime.TryParseExact(input, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
         out inputDate))
                {
                    if (inputDate > DateTime.Now)
                    {
                        return inputDate;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a future date.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please try again.");
                }
            } while (true);
        }
        public static string getYesNoInput()
        {
            string input = "h";
            Console.Write("Enter 'y' for yes or 'n' for no: ");
            do
            { 
                input = Console.ReadLine()?.ToLower();
            } while (input != "y" && input != "n");

            return input;
        }
        public static string getNonNullStringInput()
        {
            string input = "";
            do
            {
                input = Console.ReadLine();
            } while (string.IsNullOrEmpty(input));

            return char.ToUpper(input[0]) + input.Substring(1).ToLower(); 
        }
        public static int findMatchingNumber(int[] numbers)
        {
            int inputNumber = 0;
            Console.Write("Enter a number: ");
            do
            {

                while (!int.TryParse(Console.ReadLine(), out inputNumber))
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                    Console.Write("Enter a number: ");
                }

                foreach (int number in numbers)
                {
                    if (number == inputNumber)
                    {
                        return inputNumber;
                    }
                }

                Console.WriteLine("The number you entered is not in the array. Please try again.");
            } while (true);
        }

        static void printAllProjects() {
            Console.Clear();
            foreach (var item in database.Keys)
            {
                Console.WriteLine($"Project name: {item.Name}");
                for (global::System.Int32 i = 0; i < database[item].Count; i++)
                {
                    Console.WriteLine($"         Assignment{i+1}: {database[item][i].Name}, {database[item][i].Description}, deadline: {database[item][i].Deadline.Date} ");
                }
            }
            Console.WriteLine("Press any key to move back to start");
            Console.ReadKey();
            start();
        }

        static void addNewProject() {
            Console.Clear();
            Console.WriteLine("Enter desired project name: ");
            string newProjectName = getNonNullStringInput();
            foreach (var item in database.Keys)
            {
                if (item.Name.ToLower().Trim() == newProjectName.ToLower().Trim())
                {
                    Console.WriteLine("Project with that name already exists.");
                    Console.WriteLine("Aborting the addition of a new project, do you want to try again?");
                    string yesOrNo = getYesNoInput();
                    switch (yesOrNo)
                    {
                        case "y":
                            addNewProject();
                            break;
                        case "n":
                            start();
                            break;
                    }

                }
            }
            Console.WriteLine("Enter desired project description: ");
            string newProjectDesc = getNonNullStringInput();

            DateTime expectedProjectEnd = getFutureDateInput();
            Project newProject = new Project(newProjectName, newProjectDesc, expectedProjectEnd);
            List<Assignment> assignments = new List<Assignment>();
            database.Add(newProject, assignments);
            Console.WriteLine("Project added successfully. Press any key to move back to the start.");
            Console.ReadKey();
            start();
        }
        static void deleteProject() {
            Console.Clear();
            Console.WriteLine("Enter name of project you wish to delete: ");
            string targetName = getNonNullStringInput();
            foreach (var item in database.Keys)
            {
                if (item.Name.ToLower().Trim() == targetName.ToLower().Trim())
                {
                    Console.WriteLine("Project found.");
                    Console.WriteLine("Are you sure you want to delete: "+targetName+"?");
                    string yesOrNo = getYesNoInput();
                    switch (yesOrNo)
                    {
                        case "y":
                            database.Remove(item);
                            Console.WriteLine("Project removed successfully, press any key to move back to the start.");
                            Console.ReadKey();
                            start();
                            break;
                        case "n":
                            Console.WriteLine("Aborting the deletion process, press any key to move back to the start");
                            Console.ReadKey();
                            start();
                            break;
                    }

                }
                else
                {
                    Console.WriteLine("Inputted project name not found in our database, do you want to try again? ");
                    string retry = getYesNoInput();
                    switch(retry)
                    {
                        case "y":
                            deleteProject();
                            break;
                        case "n":
                            start();
                            break;
                    }
                }
            }

        }
        static void start()
        {
            Console.Clear();
            string startMessage = """
                1. Ispis svih projekata s pripadajućim zadatcima
                2. Dodavanje novog projekta
                3. Brisanje projekta
                4. Prikaz svih zadataka s rokom u sljedećih 7 dana
                5. Prikaz projekata filtriranih po statusu(samo aktivni, samo završeni, ili samo na čekanju)
                6. Upravljanje pojedinim projektom
                7. Upravljanje pojedinim zadatkom
                """;
            Console.WriteLine(startMessage);
            int switchInteger = findMatchingNumber([1, 2, 3, 4, 5, 6, 7]);
            switch (switchInteger)
            {
                case 1:
                    printAllProjects();
                    break;
                case 2:
                    addNewProject();
                    break;
                case 3:
                    deleteProject();
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;

            }


        }
        static void Main(string[] args)
        {
            
            DateTime timeCompleted = DateTime.Now;
            Project p1 = new Project("Kaufland", "Izgradnja kauflanda na poljičkoj", timeCompleted.AddMonths(2).Date);
            Assignment t1 = new Assignment("Garaža", "Završetak podzemne garaže za parking", timeCompleted.AddDays(14).Date, 10080, p1);
            Assignment t2 = new Assignment("Police", "Instalacija svih polica u trgovini", timeCompleted.AddDays(7).Date, 5040, p1);
            List<Assignment> list = new List<Assignment>();
            list.Add(t1);
            list.Add(t2);
            database.Add(p1, list);
            Project p2 = new Project("Poljud", "Renovacija stadiona", timeCompleted.AddMonths(7).Date);
            Assignment t11 = new Assignment("Stolice", "Zamjena slomljenih stolica", timeCompleted.AddDays(30).Date, 10080, p2);
            Assignment t22 = new Assignment("Kucice s hranom", "Bolje rasporediti kućice s hranom i pićem", timeCompleted.AddDays(21).Date, 5040, p2);
            List<Assignment> lista = new List<Assignment>();
            lista.Add(t11);
            lista.Add(t22);
            database.Add(p2, lista);
            Project p3 = new Project("Lidl", "Proširenje trgovine na Poljičkoj", timeCompleted.AddMonths(3).Date);
            List<Assignment> blanklist = new List<Assignment>();
            database.Add(p3, blanklist);

            start();
            Console.ReadKey();
        }
    }
}