using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;  //pravimo varijablu za brzinu kretanja playera(karaktera u igrici)
    public Rigidbody2D theRB; //varijabla karaktera koju vezujemo u unity
    public float jumpForce; //varijabla za skakanje karaktera

    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;


    private bool canDoubleJump;

    private Animator anim;
    private SpriteRenderer theSR;

    public float knockBackLength, knockBackForce;
    private float knockBackCounter;

    public float bounceForce; // varijabla za odskakanje kada skocimo na enemya

    public bool stopInput;

    private void Awake()
    {
        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!PauseMenu.instance.isPaused && !stopInput) //ako nije pauzirana igrica i nismo rekli da moramo da prekinemo unos tastera igrac moze da se krece
        {

            if (knockBackCounter <= 0)
            {



                theRB.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), theRB.velocity.y); //sa ovim input.getAxis dobijamo da mozemo da krecemo karaktera uz pomoc tastera a i d ili strelice
                                                                                                            //dobijamo tako sto imamo vrednosti po horizontali -1 i 1
                                                                                                            //ako ne kliknemo nista onda je na 0 i karakter stoji a ako pomeramo u levo prelazi u -1 i mnozimo ga sa movespeed koji je 7.5 a to takodje vazi i za desnu stranu
                                                                                                            //ovo je predefinisano unutar unitya, unutar project seetings, input manager i imamo takodje i za vertical,fire1,jump...
                                                                                                            //input.getaxisraw ili getaxis obican svejedno je zavisi da li ce se u samom momentu zaustaviti player
                isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);


                if (Input.GetButtonDown("Jump"))
                {
                    if (isGrounded)
                    {

                        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                        canDoubleJump = true;
                        AudioManager.instance.PlaySFX(10);

                    }
                    else
                    {
                        if (canDoubleJump)
                        {
                            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                            canDoubleJump = false;
                            AudioManager.instance.PlaySFX(10);


                        }
                    }

                }

                if (theRB.velocity.x < 0)  // ovaj deo koda nam sluzi za to da vidimo da li je karakter kada kliknemo dugme okrenut levo ili desno
                {
                    theSR.flipX = true;
                }
                else if (theRB.velocity.x > 0)
                {
                    theSR.flipX = false;
                }
            }
            else
            {
                knockBackCounter -= Time.deltaTime;
                if(!theSR.flipX) // ako je false knockbackforce nas vraca u levo a ako je true onda u desno kada naletimo na prepreku
                {
                    theRB.velocity = new Vector2(-knockBackForce, theRB.velocity.y);
                }else
                {
                    theRB.velocity = new Vector2(knockBackForce, theRB.velocity.y);
                }

            }

        }
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
    }

    public void KnockBack()
    {
        knockBackCounter = knockBackLength;
        theRB.velocity = new Vector2(0f, knockBackForce);

        anim.SetTrigger("hurt");
    }

    public void Bounce() //pravimo funkciju za bounce
    {
        theRB.velocity = new Vector2(theRB.velocity.x, bounceForce);
        AudioManager.instance.PlaySFX(10);

    }
}
