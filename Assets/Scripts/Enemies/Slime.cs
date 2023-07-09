using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    private void Update()
    {
        #region TIMERS
        jumpTime -= Time.deltaTime;
        #endregion
        if (jumpTime <= 0)
            Jump();
    }

    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float jumpCouldown = 1f;
    private float jumpTime=1f;
    private void Jump()
    {
        jumpTime=jumpCouldown;
        float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    // DELETE THIS
    public void Kill()
    {
        Health--;
    }
}
