using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextConvert : MonoBehaviour {
	public string[] strList;
	WordSpliter wordSpliter = new WordSpliter() ;
	string s;
	public string text;
	public Text lableInput;


	void Start()
	{
		lableInput=transform.GetComponent<Text>();
		s = wordSpliter.SplitTheWord (lableInput.text, 4) ;
		lableInput.text = s;
		//		if(convert)
		//		SetPersianText ();
		SetPersianText();
	}

	public void SetPersianText()
	{
		lableInput.text.faConvert();
		//InputText(lableInput.text.faConvert());

	}

}
