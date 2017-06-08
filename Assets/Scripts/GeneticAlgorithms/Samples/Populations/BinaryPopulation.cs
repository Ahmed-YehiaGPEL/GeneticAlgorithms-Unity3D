using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Base;
using GeneticAlgorithm.Samples.Chromosomes;

namespace GeneticAlgorithm.Samples.Populations
{
    public class BinaryPopulation : PopulationBase<byte>
    {
        public BinaryPopulation(int populationSize, bool initialize) : base(populationSize)
        {
            Initialize(initialize);
        }
        public sealed override void Initialize(bool generateChromsomes)
        {
            Chromosomes = new IChromosome[PopulationSize];
            if (!generateChromsomes) return;
            for (var i = 0; i < PopulationSize; i++)
            {
                var chromosome = new BinaryChromosome(64);
                chromosome.GenerateRandomCode();
                Chromosomes[i] = (chromosome);
            }
            IsInitialized = true;
        }

    }
}
