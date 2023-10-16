using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public bool isGem;
    public bool isHeal;
    public GameObject pickupEffect;


    private bool isCollected;

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
        if(other.CompareTag("Player") && !isCollected) //ovde stavljamo uslov sve dok je player i nije pokupio gem onda u nastavku koda ako je gem uvecaj za jedan
        {
            if(isGem) // ako player hoce da pokupi Gem(ako je true)
            {
                LevelManager.instance.gemsCollected++;
                isCollected = true;

                Destroy(gameObject); //unistava gameObject u nasem slucaju gem

                Instantiate(pickupEffect, transform.position, transform.rotation); //znaci napravi kopiju objekta

                UIController.instance.UpdateGemCount();

                AudioManager.instance.PlaySFX(6);


            }

            if (isHeal)
            {
                if(PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth) // proveravamo da li nije full health
                {
                    PlayerHealthController.instance.HealPlayer();
                    isCollected = true;
                    Destroy(gameObject);

                    Instantiate(pickupEffect, transform.position, transform.rotation); //znaci napravi kopiju objekta

                    AudioManager.instance.PlaySFX(7);


                }
            }
        }
    }
}
