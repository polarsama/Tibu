using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody Rgb;

    public CharacterController controller;

    public float movementSpeed = 0;

    public Vector2 sensibility;

    public Animator animator;

    private float x, z, y;
    private new Transform camera;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        camera = transform.Find("MainCamera");

        Rgb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        Vector3 move = (transform.forward * z + transform.right * x).normalized;

        controller.Move(move);


        animator.SetFloat("VelX", x);
        animator.SetFloat("VelY", y);


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
