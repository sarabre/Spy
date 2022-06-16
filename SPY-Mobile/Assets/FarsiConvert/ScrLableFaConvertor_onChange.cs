using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrLableFaConvertor_onChange : MonoBehaviour {
	
	public Text lable;
	public int wordsNumInRow;
	WordSpliter wordSpliter = new WordSpliter();
	
	void Start(){
		
		if (!lable)
            if (this.GetComponent<Text>())
                lable = this.GetComponent<Text>();
		
		ChekInputsOnChange ();
		
	}
	
	string str="";
	string strTemp="";
	
	public void ChekInputsOnChange(){
		
		strTemp = str;
		str = lable.text;
		
		if (str != strTemp) {
			
			str = wordSpliter.SplitTheWord ( str, wordsNumInRow);
			lable.text = str;
		}
		
	}
	
	void LateUpdate(){
		
		ChekInputsOnChange ();
		
		
	}
	
	
}
