using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private Vector3 spawnPosition;
    private Rigidbody rigidbody;

    void Start()
    {
        spawnPosition = transform.position;
        rigidbody = GetComponent<Rigidbody>();
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
        /*
        if (hit.gameObject.CompareTag("Target"))
        {
            //点数加算
        }
        */
        //rigidbody.velocity = Vector3.zero;
        //rigidbody.useGravity = false;
        rigidbody.isKinematic = true;

    }
}
