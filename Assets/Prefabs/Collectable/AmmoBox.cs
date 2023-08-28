using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class AmmoBox : MonoBehaviour
{
    [SerializeField] public int ammoCount;

    [SerializeField] private TextMeshPro myAmmo;

    [SerializeField] public GameObject holderMe;//for destroy

    void Start()
    {
        myAmmo.text = ammoCount.ToString();
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
                int willAmmoLeft = col.gameObject.GetComponent<Shoot>().collectAmmo(ammoCount);
                if (willAmmoLeft <= 0)
                {
                    holderMe.gameObject.name = "Destroy";
                    itemSpawner.staticFillSlots();
                    Destroy(holderMe);

                }
                else
                {
                    ammoCount = willAmmoLeft;
                    myAmmo.text = ammoCount.ToString();
                }

            }
            catch
            {
                Debug.Log("missing Shoot.cs in " + col.gameObject.name);

            }
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
