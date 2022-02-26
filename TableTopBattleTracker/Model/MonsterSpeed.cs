﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableTopBattleTracker.Model
{
    [Table("monster_speed")]
    public class MonsterSpeed
    {
        [Key, Column(Order =1)]
        public int CharacterId { get; set; }
        
        [Key, Column(Order =2)]
        public ESpeedType SpeedTypeId { get; set; }

        public SpeedType? SpeedType { get; set; }

        public int Value { get; set; }

    }
}