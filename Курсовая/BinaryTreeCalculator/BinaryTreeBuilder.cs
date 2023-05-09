using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationCalculator.BinaryTreeCalculator
{
    public class BinaryTreeBuilder : TreeBuilder
    {
        public override BinaryTree Build(Formula formula)
        {
            var vertex = GetHighestVertex(formula.FormulaElements, formula.MaxPriority, 1, formula.FormulaElements.Count() - 1);
            return new BinaryTree(vertex);
        }
        private Vertex GetHighestVertex(FormulaElement[] elements, int priority, int startIndex, int endIndex)
        { //Возвращаем Vertex для определённого priority
            Queue<Vertex> vertexQueue = new Queue<Vertex>();
            Vertex mainVertex;
            Queue<OperationFormulaElement> operationsQueue = new Queue<OperationFormulaElement>();
            for (int step = startIndex; step <= endIndex; step += 2)
            {
                if (elements[step].Priority == priority)
                {
                    OperationFormulaElement leftElement;
                    vertexQueue.Enqueue(GetVertex(elements, step, out leftElement));
                    if (leftElement != null) operationsQueue.Enqueue(leftElement);
                    step += 2;
                }
            }
            if (vertexQueue.Count < 2) mainVertex = vertexQueue.Dequeue();
            else
            {
                Vertex leftVertex = vertexQueue.Dequeue();
                for (int step = 0; step < vertexQueue.Count(); step++)
                {
                    var operation = operationsQueue.Dequeue();
                    Vertex rigthVertex = vertexQueue.Dequeue();
                    leftVertex = new OperationVertex(operation, leftVertex, rigthVertex);
                }
                mainVertex = leftVertex;
            }
            return mainVertex;
        }
        private Vertex GetVertex(FormulaElement[] elements, int index, out OperationFormulaElement leftElement)
        {  //Возвращаем Vertex в обе стороны пока не встретиться более высший
            Vertex mainVertex = null;

            if ((index >= elements.Length - 2 || elements[index + 2].Priority <= elements[index].Priority) && //Что-бы также не заходил за пределы
                (index <= 2 || elements[index - 2].Priority <= elements[index].Priority))
            {
                mainVertex = new OperationVertex((OperationFormulaElement)elements[index],
                    new VariableVertex((VariableFormulaElement)elements[index - 1]), new VariableVertex((VariableFormulaElement)elements[index + 1]));
            }
            int leftEndIndex;
            int rightEndIndex;
            mainVertex = VertexFromQueues(mainVertex, TryMoveBackward(elements, index, out leftElement, out leftEndIndex),
                TryMoveForward(elements, index, out rightEndIndex));
            if (leftEndIndex != 1)
            {
                int nextPriority = elements[leftEndIndex - 2].Priority;
                int nextLeftIndex = 1;
                for (int step = leftEndIndex - 2; step > 2; step -= 2)
                {
                    if (elements[step - 2].Priority >= elements[step].Priority) //Идём до ближайшего элемента с высшим приоритетом
                    {
                        nextPriority = elements[step - 2].Priority;
                    }
                    else //Теперь ищем следующий низший приоритет
                    {
                        for (int inStep = step; inStep > 2; inStep -= 2) if (elements[inStep - 2].Priority > elements[inStep].Priority)
                            {
                                nextLeftIndex = inStep;
                                break;
                            }
                        break;
                    }
                }
                if (nextPriority < elements[index].Priority)
                {
                    mainVertex = new OperationVertex(leftElement, GetHighestVertex(elements, nextPriority, nextLeftIndex, leftEndIndex - 2), mainVertex);
                    leftElement = null;
                }
            }
            if (rightEndIndex != elements.Length - 2)
            {
                int nextPriority = elements[rightEndIndex + 2].Priority;
                int nextRightIndex = elements.Length - 2;
                for (int step = rightEndIndex + 2; step < elements.Length - 2; step += 2)
                {
                    if (elements[step + 2].Priority >= elements[step].Priority) //Идём до ближайшего элемента с высшим приоритетом
                    {
                        nextPriority = elements[step + 2].Priority;
                    }
                    else //Теперь ищем следующий низший приоритет
                    {
                        for (int inStep = step; inStep < elements.Length - 2; inStep += 2) if (elements[inStep + 2].Priority > elements[inStep].Priority)
                            {
                                nextRightIndex = inStep - 2;
                                break;
                            }
                        break;
                    }
                }
                if (nextPriority < elements[index].Priority)
                {

                    mainVertex = new OperationVertex((OperationFormulaElement)elements[rightEndIndex + 2], mainVertex, GetHighestVertex(elements, nextPriority, rightEndIndex + 2, nextRightIndex));
                    leftElement = null;
                }
            }
            return mainVertex;
        }
        private Queue<FormulaElement> TryMoveBackward(FormulaElement[] elements, int index, out OperationFormulaElement leftElement, out int endIndex) //Проверено
        //Просто добавляем все элементы до высшего приоритета и возвращаем,а если попадается высший приоритет, возвращаем и его
        {
            Queue<FormulaElement> queue = new Queue<FormulaElement>();
            leftElement = null;
            int step = index;
            for (; step > 2; step -= 2)
            {
                if (elements[step].Priority >= elements[step - 2].Priority &&
                    (step == 3 || elements[step - 2].Priority >= elements[step - 4].Priority))
                {
                    queue.Enqueue(elements[step - 2]);
                    queue.Enqueue(elements[step - 3]);
                }
                else
                {
                    step -= 2;
                    leftElement = (OperationFormulaElement)elements[step];
                    break;
                }
            }
            endIndex = step;
            return queue;
        }
        private Queue<FormulaElement> TryMoveForward(FormulaElement[] elements, int index, out int endIndex) //Проверено
        //Просто добавляем все элементы до высшего приоритета и возвращаем
        {
            Queue<FormulaElement> queue = new Queue<FormulaElement>();
            int step = index;
            for (; step < elements.Length - 2; step += 2)
            {
                if (elements[step].Priority >= elements[step + 2].Priority &&
                    (step == elements.Length - 4 || elements[step + 2].Priority >= elements[step + 4].Priority))
                {
                    queue.Enqueue(elements[step + 2]);
                    queue.Enqueue(elements[step + 3]);
                }
                else break;
            }
            endIndex = step;
            return queue;
        }
        private Vertex VertexFromQueues(Vertex mainVertex, Queue<FormulaElement> leftQueue, Queue<FormulaElement> rightQueue)
        {
            var vertex = mainVertex;
            int lastLeftPriority = 0;
            int lastRightPriority = 0;
            while (leftQueue.Count > 0 || rightQueue.Count > 0)
            {
                FormulaElement leftOperation = null;
                FormulaElement rightOperation = null;
                if (leftQueue.Count > 0 && !(rightQueue.Count > 0 && rightQueue.Peek().Priority == lastRightPriority))
                {
                    leftOperation = leftQueue.Dequeue();
                    lastLeftPriority = leftOperation.Priority;
                }
                if (rightQueue.Count > 0 && !(leftQueue.Count > 0 && leftQueue.Peek().Priority == lastLeftPriority))
                {
                    rightOperation = rightQueue.Dequeue();
                    lastRightPriority = rightOperation.Priority;
                }
                if (leftOperation != null && rightOperation != null)
                {
                    if (rightOperation.Priority > leftOperation.Priority)
                    { //ПРАВАЯ, ПОТОМ ЛЕВАЯ
                        vertex = new OperationVertex((OperationFormulaElement)rightOperation, vertex, new VariableVertex((VariableFormulaElement)rightQueue.Dequeue()));
                        vertex = new OperationVertex((OperationFormulaElement)leftOperation, new VariableVertex((VariableFormulaElement)leftQueue.Dequeue()), vertex);
                    }
                    else
                    { //СНАЧАЛА ЛЕВАЯ, ПОТОМ ПРАВАЯ
                        vertex = new OperationVertex((OperationFormulaElement)leftOperation, new VariableVertex((VariableFormulaElement)leftQueue.Dequeue()), vertex);
                        vertex = new OperationVertex((OperationFormulaElement)rightOperation, vertex, new VariableVertex((VariableFormulaElement)rightQueue.Dequeue()));
                    }
                }
                else if (leftOperation != null)
                {
                    vertex = new OperationVertex((OperationFormulaElement)leftOperation, new VariableVertex((VariableFormulaElement)leftQueue.Dequeue()), vertex);
                }
                else if (rightOperation != null)
                {
                    vertex = new OperationVertex((OperationFormulaElement)rightOperation, vertex, new VariableVertex((VariableFormulaElement)rightQueue.Dequeue()));
                }
            }
            return vertex;
        }
    }
}
