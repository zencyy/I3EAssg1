using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{
    public static CoroutineHelper Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}