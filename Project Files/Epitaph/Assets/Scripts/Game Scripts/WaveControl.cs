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
            Instantiate(enemy, spawner.transform.position, Quaternion.identity, reference.transform);
            if (enemy.GetComponentInChildren<PathFollow>() != null)
            {
                Debug.Log("pathfollow speed should have changed.");
                var pathFollow = enemy.GetComponentInChildren <PathFollow> ();
                pathFollow.speed = speed;
                pathFollow.waitTime = delayUntilMove * denominator;
            }
            if (enemy.GetComponentInChildren<ControlledStandardEnemyController>() != null)
            {
                var firstTierController = enemy.GetComponentInChildren<ControlledStandardEnemyController>();
                firstTierController.delayBetweenProjectiles = delayBetweenAttacks;
            }
            if (enemy.GetComponentInChildren<SecondTierEnemyController>() != null)
            {
                var secondTierController = enemy.GetComponentInChildren<SecondTierEnemyController>();
                secondTierController.delayBetweenProjectiles = delayBetweenAttacks;
            }
            if (enemy.GetComponentInChildren<ThirdTierEnemyController>() != null)
            {
                var thirdTierController = enemy.GetComponentInChildren<ThirdTierEnemyController>();
                thirdTierController.delayBetweenProjectiles = delayBetweenAttacks;
            }
            denominator++;
            enemiesLength = denominator - 1;
        }
    }

    public void InitiateMirrored()
    {
        int denominator = 0;
        foreach (GameObject enemy in mirroredEnemies)
        {
            Instantiate(enemy, spawnerMirrored.transform.position, Quaternion.identity, reference.transform);
            if (enemy.GetComponentInChildren<PathFollow>() != null)
            {
                Debug.Log("pathfollow speed should have changed.");
                var pathFollow = enemy.GetComponentInChildren<PathFollow>();
                pathFollow.speed = speed;
                pathFollow.waitTime = delayUntilMove * denominator;
                pathFollow.moveToMirrored = true;
            }
            if (enemy.GetComponentInChildren<ControlledStandardEnemyController>() != null)
            {
                var firstTierController = enemy.GetComponentInChildren<ControlledStandardEnemyController>();
                firstTierController.delayBetweenProjectiles = delayBetweenAttacks;
            }
            if (enemy.GetComponentInChildren<SecondTierEnemyController>() != null)
            {
                var secondTierController = enemy.GetComponentInChildren<SecondTierEnemyController>();
                secondTierController.delayBetweenProjectiles = delayBetweenAttacks;
            }
            if (enemy.GetComponentInChildren<ThirdTierEnemyController>() != null)
            {
                var thirdTierController = enemy.GetComponentInChildren<ThirdTierEnemyController>();
                thirdTierController.delayBetweenProjectiles = delayBetweenAttacks;
            }
            denominator++;
            mirroredEnemiesLength = denominator - 1;
        }
    }

    void CheckForDestroy()
    {
        int enemiesNull = 0;
        foreach (GameObject enemy in mirroredEnemies)
        {
            if (enemy == null)
            {
                enemiesNull++;
            }
        }
        foreach (GameObject enemy in enemies)
        {
            if (enemy == null)
            {
                enemiesNull++;
            }
        }
        if (enemiesNull == enemiesLength + mirroredEnemiesLength)
        {
            Destroy(gameObject);
        }
    }
}
