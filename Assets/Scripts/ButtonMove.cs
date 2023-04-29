using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class ButtonMove : MonoBehaviour
{
	public Transform BounceButton;
	public AudioSource ButtonSFX;

	public 
    // Start is called before the first frame update
    void Start()
    {
		//BounceButton.DOLocalMove(new Vector3(0, -1, 0), 2).SetLoops(-1, LoopType.Yoyo);
    }

	public void ButtonSound()
	{
		ButtonSFX.Play();
	}
   
}
