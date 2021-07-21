using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdScript : MonoBehaviour
{
#if UNITY_IOS
    private string gameId = "3855170";
#elif UNITY_ANDROID
    private string gameId = "3855171";
#endif

    public bool testMode = true;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
    }

    public void ShowNormalAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
        else
        {
            Debug.Log("Ad not ready at the moment.");
        }
    }
}
