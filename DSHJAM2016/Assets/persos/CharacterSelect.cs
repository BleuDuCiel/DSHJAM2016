using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections;

public class CharacterSelect : MonoBehaviour
{

    [SerializeField]
    private Transform[] m_Characters;

    struct pair
    {
        public string pre;
        public string suf;
        public pair(string p, string s)
        {
            pre = p;
            suf = s;
        }
    }

    [SerializeField]
    public Sprite[] numbers;

    private int nbPlayers = 1;
    private int nbMaxPlayers = 2;

    private Stack players;
    private pair[] input;

    private int cpt = 0;
    // Use this for initialization
    void Start()
    {
        players = new Stack();
        players.Push(new Player(0, generateInputs()));
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
            addPlayer();
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
            delPlayer();


        if (Input.GetKeyDown(KeyCode.W))
        {
            GameObject.FindGameObjectWithTag("GameManager").SendMessage("characterSelect", players);

        }




    }

    void addPlayer()
    {

        if (nbPlayers < nbMaxPlayers)
        {
            nbPlayers++;
            GetComponent<SpriteRenderer>().sprite = numbers[nbPlayers - 1];
            players.Push(new Player(0, generateInputs()));
        }
    }

    void delPlayer()
    {
        if (nbPlayers > 1)
        {
            nbPlayers--;
            GetComponent<SpriteRenderer>().sprite = numbers[nbPlayers - 1];
            players.Pop();
        }
    }

	// TODO: y u do dis?
    private string generateInputs()
    {
        if (Input.GetJoystickNames().Length > 0)
			return "JS" + "," + (players.ToArray().Length).ToString();
        return "KB" + "," + ((int)(Random.value * 2)).ToString();
    }
}
