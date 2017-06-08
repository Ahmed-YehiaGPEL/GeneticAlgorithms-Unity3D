using System;

namespace GeneticAlgorithm.Base
{
    public abstract class CrossOverBase
    {

        public abstract void Crossover(IChromosome i1, IChromosome i2, ref IChromosome result);
        public abstract void Crossover<T>(ref IChromosome i1, ref IChromosome i2) where T : IEquatable<T>; 
    }
}