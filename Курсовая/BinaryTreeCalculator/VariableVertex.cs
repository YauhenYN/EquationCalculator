using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationCalculator.BinaryTreeCalculator
{
    public class VariableVertex : Vertex
    {
        private VariableFormulaElement _variable;
        public VariableFormulaElement Variable { get => _variable; }
        public VariableVertex(VariableFormulaElement variable)
        {
            _variable = variable;
        }
    }
}
