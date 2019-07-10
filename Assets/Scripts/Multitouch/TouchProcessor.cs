using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchProcessor : MonoBehaviour
{
    
    GameObject[] touchesOld;
    List<GameObject> touchList = new List<GameObject>();

    private void Start() {
        
        Input.multiTouchEnabled = true;
    }

    void Update()
    {
        print(Input.touchCount);

        if(Input.touchCount > 0){
            touchesOld = new GameObject[touchList.Count];
            touchList.CopyTo(touchesOld);
            touchList.Clear();


            foreach (Touch touch in Input.touches)

            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                RaycastHit2D hit;
                if(hit = Physics2D.GetRayIntersection(ray)){
                    GameObject recipient = hit.collider.gameObject;
                    touchList.Add(recipient);

                    if(touch.phase == TouchPhase.Began){
                        recipient.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                    if(touch.phase == TouchPhase.Ended){
                        recipient.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                    if(touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved){
                        recipient.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                    if(touch.phase == TouchPhase.Canceled){
                        recipient.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                    }

                }
            }
            foreach (GameObject g in touchesOld){
                if(!touchList.Contains(g)){
                    g.SendMessage("OnTouchExit",  SendMessageOptions.DontRequireReceiver);
                }
            }
        
        }
            
        
    }
}
