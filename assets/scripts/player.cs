using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxcollider;
    private Vector3 moveDelta;
    private bool FacingRight = true;
    private RaycastHit2D hit;
    //what is the unit difference of the distance between each movement
    void Start()
    {
        boxcollider = GetComponent<BoxCollider2D>();
    }
    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveDelta = new Vector3(x, y, 0);

        //FLIPPY
        if (moveDelta.x > 0 && !FacingRight) //move right while facing left
            Flippy();
        else if (moveDelta.x < 0 && FacingRight) //move left while facing right
            Flippy();
        hit = Physics2D.BoxCast(transform.position, boxcollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Kyara", "Obstacles"));
        if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxcollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Kyara", "Obstacles"));
        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }

    void Flippy()
    {
        // Switch the way the player is labelled as facing.
        //to stop any unwanted flippys from happening
        FacingRight = !FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 scaly = transform.localScale;
        scaly.x *= -1;
        transform.localScale = scaly;
    }
}
