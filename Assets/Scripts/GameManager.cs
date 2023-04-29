using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using DG.Tweening;// This is a unity asset, which allows users to use animations through code

public class GameManager : MonoBehaviour
{
	public Question[] questions;// A variable array is declared called Question and this is used for storing questions
	private static List<Question> unAnswered; //A variable list is declared called unanswered and this is used for storing unanswered questions

	private Question currentQuestion; // A local variable is declared called currentQuestion 
	//GameObject variables are declared for changing colours of the buttons when the user answers
	public GameObject AnswerButton;
	public GameObject AnswerButton2;
	//Gameobject variables are declared for transitioning into different screens e.g. the score/feedback screen and game screen
	public GameObject GameOverScreen;
	public GameObject TimesUpScreen;
	public GameObject TryAgainScreen;
	public GameObject GameScreen;
	public GameObject SecretTipScreen;
	public GameObject SecretTipScreen2;
    //Text variables are declared for the different score screens
	public Text FScore;
	public Text FScore2;
	public Text FScore3;
	// Audio sources are declared for the sound effects and background music
	public AudioSource rightSrc;
	public AudioSource wrongSrc;
	public AudioSource endSrc;
	public AudioSource timesUpSrc;
	public AudioSource BackSrc;
	public AudioSource TASrc;
	//These variables declared are used for coins/XP and streaks
	public Text ScoreXP;
	public static int StreakScore = 0;
	public Text StreakText;
	// The string variables below are used for obtaining scene names
	public string sceneName;
	public string sceneName2;
	//These variables declared are used for coins/XP and streaks
	public static int TotalXP = 0;
	public Text XPAmount;
	public GameObject StreaksImage;

	public Image TestFade;//Image variable for adding fade animation 
	public GameObject BackFade;//gameobject variable for adding fade animation 
	public GameObject BackFade2;//gameobject variable for adding fade animation 
	// The variables below are used for stopwatch timer. Below demonstrates the setup
	public float timeStart;
	public Text timeText;
	public Text timeText2;
	public Text timeText3;
	public Text timeText4;
	bool timerActive  = true;
	//public static int ScreenWidth = Screen.width;
	//public static int ScreenHeight = Screen.height;

	private int totalQuestions = 0;//int variable to store the total amount of questions

	[SerializeField]
	private Text QuestionText;//question text for the game and the serializable field allows editing in the inspector

	[SerializeField]
	private float timeDelay = 1f;// time delay variable
	//private float timeDelay2 = 19f;

	void Start()
	{
		/*if (ScreenWidth == Screen.width || ScreenHeight == Screen.height)
		{
			ScreenHeight = Screen.width - (640 * 2);
			ScreenWidth = Screen.height - (360 * 2);
			Screen.SetResolution(ScreenWidth, ScreenHeight, true);
		}*/
		Screen.SetResolution(1080, 1920, true);// This is for setting the scene resolution
		//Screen.fullScreen = false;
		GameOverScreen.SetActive(false);
		TryAgainScreen.SetActive(false);
		// These text vatiables are used for stopwatch timer. The current start times are declared in the start function
		timeText.text = timeStart.ToString("F2");
		timeText2.text = timeStart.ToString("F2");
		timeText3.text = timeStart.ToString("F2");
		timeText4.text = timeStart.ToString("F2");
		BackSrc.Play();
		if (unAnswered == null || unAnswered.Count == 0)
		{
			unAnswered = questions.ToList<Question>();// the unanswered variables is equal to the data stored within the questions list
		}
		SetQuestion();
		
		//EndScreen();
		//FinalScore();
		
		
		totalQuestions = unAnswered.Count;
		//Debug.Log(currentQuestion.Enquiry + " is " + currentQuestion.isCorrect);
		Debug.Log(unAnswered.Count);
	}

	void Update()
	{
		if(timerActive){// This is used for updating the stopwatch timer
			timeStart += Time.deltaTime;
			timeText.text = timeStart.ToString("F2");
			timeText2.text = timeStart.ToString("F2");
			timeText3.text = timeStart.ToString("F2");
			timeText4.text = timeStart.ToString("F2");
		}
	
	}
	void SetQuestion()// This function checks whether a questions has been answered and sets a new question for the user
	{
		if(unAnswered.Count > 0)
		{
		int RandomQuestionIndex = Random.Range(0, unAnswered.Count);
			currentQuestion = unAnswered[RandomQuestionIndex];// currentQuestion is equal to the values within the unansweref array

			QuestionText.text = currentQuestion.Enquiry;// the question text is equal to currentquestion avaiable
			AnswerButton.GetComponent<Image>().color = Color.green;
			AnswerButton2.GetComponent<Image>().color = Color.red;
		}
		else
		{
			Debug.Log("Game Over");// if all questions are finished the score screen will appear as well as sending a note to the console
			EndScreen();
			TryAgain();
		}
	
		//EndScreen();
		//unAnswered.RemoveAt(RandomQuestionIndex);
	}

	IEnumerator NextQuestion()// This functions adds a time delay using IEnumerators and calls the SetQuestion function
	{
			unAnswered.Remove(currentQuestion);// removes previously displayed question
			yield return new WaitForSeconds(timeDelay);// delay time for 1 second
		SetQuestion();
;		//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	
	}
	public void UserCorrect()// This function checks whether the answer to the questions is true/real
	{
		if (currentQuestion.isCorrect)
		{
			Debug.Log("Your Answer is Correct");// This logs correct answer to the console
			AnswerButton.GetComponent<Image>().color = Color.green;//colour of the correct button wil go green
			AnswerButton2.GetComponent<Image>().color = Color.grey;//colour of the wrong button wil go grey
			Score.ScoreAmount += 1;// 1 point added to the score
			TotalXP += 10;// 10 point added to the coins/xp score
			rightSrc.Play();//sound effect plays
		}
		else
		{
			Debug.Log("Your Answer is Incorrect");// This logs incorrect answer to the console
			AnswerButton2.GetComponent<Image>().color = Color.grey;//colour of the correct button wil go grey
			AnswerButton.GetComponent<Image>().color = Color.red;//colour of the wrong  button wil go red
			wrongSrc.Play();//sound effect plays
		}
		StartCoroutine(NextQuestion());//generate new question
		

	}

	public void UserIncorrect()// This functions checks whether the answer to the questions is false/fake
	{
		if (!currentQuestion.isCorrect)
		{
			Debug.Log("Your Answer is Correct");
			AnswerButton2.GetComponent<Image>().color = Color.green;//colour of the correct button wil go green
			AnswerButton.GetComponent<Image>().color = Color.grey;//colour of the wrong button wil go grey
			Score.ScoreAmount += 1;// 1 point added to the score
			TotalXP += 10;// 10 point added to the coins/xp score
			rightSrc.Play();//sound effect plays
		}
		else
		{
			Debug.Log("Your Answer is Incorrect");
			AnswerButton2.GetComponent<Image>().color = Color.red;//colour of the incorrect button wil go red
			AnswerButton.GetComponent<Image>().color = Color.grey;//colour of the correct button wil go grey
			wrongSrc.Play();//sound effect plays
		}
		StartCoroutine(NextQuestion());// generate new question
		
	}
	public void GameOver()// This function is used for the score screen
	{
		GameOverScreen.SetActive(true);//The gameoverscreen gameobject appears 
		TestFade.DOFade(1, 2f);//fade animation added to gameobject
		//GameOverScreen.DOFade(1, .5f);
	}

	public void EndScreen()// This function is used for the score screen
	{
		//if ( Score.ScoreAmount >= 5)
		//{
		//}
			GameOver();
			GameScreen.SetActive(false);
			FinalScore();
			endSrc.Play();
			BackSrc.Stop();
			FinalXP();
			OverallXP();
			//StreaksAmount();
			timerEnd2();

	}

	public void TimesUp()// This function is used for the score screen if time runs out
	{
		//if ( Score.ScoreAmount >= 5)
		//{
		//}
		TimesUpScreen.SetActive(true);
		GameScreen.SetActive(false);
		FinalScore2();
		timesUpSrc.Play();
		BackSrc.Stop();
		timerEnd();

	}

	public void TryAgain()// This function is used for the score screen if the user does not get enough questions correct
	{
	   if ( Score.ScoreAmount < 3)
		{
		timerEnd2();
		TryAgainScreen.SetActive(true);
		GameScreen.SetActive(false);
			GameOverScreen.SetActive(false);
			FinalScore3();
		BackSrc.Stop();
			TASrc.Play();
			
		}
		

	}
	//These functions are used to display the final score
	public void FinalScore()
	{
		FScore.text = Score.ScoreAmount + "/" + totalQuestions;
	}
	// This function displays final score on times up screen
	public void FinalScore2()
	{
		FScore2.text = Score.ScoreAmount + "/" + totalQuestions;
	}
	// This function displays final score on times up screen
	public void FinalScore3()
	{
		FScore3.text = Score.ScoreAmount + "/" + totalQuestions;
	}
	// This function displays coins/xp
	public void FinalXP()
	{
		ScoreXP.text = " " + Score.ScoreAmount * 10;
	}
	// This function displays total coins earned
	public void OverallXP()
	{
		XPAmount.text = " " + TotalXP;
	}


	public void Restart()// Restart the game when user presses retry
	{
		//NextQuestion();
		//GameOverScreen.SetActive(false);
		//GameScreen.SetActive(true);
		Score.ScoreAmount = 0;/// reset score to 0
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//load active scene
	}

	public void NextScene()// This function is used for changing scenes
	{
		SceneManager.LoadScene(sceneName);// load scene with specific scene name
	}
	public void NextScene2()//This function is used for changing scenes
	{
		SceneManager.LoadScene(sceneName2);// load scene with specific scene name
		Score.ScoreAmount = 0;
		TotalXP = 0;
		StreakScore = 0;
	}
	public void ExitGame()// This function is used to exit the game application
	{
		Application.Quit();// quit unity application
	}
	public void StreaksAmount(){// This function is used to calculate streaks, how many questions a user gets correct in a row
		if (Score.ScoreAmount == 5){
			StreakScore += 5;
			StreakText.text = " " + StreakScore;
			StreaksImage.SetActive(true);
		}else{
			StreakScore = 0;
		}
		timerActive = false;
	}
	public void SecretTips(){// This function is used to display schievements screen
		GameOverScreen.SetActive(false);
		SecretTipScreen.SetActive(true);
		endSrc.Play();
		//BackFade.DOFade(1, 2f)
		
	}
	public void SecretTips2(){// This function is used to display the second schievements screen
		
		SecretTipScreen.SetActive(false);
		SecretTipScreen2.SetActive(true);
		endSrc.Play();
		//BackFade2.DOFade(1, 2f)
		
	}
	public void timerEnd(){// This function is used to stop the timer for the stopwatch
		
		timerActive = !timerActive;
	}
	public void timerEnd2(){// This function is used to stop the timer for the stopwatch
		
		timerActive = false;
	}

}
