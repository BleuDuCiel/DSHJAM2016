using UnityEngine;
using System.Collections;

public class Swap : MonoBehaviour {

	public Sprite left_hand;
	public Sprite right_hand;

	// Use this for initialization
	void Start () {
		Debug.Log ("IMA STARTFING");
		SwapRight (0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SwapRight(int skina, int skinb) {
		GameObject go = new GameObject();
		go.name = "HUD";
		go.AddComponent<Canvas> ();
		Canvas hud = go.GetComponents<Canvas> ()[0];
		hud.renderMode = RenderMode.ScreenSpaceOverlay;
		go.AddComponent<SpriteRenderer> ();
		SpriteRenderer spriter = go.GetComponent<SpriteRenderer> ();
		spriter.sprite = left_hand;
	}
}
