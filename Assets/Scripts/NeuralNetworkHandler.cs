using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetworkHandler : MonoBehaviour
{
    public NeuralNetwork nn;
    public NeuralNetworkData nnd;
    int[] sizes = { 2, 1024, 2 };

    int score = 0;

    void Start ()
	{        
        nn = new NeuralNetwork(sizes);
        nnd = nn.nnd;

        //StartCoroutine("UpdatePosition");
    }
	
    IEnumerator UpdatePosition()
    {
        while (true)
        {
            Vector3 position = this.transform.position;
            float[] inputs = { position.x, position.y };
            float[] outputs = nn.CalculateOutput(inputs);
            int outputId = GetStrongerOutputIndex(outputs);

            switch (outputId)
            {
                case 0:
                    Move(0, 1);
                    break;
                case 1:
                    Move(0, -1);
                    break;
                case 2:
                    Move(1, 0);
                    break;
                case 3:
                    Move(-1, 0);
                    break;
            }

            yield return new WaitForSeconds(0.1f);
        }

        
    }

    private void Move(int dx, int dy)
    {
        Vector3 position = this.transform.position;
        this.transform.position = new Vector3(position.x + dx, position.y + dy);
    }

    void Update ()
	{
        Vector3 position = this.transform.position;
        float[] inputs = { position.x, position.y };
        float[] outputs = nn.CalculateOutput(inputs);

        //PrintFloatArray(outputs);

        this.transform.position = new Vector3(position.x - ((outputs[0] - 0.5f)), position.y - ((outputs[1] - 0.5f)));
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

    void PrintFloatArray(float[] floats)
    {
        for (int i = 0; i < floats.Length; i++)
        {
            Debug.Log("Output " + i + ": " + floats[i]);
        }
    }
}
