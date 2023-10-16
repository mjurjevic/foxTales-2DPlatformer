using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
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
        if(other.tag == "Player") // da li je neki drugi tag jednak tagu player
        {

            //FindObjectOfType<PlayerHealthController>().DealDamage(); //iz liste objekata pronalazimo gde se nalazi PlayerHealthController zatim iz njega izvlacimo nasu funkciju DealDamage

            //Debug.Log("Hit"); // ispisi u konzolu hit

            PlayerHealthController.instance.DealDamage(); //sa ovim olaksavamo da dohvatimo funkciju deal damage 

        }

    }
}
