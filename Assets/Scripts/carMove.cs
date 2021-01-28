using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class carMove : MonoBehaviour
{
    private float horizontalInput; //Turn input.
    public float Velocity = 2.65f;  // Max applied force.
    public float steerSpeed = 1.0f; //Steer (rotation) speed).
    private Rigidbody rigidBody; //Body of the object.
    private Button turnLeft;
    private Button turnRight;
    

    public static bool isFail = false;
    public static bool isFinish = false;

    public static bool buttonDownLeft;
    public static bool buttonDownRight;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        turnLeft = GameObject.Find("Left").GetComponent<Button>();
        turnRight = GameObject.Find("Right").GetComponent<Button>();
        
    }
      void Update()
    {
        buttonDownLeft = false;
        turnLeft.onClick.AddListener(incrementLeft);
        turnRight.onClick.AddListener(incrementRight);

        transform.Rotate(0, steerSpeed * horizontalInput, 0);
        horizontalInput = 0;
        rigidBody.velocity = transform.right * Velocity;

    }
    void incrementLeft()
    {
        buttonDownLeft = true;
        horizontalInput = -10.0f;
    }
    void incrementRight()
    {
        buttonDownRight = true;
        horizontalInput = 10.0f;
    }
    public void stopCar()
    {
        rigidBody.velocity = new Vector3(0, 0, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            isFinish = true;

        }

        else
        {
            isFail = true;
  
        }

    }
}
