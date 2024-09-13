using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;

namespace Interpreterr
{
    class ASTPrinter
    {
        TMP_Text terminal; 

        public ASTPrinter(TMP_Text terminal)
        {
            this.terminal = terminal;
            Clear();
        }

        public void Print(string message)
        {
            string[] messages = message.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in messages)
            {
                terminal.text += item + '\n';
            }
        }

        public void Clear() => terminal.text = "";
    }
}
