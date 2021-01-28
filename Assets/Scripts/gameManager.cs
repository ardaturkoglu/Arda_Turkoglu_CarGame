using UnityEngine;

public class gameManager : carMove
{
    public GameObject[] levels; //Game levels
    private Transform[] startingPositions; //Entrances
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
        //Inıtalize objects of the first level.
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
        if (Input.touchCount > 0 && levelFinished == false) //Touch to resume game.
            Time.timeScale = 1;
        {
            if (isFail) //If car crushed,restart and stop time.
            {
                restartCar();
                isFail = false;
                Time.timeScale = 0;
                carInfo.text = "Level " + (levelNo + 1).ToString() + "\nCar No:" + (carCount + 1).ToString() + "\nYou Failed! \nPress screen to try again.";
            }
            if (isFinish && carCount <= 7) //Car achieved its own exit,disable current, move to next exit.
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
                if (carCount <= 7)
                {
                    startPos = startingPositions[carCount].position; //Get new positions.
                    startRot = startingPositions[carCount].rotation;
                }
                Time.timeScale = 0; //Stop game.
                if (carCount > 7)
                    levelFinished = true;//Max car no achieved,go next level.
            }
            else if (levelFinished && levelNo < levels.Length) //Disable current level and move to the new level.
            {
                carInfo.text = "Entering Level " + (levelNo + 1).ToString() + "\nCar No:" + (carCount + 1).ToString();
                levelFinished = false;
                levels[levelNo].SetActive(false);
                levelNo += 1;
                if (levelNo < levels.Length)
                    levels[levelNo].SetActive(true);
                carCount = 0;
                carInfo.text = "Entering Level " + (levelNo + 1).ToString() + "\nCar No:" + (carCount + 1).ToString();
                if (levelNo < levels.Length)
                    getLevelInfo(levels); //Info about new level.
                startPos = startingPositions[0].position; //Update starting position.
                startRot = startingPositions[0].rotation;

            }
            else if (levelNo > levels.Length - 1) //No more levels,game over.
                carInfo.text = "Game Over. Thanks for playing.";
        }
    }

    void getLevelInfo(GameObject[] level) //Get current level's children.
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

    void restartCar() //Restart car 
    {
        cars[carCount].transform.position = startPos;
        cars[carCount].transform.rotation = startRot;
    }
}