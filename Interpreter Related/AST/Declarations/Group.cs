using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    class Block : IStatement
    {
        IEnumerable<IStatement> statements;
        public (int, int) CodeLocation  => throw new NotImplementedException();

        public Block(IEnumerable<IStatement> statements)
        {
            this.statements = statements;
        }

        public void RunIt()
        {
            foreach (var Statement in statements)
            {
                Statement.RunIt();
            }
        }

        public bool CheckSemantic(out List<string> errorsList)
        {
            string attention = "";
            errorsList = new List<string>();

            foreach (var Statement in statements)
            {
                try 
                {
                    if (!Statement.CheckSemantic(out List<string> temperrorsList))
                    {
                        errorsList.AddRange(temperrorsList);
                    }
                }
                catch (Attention a)
                {
                    attention += a.Message + "\n";
                }
            }

            if (attention != "") throw new Attention(attention);

            return errorsList.Count == 0;
        }
    }
}