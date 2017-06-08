using System;
using GeneticAlgorithm.Base;
using GeneticAlgorithm.Samples.Chromosomes;

namespace GeneticAlgorithm
{
    public class BinaryMutation : MutationBase
    {
        private readonly Random _randomizer;
        public BinaryMutation()
        {
            _randomizer = new Random((int)DateTime.Now.ToBinary());
        }
        public override IChromosome Mutate(IChromosome chromosome)
        {
            var targetChromsome = ((BinaryChromosome) chromosome);
            if (targetChromsome.Type != ChromosomeType.Binary)
                throw new Exception("Invalid Chromosome Type for bit flipping.");

            var tempChromosome = targetChromsome;
            for (var i = 0; i < tempChromosome.Size(); i++)
                if (_randomizer.NextDouble() <= MutationRate)
                    tempChromosome.SetGene<byte>(i, ((byte)Math.Round(_randomizer.NextDouble())));
            tempChromosome.Recolor();
            return tempChromosome;
        }
        public override void Mutate(ref IChromosome chromosome)
        {
            var targetChromsome = ((BinaryChromosome)chromosome);
            if (targetChromsome.Type != ChromosomeType.Binary)
                throw new Exception("Invalid Chromosome Type for bit flipping.");

            for (var i = 0; i < targetChromsome.Size(); i++)
                if (_randomizer.NextDouble() <= MutationRate)
                    targetChromsome.SetGene<byte>(i, ((byte)Math.Round(_randomizer.NextDouble())));
            targetChromsome.Recolor();
        }
    }
}
