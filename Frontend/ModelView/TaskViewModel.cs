using Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ModelView
{
    public class TaskViewModel
    {
        public TaskModel Task { get; set; }
        public string TaskDescription;
        public TaskViewModel(TaskModel task)
        {
            Task = task;
            TaskDescription = task.FullString();
        }
    }
}
