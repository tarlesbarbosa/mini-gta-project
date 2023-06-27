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

    // Update is called once per frame
    void Update()
    {
        float move = (Input.GetAxis("Vertical") * movementSpeed) * Time.deltaTime;
        float rotation = (Input.GetAxis("Horizontal") * rotationSpeed) * Time.deltaTime;

        transform.Rotate(0, rotation, 0);

        if(move != 0){
            characterAnimator.SetBool("isCharacterWalking", true);
        } else{
            characterAnimator.SetBool("isCharacterWalking", false);
        }
    }

    // IK

    private void OnAnimatorIK(int layerIndex)
    {
        characterAnimator.SetLookAtWeight(1);
        characterAnimator.SetLookAtPosition(targetObject.position);

        characterAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        characterAnimator.SetIKPosition(AvatarIKGoal.LeftHand, targetObject.position);
    }

    // END IK

}
