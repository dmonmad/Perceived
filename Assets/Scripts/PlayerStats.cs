using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public int health, thirst, hunger, energy;
    public int maxHealth, maxThirst, maxHunger, maxEnergy;
    public int maxFoodSaturation, maxThirstSaturation;
    public int hungerRate, thirstRate;
    public int foodSaturation, thirstSaturation;
    public float baseAttackDamage, attackDamage;
    public int baseNoise, noise;
    public TextMeshProUGUI healthText, thirstText, hungerText;


    PlayerController playerc;

    // Start is called before the first frame update
    void Start()
    {
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

        thirst -= (int)Time.deltaTime * thirstRate;
        hunger -= (int)Time.deltaTime * hungerRate;
        updateThirst();
        updateHunger();


    }

    public void GetDamage(int damage)
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

    public void updateThirst()
    {
        thirstText.SetText(thirst.ToString());
    }

    public void updateHunger()
    {
        hungerText.SetText(hunger.ToString());
    }

    

}
