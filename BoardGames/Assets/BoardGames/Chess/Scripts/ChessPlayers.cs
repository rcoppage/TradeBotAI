using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPlayers : MonoBehaviour
{
    //Refrences 
    public GameObject controller; //Controls pieces
    public GameObject movePlate; //Visual circles to show where you can move and tap to move

    //Positions
    private int xBoard = -1; //Default value offboard
    private int yBoard = -1; //Defaultvalue offboard

    // Variable to keep track of "black" or "white" player (brown and dark bropwn currentl) will add lots of color variatons later
    private string player; 

    //Refrences for all sprites of chess pieces
}
