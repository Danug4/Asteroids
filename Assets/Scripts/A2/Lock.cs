using System.Collections;
using UnityEngine;

public class Lock : MonoBehaviour
{
    //Player Touches key > Key held in front of player
    //Key touches lock > Lock disappears


    //Move in a direction
    //after a period of time, change directions
    public float moveSpeed;
    public float moveDir; //1 or -1 
    public float moveDirPeriod;
    public int scoreGained; 

    SpaceShip player;

    void Awake()
    {
        player = FindFirstObjectByType<SpaceShip>().GetComponent<SpaceShip>();
        StartCoroutine(ChangeDirection());

    }

    void OnTriggerEnter2D (Collider2D collider)
    {
        //Debug.Log("Hit tag: " + collider.gameObject.tag);

        //Remove key from player and destroy self (lock)
        player.RemoveKey();
        if (collider.gameObject.tag == "Key")
        {
            // Add score to the scoreUI script.
            player.score += scoreGained;
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.position += new Vector3(moveSpeed * moveDir * Time.deltaTime,0,0);
        
    }

    IEnumerator ChangeDirection()
    {
        while (player.isAlive == true)
        {
            yield return new WaitForSeconds(moveDirPeriod);
            moveDir = moveDir * -1; //Swap direction
            
        }
    }
}
