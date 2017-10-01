using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm.Base;
using GeneticAlgorithm.Samples.Chromosomes;
using GeneticAlgorithm.Samples.Fitness;
using GeneticAlgorithm.Samples.Populations;
using GeneticAlgorithm.Samples.Selection;
using UnityEngine;
using UnityEngine.UI;

namespace GeneticAlgorithm.Samples.Drivers
{
    
    public class BinaryAlgorithmDriver : Base.AlgorithmDriver
    {
        private const float CrossOverUniformRate = .5f;
        private const float MutationRate = 0.015f;
        public bool EliteSelection;
        public Text PopulationSizeText;

        public Text SolutionPatternText;

        public Text TournamentSizeText;

        public Transform SpawnPoint;
        public int GridWidth;
        public int CellWidth;
        public List<ParticleSystem.Particle> ParticlePoints;
        private int _tournamentSize = 5;

        public static BinaryAlgorithmDriver GetInstance()
        {
            return FindObjectOfType<BinaryAlgorithmDriver>();
        }
        public override IPopulation Evolve(IPopulation population)
        {
            
            var newPopulation = new BinaryPopulation(population.GetSize(), false);
            if (EliteSelection)
            {
                newPopulation.SetChromosome(0, population.GetFittest());
            }
            var eliteOffset = EliteSelection ? 1 : 0;
            //Cross Over
            for (var i = eliteOffset; i < population.GetSize(); i++)
            {
                var i1 = SelectionBaseController.Select<BinaryChromosome>(population);
                var i2 = SelectionBaseController.Select<BinaryChromosome>(population);
                var newChromosome = new BinaryChromosome(64) as IChromosome;
                CrossoverController.Crossover(i1, i2, ref newChromosome);
                ((BinaryChromosome) newChromosome).Recolor();
                newPopulation.SetChromosome(i, newChromosome);
            }
            //Mutate
            for (var i = eliteOffset; i < population.GetSize(); i++)
            {
                MutationController.Mutate(newPopulation.GetChromosome(i));
                ((BinaryChromosome)newPopulation.GetChromosome(i)).Recolor();
            }
            return newPopulation;
        }

        public override void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                EliteSelection = true;
                StartAlgorithm();
            }
        }

        public override void Initialize()
        {
            CrossoverController = new UniformCrossOver();
            ((UniformCrossOver) CrossoverController).UniformRate = CrossOverUniformRate;

            MutationController = new BinaryMutation();
            ((BinaryMutation) MutationController).MutationRate = MutationRate;

            SelectionBaseController = new TournamentSelection();
            ((TournamentSelection) SelectionBaseController).SelectionSize = _tournamentSize;

            BinaryChromosome solution = new BinaryChromosome(64);
            for (var i = 0; i < 64; i++)
                solution.Genes[i]  = (new BinaryGene(1));

            FitnessCalculator = new BinaryFitnessCalculator(solution);
            IsInitialized = true;
        }

        public override void Awake()
        {
            Initialize();
            print("Initialized");
            
            ParticlePoints = new List<ParticleSystem.Particle>();
        }

        public override void Start()
        {
            print("Algorithm Started");
        }

        public void StartAlgorithm()
        {

            SpawnPoint.GetComponent<ParticleSystem>().Clear(true);
            ParticlePoints = new List<ParticleSystem.Particle>();
      

            FitnessCalc.SetSolution(SolutionPatternText.text);

            var popSize = int.Parse(PopulationSizeText.text);
            _tournamentSize = int.Parse(TournamentSizeText.text);
            StartCoroutine(StartGa(popSize));
        }
        private IEnumerator StartGa(int popSize)
        {
            Chromosome.lastPosition = Vector3.zero;

            var myPop = new BinaryPopulation(popSize, true);
            var generationCount = 0;
            while (myPop.GetFittest().GetFitness() < FitnessCalculator.GetPeakFitness())
            {
                Debug.Log("Evolution Cycle: Population Fitness = " + myPop.GetFittest().GetFitness());
                generationCount++;
                // print("Generation: " + generationCount + " Fittest: " + myPop.GetFittest().GetFitness());
                myPop = Evolve(myPop) as BinaryPopulation;
                //UpdateGenerationInfo();
                Draw();
                yield return new WaitForSeconds(1.0f);
            }
            yield return null;
            print("Solution found!");
            print("Generation: " + generationCount);
            print("Genes:");
            print(myPop.GetFittest());
            print(((BinaryChromosome) myPop.GetFittest()).GetColor());


        }
        void Draw()
        {
            SpawnPoint.GetComponent<ParticleSystem>().Clear(true);
            ParticlePoints = new List<ParticleSystem.Particle>();
            foreach (var individual in ChromosomesCollection)
            {
                ParticlePoints.Add(((BinaryChromosome) individual).particle);
            }
            SpawnPoint.GetComponent<ParticleSystem>().SetParticles(ParticlePoints.ToArray(), ParticlePoints.Count);
        }
    }
}
