using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;

    float time;

    private void Update()
    {
        time += Time.deltaTime;
        scoreText.text = ((int)time).ToString();
    }


}
