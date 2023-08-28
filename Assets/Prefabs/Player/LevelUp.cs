using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


[System.Serializable]
public class LevelUp : MonoBehaviour
{
    [SerializeField] private int myLvl;
    [SerializeField] private float expMulti;
    [SerializeField] private float expNow;
    [SerializeField] private TextMeshProUGUI myExp;

    [SerializeField] private int Points;
    [SerializeField] private TextMeshProUGUI myPoints;


    [SerializeField] private int totalKill;
    [SerializeField] private TextMeshProUGUI myKillCount;

    [SerializeField] private GameObject menu;

    [Header("Main Skills")]
    [SerializeField] private skill walk;
    [SerializeField] private skill jumpp;
    [SerializeField] private skill maxHealth;
    [Header("Gun Skills")]
    [SerializeField] private skill Ammo;
    [SerializeField] private skill Damage;
    [SerializeField] private skill pierce;

    void Start()
    {
        updateExp();
        updateKill();

        menu.SetActive(true);
        updateAllTexts();
        menu.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (menu.activeSelf)
            {
                menu.SetActive(false);
                menuEvents.Resume();
            }
            else
            {
                menu.SetActive(true);
                menuEvents.Pause();
            }

        }


    }
    public void updateAllTexts()
    {
        try
        {
            walk.myText.text = ("Walk Level: " + walk.lvlNow + "/" + walk.lvlMax + "   Cost for Level Up: " + walk.cost);
            jumpp.myText.text = ("Jump Level: " + jumpp.lvlNow + "/" + jumpp.lvlMax + "   Cost for Level Up: " + jumpp.cost);
            maxHealth.myText.text = ("maxHealth Level: " + maxHealth.lvlNow + "/" + maxHealth.lvlMax + "   Cost for Level Up: " + maxHealth.cost);

            Ammo.myText.text = ("Ammo Level: " + Ammo.lvlNow + "/" + Ammo.lvlMax + "   Cost for Level Up: " + Ammo.cost);
            Damage.myText.text = ("Damage Level: " + Damage.lvlNow + "/" + Damage.lvlMax + "   Cost for Level Up: " + Damage.cost);
            pierce.myText.text = ("Pierce Level: " + pierce.lvlNow + "/" + pierce.lvlMax + "   Cost for Level Up: " + pierce.cost);

            myPoints.text = ("Talent Points: " + Points);
        }
        catch
        {
            Debug.LogError("Fatal Error LevelUp.cs");
        }
        updateSkills();
    }
    public void lvlUpWalk()
    {
        if(Points >= walk.cost && walk.lvlNow < walk.lvlMax)
        {
            walk.lvlNow++;
            Points--;
        }
        updateAllTexts();
    }
    public void lvlUpJumpp()
    {
        if (Points >= jumpp.cost && jumpp.lvlNow < jumpp.lvlMax)
        {
            jumpp.lvlNow++;
            Points--;
        }
        updateAllTexts();
    }
    public void lvlUpmaxHealth()
    {
        if (Points >= maxHealth.cost && maxHealth.lvlNow < maxHealth.lvlMax)
        {
            maxHealth.lvlNow++;
            Points--;
        }
        updateAllTexts();
    }
    public void lvlUpAmmo()
    {
        if (Points >= Ammo.cost && Ammo.lvlNow < Ammo.lvlMax)
        {
            Ammo.lvlNow++;
            Points--;
        }
        updateAllTexts();
    }
    public void lvlUpDamage()
    {
        if (Points >= Damage.cost && Damage.lvlNow < Damage.lvlMax)
        {
            Damage.lvlNow++;
            Points--;
        }
        updateAllTexts();
    }
    public void lvlUppierce()
    {
        if (Points >= pierce.cost && pierce.lvlNow < pierce.lvlMax)
        {
            pierce.lvlNow++;
            Points--;
        }
        updateAllTexts();
    }

    public void updateSkills()
    {
        this.gameObject.GetComponent<Move3D_fp>().setLvlSpeed(walk.lvlNow);
        this.gameObject.GetComponent<Jump>().setLvlJumpPow(jumpp.lvlNow);
        this.gameObject.GetComponent<PlayerHP>().setLvlHP(maxHealth.lvlNow);

        this.gameObject.GetComponent<Shoot>().setMyLvls(Ammo.lvlNow, Damage.lvlNow, pierce.lvlNow);

    }
    public void updateExp()
    {
        try
        {
            myExp.text = ("Level: " + myLvl + "\n" + "exp: " + expNow + "/" + myLvl*expMulti);
        }
        catch
        {
            Debug.Log("text mesh pro missing");
        }
    }
    public void updateKill()
    {
        try
        {
            myKillCount.text = ("Kill: " + totalKill);
        }
        catch
        {
            Debug.Log("text mesh pro missing");
        }
    }
    public void earnExp(float exp)
    {
        totalKill++;
        updateKill();

        expNow += exp;
        if(expNow >= (myLvl * expMulti))
        {
            expNow = expNow - (myLvl * expMulti);
            myLvl++;
            Points++;
            myPoints.text = ("Talent Points: " + Points);
        }
        updateExp();
    }



}

[System.Serializable]
public class skill
{
    [SerializeField] public int lvlNow;
    [SerializeField] public int lvlMax;
    [SerializeField] public int cost;

    [SerializeField] public TextMeshProUGUI myText;
}