using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class EnemyHP : MonoBehaviour
{
    [SerializeField] private float HPmax;
    [SerializeField] private float HP;

    [SerializeField] private float ExpToGive;

    [SerializeField] private TextMeshPro myHP;

    [SerializeField] private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        if (anim == null)
        {
            anim = this.gameObject.GetComponent<Animator>();
        }
    }
    public void takeDmg(float dmg)
    {
        StartCoroutine(playSFX(SFX_hit));
        anim.SetTrigger("TakeDmg");
        HP -= dmg;
        if (HP <= 0)
        {
            StartCoroutine(playDeath());
            LevelUp[] levelUpCodes = FindObjectsOfType<LevelUp>();
            levelUpCodes[0].earnExp(ExpToGive);

            
        }
    }
    // Update is called once per frame
    void Update()
    {
        try
        {
            myHP.text = (HP + "/" + HPmax);
        }
        catch
        {
            Debug.Log("text mesh pro missing");
        }
    }

    IEnumerator playDeath()
    {
        EnemySpawner[] EnemySpawners = FindObjectsOfType<EnemySpawner>();

        EnemySpawners[0].SpawnNewEnemy();

        StartCoroutine(playSFX(SFX_die));

        Destroy(this.gameObject);
        yield break;
    }

    [Header("SFX")]
    [SerializeField] private GameObject SFX_hit, SFX_die;
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
