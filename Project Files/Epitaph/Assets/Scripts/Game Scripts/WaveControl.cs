using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveControl : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<GameObject> mirroredEnemies;

    private int enemiesLength = 0;
    private int mirroredEnemiesLength = 0;
    public int waveSize;
    public int speed;
    public int delayUntilMove;
    public int deathTally = 0;
    public float delayBetweenAttacks;

    GameObject reference;
    public GameObject spawner;
    public GameObject spawnerMirrored;

    // Use this for initialization
    void Start ()
    {
        reference = gameObject;
        Initiate();
        InitiateMirrored();
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckForDestroy();
	}

    public void Initiate()
    {
        int denominator = 0;
        foreach (GameObject enemy in enemies)
        {
            GameObject instanceOfEnemy = Instantiate(enemy, spawner.transform.position, Quaternion.identity, reference.transform);
            if (instanceOfEnemy.GetComponentInChildren<PathFollow>() != null)
            {
                Debug.Log("pathfollow speed should have changed.");
                var pathFollow = instanceOfEnemy.GetComponentInChildren <PathFollow> ();
                pathFollow.speed = speed;
                pathFollow.waitTime = delayUntilMove * denominator;
            }
            if (instanceOfEnemy.GetComponentInChildren<ControlledStandardEnemyController>() != null)
            {
                var firstTierController = instanceOfEnemy.GetComponentInChildren<ControlledStandardEnemyController>();
                firstTierController.delayBetweenProjectiles = delayBetweenAttacks;
            }
            if (instanceOfEnemy.GetComponentInChildren<SecondTierEnemyController>() != null)
            {
                var secondTierController = instanceOfEnemy.GetComponentInChildren<SecondTierEnemyController>();
                secondTierController.delayBetweenProjectiles = delayBetweenAttacks;
            }
            if (instanceOfEnemy.GetComponentInChildren<ThirdTierEnemyController>() != null)
            {
                var thirdTierController = instanceOfEnemy.GetComponentInChildren<ThirdTierEnemyController>();
                thirdTierController.delayBetweenProjectiles = delayBetweenAttacks;
            }
            denominator++;
            enemiesLength = denominator;
        }
    }

    public void InitiateMirrored()
    {
        int denominator = 0;
        foreach (GameObject enemy in mirroredEnemies)
        {
            GameObject instanceOfEnemy = Instantiate(enemy, spawnerMirrored.transform.position, Quaternion.identity, reference.transform);
            if (instanceOfEnemy.GetComponentInChildren<PathFollow>() != null)
            {
                Debug.Log("pathfollow speed should have changed.");
                var pathFollow = instanceOfEnemy.GetComponentInChildren<PathFollow>();
                pathFollow.speed = speed;
                pathFollow.waitTime = delayUntilMove * denominator;
                pathFollow.moveToMirrored = true;
            }
            if (instanceOfEnemy.GetComponentInChildren<ControlledStandardEnemyController>() != null)
            {
                var firstTierController = instanceOfEnemy.GetComponentInChildren<ControlledStandardEnemyController>();
                firstTierController.delayBetweenProjectiles = delayBetweenAttacks;
            }
            if (instanceOfEnemy.GetComponentInChildren<SecondTierEnemyController>() != null)
            {
                var secondTierController = instanceOfEnemy.GetComponentInChildren<SecondTierEnemyController>();
                secondTierController.delayBetweenProjectiles = delayBetweenAttacks;
            }
            if (instanceOfEnemy.GetComponentInChildren<ThirdTierEnemyController>() != null)
            {
                var thirdTierController = instanceOfEnemy.GetComponentInChildren<ThirdTierEnemyController>();
                thirdTierController.delayBetweenProjectiles = delayBetweenAttacks;
            }
            denominator++;
            mirroredEnemiesLength = denominator;
        }
    }

    void CheckForDestroy()
    {
        if (deathTally == enemiesLength + mirroredEnemiesLength)
        {
            StartCoroutine("destroyThis");
        }
    }

    IEnumerator destroyThis()
    {
        yield return new WaitForSeconds(0.75f);
        Destroy(gameObject);
    }
}
