using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationCalculator.BinaryTreeCalculator
{
    public class OperationFormulaElement : FormulaElement
    {
        private Operations _operation;
        public Operations Operation { get => _operation; }
        public OperationFormulaElement(Operations operation, int priority)
            :base(priority)
        {
            _operation = operation;
        }
    }
}
