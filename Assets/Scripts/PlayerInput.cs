using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Touch _touch;
    bool _touched = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);

            if(_touch.phase == TouchPhase.Began && !_touched)
            {
                EventManager.OnTap.Invoke();
                _touched = true;

            }
            if(_touch.phase == TouchPhase.Ended)
            {
                _touched = false;
            }
        }
    }

}
