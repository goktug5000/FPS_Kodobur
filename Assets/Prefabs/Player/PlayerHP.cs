using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class PlayerHP : MonoBehaviour
{
    [SerializeField] private float HPmax;
    [SerializeField] private float HPmaxMulti, HPmaxBase;
    [SerializeField] private float HP;

    [SerializeField] private TextMeshProUGUI myHP;
    [SerializeField] private GameObject HPbar;


    [SerializeField] private GameObject youHaveDiedPanel;
    

    void Start()
    {
        HPmax = HPmaxBase;

        youHaveDiedPanel.SetActive(false);
        HPbar.transform.localScale = new Vector3(1f, 1, 1f);
        myHP.text = ("HP: " + HP + "/" + HPmax);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setLvlHP(int lvl)
    {
        float saveHp;
        saveHp = HPmax;
        HPmax = HPmaxBase + (lvl * HPmaxMulti);

        HP += (HPmax - saveHp);

        HPbar.transform.localScale = new Vector3((HP / HPmax), 1, 1f);
        myHP.text = ("HP: " + HP + "/" + HPmax);
    }
    public void takeDmg(float dmg)
    {
        StartCoroutine(playSFX(SFX_hit));
        HP -= dmg;

        if (HP >= HPmax)
        {
            HP = HPmax;
        }

        HPbar.transform.localScale = new Vector3((HP / HPmax), 1, 1f);
        myHP.text = ("HP: " + HP + "/" + HPmax);

        if (HP <= 0)
        {
            StartCoroutine(playSFX(SFX_die));
            youHaveDiedPanel.SetActive(true);
            menuEvents.Pause();

        }
    }

    [Header("SFX")]
    [SerializeField] private GameObject SFX_hit,SFX_die;
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
