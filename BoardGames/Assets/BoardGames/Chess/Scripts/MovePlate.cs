using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
   public GameObject controller;

   GameObject refrence = null;

   //Board Positions
   int matrixX;
   int matrixY;

   //false: movement, true: attacking
   public bool attack = false;

   public void Start() {
    if(attack) {
        //change to red
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
    }
   }

   public void OnMouseUp() {
    controller = GameObject.FindGameObjectWithTag("GameController");
    //finds piece at moveplate positions and takes it
    if(attack) {
        GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

        Destroy(cp);
    }

    //Moves piece and makes old spot empty
    controller.GetComponent<Game>().SetPositionEmpty(refrence.GetComponent<ChessPlayers>().GetXBoard(),
        refrence.GetComponent<ChessPlayers>().GetYBoard());

        refrence.GetComponent<ChessPlayers>().SetXBoard(matrixX);
        refrence.GetComponent<ChessPlayers>().SetYBoard(matrixY);
        refrence.GetComponent<ChessPlayers>().SetCoords();

        controller.GetComponent<Game>().SetPosition(refrence);

        refrence.GetComponent<ChessPlayers>().DestroyMovePlates();
   }

   public void SetCoords(int x, int y) {
    matrixX = x;
    matrixY = y;
   }

   public void SetRefrence(GameObject obj) {
    refrence = obj;
   }

   public GameObject GetRefrence() {
    return refrence; 
   }
}
