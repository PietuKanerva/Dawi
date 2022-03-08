using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class PlayerHandler : MonoBehaviour
{
 
    [Header("Character Movement Stats")]
    public CharacterController controller;
    private Musket musket;

    #region CharacterController
    private float speed;
    private float normalSpeed = 8f;
    private float runSpeed = 12f;
    private float gravity = -9.81f;
    public Transform groundCheck;
    private float groundDistance = 0.4f;
    private LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;
    public SceneHandler sceneHandler;

    private bool running = false;
    #endregion
    [Header("Player Health")]
    public float maxhealth = 50f;
    public float currenthealth;
    bool canDMG = true;

    Enemy enemy;

    [Header("Coins and sich.")]

    public int coins = 100;
    public int currentCoins;
    public static int gold = 0;
    public Text coinCount;
    public TMP_Text health;


    // Setting up the game, getting some of the needed components.

    private void Start()
    {
        currenthealth = maxhealth;
        musket = GameObject.FindGameObjectWithTag("Musket").GetComponent<Musket>();
        coinCount.text = "Amount of gold: " + currentCoins.ToString();
        health.text = currenthealth.ToString();
        Heal();   
    }
    void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            musket.Shoot();
        }
        coinCount.text = "Amount of gold: " + currentCoins.ToString();
        health.text = "Health " + currenthealth.ToString();
        gold = currentCoins;
    }
    #region Movement
    private void Movement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            running = true;
            speed = runSpeed;
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
        else {
            running = false;
            speed = normalSpeed;
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }

    }
    #endregion
    #region Taking Damage
    //Player takes this amount of damage. Comes from Enemy.
    public void TakeDamage(float damage)
    {
        if (canDMG)
        {
            canDMG = false;
            currenthealth -= damage;
            StartCoroutine(WaitingTime());
        }
        else if (currenthealth <= 0)
        {
            Destroy(this);
            sceneHandler.GameOver();
        }
    }
    // Taking the damage
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Zombie")
        {
            enemy = GameObject.Find("Zombie(Clone)").GetComponent<Enemy>();
            TakeDamage(enemy.enemyDamage);
            Debug.Log(currenthealth);
        }
    }
#endregion
    #region Healing damage
    // Healing to maximum amount of HP.
    public void Heal()
    {
        currenthealth = maxhealth;
    }
#endregion
    IEnumerator WaitingTime()
    {
        yield return new WaitForSeconds(2);
        canDMG = true;
    }

    
}
