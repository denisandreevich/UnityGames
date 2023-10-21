using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public Joystick joystick;
    public float speed;

    [Header("Health")]
    public Text healthDisplay;
    public float health;
    public GameObject shield;
    public Shield shieldTimer;
    public float removeAmount;

    [Header("Weapons")]
    public List<GameObject> unlockedWeapons;
    public GameObject[] allWeapons;
    public UnityEngine.UI.Image weaponIcon;


    private Animator anim;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;


    private bool facingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        moveVelocity = moveInput.normalized * speed;
        if(moveInput.x == 0){
            anim.SetBool("isRuning", false);
        } else {
            anim.SetBool("isRuning", true);
        }
        if(!facingRight && moveInput.x > 0){
            Flip();
        } else if(facingRight && moveInput.x < 0){
            Flip();
        }
    }
    private void FixedUpdate() {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
    private void Flip(){
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Potion")){
            ChangeHealth(5);
            Destroy(other.gameObject);
        } else if(other.CompareTag("Shield")){
            if(!shield.activeInHierarchy)
            {   shield.SetActive(true);
                shieldTimer.gameObject.SetActive(true);
                shieldTimer.isCooldown = true;
                Destroy(other.gameObject);
            } else {
                shieldTimer.ResetTimer();
                Destroy(other.gameObject);
            }
        } else if(other.CompareTag("Weapon")){
            for (int i = 0; i < allWeapons.Length; i++){
                if(other.name == allWeapons[i].name){
                    unlockedWeapons.Add(allWeapons[i]);
                }
            } 
            SwitchWeapon();
            Destroy(other.gameObject);
        } 
    }
    public void ChangeHealth(int healthValue){
        if(!shield.activeInHierarchy || shield.activeInHierarchy && healthValue > 0)
        {   health += healthValue;
            healthDisplay.text = "HP:" + health;
        } else if(shield.activeInHierarchy && healthValue < 0){
            shieldTimer.ReduceTime(healthValue);
        }
        
        if (health < 1){
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void SwitchWeapon(){
        for (int i = 0; i < unlockedWeapons.Count; i++){
            if(unlockedWeapons[i].activeInHierarchy){
                unlockedWeapons[i].SetActive(false);
                if(i != 0){
                    unlockedWeapons[i - 1].SetActive(true);
                    weaponIcon.sprite = unlockedWeapons[i - 1].GetComponent<SpriteRenderer>().sprite;
                } else {
                    unlockedWeapons[unlockedWeapons.Count - 1].SetActive(true);
                    weaponIcon.sprite = unlockedWeapons[unlockedWeapons.Count - 1].GetComponent<SpriteRenderer>().sprite;
                } 
                break;
            }
        }
    }
}

