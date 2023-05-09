using EquationCalculator.BinaryTreeCalculator;
using System;
using System.Linq;

namespace EquationCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var formulaBuilder = new BinaryTreeFormulaBuilder();
            var binaryTreeBuilder = new BinaryTreeBuilder();
            BinaryTree binaryTree = null;
            BinaryTreeSolver solver = null;
            while (true)
            {
                try
                {
                    Console.WriteLine("Выберите действие: \n" +
                        (binaryTree != null && solver != null ? "1. Решение уравнений через бинарное дерево (дерево выражений)\n" : "") +
                        "2. Ввести параметры\n3. Ввести формулу\n4. Выход");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            if(binaryTree != null && solver != null) Console.WriteLine("Итог: " + solver.EvaluateBinaryTree(binaryTree));
                            goto default;
                        case "2":
                            Console.WriteLine("Пожалуйста, введите планируемые параметры и значения которые будут использоваться в формате \"a=1 bbb=2 abc=5 t=4\"");
                            var values = Console.ReadLine().Split(" ").Select(el =>
                            {
                                var splitted = el.Split("=");
                                return (splitted[0], double.Parse(splitted[1]));
                            }).ToArray();
                            solver = new BinaryTreeSolver(values);
                            break;
                        case "3":
                            Console.WriteLine("Введите формулу, параменты можно задавать в качестве строки либо символа, пробелы не важны");
                            var formula = formulaBuilder.Build(Console.ReadLine());
                            binaryTree = binaryTreeBuilder.Build(formula);
                            break;
                        case "4":
                            return;
                        default:
                            Console.WriteLine("Пожалуйста, попробуйте ещё раз");
                            break;
                    }
                }
                catch (ArgumentException e)
                {
                    if (e.Message == "Couldn't read the provided formula") Console.WriteLine("Невозможно прочитать формулу, попробуйте ещё раз");
                }
                catch
                {
                    Console.WriteLine("Что-то пошло не так, попробуйте ещё раз");
                }
            }
        }
    }
}
