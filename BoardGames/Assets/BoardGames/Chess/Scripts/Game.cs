using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject chesspiece;


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity); 
        //0 0 -1 is the starting position of piece Quaternion identity is related to 3d rotation can just put Quaternion.idenity for all 2d Instantiations
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
