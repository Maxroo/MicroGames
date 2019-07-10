using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gumba : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rg;

    private float speed = 10; 

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // rg.AddForce(new Vector2(-2,0));

        switch(BearioGameManager.instance.level)
        {
            case 1:
            speed = 10;
            break;
            case 2:
            speed = 18;
            break;
            case 3:
            speed = 18;
            break;
        }
        rg.MovePosition(rg.position + new Vector2(-speed,0) * Time.deltaTime);
    }
}
