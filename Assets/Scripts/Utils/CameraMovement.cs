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
    private bool valueUpdated = false;

    public float Speed { get => speed; set => speed = value; }

    private float temp_speed;
    private float temp_sensitivity;

    private Vector3 temp_position;
    private Vector3 temp_rotation;

    // Start is called before the first frame update
    void Start()
    {
        temp_position = transform.position;
        temp_rotation = transform.eulerAngles;

        temp_speed = Speed;
        temp_sensitivity = sensitivity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (!valueUpdated)
                {
                    Speed = Speed * 2f;
                    sensitivity = sensitivity * 2f;
                    valueUpdated = true;
                }
            }

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

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            valueUpdated = false;
            Speed = temp_speed;
            sensitivity = temp_sensitivity;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            transform.position = temp_position;
            transform.eulerAngles = temp_rotation;
        }

    }
}
