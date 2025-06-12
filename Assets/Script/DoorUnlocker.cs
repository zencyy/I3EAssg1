using UnityEngine;

public class DoorUnlocker : MonoBehaviour
{
    public static bool KeyCollected = false;
    private bool isPlayerNear = false;
    public Transform doorHinge;

    public float openAngle = 90f;
    private bool isOpen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (KeyCollected && !isOpen)
            {
                OpenDoor();
            }
            else
            {
                Debug.Log("Door is locked. Find the key.");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerNear = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerNear = false;
    }
    void OpenDoor()
    {
        if (doorHinge != null)
            doorHinge.Rotate(Vector3.up, openAngle); // Simple open

        isOpen = true;
        Debug.Log("Door unlocked and opened!");
    }
}

