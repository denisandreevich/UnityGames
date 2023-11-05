using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public GameObject block;
    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Wall")){
            Instantiate(block, transform.GetChild(0).position, Quaternion.identity);
            Instantiate(block, transform.GetChild(1).position, Quaternion.identity);
            Instantiate(block, transform.GetChild(2).position, Quaternion.identity);
            Instantiate(block, transform.GetChild(3).position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
