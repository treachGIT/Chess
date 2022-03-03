using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Chess.Pieces
{
    class Queen : Piece
    {
        public Queen()
        {
            symbol = "Q";
            weight = 9;
        }

        public override void FindLegalMoves(Square[,] board, Square currentsquare)
        {
            squaresToMove = GetHorizontalSquares(board,currentsquare);
            squaresToMove.AddRange(GetVerticalSquares(board, currentsquare));
            squaresToMove.AddRange(GetDiagonalSquares(board, currentsquare));
        }
    }
}
