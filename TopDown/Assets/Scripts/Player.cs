using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    public Joystick joystick;
    private Animator anim;
    public float health;
    public int numOfHearts;
    public UnityEngine.UI.Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public float heal;

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
        if (health > numOfHearts){
            health = numOfHearts;
        }health += Time.deltaTime * heal;
        for (int i = 0; i < hearts.Length; i++){
            if(i < Mathf.RoundToInt(health)){
                hearts[i].sprite = fullHeart;
            }else{
                hearts[i].sprite = emptyHeart;
            }if(i < numOfHearts){
                hearts[i].enabled = true;
            }else {
                hearts[i].enabled = false;
            }if (health < 1){
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

        }
    }
    private void Flip(){
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
