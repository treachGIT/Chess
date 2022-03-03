using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Chess.Pieces;

namespace Chess
{
    class Square
    {
        public Piece currentPiece;
        public int x;
        public int y;

        public Square(int x, int y) {
            currentPiece = null;
            this.x = x;
            this.y = y;
        }

        public Square(Piece piece, int x, int y)
        {
            currentPiece = piece;
            this.x = x;
            this.y = y;
        }

        public int GetSquareState(Color piececolor)
        {
            if (currentPiece != null)
            {
                if (currentPiece.color == piececolor)
                    return 1; //friendly piece
                else
                    return 2; //enemypiece
            }
            else
            {
                return 0; //empty square
            }
        }

        public List<Square> GetMoves(Square[,] board)
        {
            List<Square> moves = new List<Square>();
            if (currentPiece!=null)
            {
                currentPiece.FindLegalMoves(board, this);
                moves = currentPiece.GetLegalMoves();
            }
            return moves;
        }

        public int Move(Square square , Square [,] board)
        {
            Color currentcolor = currentPiece.color;

            Piece nextSquarePiece;
            if (square.currentPiece != null)
                nextSquarePiece = square.currentPiece;
            else
                nextSquarePiece = null;

            bool isCastle = false;
            if (isCastleMove(square) == 1)
            {
                board[square.x, square.y - 1].currentPiece = board[square.x, square.y + 1].currentPiece;
                board[square.x, square.y + 1].currentPiece = null;
                isCastle = true;
            }
            else if (isCastleMove(square) == 2)
            {
                board[square.x, square.y + 1].currentPiece = board[square.x, square.y - 2].currentPiece;
                board[square.x, square.y - 2].currentPiece = null;
                isCastle = true;
            }


            currentPiece.moveNumber++;
            square.currentPiece = currentPiece;
            currentPiece = null;
            
            for (int a = 0; a < 8; a++)
            {
                for (int b = 0; b < 8; b++)
                {
                    if (board[a, b].GetSquareState(currentcolor) == 1 && board[a, b].currentPiece.symbol == "K")
                    {
                        if (isCastle == true)
                        {
                            return 3; //roszada
                        }

                        int result = board[a, b].CheckKing(board);
                        if (result == 1)
                        {
                            currentPiece = square.currentPiece;
                            currentPiece.moveNumber--;
                            square.currentPiece = nextSquarePiece;
                            return 1;   //nasz krol szachowany cofamy ruch
                        }
                    }
                    else if (board[a, b].GetSquareState(currentcolor) == 2 && board[a, b].currentPiece.symbol == "K")
                    {
                        int result = board[a, b].CheckKing(board);
                        if (result == 2)
                        {
                            return 2;  //szach mat
                        }
                    }
                }
            }

            return 0;  //ruch wykonany
        }

        public int CheckKing(Square[,] board)
        {
            if(currentPiece != null && currentPiece.symbol == "K")
            {
                if (currentPiece.isCheck(board, this) == false)
                    return 0; //nie ma szacha
                else 
                {
                    List<Square> squaresToCheck = GetMoves(board);
                    for(int i=0;i< squaresToCheck.Count; i++)
                    {
                        Piece tempPiece;
                        if (squaresToCheck[i].currentPiece != null)
                            tempPiece = squaresToCheck[i].currentPiece;
                        else
                            tempPiece = null;

                        squaresToCheck[i].currentPiece = currentPiece;
                        currentPiece = null;
                        if (squaresToCheck[i].currentPiece.isCheck(board, squaresToCheck[i]) == false)
                        {
                            currentPiece = squaresToCheck[i].currentPiece;
                            if (tempPiece == null)
                                squaresToCheck[i].currentPiece = null;
                            else
                                squaresToCheck[i].currentPiece = tempPiece;

                            return 1; //jest szach
                        }
                        currentPiece = squaresToCheck[i].currentPiece;
                        squaresToCheck[i].currentPiece = null;
                    }

                    bool test = currentPiece.isCheck(board, this);
                    if (currentPiece.attackingSquares.Count > 1)
                        return 2;

                    List<Square> tempmoves = new List<Square>();
                    for (int a = 0; a < 8; a++)
                    {
                        for (int b = 0; b < 8; b++)
                        {
                            if (board[a, b].GetSquareState(currentPiece.color) == 1)
                            {
                                tempmoves.AddRange(board[a, b].GetMoves(board));
                            }
                        }
                    }

                    for (int i = 0; i< currentPiece.squaresOnCheckLine.Count; i++)
                    {
                        for (int j = 0; j < tempmoves.Count; j++)
                        {
                            if (currentPiece.squaresOnCheckLine[i] == tempmoves[j])
                                return 1; // mozna zablokowac
                        }

                    }

                    return 2; //szach mat
                }
            }
            return 0;
        }


        public int isCastleMove(Square next)
        {

            if (currentPiece.symbol == "K" && currentPiece.moveNumber == 0)
            {
                if (currentPiece.color == Color.White)
                {
                    if (next.x == 7 && next.y == 6)
                        return 1;
                    else if (next.x == 7 && next.y == 2)
                        return 2;
                }
                else if (currentPiece.color == Color.Black)
                {
                    if (next.x == 0 && next.y == 6)
                        return 1;
                    if (next.x == 0 && next.y == 2)
                        return 2;
                }
            }
            return 0;
        }

        public bool isPromotionMove(Square next)
        {         
            if (currentPiece.symbol == "p")
            {
                if (currentPiece.color == Color.White)
                {
                    if (next.x == 0)
                        return true;
                }
                else if (currentPiece.color == Color.Black)
                {
                    if (next.x == 7)
                        return true;
                }
            }
            return false;
        }
    }
   
}
