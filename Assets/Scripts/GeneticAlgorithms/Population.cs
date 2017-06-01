using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeneticAlgorithm
{
    public class Population 
    {
        private readonly Individual[] _individuals;

        public Population(int populationSize,bool initialize)
        {
            _individuals = new Individual[populationSize];
            if (!initialize) return;
            for (var i = 0; i < populationSize; i++)
            {
                var newIndiv = new Individual();
                newIndiv.GenerateCode();
                _individuals[i] = newIndiv;
            }
        }

        public void SetIndividual(int index, Individual ind)
        {
            _individuals[index] = ind;
        }
        public int GetSize()
        {
            return _individuals.Length;
        } 
        public Individual GetIndividual(int index)
        {
            return _individuals[index];
        }

        public Individual GetFittest()
        {
            var fittest = _individuals[0];
            for (var i = 0; i < _individuals.Length; i++)
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