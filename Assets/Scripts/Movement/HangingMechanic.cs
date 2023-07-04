using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingMechanic : MonoBehaviour
{
    public GameObject rootP;

    void OnTriggerEnter(Collider col){
        if(col.gameObject.CompareTag("Hand")){
            col.GetComponentInParent<PlayerMovement>().hangingMechanic(rootP.transform);
        }
    }
}
