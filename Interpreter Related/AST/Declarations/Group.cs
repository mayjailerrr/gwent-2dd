using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    class Block : IStatement
    {
        IEnumerable<IStatement> Statements;
       // CodeLocation location;

        public CodeLocation Location => throw new NotImplementedException();

        public Block(IEnumerable<IStatement> Statements)
        {
            this.Statements = Statements;
           // this.location = location;
        }

        public void RunIt()
        {
            foreach (var Statement in Statements)
            {
                Statement.RunIt();
            }
        }

        public bool CheckSemantic(out List<string> errorsList)
        {
            string attention = "";
            errorsList = new List<string>();

            foreach (var Statement in Statements)
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