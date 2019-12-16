using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerStats : MonoBehaviour
{
    public float health, thirst, hunger, energy;
    public float maxHealth, maxThirst, maxHunger, maxEnergy;
    public float maxFoodSaturation, maxThirstSaturation;
    public float hungerRate, thirstRate;
    public float foodSaturation, thirstSaturation;
    public float baseAttackDamage, attackDamage;
    public int baseNoise, noise;
    public TextMeshProUGUI healthText, thirstText, hungerText;

    Boolean isDead;
    PlayerController playerc;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        noise = baseNoise;
        playerc = GetComponent<PlayerController>();
        health = maxHealth;
        energy = maxEnergy;
        hunger = maxHunger;
        thirst = maxThirst;
        foodSaturation = maxFoodSaturation;
        thirstSaturation = maxThirstSaturation;
        attackDamage = baseAttackDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            thirst -= Time.deltaTime * thirstRate;
            hunger -= Time.deltaTime * hungerRate;
            updateThirst();
            updateHunger();
            updateHealth();
        }
        


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

    private void updateHealth()
    {
        healthText.SetText(health.ToString());
    }

    private void updateThirst()
    {
        thirstText.SetText(thirst.ToString());
    }

    private void updateHunger()
    {
        hungerText.SetText(hunger.ToString());
    }

    private void killPlayer()
    {
        isDead = true;
        playerc.Die();
    }

    

}
