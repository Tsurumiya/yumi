using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	private List<Joycon> joycons;

    //Joycon
    public float[] stick;
    public Vector3 gyro;
    public Vector3 accel;
    public int jc_ind = 0;
    public Quaternion orientation;
    public Quaternion orientation_original;

    //Arduino
    public SerialController serialController;
    private float shootForce;
    private bool isShooting = false;

    //弓矢関連
    [SerializeField]
    private GameObject shootObject;
    [SerializeField]
    private GameObject itemSpawnPoint;
    private GameObject cloneObject;

    private Color pointerColor;
    [SerializeField]
    private float fadeSpeed;
    private bool isYPressed = false;

    void Start ()
    {
        gyro = new Vector3(0, 0, 0);
        accel = new Vector3(0, 0, 0);
        // get the public Joycon array attached to the JoyconManager in scene
        joycons = JoyconManager.Instance.j;
		if (joycons.Count < jc_ind+1){
			Destroy(gameObject);
		}

        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();

        pointerColor = itemSpawnPoint.GetComponent<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    void Update () {
        //Joycon//
		// make sure the Joycon only gets checked if attached
		if (joycons.Count > 0)
        {
			Joycon j = joycons [jc_ind];

            /*
			// GetButtonDown checks if a button has been pressed (not held)
            if (j.GetButtonDown(Joycon.Button.SHOULDER_2))
            {
				Debug.Log ("Shoulder button 2 pressed");
				// GetStick returns a 2-element vector with x/y joystick components
				Debug.Log(string.Format("Stick x: {0:N} Stick y: {1:N}",j.GetStick()[0],j.GetStick()[1]));
            
				// Joycon has no magnetometer, so it cannot accurately determine its yaw value. Joycon.Recenter allows the user to reset the yaw value.
				j.Recenter ();
			}
			// GetButtonDown checks if a button has been released
			if (j.GetButtonUp (Joycon.Button.SHOULDER_2))
			{
				Debug.Log ("Shoulder button 2 released");
			}
			// GetButtonDown checks if a button is currently down (pressed or held)
			if (j.GetButton (Joycon.Button.SHOULDER_2))
			{
				Debug.Log ("Shoulder button 2 held");
			}

			if (j.GetButtonDown (Joycon.Button.DPAD_DOWN)) {
				Debug.Log ("Rumble");

				// Rumble for 200 milliseconds, with low frequency rumble at 160 Hz and high frequency rumble at 320 Hz. For more information check:
				// https://github.com/dekuNukem/Nintendo_Switch_Reverse_Engineering/blob/master/rumble_data_table.md

				j.SetRumble (160, 320, 0.6f, 200);

				// The last argument (time) in SetRumble is optional. Call it with three arguments to turn it on without telling it when to turn off.
                // (Useful for dynamically changing rumble values.)
				// Then call SetRumble(0,0,0) when you want to turn it off.
			}
            */

            stick = j.GetStick();

            // Gyro values: x, y, z axis values (in radians per second)
            gyro = j.GetGyro();

            // Accel values:  x, y, z axis values (in Gs)
            accel = j.GetAccel();

            orientation = Quaternion.identity * 
                Quaternion.AngleAxis(-j.GetVector().eulerAngles.x, Vector3.right) * 
                Quaternion.AngleAxis(j.GetVector().eulerAngles.y, Vector3.forward) * 
                Quaternion.AngleAxis(-j.GetVector().eulerAngles.z, Vector3.up);

            if (j.GetButtonUp(Joycon.Button.DPAD_LEFT))     //角度修正（なぜか2回押さなければ効かない）
            {
                isYPressed = true;
                gameObject.transform.rotation = Quaternion.identity;
                orientation_original = orientation;
                j.Recenter();
                StartCoroutine(FadeOut());
            }
            else
            {
                gameObject.transform.rotation = orientation * Quaternion.Inverse(orientation_original);
            }

            //発射
            if (isShooting == true) 
            {
                isShooting = false;
                cloneObject = Instantiate(shootObject, itemSpawnPoint.transform.position, itemSpawnPoint.transform.rotation);
                cloneObject.SetActive(true);
                cloneObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, shootForce);
            }
        }

        //Arduino//
        string message = serialController.ReadSerialMessage();
        if (message != null)
        {
            // Check if the message is plain data or a connect/disconnect event.
            if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
                Debug.Log("Connection established");
            else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
                Debug.Log("Connection attempt failed or disconnection detected");
            else
            {
                Debug.Log("Message arrived: " + message);
                shootForce = float.Parse(message);
            }
        }
    }

    IEnumerator FadeOut()
    {
        pointerColor.a = 255;
        yield return new WaitForSeconds(0.5f);
        isYPressed = false;
        while (pointerColor.a > 0)
        {
            pointerColor.a -= Time.deltaTime * fadeSpeed;
            if (isYPressed == true)
                yield break;
        }
    }
}