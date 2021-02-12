using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Sprite redFlag;
    public Sprite greenFlag;
    private SpriteRenderer checkpointSpriteRenderer;
    public bool checkpointReached;
    // Start is called before the first frame update
    void Start()
    {
        checkpointSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerCollider2D(Collider2D other)
    {
        if( other.tag == "Player_1")
        {
            checkpointSpriteRenderer.sprite = greenFlag;
            checkpointReached = true;
        }
    }
}
