using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public float respawnDelay;
    public Movement gamePlayer;
    public int coins;
    public Text gemText;

    // Start is called before the first frame update
    void Start()
    {
        gamePlayer = FindObjectOfType<Movement> ();
        gemText.text = "Gems: " + coins;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        StartCoroutine("RespawnCoroutine");
    }

    public IEnumerator RespawnCoroutine()
    {
        gamePlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(respawnDelay);
        gamePlayer.transform.position = gamePlayer.respawnPoint;
        gamePlayer.gameObject.SetActive(true);
    }

    public void AddCoins(int numberOfCoins)
    {
        coins += numberOfCoins;
        gemText.text = "Gems: " + coins;
    }
}
