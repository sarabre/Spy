using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class scrTexts : MonoBehaviour {


	public string[] strList;
	public GUISkin guiSkin;
	public bool convert;

	WordSpliter wordSpliter = new WordSpliter() ;
	string s;

	void Start()
	{


//		wordSpliter.SplitTheWord (lableInput.text, 15);
		s = wordSpliter.SplitTheWord (lableInput.text, 4) ;
		
		lableInput.text = s;
//		if(convert)
//		SetPersianText ();

	}
	public bool isSelect;
	public bool isDeselect;
	public Input input;
    public Text lableInput;

	void InputText(string strInput)
	{
		lableInput.text = strInput;

	}


	void OnClick()
	{
		SetPersianText();
		print("call");
	}


	public float right;
	public float up;
	public float scaleX;
	public float scaleY;

	public GUISkin skin;


	public string text;


	void OnGUI(){





		GUI.Label (new Rect (Screen.width / 2 - right, Screen.height / 2 - up, scaleX, scaleY),lableInput.text);

		}




	public void SetPersianText()
	{
		InputText(lableInput.text.faConvert());

	}


}
