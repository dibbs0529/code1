using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour
{




    public float speed = 10;
    public float gravity = 10;
    public float maxVelocityChange = 10;
    private bool grounded;
    public float jumpheight = 2;
    public int points = 0;
    private bool dead;
    public int health;
    private Transform playerTransform;
    private Rigidbody _rigidbody;




    // Use this for initialization
    void Start()
    {

        playerTransform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        _rigidbody.freezeRotation = true;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

        playerTransform.Rotate(0, Input.GetAxis("Horizontal"), 0);

        Vector3 targetVelocity = new Vector3(0, 0, Input.GetAxis("Vertical"));
        targetVelocity = playerTransform.TransformDirection(targetVelocity);
        targetVelocity = targetVelocity * speed;

        Vector3 velocity = _rigidbody.velocity;
        Vector3 velocityChange = targetVelocity - velocity;
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;
        _rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

        if (Input.GetButton("Jump") && grounded)
        {
            _rigidbody.velocity = new Vector3(velocity.x, CalculateJump(), velocity.z);

        }

        _rigidbody.AddForce(new Vector3(0, -gravity * _rigidbody.mass, 0));
        grounded = false;



    }
    void OnCollisionStay()
    {

        grounded = true;

    }


    float CalculateJump()
    {

        float Jump = Mathf.Sqrt(2 * jumpheight * gravity);
        return Jump;
    }

    void OnTriggerEnter(Collider bud)
    {

        if (bud.tag == "Coin")
        {
            points = points + 5;
            Destroy(bud.gameObject);
        }

    }
}




