using Battleships.Business.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Data.Models
{
    public class DbUsers : User
    {
        [NotMapped]
        public int Rank => Wins - Losses;
    }
}
