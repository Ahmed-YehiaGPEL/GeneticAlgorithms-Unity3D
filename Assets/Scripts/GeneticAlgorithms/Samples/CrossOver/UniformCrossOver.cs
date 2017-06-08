using System;
using System.Net;
using GeneticAlgorithm.Base;
using GeneticAlgorithm.Samples.Chromosomes;

namespace GeneticAlgorithm
{
    public class UniformCrossOver : CrossOverBase
    {
        private readonly Random _randomizer;
        public double UniformRate
        {
            get { return _uniformRate; }
            set { _uniformRate = value; }
        }
        protected double _uniformRate = .5;
        public UniformCrossOver()
        {
            _randomizer = new Random((int)DateTime.Now.ToBinary());
        }
        public override void Crossover(IChromosome i1, IChromosome i2,ref IChromosome result)
        {
            for (var i = 0; i < i1.Size(); i++)
            {
                result.SetGene(i, _randomizer.NextDouble() <= UniformRate ? i1.GetGene(i) : i2.GetGene(i));
            }
        }
        public override void Crossover<T>(ref IChromosome i1, ref IChromosome i2)
        {
            for (var i = 0; i < i1.Size(); i++)
            {
                var threshold = _randomizer.NextDouble();
                if (!(threshold <= UniformRate)) continue;
                //Cross
                i1.SetGene<T>(i, i2.GetGene(i));
                i2.SetGene<T>(i, i1.GetGene(i));
            }
        }
    }
}
