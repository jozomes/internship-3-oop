using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using ProjectApp.Classes;
using ProjectApp.Enums;
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
        public static void backToStart() {
            Console.WriteLine("Press any key to go back to the start.");
            Console.ReadKey();
            start();
        }
        public static double getDoubleGreaterThanOne()
        {
            double input;

            do
            {
                Console.Write("Enter a double value greater than 1: ");
                string inputString = Console.ReadLine();

                if (double.TryParse(inputString, out input))
                {
                    if (input > 1)
                    {
                        return input;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid double value greater than 1.");
                }
            } while (true);
        }
        public static string getYesNoInput()
        {
            string input = "h";
            Console.Write("Enter 'y' for yes or 'n' for no: ");
            do
            { 
                input = Console.ReadLine()?.ToLower().Trim();
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
            bool found = false;
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
                /*else
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
                }*/
                }
            if (!found)
            {
                Console.WriteLine("Inputted project name not found in our database, do you want to try again? ");
                string retry = getYesNoInput();
                switch (retry)
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
        static void dueIN7Days() {
            Console.Clear();
            List<Assignment> dueIN7Days = new List<Assignment>();
            foreach (var item in database.Keys)
            {
                for (global::System.Int32 i = 0; i < database[item].Count; i++)
                {
                    TimeSpan days = database[item][i].Deadline - DateTime.Now;
                    if (days.TotalDays < 7)
                    {
                        dueIN7Days.Add(database[item][i]);
                    }
                }
            }
            Console.WriteLine("List of all assignments due in 7 days: ");
            foreach (var item in dueIN7Days)
            {
                Console.WriteLine($"Assignment: {item.Name} {item.Description} {item.Deadline}, parent project: {item.parentProject.Name}");
            }
            Console.WriteLine("Press any key to move back to the start.");
            Console.ReadKey();
            start();
        }
        static void filterByStatus() {
            Console.Clear();
            Console.WriteLine("Enter option you want to filter by: ");
            string startMessage = """
                1. Samo aktivni
                2. Samo završeni
                3. Samo na čekanju
                """;
            Console.WriteLine(startMessage);
            int input = findMatchingNumber([1, 2, 3]);
            List<Project> projects = new List<Project>();
            switch (input)
            {
                case 1:
                    foreach (var item in database.Keys)
                    {
                        if (item.status == projectStatus.Active)
                        {
                            projects.Add(item);
                        }
                    }
                    backToStart();
                    break;
                case 2:
                    foreach (var item in database.Keys)
                    {
                        if (item.status == projectStatus.Completed)
                        {
                            projects.Add(item);
                        }
                    }
                    backToStart();
                    break;
                case 3:
                    foreach (var item in database.Keys)
                    {
                        if (item.status == projectStatus.OnHold)
                        {
                            projects.Add(item);
                        }
                    }
                    backToStart();
                    break;
            }

            foreach (var item in projects)
            {
                Console.WriteLine($"{item.Name} - {item.Description} - {item.status}");
            }
            Console.WriteLine("Press any key to move back to the start.");
            Console.ReadKey();
        }
        static void manageProject()
        {
            Console.Clear();
            Console.WriteLine("Enter name of project you wish to modify: ");
            string targetName = getNonNullStringInput();
            Project targetedProject = null;
            bool found = false;
            foreach (var item in database.Keys)
            {
                if (item.Name.ToLower().Trim() == targetName.ToLower().Trim())
                {
                    targetedProject = item;
                    found = true;
                    break;
                }
            }
            switch (found)
            {
                case true:
                    Console.WriteLine("Project found!");
                    break;
                case false:
                    Console.WriteLine("Project not found, do you want to try again? ");
                    string input = getYesNoInput();
                    switch (input)
                    {
                        case "y":
                            manageProject();
                            break;
                        case "n":
                            Console.WriteLine("Press any key to move back to the start");
                            Console.ReadKey();
                            start();
                            break;
                    }
                    break;
            }
            bool assignmentsDone = true;
            foreach (var item in database[targetedProject])
            {
                if (item.Status != assignmentStatus.Completed)
                {
                    assignmentsDone = false;
                    break;
                }
            }
            if (assignmentsDone) targetedProject.status = projectStatus.Completed;
            if (targetedProject.status == projectStatus.Completed)
            {
                foreach (var item in database[targetedProject])
                {
                    item.Status = assignmentStatus.Completed;
                }
            }
            string startMessage = """
                1. Ispis svih zadataka unutar odabranog projekta
                2. Prikaz detalja odabranog projekta
                3. Uređivanje statusa projekta
                4. Dodavanje zadatka unutar projekta
                5. Brisanje zadatka iz projekta
                6. Prikaz ukupno očekivanog vremena potrebnog za sve aktivne zadatke u projektu
                7. Ispis svih zadataka unutar odabranog projekta sortiranih prema vremenu trajanje (najkraće -> najduže)
                8. Sortirati zadatke po priroritetu.
                """;
            Console.WriteLine(startMessage);
            int switchInteger = findMatchingNumber([1, 2, 3, 4, 5, 6, 7, 8]);
            switch (switchInteger)
            {
                case 1:
                    foreach (var item in database[targetedProject])
                    {
                        Console.WriteLine($"{item.Name} {item.Description} {item.Deadline} {item.Status}");
                    }
                    break;
                case 2:
                    Console.WriteLine($"{targetedProject.Name} {targetedProject.Description} Start date: {targetedProject.startDate} End date: {targetedProject.endDate}");
                    break;
                case 3:
                    if (targetedProject.status == projectStatus.Completed)
                    {
                        foreach (var item in database[targetedProject])
                        {
                            item.toCompleted();
                        }
                        Console.WriteLine("You are not allowed to modify a completed project!");
                        backToStart();

                    }
                    Console.WriteLine("Set to which stats you want to change the project: ");
                    string startMessageStatus = """
                        1. Aktivan
                        2. Završen
                        3. Na čekanju
                        """;
                    Console.WriteLine(startMessageStatus);
                    int input = findMatchingNumber([1, 2, 3]);
                    switch (input)
                    {
                        case 1:
                            targetedProject.toActive();
                            backToStart();
                            break;
                        case 2:
                            targetedProject.toCompleted();
                            backToStart();
                            break;
                        case 3:
                            targetedProject.toOnHold();
                            backToStart();
                            break;
                    }
                    break;
                case 4:
                    if (targetedProject.status == projectStatus.Completed)
                    {
                        foreach (var item in database[targetedProject])
                        {
                            item.toCompleted();
                        }
                        Console.WriteLine("You are not allowed to modify a completed project!");
                        backToStart();

                    }
                    Console.WriteLine("Enter new assignment name: ");
                    string newAssignmentName = getNonNullStringInput();
                    foreach (var item in database[targetedProject])
                    {
                        if (item.Name.ToLower().Trim() == newAssignmentName.ToLower().Trim())
                        {
                            Console.WriteLine("Assignment with that name in that project already exists, do you want to try again?");
                            string yesOrNo = getYesNoInput();
                            switch (yesOrNo)
                            {
                                case "a":
                                    manageProject();
                                    break;
                                case "b":
                                    backToStart();
                                    break;
                            }
                        }
                    }
                    Console.WriteLine("Enter new assignment description: ");
                    string newAssignmentDescription = getNonNullStringInput();
                    Console.WriteLine("Enter Deadline: ");
                    DateTime assignmentEnd = getFutureDateInput();
                    Console.WriteLine("How long will the duration last (in minutes: )");
                    double minutes = getDoubleGreaterThanOne();
                    Assignment newAssignment = new Assignment(newAssignmentName, newAssignmentDescription, assignmentEnd, minutes, targetedProject, assignmentPriority.Srednji);
                    Console.WriteLine("Set assignment priority: ");
                    
                    string priorityChoose = """
                    1. Visoki
                    2. Srednji
                    3. Niski
                    """;
                    Console.WriteLine(priorityChoose);
                    int prirorityInput = findMatchingNumber([1, 2, 3]);
                    switch (prirorityInput)
                    {
                        case 1:
                            newAssignment.Priority = assignmentPriority.Visoki;
                            break;
                        case 2:
                            newAssignment.Priority = assignmentPriority.Srednji;
                            break;
                        case 3:
                            newAssignment.Priority = assignmentPriority.Niski;
                            break;
                    }
                    database[targetedProject].Add(newAssignment);
                    Console.WriteLine("Assignment added successfully, press any key to move back to the start.");
                    Console.ReadKey();
                    start();


                    break;
                case 5:
                    if (targetedProject.status == projectStatus.Completed)
                    {
                        foreach (var item in database[targetedProject])
                        {
                            item.toCompleted();
                        }
                        Console.WriteLine("You are not allowed to modify a completed project!");
                        backToStart();

                    }
                    Console.WriteLine("Enter the name of assignment you are deleting: ");
                    string deleteAssignmentName = getNonNullStringInput();
                    Assignment targetDelete = null;
                    foreach (var item in database[targetedProject])
                    {
                        if (item.Name.ToLower().Trim() == deleteAssignmentName.ToLower().Trim())
                        {
                            Console.WriteLine("Assignment with that name in this project has been found, are you sure you want to delete it?");
                            targetDelete = item;
                            string yesOrNo = getYesNoInput();
                            switch (yesOrNo)
                            {
                                case "a":
                                    database[targetedProject].Remove(item);
                                    Console.WriteLine("Assignment deleted successfully, press any key to move back to the start.");
                                    Console.ReadKey();
                                    start();
                                    break;
                                case "b":
                                    Console.WriteLine("Aborting deletion, press any key to move back to the start.");
                                    Console.ReadKey();
                                    start();
                                    break;
                            }
                        }
                    }
                    break;
                case 6:
                    if (targetedProject.status == projectStatus.Completed)
                    {
                        foreach (var item in database[targetedProject])
                        {
                            item.toCompleted();
                        }
                        Console.WriteLine("You are not allowed to modify a completed project!");
                        backToStart();

                    }
                    double expectedMinutes = 0;
                    foreach (var item in database[targetedProject])
                    {
                        expectedMinutes += item.expectedDurationMinutes;
                    }
                    Console.WriteLine("Expected duration for all assignments in project (in minutes): " + expectedMinutes);
                    double hours = expectedMinutes / 60;
                    double remainingMinutes = expectedMinutes % 60;
                    Console.WriteLine($"Expected duration for all assignments in project (in hours : minutes): {hours} {remainingMinutes} ");
                    break;
                case 7:
                    var sortedAssignments = database[targetedProject].OrderBy(x => x.expectedDurationMinutes).ToList();
                    foreach (var item in sortedAssignments)
                    {
                        Console.WriteLine($"Expected duration in minutes: {item.expectedDurationMinutes} {item.Name} {item.Description} {item.Deadline}");
                    }
                    break;
                case 8:
                    var sortedByPriority = database[targetedProject].OrderBy(x => x.expectedDurationMinutes).ToList();
                    foreach (var item in sortedByPriority)
                    {
                        Console.WriteLine($"Priority: {item.Priority} {item.Name} {item.Description} {item.Deadline}");
                    }
                    break;
            }


        }
        static void manageAssignment()
        {
            Console.WriteLine("Enter new assignment name: ");
            string targetedAssignment = getNonNullStringInput();
            Assignment selectedAssignment = null;
            bool found = false;
            foreach (var item in database.Keys)
            {
                foreach (var assignment in database[item])
                {
                    if (assignment.Name.ToLower().Trim() == targetedAssignment.ToLower().Trim())
                    {
                        Console.WriteLine("Assignment found.");
                        selectedAssignment = assignment;
                        found = true;
                        break;
                    }
                    
                }
                if (found) break;

            }
            if (!found)
            {
                Console.WriteLine("Assignment with that name doesn't exist, taking you back to the start.");
                Console.ReadKey();
                start();
            }
            Project parentProject = null;
            foreach (var item in database.Keys)
            {
                if (item.Name.ToLower().Trim() == targetedAssignment.ToLower().Trim()) parentProject = item;
                
            }
            bool allDone = true;
            foreach (var item in database[parentProject])
            {
                if (item.Status != assignmentStatus.Completed)
                {
                    allDone = false;
                    break;
                }
            }
            if (parentProject.status == projectStatus.Completed)
            {
                foreach (var item in database[parentProject])
                {
                    item.Status = assignmentStatus.Completed;
                }
            }

            Console.Clear();
            string startMessage = """
                1. Prikaz detalja odabranog zadatka
                2. Uređivanje statusa zadatka
                """;
            Console.WriteLine(startMessage);
            int input = findMatchingNumber([1,2,3,4]);
            switch (input)
            {
                case 1:
                    Console.WriteLine($"{selectedAssignment.Name}  {selectedAssignment.Description}  {selectedAssignment.Priority} Deadline: {selectedAssignment.Deadline}  Parent-project: {selectedAssignment.parentProject.Name}  ");
                    backToStart();
                    break;
                case 2:
                    if (selectedAssignment.Status == assignmentStatus.Completed || allDone)
                    {
                        Console.WriteLine("You are not allowed to modify a completed assignment");
                        backToStart();
                    }
                    string changeStatus = """
                        1. Active
                        2. Delayed
                        3. Completed
                        """;
                    Console.WriteLine(changeStatus);
                    int assignStatus = findMatchingNumber([1,2,3]);
                    switch (assignStatus)
                    {
                        case 1:
                            selectedAssignment.Status = assignmentStatus.Active;
                            Console.WriteLine("Status changed successfully.");
                            backToStart();
                            break;
                        case 2:
                            selectedAssignment.Status = assignmentStatus.Delayed;
                            Console.WriteLine("Status changed successfully.");
                            backToStart();
                            break;
                        case 3:
                            selectedAssignment.Status = assignmentStatus.Completed;
                            Console.WriteLine("Status changed successfully.");
                            backToStart();
                            break;
                    }
                    break;
            
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
                    dueIN7Days();
                    break;
                case 5:
                    filterByStatus();
                    break;
                case 6:
                    manageProject();
                    break;
                case 7:
                    manageAssignment();
                    break;

            }


        }
   
        static void Main(string[] args)
        {
            
            DateTime timeCompleted = DateTime.Now;
            Project p1 = new Project("Kaufland", "Izgradnja kauflanda na poljičkoj", timeCompleted.AddMonths(2).Date);
            Assignment t1 = new Assignment("Garaža", "Završetak podzemne garaže za parking", timeCompleted.AddDays(14).Date, 10080, p1, assignmentPriority.Visoki);
            Assignment t2 = new Assignment("Police", "Instalacija svih polica u trgovini", timeCompleted.AddDays(7).Date, 5040, p1, assignmentPriority.Srednji);
            List<Assignment> list = new List<Assignment>();
            list.Add(t1);
            list.Add(t2);
            database.Add(p1, list);

            Project p2 = new Project("Poljud", "Renovacija stadiona", timeCompleted.AddMonths(7).Date);
            Assignment t11 = new Assignment("Stolice", "Zamjena slomljenih stolica", timeCompleted.AddDays(30).Date, 10080, p2,assignmentPriority.Niski);
            Assignment t22 = new Assignment("Kucice s hranom", "Bolje rasporediti kućice s hranom i pićem", timeCompleted.AddDays(21).Date, 5040, p2,assignmentPriority.Srednji);
            List<Assignment> lista = new List<Assignment>();
            lista.Add(t11);
            lista.Add(t22);
            database.Add(p2, lista);

            Project p3 = new Project("Lidl", "Proširenje trgovine na Poljičkoj", timeCompleted.AddMonths(3).Date);
            Assignment lidl1 = new Assignment("Lidl", "Proširenje trgovine na Poljičkoj", timeCompleted.AddMonths(3).Date,1000,p3, assignmentPriority.Visoki);
            p3.status = projectStatus.OnHold;
            List<Assignment> blanklist = new List<Assignment>();
            blanklist.Add(lidl1);

            database.Add(p3, blanklist);

            start();
            Console.ReadKey();
        }
    }
}