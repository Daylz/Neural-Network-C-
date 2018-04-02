using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ANeuralNetworkObjectController : MonoBehaviour, INeuralNetworkObjectController
{
    public bool Alive = true;
    public int Score = 0;

    abstract public float[] GetInputs();
    //abstract public void SetInputs(int inputId);
    abstract public void SetInputs(float[] inputId);
    abstract public int CalculatedScore();
}
