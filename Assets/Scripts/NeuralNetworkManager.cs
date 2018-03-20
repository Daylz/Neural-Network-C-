using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Daylz.Mathf;

public class NeuralNetworkManager : MonoBehaviour
{
    public int AmountToSpawn;
    public GameObject ObjectToSpawn;
    public Transform SpawnPosition;

    public List<GameObject> objects;
    public List<NeuralNetworkObjectInterfacer> bestNNObjects;
    public List<NeuralNetworkData> bestObjectDatas;

    public int generationId = 1;
    public float timePerGeneration = 30;
    public float timeLeft;
    public bool isTimeUp = false;

    public int bestScore = 0;

    public TextMeshProUGUI generationTxt;
    public TextMeshProUGUI timeLeftTxt;

    public float mutationRate = 0.1f;

    int[] sizes = { 9, 4, 4, 8 };

    System.Random rand = new System.Random();

    public void Start()
    {
        objects = new List<GameObject>();

        SpawnNewGeneration(generationId++);
    }

    private void SpawnNewGeneration(int generationId, NeuralNetworkData bestObjectNnd = null)
    {
        generationTxt.text = "Generation: " + generationId;

        for (int i = 0; i < objects.Count; i++)
        {
            Destroy(objects[i].gameObject);
        }
        objects = new List<GameObject>();

        for (int i = 0; i < AmountToSpawn; i++)
        {
            float[][] newBias = bestObjectNnd != null ? CopyNewBiasArrayByValue(bestObjectNnd.biases) : null;
            float[][,] newWeights = bestObjectNnd != null ? CopyNewWeightArrayByValue(bestObjectNnd.weights) : null;

            NeuralNetworkData nnd = new NeuralNetworkData
            {
                sizes = this.sizes,
                biases = bestObjectNnd != null ? GenerateBiasesFrom(newBias) : GenerateNewBiases(),
                weights = bestObjectNnd != null ? GenerateWeightsFrom(newWeights) : GenerateNewWeights()
            };

            GameObject obj = Instantiate(ObjectToSpawn, SpawnPosition.position, Quaternion.identity);
            obj.name = "Objects" + i;
            obj.GetComponent<NeuralNetworkObjectInterfacer>().InitNeuralNetwork(nnd);
            obj.transform.parent = this.transform;
            objects.Add(obj);
        }

        if (bestObjectNnd != null)
        {
            NeuralNetworkData bestNndCopy = NeuralNetworkData.Copy(bestObjectNnd);

            GameObject obj = Instantiate(ObjectToSpawn, SpawnPosition.position, Quaternion.identity);
            obj.name = "Former Best";
            obj.GetComponent<NeuralNetworkObjectInterfacer>().InitNeuralNetwork(bestNndCopy);
            obj.transform.parent = this.transform;
            objects.Add(obj);
        }

        timeLeft = timePerGeneration;
        isTimeUp = false;
    }

    private void Update()
    {
        timeLeftTxt.text = "Time Left: " + timeLeft;

        if (!isTimeUp)
        {
            timeLeft -= Time.deltaTime;
        }
        
        if ((timeLeft < 0 || !AreObjectsAlive()) && !isTimeUp)
        {
            isTimeUp = true;
            EndGeneration();
        }
    }

    private bool AreObjectsAlive()
    {
        if (!isTimeUp)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                ANeuralNetworkObjectController objCtrlr = objects[i].GetComponent<ANeuralNetworkObjectController>();
                if (objCtrlr.Alive)
                {
                    return true;
                }
            }
        }       

        return false;
    }

    private void EndGeneration()
    {        
        bestNNObjects = new List<NeuralNetworkObjectInterfacer>();
        bestScore = 1;

        for (int i = 0; i < objects.Count; i++)
        {
            NeuralNetworkObjectInterfacer nnoi = objects[i].GetComponent<NeuralNetworkObjectInterfacer>();
            int currentScore = nnoi.GetScore();

            nnoi.KillNNObject();

            if (currentScore == bestScore)
            {
                bestNNObjects.Add(objects[i].GetComponent<NeuralNetworkObjectInterfacer>());
            }

            if (currentScore > bestScore)
            {
                bestScore = currentScore;
                bestNNObjects = new List<NeuralNetworkObjectInterfacer>();
                bestNNObjects.Add(objects[i].GetComponent<NeuralNetworkObjectInterfacer>());
            }
        }   
        
        if (bestNNObjects.Count > 0)
        {
            NeuralNetworkObjectInterfacer bestNNObject = bestNNObjects[0];

            for (int i = 1; i < bestNNObjects.Count; i++)
            {
                if (bestNNObjects[i].GetDistance() < bestNNObject.GetDistance())
                {
                    bestNNObject = bestNNObjects[i];
                }
            }

            SpawnNewGeneration(generationId++, bestNNObject.GetData());
        }
        else
        {
            SpawnNewGeneration(generationId++);
        }
    }

    private float[][] GenerateNewBiases()
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

    private float[][,] GenerateNewWeights()
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

    private float[][] GenerateBiasesFrom(float[][] biases)
    {
        for (int i = 0; i < sizes.Length - 1; i++)
        {
            for (int j = 0; j < biases[i].Length; j++)
            {
                if (UnityEngine.Random.value < mutationRate)
                {
                    biases[i][j] = rand.NextGaussianFloat();
                }                
            }
        }

        return biases;
    }

    private float[][,] GenerateWeightsFrom(float[][,] weights)
    {
        for (int i = 0; i < weights.Length; i++)
        {
            for (int j = 0; j < weights[i].GetLength(0); j++)
            {
                for (int k = 0; k < weights[i].GetLength(1); k++)
                {
                    if (UnityEngine.Random.value < mutationRate)
                    {
                        weights[i][j, k] = rand.NextGaussianFloat();
                    }
                }
            }
        }

        return weights;
    }

    private float[][] CopyNewBiasArrayByValue(float[][] biases)
    {
        float[][] biasesCopy = new float[sizes.Length - 1][];

        for (int i = 0; i < sizes.Length - 1; i++)
        {
            biasesCopy[i] = new float[sizes[i + 1]];

            for (int j = 0; j < biases[i].Length; j++)
            {              
                biasesCopy[i][j] = biases[i][j];                
            }
        }

        return biasesCopy;
    }

    private float[][,] CopyNewWeightArrayByValue(float[][,] weights)
    {
        float[][,] weightsCopy = new float[sizes.Length - 1][,];

        for (int i = 0; i < weightsCopy.Length; i++)
        {
            weightsCopy[i] = new float[sizes[i + 1], sizes[i]];

            for (int j = 0; j < weightsCopy[i].GetLength(0); j++)
            {
                for (int k = 0; k < weights[i].GetLength(1); k++)
                {
                    weightsCopy[i][j, k] = weights[i][j, k];
                }
            }
        }

        return weightsCopy;
    }
}
