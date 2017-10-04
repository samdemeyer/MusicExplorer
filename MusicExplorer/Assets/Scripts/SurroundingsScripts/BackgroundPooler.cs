using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPooler : MonoBehaviour
{
    public GameObject pooledBackground;
    public int pooledAmount;

    List<GameObject> pooledBackgrounds;
    // Use this for initialization
    void Start()
    {
        pooledBackgrounds = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledBackground);
            obj.SetActive(false);
            pooledBackgrounds.Add(obj);
        }
    }

    public GameObject GetPooledBackground()
    {
        for (int i = 0; i < pooledBackgrounds.Count; i++)
        {
            if (!pooledBackgrounds[i].activeInHierarchy)
            {
                return pooledBackgrounds[i];
            }
        }

        GameObject obj = (GameObject)Instantiate(pooledBackground);
        obj.SetActive(false);
        pooledBackgrounds.Add(obj);
        return obj;
    }
}
