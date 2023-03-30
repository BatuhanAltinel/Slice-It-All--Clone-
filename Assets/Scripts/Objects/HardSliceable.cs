using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


[RequireComponent(typeof(MeshRenderer))]

public class HardSliceable : MonoBehaviour
{
    [SerializeField] private int _currencyAmount;

    [SerializeField] List<Rigidbody> _rigidBodies;

    [SerializeField] int X_direction = 1;
    [SerializeField] float _sliceForce = 10f;

    [SerializeField] ParticleSystem _splashParticle;
    [SerializeField] TextMeshProUGUI _currencyText;

    [SerializeField] float _moveSpeed;
    [SerializeField] GameObject _warningImage;

    MeshRenderer _meshRenderer;

    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        if(_currencyText != null)
        {
            _currencyText.text = $"+ {_currencyAmount}";
            _currencyText.gameObject.SetActive(false);
        }
            
    }

    void Start()
    {
        WarningBouncing();
    }

     void WarningBouncing()
    {
        _warningImage.transform.DOScale(new Vector3(1.7f,1.7f,1),1f).SetEase(Ease.InBounce).SetLoops(-1,LoopType.Yoyo);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<KnifeSharpSide>(out KnifeSharpSide ks))
        {
            if(ks.knifeState == KnifeState.SUPER)
            {
                OnSlice();
                EventManager.OnSlice.Invoke();
                EventManager.OnObjectSliced?.Invoke(this.GetCurrencyAmount());
            }else
                EventManager.OnPushBack.Invoke();
        }
    }

    public void OnSlice()
    {
        Combo _combo = GameManager.Instance.GetComponent<Combo>();

        _meshRenderer.enabled = false;
        _warningImage.gameObject.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;

        _combo.IsSlice = true;

        if(_splashParticle != null)
            _splashParticle.Play();
        if(_currencyText != null)
            CurrencyTextTween();

        foreach (var rb in _rigidBodies)
        {
            if(rb != null)
            {
                rb.isKinematic = false;
                SliceForce(rb);
                X_direction *= -1;    
            }
            
        }
    }

    void SliceForce(Rigidbody rb)
    {
        rb.AddForce(new Vector3(X_direction,0.1f,0) * _sliceForce * Time.deltaTime,ForceMode.Impulse);
    }

    public int GetCurrencyAmount()
    {
        return _currencyAmount;
    }

    void CurrencyTextTween()
    {
        _currencyText.gameObject.SetActive(true);
        _currencyText.transform.DOLocalMoveY(4f,1.2f).OnComplete(() => _currencyText.gameObject.SetActive(false));
    }

}
