using UnityEngine;

public class Asteriod : MonoBehaviour
{
    public float collisionDamage = 1f;

    public float healthMax = 3f;
    public float healthCurrent;

    public GameObject explodeParticle;

    public GameObject[] chunks;
    public int chunksMin = 0;
    public int chunksMax = 4;
    public float explodeDist = 5f;
    public float explosionForce = 1000f;

    public bool spawnChunks;

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
        if (spawnChunks == true)
        {
            int numChunks = Random.Range(chunksMin, chunksMax + 1);

            for (int i = 0; i < numChunks; i++)
            {
                CreateAsteriodChunk();
            }
        }
        
        Instantiate(explodeParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void CreateAsteriodChunk()
    {
        int rndIndex = Random.Range(0, chunks.Length);
        GameObject chunkRef = chunks[rndIndex];

        Vector2 spawnPos = transform.position;
        spawnPos.x += Random.Range(-explodeDist, explodeDist);
        spawnPos.y += Random.Range(-explodeDist, explodeDist);

        GameObject chunk = Instantiate(chunkRef, spawnPos, transform.rotation);

        Vector2 dir = (spawnPos - (Vector2)transform.position).normalized;

        Rigidbody2D rb = chunk.GetComponent<Rigidbody2D>();
        rb.AddForce(dir * explosionForce);
    }

  


}
