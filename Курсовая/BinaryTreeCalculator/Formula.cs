using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationCalculator.BinaryTreeCalculator
{
    public class Formula
    {
        private FormulaElement[] _formulaElements;
        private string _formulaInString;
        private readonly int _maxPriority;
        private readonly int _minPriority;
        public FormulaElement[] FormulaElements { get => _formulaElements; }
        public string FormulaInString { get => _formulaInString; }
        public int MaxPriority { get => _maxPriority; }
        public int MinPriority { get => _minPriority; }
        public int Length { get => _formulaElements.Length; }
        public Formula(FormulaElement[] formulaElements, string formula)
        {
            _formulaElements = formulaElements;
            _formulaInString = formula;
            FormulaElement[] operations = formulaElements.Where(el => el.GetType() == typeof(OperationFormulaElement)).ToArray();
            _maxPriority = operations.Max(el => el.Priority);
            _minPriority = operations.Min(el => el.Priority);
        }
    }
}
