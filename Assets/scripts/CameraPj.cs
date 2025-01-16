using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPj : MonoBehaviour
{

    public Vector2 sensibility;

    private new Transform camera;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        camera = transform.Find("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");

        if (hor != 0)
        {
            transform.Rotate(Vector3.up * hor * sensibility);
        }

        if (ver != 0)
        {
            float angle = (camera.localEulerAngles.x - ver * sensibility.y + 360) % 360;

            if (angle > 180) { angle -= 360; }
            angle = Mathf.Clamp(angle, -80, 80);

            camera.localEulerAngles = Vector3.right * angle;
        }
    }
}
