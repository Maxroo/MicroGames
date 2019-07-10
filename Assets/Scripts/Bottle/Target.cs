using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource AS;

    public AudioClip Clicking;
    public bool isOut = false;

    Rigidbody rbRef;

    public float soundThreshold;

    private void Awake() {
        rbRef = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        isOut = true;
    }

    void OnTriggerEnter(Collider other)
    {
        isOut = false;
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        print(rbRef.velocity.magnitude);
        if(rbRef.velocity.magnitude > soundThreshold){
            AS.PlayOneShot(Clicking);
        }
    }
}
