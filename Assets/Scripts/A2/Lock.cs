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

    SpaceShip player;

    void Awake()
    {
        player = FindFirstObjectByType<SpaceShip>().GetComponent<SpaceShip>();

    }

    void OnTriggerEnter2D (Collider2D collider)
    {
        //Debug.Log("Hit tag: " + collider.gameObject.tag);

        //Remove key from player and destroy self (lock)
        player.RemoveKey();
        if (collider.gameObject.tag == "Key")
        {
            // Add score to the scoreUI script.
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.position.x += moveSpeed * moveDir * Time.deltaTime; 
    }

    IEnumerator ChangeDirection()
    {
        while (player.isAlive == true)
        {
            moveDir = moveDir * -1; //Swap direction
            yield return new WaitForSeconds(moveDirPeriod);
        }
    }
}
