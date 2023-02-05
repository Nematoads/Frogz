using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("IncreaseScoreEnum");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator IncreaseScoreEnum()
    {
        while (true)
        {
           yield return new WaitForSeconds(1);
           this.IncreaseScore();
        }
    }

    private void IncreaseScore() {
        this.score++;
        this.scoreText.text = $"Score: {this.score}";
    }
}
