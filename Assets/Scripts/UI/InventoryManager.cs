using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventory;
    public GameObject slotHolder;

    private int slots;
    private Transform[] slot;


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
        
    }

    public void AddItem(GameObject item)
    {

    }

    void DetectInventorySlots()
    {
        for(int i=0; i<slots; i++)
        {
            slot[i] = slotHolder.transform.GetChild(i);
            print(slot[i].name);
        }

    }
}
