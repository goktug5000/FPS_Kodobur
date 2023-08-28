using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class itemSpawner : MonoBehaviour
{
    [SerializeField] public itemWithAmount[] items;

    // Start is called before the first frame update
    void Start()
    {
        foreach(itemWithAmount itemWAmount in items)
        {
            itemWAmount.Startt();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void fillSlots()
    {
        foreach (itemWithAmount itemWAmount in items)
        {
            itemWAmount.CreateAll();
        }
    }
    public static void staticFillSlots()
    {
        itemSpawner[] itemSpawners = FindObjectsOfType<itemSpawner>();
        itemSpawners[0].fillSlots();
    }
}

[System.Serializable]
public class itemWithAmount
{
    public GameObject item;
    public GameObject[] items;
    public int Amount;
    public spawnBetween spawnLocs;

    public void Startt()
    {
        items = new GameObject[Amount];

        CreateAll();
    }
    public void CreateAll()
    {

        for(int q = 0; q < items.Length; q++)
        {

            if (items[q] == null)
            {
                float xx = Random.Range(spawnLocs.locations[0].transform.position.x, spawnLocs.locations[1].transform.position.x);
                float yy = Random.Range(spawnLocs.locations[0].transform.position.y, spawnLocs.locations[1].transform.position.y);
                float zz = Random.Range(spawnLocs.locations[0].transform.position.z, spawnLocs.locations[1].transform.position.z);
                items[q] = Create(xx, yy, zz);
            }
            else if(items[q].gameObject.name=="Destroy")
            {
                float xx = Random.Range(spawnLocs.locations[0].transform.position.x, spawnLocs.locations[1].transform.position.x);
                float yy = Random.Range(spawnLocs.locations[0].transform.position.y, spawnLocs.locations[1].transform.position.y);
                float zz = Random.Range(spawnLocs.locations[0].transform.position.z, spawnLocs.locations[1].transform.position.z);
                items[q] = Create(xx, yy, zz);
            }
            else
            {
                Debug.Log(items[q].gameObject.name);
            }

        }
    }
    public GameObject Create(float x, float y, float z)
    {
        GameObject collectItem = MonoBehaviour.Instantiate(item, new Vector3(x,y,z), Quaternion.identity);
        return collectItem;
    }

}

[System.Serializable]
public class spawnBetween
{
    public GameObject[] locations=new GameObject[2];
}
