using UnityEngine;

public class NeuralNetwork
{
    public int[] sizes;
    public int numLayers;
    public float[] inputs;
    public float[][] biases;
    public float[][][] weights;

    public NeuralNetwork(int[] sizes)
    {
        this.sizes = sizes;
        numLayers = sizes.Length;

        biases = new float[sizes.Length - 1][];
        for (int i = 0; i < sizes.Length - 1; i++)
        {
            biases[i] = new float[sizes[i + 1]];

            for (int j = 0; j < biases[i].Length; j++)
            {
                biases[i][j] = Random.Range(-3.0f, 3.0f);
            }
        }

        //PrintBiases();

        weights = new float[sizes.Length - 1][][];
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = new float[sizes[i + 1]][];

            for (int j = 0; j < weights[i].Length; j++)
            {
                weights[i][j] = new float[sizes[i]];

                for (int k = 0; k < weights[i][j].Length; k++)
                {
                    weights[i][j][k] = Random.Range(-3.0f, 3.0f);
                }
            }
        }

        //PrintWeights();

        //PrintTotalParams();
    }

    public float[] CalculateOutput(float[] inputs)
    {
        float[] outputs = { };

        for (int layerId = 1; layerId < sizes.Length; layerId++)
        {
            outputs = new float[sizes[layerId]];

            for (int neuronId = 0; neuronId < sizes[layerId]; neuronId++)
            { 
                for (int weightId = 0; weightId < sizes[layerId - 1]; weightId++)
                {
                    // Dot product weights matrix * inputs vector
                    outputs[neuronId] += weights[layerId-1][neuronId][weightId] * inputs[weightId];
                }

                outputs[neuronId] = Sigmoid(outputs[neuronId] + biases[layerId-1][neuronId]);
            }

            inputs = outputs;
        }

        return outputs;
    }

    float Sigmoid(float x)
    {
        return 1 / (1 + Mathf.Exp(-x));
    }

    void PrintBiases()
    {
        Debug.Log("Biases:\n");

        for (int i = 0; i < biases.Length; i++)
        {
            for (int j = 0; j < biases[i].Length; j++)
            {
                Debug.Log(i + "," + j + "\t" + biases[i][j] + "\n");
            }
        }
    }

    void PrintWeights()
    {
        Debug.Log("Weights:\n");

        for (int i = 0; i < weights.Length; i++)
        {
            for (int j = 0; j < weights[i].Length; j++)
            {
                for (int k = 0; k < weights[i][j].Length; k++)
                {
                    Debug.Log(i + "," + j + "," + k + "\t" + weights[i][j][k] + "\n");
                }
            }
        }
    }

    void PrintTotalParams()
    {
        int sum = 0;

        for (int i = 1; i < sizes.Length; i++)
        {
            sum += sizes[i - 1] * sizes[i];
            sum += sizes[i];
        }

        Debug.Log("Total Parameters: " + sum + "\n");
    }
}
