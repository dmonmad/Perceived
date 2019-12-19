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
    public Texture defaultIcon;

    private bool itemIsDeleted;


    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        itemIsDeleted = false;
        empty = true;
        hovered = false;
        updateSlot();
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
        Debug.Log("ONPOINTER");
        if (item)
        {
            Debug.Log("ONPOINTER ITEM");

            //checking for item type
            if(item.GetComponent<Item>().type == "water")
            {
                player.GetComponent<PlayerController>().Drink(item.GetComponent<Item>().quantitySatisfied);
                Destroy(item);
                Debug.Log("UPDATE SLOT CALL##");
                itemIsDeleted = true;
                updateSlot();
            }
        }
    }

    private void updateSlot()
    {
        if (item && !itemIsDeleted)
        {
            Debug.Log("UPDATE SLOT NORMAL");
            empty = false;

            itemIcon = item.GetComponent<Item>().icon;

            this.GetComponent<RawImage>().texture = itemIcon;
        }
        else
        {
            Debug.Log("UPDATE SLOT ELSE");
            empty = true;
            itemIcon = null;
            this.GetComponent<RawImage>().texture = defaultIcon;
        }
    }

    public void setItem(GameObject item, Texture icon)
    {
        this.item = item;
        this.itemIcon = icon;
        itemIsDeleted = false;
        updateSlot();
    }
}
