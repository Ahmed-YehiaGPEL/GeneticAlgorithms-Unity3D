using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithm.Base
{
    public interface IFitness
    {
        double CalculateFitness(IChromosome chromosome);// where T : IConvertible;
    }
    public abstract class FitnessBase : IFitness
    {
        public  bool IsFinite { get {return _isFinite;} }
        private bool _isFinite = true;  
        public IChromosome SolutionPattern;
        
        public void Init(IChromosome chromosome)
        {
            SolutionPattern = chromosome;
        }
        public abstract double GetPeakFitness();

        public void SetSolution(IChromosome solution)
        {
            _isFinite = solution == null;
            SolutionPattern = solution;
        }

        public abstract double CalculateFitness(IChromosome chromosome);//where T1 : IConvertible;
    }
}
