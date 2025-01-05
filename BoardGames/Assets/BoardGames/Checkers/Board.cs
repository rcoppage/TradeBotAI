using UnityEngine;
using System.Collections.Generic;
public class Board : MonoBehaviour
{
    public GameObject player1PiecePrefab; // Assign in Inspector
    public GameObject player2PiecePrefab; // Assign in Inspector
    public GameObject player1KingPrefab;  // Assign in Inspector
    public GameObject player2KingPrefab;  // Assign in Inspector

    private int boardSize = 8;
    private GameObject[,] boardObjects; // To keep track of GameObjects on the board

    // Enum to represent pieces
    public enum Piece { Empty, Player1, Player2, Player1King, Player2King }

    private Piece[,] boardState = new Piece[8, 8];
    private readonly Vector3 boardOffset = new Vector3(-3.5f, -3.5f, 0); // Adjust values as needed
    private void Start()
    {
        InitializeBoardState();
        InitializeBoardGraphics();
    }

    private void InitializeBoardState()
    {
        // Initialize starting positions
        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                if (row < 3 && (row + col) % 2 == 1)
                {
                    boardState[row, col] = Piece.Player1;
                }
                else if (row > 4 && (row + col) % 2 == 1)
                {
                    boardState[row, col] = Piece.Player2;
                }
                else
                {
                    boardState[row, col] = Piece.Empty;
                }
            }
        }
        // Debug: Print board state
        for (int row = 0; row < boardSize; row++)
        {
            string rowContent = "";
            for (int col = 0; col < boardSize; col++)
            {
                rowContent += boardState[row, col] + " ";
            }
            Debug.Log(rowContent);
        }
    }

    private void InitializeBoardGraphics()
    {
        float squareSize = 1.0f; // Size of one board square
        Vector3 boardOffset = new Vector3(-3.5f, -3.5f, 0); // Center the board in the scene
        
        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                // Calculate the position for the piece
                Vector3 position = new Vector3(col * squareSize, row * squareSize, 0) + boardOffset;

                // Check the board state and instantiate the appropriate piece
                if (boardState[row, col] == Piece.Player1)
                {
                    Instantiate(player1PiecePrefab, position, Quaternion.identity, transform);
                }
                else if (boardState[row, col] == Piece.Player2)
                {
                    Instantiate(player2PiecePrefab, position, Quaternion.identity, transform);
                }
                else if (boardState[row, col] == Piece.Player1King)
                {
                    Instantiate(player1KingPrefab, position, Quaternion.identity, transform);
                }
                else if (boardState[row, col] == Piece.Player2King)
                {
                    Instantiate(player2KingPrefab, position, Quaternion.identity, transform);
                }
            }
        }
    }

    private void PromotePieceIfNeeded(int row, int col)
    {
        if (boardState[row, col] == Piece.Player1 && row == 7)
        {
            boardState[row, col] = Piece.Player1King;
            ReplaceWithKing(row, col, player1KingPrefab);
        }
    }

    private void ReplaceWithKing(int row, int col, GameObject kingPrefab)
    {
        Vector3 position = new Vector3(col, row, 0) + boardOffset; // Use boardOffset here
        Destroy(boardObjects[row, col]); // Destroy the current piece
        boardObjects[row, col] = Instantiate(kingPrefab, position, Quaternion.identity);
    }

    // Method to replace the piece GameObject with the King prefab
    private void ReplaceWithKingPrefab(int row, int col, GameObject kingPrefab)
    {
        // Destroy the old piece
        GameObject oldPiece = boardObjects[row, col];
        Destroy(oldPiece);

        // Instantiate the king prefab
        Vector3 position = new Vector3(col, row, 0) + boardOffset;
        GameObject newPiece = Instantiate(kingPrefab, position, Quaternion.identity, transform);
        boardObjects[row, col] = newPiece;
    }
    private void CapturePiece(int fromRow, int fromCol, int toRow, int toCol)
    {
        int capturedRow = (fromRow + toRow) / 2;
        int capturedCol = (fromCol + toCol) / 2;

        // Remove the captured piece from the board state
        boardState[capturedRow, capturedCol] = Piece.Empty;

        // Destroy the captured piece GameObject
        Destroy(boardObjects[capturedRow, capturedCol]);
        boardObjects[capturedRow, capturedCol] = null;
    }

    //Gameplay
    private bool isPlayerTurn = true; // Player1 starts first
    private GameObject selectedPiece = null;

    private void Update()
    {
        if (isPlayerTurn)
        {
            HandlePlayerInput(Piece.Player1, Piece.Player1King); // For Player 1
        }
        else
        {
            HandlePlayerInput(Piece.Player2, Piece.Player2King); // For Player 2
        }
    }

    private void HandlePlayerInput(Piece playerPiece, Piece playerKing)
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse click
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int boardPos = GetBoardPositionFromMouse(mousePos);

            // Check if the click is within board bounds
            if (boardPos.x < 0 || boardPos.x >= 8 || boardPos.y < 0 || boardPos.y >= 8)
            {
                Debug.LogWarning("Clicked outside of the board!");
                return;
            }

            if (selectedPiece == null)
            {
                SelectPiece(boardPos, playerPiece, playerKing);
            }
            else
            {
                TryMovePiece(boardPos);
            }
        }
    }

    private void SelectPiece(Vector2Int boardPos, Piece playerPiece, Piece playerKing)
    {
        int row = boardPos.y;
        int col = boardPos.x;

        // Check if the selected piece belongs to the current player
        if (boardState[row, col] == playerPiece || boardState[row, col] == playerKing)
        {
            selectedPiece = boardObjects[row, col];
        }
    }

    private void TryMovePiece(Vector2Int boardPos)
    {
        int targetRow = boardPos.y;
        int targetCol = boardPos.x;

        if (IsValidMove(selectedPiece, targetRow, targetCol))
        {
            MovePiece(selectedPiece, targetRow, targetCol);
            isPlayerTurn = false; // End player's turn
        }

        selectedPiece = null;
    }

    private bool IsValidMove(GameObject piece, int targetRow, int targetCol)
    {
        // Implement logic to check if the move is valid
        return true; // Placeholder
    }

    private void MovePiece(GameObject piece, int targetRow, int targetCol)
    {
        // Update board state and move the piece visually
        Vector3 targetPosition = new Vector3(targetCol, targetRow, 0) + boardOffset;
        piece.transform.position = targetPosition;

        // Update the boardState array
        int oldRow = (int)piece.transform.position.y - (int)boardOffset.y;
        int oldCol = (int)piece.transform.position.x - (int)boardOffset.x;

        boardState[oldRow, oldCol] = Piece.Empty;
        boardState[targetRow, targetCol] = Piece.Player1; // Or Player1King
    }
    private void AITakeTurn()
    {
        // Find all valid moves for AI pieces
        List<Move> validMoves = FindAllValidMoves(Piece.Player2);

        if (validMoves.Count > 0)
        {
            // Pick a move (randomly for now)
            Move chosenMove = validMoves[Random.Range(0, validMoves.Count)];

            // Perform the move
            MovePiece(boardObjects[chosenMove.FromRow, chosenMove.FromCol], chosenMove.ToRow, chosenMove.ToCol);
        }

        isPlayerTurn = true; // End AI's turn
    }
    private List<Move> FindAllValidMoves(Piece pieceType)
    {
        List<Move> moves = new List<Move>();

        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                if (boardState[row, col] == pieceType)
                {
                    // Check all possible moves for this piece
                    List<Move> pieceMoves = GetValidMovesForPiece(row, col);
                    moves.AddRange(pieceMoves);
                }
            }
        }

        return moves;
    }

    private List<Move> GetValidMovesForPiece(int row, int col)
    {
        List<Move> moves = new List<Move>();

        // Example: Check diagonal moves
        int[] rowOffsets = { 1, -1 };
        int[] colOffsets = { 1, -1 };

        foreach (int rowOffset in rowOffsets)
        {
            foreach (int colOffset in colOffsets)
            {
                int newRow = row + rowOffset;
                int newCol = col + colOffset;

                if (IsValidMove(null, newRow, newCol)) // null for now
                {
                    moves.Add(new Move(row, col, newRow, newCol));
                }
            }
        }

        return moves;
    }
    public struct Move
    {
        public int FromRow;
        public int FromCol;
        public int ToRow;
        public int ToCol;

        public Move(int fromRowP, int fromColP, int toRowP, int toColP)
        {
            FromRow = fromRowP;
            FromCol = fromColP;
            ToRow = toRowP;
            ToCol = toColP;
        }
    }
    private bool IsGameOver()
    {
        // Check if Player1 or Player2 has no pieces or valid moves
        bool player1HasMoves = FindAllValidMoves(Piece.Player1).Count > 0;
        bool player2HasMoves = FindAllValidMoves(Piece.Player2).Count > 0;

        return !player1HasMoves || !player2HasMoves;
    }
    private Vector2Int GetBoardPositionFromMouse(Vector3 mousePosition)
    {
        int col = Mathf.RoundToInt(mousePosition.x - boardOffset.x);
        int row = Mathf.RoundToInt(mousePosition.y - boardOffset.y);

        return new Vector2Int(col, row);
    }
}