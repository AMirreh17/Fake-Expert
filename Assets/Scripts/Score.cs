using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	public static int ScoreAmount = 0;// variable created for score.
	Text score;
    // Start is called before the first frame update
    void Start()
    {
		score = GetComponent<Text>();// get text component from score text variable
    }

    // Update is called once per frame
    void Update()
    {
		score.text = "Score: " + ScoreAmount;// score text will display ScoreAmount 

		
    }
}
