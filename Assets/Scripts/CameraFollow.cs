using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _knife;
    [SerializeField] Vector3 _offset;
    [SerializeField] float moveSpeed;

    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _offset + _knife.position,moveSpeed * Time.deltaTime);
        // transform.position = _offset + _knife.transform.position;
    }
}
