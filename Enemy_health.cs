using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] Animator animator;             // allows animator to be attached within inspector (each enemy has its own animator)
    public AnimationClip deathClip;
    public bool enemyDied = false;
    //public string type;
    public float animationHelper;
    public bool canHit = true;


    //public AnimationClip deathClip;
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //Added a few checks 
    public void TakeDamage(int damage)
    {
        if (enemyDied != true && canHit) { 

            currentHealth -= damage;
            animator.SetTrigger("Hurt");                        // every enemy need to have trigger "Hurt" and bool "isDead"

            if (currentHealth <= 0) //death 
            {
                Debug.Log("Health: " + currentHealth);
                FindObjectOfType<AudioManager>().Play("EnemyDeath");
                animator.SetBool("isDead", true);
                enemyDied = true;
            }
        }
    }

    public IEnumerator death(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }

}
   
