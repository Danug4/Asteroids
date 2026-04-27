using UnityEngine;

public class Lock : MonoBehaviour
{
    //Player Touches key > Key held in front of player
    //Key touches lock > Lock disappears

    SpaceShip player;

    void OnTriggerEnter2D (Collider2D collider)
    {
        Debug.Log("Hit tag: " + collider.gameObject.tag);
        if (collider.gameObject.tag == "Key")
        {
            Destroy(gameObject);
        }
    }
}
