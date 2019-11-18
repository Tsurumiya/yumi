using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowScript : MonoBehaviour
{
    private Vector3 spawnPosition;
    private Rigidbody rigidbody;

    private Text scoreText = default;

    private AudioSource audioSource;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip missSound;
    private bool isHit = false;

    void Start()
    {
        spawnPosition = transform.position;
        rigidbody = GetComponent<Rigidbody>();

        scoreText = GameObject.Find("Score").GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, spawnPosition) > 100)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (rigidbody.velocity != Vector3.zero)
            rigidbody.rotation = Quaternion.LookRotation(rigidbody.velocity) * Quaternion.AngleAxis(180, Vector3.right);
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.CompareTag("Target"))
        {
            //点数加算
            scoreText.text = (int.Parse(scoreText.text)+1).ToString();
            audioSource.clip = hitSound;
            audioSource.Play();
            isHit = true;
        }
        else if (!isHit)
        {
            audioSource.clip = missSound;
            audioSource.Play();
        }
        rigidbody.isKinematic = true;
    }
}
