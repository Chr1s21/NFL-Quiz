using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media; 

namespace NFL_Quiz.Model
{
    public class GuessResult
    {
        public Player Player { get; set; }
        public Brush nameBrush { get; set; }
        public Brush trikotBrush { get; set; }
        public Brush positionBrush { get; set; }
        public Brush teamBrush { get; set; }
        public Brush conferenceBrush { get; set; }
        public Brush divisionBrush { get; set; }
        public string trikotDisplay { get; set; }

    }
}
