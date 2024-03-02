using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private float sensivity = 1.0f;
    [SerializeField] private float yClamp = 60.0f;

    private float xRotation = 0;

    private void Awake()
    {
        //Cursor.lockState = CursorLockMode.Locked;
       // Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();   
    }

    private void RotateCamera()
    {
        Vector2 input = InputManager.turnInput;

        //Rotating around Y-axis ( left and right )
        transform.Rotate(Vector3.up * (input.x * sensivity));

        //Rotating around the X-axis (up and down)
        xRotation -= input.y;
        xRotation = Mathf.Clamp(xRotation, -yClamp, yClamp); //Clamp prevents over-rotation

        transform.localEulerAngles = new Vector3(xRotation, transform.localEulerAngles.y, 0);



    }
}
