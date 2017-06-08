using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Base;
using GeneticAlgorithm.Samples.Chromosomes;
using GeneticAlgorithm.Samples.Populations;

namespace GeneticAlgorithm.Samples.Selection
{
    class TournamentSelection : SelectionBase
    {

        public override IChromosome Select<T>(IPopulation population)
        {
            // Create a tournament population
            var tournament = new BinaryPopulation(SelectionSize,false);
            // For each place in the tournament get a random individual
            for (var i = 0; i < SelectionSize; i++)
            {
                var randomId = (int)(UnityEngine.Random.value * population.GetSize());
                tournament.SetChromosome(i, population.GetChromosome(randomId));
            }
            // Get the fittest
            var fittest = tournament.GetFittest();
            return (T)fittest;
        }
    }
}
