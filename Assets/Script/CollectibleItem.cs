using UnityEngine;
using TMPro;
public class CollectibleItem : MonoBehaviour
{
    public int itemScore = 1;
    private bool isPlayerNear = false;

    private TextMeshProUGUI promptText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Collected Item! +" + itemScore + "points");
            if (promptText != null)
                promptText.enabled = false;

            Destroy(gameObject);
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
}
