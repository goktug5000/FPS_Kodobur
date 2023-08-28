using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Shoot : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private int ammoMax, ammoMaxBase, ammoNow;
    [SerializeField] private int ammoMaxMulti;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform ShootSpot;
    [SerializeField] private float Damage;
    [SerializeField] private float DamageBase;
    [SerializeField] private float DamageMulti;
    [SerializeField] private int pierceLeft;

    [SerializeField] private TextMeshProUGUI myAmmo;

    [Header("ArmLookAt")]
    [SerializeField] private GameObject arm;
    [SerializeField] private Transform lookAtPos;

    void Start()
    {
        ammoMax = ammoMaxBase;
        Damage = DamageBase;
        pierceLeft = 0;
        updateBullet();
    }
    public void setMyLvls(int ammoMaxx, int dmgLvl,int pierceLeftt)
    {
        ammoMax = ammoMaxBase + ammoMaxx * ammoMaxMulti;
        Damage = DamageBase + dmgLvl * DamageMulti;
        pierceLeft = pierceLeftt;

        updateBullet();
    }
    public int collectAmmo(int amount)
    {
        ammoNow += amount;
        int returnThis = ammoNow - ammoMax;
        if (ammoNow >= ammoMax)
        {
            ammoNow = ammoMax;
        }
        updateBullet();
        return returnThis;
    }
    public void updateBullet()
    {
        try
        {
            myAmmo.text = ("ammo: " + ammoNow + "/" + ammoMax);
        }
        catch
        {
            Debug.Log("text mesh pro missing");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && ammoNow>0 && menuEvents.isGamePaused==false)
        {
            var bulll=Instantiate(bullet, ShootSpot.position,ShootSpot.rotation);
            bulll.GetComponent<Bullet>().setDmg(Damage,pierceLeft);
            ammoNow--;
            StartCoroutine(playSFX(SFX_shoot));
            updateBullet();

        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            ArmLookAt();
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {

            arm.transform.localRotation = Quaternion.identity;
        }
    }
    void ArmLookAt()
    {
        Quaternion targetRotation = Quaternion.LookRotation(lookAtPos.position - arm.transform.position);

        // Smoothly rotate towards the target point.
        arm.transform.rotation = Quaternion.Slerp(arm.transform.rotation, targetRotation, 5 * Time.deltaTime);



        //arm.transform.LookAt(lookAtPos);
    }

    [Header("SFX")]
    [SerializeField] private GameObject SFX_shoot;
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
