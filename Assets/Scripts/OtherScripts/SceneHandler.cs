using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneHandler : MonoBehaviour
{
    // Handles the scenes in the game. 
    public void Scene1()
    {
        SceneManager.LoadScene("Intro");
    }
    public void Dream()
    {
        SceneManager.LoadScene("Game");
    }
    public void SceneSelection()
    {    
        SceneManager.LoadScene("Scene2");
    }
    public void Merchant()
    {
        SceneManager.LoadScene("Merchant");
    }
    public void GameOver()
    {
        PlayerHandler player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
        Destroy(player.gameObject);
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("EndScreen");
    }
    public void Quit()
    {
        Application.Quit();
    }

}
