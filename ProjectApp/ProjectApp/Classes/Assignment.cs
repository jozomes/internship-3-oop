using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectApp.Enums;

namespace ProjectApp.Classes
{
    public class Assignment
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime Deadline { get; set; }
        public assignmentStatus Status { get; set; }
        public assignmentPriority Priority { get; set; }
        public double expectedDurationMinutes { get; set; }
        public Project parentProject { get; set; }
        public Guid id { get;}
        public Assignment(string name, string desc, DateTime deadlineDate, double minutes, Project project, assignmentPriority priority)
        {
            Name = name;
            Description = desc;
            Deadline = deadlineDate;
            Status = assignmentStatus.Active;
            expectedDurationMinutes = minutes;
            parentProject = project;
            Priority = priority;
            id = Guid.NewGuid();
        }
        public void toActive()
        {
            this.Status = assignmentStatus.Active;
        }
        public void toDelayed()
        {
            this.Status = assignmentStatus.Delayed;
        }
        public void toCompleted()
        {
            this.Status = assignmentStatus.Completed;
        }
    }
}
