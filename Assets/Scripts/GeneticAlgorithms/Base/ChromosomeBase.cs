using System;
using System.Collections.Generic;
using System.Text;
namespace GeneticAlgorithm.Base
{
    public enum ChromosomeType
    {
        Binary,
        Integer,
        StringLiteral
    }

    public interface IChromosome
    {
        //ChromosomeType GetChromosomeType();
        double GetFitness();
        IGene GetGene(int i) ;
        int Size();
        void SetGene<T>(int index, object value) where T : IEquatable<T>;
        void SetGene(int index, IGene value);
    }

    public abstract class ChromosomeBase<T> : IChromosome where T: IEquatable<T>
    {
        public ChromosomeType Type;
        public IGene[] Genes;
        public IAlgorithmDriver Driver;
        public double Fitness { get; set; }
        public int DefaultGeneLength { get { return _defaultGeneLength; } set { _defaultGeneLength = value; } }
        private int _defaultGeneLength = 64;

        protected ChromosomeBase()
        {
            Fitness = 0;
            Genes = new IGene[_defaultGeneLength];
        }
        public double GetFitness()
        {
            if (Fitness == 0)
            {
                Fitness = Driver.GetFitnessCalculator().CalculateFitness(this);
            }
            return Fitness;
        }
        public IGene GetGene(int i)
        {
            return Genes[i];
        }
        public int GetGenesLength()
        {
            return _defaultGeneLength;
        }
        public int Size()
        {
            return Genes.Length;
        }
        public abstract void GenerateRandomCode();
        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var gene in Genes)
            {
                builder.Append(gene);
            }
            return builder.ToString();
        }
        public void SetGene(int index, IGene value)
        {
            Genes[index] = value;
            Fitness = 0; //Reset Fitness
        }
        public void SetGene<T1>(int index, object value) where T1: IEquatable<T1>
        {
            ((GeneBase<T1>)Genes[index]).Value = value;
            Fitness = 0; //Reset Fitness
        }
    }
}
