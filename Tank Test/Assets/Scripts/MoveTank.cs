using UnityEngine;

public class MoveTank : MonoBehaviour
{

    public float acceleration;
    public float maxSpeed;
    public float brakeSpeed;
    public float turnSpeed;

    private Vector2 currentSpeed;
    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        currentSpeed = new Vector2(rb2d.velocity.x, rb2d.velocity.y);

        if(currentSpeed.magnitude > maxSpeed)
        {
            currentSpeed = currentSpeed.normalized;
            currentSpeed *= maxSpeed;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb2d.AddForce(transform.up * acceleration);
            rb2d.drag = brakeSpeed;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb2d.AddForce(-(transform.up * acceleration));
            rb2d.drag = brakeSpeed;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Rotate(Vector3.forward * turnSpeed);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Rotate(Vector3.back * turnSpeed);
        }
        
        if (!(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
        {
           rb2d.drag = brakeSpeed * 2;
        }
    }
}