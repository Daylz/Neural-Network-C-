using System;
using UnityEngine;

public class NNSphereController : ANeuralNetworkObjectController
{
    public float distanceTravelled = 0;
    public float timeSinceLastCheckpoint = 0;
    public Vector3 lastPosition;

    public float speed = 1f;

    public Material aliveMaterial;
    public Material deadMaterial;

    private Rigidbody rb;

    private float moveHorizontal;
    private float moveVertical;

    public int nextCheckPoint = 0;

    private bool initialized = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastPosition = this.transform.position;
        timeSinceLastCheckpoint = 0f;

        initialized = true;
    }

    private void Update()
    {
        distanceTravelled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;
        timeSinceLastCheckpoint += Time.deltaTime;

        Vector3 position = this.transform.position;

    }

    private void FixedUpdate()
    {
        //moveHorizontal = Input.GetAxis("Horizontal");
        //moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);     
    }

    // Inputs that are fed to the Neural Network
    override public float[] GetInputs()
    {
        float[] inputs = new float[9];
        float maxDistance = 20.0f;
        RaycastHit hit;

        if (!initialized)
        {
            return inputs;
        }

        // Up
        if (Physics.Raycast(transform.position, new Vector3(0,0,1), out hit, maxDistance))
        {
            inputs[0] = hit.distance;
        }
        // Down
        if (Physics.Raycast(transform.position, new Vector3(0, 0, -1), out hit, maxDistance))
        {
            inputs[1] = hit.distance;
        }
        // Left
        if (Physics.Raycast(transform.position, new Vector3(-1, 0, 0), out hit, maxDistance))
        {
            inputs[2] = hit.distance;
        }
        // Right
        if (Physics.Raycast(transform.position, new Vector3(1, 0, 0), out hit, maxDistance))
        {
            inputs[3] = hit.distance;
        }
        // Up Left
        if (Physics.Raycast(transform.position, new Vector3(-1, 0, 1), out hit, maxDistance))
        {
            inputs[4] = hit.distance;
        }
        // Up Right
        if (Physics.Raycast(transform.position, new Vector3(1, 0, 1), out hit, maxDistance))
        {
            inputs[5] = hit.distance;
        }
        // Down Left
        if (Physics.Raycast(transform.position, new Vector3(-1, 0, -1), out hit, maxDistance))
        {
            inputs[6] = hit.distance;
        }
        // Down Right
        if (Physics.Raycast(transform.position, new Vector3(1, 0, -1), out hit, maxDistance))
        {
            inputs[7] = hit.distance;
        }

        inputs[8] = rb.velocity.magnitude;

        return inputs;
    }

    // Controller inputs that are given to the object by the Neural Network
    override public void SetInputs(int inputId)
    {
        switch (inputId)
        {
            // Left
            case 0:
                moveHorizontal = -1f;
                moveVertical = 0f;
                break;
            //Right
            case 1:
                moveHorizontal = 1f;
                moveVertical = 0f;
                break;
            // Up
            case 2:
                moveHorizontal = 0f;
                moveVertical = 1f;
                break;
            // Down
            case 3:
                moveHorizontal = 0f;
                moveVertical = -1f;
                break;
            // Down left
            case 4:
                moveHorizontal = -1f;
                moveVertical = -1f;
                break;
            // Down Right
            case 5:
                moveHorizontal = 1f;
                moveVertical = -1f;
                break;
            // Up left
            case 6:
                moveHorizontal = -1f;
                moveVertical = 1f;
                break;
            // Up right
            case 7:
                moveHorizontal = 1f;
                moveVertical = 1f;
                break;
            // Nothing
            /*case 8:
                moveHorizontal = 0f;
                moveVertical = 0f;
                break;*/
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string checkpoint = "Checkpoint" + nextCheckPoint;

        if (checkpoint.Equals(other.name))
        {
            timeSinceLastCheckpoint = 0;
            nextCheckPoint++;
            Score += nextCheckPoint;
        }

        if (other.tag == "Walls")
        {
            OnWallHit();
        }       
    }

    private void OnWallHit()
    {
        Alive = false;
        this.gameObject.SetActive(false);
    }

    void PrintFloatArray(float[] floats)
    {
        for (int i = 0; i < floats.Length; i++)
        {
            Debug.Log("Output " + i + ": " + floats[i]);
        }
    }
}
