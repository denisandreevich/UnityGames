using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject damageText;
    public int health;
    public float speed;
    public int damage;
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public float startStopTime;

    private Player player;
    private Animator anim;


    private void Start() {


        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }
    private void Update() {
        if(player.transform.position.x > transform.position.x){
            transform.eulerAngles = new Vector3(0, 180, 0);
        }else {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    public void TakeDamage(int damage){
        health -= damage;
        UnityEngine.Vector2 damagePos = new UnityEngine.Vector2(transform.position.x, transform.position.y + 2.75f);
        Instantiate(damageText, damagePos, UnityEngine.Quaternion.identity);
        damageText.GetComponentInChildren<DamageText>().damage = damage;
        if(health < 1){
            Destroy(gameObject);
        }
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
        player.ChangeHealth(-damage);
        timeBtwAttack = startTimeBtwAttack;
        
    }
}
