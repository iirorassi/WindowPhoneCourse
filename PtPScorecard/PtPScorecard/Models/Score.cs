using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtPScorecard.Models
{
    class Score : ModelBase
    {
        private int _roundNo = 0;
        public int RoundNo
        {
            set
            {
                if (value != _roundNo)
                {
                    _roundNo = value;
                }
            }
            get
            {
                return _roundNo;
            }
        }

        private int _roundScore = 0;
        public int RoundScore
        {
            set
            {
                if (value != _roundScore)
                {
                    _roundScore = value;
                    NotifyPropertyChanged("RoundScore");

                }
            }
            get
            {
                return _roundScore;
            }
        }

        private int _player = 0;
        public int Player
        {
            set
            {
                if (value != _player)
                {
                    _player = value;
                }
            }
            get
            {
                return _player;
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}
