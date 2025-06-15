using UnityEngine;
using TMPro;

public class DoorUnlocker : MonoBehaviour
{
    public static bool KeyCollected = false;
    private bool isPlayerNear = false;
    public Transform doorHinge;

    public float openAngle = 90f;
    private bool isOpen = false;

    public TextMeshProUGUI doorMessageText;
    public float messageDisplayTime = 2f;
    private float messageTimer = 0f;

    public TextMeshProUGUI openDoorPromptText;


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

                if (openDoorPromptText != null)
                    openDoorPromptText.enabled = false;
            }
            else if (!KeyCollected)
            {
                ShowDoorMessage("You need a key to unlock this door!");
            }
        }
        if (doorMessageText != null && doorMessageText.enabled)
        {
            messageTimer -= Time.deltaTime;
            if (messageTimer <= 0f)
            {
                doorMessageText.enabled = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerNear = true;

        if (!isOpen && openDoorPromptText != null)
        {
            openDoorPromptText.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerNear = false;

            if (openDoorPromptText != null)
                openDoorPromptText.enabled = false;

            if (doorMessageText != null)
                doorMessageText.enabled = false;
    }
    void OpenDoor()
    {
        if (doorHinge != null)
            doorHinge.Rotate(Vector3.up, openAngle); // Simple open

        isOpen = true;
        Debug.Log("Door unlocked and opened!");
    }

    void ShowDoorMessage(string message)
{
    if (doorMessageText != null)
    {
        doorMessageText.text = message;
        doorMessageText.enabled = true;
        messageTimer = messageDisplayTime;
    }
}
}

