﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsciiConverter
{
    class AsciiConverter
    {
        private byte ColorAverager(Color color)
        {
            return (byte) ((color.R + color.G + color.B + color.A) / 4);
        }

        private readonly Bitmap Bmp;

        public string AsciiText { get; private set; }

        public AsciiConverter(Bitmap bmp) => Bmp = bmp;

        public Task ConvertToAscii()
        {
            return Task.Run(() =>
            {
                StringBuilder sB = new StringBuilder();

                for (int i = 0; i < Bmp.Height; i++)
                {
                    for (int j = 0; j < Bmp.Width; j++)
                    {
                        Color color = Bmp.GetPixel(j, i);
                        byte grayColor = ColorAverager(color);

                        sB.Append(GetSymbolValue(grayColor));
                    }
                    sB.Append("\n");
                }
                AsciiText = sB.ToString();
                Bmp.Dispose();
            });
        }

        private char GetSymbolValue(byte value)
        {
            char symbolValue = БЕЛЫЙ;

            if (value >= 230)
            {
                symbolValue = БЕЛЫЙ;
            }
            else if (value >= 200)
            {
                symbolValue = ТЕМНЕЕ_БЕЛОГО;
            }
            else if (value >= 180)
            {
                symbolValue = ПОЧТИ_СЕРЫЙ;
            }
            else if (value >= 160)
            {
                symbolValue = СЕРЫЙ;
            }
            else if (value >= 130)
            {
                symbolValue = ТЕМНЕЕ_СЕРОГО;
            }
            else if (value >= 100)
            {
                symbolValue = ТЕМНЕЕ_СЕРОГО2;
            }
            else if (value >= 70)
            {
                symbolValue = СЕРЫЙ_СЕРЫЙ;
            }
            else if (value >= 50)
            {
                symbolValue = ТЁМНО_СЕРЫЙ;
            }
            else
            {
                symbolValue = ЧЁРНЫЙ;
            }

            return symbolValue;
        }

        private const char ЧЁРНЫЙ = '@';
        private const char ТЁМНО_СЕРЫЙ = '#';
        private const char СЕРЫЙ_СЕРЫЙ = '&';
        private const char ТЕМНЕЕ_СЕРОГО2 = '8';
        private const char ТЕМНЕЕ_СЕРОГО = 'o';
        private const char СЕРЫЙ = ':';
        private const char ПОЧТИ_СЕРЫЙ = '*';
        private const char ТЕМНЕЕ_БЕЛОГО = '.';
        private const char БЕЛЫЙ = ' ';
    }
}
