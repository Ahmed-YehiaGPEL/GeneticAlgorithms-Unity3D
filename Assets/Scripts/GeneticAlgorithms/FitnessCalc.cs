using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using GeneticAlgorithm;
using UnityEngine;

namespace GeneticAlgorithm
{



    public class FitnessCalc
    {
        private static byte[] _solution = new byte[Individual.GetGeneLength()];
        private static int defaultGeneLength;

        public FitnessCalc()
        {
            defaultGeneLength = Individual.GetGeneLength();
        }
        public static void SetSolution(string newSolution)
        {
            _solution = new byte[newSolution.Length];

            for (var i = 0; i < newSolution.Length; i++)
            {
                if (newSolution[i] == '0')
                    _solution[i] = 0;
                else if (newSolution[i] == '1')
                    _solution[i] = 1;
                else
                    _solution[i] = 0;
            }
        }

        public static int CalculateFitness(Individual individual)
        {
            var fitness = 0;
            for (var i = 0; i < _solution.Length; i++)
            {
                if (individual.GetGene(i) == _solution[i])
                {
                    fitness++;
                }
            }
            return fitness;
        }

        public static int GetMaxFitness()
        {
            return _solution.Length;
        }
    }

}