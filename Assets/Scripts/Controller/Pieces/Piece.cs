using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Chess
{
    abstract class Piece
    {
        public Color color = Color.Empty;
        public string symbol;
        public int weight;
        public int moveNumber = 0;
        public List<Square> squaresToMove = new List<Square>();
        public bool kingMovesCheck = false;
        public bool checkKing = false;
        public List<Square> attackingSquares = new List<Square>();
        public List<Square> squaresOnCheckLine = new List<Square>();

        public void SetColor(Color color)
        {
            this.color = color;
        }

        public abstract void FindLegalMoves(Square[,] board, Square currentSquare);

        public List<Square> GetHorizontalSquares(Square[,] board, Square currentsquare)
        {
            List<Square> squares = new List<Square>();
            List<Square> attackingsqares = new List<Square>();
            int x = currentsquare.x;
            int y = currentsquare.y;
            Color currentPieceColor = currentsquare.currentPiece.color;

            for (int i = y; i > -1; i--)
            {
                if (i != y)
                {
                    if(board[x, i].GetSquareState(currentPieceColor) == 1){                     
                        break;
                    }
                    else if(board[x, i].GetSquareState(currentPieceColor) == 2)
                    {
                        if (checkKing == true)
                        {
                            if (board[x, i].currentPiece.symbol == "Q" || board[x, i].currentPiece.symbol == "R")
                            {
                                attackingsqares.Add(board[x, i]);
                                for (int k = 0; k < squares.Count; k++)
                                    squaresOnCheckLine.Add(squares[k]);
                                //squaresOnCheckLine = new List<Square>(squares);
                                break;
                            }
                        }

                        squares.Add(board[x, i]);
                        break;
                    }
                    else 
                    {
                        squares.Add(board[x, i]);
                    }

                    if (kingMovesCheck == true)
                        break;
                }            
            }

            for(int i = y; i < 8; i++)
            {
                if(i != y)
                {
                    if (board[x, i].GetSquareState(currentPieceColor) == 1)
                    {
                        break;
                    }
                    else if (board[x, i].GetSquareState(currentPieceColor) == 2)
                    {
                        if (checkKing == true)
                        {
                            if (board[x, i].currentPiece.symbol == "Q" || board[x, i].currentPiece.symbol == "R")
                            {
                                attackingsqares.Add(board[x, i]);
                                for (int k = 0; k < squares.Count; k++)
                                    squaresOnCheckLine.Add(squares[k]);
                                //squaresOnCheckLine = new List<Square>(squares);
                                break;
                            }
                        }
                        squares.Add(board[x, i]);
                        break;
                    }
                    else
                    {
                        squares.Add(board[x, i]);
                    }

                    if (kingMovesCheck == true)
                        break;
                }
            }

            if (checkKing == true)
                return attackingsqares;
            else
                return squares;
        }

        public List<Square> GetVerticalSquares(Square[,] board, Square currentsquare)
        {
            List<Square> squares = new List<Square>();
            List<Square> attackingsqares = new List<Square>();
            int x = currentsquare.x;
            int y = currentsquare.y;
            Color currentPieceColor = currentsquare.currentPiece.color;

            for (int i = x; i > -1; i--)
            {
                if (i != x)
                {
                    if (board[i, y].GetSquareState(currentPieceColor) == 1)
                    {
                        break;
                    }
                    else if (board[i, y].GetSquareState(currentPieceColor) == 2)
                    {
                        if (checkKing == true)
                        {
                            if (board[i, y].currentPiece.symbol == "Q" || board[i, y].currentPiece.symbol == "R")
                            {
                                attackingsqares.Add(board[i, y]);
                                for (int k = 0; k < squares.Count; k++)
                                    squaresOnCheckLine.Add(squares[k]);
                                //squaresOnCheckLine = new List<Square>(squares);
                                break;
                            }
                        }

                        squares.Add(board[i, y]);
                        break;
                    }
                    else
                    {
                        squares.Add(board[i, y]);
                    }

                    if (kingMovesCheck == true)
                        break;
                }

            }
            for (int i = x; i < 8; i++)
            {
                if (i != x)
                {
                    if (board[i, y].GetSquareState(currentPieceColor) == 1)
                    {
                        break;
                    }
                    else if (board[i, y].GetSquareState(currentPieceColor) == 2)
                    {
                        if (checkKing == true)
                        {
                            if (board[i, y].currentPiece.symbol == "Q" || board[i, y].currentPiece.symbol == "R")
                            {
                                attackingsqares.Add(board[i, y]);
                                for (int k = 0; k < squares.Count; k++)
                                    squaresOnCheckLine.Add(squares[k]);
                                //squaresOnCheckLine = new List<Square>(squares);
                                break;
                            }

                        }

                        squares.Add(board[i, y]);
                        break;
                    }
                    else
                    {
                        squares.Add(board[i, y]);
                    }

                    if (kingMovesCheck == true)
                        break;
                }
            }

            if (checkKing == true)
                return attackingsqares;
            else
                return squares;
        }

        public List<Square> GetDiagonalSquares(Square[,] board, Square currentsquare)
        {
            List<Square> squares = new List<Square>();
            List<Square> attackingsqares = new List<Square>();
            int x = currentsquare.x;
            int y = currentsquare.y;
            Color currentPieceColor = currentsquare.currentPiece.color;

            int colIndex = y;
            for (int rowIndex = x; rowIndex > -1; rowIndex--)
            {       
                if (rowIndex != x)
                {
                    if (board[rowIndex, colIndex].GetSquareState(currentPieceColor) == 1)
                    {
                        break;
                    }
                    else if (board[rowIndex, colIndex].GetSquareState(currentPieceColor) == 2)
                    {
                        if (checkKing == true)
                        {
                            if (board[rowIndex, colIndex].currentPiece.symbol == "Q" || board[rowIndex, colIndex].currentPiece.symbol == "B")
                            {
                                attackingsqares.Add(board[rowIndex, colIndex]);
                                for (int k = 0; k < squares.Count; k++)
                                    squaresOnCheckLine.Add(squares[k]);
                                //squaresOnCheckLine = new List<Square>(squares);
                                break;
                            }

                            if (rowIndex + 1 == x && currentPieceColor==Color.White && board[rowIndex, colIndex].currentPiece.symbol == "p")
                                attackingsqares.Add(board[rowIndex, colIndex]);

                        }

                        squares.Add(board[rowIndex, colIndex]);
                        break;
                    }
                    else
                    {
                        squares.Add(board[rowIndex, colIndex]);
                    }

                    if (kingMovesCheck == true)
                        break;
                }
                colIndex--;

                if (colIndex < 0)
                    break;

            }

            colIndex = y;
            for (int rowIndex = x; rowIndex < 8; rowIndex++)
            {
                if (rowIndex != x)
                {
                    if (board[rowIndex, colIndex].GetSquareState(currentPieceColor) == 1)
                    {
                        break;
                    }
                    else if (board[rowIndex, colIndex].GetSquareState(currentPieceColor) == 2)
                    {
                        if (checkKing == true)
                        {
                            if (board[rowIndex, colIndex].currentPiece.symbol == "Q" || board[rowIndex, colIndex].currentPiece.symbol == "B")
                            {
                                attackingsqares.Add(board[rowIndex, colIndex]);
                                for (int k = 0; k < squares.Count; k++)
                                    squaresOnCheckLine.Add(squares[k]);
                                //squaresOnCheckLine = new List<Square>(squares);
                                break;
                            }
                            if (rowIndex - 1 == x && currentPieceColor == Color.Black && board[rowIndex, colIndex].currentPiece.symbol == "p")
                                attackingsqares.Add(board[rowIndex, colIndex]);
                        }

                        squares.Add(board[rowIndex, colIndex]);
                        break;
                    }
                    else
                    {
                        squares.Add(board[rowIndex, colIndex]);
                    }


                    if (kingMovesCheck == true)
                        break;
                }
                colIndex--;

                if (colIndex < 0)
                    break;

            }

            colIndex = y;
            for (int rowIndex = x; rowIndex > -1; rowIndex--)
            {
                if (rowIndex != x)
                {
                    if (board[rowIndex, colIndex].GetSquareState(currentPieceColor) == 1)
                    {
                        break;
                    }
                    else if (board[rowIndex, colIndex].GetSquareState(currentPieceColor) == 2)
                    {
                        if (checkKing == true)
                        {
                            if (board[rowIndex, colIndex].currentPiece.symbol == "Q" || board[rowIndex, colIndex].currentPiece.symbol == "B")
                            {
                                attackingsqares.Add(board[rowIndex, colIndex]);
                                for (int k = 0; k < squares.Count; k++)
                                    squaresOnCheckLine.Add(squares[k]);
                                //squaresOnCheckLine = new List<Square>(squares);
                                break;
                            }
                            if (rowIndex + 1 == x && currentPieceColor == Color.White && board[rowIndex, colIndex].currentPiece.symbol == "p")
                                attackingsqares.Add(board[rowIndex, colIndex]);
                        }

                        squares.Add(board[rowIndex, colIndex]);
                        break;
                    }
                    else
                    {
                        squares.Add(board[rowIndex, colIndex]);
                    }

                    if (kingMovesCheck == true)
                        break;
                }
                colIndex++;

                if (colIndex > 7)
                    break;
            }

            colIndex = y;
            for (int rowIndex = x; rowIndex < 8; rowIndex++)
            {
                if (rowIndex != x)
                {
                    if (board[rowIndex, colIndex].GetSquareState(currentPieceColor) == 1)
                    {
                        break;
                    }
                    else if (board[rowIndex, colIndex].GetSquareState(currentPieceColor) == 2)
                    {
                        if (checkKing == true)
                        {
                            if (board[rowIndex, colIndex].currentPiece.symbol == "Q" || board[rowIndex, colIndex].currentPiece.symbol == "B")
                            {
                                attackingsqares.Add(board[rowIndex, colIndex]);
                                for (int k = 0; k < squares.Count; k++)
                                    squaresOnCheckLine.Add(squares[k]);
                                //squaresOnCheckLine = new List<Square>(squares);
                                break;
                            }
                            if (rowIndex - 1 == x && currentPieceColor == Color.Black && board[rowIndex, colIndex].currentPiece.symbol == "p")
                                attackingsqares.Add(board[rowIndex, colIndex]);
                        }

                        squares.Add(board[rowIndex, colIndex]);
                        break;
                    }
                    else
                    {
                        squares.Add(board[rowIndex, colIndex]);
                    }


                    if (kingMovesCheck == true)
                        break;
                }
                colIndex++;

                if (colIndex > 7)
                    break;
            }

            if (checkKing == true)     
                return attackingsqares;          
            else
                return squares;
        }

        public List<Square> GetLegalMoves()
        {
            return squaresToMove;
        }

        public bool isCheck(Square[,] board, Square currentsquare)
        {
            squaresOnCheckLine.Clear();
            attackingSquares.Clear();
            checkKing = true;
            attackingSquares = GetHorizontalSquares(board, currentsquare);
            attackingSquares.AddRange(GetVerticalSquares(board, currentsquare));
            attackingSquares.AddRange(GetDiagonalSquares(board, currentsquare));
            checkKing = false;

            int x = currentsquare.x;
            int y = currentsquare.y;
            int xPos;
            int yPos;

            xPos = x - 1;
            yPos = y - 2;
            if (xPos >= 0 && yPos >= 0 && board[xPos, yPos].GetSquareState(color) == 2)
            {
                if(board[xPos, yPos].currentPiece.symbol == "N")
                    attackingSquares.Add(board[xPos, yPos]);
            }
            xPos = x - 1;
            yPos = y + 2;
            if (xPos >= 0 && yPos < 8 && board[xPos, yPos].GetSquareState(color) == 2)
            {
                if (board[xPos, yPos].currentPiece.symbol == "N")
                    attackingSquares.Add(board[xPos, yPos]);
            }
            xPos = x - 2;
            yPos = y - 1;
            if (xPos >= 0 && yPos >= 0 && board[xPos, yPos].GetSquareState(color) == 2)
            {
                if (board[xPos, yPos].currentPiece.symbol == "N")
                    attackingSquares.Add(board[xPos, yPos]);
            }
            xPos = x - 2;
            yPos = y + 1;
            if (xPos >= 0 && yPos < 8 && board[xPos, yPos].GetSquareState(color) == 2)
            {
                if (board[xPos, yPos].currentPiece.symbol == "N")
                    attackingSquares.Add(board[xPos, yPos]);
            }



            xPos = x + 1;
            yPos = y - 2;
            if (xPos < 8 && yPos >= 0 && board[xPos, yPos].GetSquareState(color) == 2)
            {
                if (board[xPos, yPos].currentPiece.symbol == "N")
                    attackingSquares.Add(board[xPos, yPos]);
            }
            xPos = x + 1;
            yPos = y + 2;
            if (xPos < 8 && yPos < 8 && board[xPos, yPos].GetSquareState(color) == 2)
            {
                if (board[xPos, yPos].currentPiece.symbol == "N")
                    attackingSquares.Add(board[xPos, yPos]);
            }
            xPos = x + 2;
            yPos = y - 1;
            if (xPos < 8 && yPos >= 0 && board[xPos, yPos].GetSquareState(color) == 2)
            {
                if (board[xPos, yPos].currentPiece.symbol == "N")
                    attackingSquares.Add(board[xPos, yPos]);
            }
            xPos = x + 2;
            yPos = y + 1;
            if (xPos < 8 && yPos < 8 && board[xPos, yPos].GetSquareState(color) == 2)
            {
                if (board[xPos, yPos].currentPiece.symbol == "N")
                    attackingSquares.Add(board[xPos, yPos]);
            }


            List<Square> kingAttackSquares;
            kingMovesCheck = true;
            kingAttackSquares = GetHorizontalSquares(board, currentsquare);
            kingAttackSquares.AddRange(GetVerticalSquares(board, currentsquare));
            kingAttackSquares.AddRange(GetDiagonalSquares(board, currentsquare));
            kingMovesCheck = false;

            foreach(Square square in kingAttackSquares)
            {
                if (square.GetSquareState(currentsquare.currentPiece.color)==2 && square.currentPiece.symbol == "K")
                    attackingSquares.Add(square);
            }


            squaresOnCheckLine.AddRange(attackingSquares);

            if (attackingSquares.Count == 0)
                return false;
            else
                return true;

        }
    }
}
