using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour 
{
    Transform Player;

    [HideInInspector]
    public float rotationX;

    [HideInInspector]
    public float rotationY;

    [HideInInspector]
    public float targetrotationX;

    [HideInInspector]
    public float targetrotationY;

    [HideInInspector]
    public float xRotV;
    [HideInInspector]
    public float yRotV;

    public float dampSpeed;
    public float mouseSpeed;

    void Start()
    {
        Player = transform.root;
    //    Screen.lockCursor(true);
    }



    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * mouseSpeed; ;
        rotationY += -Input.GetAxis("Mouse Y") * mouseSpeed; ;

        rotationY = Mathf.Clamp(rotationY, -85, 85);

        targetrotationX = Mathf.SmoothDamp(targetrotationX, rotationX, ref xRotV, dampSpeed);
        targetrotationY = Mathf.SmoothDamp(targetrotationY, rotationY, ref yRotV, dampSpeed);

        transform.localEulerAngles = new Vector3(targetrotationY, 0, 0);
        Player.localEulerAngles = new Vector3(0, targetrotationX, 0);
    }

}
