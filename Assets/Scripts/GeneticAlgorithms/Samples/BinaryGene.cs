using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Base;

namespace GeneticAlgorithm.Samples
{
    public class BinaryGene : GeneBase<byte>
    {
        public BinaryGene(byte value)
        {
            this.Value = value;
        }

        public static implicit operator BinaryGene(byte value)
        {
            return new BinaryGene(value);
        }
    }
}
