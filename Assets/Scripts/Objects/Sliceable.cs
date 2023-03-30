using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


[RequireComponent(typeof(MeshRenderer))]

public class Sliceable : MonoBehaviour
{
    [SerializeField] private int _currencyAmount;

    [SerializeField] GameObject _fakeObj;
    [SerializeField] List<Rigidbody> _rigidBodies;

    [SerializeField] int X_direction = 1;
    [SerializeField] float _sliceForce = 10f;

    [SerializeField] ParticleSystem _splashParticle;
    [SerializeField] TextMeshProUGUI _currencyText;

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

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<KnifeSharpSide>())
        {
            OnSlice();
            EventManager.OnSlice.Invoke();
            EventManager.OnObjectSliced?.Invoke(this.GetCurrencyAmount());
        }
    }

    public void OnSlice()
    {
        Combo _combo = GameManager.Instance.GetComponent<Combo>();

        _meshRenderer.enabled = false;

        /* Karışıklık olmasın diye böyle yaptım gelen modellerde prefabler için fake objeyi SetActive(false) yapmayacağız. */
        if(_fakeObj != null)
            _fakeObj.SetActive(false);

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
        _currencyText.transform.DOLocalMoveY(0.9f,0.7f).OnComplete(() => _currencyText.gameObject.SetActive(false));
    }

}
