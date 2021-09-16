using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallsManager : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> ballsRigidbodies = new List<Rigidbody>();
    void Start()
    {
        EventManagers.CueEvents.OnCueHitBall += OnBallsStartMove;
    }

    private void OnBallsStartMove(object sender, CueEventHandler handler)
    {
        time = 0;
        needUpdate = true;
    }

    // Update is called once per frame
    private float CustomUpdateInterval = 0.5f;
    private float time = 0;
    private bool needUpdate = false;
    void Update()
    {
        time += Time.deltaTime;
        if (time >= CustomUpdateInterval && needUpdate)
        {
            CustomUpdate();
            time = 0;
        }
    }

    void CustomUpdate()
    {
        int stopedCount = 0;
        List<Rigidbody> toDestroy = new List<Rigidbody>();
        
        //Проверяем движение всех обьектов
        foreach (var ball in ballsRigidbodies)
        {
            //TODO сделать нормулью проверку и очистку списка
            try
            {
                if (ball.velocity.magnitude <= 0.05f)
                {
                    ball.velocity = Vector3.zero;
                    stopedCount++;
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
                toDestroy.Add(ball);
            }
        }
        
        //Удаляем пустые ссылки
        if (toDestroy.Count > 0)
        {
            foreach (var rigidbody1 in toDestroy)
                ballsRigidbodies.Remove(rigidbody1);
        }

        Debug.Log("Custom update");
        if (ballsRigidbodies.Count == 0)
        {
            //TODO нормальный менеджер сцен
            SceneManager.LoadScene("Game");
            return;
        }
        
        if (stopedCount == ballsRigidbodies.Count)
        {
            needUpdate = false;
            EventManagers.BallsEvents.OnBallsStoped?.Invoke(this);
        }
    }
}
