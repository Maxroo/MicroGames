using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lowerText;
    bool isFade = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fade();
    }

    void fade()
    {
        CanvasGroup CG = lowerText.GetComponent<CanvasGroup>();

        if(isFade)
        {
            CG.alpha -= 0.03f;
            if(CG.alpha == 0)
            {
                isFade = false;
            }
        }else if(!isFade)
        {
            CG.alpha += 0.03f;
            if(CG.alpha == 1)
            {
                isFade = true;
            }
        }

    }
}
