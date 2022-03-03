using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Chess.Pieces
{
    class Rook : Piece
    {
        public Rook()
        {
            symbol = "R";
            weight = 5;
        }

        public override void FindLegalMoves(Square[,] board, Square currentsquare)
        {
            squaresToMove = GetHorizontalSquares(board, currentsquare);
            squaresToMove.AddRange(GetVerticalSquares(board, currentsquare));
        }
    }
}
