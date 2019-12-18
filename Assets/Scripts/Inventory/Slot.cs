using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public bool hovered;
    public bool empty;

    public GameObject item;
    public Texture itemIcon;

    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        empty = true;
        hovered = false;
        player = GameObject.FindWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item)
        {
            Item thisItem = item.GetComponent<Item>();

            //checking for item type
            if(thisItem.type == "water")
            {
                player.GetComponent<PlayerController>().Drink(thisItem.quantitySatisfied);
                Destroy(item);
                updateSlot();
            }
        }
    }

    internal void updateSlot()
    {
        if (item)
        {

            empty = false;

            itemIcon = item.GetComponent<Item>().icon;

            this.GetComponent<RawImage>().texture = itemIcon;
        }
        else
        {
            empty = true;
            this.GetComponent<RawImage>().texture = null;
        }
    }
}
