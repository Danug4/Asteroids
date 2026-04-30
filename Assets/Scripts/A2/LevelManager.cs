using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    // Get current value of items on screen (one for each category)
    // Compare current value to the limit (increases over time)
    // --> if current value is less than limit: spawn extra
    // 
    // use enumerator?

    public GameObject[] pickups;
    public GameObject[] enemies;
    // pickup GameObject[] asteroids;

    SpaceShip player;
    bool keepGoing;

    public Camera cam;

    int enemyValueCurrent;
    int enemyValueLimit;
    public int enemyValueChangeAmount;

    int pickupValueCurrent;
    int pickupValueLimit;
    public int pickupValueChangeAmount;

    public float waitTime;

    void Start()
    {
        player = FindFirstObjectByType<SpaceShip>().GetComponent<SpaceShip>();
        StartCoroutine(CalculateAndSpawn());
        //cam = FindFirstObjectByType<Camera>();
    }

    IEnumerator CalculateAndSpawn()
    {
        //Check whether the player alive (and thus whether to continue)
        keepGoing = player.isAlive;

        while(keepGoing == true)
        {
            //Update limits
            enemyValueLimit += enemyValueChangeAmount;
            pickupValueLimit += pickupValueChangeAmount;
            //Debug.Log("Pickup spawn limit: " + pickupValueLimit);

            //Determine the current value 
            enemyValueCurrent = FindCurrentEnemyValue();
            pickupValueCurrent = FindCurrentPickupValue();
            //Debug.Log("Current pickup value: " + pickupValueCurrent);

            //Spawn new objects
            if (pickupValueCurrent < pickupValueLimit)
            {
                Spawner(pickups, true);
                //Debug.Log("Spawned a pickup :)");
            }
            if (enemyValueCurrent < enemyValueLimit)
            {
                Spawner(enemies, false);
            }

            yield return new WaitForSeconds(waitTime);
        }
    }

    void Spawner(GameObject[] objects, bool spawnOnScreen)
    {
        //Pick a random object of given type
        int objectIndex = Random.Range(0, objects.Length);
        GameObject objectToSpawn = objects[objectIndex];

        //Determine where to spawn it
        Vector3 spawnPoint = Vector3.zero;
        if (spawnOnScreen == true)
        {
            spawnPoint = OnScreenSpawnPoint();
        }
        else
        {
             spawnPoint = OffScreenSpawnPoint();
        }

        //Spawn the object
        Instantiate(objectToSpawn, spawnPoint, transform.rotation);
    }

    int FindCurrentEnemyValue()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        int value = 0;
        for (int index = 0; index < enemies.Length; index++)
        {
            value += enemies[index].spawnValue;
        }
        return value;
    }
    int FindCurrentPickupValue()
    {
        //Make a list of all collectables that are not keys
        Collection[] collectables = FindObjectsByType<Collection>(FindObjectsSortMode.None);
        /*Collection[] nonKeyCollectables = new Collection[collectables.Length];
        int nonKeyColIndex = 0;
        for (int index = 0; index < collectables.Length; index++)
        {
            if (collectables[index].pickupType != 3) //3 is the number that signifies a key
            {
                nonKeyCollectables[nonKeyColIndex] = collectables[index];
                nonKeyColIndex += 1;
            }
        }*/


        //Determine value 
        int value = 0;
        for (int index = 0; index < collectables.Length; index++)
        {
            value += collectables[index].spawnValue;
        }
        return value;
    }

    public Vector3 OnScreenSpawnPoint()
    {
        //Find screen width and screen height &
        //Pick a random position for each
        int xPos = Random.Range(0, Screen.width);
        int yPos = Random.Range(0, Screen.height);

        //convert position to world space 
        if (cam == null)
            return transform.position;

        Vector2 screenPos = new Vector2(xPos, yPos);
        Vector3 worldPos = cam.ScreenToWorldPoint(screenPos);
        worldPos.z = transform.position.z;

        //return it
        return worldPos;
    }
    public Vector3 OffScreenSpawnPoint()
    {
        //Debug.Log("Camera: " + cam);
        if (cam == null)
            return transform.position;

        // Choose one of the four screen edges
        int edge = Random.Range(0, 4);
        Vector2 viewportPos;
        switch (edge)
        {
            case 0: // Left
                viewportPos = new Vector2(-0.1f, Random.value);
                break;
            case 1: // Right
                viewportPos = new Vector2(1.1f, Random.value);
                break;
            case 2: // Bottom
                viewportPos = new Vector2(Random.value, -0.1f);
                break;
            default: // Top
                viewportPos = new Vector2(Random.value, 1.1f);
                break;
        }
        Vector3 worldPos = cam.ViewportToWorldPoint(viewportPos);
        worldPos.z = transform.position.z;
        return worldPos;
    }
}
