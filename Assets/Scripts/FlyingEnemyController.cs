using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    public Transform[] points;
    public float moveSpeed;
    private int currentPoint;


    public SpriteRenderer theSR;

    public float distanceToAttackPlayer, chaseSpeed;

    private Vector3 attackTarget;

    public float waitAfterAttack;
    private float attackCounter;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < points.Length; i++)
        {
            points[i].parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (attackCounter > 0)
        {
            attackCounter -= Time.deltaTime;
        }
        else
        {

            if(Vector3.Distance(transform.position, PlayerController.instance.transform.position) > distanceToAttackPlayer)
            {
                attackTarget = Vector3.zero;
                transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, moveSpeed); //trenutna pozicija, gde ide i kojom brzinom

                if (Vector3.Distance(transform.position, points[currentPoint].position) < .05f)
                {
                    currentPoint++;

                    if (currentPoint >= points.Length)
                    {
                        currentPoint = 0;
                    }
                }


                if (transform.position.x < points[currentPoint].position.x) //menjanje strana kako se krece nas neprijatelj
                {
                    theSR.flipX = true;

                }else if (transform.position.x > points[currentPoint].position.x)
                {
                    theSR.flipX = false;
                }
            } 
            else
            {
                if(attackTarget == Vector3.zero)
                {
                    attackTarget = PlayerController.instance.transform.position; //dolazi do pointa gde se nasao igrac u trenutku kada je usao u polje gde se krece neprijatelj
                }
            
                transform.position = Vector3.MoveTowards(transform.position, attackTarget, chaseSpeed * Time.deltaTime);

                if(Vector3.Distance(transform.position, attackTarget) <= .1f)
                {              
                    attackCounter = waitAfterAttack;
                    attackTarget = Vector3.zero;
                }


                if (transform.position.x < PlayerController.instance.transform.position.x)
                {
                    theSR.flipX = true;
                }
                else if (transform.position.x > PlayerController.instance.transform.position.x)
                {
                    theSR.flipX = false;
                }

            }
        }
    }
}
