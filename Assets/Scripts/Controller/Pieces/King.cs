using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Chess.Pieces
{
    class King : Piece
    {
        public King()
        {
            symbol = "K";
        }

        public override void FindLegalMoves(Square[,] board, Square currentsquare)
        {
            kingMovesCheck = true;
            squaresToMove = GetHorizontalSquares(board, currentsquare);
            squaresToMove.AddRange(GetVerticalSquares(board, currentsquare));
            squaresToMove.AddRange(GetDiagonalSquares(board, currentsquare));
            kingMovesCheck = false;

            FindCastleMoves(board, currentsquare);

        }



        public void FindCastleMoves(Square[,] board, Square currentsquare)
        {
            List<Square> tempCheck = new List<Square>();

            if (moveNumber == 0)
            {
                if (color == Color.White)
                {
                    if (board[7, 5].currentPiece == null && board[7, 6].currentPiece == null)
                    {
                        if (board[7, 7].GetSquareState(color) == 1 && board[7, 7].currentPiece.symbol == "R" && board[7, 7].currentPiece.moveNumber == 0)
                        {
                            tempCheck.Add(board[7, 5]);
                            tempCheck.Add(board[7, 6]);

                            if (CheckCastle(tempCheck, board, currentsquare) == true)
                                squaresToMove.Add(board[7,6]);

                            tempCheck.Clear();
                        }
                    }

                    if (board[7, 3].currentPiece == null && board[7, 2].currentPiece == null && board[7, 1].currentPiece == null)
                    {
                        if (board[7, 0].GetSquareState(color) == 1 && board[7, 0].currentPiece.symbol == "R" && board[7, 0].currentPiece.moveNumber == 0)
                        {
                            tempCheck.Add(board[7, 3]);
                            tempCheck.Add(board[7, 2]);
                            tempCheck.Add(board[7, 1]);

                            if (CheckCastle(tempCheck, board, currentsquare) == true)
                                squaresToMove.Add(board[7, 2]);

                            tempCheck.Clear();
                        }
                    }
                }
                else if (color == Color.Black)
                {
                    if (board[0, 5].currentPiece == null && board[0, 6].currentPiece == null)
                    {
                        if (board[0, 7].GetSquareState(color) == 1 && board[0, 7].currentPiece.symbol == "R" && board[0, 7].currentPiece.moveNumber == 0)
                        {
                            tempCheck.Add(board[0, 5]);
                            tempCheck.Add(board[0, 6]);

                            if (CheckCastle(tempCheck, board, currentsquare) == true)
                                squaresToMove.Add(board[0, 6]);

                            tempCheck.Clear();
                        }
                    }

                    if (board[0, 3].currentPiece == null && board[0, 2].currentPiece == null && board[0, 1].currentPiece == null)
                    {
                        if (board[0, 0].GetSquareState(color) == 1 && board[0, 0].currentPiece.symbol == "R" && board[0, 0].currentPiece.moveNumber == 0)
                        {
                            tempCheck.Add(board[0, 3]);
                            tempCheck.Add(board[0, 2]);
                            tempCheck.Add(board[0, 1]);

                            if (CheckCastle(tempCheck, board, currentsquare) == true)
                                squaresToMove.Add(board[0, 2]);

                            tempCheck.Clear();
                        }
                    }
                }
            }
        }

        public bool CheckCastle(List<Square> tempCheck, Square[,] board, Square currentsquare)
        {
            if (currentsquare.currentPiece.isCheck(board, currentsquare) == false)
            {
                for (int i = 0; i < tempCheck.Count; i++)
                {
                    tempCheck[i].currentPiece = this;
                    if (tempCheck[i].currentPiece.isCheck(board, tempCheck[i]) == true)
                    {
                        tempCheck[i].currentPiece = null;
                        return false;
                    }
                    tempCheck[i].currentPiece = null;
                }
                return true;
            }
            return false;
        }

    }
}
