using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 0;
    public float speedRotation = 0;

    public Animator animator;

    private float x, y;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");

        y = Input.GetAxis("Vertical");

        transform.Rotate(0, x * Time.deltaTime * speedRotation, 0);
        transform.Translate(0, 0,y * Time.deltaTime * speed);

    }
}
