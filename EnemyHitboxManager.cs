using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitboxManager : MonoBehaviour
{
    
    public Transform AttackPoint; 
    public bool fist = false;
    public bool slam = false;

    //Communicates to player health script for animation  
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            if (AttackPoint.name.ToString() == "FistAttackPoint1" || AttackPoint.name.ToString() == "FistAttackPoint2")
            {
                fist = true;
                
            }
            else if (AttackPoint.name.ToString() == "SlamAttackPoint") 
            {
                slam = true;
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (AttackPoint.ToString() == "FistAttackPoint1" || AttackPoint.ToString() == "FistAttackPoint2")
            {
                fist = false;
            }
            else if (AttackPoint.name.ToString() == "SlamAttackPoint")
            {
                slam = false;
            }
        }
    }
}
