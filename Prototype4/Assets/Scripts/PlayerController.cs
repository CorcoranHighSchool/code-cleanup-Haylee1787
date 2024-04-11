using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    [SeralizedFeild] float speed = 5.0f;
    [SeralizedFeild] GameObject focalPoint;
    [SeralizedFeild] bool hasPowerup;
    [SeralizedFeild] float powerUpStrength = 15.0f;
    [SeralizedFeild] GameObject powerupIndicator;
    private const string focalPoint = "Focal Point";
    private const string vertical = "Vertical";
    private const string powerup = "Powerup";
    private const string enemy = "Enemy";
    private const string collideWith = "Collide with";
    private const string withPowerupSetTO = "with powerup set to";

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find(focalPoint);
    }

    // Update is called once per frame
    void Update()
    {
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        float verticalInput = Input.GetAxis(vertical);
        playerRb.AddForce(focalPoint.transform.forward * speed * verticalInput);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(powerup))
        {
            powerupIndicator.SetActive(true);   //
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDownRoutine());
        }
    }

    private IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.SetActive(false);      //
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(enemy) && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayfromPlayer = (collision.gameObject.transform.position - transform.position);

            Debug.Log( collideWith + collision.gameObject.name +
                withPowerupSetTo + hasPowerup);
            enemyRigidbody.AddForce(awayfromPlayer * powerUpStrength, ForceMode.Impulse);
        }
    }
}
