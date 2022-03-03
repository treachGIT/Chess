using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Chess.Pieces
{
    class Bishop : Piece 
    {
        public Bishop()
        {
            symbol = "B";
            weight = 3;
        }

        public override void FindLegalMoves(Square[,] board, Square currentsquare)
        {
            squaresToMove = GetDiagonalSquares(board, currentsquare);
        }
    }
}
