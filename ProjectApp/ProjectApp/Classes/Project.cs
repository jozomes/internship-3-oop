using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectApp.Enums;
namespace ProjectApp.Classes
{
    public class Project
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime startDate { get; }
        public DateTime endDate { get; set; }
        public projectStatus status { get; set; }
        public Guid id { get;}
        public Project(string name, string desc, DateTime end) {
            Name = name;
            Description = desc;
            startDate = DateTime.Now;
            endDate = end;
            status = projectStatus.Active; //it's intuitive that a new project is set as active
            id = Guid.NewGuid();
        }

        public void toActive(){
            this.status = projectStatus.Active;
        }
        public void toOnHold()
        {
            this.status = projectStatus.OnHold;
        }
        public void toCompleted()
        {
            this.status = projectStatus.Completed;
        }

    }
}
