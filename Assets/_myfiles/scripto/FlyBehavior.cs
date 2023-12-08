using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBehavior : MonoBehaviour
{
    public float moveSpd;
    public float maxFloatHeight = 10;
    public float minFloatHeight;

    public Camera freeLookCamera;
    private float curHeight;

    private float xRot;

    private void Start()
    {
        curHeight = transform.position.y;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        xRot = freeLookCamera.transform.rotation.eulerAngles.x;

        if (Input.GetKey(KeyCode.W))
        {
            MoveChar();
        }
        else
        {
            DisableMov();
        }

        curHeight = Mathf.Clamp(transform.position.y, curHeight, maxFloatHeight);
        transform.position = new Vector3(transform.position.x, curHeight, transform.position.z);
    }

    private void MoveChar()
    {
        Vector3 cameraFwd = new Vector3(freeLookCamera.transform.forward.x, 0, freeLookCamera.transform.forward.z);
        transform.rotation = Quaternion.LookRotation(cameraFwd);
        transform.Rotate(new Vector3(xRot, 0, 0), Space.Self);

        Vector3 forward = freeLookCamera.transform.forward;
        Vector3 flyDir = forward.normalized;

        curHeight += flyDir.y * moveSpd * Time.deltaTime;
        curHeight = Mathf.Clamp(curHeight, minFloatHeight, maxFloatHeight);

        transform.position += flyDir * moveSpd * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, curHeight, transform.position.z);
    }

    private void DisableMov()
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}



