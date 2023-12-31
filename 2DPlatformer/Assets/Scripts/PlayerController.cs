using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public Joystick joystick;
    public VectorValue pos;
    private void Start() {
        transform.position = pos.initialValue;
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate() {
        moveInput = joystick.Horizontal;
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if(facingRight == false && moveInput > 0){
            Flip();
        }
        else if(facingRight == true && moveInput < 0){
            Flip();
        }
    }
    private void Update() {
        float verticalMove = joystick.Vertical;
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if(isGrounded == true && verticalMove >= .5f){
            rb.velocity = Vector2.up * jumpForce;
        }
    }
    void Flip(){
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    

}
