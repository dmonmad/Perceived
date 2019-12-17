using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventory;
    public GameObject slotHolder;

    public GameObject itemManager;

    private int slots;
    private Transform[] slot;
    private bool inventoryEnabled;

    private bool itemAdded;

    private GameObject itemPickedUp;


    // Start is called before the first frame update
    void Start()
    {
        slots = slotHolder.transform.childCount;
        slot = new Transform[slots];
        DetectInventorySlots();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            inventoryEnabled = !inventoryEnabled;
        }

        if (inventoryEnabled)
        {
            inventory.SetActive(true);
        }
        else
        {
            inventory.SetActive(false);
        }
    }



    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("ONENTER"+other.gameObject.name + " || " + other.gameObject.tag);
        if (other.gameObject.tag == "Item")
        {
            Debug.Log("WTF 111");
            itemPickedUp = other.gameObject;
            AddItem(itemPickedUp);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("ONEXIT" + other.gameObject.name + " || " + other.gameObject.tag);
        if (other.gameObject.tag == "Item"){
            Debug.Log("WTF 222");
            itemAdded = false;
        }
    }

    public void AddItem(GameObject item)
    {
        for(int i = 0; i< slots; i++)
        {
            if (slot[i].GetComponent<Slot>().empty && !itemAdded)
            {
                slot[i].GetComponent<Slot>().item = itemPickedUp;
                slot[i].GetComponent<Slot>().itemIcon = itemPickedUp.GetComponent<Item>().icon;

                item.transform.parent = itemManager.transform;
                item.transform.position = itemManager.transform.position;
                

                itemAdded = true;
            }
        }
    }

    void DetectInventorySlots()
    {
        for(int i=0; i<slots; i++)
        {
            slot[i] = slotHolder.transform.GetChild(i);
        }

    }
}
