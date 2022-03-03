using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Move
    {
        string pgnNotation;
        public string botNotation;
        static string[,] boardNotation =
        {
            {"a8", "b8", "c8", "d8", "e8", "f8", "g8", "h8"},
            {"a7", "b7", "c7", "d7", "e7", "f7", "g7", "h7"},
            {"a6", "b6", "c6", "d6", "e6", "f6", "g6", "h6"},
            {"a5", "b5", "c5", "d5", "e5", "f5", "g5", "h5"},
            {"a4", "b4", "c4", "d4", "e4", "f4", "g4", "h4"},
            {"a3", "b3", "c3", "d3", "e3", "f3", "g3", "h3"},
            {"a2", "b2", "c2", "d2", "e2", "f2", "g2", "h2"},
            {"a1", "b1", "c1", "d1", "e1", "f1", "g1", "h1"},
        };

        public Move(Square current, Square next)
        {
            string movedPiece = "";
            string first = boardNotation[current.x, current.y];
            string capture = "";
            string second = boardNotation[next.x, next.y];

            switch (current.currentPiece.symbol)
            {
                case "R":
                    movedPiece = "R";
                    break;
                case "N":
                    movedPiece = "N";
                    break;
                case "B":
                    movedPiece = "B";
                    break;
                case "Q":
                    movedPiece = "Q";
                    break;
                case "K":
                    movedPiece = "K";
                    break;
                default:
                    break;
            }

            if (next.currentPiece != null)
                capture = "x";

            pgnNotation = movedPiece + first + capture + second;

            botNotation = first + second;
        }

        public string GetMove()
        {
            return pgnNotation;
        }

        public string GetBotMove()
        {
            return botNotation;
        }

        public static Square GetSquareFromMove(string x, Square[,] board)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                { 
                    if(x == boardNotation[i, j])
                    {
                        return board[i, j];
                    }
                }
            }
            return null;
        }
    }
}
