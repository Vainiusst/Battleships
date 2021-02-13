﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Presentation.Models
{
    public static class CoordToLetterDict
    {
        public readonly static Dictionary<int, string> coordToLetter = new Dictionary<int, string>
        {
            { 0, "A" },
            { 1, "B" },
            { 2, "C" },
            { 3, "D" },
            { 4, "E" },
            { 5, "F" },
            { 6, "G" },
            { 7, "H" },
            { 8, "I" },
            { 9, "J" }
        };
    }
}