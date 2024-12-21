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
    public Sprite black_queen, black_king, black_knight, black_bishop, black_rook, black_pawn;
    public Sprite white_queen, white_king, white_knight, white_bishop, white_rook, white_pawn;

    public void Activate() {

        controller = GameObject.FindGameObjectWithTag("GameController");

        // Set the size of the piece
        this.transform.localScale = new Vector3(1.6f, 1.6f, 1.0f); // Adjust the scale as needed

        //take the instantiated location and adjust the transform
        SetCoords();

        //Sets chesspiece object to correct image based opn name
        switch (this.name) {
            case "black_queen": this.GetComponent<SpriteRenderer>().sprite = black_queen; break;
            case "black_king": this.GetComponent<SpriteRenderer>().sprite = black_king; break;
            case "black_knight": this.GetComponent<SpriteRenderer>().sprite = black_knight; break;
            case "black_bishop": this.GetComponent<SpriteRenderer>().sprite = black_bishop; break;
            case "black_rook": this.GetComponent<SpriteRenderer>().sprite = black_rook; break;
            case "black_pawn": this.GetComponent<SpriteRenderer>().sprite = black_pawn; break;
            
            case "white_queen": this.GetComponent<SpriteRenderer>().sprite = white_queen; break;
            case "white_king": this.GetComponent<SpriteRenderer>().sprite = white_king; break;
            case "white_knight": this.GetComponent<SpriteRenderer>().sprite = white_knight; break;
            case "white_bishop": this.GetComponent<SpriteRenderer>().sprite = white_bishop; break;
            case "white_rook": this.GetComponent<SpriteRenderer>().sprite = white_rook; break;
            case "white_pawn": this.GetComponent<SpriteRenderer>().sprite = white_pawn; break;
        }
    }

    public void SetCoords() {
        float x = xBoard;
        float y = yBoard;

        x *= 0.785f;// Adjust this factor to control spacing
        y *= 0.785f;

        // Offset the positions to center the pieces in the squares
        x += -2.78f; // Adjust this offset to shift hotizontally
        y += -2.65f; // Adjust this offset to shift vertically

        this.transform.position = new Vector3(x, y, -1.0f);
    }

    //Getters and setters
    public int GetXBoard() {
        return xBoard; 
    }

    public int GetYBoard() {
        return yBoard;
    }

    public void SetXBoard(int x) {
        xBoard = x;
    }

    public void SetYBoard(int y) {
        yBoard = y;
    }

    //Mouse Events
    private void OnMouseUp() {
        DestroyMovePlates();
        
        InitiateMovePlates();
    }

    public void DestroyMovePlates() {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for(int i = 0; i< movePlates.Length; i++){
            Destroy(movePlates[i]);
        }
    }
}
