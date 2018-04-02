using System;
using UnityEngine;
using Daylz.Mathf;

public class NNSphereController : ANeuralNetworkObjectController
{
    public float distanceTravelled = 0;
    public float timeSinceLastCheckpoint = 0;
    public Vector3 lastPosition;

    public float speed = 1f;
    public float rotationSpeed = 10f;

    public Material aliveMaterial;
    public Material deadMaterial;

    //private Rigidbody rb;

    private float moveHorizontal;
    private float moveVertical;

    public int nextCheckPoint = 0;

    private bool initialized = false;

    private void Start()
    {
        //rb = GetComponent<Rigidbody>();
        lastPosition = this.transform.position;
        timeSinceLastCheckpoint = 0f;

        initialized = true;
    }

    private void Update()
    {
        distanceTravelled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;
        timeSinceLastCheckpoint += Time.deltaTime;

        /*moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");*/

        Vector3 position = this.transform.position;
        Quaternion rotation = this.transform.rotation;
        position += moveVertical * Time.deltaTime * speed * transform.up;
        transform.Rotate(new Vector3(0f, 0f, -moveHorizontal * Time.deltaTime * rotationSpeed));

        this.transform.position = position;
    }

    /*private void FixedUpdate()
    {
        //moveHorizontal = Input.GetAxis("Horizontal");
        //moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);     
    }*/

    // Inputs that are fed to the Neural Network
    override public float[] GetInputs()
    {
        float[] inputs = new float[4];
        float maxDistance = 50.0f;
        RaycastHit hit;

        if (!initialized)
        {
            return inputs;
        }

        // Up
        /*if (Physics.Raycast(transform.position, new Vector3(0, 1, 0), out hit, maxDistance))
        {
            inputs[0] = hit.distance / maxDistance;
        }
        // Down
        if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), out hit, maxDistance))
        {
            inputs[1] = hit.distance / maxDistance;
        }
        // Left
        if (Physics.Raycast(transform.position, new Vector3(-1, 0, 0), out hit, maxDistance))
        {
            inputs[2] = hit.distance / maxDistance;
        }
        // Right
        if (Physics.Raycast(transform.position, new Vector3(1, 0, 0), out hit, maxDistance))
        {
            inputs[3] = hit.distance / maxDistance;
        }*/
        // Up Left
        if (Physics.Raycast(transform.position, -transform.right + transform.up, out hit, maxDistance))
        {
            inputs[0] = hit.distance / maxDistance;
        }
        // Up Right
        if (Physics.Raycast(transform.position, transform.right + transform.up, out hit, maxDistance))
        {
            inputs[1] = hit.distance / maxDistance;
        }
        // Down Left
        if (Physics.Raycast(transform.position, -transform.right + -transform.up, out hit, maxDistance))
        {
            inputs[2] = hit.distance / maxDistance;
        }
        // Down Right
        if (Physics.Raycast(transform.position, transform.right + -transform.up, out hit, maxDistance))
        {
            inputs[3] = hit.distance / maxDistance;
        }

        /*inputs[8] = rb.velocity.magnitude;*/

        return inputs;
    }

    // Controller inputs that are given to the object by the Neural Network
    override public void SetInputs(float[] inputs)
    {
        moveHorizontal = inputs[0] - 0.5f;
        moveVertical = inputs[1] - 0.5f;
    }

    // Controller inputs that are given to the object by the Neural Network
    /*override public void SetInputs(int inputId)
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
            case 8:
                moveHorizontal = 0f;
                moveVertical = 0f;
                break;
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        string checkpoint = "Checkpoint" + nextCheckPoint;

        if (checkpoint.Equals(other.name))
        {
            distanceTravelled = 0;
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

    public override int CalculatedScore()
    {
        return (int)(Score + distanceTravelled);
    }
}
