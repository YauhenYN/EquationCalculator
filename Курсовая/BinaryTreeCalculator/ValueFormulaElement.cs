using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationCalculator.BinaryTreeCalculator
{
    public class ValueFormulaElement<T> : FormulaElement
    {
        private T _value;
        public T Value { get => _value; }
        public ValueFormulaElement(T value)
            :base(-1)
        {
            _value = value;
        }
    }
}
