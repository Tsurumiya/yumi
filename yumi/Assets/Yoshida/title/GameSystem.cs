using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSystem : MonoBehaviour
{

    private List<Joycon> joycons;

    // Values made available via Unity
    public float[] stick;
    public Vector3 gyro;
    public Vector3 accel;
    public int jc_ind = 0;
    public Quaternion orientation;

    void Start()
    {
        gyro = new Vector3(0, 0, 0);
        accel = new Vector3(0, 0, 0);
        // get the public Joycon array attached to the JoyconManager in scene
        joycons = JoyconManager.Instance.j;
        if (joycons.Count < jc_ind + 1)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // make sure the Joycon only gets checked if attached
        if (joycons.Count > 0)
        {
            Joycon j = joycons[jc_ind];
            // GetButtonDown checks if a button has been pressed (not held)
            if (j.GetButtonDown(Joycon.Button.SHOULDER_1))
            {
                SceneManager.LoadScene("Stage");
            }
            // GetButtonDown checks if a button has been release
            if (j.GetButtonDown(Joycon.Button.SHOULDER_2))
            {
                SceneManager.LoadScene("Stage");
            }

            if (j.GetButtonDown(Joycon.Button.DPAD_DOWN))
            {
                SceneManager.LoadScene("Stage");
            }
            if (j.GetButton(Joycon.Button.DPAD_RIGHT))
            {
                SceneManager.LoadScene("Stage");   //change the name of scene
            }
            if (j.GetButton(Joycon.Button.DPAD_UP))
            {
                SceneManager.LoadScene("Stage");   //change the name of scene
            }
            if (j.GetButton(Joycon.Button.DPAD_LEFT))
            {
                SceneManager.LoadScene("Stage");   //change the name of scene
            }
            stick = j.GetStick();

            // Gyro values: x, y, z axis values (in radians per second)
            gyro = j.GetGyro();

            // Accel values:  x, y, z axis values (in Gs)
            accel = j.GetAccel();

            orientation = j.GetVector();
            
            gameObject.transform.rotation = orientation;
        }
    }
}