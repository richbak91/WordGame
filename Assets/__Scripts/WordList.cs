using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WordList : MonoBehaviour {
	public static WordList S;
	public TextAsset wordListText;
	public int numToParseBeforeYield = 10000;
	public int wordLengthMin = 3;
	public int wordLengthMax = 7;
	public bool ________________;
	public int currLine = 0;
	public int totalLines;
	public int longWordCount;
	public int wordCount;

	private string[] lines; // 1
	private List<string> longWords;
	private List<string> words;
	void Awake() {
		S = this;
	}

	public void Init() {
		// Split the text of wordListText on line feeds, which creates a large,
		// populated string[] with all the words from the list
		lines = wordListText.text.Split('\n');
		totalLines = lines.Length;
		// This starts the coroutine ParseLines(). Coroutines can be paused in
		// the middle to allow other code to execute.
		StartCoroutine( ParseLines() );
	}

	// All coroutines have IEnumerator as their return type.
	public IEnumerator ParseLines() {
		string word;
		// Init the Lists to hold the longest words and all valid words
		longWords = new List<string>();
		words = new List<string>();
		for (currLine = 0; currLine < totalLines; currLine++) {
			word = lines[currLine];

			if (word.Length == wordLengthMax) {
				longWords.Add(word);
			}
	
			if ( word.Length>=wordLengthMin && word.Length<=wordLengthMax ) {
				words.Add(word);
			}
			// Determine whether the coroutine should yield
			// This uses a modulus (%) function to yield every 10,000th record
			// (or whatever you have numToParseBeforeYield set to)
			if (currLine % numToParseBeforeYield == 0) {
				// Count the words in each list to show that the parsing is
				// progressing
				longWordCount = longWords.Count;
				wordCount = words.Count;
				// This yields execution until the next frame
				yield return null;
				// The yield will cause the execution of this method to wait
				// here while other code executes and then continue from this
				// point.
			}
		}
		gameObject.SendMessage("WordListParseComplete");
	}

	public List<string> GetWords() {
		return(words);
	}
	public string GetWord(int ndx) {
		return( words[ndx] );
	}
	public List<string> GetLongWords() {
		return( longWords );
	}
	public string GetLongWord(int ndx) {
		return( longWords[ndx] );
	}
}