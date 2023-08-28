using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyDuty : MonoBehaviour
{
    [SerializeField] private EnemyMoveNavMesh myEnemyMoveNavMesh;
    [SerializeField] private GameObject[] wayPoints;
    [SerializeField] private float followDistance;
    [SerializeField] private float followStopDistance;
    [SerializeField] private float attackRange;
    [SerializeField] private GameObject player;


    [SerializeField] private bool following;
    [SerializeField] private bool patroling;


    [SerializeField] private Animator anim;

    
    void Start()
    {
        player = GameObject.Find("Player");
        if (myEnemyMoveNavMesh == null)
        {
            myEnemyMoveNavMesh = this.gameObject.GetComponent<EnemyMoveNavMesh>();
        }
        if (anim == null)
        {
            anim = this.gameObject.GetComponent<Animator>();
        }
    }
    public void setMyWayPoints(GameObject[] a)
    {
        wayPoints = a;
    }
    // Update is called once per frame
    void Update()
    {
        if (playerDistance() <= followDistance)
        {
            StartCoroutine(followMission());
        }
        else if(following == false && patroling == false)
        {
            StartCoroutine(goToWaypoint());
        }
        /*
        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {

                myEnemyMoveNavMesh.newCommand();
                StartCoroutine(goToWaypoint(hit.point));
            }

            //godLevelOrder = false;

        }
        */
    }
    public float playerDistance()
    {
        float distance = myEnemyMoveNavMesh.CalculatePathDistance(player.transform.position);
        return distance;
    }
    public GameObject chooseWaypoint()
    {

        var wayPoint = wayPoints[Random.Range(0, wayPoints.Length)];
        return wayPoint;
    }


    IEnumerator followMission()
    {
        following = true;
        patroling = false;

        anim.SetBool("Walking", true);//Anim

        myEnemyMoveNavMesh.moveToPoint(player.transform.position);
        myEnemyMoveNavMesh.agent.stoppingDistance = attackRange;

        while (!myEnemyMoveNavMesh.anyPathRemaining())//gitmesini bekle
        {
            if (myEnemyMoveNavMesh.CalculateNormalDistance(player.transform.position) > followStopDistance)
            {
                following = false;
                StartCoroutine(goToWaypoint());
                yield break;
            }
           
            myEnemyMoveNavMesh.moveToPoint(player.transform.position);

            yield return null;
        }
        //Debug.Log("in position");
        yield return new WaitForSeconds(0.2f);//0.2sn bekle

        anim.SetTrigger("Attack");//Anim
        yield break;
    }
    IEnumerator goToWaypoint()
    {
        patroling = true;
        StartCoroutine(goToWaypoint(chooseWaypoint().transform.position));
        yield return null;
    }
    IEnumerator goToWaypoint(Vector3 Waypoint)
    {
        patroling = true;
        anim.SetBool("Walking", true);//Anim
        myEnemyMoveNavMesh.agent.stoppingDistance = 0;
        myEnemyMoveNavMesh.moveToPoint(Waypoint);
        while (!myEnemyMoveNavMesh.anyPathRemaining())//gitmesini bekle
        {
            yield return null;
        }

        anim.SetBool("Walking", false);//Anim

        yield return new WaitForSeconds(0.5f);//1sn bekle
        patroling = false;

        yield return null;
    }
}
