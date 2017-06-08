namespace GeneticAlgorithm.Base
{
    public abstract class SelectionBase
    {
        public int SelectionSize { get; set; }
        public abstract IChromosome Select<T>(IPopulation population) where T : IChromosome;
    }
}
