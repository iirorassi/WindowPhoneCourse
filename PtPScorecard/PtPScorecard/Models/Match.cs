using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtPScorecard.Models
{
    class Match
    {


        private string _p1Name = " ";
        public string P1Name
        {
            set
            {
                if (value != _p1Name)
                {
                    _p1Name = value;
                }
            }
            get
            {
                return _p1Name;
            }
        }

        private string _p2Name = " ";
        public string P2Name
        {
            set
            {
                if (value != _p2Name)
                {
                    _p2Name = value;
                }
            }
            get
            {
                return _p2Name;
            }
        }

        private string _p3Name = " ";
        public string P3Name
        {
            set
            {
                if (value != _p3Name)
                {
                    _p3Name = value;
                }
            }
            get
            {
                return _p3Name;
            }
        }

        private string _p4Name = " ";
        public string P4Name
        {
            set
            {
                if (value != _p4Name)
                {
                    _p4Name = value;
                }
            }
            get
            {
                return _p4Name;
            }
        }

        private string _timeStamp = " ";
        public string TimeStamp
        {
            set
            {
                if (value != _timeStamp)
                {
                    _timeStamp = value;
                }
            }
            get
            {
                return _timeStamp;
            }
        }

        private string _otherInfo = " ";
        public string OtherInfo
        {
            set
            {
                if (value != _otherInfo)
                {
                    _otherInfo = value;
                }
            }
            get
            {
                return _otherInfo;
            }
        }

        private int _noOfRounds = 0;
        public int NoOfRounds
        {
            set
            {
                if (value != _noOfRounds)
                {
                    _noOfRounds = value;
                }
            }
            get
            {
                return _noOfRounds;
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}
