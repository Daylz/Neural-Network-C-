using Daylz.Mathf;
using System;

[System.Serializable]
public class NeuralNetworkData
{
    public int[] sizes;
    public float[][] biases;
    public float[][,] weights;

    private static Random rand = new Random();

    public static NeuralNetworkData Copy(NeuralNetworkData data)
    {
        NeuralNetworkData newData = new NeuralNetworkData();

        newData.sizes = data.sizes;
        newData.biases = data.biases;
        newData.weights = data.weights;

        return newData;
    }

    public static float[][] GenerateBiases(int[] sizes)
    {
        float[][] biases = new float[sizes.Length - 1][];        

        for (int i = 0; i < sizes.Length - 1; i++)
        {
            biases[i] = new float[sizes[i + 1]];

            for (int j = 0; j < biases[i].Length; j++)
            {
                biases[i][j] = rand.NextGaussianFloat();
            }
        }

        return biases;
    }

    public static float[][,] GenerateWeights(int[] sizes)
    {
        float[][,] weights = new float[sizes.Length - 1][,];

        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = new float[sizes[i + 1], sizes[i]];

            for (int j = 0; j < weights[i].GetLength(0); j++)
            {
                for (int k = 0; k < weights[i].GetLength(1); k++)
                {
                    weights[i][j, k] = rand.NextGaussianFloat();
                }
            }
        }

        return weights;
    }
}
