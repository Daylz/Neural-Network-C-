public interface INeuralNetworkObjectController
{
    float[] GetInputs();
    //void SetInputs(int inputId);
    void SetInputs(float[] inputId);
    int CalculatedScore();
}
