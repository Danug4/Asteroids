using UnityEngine;

public class TennisPlayer : MonoBehaviour
{
    public int playerNumber;
    public float speed;
    public float hitPower;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Swing("Player Swung");
        }
    }

    public void Swing(string _string)
    {
        Debug.Log(_string);
    }
}
