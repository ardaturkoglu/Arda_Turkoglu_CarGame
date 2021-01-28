using UnityEngine;

public class gameManager : carMove
{
    public GameObject[] levels;
    private Transform[] startingPositions;
    private GameObject[] cars;
    private GameObject[] exits;
    private int carCount = 0;
    private int levelNo = 0;
    private bool levelFinished = false;
    private TextMesh carInfo;
    private Vector3 startPos;
    private Quaternion startRot;


    // Start is called before the first frame update
    void Start()
    {
        carInfo = new TextMesh();
        carInfo = GameObject.Find("Info").GetComponent<TextMesh>();
        carInfo.text = "Level " + (levelNo + 1).ToString() + "\nCar No:" + (carCount + 1).ToString();
        levels[0].SetActive(true);
        for (int i = 1; i < levels.Length; i++)
            levels[i].SetActive(false);
        exits = new GameObject[levels[levelNo].transform.GetChild(1).transform.childCount];
        cars = new GameObject[levels[levelNo].transform.GetChild(2).transform.childCount];
        startingPositions = new Transform[cars.Length];
        getLevelInfo(levels);
        startPos = startingPositions[0].position;
        startRot = startingPositions[0].rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && levelFinished == false)
            Time.timeScale = 1;
        {
            if (isFail)
            {
                restartCar();
                isFail = false;
                Time.timeScale = 0;
                carInfo.text = "Level " + (levelNo + 1).ToString() + "\nCar No:" + (carCount + 1).ToString() + "\nYou Hit! \nPress screen to try again.";
            }
            if (isFinish && carCount <= 7)
            {
                cars[carCount].SetActive(false);
                if (carCount < 7)
                    cars[carCount + 1].SetActive(true);
                exits[carCount].SetActive(false);
                if (carCount < 7)
                    exits[carCount + 1].SetActive(true);
                isFinish = false;
                carCount += 1;
                carInfo.text = "Level " + (levelNo + 1).ToString() + "\nCar No:" + (carCount + 1).ToString();
                if (carCount < 7)
                {
                    startPos = startingPositions[carCount].position;
                    startRot = startingPositions[carCount].rotation;
                }
                Time.timeScale = 0;
                if (carCount > 7)
                    levelFinished = true;
            }
            else if (levelFinished && levelNo < levels.Length)
            {
                carInfo.text = "Entering Level " + (levelNo + 1).ToString() + "\nCar No:" + (carCount + 1).ToString();
                levelFinished = false;
                levels[levelNo].SetActive(false);
                levelNo += 1;
                levels[levelNo].SetActive(true);
                carCount = 0;

                getLevelInfo(levels);
                startPos = startingPositions[0].position;
                startRot = startingPositions[0].rotation;

            }
        }
    }

    void getLevelInfo(GameObject[] level)
    {
        for (int j = 0; j < levels[levelNo].transform.GetChild(1).transform.childCount; j++)
        {
            exits[j] = levels[levelNo].transform.GetChild(1).transform.GetChild(j).gameObject;
        }
        for (int j = 0; j < levels[levelNo].transform.GetChild(2).transform.childCount; j++)
        {
            cars[j] = levels[levelNo].transform.GetChild(2).transform.GetChild(j).gameObject;
        }

        for (int i = 0; i < cars.Length; i++)
        {
            startingPositions[i] = cars[i].transform;
        }
    }

    void restartCar()
    {
        cars[carCount].transform.position = startPos;
        cars[carCount].transform.rotation = startRot;
    }
}
