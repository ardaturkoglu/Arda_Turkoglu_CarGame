using UnityEngine;

public class failSystem : MonoBehaviour
{
    public Transform startingPosition;
    public GameObject nextCar;
    public GameObject nextExit;
    private Vector3 startPos;
    private Quaternion startRot;
    private bool isFail = false;
    private bool isFinish = false;


    // Start is called before the first frame update
    void Start()
    {
        startPos = startingPosition.position;
        startRot = startingPosition.rotation;
      
    }

    // Update is called once per frame
    void Update()
    {
        if (isFail)
        {
            restartCar();
            isFail = false;
        }
        if (isFinish)
        {
            gameObject.SetActive(false);
            nextCar.SetActive(true);
            isFinish = false;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.CompareTag("Finish"))
        {
            isFinish = true;
            Debug.Log("Exit");
            
        }
        else
        {
            Debug.Log("Hit " + other.gameObject.name);
            isFail = true;
        }
            

    }



    void restartCar()
    {
        transform.position = startPos;
        transform.rotation = startRot;
        
    }
}
