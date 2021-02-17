﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Battleships.Data.Models
{
    public class DbGame
    {
        [Key]
        public int GameId { get; set; }
        public int UserId { get; set; }
        public DateTime GameTime { get; set; }
        public string GameMoves { get; set; }

        [ForeignKey("UserId")]
        public virtual DbUser User { get; set; }
    }
}
