using UnityEngine;

public class NNEvolution : MonoBehaviour
{
    public int GenerationId = 1;
    public int ItemsPerGeneration;


    public NeuralNetwork[] nns;

    public int[] sizes = { 2, 16, 16, 4 };

    private void Start()
    {
        // Best function name 2018
        GenerateGeneration();
    }

    private void GenerateGeneration()
    {
        nns = new NeuralNetwork[ItemsPerGeneration];

        for (int i = 0; i < ItemsPerGeneration; i++)
        {
            nns[i] = new NeuralNetwork(sizes);
        }
    }
}
