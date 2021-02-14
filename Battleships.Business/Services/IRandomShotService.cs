using Battleships.Business.Models;
using System;

namespace Battleships.Business.Services
{
    public interface IRandomShotService
    {
        Player Player { get; }
        Random Rand { get; set; }

        Coordinate Shoot();
    }
}