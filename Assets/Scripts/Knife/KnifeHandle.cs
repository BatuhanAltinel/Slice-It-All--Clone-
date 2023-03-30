using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeHandle : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Sliceable>() || other.CompareTag("Platform"))
        {
           EventManager.OnPushBack.Invoke();
        }
        
        if (other.CompareTag("Ground"))
        {
            EventManager.OnStuck.Invoke();
            GameManager.Instance.SetGameState(GameState.Lose);
        }
    }    
     
}
