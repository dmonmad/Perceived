using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public float energy;
    public float hunger;
    public float attackDamage;

    PlayerController playerc;

    // Start is called before the first frame update
    void Start()
    {
        playerc = GetComponent<PlayerController>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDamage(float damage)
    {

        Debug.Log("PLAYER / GET DAMAGE " + damage);
        if (health > 0)
        {
            health -= damage;

        }
        else
        {
            playerc.Die();
        }


    }

}
