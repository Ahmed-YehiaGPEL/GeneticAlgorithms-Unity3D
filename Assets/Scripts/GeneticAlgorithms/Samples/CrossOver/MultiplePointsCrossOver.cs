using System;
using GeneticAlgorithm.Base;

namespace GeneticAlgorithm
{
    public class MultiplePointsCrossOver : CrossOverBase
    {
        private readonly Random _randomizer;
        public MultiplePointsCrossOver()
        {
            _randomizer = new Random((int)DateTime.Now.ToBinary());
        }


        public override void Crossover(IChromosome i1, IChromosome i2, ref IChromosome result)
        {
            var p1 = _randomizer.Next(0, i1.Size() / 2);
            var p2 = _randomizer.Next(p1, i1.Size());
            for (var i = 0; i < i1.Size(); i++)
            {
                result.SetGene(i, i1.GetGene(i));
                if (i == p1 || i <= p2 && i >= p1)
                    result.SetGene(i, i2.GetGene(i));
                else if (i == p2 || i >= p2 && i < i1.Size())
                    result.SetGene(i, i2.GetGene(i));
            }
        }

        public override void Crossover<T>(ref IChromosome i1, ref IChromosome i2)
        {
            var p1 = _randomizer.Next(0, i1.Size() / 2);
            var p2 = _randomizer.Next(p1, i1.Size());

            for (var i = p1; i < p2; i++)
            {
                i1.SetGene<T>(i, i2.GetGene(i));
                i2.SetGene<T>(i, i1.GetGene(i));
            }
            for (var i = p2; i < i1.Size(); i++)
            {
                i1.SetGene<T>(i, i2.GetGene(i));
                i2.SetGene<T>(i, i1.GetGene(i));
            }
        }
    }
}
