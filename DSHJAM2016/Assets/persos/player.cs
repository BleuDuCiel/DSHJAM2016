using UnityEngine;
using System.Collections;

public class Player
{


    public int number { get; set; }
    public string prefixsuffix { get; set; }



    public Player(int n, string ps)
    {
        number = n;
        prefixsuffix = ps;
    }

    public string getInputs()
    {
        return prefixsuffix;
    }


}
