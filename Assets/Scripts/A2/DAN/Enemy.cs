using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    
    public GameObject spaceShip;
    
    Rigidbody2D rb2D;

    Vector2 spaceShipPos;

    public float movementSpeed;

    public float detectionRange;

    public float healthMax;
    float healthCurrent;

    public float damage;


    //Get player position
    //Find Current Position
    //Add force towards player 
    private void Awake()
    {
        //spaceShip = GameObject.FindGameObjectWithTag("Player");
        rb2D = GetComponent<Rigidbody2D>();

        healthCurrent = healthMax;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //Destroy bullet
            Destroy(collision.gameObject);

            // take damage, if remaining health <= 0, die
            healthCurrent -= collision.gameObject.GetComponent<Bullet>().bulletDamage;
            if (healthCurrent <= 0)
            {
                Death();
            }
        }

        if (collision.gameObject.tag == "Player")
        {
            //Inflict damage upon player
            spaceShip.GetComponent<SpaceShip>().TakeDamage(damage);
        }
    }

    void FixedUpdate()
    {
        // Find distance to player 
        float playerDistance = FindDistanceToPlayer();

        // If distance < threshold: home towards player 
        if (playerDistance < detectionRange)
        {
            HomeTowardsPlayer();
        }
    }
    float FindDistanceToPlayer()
    {
        float xDim = Mathf.Pow(transform.position.x - spaceShip.transform.position.x,2);
        float yDim = Mathf.Pow(transform.position.y - spaceShip.transform.position.y,2);

        float distance = Mathf.Sqrt(xDim + yDim);

        return distance;
    }
    void HomeTowardsPlayer()
    {
        //Find direction
        spaceShipPos = new Vector2(spaceShip.transform.position.x, spaceShip.transform.position.y);
        Vector2 direction = new Vector2(spaceShipPos.x - transform.position.x, spaceShipPos.y - transform.position.y);

        //Turn towards Space Ship
        float turnAngle = FindTurnAngle(direction);
        transform.rotation = Quaternion.Euler(0f, 0f, turnAngle);

        //Calculate and apply force
        Vector2 force = direction * Time.deltaTime * movementSpeed;
        rb2D.AddForce(force);
    }
    float FindTurnAngle(Vector2 direction)
    {
        float turnAngleRadians = Mathf.Atan2(direction.y, direction.x);
        float turnAngle = Mathf.Rad2Deg * turnAngleRadians;

        return turnAngle;
    }

    void Death()
    {
        Destroy(gameObject);
    }

}
