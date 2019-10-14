using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStats : MonoBehaviour
{

    public float zombiehealth;
    public float damageMultiplier;
    

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void GetDamage(float damage)
    {
        if(zombiehealth > 0)
        {
            zombiehealth -= damage * damageMultiplier;
            if(zombiehealth < 0)
            {
                Die();
            }
        }

        
    }

    void Die()
    {
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
    
}
