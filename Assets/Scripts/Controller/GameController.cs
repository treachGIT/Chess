using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using Chess.Pieces;
using Stockfish.NET;
using UnityEngine;
using UnityEngine.UI;

namespace Chess
{
    public class GameController
    {
        private Square[,] board = new Square[8,8];
        private Piece[] blackPieces =
        {
            new Rook(), new Knight(), new Bishop(), new Queen(), new King(), new Bishop(), new Knight(), new Rook(),
            new Pawn(), new Pawn() , new Pawn(), new Pawn(), new Pawn(), new Pawn(), new Pawn(), new Pawn()
        };
        private Piece[] whitePieces =
        {
            new Pawn(), new Pawn() , new Pawn(), new Pawn(), new Pawn(), new Pawn(), new Pawn(), new Pawn(),
            new Rook(), new Knight(), new Bishop(), new Queen(), new King(), new Bishop(), new Knight(), new Rook()
        };
        private List<Move> moves = new List<Move>();
        public int gameTime;
        public System.Drawing.Color side;
        public string currentSideMove = "white";
        public string winner = "";
        private GameView gameView;
        IStockfish stockfish;
        public int moveNumber = 1;

        public GameController()
        {
            gameTime = Settings.gameLength;
            this.gameTime = gameTime * 60;
            if (Settings.side == "white")
                this.side = System.Drawing.Color.White;
            else
                this.side = System.Drawing.Color.Black;
        }
        public void StartGame()
        {
            InitPieces();
            InitBoard();
           // Debug.Log(Settings.botPath);
            stockfish = new Stockfish.NET.Core.Stockfish(Settings.botPath);//@"D:\Unity\dsadsa\Chess\Assets\Resources\Stockfish.NET-master\Stockfish.NET\stockfish_12_win_x64\stockfish_20090216_x64.exe");


            GameObject boardObject= GameObject.Find("Board");
            gameView = boardObject.GetComponent<GameView>();
            gameView.SetGameController(this);

            if (side == System.Drawing.Color.White)
                gameView.SetGameParams("Gracz", "StockfishBot");
            else
                gameView.SetGameParams("StockfishBot", "Gracz");

            gameView.blackTimer.GetComponent<Timer>().SetTimer(gameTime);
            gameView.whiteTimer.GetComponent<Timer>().SetTimer(gameTime);

            gameView.PrintMoveNumber(moveNumber);

        }

        void InitPieces()
        {
            for (int i = 0; i < 16; i++)
            {
                System.Drawing.Color whiteColor = System.Drawing.Color.White;
                System.Drawing.Color blackColor = System.Drawing.Color.Black;
                whitePieces[i].SetColor(whiteColor);
                blackPieces[i].SetColor(blackColor);
            }
        }

        void InitBoard()
        {
            int whiteIndex = 0;
            int blackIndex = 0;
            for (int i = 0; i < 8; i++)
            {

                for (int j = 0; j < 8; j++)
                {
                    Square square;

                    if (i > 5)
                    {
                        square = new Square(whitePieces[whiteIndex], i, j);
                        whiteIndex++;

                    }
                    else if (i < 2)
                    {
                        square = new Square(blackPieces[blackIndex], i, j);
                        blackIndex++;
                    }
                    else
                    {
                        square = new Square(i, j);
                    }

                    board[i, j] = square;

                }
            }
        }

        public int MoveControl(int startX, int startY, int endX, int endY)
        {
            int returnValue = 0;
            Square currentSelected = board[startY, startX];
            Square currentNextSquare = board[endY, endX];

            Move tempmove = new Move(currentSelected, currentNextSquare);
            bool promotion = currentSelected.isPromotionMove(currentNextSquare);
            int result = currentSelected.Move(currentNextSquare, board);
            if (result == 0)
            {
                returnValue = 1; //legal move
                if (promotion == true)
                {
                    Piece promotionPiece = new Queen();
                    System.Drawing.Color color = currentNextSquare.currentPiece.color;
                    currentSelected.currentPiece = promotionPiece;
                    currentSelected.currentPiece.SetColor(color);
                    currentNextSquare.currentPiece = null;
                    gameView.ChangePieceSprite(endY, endX, Settings.side);
                    int tempres = currentSelected.Move(currentNextSquare, board);
                }
                
            }
            else if (result == 2)
            {
                returnValue = 2; //game end

                winner = "Gracz";

                gameView.ShowPanel(winner);
            }
            else if (result == 3)
            {
                returnValue = 3; //roszada
            }
            else if(result == 1)
            {
                returnValue = 0; //wrong move
            }

            if (result != 1)
            {
                moves.Add(tempmove);
                if(Settings.side == "white")
                {
                    gameView.PrintWhiteMove(tempmove.GetMove());
                }
                else
                {
                    moveNumber++;
                    gameView.PrintBlackMove(tempmove.GetMove());
                    gameView.PrintMoveNumber(moveNumber);
                }
             
            }

            if (result != 1)
            {
                if(currentSideMove == "white")
                {
                    currentSideMove = "black";

                    foreach(GameObject piece in gameView.whitePieces)
                    {
                        piece.GetComponent<CanvasGroup>().blocksRaycasts = false;
                    }

                    foreach (GameObject piece in gameView.blackPieces)
                    {
                        piece.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    }

                    gameView.blackTimer.GetComponent<Timer>().StartTimer();
                    gameView.whiteTimer.GetComponent<Timer>().StopTimer();
                }
                else
                {
                    currentSideMove = "white";
                    foreach (GameObject piece in gameView.blackPieces)
                    {
                        piece.GetComponent<CanvasGroup>().blocksRaycasts = false;

                    }

                    foreach (GameObject piece in gameView.whitePieces)
                    {
                        piece.GetComponent<CanvasGroup>().blocksRaycasts = true;

                    }

                    gameView.whiteTimer.GetComponent<Timer>().StartTimer();
                    gameView.blackTimer.GetComponent<Timer>().StopTimer();
                }
            }

            return returnValue;
        }

        public List<GameObject> getLegalMoves(int X, int Y, GameObject[,] cells)
        {
            Square square = board[Y, X];
            List<Square> moves = square.GetMoves(board);
            List<GameObject> moveCells = new List<GameObject>();

            foreach (Square move in moves)
            {
                moveCells.Add(cells[move.x, move.y]);
            }

            return moveCells;
        }

        public void MakeBotMove()
        {        

            string[] currentMoves = new string[moves.Count];
            int index = 0;
            foreach (Move move in moves)
            {
                currentMoves[index] = move.GetBotMove();
                index++;
            }

            stockfish.SetPosition(currentMoves);
            string bestMove = stockfish.GetBestMove();

            string res = bestMove[0].ToString() + bestMove[1].ToString();
            string res2 = bestMove[2].ToString() + bestMove[3].ToString();
            Square start = Move.GetSquareFromMove(res, board);
            Square end = Move.GetSquareFromMove(res2, board);
            Debug.Log(start.x + " start " + start.y);
            Debug.Log(end.x + " end " + end.y);

            if (gameView.Cells[end.x, end.y].GetComponent<DropCell>().currentPiece != null)
                gameView.Cells[end.x, end.y].GetComponent<DropCell>().currentPiece.SetActive(false);

            Debug.Log(start.x + " start " + start.y);
            Debug.Log(end.x + " start " + end.y);
            gameView.Cells[start.x, start.y].GetComponent<DropCell>().currentPiece.transform.position = gameView.Cells[end.x, end.y].transform.position;
            gameView.Cells[end.x, end.y].GetComponent<DropCell>().currentPiece = gameView.Cells[start.x, start.y].GetComponent<DropCell>().currentPiece;
            gameView.Cells[start.x, start.y].GetComponent<DropCell>().currentPiece = null;

        
            Move tempmove = new Move(start, end);
            int result = start.Move(end, board);
            if (bestMove.Length > 4)
                tempmove.botNotation += "q";

            moves.Add(tempmove);
           
            if (Settings.side == "white")
            {
                gameView.PrintBlackMove(tempmove.GetMove());
                moveNumber++;
                gameView.PrintMoveNumber(moveNumber);
            }
            else
            {
                gameView.PrintWhiteMove(tempmove.GetMove());
            }

            if (bestMove.Length > 4)
            {
                Piece promotionPiece = new Queen();
                System.Drawing.Color color = end.currentPiece.color;
                start.currentPiece = promotionPiece;
                start.currentPiece.SetColor(color);
                end.currentPiece = null;

                if (Settings.side == "white")
                    gameView.ChangePieceSprite(end.x, end.y, "black");
                else
                    gameView.ChangePieceSprite(end.x, end.y, "white");

                int tempres = start.Move(end, board);
            }

            if(result == 3)
            {
                Debug.Log(end.x + " castle " + end.y);
                gameView.ApplyCastle(end.y, end.x);
            }

            if(result == 2 || CheckGameEnd())
            {
                Debug.Log("mate");
                foreach (GameObject piece in gameView.whitePieces)
                {
                    piece.GetComponent<CanvasGroup>().blocksRaycasts = false;
                }

                foreach (GameObject piece in gameView.blackPieces)
                {
                    piece.GetComponent<CanvasGroup>().blocksRaycasts = false;
                }

                gameView.blackTimer.GetComponent<Timer>().StopTimer();
                gameView.whiteTimer.GetComponent<Timer>().StopTimer();

                winner = "StockfishBot";

                gameView.ShowPanel(winner);
            }
            else
            {
                if (currentSideMove == "white")
                {
                    currentSideMove = "black";

                    foreach (GameObject piece in gameView.whitePieces)
                    {
                        piece.GetComponent<CanvasGroup>().blocksRaycasts = false;
                    }

                    foreach (GameObject piece in gameView.blackPieces)
                    {
                        piece.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    }

                    gameView.blackTimer.GetComponent<Timer>().StartTimer();
                    gameView.whiteTimer.GetComponent<Timer>().StopTimer();
                }
                else
                {
                    currentSideMove = "white";
                    foreach (GameObject piece in gameView.blackPieces)
                    {
                        piece.GetComponent<CanvasGroup>().blocksRaycasts = false;

                    }

                    foreach (GameObject piece in gameView.whitePieces)
                    {
                        piece.GetComponent<CanvasGroup>().blocksRaycasts = true;

                    }
                    gameView.blackTimer.GetComponent<Timer>().StopTimer();
                    gameView.whiteTimer.GetComponent<Timer>().StartTimer();

                }
            }
        }

        public bool CheckGameEnd()
        {
            string[] currentMoves = new string[moves.Count];
            int index = 0;
            foreach (Move move in moves)
            {
                currentMoves[index] = move.GetBotMove();
                index++;
            }

            stockfish.SetPosition(currentMoves);
            string bestMove = stockfish.GetBestMove();

            if (bestMove == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
