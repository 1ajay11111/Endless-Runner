using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    private int desiredLane = 1;// 0:Left,1:middle,2:Right
    public float laneDistance = 4;// the distance between 2 lanes
    public float jumpForce;
    public float Gravity = -20;
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float maxSpeed;
    public Animator animator;
    private bool isSliding = false;
    
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!PlayerManager.isGameStarted)
            return;
        //increase speed 
        if(forwardSpeed<maxSpeed)
        {
            forwardSpeed += .1f * Time.deltaTime;
        }
        
        animator.SetBool("isGameStarted", true);

        direction.z = forwardSpeed;
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);
        
        if (controller.isGrounded)

        {
            
            if (SwipeManager.swipeUp)
            {
                isGrounded = true;
                Jump();
            }
        }else
        {
            direction.y += Gravity * Time.deltaTime;

        }
        if (SwipeManager.swipeDown &&!isSliding)
        {
            StartCoroutine(Slide());
        }
        //gather the  inputs on which lane should be
        if (SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
             desiredLane = 2;
        }
        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }
        //calculate where we should be in future

        Vector3 targetposition =transform.position.z*transform.forward + transform.position.y*transform.up;
        if(desiredLane == 0)
        {
            targetposition += Vector3.left * laneDistance;
        }else if (desiredLane == 2)
        {
            targetposition += Vector3.right* laneDistance;
        }

        // transform.position = Vector3.Lerp(transform.position,targetposition, 70 * Time.fixedDeltaTime);
        // controller.center = controller.center;
        if (transform.position == targetposition)
            return;
        Vector3 diff = targetposition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
        

    }
    private void FixedUpdate()
    {
        if (!PlayerManager.isGameStarted)
            return;
        controller.Move(direction * Time.fixedDeltaTime);

    }
    private void Jump()
    {
        direction.y = jumpForce;
        FindObjectOfType<AudioManager>().PlaySound("jump");

    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag =="Obstacle")
        {
            PlayerManager.gameover = true;
            FindObjectOfType<AudioManager>().PlaySound("gameover");
        }
    }

   
    private IEnumerator Slide()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);
        controller.center = new Vector3(0,-0.5f, 0);
        controller.height = 1;
        yield return new WaitForSeconds(1.3f);
        controller.center = new Vector3(0, 0, 0);
        controller.height = 2;
        animator.SetBool("isSliding", false);
        isSliding = false;
    }

}
