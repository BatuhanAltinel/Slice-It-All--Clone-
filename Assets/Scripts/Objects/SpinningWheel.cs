using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningWheel : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0.5f, 0f, 0f));    
    }
}
