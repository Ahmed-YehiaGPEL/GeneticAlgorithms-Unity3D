using System.Collections.Generic;
using UnityEngine;

namespace GeneticAlgorithm.Base
{
    public interface IAlgorithmDriver
    {
        IPopulation Evolve(IPopulation population);
        void Update();
        void Initialize();
        void Awake();
        void Start();
        void Add(IChromosome chromosome);
        IFitness GetFitnessCalculator();
    }
    public abstract class AlgorithmDriverBase : MonoBehaviour, IAlgorithmDriver
    {
        public CrossOverBase CrossoverController;
        public MutationBase MutationController;
        public SelectionBase SelectionBaseController;
        public FitnessBase FitnessCalculator;
        public List<IChromosome> ChromosomesCollection;
        public bool IsInitialized { get; protected set; }
        protected AlgorithmDriverBase()
        {
            IsInitialized = false;
            ChromosomesCollection = new List<IChromosome>();
        }
        public abstract IPopulation Evolve(IPopulation population);
        public abstract void Update();
        public abstract void Awake();
        public abstract void Start();
        public abstract void Initialize();
        public IFitness GetFitnessCalculator() { return FitnessCalculator;}
        public void Add(IChromosome chromosome)
        {
            if(ChromosomesCollection == null)
                ChromosomesCollection = new List<IChromosome>();

            ChromosomesCollection.Add(chromosome);
        }
    }
}