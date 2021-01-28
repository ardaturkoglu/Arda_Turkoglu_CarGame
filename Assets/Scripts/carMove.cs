using UnityEngine;
using UnityEngine.UI;

public class carMove : MonoBehaviour
{
    //Give references to public's from editor.
    private float horizontalInput; //Turn input.

    public float Velocity = 2.65f;  // Max applied force.
    public float steerSpeed = 1.0f; //Steer (rotation) speed).

    private Rigidbody rigidBody; //Body of the object.
    private Button turnLeft; //Buttons
    private Button turnRight;


    public static bool isFail = false;//Is car crashed.
    public static bool isFinish = false;//Is car reached target.

    public static bool buttonDownLeft; //Is button pressed.
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
        turnLeft.onClick.AddListener(incrementLeft);//Rotate left
        turnRight.onClick.AddListener(incrementRight);//Rotate right

        transform.Rotate(0, steerSpeed * horizontalInput, 0);
        horizontalInput = 0;
        rigidBody.velocity = transform.right * Velocity; //Velocity of object.

    }
    void incrementLeft() //Rotate Left
    {
        buttonDownLeft = true;
        horizontalInput = -10.0f;
    }
    void incrementRight() // Rotate Right
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
        if (other.gameObject.CompareTag("Finish")) //If object hits exit, car finished.
        {
            isFinish = true;

        }

        else //If hits another object,return fail.
        {
            isFail = true;

        }

    }
}
