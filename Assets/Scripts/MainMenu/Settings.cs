using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Chess
{
    static class Settings
    {
        public static string botPath;
        public static string piecePath;
        public static int gameLength;
        public static string side;

        public static Byte lightR;
        public static Byte lightG;
        public static Byte lightB;
        public static Byte lightA;

        public static Byte darkR;
        public static Byte darkG;
        public static Byte darkB;
        public static Byte darkA;

        public static Color32 LightColor;
        public static Color32 DarkColor;

        public static Color32 LightBoardTheme1 = new Color32(230, 220, 187, 255);
        public static Color32 DarkBoardTheme1 = new Color32(186, 177, 150, 255);
        public static Color32 LightBoardTheme2 = new Color32(240, 240, 240, 255);
        public static Color32 DarkBoardTheme2 = new Color32(117, 123, 236, 255);
        public static Color32 LightBoardTheme3 = new Color32(240, 240, 240, 255);
        public static Color32 DarkBoardTheme3 = new Color32(202, 167, 132, 255);

        public static void SetBotPath()
        {
            botPath = @"D:\Unity\dsadsa\Chess\Assets\Resources\Stockfish.NET-master\Stockfish.NET\stockfish_12_win_x64\stockfish_20090216_x64.exe";
        }

        public static void SetDefaultTheme()
        {
            botPath = @"D:\Unity\dsadsa\Chess\Assets\Resources\Stockfish.NET-master\Stockfish.NET\stockfish_12_win_x64\stockfish_20090216_x64.exe";
            piecePath = "Pieces1/";
            side = "white";
            gameLength = 30;

            LightColor = new Color32(230, 220, 187, 255);
            DarkColor = new Color32(186, 177, 150, 255);

            lightR = 230;
            lightG = 220;
            lightB = 187;
            lightA = 255;
            darkR = 186;
            darkG = 177;
            darkB = 150;
            darkA = 255;
        }

        public static void SetPieceTheme(int id)
        {
            if (id == 1)
                piecePath = "Pieces1/";
            else if (id == 2)
                piecePath = "Pieces2/";
            else if (id == 3)
                piecePath = "Pieces3/";
        }

        public static void SetBoardTheme(int id)
        {
            if (id == 1)
            {
                LightColor = LightBoardTheme1;
                DarkColor = DarkBoardTheme1;

                lightR = LightColor.r;
                lightG = LightColor.g;
                lightB = LightColor.b;
                lightA = LightColor.a;
                darkR = DarkColor.r;
                darkG = DarkColor.g;
                darkB = DarkColor.b;
                darkA = DarkColor.a;
            }               
            else if (id == 2)
            {
                LightColor = LightBoardTheme2;
                DarkColor = DarkBoardTheme2;

                lightR = LightColor.r;
                lightG = LightColor.g;
                lightB = LightColor.b;
                lightA = LightColor.a;
                darkR = DarkColor.r;
                darkG = DarkColor.g;
                darkB = DarkColor.b;
                darkA = DarkColor.a;
            }         
            else if (id == 3)
            {
                LightColor = LightBoardTheme3;
                DarkColor = DarkBoardTheme3;

                lightR = LightColor.r;
                lightG = LightColor.g;
                lightB = LightColor.b;
                lightA = LightColor.a;
                darkR = DarkColor.r;
                darkG = DarkColor.g;
                darkB = DarkColor.b;
                darkA = DarkColor.a;
            }
        }

        public static void SetSide(string s)
        {
            if(s == "white")
            {
                side = "white";
            }
            else
            {
                side = "black";
            }
        }

        public static void SetGameLength(int l)
        {
            gameLength = l;
        }

        public static void SaveSettings()
        {
            List<string> lines = new List<string>();
            lines.Add(botPath);
            lines.Add(piecePath);
            lines.Add(gameLength.ToString());
            lines.Add(side);
            lines.Add(lightR.ToString());
            lines.Add(lightG.ToString());
            lines.Add(lightB.ToString());
            lines.Add(lightA.ToString());

            lines.Add(darkR.ToString());
            lines.Add(darkG.ToString());
            lines.Add(darkB.ToString());
            lines.Add(darkA.ToString());

            string path = @"Info.txt";
            File.WriteAllLines(path, lines);

            Debug.Log(lines[1]);
        }

        public static void ReadSettings()
        {
            List<string> lines = new List<string>();
            string path = @"Info.txt";
            if (File.Exists(path))
            {
                lines = File.ReadAllLines(path).ToList();
                botPath = lines[0];
                piecePath = lines[1];
                gameLength = Int32.Parse(lines[2]);
                side = lines[3];

                lightR = Byte.Parse(lines[4]);
                lightG = Byte.Parse(lines[5]);
                lightB = Byte.Parse(lines[6]);
                lightA = Byte.Parse(lines[7]);
                darkR = Byte.Parse(lines[8]);
                darkG = Byte.Parse(lines[9]);
                darkB = Byte.Parse(lines[10]);
                darkA = Byte.Parse(lines[11]);

                LightColor = new Color32(lightR, lightG, lightB, lightA);
                DarkColor = new Color32(darkR, darkG, darkB, darkA);
            }
            else
            {
                SetDefaultTheme();
                SaveSettings();
            }

        }

    }

}
