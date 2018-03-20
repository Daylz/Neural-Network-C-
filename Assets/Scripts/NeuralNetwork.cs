using Daylz.Mathf;

public class NeuralNetwork
{
    public int[] sizes;
    public float[] inputs;
    public float[][] biases;
    public float[][,] weights;

    public NeuralNetworkData nnd;

    public void Init(NeuralNetworkData nnd)
    {
        this.sizes = nnd.sizes;
        this.biases = nnd.biases;
        this.weights = nnd.weights;
        this.nnd = nnd;
    }

    public float[] CalculateOutput(float[] inputs)
    {
        float[] outputs = { };

        inputs = MathExtension.Sigmoid(inputs);

        for (int layerId = 1; layerId < sizes.Length; layerId++)
        {
            outputs = new float[sizes[layerId]];
            
            outputs = MathExtension.Sigmoid(weights[layerId - 1].RMultiply(inputs).RAdd(biases[layerId - 1]));

            inputs = outputs;
        }

        return outputs;
    }

    public NeuralNetworkData GetData()
    { 
        return this.nnd;
    }
}