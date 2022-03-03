using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Chess.Pieces
{
    class Knight : Piece
    {
        public Knight()
        {
            symbol = "N";
            weight = 3;
        }

        public override void FindLegalMoves(Square[,] board, Square currentsquare)
        {
            squaresToMove.Clear();
            int x = currentsquare.x;
            int y = currentsquare.y;
            int xPos;
            int yPos;
            
            xPos = x - 1;
            yPos = y - 2;
            if (xPos >=0 && yPos >= 0 && board[xPos, yPos].GetSquareState(color) != 1)
            {              
                squaresToMove.Add(board[xPos, yPos]);
            }
            xPos = x - 1;
            yPos = y + 2;
            if (xPos >= 0 && yPos < 8 && board[xPos, yPos].GetSquareState(color) != 1)
            {
                squaresToMove.Add(board[xPos, yPos]);
            }
            xPos = x - 2;
            yPos = y - 1;
            if (xPos >=0 && yPos >= 0 && board[xPos, yPos].GetSquareState(color) != 1)
            {
                squaresToMove.Add(board[xPos, yPos]);
            }
            xPos = x - 2;
            yPos = y + 1;
            if (xPos >=0 && yPos < 8 && board[xPos, yPos].GetSquareState(color) != 1)
            {
                squaresToMove.Add(board[xPos, yPos]);
            }



            xPos = x + 1;
            yPos = y - 2;
            if (xPos < 8 && yPos >= 0 && board[xPos, yPos].GetSquareState(color) != 1)
            {
                squaresToMove.Add(board[xPos, yPos]);
            }
            xPos = x + 1;
            yPos = y + 2;
            if (xPos < 8 && yPos < 8 && board[xPos, yPos].GetSquareState(color) != 1)
            {
                squaresToMove.Add(board[xPos, yPos]);
            }
            xPos = x + 2;
            yPos = y - 1;
            if (xPos < 8 && yPos >= 0 && board[xPos, yPos].GetSquareState(color) != 1)
            {
                squaresToMove.Add(board[xPos, yPos]);
            }
            xPos = x + 2;
            yPos = y + 1;
            if (xPos < 8 && yPos < 8 && board[xPos, yPos].GetSquareState(color) != 1)
            {
                squaresToMove.Add(board[xPos, yPos]);
            }

        }
    }
}
