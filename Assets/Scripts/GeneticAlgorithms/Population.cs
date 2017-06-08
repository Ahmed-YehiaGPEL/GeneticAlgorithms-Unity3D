using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeneticAlgorithm
{
    public class Population 
    {
        private readonly Chromosome[] _chromosomes;

        public Population(int populationSize,bool initialize)
        {
            _chromosomes = new Chromosome[populationSize];
            if (!initialize) return;
            for (var i = 0; i < populationSize; i++)
            {
                var newIndiv = new Chromosome();
                newIndiv.GenerateCode();
                _chromosomes[i] = newIndiv;
            }
        }

        public void SetIndividual(int index, Chromosome ind)
        {
            _chromosomes[index] = ind;
        }
        public int GetSize()
        {
            return _chromosomes.Length;
        } 
        public Chromosome GetIndividual(int index)
        {
            return _chromosomes[index];
        }

        public Chromosome GetFittest()
        {
            var fittest = _chromosomes[0];
            for (var i = 0; i < _chromosomes.Length; i++)
            {
                if (fittest.GetFitness() <= GetIndividual(i).GetFitness())
                {
                    fittest = GetIndividual(i);
                }
            }
            return fittest;
        }
        
    }
}