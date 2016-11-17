using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {



	// Use this for initialization
	void Start () {
        setupPlayerInputs();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void setupPlayerInputs()
    {
        GameObject[] players= GameObject.FindGameObjectsWithTag("Player");

        /*
        foreach (GameObject g in players)
        {
            g.SendMessage("setupInputs", )
        }
        */

        players[0].SendMessage("SetupInputs", "KB,0");
        players[1].SendMessage("SetupInputs", "KB,1");
    }
}
