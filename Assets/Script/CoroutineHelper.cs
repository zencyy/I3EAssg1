/// CoroutineHelper.cs
/// Provides a persistent helper for running coroutines from non-MonoBehaviour scripts,
/// or from objects that are destroyed during gameplay.
using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{
    public static CoroutineHelper Instance;

    /// Ensures only one instance of this helper exists and persists between scenes.
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