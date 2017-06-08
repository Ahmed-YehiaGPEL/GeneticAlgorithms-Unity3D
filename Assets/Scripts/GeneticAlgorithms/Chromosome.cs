using System;
using System.Text;
using GeneticAlgorithm.Base;
using UnityEngine;

namespace GeneticAlgorithm
{
    public class Chromosome 
    {
        private static int _defaultGeneLength = 64;
        public static Vector3 lastPosition = Vector3.zero;
        private readonly byte[] _genes = new byte[_defaultGeneLength];

        private int _fitness = 0;

        private Transform attachedTransform;
        public ParticleSystem.Particle particle;

        public Chromosome()
        {
            _fitness = 0;
            particle = new ParticleSystem.Particle();
            particle.position = lastPosition;
            //UnityEngine.Debug.Log("POS = " + lastPosition);
            if (lastPosition.x == AlgorithmDriver.GetInstance().GridWidth)
            {
               // UnityEngine.Debug.Log("Crossing at " + lastPosition + " to " + new Vector3(0, 0.0f, lastPosition.z + AlgorithmDriver.GetInstance().CellWidth) );
                lastPosition =  new Vector3(0,0.0f, lastPosition.z + AlgorithmDriver.GetInstance().CellWidth);
            }
            else
            {
               lastPosition = lastPosition + new Vector3(AlgorithmDriver.GetInstance().CellWidth,0.0f,0.0f);
            }
            AlgorithmDriver.GetInstance().Individuals.Add(this);
        }
        public void GenerateCode()
        {
            for (int i = 0; i < _defaultGeneLength; i++)
            {
                _genes[i] = (byte) Math.Round(UnityEngine.Random.value);
            }
            Recolor();
        }
        public static void SetDefaultLength(int pLength)
        {
            _defaultGeneLength = pLength;
        }
        public void SetGene(int index, byte value)
        {
            _genes[index] = value;
            _fitness = 0; //Reset Fitness
        }
        public byte GetGene(int index)
        {
            return _genes[index];
        }
        public int GetFitness() 
        {
            if (_fitness == 0)
            {
                _fitness =  FitnessCalc.CalculateFitness(this);
            }

            return _fitness;
        }
        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var gene in _genes)
            {
                builder.Append(gene.ToString());
            }
            return builder.ToString();
        }
        public int Size()
        {
            return _genes.Length;
        }
        public static int GetGeneLength()
        {
            return _defaultGeneLength;
            
        }
        public Color GetColor()
        {
            var blueCount = 0;
            var redCount = 0;
            foreach (var gene in _genes)
            {
                if (gene == 1)
                    blueCount++;
                else
                    redCount++;
            }
          //  Debug.Log("Color b = " + blueCount + " Red = " + redCount);
            return new Color((redCount * 3.4f) / 64.0f,0.0f, (3.4f * blueCount) / 64.0f);
        }
        public void Recolor()
        {
         //   attachedTransform.GetComponent<MeshRenderer>().material.SetColor("_Color", GetColor());

            particle.color = GetColor();
            particle.size = 5.0f;
        }
    }
}
