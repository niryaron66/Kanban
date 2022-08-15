using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTests
{
    public class Task
    {
        private int taskId;
        private DateTime creationTime;
        private string title;
        private string description;
        private DateTime dueDate;

        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
        public DateTime DueDate { get => dueDate; set => dueDate = value; }
        public DateTime CreationTime { get => creationTime; set => creationTime = value; }
        public int TaskId { get => taskId; set => taskId = value; }
    }
}
