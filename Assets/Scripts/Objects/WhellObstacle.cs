using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class WhellObstacle : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] GameObject _warningImage;
    void Start()
    {
        WarningBouncing();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _moveSpeed * Time.deltaTime);    
    }

    void WarningBouncing()
    {
        _warningImage.transform.DOScale(new Vector3(1.2f,1.2f,1),1f).SetEase(Ease.InBounce).SetLoops(-1,LoopType.Yoyo);
    }
}
