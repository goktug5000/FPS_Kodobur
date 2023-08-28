using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Move3D_fp : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float speedBase=5;
    [SerializeField] private float speedMulti = 1.5f;
    private float sprintSpeedMulti=2;
    [SerializeField] private float speed;
    [SerializeField] private float speedNow;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 movedirection;

    public void setLvlSpeed(int lvl)
    {
        speed = speedBase + (lvl * speedMulti);
    }
    // Start is called before the first frame update
    void Start()
    {
        speed = speedBase;
        setSpeed();
    }
    public void setSpeed()
    {
        float sprintSpeedMultii = 1;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprintSpeedMultii = sprintSpeedMulti;
        }
        speedNow = speed * sprintSpeedMultii;
    }
    // Update is called once per frame
    void Update()
    {
        setSpeed();

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        //Debug.Log(horizontalInput + "   " + verticalInput);
        movedirection = new Vector3(horizontalInput, 0, verticalInput);
        //Debug.Log(movedirection);
        transform.Translate(movedirection * speedNow * Time.deltaTime);
    }
}
