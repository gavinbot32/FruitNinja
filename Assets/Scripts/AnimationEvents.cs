using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activeFalse()
    {
       gameObject.SetActive(false);
    }

    public void activeTrue()
    {
        gameObject.SetActive(true);
    }
}
