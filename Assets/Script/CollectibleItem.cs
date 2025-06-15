/// 
/// CollectibleItem.cs
/// 
/// Handles coin/key collection, UI prompts, audio playback, and item animation.
/// Can be used for coins (score) or keys (unlocking doors).

using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class CollectibleItem : MonoBehaviour
{
    
    /// The score value this collectible adds.
    
    public int itemScore = 1;

    
    /// Whether the player is near enough to interact.
    
    private bool isPlayerNear = false;

    
    /// Whether this collectible is a key (vs a coin).
    
    public bool isKeyItem = false;

    
    /// Speed at which the object rotates.
    
    public float rotationSpeed = 50f;

    
    /// Amplitude of the floating motion.
    
    public float floatAmplitude = 0.25f;

    
    /// Frequency of the floating motion.
    
    public float floatFrequency = 1f;

    
    /// Original position used for floating calculation.
    
    private Vector3 startPos;

    
    /// UI Text displayed when the key is collected.
    
    public TextMeshProUGUI keyMessageText;

    
    /// Sound to play on collection.
    
    public AudioClip collectSFX;

    
    /// UI image icon shown when the key is collected.
    
    public Image keyIcon;

    
    /// UI prompt text shown when player is near the item.
    
    private TextMeshProUGUI promptText;

    
    /// Initializes the collectible, finds prompt text if not assigned.

    void Start()
    {
        startPos = transform.position;

        if (promptText == null)
        {
            GameObject promptObj = GameObject.Find("InteractPromptText");
            if (promptObj != null)
                promptText = promptObj.GetComponent<TextMeshProUGUI>();
        }
    }

    
    /// Rotates and listens for player interaction.
    
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Collected Item! +" + itemScore + "points");

            if (promptText != null)
                promptText.enabled = false;

            if (collectSFX != null)
                AudioSource.PlayClipAtPoint(collectSFX, transform.position);

            if (isKeyItem)
            {
                Debug.Log("Collected the Key!");
                DoorUnlocker.KeyCollected = true;

                if (keyIcon != null)
                    keyIcon.enabled = true;

                if (keyMessageText != null)
                {
                    keyMessageText.enabled = true;
                    TextMeshProUGUI tempRef = keyMessageText;
                    CoroutineHelper.Instance.StartCoroutine(HideMessage(tempRef));
                }

                Destroy(gameObject);
            }
            else
            {
                ScoreBehaviour.Instance.AddScore(itemScore);
                Destroy(gameObject);
            }
        }
    }

    
    /// Triggered when the player enters the collectible area.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && promptText != null)
        {
            isPlayerNear = true;
            promptText.enabled = true;
        }
    }

    /// Triggered when the player leaves the collectible area.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && promptText != null)
        {
            isPlayerNear = false;
            promptText.enabled = false;
        }
    }

    /// Hides the key message text after a delay.
    private IEnumerator HideMessage(TextMeshProUGUI tempText)
    {
        yield return new WaitForSeconds(2f);
        if (tempText != null)
        {
            tempText.enabled = false;
            Debug.Log("Key message hidden");
        }
    }
}
