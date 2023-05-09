using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationCalculator.BinaryTreeCalculator
{
    public abstract class FormulaElement
    {
        private int _priority;
        public int Priority { get => _priority; }
        public FormulaElement(int priority)
        {
            _priority = priority;
        }
    }
}
