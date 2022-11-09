using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Rigidbody2D rb;

    public Animator animator;

    Vector2 movement;
    Vector2 mousePos;
    


    private void Start() 
    {
        movement = Vector2.down; //Startowy kierunek postaci = Dó³.
    }
    void Update() //zmienne odpowiedzialne za ruch i animacje.
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

    }

    void FixedUpdate() //Skrypt chodzenia.
    {

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }
}
