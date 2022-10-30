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
    [SerializeField] private float _sideJumpForce;
    [SerializeField] private float CAYOTE_TIME = 0.5f;
    float _cayoteTime ;

    
    [Header("Components")]
    private Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider2D;
    [SerializeField] LayerMask platformLayer;

    private void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
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
        

            if (Input.GetButtonDown("Jump") && isGrounded())
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, _jumpForce);
            }

            if (Input.GetButtonUp("Jump") && rigidbody2D.velocity.y > 0f)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y * 0.5f);
            }


    }

    private bool isGrounded(){
        float extraHeight = .2f;
        float extraWidth = .1f;

        RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.down,boxCollider2D.bounds.extents.y + extraHeight, platformLayer);
        RaycastHit2D raycastHitLeft = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.left,boxCollider2D.bounds.extents.x + extraWidth, platformLayer);
        RaycastHit2D raycastHitRight = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.right,boxCollider2D.bounds.extents.x + extraWidth, platformLayer);
        

        if (raycastHit.collider == null && raycastHitLeft.collider == null && raycastHitRight.collider == null ){
            return false;
        }
        else{
            return true;        
        }
        
    }

    
    

    
}

