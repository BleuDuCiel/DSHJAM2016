using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Collections.Generic;
using Inputs;


public class CharacterSelect : MonoBehaviour
{

    [SerializeField]
    private Transform[] m_Characters;

	[SerializeField]
	public SpriteRenderer[] jx;

	public Texture2D graphic;
	private Sprite[] graphics;

    private int nbPlayers = 0;
    private static int nbMaxPlayers = 4;

	private List<Player> players;
	private List<player_setup> players_tmp;

	class player_setup : IEquatable<player_setup>	{
		public string input;
		public int id;
		public int skin;

		public player_setup(string input, int id) {
			this.input = input;
			this.id = id;
			this.skin = UnityEngine.Random.Range(0,6);
		}

		public override string ToString() {
			return "input: " + input + " id: " + id + " skin: " + skin;
		}

		public override bool Equals(object o) {
			player_setup other = o as player_setup;
			return (this.id == other.id) && (this.input.Equals (other.input));
		}

		public bool Equals(player_setup other) {
			return (this.id == other.id) && (this.input.Equals (other.input));
		}
			
		public static bool operator== (player_setup a, player_setup b) {
			return a.Equals(b);
		}

		public static bool operator!= (player_setup a, player_setup b) {
			return !a.Equals(b);
		}

	}

    // Use this for initialization
    void Start()
    {
		players_tmp = new List<player_setup>(nbMaxPlayers); 
		graphics = Resources.LoadAll<Sprite>(graphic.name);
    }

    // Update is called once per frame
    void Update()
    {
        // Listen for jumps on JS
        for (int i = 0; i < nbMaxPlayers; i++)
        {
            if (Input.GetButtonDown("JS" + "Jump" + i))
                addPlayer("JS", i);
            //if (Input.GetButtonDown("KB" + "Jump" + i))
            //    addPlayerKB(i);
        }

        // Listen for items on JS
        for (int i = 0; i < nbMaxPlayers; i++)
        {
            if (Input.GetButtonDown("JS" + "Item" + i))
                delPlayer("JS", i);
            //if (Input.GetButtonDown("KB" + "Item" + i))
            //    delPlayerKB(i);
        }
	
        if (Input.GetButtonDown("Submit")) createGame ();

		// Display players and skins
		int index = 0;
		foreach (player_setup p in players_tmp) {
			jx [index].sprite = graphics [p.skin+1];
			index++;
		}
		for (int i = index; i < 4; i++) {
			jx [i].sprite = graphics [0];
		}
    }

	void createGame() {
		Debug.Log("Start Game");
		players = new List<Player>(nbMaxPlayers);

		int index = 0;
		foreach (player_setup p in players_tmp) {
			players.Add(new Player(index, p.skin, new LeftHand(p.input, p.id.ToString()), new RightHand(p.input, p.id.ToString())));
			index++;
		}
		GameObject.FindGameObjectWithTag("GameManager").SendMessage("characterSelect", players);
	}

	void addPlayer(string pad, int id=0) {
		player_setup newplayer = new player_setup(pad, id);
		if (players_tmp.Contains (newplayer)) return;

		players_tmp.Add (newplayer);
		Debug.Log ("New player added " + newplayer);
	}

	void delPlayer(string pad, int id=0) {
		player_setup oldplayer = new player_setup(pad, id);
		if (!players_tmp.Contains (oldplayer)) return;

		players_tmp.Remove(oldplayer);
		Debug.Log ("New player deleted " + oldplayer);
	}
}