using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestWall : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

   public void OnTriggerEnter2D(Collider2D other) {
    if(other.tag != "Player" && other.GetComponent<PickUp>().id == 1){
        Destroy(other.gameObject);
        anim.SetTrigger("isTriggered");
    }
    
   }
}
