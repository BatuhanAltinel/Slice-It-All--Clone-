using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum KnifeState
{
    NORMAL,
    SUPER
}
public class KnifeSharpSide : MonoBehaviour
{

    public KnifeState knifeState;
    Combo _combo;
    [SerializeField] ParticleSystem _flameParticle;

    void OnEnable()
    {
        EventManager.OnKnifeStateChange += KnifeStateChange;
    }

    void OnDisable()
    {
        EventManager.OnKnifeStateChange -= KnifeStateChange;
    }

    void Start()
    {
        _combo = GameManager.Instance.GetComponent<Combo>();
        knifeState = KnifeState.NORMAL;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Platform"))
        {
            EventManager.OnStuck.Invoke();
        }

        if (other.CompareTag("FinishPlatform"))
        {
            EventManager.OnStuck.Invoke();
            EventManager.OnFinish.Invoke();
            // Get and set multiplier of finishing platform 
            other.TryGetComponent<FinishPlatform>(out FinishPlatform fp);
            GameManager.Instance.SetMultiplier(fp.GetMultiplier());
            
            GameManager.Instance.SetGameState(GameState.Win);
        }
    }

    void KnifeStateChange(KnifeState ks)
    {
        knifeState = ks;

        switch (ks)
        {
            case KnifeState.NORMAL:
            Debug.Log("Knife Normal");
            KnifeNormalActions();
            break;
            case KnifeState.SUPER:
            Debug.Log("Knife super");
            KnifeSuperActions();
            break;
            
        }
    }

    void KnifeSuperActions()
    {
        StartCoroutine(ComboTimeRoutine());
    }

    IEnumerator ComboTimeRoutine()
    {
        
        // GetComponentInParent<MeshRenderer>().materials[1].color = Color.red; 
        _flameParticle.Play();
        yield return new WaitForSeconds(_combo.ComboDuration);
        EventManager.OnKnifeStateChange.Invoke(KnifeState.NORMAL);
    }

    void KnifeNormalActions()
    {
        _combo._isOnFire = false;
        _combo.ComboPoints = 0;
        _combo.ComboSliderAction();
        EventManager.OnNotCombo.Invoke();
        // GetComponentInParent<MeshRenderer>().materials[1].color = Color.white;
        _flameParticle.Stop();
    }
}
