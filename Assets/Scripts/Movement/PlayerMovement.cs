using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 6,  rotationSpeed = 100;
    [SerializeField]
    private Animator characterAnimator;

    [SerializeField]
    private Transform targetObject;

    [SerializeField]
    private bool isCharacterDead = false;

    // Update is called once per frame
    void Update()
    {
        float move = (Input.GetAxis("Vertical") * movementSpeed) * Time.deltaTime;
        float rotation = (Input.GetAxis("Horizontal") * rotationSpeed) * Time.deltaTime;

        if(!isCharacterDead){
            transform.Rotate(0, rotation, 0);
        }
        
        if(move != 0){
            characterAnimator.SetBool("isCharacterWalking", true);
        } else{
            characterAnimator.SetBool("isCharacterWalking", false);
        }

        if(isCharacterDead && Input.GetKeyDown(KeyCode.Space)){
            characterAnimator.SetTrigger("CharacterRevive");
            isCharacterDead = false;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Box")){
            characterAnimator.SetTrigger("CharacterDeath");
            isCharacterDead = true;
        }
    }
    // IK

    // private void OnAnimatorIK(int layerIndex)
    // {
    //     characterAnimator.SetLookAtWeight(1);
    //     characterAnimator.SetLookAtPosition(targetObject.position);

    //     characterAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
    //     characterAnimator.SetIKPosition(AvatarIKGoal.LeftHand, targetObject.position);
    // }

    // END IK

}
