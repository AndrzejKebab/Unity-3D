using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public float playerScore;
    [SerializeField] private Text scoreText;
	

	public GameObject enemyPrefab;

	//float spawnInterval = 3;
	//float timeSinceLastSpawn = 0;
   
	public void UpdateScore()
    {
        scoreText.text = playerScore.ToString("0");
    }

	//public void Update()
	//{
	//	timeSinceLastSpawn += Time.fixedDeltaTime;
	//	if (timeSinceLastSpawn > spawnInterval)
	//	{
	//		Instantiate(enemyPrefab);
	//		timeSinceLastSpawn = 0;
	//		Debug.Log("respawn");
	//	}
	//} w piüdzie z tym
}
