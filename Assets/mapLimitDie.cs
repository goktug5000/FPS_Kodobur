using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapLimitDie : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag != "IgnoreHit")
        {

        }
        else
        {
            return;
        }

        if (col.gameObject.tag == "Player")
        {
            try
            {
                col.gameObject.GetComponent<PlayerHP>().takeDmg(Mathf.Infinity);
            }
            catch
            {
                Debug.Log("missing PlayerHP.cs in " + col.gameObject.name);

            }
        }
    }
}
