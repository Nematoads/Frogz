using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    bool hasGameEnded = false;
    float time;

    private void Start()
    {
        EventBroker.rootdied += GameEnded;
    }
    private void Update()
    {
        if (!hasGameEnded)
        {
            time += Time.deltaTime;
            scoreText.text = ((int)time).ToString();
        }
    }

    void GameEnded()
    {
        hasGameEnded = true;
    }

    private void OnDestroy()
    {
        EventBroker.rootdied -= GameEnded;
    }


}
