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
        promptText = GetComponentInChildren<TextMeshProUGUI>(true);
        if (promptText != null)
            promptText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Collected Item! +" + itemScore + "points");

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (promptText != null)
                promptText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (promptText != null)
                promptText.gameObject.SetActive(false);
        }
    }
}
