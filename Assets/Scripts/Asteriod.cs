using UnityEngine;

public class Asteriod : MonoBehaviour
{
    public float collisionDamage = 1f;

    public float healthMax = 3f;
    public float healthCurrent;

    public GameObject explodeParticle;

    private void Start()
    {
        healthCurrent = healthMax;
    }

     private void OnCollisionEnter2D(Collision2D collision)
    {

        //Debug.Log("Hit Something!");

        SpaceShip ship = collision.gameObject.GetComponent<SpaceShip>();
        if (ship != null)
        {
            ship.TakeDamage(collisionDamage);
        }


        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(1f);
            Destroy(collision.gameObject);
        }
        /*
        Bullet bullet =  collision.gameObject.GetComponent<Bullet>();
        Debug.Log(bullet);
        if (bullet != null)
        {
            Debug.Log("Asteroid hit");
            Destroy(bullet);
            TakeDamage(bullet.bulletDamage);
            //Take Damage / explode
        }
        */
    }

    public void TakeDamage(float damage)
    {
        healthCurrent = healthCurrent - damage;

        if (healthCurrent <= 0)
        {
            Debug.Log("Explode");
            Explode();
        }
    }

    private void Explode()
    {
        //Destroy itself
        Instantiate(explodeParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
