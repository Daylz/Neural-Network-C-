using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetworkHandler : MonoBehaviour
{
    public NeuralNetwork nn;

	void Start ()
	{
        int[] sizes = { 2, 64, 64, 2 };
        nn = new NeuralNetwork(sizes);
    }
	
	void Update ()
	{
        Vector3 position = this.transform.position;
        float[] inputs = { position.x, position.y };
        float[] outputs = nn.CalculateOutput(inputs);

        //PrintFloatArray(outputs);

        this.transform.position = new Vector3(position.x - ((outputs[0] - 0.5f)), position.y - ((outputs[1] - 0.5f)));

    }

    void PrintFloatArray(float[] floats)
    {
        for (int i = 0; i < floats.Length; i++)
        {
            Debug.Log("Output " + i + ": " + floats[i]);
        }
    }
}
