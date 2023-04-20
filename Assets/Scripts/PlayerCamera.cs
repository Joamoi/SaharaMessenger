using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject fox;

    public float mouseSensX;
    public float mouseSensY;

    float mouseX;
    float mouseY;

    // Start is called before the first frame update
    void Start()
    {
        // hides cursor
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensX * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensY * Time.deltaTime * -1;

        mouseY = Mathf.Clamp(mouseY, -10f, 90f);

        transform.RotateAround(fox.transform.position, Vector3.up, mouseX);
        transform.RotateAround(fox.transform.position, transform.right, mouseY);
    }
}
