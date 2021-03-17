using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UniversalTimerTool.Model
{
    public class ToDoList
    { //ToDo List format bool:name|bool:name|...
        Dictionary<string, ToDo> List { get; set; }
        public ToDoList() { this.List = new Dictionary<string, ToDo>(); }
        public ToDoList(string savedList) {
            this.List = new Dictionary<string, ToDo>();
            string[] toDos = savedList.Split('|');
            foreach (string toDo in toDos)
            {
                string[] x = toDo.Split(':');
                this.addTaskToList(x[0], Convert.ToBoolean(x[1]));
            }
        }

        public ToDo getTask(string unigueID) {
            if (List.ContainsKey(unigueID)) return List[unigueID];
            return null;
        }

        public void addTaskToList(ToDo task)
        {
            this.List.Add(computeSha256Hash(task.Name), task);
        }
        public void addTaskToList(String name, bool status = false)
        {
            this.List.Add(computeSha256Hash(name), new ToDo(name, status));
        }
        public void removeTaskfromList(string unigueID) {
            if (List.ContainsKey(unigueID)) return;
            this.List.Remove(unigueID);
        }
        public void changeStatus(string unigueID) {
            if (List.ContainsKey(unigueID)) return;
            this.List[unigueID].Done = !this.List[unigueID].Done;
        }

        public override string ToString() {
            if (this.List.Count == 0) return "";
            string output ="";
            foreach (ToDo task in this.List.Values)
            {
                output += task.Name + ":" + task.Done + "|";
            }
            return output.Remove(output.Length - 1, 1);
        }

        private string computeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }

    public class ToDo
    { 
        public bool Done { get; set; }
        public string Name { get; set; }

        public ToDo()
        {

        }
        public ToDo(string _name, bool _done = false) {
            this.Done = _done;
            this.Name = _name;
        }
    }
}
