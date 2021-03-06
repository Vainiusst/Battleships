﻿using Battleships.Business.Models;
using System.Collections.Generic;

namespace Battleships.Business.Services
{
    public class CoordinateTranslationService : ICoordinateTranslationService
    {
        //Class to translate Coordinate class into more readable coordinates that are displayed on the grids
        private readonly Dictionary<int, string> coordToLetter = new Dictionary<int, string>
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

        public string Translate(Coordinate coord)
        {
            return $"{coordToLetter[coord.Row]}{coord.Column + 1}";
        }
    }
}
