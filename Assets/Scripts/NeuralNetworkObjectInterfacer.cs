using UnityEngine;
using Daylz.Mathf;

public class NeuralNetworkObjectInterfacer : MonoBehaviour
{
    private NeuralNetwork nn;
    private NNSphereController nnctlr;
    private bool isInitialised = false;

    public void InitNeuralNetwork(NeuralNetworkData nnd)
    {
        nn = new NeuralNetwork();
        nn.Init(nnd);
        nnctlr = GetComponent<NNSphereController>();
        isInitialised = true;
    }

    void FixedUpdate()
    {
        if (isInitialised)
        {
            float[] inputs = nnctlr.GetInputs();
            float[] outputs = nn.CalculateOutput(inputs);
            int inputId = GetStrongerOutputIndex(outputs);

            nnctlr.SetInputs(inputId);
        }
    }

    public NeuralNetworkData GetData()
    {
        return nn.GetData();
    }

    public float GetDistance()
    {
        return nnctlr.distanceTravelled;
    }

    public int GetScore()
    {
        return nnctlr.Score;
    }

    public float GetTimeAlive()
    {
        return nnctlr.timeSinceLastCheckpoint;
    }

    public void KillNNObject()
    {
        nnctlr.Alive = false;
        this.gameObject.SetActive(false);
    }

    int GetStrongerOutputIndex(float[] outputs)
    {
        int strongerID = 0;
        float strongerOutput = outputs[0];

        for (int i = 1; i < outputs.Length; i++)
        {
            if (strongerOutput < outputs[i])
            {
                strongerOutput = outputs[i];
                strongerID = i;
            }
        }

        return strongerID;
    }
}