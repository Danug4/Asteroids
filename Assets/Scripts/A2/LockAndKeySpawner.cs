using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class LockAndKeySpawner : MonoBehaviour
{
    //Every x seconds:
    //  Check the number of locks in game 
    //  1 or more locks, do nothing 
    //  0 or less locks, spawn a new lock after a period of time
    //Stop if the player is defeated

    public float checkWaitTime;
    public float spawnWaitTime;
    public float lockLimit;
    
    SpaceShip player;
    //LevelManager levelManager;
    public Camera cam;

    public GameObject lockPrefab;
    public Transform lockSpawnPoint;
    public GameObject keyPrefab;

    

    void Awake()
    {
        //Make a reference
        player = FindFirstObjectByType<SpaceShip>().GetComponent<SpaceShip>();
        //levelManager = FindFirstObjectByType<LevelManager>().GetComponent<LevelManager>();
        
    }
    void Start()
    {
        StartCoroutine(AdministerLockAndKey());
    }

    IEnumerator AdministerLockAndKey()
    {
        //Keep going while the player is alive 
        while (player.isAlive == true)
        {
            int lockCount = DetermineNumberOfLocks();
            Debug.Log("Number of locks: " + lockCount);

            if (lockCount < lockLimit)
            {
                yield return new WaitForSeconds(spawnWaitTime); // wait before spawning

                SpawnLock();
                SpawnKey();
            }

            yield return new WaitForSeconds(checkWaitTime);
        }
        
    }

    void SpawnLock()
    {
        Instantiate(lockPrefab, lockSpawnPoint.position, lockSpawnPoint.rotation);
    }

    void SpawnKey()
    {
        Vector3 spawnPoint = OnScreenSpawnPoint();
        Instantiate(keyPrefab, spawnPoint, transform.rotation);
    }

    int DetermineNumberOfLocks()
    {
        Lock[] locks = FindObjectsByType<Lock>(FindObjectsSortMode.None);
        int lockCount = locks.Length;
        return lockCount;
    }

    
    public Vector3 OnScreenSpawnPoint()
    {
        //Find screen width and screen height &
        //X-axis only randomised
        int xPos = Random.Range(0, Screen.width);
        int yPos = Screen.height - 20;// Random.Range(0, Screen.height);

        //convert position to world space 
        if (cam == null)
            return transform.position;

        Vector2 screenPos = new Vector2(xPos, yPos);
        Vector3 worldPos = cam.ScreenToWorldPoint(screenPos);
        worldPos.z = transform.position.z;

        //return it
        return worldPos;
    }
}
