using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEmitter : MonoBehaviour 
{

	//calls the game controller script
	public GameController controller;

	//bools
	public bool canSpawnBoss = true;
	public bool bossSpawned = false;

    public int firstTierEnd = 5000;
    public int secondTierEnd = 10000;
    public int thirdTiernEnd = 15000;

	//floats
	public float bossCounter = 1;
	public float secondsBetweenEnemies;
    public float secondsBetweenLinearEnemies;
	public float secondsBetweenSpecialEnemies;
	public float secondsBetweenPickups = 1f;
	public float secondsBeforeBegin = 4f;
    private float randomY;
    public float randomYSpawn;
    private float maxX, maxY, minY;

    //integers
    int randomSelector = 0;
	int specialEnemyArraySize;
	int currentRandomEnemy;
	int currentRandomSpecialEnemy;
	int pickupArraySize;
	int currentRandomPickup;
	int emitterArraySize = 0;
	int topEmitterArraySize;
	int randomEmitterNumber;
	int topBottomRandomEmitterNumber;
	int randomEmitterSpawn;
    int selectTier;
    int linearSelector;

    //Array that holds enemy gameobject prefab references
    public GameObject[] firstTierWaves;
    public GameObject[] secondTierWaves;
    public GameObject[] thirdTierWaves;
    public GameObject [] enemy;
	public GameObject [] specialEnemy;
	public GameObject [] pickup;
    public GameObject [] linearEnemies;
    public GameObject[] turrets;

    public float timeBetweenTurrets = 4;

	public GameObject boss1;
	public GameObject currentBoss;

	//Holds the location of all emitters in an array for easy referencing
	public GameObject [] emitters;
	public GameObject [] topEmitters;
	public GameObject [] bottomEmitters;
	GameObject currentEmitter = null;
	GameObject secondCurrentEmitter;
	GameObject specialCurrentEmitter;

    Vector2 center = new Vector2(0, 0);

    //vectors for screen boundaries.
    Vector2 topCorner;
    Vector2 bottomCorner;


    // Use this for initialization
    void Start () 
	{
		GetArrayDetails ();
		currentEmitter = emitters [1]; //needs to be assigned on start
		secondCurrentEmitter = emitters [2]; //needs to be assigned on start
		StartCoroutine ("Initialization");
        topCorner = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        bottomCorner = Camera.main.ScreenToViewportPoint(new Vector2(0, 0));
        setBounds();
        Vector2 startPosition = new Vector2(maxX + 1, transform.position.y);
        transform.position = startPosition;
    }
	
	// Update is called once per frame
	void Update () 
	{
		RandomGenerator ();
		boss ();
	}

    void setBounds()
    {
        //takes the minimum and maximum x and y values from the top and bottom corner vectors.
        maxX = topCorner.x;
        maxY = topCorner.y;
        minY = bottomCorner.y;
    }

    //gets the details of array length at startup and then saves them to variables for use by the random generators
    void GetArrayDetails () 
	{
		emitterArraySize = emitters.Length ;
		topEmitterArraySize = topEmitters.Length;
		pickupArraySize = pickup.Length;
		specialEnemyArraySize = specialEnemy.Length;
	}

	//generates random numbers to call items from the arrays
	void RandomGenerator () 
	{//if statements to check the gamescore and spawn harder enemies
		if (GameController.gameScore < firstTierEnd)
		{
            selectTier = 1;
		    randomSelector = Random.Range (0, firstTierWaves.Length); //selects for waves!!!
		}
		else if (GameController.gameScore >=firstTierEnd && GameController.gameScore < secondTierEnd)
		{
            selectTier = 2;
			randomSelector = Random.Range (0, secondTierWaves.Length); //selects for waves!!!
		}	
		else if (GameController.gameScore >= secondTierEnd)
		{
            selectTier = 3;
			randomSelector = Random.Range (0, thirdTierWaves.Length); //selects for waves!!!
		}

        //generates randoms	
        linearSelector = Random.Range(0, linearEnemies.Length);
		topBottomRandomEmitterNumber = Random.Range (0, topEmitterArraySize);
		currentRandomPickup =  Random.Range (0, pickupArraySize);
		currentRandomSpecialEnemy = Random.Range (0, specialEnemyArraySize);
		randomEmitterSpawn = Random.Range (0, 2);

		//controls where the special enemies spawn
		if (randomEmitterSpawn == 0) 
		{
			specialCurrentEmitter = topEmitters [topBottomRandomEmitterNumber];
		}
		if (randomEmitterSpawn == 1) 
		{
			specialCurrentEmitter = bottomEmitters [topBottomRandomEmitterNumber];
		}
	}

	IEnumerator Initialization () 
	{
		yield return new WaitForSeconds (secondsBeforeBegin);
		StartCoroutine ("SpawnPickup");
        StartCoroutine("SpawnLinear");
        yield return new WaitForSeconds(20); //added for balancing
        StartCoroutine("SpawnEnemy");
        yield return new WaitForSeconds(10); //added for balancing
        StartCoroutine ("SpawnSpecialEnemy");
        yield return new WaitForSeconds(10); //added for balancing
        StartCoroutine("SpawnTurret");
    }

    IEnumerator SpawnEnemy ()
    {
        while (true)
        {
            if (selectTier == 1)
            {
                Instantiate(firstTierWaves[randomSelector], center, Quaternion.identity);
            }
            else if (selectTier == 2)
            {
                Instantiate(secondTierWaves[randomSelector], center, Quaternion.identity);
            }
            else if (selectTier == 3)
            {
                Instantiate(thirdTierWaves[randomSelector], center, Quaternion.identity);
            }
            yield return new WaitForSeconds(secondsBetweenEnemies);
        }
    }

	//Coroutine that handles spawning enemies
	IEnumerator SpawnSpecialEnemy () 
	{
		while (true) 
		{
			yield return new WaitForSeconds (secondsBetweenSpecialEnemies);
			Instantiate (specialEnemy [currentRandomSpecialEnemy], specialCurrentEmitter.transform.position , Quaternion.identity);
			yield return new WaitForSeconds(secondsBetweenSpecialEnemies); //for testing multiple enemies
		}
	}
	 
	//Coroutine that handles pickup spawning
	IEnumerator SpawnPickup () 
	{
		while (true) 
		{
			Instantiate (pickup [currentRandomPickup], secondCurrentEmitter.transform.position , Quaternion.identity);
			yield return new WaitForSeconds(secondsBetweenPickups);
		}
	}

    IEnumerator SpawnLinear()
    {
        while (true)
        {
            randomY = Random.Range(0, maxY * 2);
            randomYSpawn = randomY - maxY;
            Vector2 linearPosition = new Vector2(maxX + 1, randomYSpawn);
            Instantiate(linearEnemies[linearSelector], linearPosition, Quaternion.identity);
            yield return new WaitForSeconds(secondsBetweenLinearEnemies);
        }
    }

    IEnumerator SpawnTurret()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenTurrets);
            int controlRandom = Random.Range(0, 2);
            if (controlRandom == 0)
            {
                Instantiate(turrets[0], emitters[0].transform.position, Quaternion.identity);
            }
            if (controlRandom == 1)
            {
                Instantiate(turrets[1], emitters[8].transform.position, Quaternion.identity);
            }
        }
    }

    void boss ()
	{
		if (GameController.gameScore >= (bossCounter * 15000) && canSpawnBoss == true) 
		{
			StopCoroutine ("SpawnEnemy");
			StopCoroutine ("SpawnSpecialEnemy");
            StopCoroutine("SpawnLinear");
            StopCoroutine("SpawnTurret");
            currentBoss = Instantiate (boss1, emitters [4].transform.position, Quaternion.identity) as GameObject;
			canSpawnBoss = false;
			bossSpawned = true;
			bossCounter++;
		}
		if (currentBoss == null && bossSpawned == true) 
		{
			StartCoroutine ("SpawnEnemy");
			StartCoroutine ("SpawnSpecialEnemy");
            StartCoroutine("SpawnLinear");
            StartCoroutine("SpawnTurret");
            bossSpawned = false;
			canSpawnBoss = true;
		}
	}
}
