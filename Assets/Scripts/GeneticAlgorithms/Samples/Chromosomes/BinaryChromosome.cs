using System;
using System.Collections.Generic;
using GeneticAlgorithm.Base;
using GeneticAlgorithm.Samples.Drivers;
using GeneticAlgorithm.Samples.Fitness;
using UnityEngine;

namespace GeneticAlgorithm.Samples.Chromosomes
{
    public class BinaryChromosome : ChromosomeBase<byte>
    {
        private Transform attachedTransform;
        public ParticleSystem.Particle particle;
        public static Vector3 lastPosition = Vector3.zero;
        public BinaryChromosome(int length)
        {
            DefaultGeneLength = length;
            Fitness = 0;
            particle = new ParticleSystem.Particle();
            particle.position = lastPosition;
           // UnityEngine.Debug.Log("POS = " + lastPosition);
            if (lastPosition.x == AlgorithmDriver.GetInstance().GridWidth)
            {
               // UnityEngine.Debug.Log("Crossing at " + lastPosition + " to " + new Vector3(0, 0.0f, lastPosition.z + AlgorithmDriver.GetInstance().CellWidth) );
                lastPosition = new Vector3(0, 0.0f, lastPosition.z + AlgorithmDriver.GetInstance().CellWidth);
            }
            else
            {
                lastPosition = lastPosition + new Vector3(AlgorithmDriver.GetInstance().CellWidth, 0.0f, 0.0f);
            }
            Driver = BinaryAlgorithmDriver.GetInstance();
            Driver.Add(this);

            Genes = new IGene[DefaultGeneLength];
        }

        public override void GenerateRandomCode()
        {
            for (int i = 0; i < DefaultGeneLength; i++)
            {
                Genes[i] = (new BinaryGene((byte) Math.Round(UnityEngine.Random.value)));
            }
            Recolor();
        }
        public Color GetColor()
        {
            var blueCount = 0;
            var redCount = 0;
            foreach (var gene in Genes)
            {
               // Debug.Log(((BinaryGene)gene).Value);

                if (((BinaryGene)gene).Value.Equals(1))
                    blueCount++;
                else
                    redCount++;
            }
          //  Debug.Log("Color b = " + blueCount + " Red = " + redCount);
            return new Color((redCount * 3.4f) / 64.0f, 0.0f, (3.4f * blueCount) / 64.0f);
        }
        public void Recolor()
        {
            //   attachedTransform.GetComponent<MeshRenderer>().material.SetColor("_Color", GetColor());

            particle.color = GetColor();
            particle.size = 5.0f;
        }

    }
}
