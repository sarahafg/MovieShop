﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Cast
    {
        public int Id { get; set; }

        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(int.MaxValue)]
        public string Gender { get; set; }

        [MaxLength(int.MaxValue)]
        public string TmbdUrl { get; set; }

        [MaxLength(2084)]
        public string ProfilePath { get; set; }
    }
}
