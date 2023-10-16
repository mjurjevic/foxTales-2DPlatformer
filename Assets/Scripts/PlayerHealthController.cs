using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    public static PlayerHealthController instance; // pravimo staticnu varijablu koja znaci da ce da zadrzava svoju vrednost svuda, ako je 1 bice svuda 1, kada napravimo staticnu varijablu nije nam prikazana u inspectoru u unity
    
    public int currentHealth, maxHealth; // pravimo varijable za health koji u unity zadajemo

    public float invincibleLength; //zadajemo ove varijable da bi kada igrac stane na prepreku ne izgubi automatski dosta healtha vec da postoji kratak period gde moze da se izvuce iz prepreka

    public GameObject deathEffect;

    private float invincibleCounter;

    private SpriteRenderer theSR; // dohvatamo sprite iz unitya i dajemo mu ime theSR


    private void Awake()
    {
        instance = this; 
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        theSR = GetComponent<SpriteRenderer>(); //dohvatamo tu komponentu odmah na startu
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime; //delta time je vreme izmedju update iz jednog framea u drugi

            if(invincibleCounter <= 0 ) // ako dodje ispod nule ili jednaka nuli(sto je retko ali moguce) vraca nam se player u nasu boju
            { 
                theSR.color = new Color(theSR.color.r, theSR.color.g,theSR.color.b, 1f);  

            }
        }
    }


    public void DealDamage()
    {
        if (invincibleCounter <= 0)
        {

            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                //gameObject.SetActive(false); // ako bude health jednak ili manji od 0 onda nestaje objekat

                Instantiate(deathEffect, transform.position, transform.rotation);
               
                LevelManager.instance.RespawnPlayer();

            }
            else
            {
                invincibleCounter = invincibleLength;
                theSR.color = new Color(theSR.color.r, theSR.color.g,theSR.color.b, .5f); //posto ne mozemo direktno doci do alpha boje gde smanjujemo opacity playeru, moramo da dohvatimo rgb koje ne menjamo i tek na kraju mozemo da menjamo alphu koja je unutar unitya
                PlayerController.instance.KnockBack();

                AudioManager.instance.PlaySFX(9);

            }


            UIController.instance.UpdateHealthDisplay();
        }
    }


    public void HealPlayer()
    {
       // currentHealth = maxHealth; //ako zelimo max health
       currentHealth++;

        if(currentHealth > maxHealth) //ako je u nekom slucaju iznad max healtha onda je current jednak maxu
        {
            currentHealth = maxHealth;
        }

        UIController.instance.UpdateHealthDisplay();
    }
    
    private void OnCollisionEnter2D(Collision2D other) // ulazi kao parent u platformu
    {
        if(other.gameObject.tag == "Platform")
        {
            transform.parent = other.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other) // sa ovom funkcijom izlazi nam player iz parenta
    {
        if(other.gameObject.tag == "Platform")
        {
            transform.parent = null;
        }
    }
}
