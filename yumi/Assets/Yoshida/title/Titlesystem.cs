using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Titlesystem : MonoBehaviour
{

	private List<Joycon> joycons;

	// Values made available via Unity
	public float[] stick;
	public Vector3 gyro;
	public Vector3 accel;
	public int jc_ind = 0;
	public Quaternion orientation;

    private AudioSource audioSource;

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

        audioSource = GetComponent<AudioSource>();
    }

	// Update is called once per frame
	void Update()
	{

		// make sure the Joycon only gets checked if attached
		if (joycons.Count > 0)
		{
			Joycon j = joycons[jc_ind];
            /*
			// GetButtonDown checks if a button has been pressed (not held)
			if (j.GetButtonDown(Joycon.Button.SHOULDER_2))
			{
				//Debug.Log("Shoulder button 2 pressed");
				// GetStick returns a 2-element vector with x/y joystick components
				//Debug.Log(string.Format("Stick x: {0:N} Stick y: {1:N}", j.GetStick()[0], j.GetStick()[1]));

				// Joycon has no magnetometer, so it cannot accurately determine its yaw value. Joycon.Recenter allows the user to reset the yaw value.
				//j.Recenter();
			}
			// GetButtonDown checks if a button has been released
			if (j.GetButtonUp(Joycon.Button.SHOULDER_2))
			{
				//Debug.Log("Shoulder button 2 released");
			}
			// GetButtonDown checks if a button is currently down (pressed or held)
			if (j.GetButton(Joycon.Button.SHOULDER_2))
			{
				//Debug.Log("Shoulder button 2 held");
			}

			if (j.GetButtonDown(Joycon.Button.DPAD_DOWN))
			{
                StartCoroutine(StartGame());
				SceneManager.LoadScene("Stage");
			}
            if (j.GetButtonDown(Joycon.Button.DPAD_UP))
            {
                SceneManager.LoadScene("Stage");
            }
            if (j.GetButtonDown(Joycon.Button.DPAD_RIGHT))
            {
                SceneManager.LoadScene("Stage");
            }
            if (j.GetButtonDown(Joycon.Button.DPAD_LEFT))
            {
                SceneManager.LoadScene("Stage");
            }
            if (j.GetButtonDown(Joycon.Button.SHOULDER_1))
            {
                SceneManager.LoadScene("Stage");
            }
            if (j.GetButtonDown(Joycon.Button.SHOULDER_2))
            {
                SceneManager.LoadScene("Stage");
            }

            stick = j.GetStick();

			// Gyro values: x, y, z axis values (in radians per second)
			gyro = j.GetGyro();

			// Accel values:  x, y, z axis values (in Gs)
			accel = j.GetAccel();

			orientation = j.GetVector();

			gameObject.transform.rotation = orientation;
            */
		}

        if(Input.anyKey)
            StartCoroutine(StartGame());

    }

    IEnumerator StartGame()
    {
        audioSource.Play();
        //2秒まつ
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Stage");
    }
}