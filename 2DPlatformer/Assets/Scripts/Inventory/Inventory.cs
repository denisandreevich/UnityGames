using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public GameObject inventory;
    private bool InventoryOn;
    private void Start() {
        InventoryOn = false;
    }
    public void Chest(){
        if(InventoryOn == false){
            InventoryOn = true;
            inventory.SetActive(true);
        }
        else if (InventoryOn == true){
            InventoryOn = false;
            inventory.SetActive(false);
        }
    }
}
