using Chess;
using Chess.Pieces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField]
    private GameObject CellPrefab;
    [SerializeField]
    private GameObject PiecePrefab;
    [SerializeField]
    public Text whiteTimer;
    [SerializeField]
    public Text blackTimer;
    [SerializeField]
    public Text whiteName;
    [SerializeField]
    public Text blackName;

    [SerializeField]
    private GameObject InfoPanel;
    [SerializeField]
    private GameObject MovePanel;
    [SerializeField]
    private GameObject MoveList;


    private Image cellImage;

    private int cellDistance = 100;

    public GameObject[,] Cells = new GameObject[8, 8];
    public List<GameObject> whitePieces = new List<GameObject>();
    public List<GameObject> blackPieces = new List<GameObject>();
    private Sprite wPawn, wRook, wBishop, wKnight, wQueen, wKing;
    private Sprite bPawn, bRook, bBishop, bKnight, bQueen, bKing;

    public GameController gameController;
    List<GameObject> currentLegalMoves = new List<GameObject>();

    bool temp = false;
    public bool moveWait = false;

    private GameObject CurrentMovePanel;

    void Start()
    {
        GetCellImage();
        DrawBoard();
        LoadPieces();
        DrawPieces();
    }

    private void GetCellImage()
    {
        cellImage = CellPrefab.GetComponent<Image>();
    }

    private void DrawBoard()
    {
        int x = 50;
        int y = -50;

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                SetCellColor(i, j);
                Cells[i, j] = Instantiate(CellPrefab, transform);

                Cells[i, j].GetComponent<DropCell>().currentPiece = null;
                Cells[i, j].GetComponent<DropCell>().whitePieces = whitePieces;
                Cells[i, j].GetComponent<DropCell>().blackPieces = blackPieces;

                RectTransform currentPos = Cells[i, j].GetComponent<RectTransform>();
                currentPos.anchoredPosition = new Vector2(x, y);

                x += cellDistance;
            }

            x = 50;
            y -= cellDistance;
        }
    }

    private void SetCellColor(int x, int y)
    {
        if (x % 2 == 0)
        {
            if (y % 2 == 0)
                cellImage.color = Settings.LightColor;
            else
                cellImage.color = Settings.DarkColor;
        }
        else
        {
            if (y % 2 == 0)
                cellImage.color = Settings.DarkColor;
            else
                cellImage.color = Settings.LightColor;
        }
    }

    private void LoadPieces()
    {
        wPawn = Resources.Load<Sprite>(Settings.piecePath + "WhitePieces/Pawn");
        wRook = Resources.Load<Sprite>(Settings.piecePath + "WhitePieces/Rook");
        wBishop = Resources.Load<Sprite>(Settings.piecePath + "WhitePieces/Bishop");
        wKnight = Resources.Load<Sprite>(Settings.piecePath + "WhitePieces/Knight");
        wQueen = Resources.Load<Sprite>(Settings.piecePath + "WhitePieces/Queen");
        wKing = Resources.Load<Sprite>(Settings.piecePath + "WhitePieces/King");

        bPawn = Resources.Load<Sprite>(Settings.piecePath + "BlackPieces/Pawn");
        bRook = Resources.Load<Sprite>(Settings.piecePath + "BlackPieces/Rook");
        bBishop = Resources.Load<Sprite>(Settings.piecePath + "BlackPieces/Bishop");
        bKnight = Resources.Load<Sprite>(Settings.piecePath + "BlackPieces/Knight");
        bQueen = Resources.Load<Sprite>(Settings.piecePath + "BlackPieces/Queen");
        bKing = Resources.Load<Sprite>(Settings.piecePath + "BlackPieces/King");
    }

    private void DrawPieces()
    {
        GameObject piece;

        for (int i = 0; i < 8; i++)
        {
            piece = Instantiate(PiecePrefab, transform);
            piece.GetComponent<Image>().sprite = bPawn;
            piece.transform.position = Cells[1, i].transform.position;
            piece.GetComponent<PieceView>().view = this;
            piece.GetComponent<PieceView>().color = "black";
            blackPieces.Add(piece);
            Cells[1, i].GetComponent<DropCell>().currentPiece = piece;

            piece = Instantiate(PiecePrefab, transform);
            piece.GetComponent<Image>().sprite = wPawn;
            piece.transform.position = Cells[6, i].transform.position;
            piece.GetComponent<PieceView>().view = this;
            piece.GetComponent<PieceView>().color = "white";
            whitePieces.Add(piece);
            Cells[6, i].GetComponent<DropCell>().currentPiece = piece;
        }


        PlacePiece(0, 0, bRook, "black");
        PlacePiece(0, 7, bRook, "black");
        PlacePiece(7, 0, wRook, "white");
        PlacePiece(7, 7, wRook, "white");

        PlacePiece(0, 1, bKnight, "black");
        PlacePiece(0, 6, bKnight, "black");
        PlacePiece(7, 1, wKnight, "white");
        PlacePiece(7, 6, wKnight, "white");

        PlacePiece(0, 2, bBishop, "black");
        PlacePiece(0, 5, bBishop, "black");
        PlacePiece(7, 2, wBishop, "white");
        PlacePiece(7, 5, wBishop, "white");

        PlacePiece(0, 3, bQueen, "black");
        PlacePiece(7, 3, wQueen, "white");

        PlacePiece(0, 4, bKing, "black");
        PlacePiece(7, 4, wKing, "white");

    }

    private void PlacePiece(int x, int y, Sprite pieceSprite, string color)
    {
        GameObject piece = Instantiate(PiecePrefab, transform);
        piece.GetComponent<Image>().sprite = pieceSprite;
        piece.transform.position = Cells[x, y].transform.position;

        piece.GetComponent<PieceView>().view = this;

        piece.GetComponent<PieceView>().color = color;

        if (color == "white")
            whitePieces.Add(piece);
        else
            blackPieces.Add(piece);

        Cells[x, y].GetComponent<DropCell>().currentPiece = piece;
    }

    public void SetGameController(GameController controller)
    {
        gameController = controller;
    }

    public void SetGameParams(string whitePlayer, string blackPlayer)
    {
        whiteName.text = whitePlayer;
        blackName.text = blackPlayer;
    }

    public void GetLegalMoves(float X, float Y)
    {
        int x = GetCellX(X);
        int y = GetCellY(Y);

        currentLegalMoves = gameController.getLegalMoves(x, y, Cells);
        HighlightLegalMoves();

        foreach (GameObject cell in Cells)
        {
            cell.GetComponent<Image>().raycastTarget = false;
        }

        foreach (GameObject cell in currentLegalMoves)
        {
            cell.GetComponent<Image>().raycastTarget = true;
        }
        Cells[y, x].GetComponent<Image>().raycastTarget = true;
    }

    public void HighlightLegalMoves()
    {
        foreach (GameObject cell in currentLegalMoves)
        {
            cell.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void DeleteHighlightMoves()
    {
        foreach (GameObject cell in currentLegalMoves)
        {
            cell.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        currentLegalMoves = null;
    }

    public bool isMoveLegal(float X, float Y)
    {
        int x = GetCellX(X);
        int y = GetCellY(Y);

        foreach (GameObject cell in currentLegalMoves)
        {
            if (cell == Cells[y, x])
            {
                return true;
            }
        }

        return false;
    }

    public void MakeMove(float startX, float startY, float endX, float endY)
    {
        int sX = GetCellX(startX);
        int sY = GetCellY(startY);

        int eX = GetCellX(endX);
        int eY = GetCellY(endY);

        int moveFlag = gameController.MoveControl(sX, sY, eX, eY);

        if (moveFlag == 0)
        {
            Cells[sY, sX].GetComponent<DropCell>().currentPiece.transform.position = Cells[sY, sX].transform.position;
            Cells[sY, sX].GetComponent<DropCell>().previousPiece = null;

            Cells[eY, eX].GetComponent<DropCell>().currentPiece = null;
            Cells[eY, eX].GetComponent<DropCell>().currentPiece = Cells[eY, eX].GetComponent<DropCell>().previousPiece;
            if (Cells[eY, eX].GetComponent<DropCell>().currentPiece != null)
                Cells[eY, eX].GetComponent<DropCell>().currentPiece.SetActive(true);
            Cells[eY, eX].GetComponent<DropCell>().previousPiece = null;

        }

        if (moveFlag != 0)
        {
            Cells[sY, sX].GetComponent<DropCell>().currentPiece = null;

            if (moveFlag == 3)
            {
                ApplyCastle(eX, eY);
            }
            if (moveFlag == 2)
            {
                foreach (GameObject piece in whitePieces)
                {
                    piece.GetComponent<CanvasGroup>().blocksRaycasts = false;
                }

                foreach (GameObject piece in blackPieces)
                {
                    piece.GetComponent<CanvasGroup>().blocksRaycasts = false;
                }

                if (Cells[eY, eX].GetComponent<DropCell>().currentPiece.GetComponent<PieceView>().color == "white")
                {
                    foreach (GameObject piece in blackPieces)
                    {
                        if (piece.GetComponent<Image>().sprite == bKing)
                        {
                            piece.GetComponent<Image>().color = Color.red;
                        }
                    }
                }
                else if (Cells[eY, eX].GetComponent<DropCell>().currentPiece.GetComponent<PieceView>().color == "black")
                {
                    foreach (GameObject piece in whitePieces)
                    {
                        if (piece.GetComponent<Image>().sprite == wKing)
                        {
                            piece.GetComponent<Image>().color = Color.red;
                        }
                    }
                }
            }
        }
        Debug.Log(moveFlag);
    }

    public void ApplyCastle(int eX, int eY)
    {
        if (eY == 7 && eX == 6)
        {
            Cells[eY, eX].GetComponent<DropCell>().currentPiece = null;
            GameObject rook = Cells[7, 7].GetComponent<DropCell>().currentPiece;
            Cells[7, 5].GetComponent<DropCell>().currentPiece = rook;
            Cells[7, 7].GetComponent<DropCell>().currentPiece = null;
            rook.transform.position = Cells[7, 5].transform.position;
        }
        if (eY == 7 && eX == 2)
        {
            Cells[eY, eX].GetComponent<DropCell>().currentPiece = null;
            GameObject rook = Cells[7, 0].GetComponent<DropCell>().currentPiece;
            Cells[7, 3].GetComponent<DropCell>().currentPiece = rook;
            Cells[7, 0].GetComponent<DropCell>().currentPiece = null;
            rook.transform.position = Cells[7, 3].transform.position;
        }
        if (eY == 0 && eX == 6)
        {
            Cells[eY, eX].GetComponent<DropCell>().currentPiece = null;
            GameObject rook = Cells[0, 7].GetComponent<DropCell>().currentPiece;
            Cells[0, 5].GetComponent<DropCell>().currentPiece = rook;
            Cells[0, 7].GetComponent<DropCell>().currentPiece = null;
            rook.transform.position = Cells[0, 5].transform.position;
        }
        if (eY == 0 && eX == 2)
        {
            Cells[eY, eX].GetComponent<DropCell>().currentPiece = null;
            GameObject rook = Cells[0, 0].GetComponent<DropCell>().currentPiece;
            Cells[0, 3].GetComponent<DropCell>().currentPiece = rook;
            Cells[0, 0].GetComponent<DropCell>().currentPiece = null;
            rook.transform.position = Cells[0, 3].transform.position;
        }
    }

    private int GetCellX(float pos)
    {
        int x = Mathf.RoundToInt(pos);
        int resultX = (x + 350) / 100;
        return resultX;
    }

    private int GetCellY(float pos)
    {
        int y = Mathf.RoundToInt(pos);
        int resultY = (350 - y) / 100;
        return resultY;
    }

    public void ChangePieceSprite(int X, int Y, string color)
    {
        if (color == "white")
            Cells[X, Y].GetComponent<DropCell>().currentPiece.GetComponent<Image>().sprite = wQueen;
        else
            Cells[X, Y].GetComponent<DropCell>().currentPiece.GetComponent<Image>().sprite = bQueen;

        Cells[X, Y].GetComponent<DropCell>().currentPiece.GetComponent<PieceView>().color = color;
    }

    private IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        temp = true;
    }

    public void WaitForBot()
    {
        StartCoroutine(Wait(Random.Range(2f, 3f)));
    }

    private void Update()
    {
        if (blackTimer.GetComponent<Timer>().gameTimeEnd == false)
        {
            if (gameController.currentSideMove != Settings.side && moveWait == true)
            {
                foreach (GameObject piece in whitePieces)
                {
                    piece.GetComponent<CanvasGroup>().blocksRaycasts = false;
                }

                foreach (GameObject piece in blackPieces)
                {
                    piece.GetComponent<CanvasGroup>().blocksRaycasts = false;
                }
                moveWait = false;
                WaitForBot();
            }

            if (temp == true)
            {
                temp = false;
                gameController.MakeBotMove();
            }
        }
    }

    public void ExitGameView()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void PrintWhiteMove(string move)
    {
        CurrentMovePanel.transform.Find("First").GetComponent<Text>().text = move;
    }
    public void PrintBlackMove(string move)
    {
        CurrentMovePanel.transform.Find("Second").GetComponent<Text>().text = move;
    }
    public void PrintMoveNumber(int number)
    {
        float x;
        float y;
        if (CurrentMovePanel == null)
        {
            RectTransform current = MovePanel.GetComponent<RectTransform>();
            x = current.anchoredPosition.x;
            y = current.anchoredPosition.y;
        }
        else
        {
            RectTransform current = CurrentMovePanel.GetComponent<RectTransform>();
            x = current.anchoredPosition.x;
            y = current.anchoredPosition.y;
            y = y - 45;
        }

        CurrentMovePanel = Instantiate(MovePanel, MoveList.transform);
        CurrentMovePanel.transform.Find("MoveNumber").GetComponent<Text>().text = number.ToString();
        RectTransform currentPos = CurrentMovePanel.GetComponent<RectTransform>();
        currentPos.anchoredPosition = new Vector2(x, y);
    }

    public void ShowPanel(string winner)
    {
        GameObject panel = Instantiate(InfoPanel, transform);
        panel.transform.GetChild(0).gameObject.GetComponent<Text>().text = winner + " won.";
    }
}
