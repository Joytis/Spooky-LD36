using UnityEngine;
using System.Collections;
using System;

public class InventoryManager : MonoBehaviour {

    private GameObject[] invItem;
    private Vector3[] slotPosition;
    
    // Use this for initialization
	void Awake ()
    {
        invItem = new GameObject[7];
        invItem[0] = null;
        slotPosition = new Vector3[7];
        slotPosition[6] = transform.position + new Vector3(3f, 0f, 0f);
        for (int i = 5; i >= 0; i--)
        {
            slotPosition[i] = slotPosition[i+1] - new Vector3(1f, 0f, 0f);
            invItem[i] = null;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        slotPosition[6] = transform.position + new Vector3(3f, 0f, 0f);
        for (int i = 5; i >= 0; i--)
        {
            slotPosition[i] = slotPosition[i + 1] - new Vector3(1f, 0f, 0f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "piece")
        {
            if (placeItemInInventory(other.gameObject))
            {
                Debug.Log("motherf");
                other.gameObject.SendMessage("SnapToInventory", slotPosition[Array.IndexOf(invItem, other.gameObject)]);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OUT AF");
        if (other.gameObject.tag == "piece")
        {
            if (removeItemFromInventory(other.gameObject))
            {
                other.gameObject.SendMessage("ExitInventory");
            }
        }
    }

    bool placeItemInInventory(GameObject item)
    {
        for(int i=6; i >= 0; i--)
        {
            if(invItem[i] == null)
            {
                invItem[i] = item;
                item.transform.SetParent(gameObject.transform);
                return true;
            }
        }

        return false;
    }

    bool removeItemFromInventory(GameObject item)
    {
        for(int i=0; i<7; i++)
        {
            if(invItem[i] != null && invItem[i].Equals(item))
            {
                invItem[i] = null;
                item.transform.SetParent(null);
                return true;
            }
        }

        return false;
    }
}
