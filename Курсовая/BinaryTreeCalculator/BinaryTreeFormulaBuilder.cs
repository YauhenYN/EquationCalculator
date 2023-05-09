using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationCalculator.BinaryTreeCalculator
{
    public sealed class BinaryTreeFormulaBuilder : FormulaBuilder<string>
    {
        private const int _higherOperationCoef = 3;
        private const int _usualOperationCoef = 2;
        private const int _paranthesisCoef = 2;
        public override Formula Build(string formula)
        {
            var values = GetValuesFromString(formula);
            return new Formula(values, formula);
        }
        private FormulaElement[] GetValuesFromString(string formula)
        {
            formula = formula.Replace(" ", ""); 
            var values = new List<FormulaElement>();
            int priority = 0;
            string variable = "";
            for (int step = 0; step < formula.Length; step++)
            {
                if (Char.IsLetter(formula[step]))
                {
                    variable += formula[step];
                    continue;
                }
                else
                {
                    if (variable.Length > 0) values.Add(new VariableFormulaElement(variable));
                    variable = "";
                    //then
                    if (formula[step].IsParanthesis())
                    {
                        if (formula[step].IsOpeningParanthesis()) priority = priority + _paranthesisCoef;
                        else if (formula[step].IsClosingParanthesis()) priority = priority - _paranthesisCoef;
                        continue;
                    }
                    OperationFormulaElement operationValue;
                    if (TryParse(formula[step], out operationValue, priority)) values.Add(operationValue);
                    else throw new ArgumentException("Couldn't read the provided formula");
                }
            }
            if (variable.Length > 0) values.Add(new VariableFormulaElement(variable));
            return values.ToArray();
        }
        private bool TryParse(char value, out OperationFormulaElement operationFormulaElement, int priority)
        {
            switch (value)
            {
                case (char)Operations.Divide:
                    operationFormulaElement = new OperationFormulaElement(Operations.Divide, priority + _higherOperationCoef);
                    return true;
                case (char)Operations.Multiply:
                    operationFormulaElement = new OperationFormulaElement(Operations.Multiply, priority + _higherOperationCoef);
                    return true;
                case (char)Operations.Plus:
                    operationFormulaElement = new OperationFormulaElement(Operations.Plus, priority + _usualOperationCoef);
                    return true;
                case (char)Operations.Minus:
                    operationFormulaElement = new OperationFormulaElement(Operations.Minus, priority + _usualOperationCoef);
                    return true;
                default:
                    operationFormulaElement = null;
                    return false;
            }
        }
    }
}
