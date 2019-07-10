using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
{
	Vector3 currentposition;
    public GameObject target;

    public bool killer = false;

    public bool isAlien = false;
    private Rigidbody2D rigidbody2D;

	Vector3 targetposition1;
    public Animator animator ;
    public float speed;

   void Awake()
   {
       switch(AlienSceneManager.instance.levelDifficulty)
       {
           case 1:
            speed = Random.Range(2f,5f);
           break;
           case 2:
            speed = Random.Range(4f,7f);
           break;
           case 3:
            speed = Random.Range(6f,9f);
           break;
       }
        animator = GetComponent<Animator>();
   }

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 0;
        
    }

    // Update is called once per frame
    void Update()
    {   
        animator.SetFloat("Speed",speed);

        currentposition = this.transform.position;
        if(target)
        {   
            targetposition1 = target.transform.position;
            rigidbody2D.MovePosition(Vector3.MoveTowards(currentposition,targetposition1, speed * Time.deltaTime));
        }else
        {
            
        }
        			
    }


    void OnMouseDown()
    {
        if(!AlienSceneManager.instance.clicked)
        {
             if(!isAlien)
            {
                AlienSceneManager.instance.killer = true;
                AlienSceneManager.instance.peoples.Remove(gameObject);

            }else
            {
                GameManager.instance.SetGameResult(true);
            }
            AlienSceneManager.instance.clicked = true;        
            Destroy(gameObject);
        }
    }
}
