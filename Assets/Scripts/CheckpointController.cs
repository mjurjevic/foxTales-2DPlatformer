using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{

    public static CheckpointController instance;


    private Checkpoint[] checkpoints;

    public Vector3 spawnPoint; //pozicija na nivou



    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        checkpoints = FindObjectsOfType<Checkpoint>();

        spawnPoint = PlayerController.instance.transform.position; // player ce se spawnovati na pocetak
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeactivateCheckpoints()
    {
        for(int i = 0; i < checkpoints.Length;  i++)
        {
            checkpoints[i].ResetCheckpoint();
        }
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

}
