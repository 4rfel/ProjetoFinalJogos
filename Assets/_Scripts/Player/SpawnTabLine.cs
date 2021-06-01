using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnTabLine : MonoBehaviour {
	[SerializeField] GameObject line;
	List<GameObject> lines;

	int dyLine = 20;
	int currentLine = 0;

	public void SpawnLine(int playerHits, string playerName) {
		GameObject lineInstance = Instantiate(line, transform);
		lineInstance.transform.position = lineInstance.transform.position - Vector3.up * dyLine * currentLine;
		lineInstance.GetComponentInChildren<Text>().text = playerName + " has " + playerHits + " hits";
		currentLine++;
		lines.Add(lineInstance);
	}

	public void Clear() {
		if(lines == null)
			lines = new List<GameObject>();

		foreach (GameObject l in lines) {
			Destroy(l);
		}
		currentLine = 0;
	}
}
