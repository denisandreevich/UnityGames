using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [HideInInspector] public float damage;
    public GameObject textParent;
    private TextMesh textMesh;

    private void Start() {
        textMesh = GetComponent<TextMesh>();
        textMesh.text = "-" + damage;
    }

    public void OnAnimationOver(){
        Destroy(gameObject);
        Destroy(textParent.gameObject);
    }
}
