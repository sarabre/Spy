using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;


public class WordSpliter : MonoBehaviour 
{
	
	
	
	public string SplitTheWord(string textToSplit, int wordNum = 5)
		
	{
		
		string fixedText = "";
		string tmpWord = "";
		
		string[] splitedWords;
		List<string> temp = new List<string> ();
		List<string> line = new List<string> (); 
		int x = 0;
		
		
		splitedWords = Regex.Split (textToSplit.faConvert (), @"[\s]");
		
		for (int i = 0; i < splitedWords.Length; i++) {
			
			temp.Add (splitedWords [i]);
		}
		
		while (temp.Count > 0) {
			
			if (x < wordNum) {
				
				tmpWord += temp [0] + " ";
				temp.Remove (temp [0]);
				
				x++;
				if (temp.Count == 0) {
					line.Add (tmpWord);
				}
			} else {
				
				line.Add (tmpWord);
				x = 0;
				tmpWord = "";
				
			}
			
		}
		
		for (int i = 0; i < line.Count; i++) {
			
			if (i != line.Count - 1) {
				
				fixedText += line [(line.Count - 1) - i] + "\n";
				
			} else {
				
				fixedText += line [(line.Count - 1) - i];
			}
		}
		
		return fixedText;
		
	}
	
	//								print ("####ADDDD##### " + temp.Count + "  " + x);
	
}
