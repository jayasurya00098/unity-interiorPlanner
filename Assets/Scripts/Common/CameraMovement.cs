using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float sensitivity = 1f;

    private float x;
    private float y;
    
    private Vector3 rotate;


    public float Speed { get => speed; set => speed = value; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Translate(Vector3.down * Speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.E))
            {
                transform.Translate(Vector3.up * Speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * Speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * Speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.back * Speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            }

            y = Input.GetAxis("Mouse X");
            x = Input.GetAxis("Mouse Y");
            rotate = new Vector3(x, y * -sensitivity, 0);
            transform.eulerAngles = transform.eulerAngles - rotate;
        }

        


    }
}
