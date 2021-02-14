using Battleships.Business.Models;
using System;
using System.Collections.Generic;

namespace Battleships.Business.Services
{
    public interface IRandomShotService
    {
        Player Player { get; }
        Random Rand { get; set; }
        List<Coordinate> ShotsTaken { get; set; }

        Coordinate Shoot();
    }
}