using System;

namespace Interpreterr
{
    class ASTPrinter
    {
        public ASTPrinter()
        {
            Clear();
        }

        public void Print(string message)
        {
            string[] messages = message.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in messages)
            {
                Console.WriteLine(item);
            }
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
