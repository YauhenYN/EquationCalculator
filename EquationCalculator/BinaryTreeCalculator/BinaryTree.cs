using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationCalculator.BinaryTreeCalculator
{
    public class BinaryTree : Tree
    {
        private Vertex _highestVertex;
        public Vertex HighestVertex { get => _highestVertex; }
        public BinaryTree(Vertex highestVertex)
        {
            _highestVertex = highestVertex;
        }
    }
}
