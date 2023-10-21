using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float offset;
    public GunType gunType;
    public GameObject bullet;
    public Transform shotPoint;
    private float timeBtwShots;
    public float startTimeBtwShots;
    private Player player;
    private Vector3 difference;
    private float rotZ;
    public Joystick joystick;
    public enum GunType{Default, Enemy}
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    void Update()
    { 
        if(gunType == GunType.Default){
            if(Mathf.Abs(joystick.Horizontal) > 0.3f || Mathf.Abs(joystick.Vertical) > 0.3f ){
            rotZ = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;}
        } else if(gunType == GunType.Enemy){
            difference = player.transform.position - transform.position;
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset); 
        if(timeBtwShots <= 0){
        if(gunType == GunType.Enemy){
            Shoot();
        }
        else if(joystick.Horizontal != 0 || joystick.Vertical != 0){
            Shoot();
        }} else{
            timeBtwShots -= Time.deltaTime;
        } 
        
    }
    public void Shoot(){
Instantiate(bullet, shotPoint.position, shotPoint.rotation);
            timeBtwShots = startTimeBtwShots;
    }
}
