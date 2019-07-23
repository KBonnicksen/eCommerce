﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    /// <summary>
    /// Represents a single video game
    /// </summary>
    public class VideoGame
    {
        /// <summary>
        /// Unique identification number for the game
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Full title of the video game
        /// </summary>
        [Required(ErrorMessage = "Title is required")]
        [StringLength(90)]
        public string Title { get; set; }

        /// <summary>
        /// Official ESRB rating
        /// </summary>
        public string Rating { get; set; }

        /// <summary>
        /// Game description
        /// </summary>
        public string Description { get; set; }

        [Range(.01, 1000)]
        [DataType(DataType.Currency)]
        //Required by default because it is a double (value type)
        /// <summary>
        /// Retail price in US dollars
        /// </summary>
        public double Price { get; set; }
    }
}
