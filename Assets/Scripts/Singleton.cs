using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour
{
    public static T Instance;
 protected void Init(T obj)
    {
        if (transform.tag.Equals("Untagged"))
        {
#if DEBUG
            Debug.LogError($"You have to change tag singleton object{transform.name}");
            return;
#endif
        }
        DontDestroyOnLoad(this);
        GameObject go = GameObject.FindGameObjectWithTag(transform.tag);

        if (go != null && go != gameObject)
        {
            Destroy(go);
        }
        Instance = obj;
    }
}
