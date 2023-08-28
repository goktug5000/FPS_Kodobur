using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideForSecs : MonoBehaviour
{
    public float hideSec;
    public GameObject showAfter;
    void Start()
    {
        showAfter.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        hideSec -= Time.deltaTime;
        if (hideSec <= 0)
        {
            showAfter.SetActive(true);
            Destroy(this);
        }
    }
}
