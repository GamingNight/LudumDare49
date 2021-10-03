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
    public float min_fov = 10;
    public float max_fov = 60;
    public float max_angle_x = 60;
    public float min_angle_x = -40;

    private float true_speed_y = 0;
    private float true_speed_x = 0;
    private float init_angle_x = 0;

    public void Start()
    {
        init_angle_x = transform.eulerAngles.x;
    }

    public void Update()
    {   
        true_speed_y = 0;
        true_speed_x = 0;

        if (automatique_rotation)
        {
            true_speed_y = speed;
        }
        if (left_right_controler)
        {
            UpdateTrueSpeedY();
            UpdateTrueSpeedX();
            UpdateFOV();
        }
        transform.Rotate(0, true_speed_y * Time.deltaTime, 0, Space.World);
        transform.Rotate(true_speed_x * Time.deltaTime, 0, 0);
    }

    void UpdateFOV()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        float fov = Camera.main.fieldOfView - zoom_speed_coef * scrollWheel;
        if (fov > max_fov)
        {
            fov = max_fov;
        }
        else if (fov < min_fov)
        {
            fov = min_fov;
        }
        Camera.main.fieldOfView = fov;
    }

    void UpdateTrueSpeedY()
    {
        bool space_down = Input.GetKeyDown(KeyCode.Space);
        bool space_held = Input.GetKey(KeyCode.Space);
        bool space_up = Input.GetKeyUp(KeyCode.Space);
        if (space_down || space_held || space_up)
        {
            true_speed_y = controler_speed_coef * speed;
        }
        bool left_down = Input.GetKeyDown(KeyCode.LeftArrow);
        bool left_held = Input.GetKey(KeyCode.LeftArrow);
        bool left_up = Input.GetKeyUp(KeyCode.LeftArrow);
        if (left_down || left_held || left_up)
        {
            true_speed_y = -controler_speed_coef * speed;
        }
        bool right_down = Input.GetKeyDown(KeyCode.RightArrow);
        bool right_held = Input.GetKey(KeyCode.RightArrow);
        bool right_up = Input.GetKeyUp(KeyCode.RightArrow);
        if (right_down || right_held || right_up)
        {
            true_speed_y = controler_speed_coef * speed;
        }
    }

    void UpdateTrueSpeedX()
    {
        Vector3 angles = transform.eulerAngles;
        float anglesx = angles.x - init_angle_x;
        if (anglesx > 180)
        {
            anglesx = anglesx-360;
        }

        bool set_down = Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKey(KeyCode.DownArrow) ||
            Input.GetKeyUp(KeyCode.DownArrow);

        bool set_up = Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKeyUp(KeyCode.UpArrow);

/*        bool no_vertical_control = (!set_down && !set_up);*/

        set_down = set_down && (anglesx > min_angle_x);
        set_up = set_up && (anglesx < max_angle_x);

/*        bool return_down = (no_vertical_control && (anglesx > 1));
        bool return_up = (no_vertical_control && (anglesx < -1));

        set_down = set_down || return_down;
        set_up = set_up || return_up;*/

        if (set_down)
        {
            true_speed_x = controler_speed_coef * speed;
        }

        if (set_up)
        {
            true_speed_x = -controler_speed_coef * speed;
        }

    }

}
