using Models;
using System;
using System.Collections.Generic;

namespace ChatWithLikes
{
    class Program
    {
        static void Main(string[] args)
        {
            ChatService chatService = new ChatService();
            var messages = chatService.GetAllHierarchicaly();
            foreach (var m in messages)
                Print(m);
        }

        static void Print(Message mes, int space = 0)
        {
            int indent = space;

            Console.WriteLine($"{new String (' ', 4 * indent)} {mes.Time}");
            Console.WriteLine($"{new String(' ', 4 * indent)} {mes.Author}");
            Console.WriteLine($"{new String(' ', 4 * indent)} {mes.Content}");
            Console.WriteLine();
            foreach (var ans in mes.Answers)
                Print(ans, indent + 1);
        }
    }
}
