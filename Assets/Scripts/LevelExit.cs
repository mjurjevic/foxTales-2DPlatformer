using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player") // ako prostor dotakne player onda pozovi funkciju zavrsi nivo iz Level Managera
        {
            LevelManager.instance.EndLevel();
        }
    }
}
