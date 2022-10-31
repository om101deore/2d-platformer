using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("movement")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    private float _direction;

    [Header("Jump Parameter")]
    private bool _canJump = true;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float CAYOTE_TIME = 0.5f;
    float _cayoteTime ;

    [Header("wall Components")]
    [SerializeField] private float _wallJumpForce;
    private bool wallJumpable;
    
    [Header("Components")]
    private Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;

    [Header("GameObjects")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask platformLayer;

    private void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Update() {
        // horizontal movement
        _direction = Input.GetAxis("Horizontal");
        rigidbody2D.velocity = new Vector2(_direction * _walkSpeed , rigidbody2D.velocity.y);
        
        // other functions
        jumpMovement();

        // cayote time
        _cayoteTime -= Time.deltaTime;

    }    

    
    private void OnTriggerEnter2D(Collider2D other) {
    
  
        Debug.Log("Triggered");
        if (other.gameObject.tag == "Obstacle")
        {
            Destroy(rigidbody2D.gameObject);
            Debug.Log("Game OVer");
        }
    }
    

    private void jumpMovement(){
        
        Debug.Log(Physics2D.OverlapCircle(groundCheck.position, 0.2f, platformLayer));

            if (Input.GetButtonDown("Jump"))
            {   
                if(isGrounded())
                    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, _jumpForce);

               
            }

            if (Input.GetButtonUp("Jump") && rigidbody2D.velocity.y > 0f)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y * 0.5f);
            }


    }

    private void wallJump(){

    }

    private bool isGrounded(){
        return Physics2D.OverlapCircle(groundCheck.position,0.2f,platformLayer);
        
    }

    private bool isWalled(){
        return Physics2D.OverlapCircle(wallCheck.position,0.2f,platformLayer);
        
    }

    private void Flip(){
        
        spriteRenderer.transform.localScale = new Vector3(((int)_direction), spriteRenderer.transform.localScale.y,spriteRenderer.transform.localScale.z);
    }

    
    

    
}

