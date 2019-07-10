using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts : MonoBehaviour
{

    private Rigidbody2D rigidibody2D;
    private Vector3 currentPosition;

    private Vector3 targetPosition;

    public bool isInRange;

    private bool attached;
    public spawnPosition spawnedPosition; 
    public enum spawnPosition
    {
        left,
        right,
        up,
        bot
    }

    private float speed = 2;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        rigidibody2D = GetComponent<Rigidbody2D>();
        switch(spawnedPosition)
            {
                case spawnPosition.left:
                transform.position = (new Vector3(Random.Range(-12, -10),Random.Range(-5, 3), 0));
                break;

                case spawnPosition.right:
                transform.position = (new Vector3(Random.Range(12, 10),Random.Range(-5, 3), 0));
                
                break;

                case spawnPosition.up:
                transform.position = (new Vector3(Random.Range(-7.8f, 7.6f),Random.Range(5.4f, 6.6f), 0));
                
                break;

                case spawnPosition.bot:
                transform.position = (new Vector3(Random.Range(-7.8f, -7.6f),Random.Range(-7.4f, -6.6f), 0));               
                break;

                
            }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = this.transform.position;

        if(!attached)
        {
            switch(spawnedPosition)
            {
                case spawnPosition.left:
                rigidibody2D.MovePosition(Vector3.MoveTowards
                (currentPosition,(new Vector3(currentPosition.x + 100,currentPosition.y,currentPosition.z)),
                speed * Time.deltaTime));
                break;

                case spawnPosition.right:
                rigidibody2D.MovePosition(Vector3.MoveTowards
                (currentPosition,(new Vector3(currentPosition.x - 100,currentPosition.y,currentPosition.z)),
                speed * Time.deltaTime));
                break;

                case spawnPosition.up:
                rigidibody2D.MovePosition(Vector3.MoveTowards
                (currentPosition,(new Vector3(currentPosition.x,currentPosition.y - 100,currentPosition.z)),
                speed * Time.deltaTime));
                break;

                case spawnPosition.bot:
                rigidibody2D.MovePosition(Vector3.MoveTowards
                (currentPosition,(new Vector3(currentPosition.x,currentPosition.y + 100,currentPosition.z)),
                speed * Time.deltaTime));
                break;

                
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Controlled")
        {
        this.transform.parent = other.gameObject.transform;
        attached = true;
        this.gameObject.tag = "Controlled";
        rigidibody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        isInRange = true;
        print("HERE!");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        isInRange = false;
    }
}
