using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationCalculator.BinaryTreeCalculator
{
    public abstract class FormulaBuilder<T> : IFormulaBuilder<T>
    {
        public abstract Formula Build(T value);

    }
}
