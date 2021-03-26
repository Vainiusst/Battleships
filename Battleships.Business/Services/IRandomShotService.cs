using Battleships.Business.Models;
using System;

namespace Battleships.Business.Services
{
    public interface IRandomShotService
    {
        Player Computer { get; set; }
        Random Rand { get; set; }

        Coordinate Shoot();
    }
}