using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
public class CollectibleItem : MonoBehaviour
{
    public int itemScore = 1;
    private bool isPlayerNear = false;
    public bool isKeyItem = false;

    public float rotationSpeed = 50f;
    public float floatAmplitude = 0.25f;
    public float floatFrequency = 1f;
    private Vector3 startPos;

    public TextMeshProUGUI keyMessageText;
    public AudioClip collectSFX;
    public Image keyIcon;


    private TextMeshProUGUI promptText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    // Update is called once per frame
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
                    StartCoroutine(ShowKeyMessageAndDestroy());
                else
                    Destroy(gameObject);
            }
            else
            {
                ScoreBehaviour.Instance.AddScore(itemScore);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && promptText != null)
        {
            isPlayerNear = true;
            promptText.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && promptText != null)
        {
            isPlayerNear = false;
            promptText.enabled = false;
        }
    }
    
    private IEnumerator ShowKeyMessageAndDestroy()
    {
        keyMessageText.enabled = true;
        yield return new WaitForSeconds(2f);
        keyMessageText.enabled = false;
        Destroy(gameObject);
    }
    
    
}
