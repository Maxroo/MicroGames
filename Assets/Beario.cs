using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beario : MonoBehaviour
{
    // Start is called before the first frame update
    
    bool isJump;

    public Animator Am;
    public Rigidbody2D rg2d;
    public CapsuleCollider2D CC;

    public AudioSource AS;

    public AudioClip BGM;
    public AudioClip Jump;
    public AudioClip Die;

    private float speed = 2;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        Am.SetFloat("Speed", speed);

    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isJump)
        {
        speed = 0;
        Am.SetFloat("Speed", speed);  
        rg2d.AddForce(new Vector2(0, 9), ForceMode2D.Impulse); 
        isJump = true;
        AS.PlayOneShot(Jump);
        }
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag == "Floor")
        {
        speed = 1;
        Am.SetFloat("Speed", speed);
        isJump = false;  
        }
             
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        speed = 0;
        Am.SetFloat("Speed", speed);
        rg2d.AddForce(new Vector2(0, 9), ForceMode2D.Impulse); 
        Destroy(CC);   
        AS.PlayOneShot(Die);
        BearioGameManager.instance.isdead = true;
    }

    
    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
  
}
