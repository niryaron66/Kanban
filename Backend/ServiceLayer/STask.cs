using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer;
using Task = IntroSE.Kanban.Backend.BusinessLayer.Task;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class STask
    {

        public int Id { get; set; }
        public DateTime CreationTime { get; set;}
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }

        public STask(Task task)
        {
            this.Description = task.description;
            this.Id = task.id;
            this.CreationTime = task.creationTime;
            this.DueDate = task.dueDate;
            this.Title = task.title;
        }

        [JsonConstructor]
        public STask(int Id, DateTime CreationTime, string Title, string Description, DateTime DueDate)
        {
            this.Id = Id;
            this.CreationTime = CreationTime;
            this.Title = Title;
            this.Description = Description; 
            this.DueDate = DueDate;
        }
    }
}
/// The structure of the JSON of a Task, is:
/// <code>
/// {
///     "Id": &lt;int&gt;,
///     "CreationTime": &lt;DateTime&gt;,
///     "Title": &lt;string&gt;,
///     "Description": &lt;string&gt;,
///     "DueDate": &lt;DateTime&gt;
/// }