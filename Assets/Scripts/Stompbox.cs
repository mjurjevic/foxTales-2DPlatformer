using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stompbox : MonoBehaviour
{

    public GameObject deathEffect;


    public GameObject collectible; //promenljiva sa kojom hvatamo taj objekat
    [Range(0,100)] public float chanceToDrop; //varijabla koja nam sluzi da ako ubijemo neprijatelja(enemy) imamo sansu da dobijemo tresnjicu(health pack) nju stavljamo unutar unitya tj zadajemo joj vrednost izmedju 0 i 100
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy") //stavljamo tag na enemy(zabu u nasem slucaju) i proveravamo, stavljamo tag na childa zabe iz razloga sto child ima BoxCollider
        {
            Debug.Log("Hit Enemy"); // proveravamo kada skocimo na neprijatelja da li ce se pojaviti poruka

            other.transform.parent.gameObject.SetActive(false); // stavljamo na roditelja da deaktiviramo tj iskljucimo neprijatelja


            Instantiate(deathEffect, other.transform.position, other.transform.rotation); //stavljamo efekat smrti kada ubijemo neprijatelja

            PlayerController.instance.Bounce(); //pozivamo funkciju iz playercontrollera da nakon sto nestane enemy odskoci nas karakter

            float dropSelect = Random.Range(0, 100f); //pravimo varijablu koja ima random range od 0 do 100


            if(dropSelect <= chanceToDrop)
            {
                Instantiate(collectible, other.transform.position, other.transform.rotation); //ovim spawnujemo health i da nas karakter pokupi health ako je iskaz true
            }


            AudioManager.instance.PlaySFX(3);
        }
    }
}
