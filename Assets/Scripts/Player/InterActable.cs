using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InterActable : MonoBehaviour
{
    private GameObject raycastedObj;

    [SerializeField] private int rayLength = 5;
    [SerializeField] private LayerMask layerMaskInteract;

    SceneHandler sceneHandler;

    AudioSource audioSource1;
    AudioSource audioSource2;

    PlayerHandler player;
    CharacterController characterController;

    public TMP_Text startLore;
    public TMP_Text upgrade;
    public TMP_Text text;
    private int gold = 100;
    private int treasure = 500;


    private int treasuresCollected = 0;

    private bool canUpgrade = true;
    private bool firstTime = true;

    public string[] dialogue = new string[] { "Have you heard of the High Elves?"};

    // Remember to make gameobjects!!!!

    // Everything that has to do with interaction comes from here. Probably could be done easier. This solution works though.
    private void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
        characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }

    private void Update()
    {
        sceneHandler = GameObject.FindGameObjectWithTag("InputHandler").GetComponent<SceneHandler>();
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, forward, out hit, rayLength, layerMaskInteract.value))
        {
            if (hit.collider.CompareTag("Door"))
            {
                raycastedObj = hit.collider.gameObject;
                if (Input.GetKeyDown("e"))
                {
                    // Here is a second dont destroy, to ensure that the player character is the same when moving through the scenes.
                    DontDestroyOnLoad(player.gameObject);

                    // Somehow the characterController component stops the transform position from moving. Have to disable and re-enable it each time.

                    characterController.enabled = false;
                    player.transform.position = new Vector3(4.99942398f, 1.48999989f, -2.09883189f);
                    sceneHandler.Merchant();
                    characterController.enabled = true;
                }
            }
            else if (hit.collider.CompareTag("Door2"))
            {
                raycastedObj = hit.collider.gameObject;
                if (Input.GetKeyDown("e"))
                {
                    // Same as above, characterController has to be disabled and enabled.
                    text.text = "";
                    characterController.enabled = false;
                    player.transform.position = new Vector3(-14.1499996f, 2.61999989f, 0f);
                    sceneHandler.SceneSelection();
                    characterController.enabled = true;
                }
            }
            else if (hit.collider.CompareTag("Merchant"))
            {
                raycastedObj = hit.collider.gameObject;
                if (Input.GetKeyDown("e"))
                {
                    // Picks from a list of dialogue options and prints out a random one. Sets the text to "" after 3 seconds.
                    if (firstTime == true)
                    {
                        text.text = "Greetings fellow Dawi! I received word ahead of your arrival. \n Please, choose from the two available upgrades on the right. And do not forget your journal on the table!";
                        firstTime = false;
                    }
                    else
                    {
                        int i = Random.Range(0, dialogue.Length);
                        text.text = dialogue[i];
                        player.Heal();
                        StartCoroutine(WaitingTime());
                    }
                }
            }
            else if (hit.collider.CompareTag("EasterEgg"))
            {
                raycastedObj = hit.collider.gameObject;
                if (Input.GetKeyDown("e"))
                {
                    // A small easter egg here to change the music in the tavern.
                    audioSource1 = GameObject.FindGameObjectWithTag("EasterEgg").GetComponent<AudioSource>();
                    audioSource1.enabled = true;
                    audioSource2 = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
                    audioSource2.enabled = false;
                }
            }
            else if (hit.collider.CompareTag("Coins"))
            {
                raycastedObj = hit.collider.gameObject;
                if (Input.GetKeyDown("e"))
                {
                    // Picks the coins and destroys the object afterwards. Adds it to the current total.
                    player.currentCoins += gold;
                    Destroy(raycastedObj);
                }
            }
            else if (hit.collider.CompareTag("Door3"))
            {
                raycastedObj = hit.collider.gameObject;
                if (Input.GetKeyDown("e") && treasuresCollected >= 7)
                {
                    // Here on loadscene player character is no longer needed and it is destroyed. Also checks for the treasures.
                    Destroy(player.gameObject);
                    sceneHandler.GameOver();
                }
                else if (treasuresCollected < 7)
                {
                    text.text = "You still need to find all the treasures!";
                    StartCoroutine(WaitingTime());
                }
            }
            else if (hit.collider.CompareTag("Treasure"))
            {
                raycastedObj = hit.collider.gameObject;
                if (Input.GetKeyDown("e"))
                {
                    // Collecting all the treasures is needed to win the game. You also get a bonus for each.
                    player.currentCoins += treasure;
                    treasuresCollected++;
                    Debug.Log(treasuresCollected);
                    Destroy(raycastedObj);
                }
            }
            else if (hit.collider.CompareTag("Book"))
            {
                raycastedObj = hit.collider.gameObject;
                if (Input.GetKeyDown("e"))
                {
                    // Activated by inteacting with the book on the counter. Tells some lore to flesh out the world.
                    startLore.text = "The nightmare was the last straw. You have made the long and ardous trek to Karak Norn. Stopping in the last inn before the end of civilization, you take a moment to rest before plunging in to the depths of the lost dwarfhold. \n To erase the shame of that day you need to find 7 treasures that your ancestors had to abandon. \n Grab the door and strike this grudge from history! \n \n And if you happen to find some gold and avenge a few grudges...";
                    StartCoroutine(ClearTime());
                }
            }
            else if (hit.collider.CompareTag("Upgrade1"))
            {
                upgrade.text = "Increases the muskets damage by 50. \n You can only choose one.";
                raycastedObj = hit.collider.gameObject;
                if(canUpgrade == false)
                {
                    upgrade.text = "";
                }
                else if (Input.GetKeyDown("e") & canUpgrade == true )
                {
                    Musket.damage += 50;
                    canUpgrade = false;
                }
                
            }
            else if (hit.collider.CompareTag("Upgrade2"))
            {
                raycastedObj = hit.collider.gameObject;
                upgrade.text = "Decreases the reload speed by 2 seconds. \n You can only choose one.";
                if (canUpgrade == false)
                {
                    upgrade.text = "";
                }
                else if (Input.GetKeyDown("e") & canUpgrade == true )
                {        
                    Musket.reloadSpeed = 1f;
                    canUpgrade = false;
                }
            }
            
        }
    }
    IEnumerator WaitingTime()
    {
        yield return new WaitForSecondsRealtime(3);
        text.text = "";
    }
    IEnumerator ClearTime()
    {
        yield return new WaitForSecondsRealtime(20);
        startLore.text = "";
    }

}
