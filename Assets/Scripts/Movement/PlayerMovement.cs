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

    private bool isCharacterHanging = false;

    private Transform rootTargetObject;

    private bool hangingMoveTrigger;

    void Start() {
        rootTargetObject = null;
        hangingMoveTrigger = true;
    }

    void FixedUpdate() {
        if(!rootTargetObject)
            return;

        if(isCharacterHanging && hangingMoveTrigger) {
            transform.position = rootTargetObject.position;
            hangingMoveTrigger = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Vertical");
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        if(!isCharacterDead && !isCharacterHanging){
            rotation *= Time.deltaTime;
            transform.Rotate(0, rotation, 0);
        }

        if(isCharacterHanging){
            if(rotation > 1){
                characterAnimator.SetBool("isCharHangingRight", true);
            } else if(rotation < -1){
                characterAnimator.SetBool("isCharHangingLeft", true);
            } else{
                characterAnimator.SetBool("isCharHangingRight", false);
                characterAnimator.SetBool("isCharHangingLeft", false);
            }
        }
        
        if(move != 0){
            characterAnimator.SetBool("isCharacterWalking", true);
        } else{
            characterAnimator.SetBool("isCharacterWalking", false);
        }

        if(isCharacterDead && Input.GetKeyDown(KeyCode.Z)){
            characterAnimator.SetTrigger("CharacterRevive");
            isCharacterDead = false;
        }

        if(!isCharacterDead && Input.GetKeyDown(KeyCode.Space)){
            characterAnimator.SetTrigger("CharacterJump");
        }

        // Climbing over wall
        if(isCharacterHanging && Input.GetKeyDown(KeyCode.Z)){
            StartCoroutine("ClimbingOverWall");
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Box")){
            characterAnimator.SetTrigger("CharacterDeath");
            isCharacterDead = true;
        }
    }

    public void hangingMechanic(Transform rootTarget){
        if(isCharacterHanging)
            return;
        
        characterAnimator.SetTrigger("CharacterHanging");
        GetComponent<Rigidbody>().isKinematic = true;
        isCharacterHanging = true;
        rootTargetObject = rootTarget;
        
    }

    IEnumerator ClimbingOverWall(){
        characterAnimator.SetTrigger("CharacterClimbOver");
        yield return new WaitForSeconds(3.7f);
        GetComponent<Rigidbody>().isKinematic = false;
        isCharacterHanging = false;
        hangingMoveTrigger = true;
        
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
