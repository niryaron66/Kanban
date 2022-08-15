// See https://aka.ms/new-console-template for more information
using BackendTests;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace BackendTests
{
    class Program
    {
        static void Main(string[] args)
        {
            GradingService g = new GradingService();
            DateTime d = DateTime.Now;
            g.LoadData();
            g.Register("mail@mail.com", "Password1");
            g.AddBoard("mail@mail.com", "board1");
            g.AddBoard("mail@mail.com", "board2");
            g.AddTask("mail@mail.com", "board1", "scary story", "once upon a time", d);
            g.AdvanceTask("mail@mail.com", "board1", 0, 0);
            g.AdvanceTask("mail@mail.com", "board1", 0, 1);
            g.AddTask("mail@mail.com", "board1", "love story", "once upon a time", d);
            g.AdvanceTask("mail@mail.com", "board2", 0, 1);
            g.AddTask("mail@mail.com", "board1", "action story", "once upon a time", d);
        }


    }
}