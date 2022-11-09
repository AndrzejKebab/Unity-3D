using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsControllet : MonoBehaviour
{
    [SerializeField] private int pointsAmount;
    [SerializeField] private ScoreController scoreController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Score();
        }
    }

    void Score()
    {
        scoreController.playerScore += pointsAmount;
        scoreController.UpdateScore();
    }
}
