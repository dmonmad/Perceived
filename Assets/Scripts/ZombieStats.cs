using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStats : MonoBehaviour
{

    public float zombiehealth;
    public float damageMultiplier;
    public ZombieAI aizomb;

    void Start()
    {
        
        aizomb = GetComponent<ZombieAI>();
    }

    
    void Update()
    {
        
    }

    public void GetDamage(float damage)
    {
        Debug.Log("ZOMB // Got hit");
        if(zombiehealth > 0)
        {
            zombiehealth -= damage * damageMultiplier;
            if(zombiehealth < 0)
            {
                aizomb.Die();
            }
        }
    }

   
    
}
