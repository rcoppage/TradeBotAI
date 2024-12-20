using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject chesspiece;

    //Postions of each chess piece
    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    //Player and is game running

    private string currentPlayer = "white";

    private bool gameOver = false;




    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity); 
        //0 0 -1 is the starting position of piece Quaternion identity is related to 3d rotation can just put Quaternion.idenity for all 2d Instantiations

        //Creates white pieces
         playerWhite = new GameObject[] { Create("white_rook", 0, 0), Create("white_knight", 1, 0),
            Create("white_bishop", 2, 0), Create("white_queen", 3, 0), Create("white_king", 4, 0),
            Create("white_bishop", 5, 0), Create("white_knight", 6, 0), Create("white_rook", 7, 0),
            Create("white_pawn", 0, 1), Create("white_pawn", 1, 1), Create("white_pawn", 2, 1),
            Create("white_pawn", 3, 1), Create("white_pawn", 4, 1), Create("white_pawn", 5, 1),
            Create("white_pawn", 6, 1), Create("white_pawn", 7, 1) };

        //Creates black pieces
        playerBlack = new GameObject[] { Create("black_rook", 0, 7), Create("black_knight",1,7),
            Create("black_bishop",2,7), Create("black_queen",3,7), Create("black_king",4,7),
            Create("black_bishop",5,7), Create("black_knight",6,7), Create("black_rook",7,7),
            Create("black_pawn", 0, 6), Create("black_pawn", 1, 6), Create("black_pawn", 2, 6),
            Create("black_pawn", 3, 6), Create("black_pawn", 4, 6), Create("black_pawn", 5, 6),
            Create("black_pawn", 6, 6), Create("black_pawn", 7, 6) };

            //Sets each piece in position
            for(int i = 0; i <playerBlack.Length; i++) {
                SetPosition(playerBlack[i]);
                SetPosition(playerWhite[i]);
            }
    }
     public GameObject Create(string name, int x, int y) {
            GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
            ChessPlayers chessplayer = obj.GetComponent<ChessPlayers>();
            chessplayer.name = name;
            chessplayer.setXBoard(x);
            chessplayer.SetYBoard(y);
            chessplayer.Activate();
            return obj;
        }

        public void SetPosition(GameObject obj) {
            ChessPlayers chessplayer = obj.GetComponent<ChessPlayers>();
            positions[chessplayer.GetXBoard(), chessplayer.GetYBoard()] = obj;
        }
}
