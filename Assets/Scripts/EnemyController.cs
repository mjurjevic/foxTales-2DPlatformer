using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float moveSpeed;


    public Transform leftPoint, rightPoint; // varijable pravimo koje cemo koristiti za to da nam se pokrece karakter levo desno naizmenicno posle nekog vremena


    private bool movingRight;

    private Rigidbody2D theRB; //varijabla za karakter
    public SpriteRenderer theSR;

    public float moveTime, waitTime;
    private float moveCount, waitCount;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); //dohvatamo animaciju koju smo napravili za anim

        leftPoint.parent = null; //ovo stavljamo jer nam je u unity roditelj objekat enemyfrog, ne zelimo da kada je pokrenuta igrica bude child zato za oba postavljamo da je null da ne bi imao roditelja
        rightPoint.parent = null;
        movingRight = true;

        moveCount = moveTime;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (moveCount > 0)
        {
            moveCount -= Time.deltaTime;

            if (movingRight)
            {

                theSR.flipX = true; //kada nam se karakter krece levo desno da se zapravo okrene u desno zato stavljamo na true
                theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y); //za kretanje udesno
                if (transform.position.x > rightPoint.position.x)// proveravamo da li je pozicija transform.position presla preko rightpoint tj pointa koji smo mu mi zadali
                {
                    movingRight = false;
                }

            }
            else
            {
                theSR.flipX = false; // ovde samo u suprotnom smeru da okrenemo

                theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y); //ovde radimo samo za levo, ne moramo da pravimo novu varijablu
                if (transform.position.x < leftPoint.position.x) //ista logika kao za desno samo u suprotnom smeru
                {
                    movingRight = true;
                }

            }


            if (moveCount <= 0)
            {
                waitCount = Random.Range(waitTime * .75f, waitTime * 1.25f); //ovde stavljamo da zapravo na random mestima staje nas karakter
            }

            anim.SetBool("isMoving", true); //stavljamo na kraju uslova da bi aktivirali animaciju dok se krece karakter 

        }else if(waitCount > 0) //znaci ovde ako nam je true uslov onda se oduzima koliko ima counta do 0, zapravo ovde pravimo da nam enemy ne krece se konstnatno vec zastane na onoliko koliko smo mu stavili waitTime a to je 1 sekund
        {
            waitCount -= Time.deltaTime;
            theRB.velocity = new Vector2(0f, theRB.velocity.y);

            if(waitCount <= 0)
            {
                moveCount = Random.Range(moveTime * .75f, waitTime * .75f); //takodje i za kretanje stavljamo random sto je logicnije nego da mu mi zadajemo vreme
            }

            anim.SetBool("isMoving", false); // stavljamo false da bi animaciju zaustavili kada je i karakter zaustavljen
        }
    } 

}
