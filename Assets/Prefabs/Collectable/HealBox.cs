using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class HealBox : MonoBehaviour
{
    [SerializeField] public int healCount;

    [SerializeField] private TextMeshPro myHeal;

    [SerializeField] public GameObject holderMe;//for destroy
    // Start is called before the first frame update
    void Start()
    {
        myHeal.text = healCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            try
            {
                col.gameObject.GetComponent<PlayerHP>().takeDmg(-healCount);

            }
            catch
            {
                Debug.Log("missing PlayerHP.cs in " + col.gameObject.name);

            }
            holderMe.gameObject.name = "Destroy";
            itemSpawner.staticFillSlots();
            Destroy(holderMe);
        }

    }


    [Header("SFX")]
    [SerializeField] private GameObject SFX_hit;
    private GameObject _sfx;
    IEnumerator playSFX(GameObject SFXX)
    {
        try
        {
            _sfx = Instantiate(SFXX);
        }
        catch
        {
            yield break;
        }
        yield return new WaitForSeconds(10);

        Destroy(_sfx);
        yield break;
    }
}
