using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectApp.Classes;
namespace DrugoPredavanje
{
    class Program
    {
        static void printAllProjects(Dictionary<Project, List<Assignment>> database) {
            foreach (var item in database.Keys)
            {
                Console.WriteLine($"Project name: {item.Name}");
                for (global::System.Int32 i = 0; i < database[item].Count; i++)
                {
                    Console.WriteLine($"         Assignment{i+1}: {database[item][i].Name}, {database[item][i].Description}, deadline: {database[item][i].Deadline.Date} ");
                }
            }
        }
        static void Main(string[] args)
        {
            Dictionary<Project,List<Assignment>> database = new Dictionary<Project, List<Assignment>>();
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

            printAllProjects(database);
            Console.ReadKey();
        }
    }
}