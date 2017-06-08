using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithm.Samples.Chromosomes;


namespace GeneticAlgorithm.Base
{
    public interface IPopulation
    {
        IChromosome GetFittest();
        int GetSize();
        IChromosome GetChromosome(int index);
        void SetChromosome<TC>(int index, TC chromosome) where TC : IChromosome;
    }
    public abstract class PopulationBase<T> : IPopulation where T : IEquatable<T>
    {
        public int InitialSize = 10;
        public bool IsInitialized { get; protected set; }
        public  IChromosome[] Chromosomes;
        public int PopulationSize { get { return _populationSize; } }
        private readonly int _populationSize;
        protected PopulationBase(int populationSize)
        {
            IsInitialized = false;
            _populationSize = populationSize;
            Chromosomes = new IChromosome[populationSize];
        }
        public abstract void Initialize(bool generateChromosomes);
        public void SetChromosome<TC>(int index, TC chromosome) where TC : IChromosome
        {
                Chromosomes[index] = chromosome;
        }
        public IChromosome GetChromosome(int index)
        {
            return Chromosomes[index];
        }
        public int GetSize()
        {
            return Chromosomes.Length;
        }
        public IChromosome GetFittest() 
        {
            var fittest = Chromosomes[0];
            foreach (var t in Chromosomes)
            {
                if (fittest.GetFitness() <= t.GetFitness())
                {
                    fittest = t;
                }
            }
            return fittest;
        }
    }
}
