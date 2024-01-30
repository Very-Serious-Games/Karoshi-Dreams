using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetosVariables : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomRotation()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        }
    }

    public void RandomScale()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.transform.localScale = new Vector3(Random.Range(child.gameObject.transform.localScale.x - 0.5f,child.gameObject.transform.localScale.x + 1.5f),
                                                                Random.Range(child.gameObject.transform.localScale.y - 0.5f, child.gameObject.transform.localScale.y + 1.5f), 1);
        }
    }

    public void randomDeactivation()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (Random.Range(0, 2) == 0)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
