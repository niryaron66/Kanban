using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
namespace Frontend.Model
{
    public class TaskModel
    {
        public string Title;
        public string CreationTime;
        public string Description;
        public string DueDate;
        public string Id;
        public TaskModel(FTask fTask)
        {

            Title = fTask.Title;
            CreationTime = fTask.CreationTime.ToString();
            Description = fTask.Description;
            DueDate = fTask.DueDate.ToString();
            Id = fTask.Id.ToString();

            /*JObject jObject = JObject.Parse(Json);
            Id = jObject["Id"].ToString();
            Title = jObject["Title"].ToString();
            CreationTime = jObject["CreationTime"].ToString();
            Description = jObject["Description"].ToString();
            DueDate = jObject["DueDate"].ToString();*/
        }
        public TaskModel(string title, string desc, string dueDate, string assignee)
        {
            Title = title;
            Description = desc;
            DueDate = dueDate;
        }
        public override string ToString()
        {
            return $"ID: {Id}\n\n" +
                $"Title: {Title}\n\n" +
                $"Description: {Description}\n\n" +
                $"CreationTime: {CreationTime}\n\n" +
                $"DueDate: {DueDate}.";
        }
        public string FullString()
        {
            return $"ID: {Id}\n\n" +
                $"Title: {Title}\n\n" +
                $"Description: {Description}\n\n" +
                $"CreationTime: {CreationTime}\n\n" +
                $"DueDate: {DueDate}.";
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
