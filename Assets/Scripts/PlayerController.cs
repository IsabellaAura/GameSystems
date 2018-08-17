using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CTRL + K + D (Cleans code)
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 10f;
    public Rigidbody rb;
    public float rayDistance = 1f;
    


    // Implement this OnDrawGizmosSelected if you want to draw gizmos only if the object is selected
    private void OnDrawGizmosSelected() 
    {
        Ray groundRay = new Ray(transform.position, Vector3.down);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundRay.origin, groundRay.origin + groundRay.direction * rayDistance);
    }

    bool IsGrounded() 
    {
        Ray groundRay = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(groundRay, out hit, rayDistance)) 
        {
            return true;    
        }
        return false;
    }  
 
    


    // Update is called once per frame
    void Update () 
    {
        float inputH = Input.GetAxis("Horizontal") * moveSpeed;
        float inputV = Input.GetAxis("Vertical") * moveSpeed;
        Vector3 moveDir = new Vector3(inputH, 0f, inputV);
        Vector3 force = new Vector3(moveDir.x, rb.velocity.y, moveDir.z);

        if (Input.GetButton("Jump") && IsGrounded()) 
        {
            force.y = jumpHeight;
        }
        rb.velocity = force;

        // If the user pressed a key (moveDir has values in it other than 0) 
        if(moveDir.magnitude > 0) 
        {
            // Rotate the player to that moveDir
            transform.rotation = Quaternion.LookRotation(moveDir);
        }
    }
}



    

    
          
    

