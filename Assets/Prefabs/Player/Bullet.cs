using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float Speed;
    private float Dmg;
    public void setDmg(float dmgg, int pierceLeftt)
    {
        Dmg = dmgg;
        pierceLeft = pierceLeftt;
    }
    private int pierceLeft;
    [SerializeField] private float lifeTime;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        if (lifeTime == 0)
        {
            lifeTime = 5;
        }

        if (rb == null)
        {

            try
            {
                rb = this.gameObject.GetComponent<Rigidbody>();
            }
            catch
            {
                Debug.Log("Rigidbody koymamýþsýn");
            }
        }

        rb.AddForce(transform.forward * Speed);
    }

    // Update is called once per frame
    void Update()
    {

        lifeTime -= 1 * Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag != "IgnoreHit")
        {
            pierceLeft--;
            StartCoroutine(playSFX(SFX_hit));
        }
        else
        {
            return;
        }

        if (col.gameObject.tag == "Enemy")
        {
            try
            {
                col.gameObject.GetComponent<EnemyHP>().takeDmg(Dmg);
            }
            catch
            {
                Debug.Log("missing EnemyHP.cs in " + col.gameObject.name);

            }
        }


        if (pierceLeft < 0)
        {
            Destroy(this.gameObject);
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
