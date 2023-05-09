using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationCalculator.BinaryTreeCalculator
{
    class OperationVertex : Vertex
    {
        private Vertex _left;
        private Vertex _right;
        private OperationFormulaElement _operation;

        public Vertex Left { get => _left; }
        public Vertex Right { get => _right; }
        public OperationFormulaElement Operation { get => _operation; }
        public OperationVertex(OperationFormulaElement operation, Vertex leftVertex, Vertex rightVertex)
        {
            _operation = operation;
            _left = leftVertex;
            _right = rightVertex;
        }
    }
}
