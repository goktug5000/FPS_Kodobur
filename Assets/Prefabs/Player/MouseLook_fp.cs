using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MouseLook_fp : MonoBehaviour
{
    [SerializeField] private float mouseSensitivityX = 600;
    [SerializeField] private float mouseSensitivityY = 400;
    public Transform CamBody;
    [SerializeField] private float xRotation = 90;
    [SerializeField] private float yRotation = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public static void showMouse(bool bol)
    {
        if (bol)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    void mouseShow()
    {
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                MouseLook_fp.showMouse(true);
            }
            else
            {
                MouseLook_fp.showMouse(false);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

        mouseShow();

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 70);


        CamBody.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);


    }
}
