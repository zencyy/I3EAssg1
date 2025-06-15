/// DoorUnlocker.cs
/// Handles door interaction based on whether the player has collected a key.
/// Displays interaction prompts and feedback messages.

using UnityEngine;
using TMPro;

public class DoorUnlocker : MonoBehaviour
{
    /// Static flag to indicate if the player has collected the key.
    public static bool KeyCollected = false;

    /// True if the player is near the door trigger.
    private bool isPlayerNear = false;

    /// Transform representing the rotating part of the door.
    public Transform doorHinge;

    /// Angle in degrees the door should open.
    public float openAngle = 90f;

    /// True if the door is already opened.
    private bool isOpen = false;

    /// Text UI to display messages like "You need a key".
    public TextMeshProUGUI doorMessageText;

    /// Duration in seconds to show door messages.
    public float messageDisplayTime = 2f;

    /// Timer used to hide the message after a delay.
    private float messageTimer = 0f;

    /// Text UI that shows "Press E to open door".
    public TextMeshProUGUI openDoorPromptText;

    /// Unity's Start method (unused but required by MonoBehaviour).
    void Start()
    {
    }

    /// Handles door input logic and UI message timing.
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

    /// Triggered when the player enters the door area.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerNear = true;

        if (!isOpen && openDoorPromptText != null)
        {
            openDoorPromptText.enabled = true;
        }
    }

    /// Triggered when the player exits the door area.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerNear = false;

        if (openDoorPromptText != null)
            openDoorPromptText.enabled = false;

        if (doorMessageText != null)
            doorMessageText.enabled = false;
    }

    /// Rotates the door hinge to simulate opening.
    void OpenDoor()
    {
        if (doorHinge != null)
            doorHinge.Rotate(Vector3.up, openAngle);

        isOpen = true;
        Debug.Log("Door unlocked and opened!");
    }

    /// Displays a message to the player for a limited time.
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
