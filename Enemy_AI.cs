using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy_AI : MonoBehaviour
{
    //health 
    [SerializeField] Enemy_health enemy_health;

    // Movement var
    Transform playerTransform;
    NavMeshAgent myNavMesh;
    private bool facingRight = false;

    //animate movement 
    [SerializeField] Animator animate;


    // attack
    private float dist;
    public int primaryDamage;
    public int secondaryDamage;
    public Vector3 enemyScale;

    GameObject player;

    bool notDead = true;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    // Start is called before the first frame update
    void Start()
    {

        if (GameObject.FindGameObjectWithTag("Player").activeInHierarchy)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

            myNavMesh = gameObject.GetComponent<NavMeshAgent>();
            animate = GetComponent<Animator>();
        }

    }


    void Update()
    {
        if (!Player.pepe.isDead && notDead)
        {

            EnemyFlip();
            followPlayer();


            if (dist <= myNavMesh.stoppingDistance)
            {
                animate.SetBool("walk", false);
                if (GetComponentInChildren<EnemiesHitBox>().getCollide())
                {
                    AttackState();
                }
            }
            else
            {
                animate.SetBool("walk", true);
            }

        }
        else if(Player.pepe.isDead)
        {
            animate.SetInteger("state", 0);
        }

        if (enemy_health.currentHealth <= 0)
        {
            DeathAxisCorrection();
            notDead = false;
        } 
    }
    public void EnemyFlip()
    {
        //Face to the left 
        if (transform.position.x < GameObject.FindGameObjectWithTag("Player").transform.position.x && facingRight)
        {
            transform.localScale = new Vector3(enemyScale.x * -1, enemyScale.y, enemyScale.z);
            facingRight = false;

        }
        //Fade to the right
        else if (transform.position.x > GameObject.FindGameObjectWithTag("Player").transform.position.x && !facingRight)
        {

            transform.localScale = new Vector3(enemyScale.x, enemyScale.y, enemyScale.z);
            facingRight = true;
        }
    }

    void followPlayer()
    {
        animate.SetInteger("state", 1);
        myNavMesh.destination = playerTransform.position;

        transform.rotation = Quaternion.Euler(0, 0, 0);
        dist = Vector3.Distance(playerTransform.position, transform.position);
    }

    public void AttackState()
    {
        // do animation if player in hitbox

        if(GetComponentInChildren<EnemiesHitBox>().GetAttackType() == "primaryAttack")
        {
            animate.SetInteger("state", 4);
 
        }
        else if (GetComponentInChildren<EnemiesHitBox>().GetAttackType() == "secondaryAttack")
        {
            animate.SetInteger("state", 5);
        }
        // cause damage to player
        BeginAttackEvent(GetComponentInChildren<EnemiesHitBox>().GetAttackType());
    }

    IEnumerator waitForAnimation(float seconds, int damage)
    {
        yield return new WaitForSeconds(seconds);
        player.gameObject.GetComponent<Player>().TakeDamage(damage);
        //animate.SetInteger("state", 1);
    }
    // prevent BB from falling on pepe
    void DeathAxisCorrection()
    {
        if (facingRight)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }


    //syncs with hitbox animation 
    void BeginAttackEvent(string attackType)
    {
        if (GetComponentInChildren<EnemiesHitBox>().getCollide())
        {
            if (attackType == "primaryAttack")
            {
                player.gameObject.GetComponent<Player>().TakeDamage(primaryDamage);

            }
            else if (attackType == "secondaryAttack")
            {
                player.gameObject.GetComponent<Player>().TakeDamage(secondaryDamage);
            }
        }

    }

}

