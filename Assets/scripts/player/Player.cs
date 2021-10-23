using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    //public  GameObject healthBar;
    public static int side;
    private  HealthBar healthBar;
    public static int currentHP;
    public static int currentEP;
    public static int PlayerShootSpeed;
    public static int PlayerShootPower;
    public static int PlayerMoveSpeed;
    public static List<Card> currentWeapon;
    private PhotonView photonView;
    /*public HealthBar healthBarOne;
    public HealthBar healthBarTwo;*/
    //public Weaponbase Weapon;
    void Start()
    {
        if(transform.position.x > 0)
            side = 1;
        else
            side = -1;
        photonView = GetComponent<PhotonView>();
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
        //healthBar.SetMaxHealth(maxHealth);
        currentHP = maxHealth;
      //  Weapon = GameObject.Find("CanvasThree").GetComponentInChildren(typeof(Weaponbase)) as Weaponbase;
        // currentHealth = maxHealth;
        // healthBar.SetMaxHealth(maxHealth);
        /*healthBarOne = GameObject.Find("CanvasOne").GetComponentInChildren(typeof(HealthBar)) as HealthBar;;
        healthBarTwo = GameObject.Find("CanvasTwo").GetComponentInChildren(typeof(HealthBar)) as HealthBar;
        healthBarOne.SetMaxHealth(maxHealth);
        healthBarTwo.SetMaxHealth(maxHealth);*/
    }
    public Rigidbody2D rb;
    public Camera cam;
    // Update is called once per frame

    Vector3 playerPosition;
    Vector2 movement;
    Vector2 mousePos;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            TakeDamage(20);
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        playerPosition = this.transform.position;
        mousePos.x = mousePos.x - playerPosition.x;
        mousePos.y = mousePos.y - playerPosition.y;

        healthBar.SetHealth(currentHP);
    }

    /*void FixedUpdate()
    {
        Vector2 lookDir = mousePos;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }*/

    void OnCollisionEnter2D(Collision2D other) {
        
        bool isHitByBullet = other.gameObject.tag == "Bullet";
        //if player is hit, destroy bullet and change healthBar
        if (isHitByBullet) {
            //Debug.Log("is hit! "+name);
            bullet_property b_p = other.gameObject.GetComponent<bullet_property>();
            TakeDamage(2*ChangeWeapon.bulletPower);
            Destroy(other.gameObject, 0.0f);
        }
    }

    void TakeDamage(int damage)
    {
        //Debug.Log("take damage");   
        if(healthBar == null)
            return;
        int currentHealth = healthBar.GetHealth();
        healthBar.SetHealth(currentHealth - damage);
        currentHP = healthBar.GetHealth();
        Debug.Log("Current Health: "+currentHP);
        if (currentHP <= 0){
            SceneManager.LoadScene("LoseScene");
            PhotonNetwork.Disconnect();
        }
    }

}
