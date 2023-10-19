using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public int damage;
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    private float stopTime;
    public float startStopTime;
    public float normalSpeed;
    private Player player;
    private Animator anim;
    private ScoreManager sm;

    private void Start() {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        normalSpeed = speed;
        sm = FindObjectOfType<ScoreManager>();
    }
    private void Update() {
        if(stopTime <= 0){
            speed = normalSpeed;
        }else{
            speed = 0;
            stopTime -= Time.deltaTime;
        }
        if(health <= 0){
            sm.Kill();
            Destroy(gameObject);
        }
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    public void TakeDamage(int damage){
        stopTime = startStopTime;
        health -= damage;
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player")){
            if(timeBtwAttack <= 0){
                anim.SetTrigger("enemyAttack");
            }else{
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }
    public void OnEnemyAttack(){
        player.health -= damage;
        timeBtwAttack = startTimeBtwAttack;
    }
}
