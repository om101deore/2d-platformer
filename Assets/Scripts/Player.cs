using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("movement")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    private float _direction;
    private bool isFacingRight;

    [Header("Jump Parameter")]
    private bool _canJump = true;
    [SerializeField] private float _jumpForce;
    
    [Header("wall Jump Components")]
    private bool canLeftWallJump ;
    private bool hasWallJumpedLeft = false;
    private bool canRightWallJump;
    private bool hasWallJumpedRight = false;
    
    
    [Header("Components")]
    private Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;

    [Header("GameObjects")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheckLeft;
    [SerializeField] private Transform wallCheckRight;
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
        
            Debug.Log("left-" + canLeftWallJump);
            Debug.Log("Right-" + canRightWallJump);

   
            if(isGrounded() && Input.GetButtonDown("Jump") )
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, _jumpForce);

            if (Input.GetButtonDown("Jump") && canLeftWallJump && isLeftWalled() )
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, _jumpForce);
                canLeftWallJump = false;
                canRightWallJump = true;
            }             


            if ( Input.GetButtonDown("Jump") && canRightWallJump && isRightWalled())
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, _jumpForce);
                canRightWallJump = false;
                canLeftWallJump = true;
            }   
                
        



            
            if (Input.GetButtonUp("Jump") && rigidbody2D.velocity.y > 0f)
            {   
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y * 0.5f);
                
            }


    }

    

    private bool isGrounded(){
        canLeftWallJump = true;
        canRightWallJump = true;

        return Physics2D.OverlapCircle(groundCheck.position,0.2f,platformLayer);
        
    }

    private bool isRightWalled(){
        return Physics2D.OverlapCircle(wallCheckRight.position,0.2f,platformLayer);
        
    }

    private bool isLeftWalled(){
        return Physics2D.OverlapCircle(wallCheckLeft.position,0.2f,platformLayer);
        
    }
     

  
    
    

    
}

