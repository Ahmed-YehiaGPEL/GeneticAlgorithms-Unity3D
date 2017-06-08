using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Base;
using GeneticAlgorithm.Samples.Chromosomes;

namespace GeneticAlgorithm.Samples.Fitness
{
    public class BinaryFitnessCalculator : FitnessBase
    {
        public IGene[] SolutionPatternGenes = new IGene[64];
        public BinaryFitnessCalculator(IChromosome solutionPattern)
        {
            SetSolution(solutionPattern);
        }

        public BinaryFitnessCalculator(IGene[] solutoinPatternGenes)
        {
            SetSolution(solutoinPatternGenes);
        }

        public void SetSolution(IGene[] solution)
        {
            SolutionPatternGenes = solution;
        }
        public override double CalculateFitness(IChromosome chromosome)
        {
            var targetChromosome = ((BinaryChromosome) SolutionPattern);
            var sourceChromosome = ((BinaryChromosome) chromosome);
            var solGene = targetChromosome.Genes;
            var tarGene = sourceChromosome.Genes;
            var fitness = 0;
            for (var i = 0; i < targetChromosome.DefaultGeneLength; i++)
            {
                if (Equals(solGene[i], tarGene[i]))
                    fitness++;
            }
            return fitness;
        }

        public override double GetPeakFitness()
        {
            return ((BinaryChromosome) SolutionPattern).DefaultGeneLength;
        }
    }
}
