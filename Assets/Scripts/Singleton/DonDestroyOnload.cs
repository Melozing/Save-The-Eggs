using System.Collections.Generic;
using UnityEngine;

public class DonDestroyOnload : MonoBehaviour
{
    private static Dictionary<string, GameObject> cache = new Dictionary<string, GameObject>();

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (cache.ContainsKey(name))
        {
            //			Debug.LogWarning("Object [" + name + "] exists. Destroy new one");
            Object.Destroy(this.gameObject);
        }
        else
            cache[name] = gameObject;
    }
}
