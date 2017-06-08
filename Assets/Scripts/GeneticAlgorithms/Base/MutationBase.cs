namespace GeneticAlgorithm.Base
{
    public abstract class MutationBase
    {
        public double MutationRate
        {
            get { return _mutationRate; }
            set { _mutationRate = value; }
        }
        private double _mutationRate = 0.015;

        public abstract IChromosome Mutate(IChromosome population);
        public abstract void Mutate(ref IChromosome population);
    }
}
