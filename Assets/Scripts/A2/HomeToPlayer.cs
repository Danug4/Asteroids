using UnityEngine;

public class HomeToPlayer : MonoBehaviour
{
    public GameObject player;
    public float speed;

    void Start()
    {
        player = FindAnyObjectByType<SpaceShip>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        Vector2 direction = (Vector2)player.transform.position - (Vector2)transform.position;

        if (direction.sqrMagnitude < Mathf.Epsilon)
            return;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}