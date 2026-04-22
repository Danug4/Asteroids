using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletDamage = 1f;
    public GameObject explodeParticleFX;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(explodeParticleFX, transform.position, transform.rotation);
    }
}
