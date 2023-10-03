using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAnimator : MonoBehaviour
{
    public Animator startAnim;
    public DialogueManager dm;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            startAnim.SetBool("startOpen", true);
        }
    }
     public void OnTriggerExit2D(Collider2D other) {
        startAnim.SetBool("startOpen", false);
        dm.EndDialogue();
    }
}
