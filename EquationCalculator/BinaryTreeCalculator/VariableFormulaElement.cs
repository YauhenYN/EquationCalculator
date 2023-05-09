using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationCalculator.BinaryTreeCalculator
{
    public class VariableFormulaElement : FormulaElement
    {
        private string _variable;
        public string Variable { get => _variable; }
        public VariableFormulaElement(string variable)
            :base(-1)
        {
            _variable = variable;
        }
    }
}
