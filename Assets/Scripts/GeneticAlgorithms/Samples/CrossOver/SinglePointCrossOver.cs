
using System;
using GeneticAlgorithm.Base;

namespace GeneticAlgorithm
{
    public class SinglePointCrossOver : CrossOverBase
    {
        private readonly Random _randomizer;
        public SinglePointCrossOver()
        {
            _randomizer = new Random((int)DateTime.Now.ToBinary());
        }
        public override void Crossover(IChromosome i1, IChromosome i2, ref IChromosome result)
        {
            var crossOverPoint = _randomizer.Next(0, i1.Size());
            for (var i = 0; i < crossOverPoint; i++)
            {
                result.SetGene(i, i1.GetGene(i));
            }
            for (var i = crossOverPoint; i < i1.Size(); i++)
            {
                result.SetGene(i, i2.GetGene(i));
            }
        }

        public override void Crossover<T>(ref IChromosome i1, ref IChromosome i2)
        {
            var crossOverPoint = _randomizer.Next(0, i1.Size());
            for (var i = crossOverPoint; i < i1.Size(); i++)
            {
                i1.SetGene<T>(i, i2.GetGene(i));
                i2.SetGene<T>(i, i1.GetGene(i));
            }
        }
    }
}
