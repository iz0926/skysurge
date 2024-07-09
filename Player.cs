using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    Rigidbody2D body;

    public float upwardForce = 50f;
    public float downwardForce = -20f; // Adjust to control the fall speed
    public float gravityScale = 2f; 
    private int coinCount = 0;
    public TextMeshProUGUI coinCountText;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = gravityScale; // adjust gravity scale here if needed
        UpdateCoinCountText();
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            body.AddForce(new Vector2(0, upwardForce), ForceMode2D.Force);
        } 
        else
        {
            // apply a smooth downward force
            body.AddForce(new Vector2(0, downwardForce), ForceMode2D.Force);
        }
    }

    // when the player collides with the objects that have the tag 'Coin', then remove the object and increase the coinCount by 1
    // this will call UpdateCoinCountText, which will update the coin count on the screen
    // the game screen's "Coins:" display is a TextMeshProGUI object
    void OnTriggerEnter2D(Collider2D collider)
    {
       if (collider.gameObject.CompareTag("Coin"))
        {
            Destroy(collider.gameObject);
            coinCount++;
            UpdateCoinCountText();
        }
    }

    void UpdateCoinCountText()
    {
        coinCountText.text = "Coins: " + coinCount;
    }
}
