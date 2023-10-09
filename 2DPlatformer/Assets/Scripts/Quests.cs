using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quests : MonoBehaviour
{
    public int questNumber;
    public int[] items;
    public GameObject[] clouds;
    public GameObject barrier;
    public GameObject key;
    public void OnTriggerEnter2D(Collider2D other) {
        if(other.tag != "Player" && other.gameObject.GetComponent<PickUp>().id == items[questNumber])
    {
        questNumber++;
        Destroy(other.gameObject);
        CheckQuest();
    }
        
    }
    public void CheckQuest(){
        for (int i = 0; i < clouds.Length; i++){
            if(i == questNumber){
                clouds[i].SetActive(true);
                clouds[i].GetComponent<Animator>().SetTrigger("isTriggered");
                break;
            }
            else{
                clouds[i].SetActive(false);
            }
        }
        if(questNumber == 1){
            barrier.SetActive(false);
        }
        if(questNumber == 2){
            key.SetActive(true);
        }
    }
}
