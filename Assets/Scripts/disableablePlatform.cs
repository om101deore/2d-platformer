
using UnityEngine;

public class disableablePlatform : MonoBehaviour
{   
    //parameters
    [SerializeField]
    private GameObject Ground;
    GameObject cloneGround;
    private Rigidbody2D rigidbody2D;

    // instantiating
    Vector3 carPos ;

    [SerializeField] private float _disappearTime = 5f;
    
    private float currentTime ;

    [SerializeField] private bool _isVisble = false;
    private bool _takeAction = true; // take action to after every two mins

    

    private void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        currentTime = _disappearTime;
        carPos = new Vector3(transform.position.x,transform.position.y,transform.position.z);
    }
    

    // Update is called once per frame
    private void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime < 0 )
        {
            _takeAction = true;
            currentTime = _disappearTime;
        }

        instantiateGround();

       
    }

    private void instantiateGround(){

        if (!_isVisble && _takeAction)
        {
            cloneGround = Instantiate(Ground, carPos, transform.rotation);
            _isVisble = true;
            _takeAction = false;
        }

        if(_isVisble && _takeAction){

            Destroy(cloneGround);
            
            _isVisble = false;
            _takeAction = false;
        }
    }

    
}
