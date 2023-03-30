using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<KnifeSharpSide>() || other.GetComponent<KnifeHandle>())
        {
            // EventManager.OnStuck.Invoke();
            EventManager.OnFail.Invoke();
            GameManager.Instance.SetGameState(GameState.Lose);
        }
    }
}
