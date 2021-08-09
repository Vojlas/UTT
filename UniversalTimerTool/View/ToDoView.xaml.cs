using System.Windows;
using UniversalTimerTool.Model;
using UniversalTimerTool.View.UserControlls;

namespace UniversalTimerTool.View
{
    /// <summary>
    /// Interakční logika pro ToDoView.xaml
    /// </summary>
    public partial class ToDoView : Window
    {
        public ToDoList ToDoList { get;set;}
        public ToDoView(ToDoList toDoList)
        {
            InitializeComponent();
            this.ToDoList = toDoList;
            if (this.ToDoList == null) { this.ToDoList = new ToDoList("name:true"); }
            Rerender();
        }

        public void Rerender() {
            this.StackPanelToDo.Children.Clear();

            foreach (ToDo task in this.ToDoList.List.Values)
            {
                ToDoControll control = new ToDoControll();
                    control.PlaceHolder = task.Name;
                    control.Status = task.Done;
                this.StackPanelToDo.Children.Add(control);
                control = null;
            }
        }

        public void Rerender(ToDoList toDoList)
        {
            this.ToDoList = toDoList;
            if (this.ToDoList == null) { this.ToDoList = new ToDoList("name:true"); }
            Rerender();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            newToDo newToDo = new newToDo();
            newToDo.ShowDialog();
            ToDo Todo = new ToDo();
            Todo.Done = false;
            Todo.Name = newToDo.name;
            this.ToDoList.addTaskToList(Todo);
            Rerender();
        }
    }
}
