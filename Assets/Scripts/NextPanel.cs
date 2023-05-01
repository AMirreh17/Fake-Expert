using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class NextPanel : MonoBehaviour
{
	//public static int ScreenWidth = Screen.width;
	//public static int ScreenHeight = Screen.height;
	// These variables below are used for home page of the application
	public GameObject StartScreen;
	public GameObject StartScreen2;
	public GameObject StartScreen3;
	public GameObject StartScreen4;
	public GameObject HomeScreen;
	public string sceneName;
	public Image image1;
	public Image image2;
	public Image image3;
	public Image image4;
	public Text Name;
	public Text Name2;
	public Text Name3;
	public Text levelName;

	// Start is called before the first frame update
	void Start()
    {
		/*if(ScreenWidth == Screen.width || ScreenHeight == Screen.height)
		{
			ScreenHeight = Screen.width - (640 * 2);
			ScreenWidth = Screen.height - (360 * 2);
			Screen.SetResolution(ScreenWidth, ScreenHeight, true);
		}*/
		Screen.SetResolution(1080, 1920, true);
		//Screen.fullScreen = false;
		HomeScreen.SetActive(true);
		StartScreen.SetActive(false);
		StartScreen2.SetActive(false);
		StartScreen3.SetActive(false);
		Name.DOFade(1, 5f);
		Name2.DOFade(1, 5f);
		Name3.DOFade(1, 5f);

	}
	// These functions are used to switch between different panels in thehome scene and also change between other scenes within the project
	public void NeztPanelScreen()
	{
		HomeScreen.SetActive(false);
		StartScreen.SetActive(true);
		image1.DOFade(1, 5f);// fade animation to image variable
	}
	public void NeztPanelScreen2()
	{
		
		StartScreen.SetActive(false);
		StartScreen2.SetActive(true);
		image2.DOFade(1, 5f);// fade animation to image variable

	}
	public void NeztPanelScreen3()
	{

		
		StartScreen4.SetActive(false);
		StartScreen3.SetActive(true);
		//image3.DOFade(1, 5f);
		levelName.DOFade(1, 5f);// fade animation to image variable

	}
	public void NeztPanelScreen4()
	{

		
		StartScreen2.SetActive(false);
		StartScreen4.SetActive(true);
		//image3.DOFade(1, 5f);
		image4.DOFade(1, 5f);// fade animation to image variable

	}

	public void NextScene()
	{
		SceneManager.LoadScene(sceneName);
	}
}
