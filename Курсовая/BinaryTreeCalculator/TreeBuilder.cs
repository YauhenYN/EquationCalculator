using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationCalculator.BinaryTreeCalculator
{
    public abstract class TreeBuilder : ITreeBuilder
    {
        public abstract Tree Build(Formula formula);
    }
}
