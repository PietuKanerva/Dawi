using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CountingCoins : MonoBehaviour
{
    public TMP_Text gold;
    private void Start()
    {
        
    }
    private void Awake()
    {
        gold.text = "You gathered " + PlayerHandler.gold.ToString() +" gold. \n And avenged "+ Enemy.grudgecount.ToString() + " grudges!";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
