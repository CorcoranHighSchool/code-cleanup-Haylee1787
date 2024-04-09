using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //The player's Rigidbody
    private Rigidbody playerRb;
    //Jump force
    private float jumpForce = 15.0f;
    //Gravity Modifier
    [SerializedFeild] private float gravityModifier;
    //Are we on the ground?
    private bool isOnGround = true;
    //Is the Game Over
    public bool gameOver = {get; private set;};
    //Player Animator
    private Animator playerAnim;
    //ParticleSystem explosion
    [SerializedFeild] private ParticleSystem explositionParticle;
    //ParticleSystem dirt
    [SerializedFeild] private ParticleSystem dirtParticle;
    //Jump sound
    [SerializedFeild] private AudioClip jumpSound;
    //Crash sound
    [SerializedFeild] private AudioClip crashSound;
    //Player Audio
    [SerializedFeild] private AudioSource playerAudio;
    //const sring 
    private const string gameOverString = "Game Over!";
    private const string jumpTrig = "Jump_trig";
    private const string obstacle = "Obstacle";
    private const string ground = "Ground";
    private const string death_b = "Death_b";
    private const string deathType = "DeathType_int"; 

    // Start is called before the first frame update
    void Start()
    {
        //Get the rigidbody
        playerRb = GetComponent<Rigidbody>();
        //Connect the Animator
        playerAnim = GetComponent<Animator>();
        //Player Audio
        //playerAudio.GetComponent<AudioSource>();
        //update the gravity
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        //If we press space, jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //trigger the jump animation
            playerAnim.SetTrigger(jumpTrig);
            isOnGround = false;
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            dirtParticle.Stop();

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(ground))
        {
            dirtParticle.Play();
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag(Obstacle))
        {
            explositionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);

            gameOver = true;
            Debug.Log(gameOverString);
            playerAnim.SetBool(deathB, true);
            playerAnim.SetInteger("deathInt, 1);
        }
    }
}
