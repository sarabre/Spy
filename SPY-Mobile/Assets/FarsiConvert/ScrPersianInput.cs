using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrPersianInput : MonoBehaviour {
	public string[] strList;
	WordSpliter wordSpliter = new WordSpliter() ;
	string s;
	public string text;
	public Text lableInput;
	public Text inputssss;
	void Start()
	{
		lableInput=transform.GetComponent<Text>();
		InvokeRepeating("SetPersianText",1,0.5f);
	}

	public void SetPersianText()
	{
		lableInput.text=inputssss.text;
		s = wordSpliter.SplitTheWord (lableInput.text, 4) ;
		lableInput.text = s;
		lableInput.text.faConvert();

	}
}
