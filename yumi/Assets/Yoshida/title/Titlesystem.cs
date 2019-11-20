using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Titlesystem : MonoBehaviour
{

	private List<Joycon> joycons;

	// Values made available via Unity
	public int jc_ind = 0;

    private AudioSource audioSource;

    void Start()
	{
		// get the public Joycon array attached to the JoyconManager in scene
		joycons = JoyconManager.Instance.j;
        /*
		if (joycons.Count < jc_ind + 1)
		{
			Destroy(gameObject);
		}
        */
        audioSource = GetComponent<AudioSource>();
    }

	// Update is called once per frame
	void Update()
	{
		// make sure the Joycon only gets checked if attached
		if (joycons.Count > 0)
		{
			Joycon j = joycons[jc_ind];
            if (j.GetButtonDown(Joycon.Button.SHOULDER_1) ||
                j.GetButtonDown(Joycon.Button.SHOULDER_2) ||
                j.GetButtonDown(Joycon.Button.DPAD_UP) ||
                j.GetButtonDown(Joycon.Button.DPAD_DOWN) ||
                j.GetButtonDown(Joycon.Button.DPAD_LEFT) ||
                j.GetButtonDown(Joycon.Button.DPAD_RIGHT))
                StartCoroutine(StartGame());
        }
    }

    IEnumerator StartGame()
    {
        audioSource.Play();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Stage");
    }
}