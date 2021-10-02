using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{

    public float speed;
    public bool automatique_rotation = true;
    public bool left_right_controler = true;
    public float controler_speed_coef = 30;
    public float zoom_speed_coef = 10;

    private float true_speed = 0;

    public void Update()
    {   
        true_speed = 0;
        if (automatique_rotation)
        {
            true_speed = speed;
        }
        if (left_right_controler)
        {
            bool space_down = Input.GetKeyDown(KeyCode.Space);
            bool space_held = Input.GetKey(KeyCode.Space);
            bool space_up = Input.GetKeyUp(KeyCode.Space);
            if (space_down || space_held || space_up)
            {
                true_speed = controler_speed_coef * speed;
            }
            bool left_down = Input.GetKeyDown(KeyCode.LeftArrow);
            bool left_held = Input.GetKey(KeyCode.LeftArrow);
            bool left_up = Input.GetKeyUp(KeyCode.LeftArrow);
            if (left_down || left_held || left_up)
            {
                true_speed = controler_speed_coef * speed;
            }
            bool right_down = Input.GetKeyDown(KeyCode.RightArrow);
            bool right_held = Input.GetKey(KeyCode.RightArrow);
            bool right_up = Input.GetKeyUp(KeyCode.RightArrow);
            if (right_down || right_held || right_up)
            {
                true_speed = -controler_speed_coef * speed;
            }
            float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
            Camera.main.fieldOfView = Camera.main.fieldOfView + zoom_speed_coef * scrollWheel;
        }
        transform.Rotate(0, true_speed * Time.deltaTime, 0);

    }
}
