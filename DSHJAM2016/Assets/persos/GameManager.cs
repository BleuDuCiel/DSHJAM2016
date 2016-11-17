﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] characterPerfabs ;

    public static GameManager instance = null;

    private Stack players;

    private bool gameStarted = false;

    void Awake()
    {
        //singleton
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted && SceneManager.GetActiveScene().name == "zbra")
        {
            setupCharacters();
        }


    }

    void characterSelect(Stack p)
    {
        players = p;

        SceneManager.LoadScene("zbra");
    }
    void setupCharacters()
    {
        int i = 0;
        foreach (Player pl in players)
        {
            Vector3 pos = Vector3.left * 5 * i;
            //TODO character selection
            Transform p = Instantiate(characterPerfabs[0]);
            p.position = pos;
            p.SendMessage("SetupInputs", pl.getInputs());
            i++;
        }

       
        gameStarted = true;
    }
}