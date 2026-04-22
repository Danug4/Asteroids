using UnityEngine;

public class Collection : MonoBehaviour
{

    public GameObject particle;
    public SoundManager _SM;

    public int pickupType;
    /*
        Pickup Type Values:
            0: N.a
            1: Health
            2: Ammo
    */
    public float healthPickupAmount;
    public int ammoPickupAmount;
    SpaceShip spaceShip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _SM = GameObject.FindFirstObjectByType<SoundManager>();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Applying any pickup effects
            spaceShip = collision.gameObject.GetComponent<SpaceShip>();
            DetermineEffect();

            //Death and particles
            Instantiate(particle, transform.position, transform.rotation);
            _SM.PlayRandomSound(_SM.collectionSounds);
            Destroy(gameObject);
        }
    }

    void DetermineEffect()
    {
        if (pickupType == 0) //No effect
        {
            return;
        }
        if (pickupType == 1) // If health
        {
            spaceShip.AddHealth(healthPickupAmount);
        }
        if (pickupType == 2) // If ammo
        {
            spaceShip.AddAmmunition(ammoPickupAmount);
        }
    }
}
