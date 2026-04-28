using UnityEngine;

public class Lock : MonoBehaviour
{
    //Player Touches key > Key held in front of player
    //Key touches lock > Lock disappears

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
            Destroy(gameObject);
        }
    }
}
