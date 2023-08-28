using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Aim_dot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    [SerializeField] private LayerMask aimContMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private GameObject crossHair;
    // Update is called once per frame
    void Update()
    {
        Vector2 centerOfScreen = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(centerOfScreen);
        if(Physics.Raycast(ray,out RaycastHit raycastHit, 999f, aimContMask))
        {
            debugTransform.position = raycastHit.point;
        }


        if (Input.GetKey(KeyCode.Mouse1))
        {
            debugTransform.gameObject.SetActive(true);
            try
            {
                crossHair.SetActive(true);
            }
            catch
            {

            }
        }
        else
        {
            debugTransform.gameObject.SetActive(false);
            try
            {
                crossHair.SetActive(false);
            }
            catch
            {

            }
        }
    }
}
