using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationCalculator.BinaryTreeCalculator
{
    static class Helpers
    {
        public static bool IsParanthesis(this char value)
        {
            return value == (char)Paranthesis.Closing || value == (char)Paranthesis.Opening;
        }
        public static bool IsOpeningParanthesis(this char value)
        {
            return value == (char)Paranthesis.Opening;
        }
        public static bool IsClosingParanthesis(this char value)
        {
            return value == (char)Paranthesis.Closing;
        }
        public static bool IsVariableVertex(this Vertex vertex)
        {
            return typeof(VariableVertex) == vertex.GetType();
        }
        public static bool IsOperationVertex(this Vertex vertex)
        {
            return typeof(OperationVertex) == vertex.GetType();
        }
        public static VariableVertex ToVariableVertex(this Vertex vertex) => (VariableVertex)vertex;
        public static OperationVertex ToOperationVertex(this Vertex vertex) => (OperationVertex)vertex;
    }
}
