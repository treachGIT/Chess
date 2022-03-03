using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Chess.Pieces
{
    class Pawn : Piece
    {
        public Pawn()
        {
            symbol = "p";
            weight = 1;
        }

        public override void FindLegalMoves(Square[,] board, Square currentsquare)
        {
            squaresToMove.Clear();
            int x = currentsquare.x;
            int y = currentsquare.y;
            int nextrow;
            if (color == Color.White)
            {
                nextrow = x - 1;
                if(nextrow >= 0)
                {
                    if (board[nextrow, y].GetSquareState(color) == 0)
                    {
                        squaresToMove.Add(board[nextrow, y]);

                        if (moveNumber == 0 && board[nextrow - 1, y].GetSquareState(color) == 0)
                            squaresToMove.Add(board[nextrow - 1, y]);
                    }
                }         
            }   
            else
            {
                nextrow = x + 1;
                if(nextrow < 8)
                {
                    if (board[nextrow, y].GetSquareState(color) == 0)
                    {
                        squaresToMove.Add(board[nextrow, y]);

                        if (moveNumber == 0 && board[nextrow + 1, y].GetSquareState(color) == 0)
                            squaresToMove.Add(board[nextrow + 1, y]);
                    }
                }             
            }

            if (y + 1 < 8)
            {
                if (board[nextrow, y + 1].GetSquareState(color) == 2)
                    squaresToMove.Add(board[nextrow, y + 1]);
            }

            if(y-1 >= 0)
            {
                if (board[nextrow, y - 1].GetSquareState(color) == 2)
                    squaresToMove.Add(board[nextrow, y - 1]);   
            }

        }

    }
}
