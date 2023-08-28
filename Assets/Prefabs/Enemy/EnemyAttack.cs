using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] public float dmg;
    [SerializeField] private GameObject attackHitBox;
    Vector3 attackOffset;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] public LayerMask attackMask;//EÞEK GÝBÝ ATAMAYI UNUTMA KAFAYI YERSÝN SONRA
    [SerializeField] public bool canAttack = true;


    public void checkDmgAble()
    {
        checkDmgAble(1);
    }
    public void checkDmgAble(float dmgMulti)
    {
        if (canAttack == false)
            return;

        Collider[] colInfos = Physics.OverlapSphere(attackHitBox.transform.position, attackRange, attackMask);

        foreach (Collider colInfo in colInfos)
        {
            Debug.Log(colInfo.gameObject.name);
            if (colInfo.GetComponent<PlayerHP>() != null)
            {
                colInfo.GetComponent<PlayerHP>().takeDmg(dmg);
                canAttack = false;
                return;
            }
        }
    }
}
