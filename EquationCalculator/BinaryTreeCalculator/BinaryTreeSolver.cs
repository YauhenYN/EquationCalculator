using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationCalculator.BinaryTreeCalculator
{
    public class BinaryTreeSolver
    {
        private Dictionary<string, double> _values;
        
        public BinaryTreeSolver(params (string, double)[] values)
        {
            _values = new Dictionary<string, double>();
            foreach (var tuple in values) _values.Add(tuple.Item1, tuple.Item2);
        }
        public double EvaluateBinaryTree(BinaryTree tree)
        {
            return Evaluate(tree.HighestVertex);
        }
        private double Evaluate(Vertex root)
        {

            if (root == null) return 0;

            // Leaf node i.e, an integer
            else if (root.IsVariableVertex()) return _values[root.ToVariableVertex().Variable.Variable];
            else
            {
                var asOperationVertex = root.ToOperationVertex();
                double leftValue = Evaluate(asOperationVertex.Left);
                double rightValue = Evaluate(asOperationVertex.Right);
                var operation = asOperationVertex.Operation.Operation;
                if (operation == Operations.Plus) return leftValue + rightValue;
                else if (operation == Operations.Minus) return leftValue - rightValue;
                else if (operation == Operations.Multiply) return leftValue * rightValue;
                else return leftValue / rightValue;
            }
        }
    }
}
