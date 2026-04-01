using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    public float healthMax = 3f;
    public float healthCurrent;

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

    public CameraShake cameraShake;

    public float shakeIntensity;
    public float shakeDuration;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        _SM = FindAnyObjectByType<SoundManager>();
        healthCurrent = healthMax;

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        ApplyThrust(vertical);
        ApplyTorque(horizontal);
        UpdateFiring();
    }

    private void UpdateFiring()
    {
        bool isFiring = Input.GetButton("Fire1");
        fireTimer = fireTimer - Time.deltaTime;
        if (isFiring && fireTimer <= 0f)
        {
            FireBullet();
            fireTimer = firingRate;
        }
    }
    private void ApplyThrust(float amount)
    {
        Vector2 thrust = transform.up * enginePower * Time.deltaTime * amount;
        rb2D.AddForce(thrust);
    }

    private void ApplyTorque(float amount)
    {
        float torque = amount * turnPower * Time.deltaTime;
        rb2D.AddTorque(-torque);
    }
    public void FireBullet()
    {
        GameObject bullet = Instantiate(bulletRef, firingPoint.transform.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        _SM.PlayRandomSound(_SM.bulletSounds);
        Vector2 force = transform.up * bulletSpeed;

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
        flash.Flash();
        cameraShake.ShakeCam(shakeDuration, shakeIntensity);

        // If the player runs out of health (less than 0), if so, destroy self.
        if (healthCurrent <= 0)
        {
            Explode();
        }
        _SM.PlayRandomSound(_SM.impactSounds);
    }

    public void Explode()
    {
    
        Debug.Log("Game Over");
        GameOver();
        _SM.PlayRandomSound(_SM.deathSounds);
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
        scoreUI.Show(celebrateHiscore);
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("Hiscore", 0);
    }
    public void SetHighScore(int score)
    {
        PlayerPrefs.SetInt("Hiscore", score);
    }

}
