using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    public float healthMax = 3f;
    public float healthCurrent;

    public float fuelMax;
    float fuelCurrent;
    //public float fuelUseRotateMod; //Modifier to amount of fuel used when moving 
    //public float fuelUseMoveMod; //Modifier to the amount of fuel used when rotating

    public int ammoMax;
    int ammoCurrent;
    public int ammoUseFire; //Amount of ammo used when a bullet is shot

    public int score;

    public float enginePower = 10f;
    public float turnPower = 10f;

    public GameObject bulletRef;
    public float bulletSpeed;
    public float bulletLifeTime = 3f;

    public float firingRate = 0.33f;
    public float fireTimer = 0f;

    private Rigidbody2D rb2D;

    public GameObject firingPoint;

    public SoundManager _SM;

    public ScreenFlash flash;

    public ScoreUI scoreUI;
    UIManager ui;

    public CameraShake cameraShake;

    public float shakeIntensity;
    public float shakeDuration;

    public GameObject explodeParticleShip;

    public GameObject key;
    GameObject currentKey;
    //public Transform keyPos;

    public bool isAlive;



    //If there is enough fuel, apply any movement / rotation and use any fuel if relevant, else ignore movement

    void Awake()
    {
        isAlive = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        _SM = FindAnyObjectByType<SoundManager>();
        ui = FindFirstObjectByType<UIManager>().GetComponent<UIManager>();

        healthCurrent = healthMax;
        ammoCurrent = ammoMax;
        fuelCurrent = fuelMax;

        ui.SetupSliders(healthMax, healthCurrent, ammoMax, ammoCurrent);

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        ApplyThrust(vertical);
        ApplyTorque(horizontal);
        
        UpdateFiring();
        //Debug.Log("Ammo amount: " + ammoCurrent);
       // Debug.Log("fuel amount: " + fuelCurrent);
    }

    private void UpdateFiring()
    {
        bool isFiring = Input.GetButton("Fire1");
        fireTimer = fireTimer - Time.deltaTime;
        if (isFiring && fireTimer <= 0f && ammoCurrent > 0)
        {
            FireBullet();
            fireTimer = firingRate;
        }
    }
    private void ApplyThrust(float amount)
    {
        Vector2 thrust = transform.up * enginePower * Time.deltaTime * amount;
        rb2D.AddForce(thrust);
        //fuelCurrent -= amount * fuelUseMoveMod;
    }

    private void ApplyTorque(float amount)
    {
        float torque = amount * turnPower * Time.deltaTime;
        rb2D.AddTorque(-torque);
        //fuelCurrent -= Mathf.Abs(amount * fuelUseRotateMod);
    }
    public void FireBullet()
    {
        GameObject bullet = Instantiate(bulletRef, firingPoint.transform.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        _SM.PlayRandomSound(_SM.bulletSounds);
        Vector2 force = transform.up * bulletSpeed;

        ammoCurrent -= ammoUseFire; //Subtract a bullet
        ui.UpdateAmmoSlider(ammoCurrent);

        rb.AddForce(force);
        Destroy(bullet, bulletLifeTime);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag != "Bullet")
    //    {
    //        TakeDamage(1f);
    //    }
        
    //}
    public void TakeDamage(float damage)
    {
        healthCurrent = healthCurrent - damage;
        ui.UpdateHealthSlider(healthCurrent);

        flash.Flash();
        cameraShake.ShakeCam(shakeDuration, shakeIntensity);

        // If the player runs out of health (less than 0), if so, destroy self.
        if (healthCurrent <= 0)
        {
            Explode();
            isAlive = false;
        }
        _SM.PlayRandomSound(_SM.impactSounds);
    }

    public void Explode()
    {
    
        //Debug.Log("Game Over");
        GameOver();
        _SM.PlayRandomSound(_SM.deathSounds);
        Instantiate(explodeParticleShip, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void GameOver()
    {
        bool celebrateHiscore = false;
        if (score > GetHighScore())
        {
            SetHighScore(score);
            celebrateHiscore = true;
        }
        //scoreUI.Show(celebrateHiscore);
        ui.Show(celebrateHiscore);

    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("Hiscore", 0);
    }
    public void SetHighScore(int score)
    {
        PlayerPrefs.SetInt("Hiscore", score);
    }

    public void AddHealth(float amount)
    {
        //Debug.Log("Health Old: " + healthCurrent);
        healthCurrent = Mathf.Clamp(healthCurrent + amount, 0f, healthMax); //Add health up to the maximum health
        ui.UpdateHealthSlider(healthCurrent);
        //Debug.Log("Health New: " + healthCurrent);
    }

    public void AddAmmunition(int amount)
    {
        ammoCurrent = Mathf.Clamp(ammoCurrent + amount, 0, ammoMax );
        ui.UpdateAmmoSlider(ammoCurrent);
    }
    
    public void DestroyAllEnemies()
    {
        //Make a list of all enemies in game 
        //For each enemy -> call destroy or equivelent function

        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        for (int index = 0; index < enemies.Length; index++)
        {
            enemies[index].Death();
        }
        cameraShake.ShakeCam(shakeDuration, shakeIntensity);
    }

    public void HoldKey()
    {
        //Create a key that follows player
        //currentKey = Instantiate(key, keyPos.position, keyPos.rotation);
        //currentKey.transform.position = keyPos.position;

        //Enable Key in front of player
        key.SetActive(true);
    }
    public void RemoveKey()
    {
        /*// If there is a key, remove it
        if (currentKey != null)
        {
            Destroy(currentKey);
        }*/

        //Disable Key in front of player 
        key.SetActive(false);
    }
}
