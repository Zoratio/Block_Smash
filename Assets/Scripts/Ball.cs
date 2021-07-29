using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 0f;
    [SerializeField] float yPush = 15f;

    [SerializeField] AudioClip paddleBounce;
    [SerializeField] AudioClip blockBounce;
    [SerializeField] AudioClip wallBounce;
    [SerializeField] AudioClip unbreakableBounce;
    [SerializeField] float randomFactor = 0.2f;



    Vector2 paddleToBallVector;
    bool hasStarted = false;
    AudioClip audioClip;
    AudioSource audioSource;    //If I change the volume for this, do it for the block breaking as well
    Rigidbody2D myRigidBody2D;



    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        audioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }        
    }
    
    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            myRigidBody2D.velocity = new Vector2(xPush, yPush);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));
        if (hasStarted)
        {
            string tag = collision.gameObject.tag;

            switch (tag)
            {
                case "Player":
                    audioClip = paddleBounce;
                    break;

                case "Wall":
                    audioClip = wallBounce;
                    break;

                case "Breakable":
                    audioClip = blockBounce;
                    break;

                case "Unbreakable":
                    audioClip = unbreakableBounce;
                    break;

                default:
                    return;
            }
            audioSource.PlayOneShot(audioClip);
            myRigidBody2D.velocity += velocityTweak;
        }        
    }
}
