using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : MonoBehaviour
{
    private LevelManager gameLevelManager;
    public int coinValue;
    // Start is called before the first frame update
    void Start()
    {
        gameLevelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player_1")
        {
            gameLevelManager.AddCoins(coinValue);
            Destroy(gameObject);
        }
        
    }
}
