using System.Collections;
using System.Collections.Generic;
using GeneticAlgorithm.Base;
using GeneticAlgorithm.Samples.Chromosomes;
using UnityEngine;
using UnityEngine.UI;

namespace GeneticAlgorithm
{
    public class AlgorithmDriver : MonoBehaviour
    {
        private static readonly double UniformRate = .5;
        private static readonly double MutationRate = .015;
        private int TournamentSize = 5;
        private static readonly bool EliteSelection = true;
        private static AlgorithmDriver _instance;

        public Text PopulationSizeText;

        public Text SolutionPatternText;

        public Text TournamentSizeText;

        public Transform SpawnPoint;
        public int GridWidth;
        public int CellWidth;
        public List<ParticleSystem.Particle> particlePoints;
        public List<Chromosome> Individuals;


        public static AlgorithmDriver GetInstance()
        {
            if (_instance == null)
                _instance = FindObjectOfType<AlgorithmDriver>();
            return _instance;
        }
        public Population EvolvePopulation(Population population)
        {
            var newPopulation = new Population(population.GetSize(),false);
            if (EliteSelection)
            {
                newPopulation.SetIndividual(0,population.GetFittest());
            }
            var eliteOffset = EliteSelection ? 1 : 0;
            //Cross Over
            for (var i = eliteOffset; i < population.GetSize(); i++)
            {
                var i1 = _tournamentSelection(population);
                var i2 = _tournamentSelection(population);
                var newIndiv = _crossOver(i1, i2);
                newPopulation.SetIndividual(i, newIndiv);
            }
            //Mutate
            for (var i = eliteOffset; i < population.GetSize(); i++)
            {
                _mutate(newPopulation.GetIndividual(i));
            }
            return newPopulation;
        }
    
        private  Chromosome _tournamentSelection(Population population)
        {
            // Create a tournament population
            var tournament = new Population(TournamentSize, false);
            // For each place in the tournament get a random individual
            for (var i = 0; i < TournamentSize; i++)
            {
                var randomId = (int) (Random.value * population.GetSize());
                tournament.SetIndividual(i, population.GetIndividual(randomId));
            }
            // Get the fittest
            var fittest = tournament.GetFittest();
            return fittest;
        }
        private  void _mutate(Chromosome chromosome)
        {
            for(var i = 0 ; i < chromosome.Size();i++)
                if (Random.value <= MutationRate)
                    chromosome.SetGene(i, ((byte) Mathf.Round(Random.value)));

            chromosome.Recolor();
        }
        private Chromosome _crossOver(Chromosome i1, Chromosome i2)
        {
            var newSolution = new Chromosome();
            for (var i = 0; i < i1.Size(); i++)
            {
                newSolution.SetGene(i, Random.value <= UniformRate ? i1.GetGene(i) : i2.GetGene(i));
            }
            newSolution.Recolor();
            return newSolution;
        }


        private void Awake()
        {
            FitnessCalc.SetSolution("1111111111111111111111111111111111111111111111111111111111111111");
            particlePoints = new List<ParticleSystem.Particle>();
            Individuals = new List<Chromosome>();
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
               StartAlgorithm();
            }
            //SpawnPoint.GetComponent<ParticleSystem>().SetParticles(particlePoints.ToArray(), particlePoints.Count);

        }

        public void StartAlgorithm()
        {
        
            SpawnPoint.GetComponent<ParticleSystem>().Clear(true);
            particlePoints = new List<ParticleSystem.Particle>();
            Individuals = new List<Chromosome>();

            FitnessCalc.SetSolution(SolutionPatternText.text);

            var popSize = int.Parse(PopulationSizeText.text);
            TournamentSize = int.Parse(TournamentSizeText.text);
            StartCoroutine(StartGa(popSize));
        }
        private IEnumerator StartGa(int popSize)
        {
            Chromosome.lastPosition = Vector3.zero;
            var myPop = new Population(popSize, true);
            var generationCount = 0;
            while (myPop.GetFittest().GetFitness() < FitnessCalc.GetMaxFitness())
            {
                generationCount++;
               // print("Generation: " + generationCount + " Fittest: " + myPop.GetFittest().GetFitness());
                myPop = EvolvePopulation(myPop);
                UpdateGenerationInfo();
                Draw();
                yield return new WaitForSeconds(1.0f);
            }
            yield return null;
            print("Solution found!");
            print("Generation: " + generationCount);
            print("Genes:");
            print(myPop.GetFittest());
            print(myPop.GetFittest().GetColor());
            
            
        }
        void Draw()
        {
            SpawnPoint.GetComponent<ParticleSystem>().Clear(true);
            particlePoints = new List<ParticleSystem.Particle>();
            foreach (var individual in Individuals)
            {
                particlePoints.Add(individual.particle);
            }
            SpawnPoint.GetComponent<ParticleSystem>().SetParticles(particlePoints.ToArray(), particlePoints.Count);
        }

        void UpdateGenerationInfo()
        {
            
        }
    }
}