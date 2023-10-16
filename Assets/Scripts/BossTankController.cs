using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankController : MonoBehaviour
{

    public enum bossStates { shooting, hurt, moving, ended}; //stanja sva koja moze glavni boss imati predstavlja listu vrednosti ne mozemo da je menjamo
    public bossStates currentState;



    public Transform theBoss;
    public Animator anim;


    [Header("Movement")]

    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    private bool moveRight;
    public GameObject mine;
    public Transform minePoint;
    public float timeBetweenMines;
    private float mineCounter;

    [Header("Shooting")]
    public GameObject bullet;
    public Transform firePoint;
    public float timeBetweenShots;
    private float shotCounter;

    [Header("Heart")]
    public float hurtTime;
    private float hurtCounter;
    public GameObject hitBox;

    [Header("Health")]

    public int health = 5;
    public GameObject explosion, winPlatform;
    private bool isDefeated;
    public float shotSpeedUp, mineSpeedUp;

    // Start is called before the first frame update
    void Start()
    {
        currentState = bossStates.shooting; //stavljamo na pocetku stanje shooting jer je to idle stanje
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case bossStates.shooting:

                shotCounter -= Time.deltaTime;

                if(shotCounter <= 0)
                {
                    shotCounter = timeBetweenShots; //resetujemo posle metka pripremamo sledeci
                    var newBullet =  Instantiate(bullet, firePoint.position, firePoint.rotation); //stvara bullet na odredjenoj poziciji
                    newBullet.transform.localScale = theBoss.localScale;
                }

                break;
            case bossStates.hurt:

                if (hurtCounter > 0) //znaci ako je player hurtovan onda promeni u to stanje nakon odredjenog vremena promeni u moving nakon sto je boss povredjen
                {
                    hurtCounter -= Time.deltaTime;

                    if(hurtCounter <= 0)
                    {
                        currentState = bossStates.moving;

                        mineCounter = 0;


                        if(isDefeated)
                        {
                            theBoss.gameObject.SetActive(false);
                            Instantiate(explosion,theBoss.position, theBoss.rotation);

                            winPlatform.SetActive(true);

                            AudioManager.instance.StopBossMusic();

                            currentState = bossStates.ended;
                            
                        }
                    }
                }
                break;

            case bossStates.moving:
                
                if(moveRight)
                {
                    theBoss.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    if(theBoss.position.x > rightPoint.position.x) 
                    {
                        theBoss.localScale = new Vector3(1f, 1f, 1f);

                        moveRight = false;

                        EndMovement();

                    }
                }
                else
                {
                    theBoss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    if (theBoss.position.x < leftPoint.position.x)
                    {

                        theBoss.localScale = new Vector3(-1f, 1f, 1f);


                        moveRight = true;

                        EndMovement();
                    }
                }

                mineCounter -= Time.deltaTime;
                if(mineCounter <= 0)
                {
                    mineCounter = timeBetweenMines;
                    Instantiate(mine, minePoint.position, minePoint.rotation);
                }
                   
                break;
        }

#if UNITY_EDITOR     //samo ako smo u unity izvrsice se ovk ne

    
        if(Input.GetKeyDown(KeyCode.H))
        {
            TakeHit();
        }
#endif
    }


    public void TakeHit()
    {
        currentState = bossStates.hurt;
        hurtCounter = hurtTime;
        anim.SetTrigger("Hit"); // pozivamo animaciju hit
        // sa ovim uslovom ulazimo u case hurt stanja

        AudioManager.instance.PlaySFX(0);

        BossTankMine[] mines = FindObjectsOfType<BossTankMine>();
        if(mines.Length > 0)
        {
            foreach(BossTankMine foundMine in mines)
            {
                foundMine.Explode();
            }
        }

        health--;

        if(health <= 0)
        {
            isDefeated = true;
        }
        else
        {
            timeBetweenShots /= shotSpeedUp;
            timeBetweenMines /= mineSpeedUp;
        }
    }


    private void EndMovement()
    {
        currentState = bossStates.shooting;
        shotCounter = 0f; // brze puca
        anim.SetTrigger("StopMoving");

        hitBox.SetActive(true);
    }
}
