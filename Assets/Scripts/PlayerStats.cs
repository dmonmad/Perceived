using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth, health;
    public float maxEnergy, energy;
    public float maxHunger, hunger;
    public float maxFoodSaturation, foodSaturation;
    public float maxThirstSaturation, thirstSaturation;
    public float baseAttackDamage, attackDamage;
    public float hungerRate, thirstRate;
    public int baseNoise, noise;

    PlayerController playerc;

    // Start is called before the first frame update
    void Start()
    {
        noise = baseNoise;
        playerc = GetComponent<PlayerController>();
        health = maxHealth;
        energy = maxEnergy;
        hunger = maxHunger;
        foodSaturation = maxFoodSaturation;
        thirstSaturation = maxThirstSaturation;
        attackDamage = baseAttackDamage;
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
