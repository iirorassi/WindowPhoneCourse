using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PtPScorecard.Models;
using Windows.Storage;
using Windows.Storage.Streams;

namespace PtPScorecard.ViewModel
{
    class MatchViewModel
    {
        //By default use 10 rounds
        private int _noOfRounds = 10;
        private Match _saveMatch;

        //Lists for each players Scores 

        //PLAYER 1
        private List<Score> _P1Scores = null;
        public List<Score> P1Scores {
            get {
                if (_P1Scores == null){
                    _P1Scores = new List<Score>();
                    LoadP1Scores();
                }
                return _P1Scores;
            }
        }
        //Setup for initial list
        private void LoadP1Scores()
        {
            if (_P1Scores.Count > 0)
            {
                _P1Scores.Clear();
            }
            for (int i = 1; i <= _noOfRounds; i++)
            {
                Score s = new Score();
                s.RoundNo = i;
                s.RoundScore = 0;
                s.Player = 1;
                _P1Scores.Add(s);
            }
        }

        //PLAYER 2
        private List<Score> _P2Scores = null;
        public List<Score> P2Scores
        {
            get
            {
                if (_P2Scores == null)
                {
                    _P2Scores = new List<Score>();
                    LoadP2Scores();
                }
                return _P2Scores;
            }
        }
        //Setup for initial list
        private void LoadP2Scores()
        {
            if (_P2Scores.Count > 0)
            {
                _P2Scores.Clear();
            }
            for (int i = 1; i <= _noOfRounds; i++)
            {
                Score s = new Score();
                s.RoundNo = i;
                s.RoundScore = 0;
                s.Player = 2;
                _P2Scores.Add(s);
            }
        }

        //PLAYER 3
        private List<Score> _P3Scores = null;
        public List<Score> P3Scores
        {
            get
            {
                if (_P3Scores == null)
                {
                    _P3Scores = new List<Score>();
                    LoadP3Scores();
                }
                return _P3Scores;
            }
        }
        //Setup for initial list
        private void LoadP3Scores()
        {
            if (_P3Scores.Count > 0)
            {
                _P3Scores.Clear();
            }
            for (int i = 1; i <= _noOfRounds; i++)
            {
                Score s = new Score();
                s.RoundNo = i;
                s.RoundScore = 0;
                s.Player = 3;
                _P3Scores.Add(s);
            }
        }

        //PLAYER 4
        private List<Score> _P4Scores = null;
        public List<Score> P4Scores
        {
            get
            {
                if (_P4Scores == null)
                {
                    _P4Scores = new List<Score>();
                    LoadP4Scores();
                }
                return _P4Scores;
            }
        }
        //Setup for initial list
        private void LoadP4Scores()
        {
            if (_P4Scores.Count > 0)
            {
                _P4Scores.Clear();
            }
            for (int i = 1; i <= _noOfRounds; i++)
            {
                Score s = new Score();
                s.RoundNo = i;
                s.RoundScore = 0;
                s.Player = 4;
                _P4Scores.Add(s);
            }
        }

        public void SetRounds (int rounds){
            _noOfRounds = rounds;
        }

        //Method for saving Match and Score data

        public async void SaveMatch(Match m, string Winner)
        {
 
            if (m != null)
            {
                _saveMatch = m;
            }
            else
            {
                _saveMatch = new Match();
            } 
            
            string P1 = "";
            foreach (Score s in P1Scores){
                P1 = P1 + s.RoundScore + "|";
            }

            string P2 = "";
            foreach (Score s in P2Scores){
                P2 = P2 + s.RoundScore + "|";
            }

            string P3 = "";
            foreach (Score s in P3Scores){
                P3 = P3 + s.RoundScore + "|";
            }

            string P4 = "";
            foreach (Score s in P4Scores){
                P4 = P4 + s.RoundScore + "|";
            }

            string text = _saveMatch.TimeStamp + "\n" + _saveMatch.OtherInfo + "\n--------------------\n" + _saveMatch.P1Name + ": " + P1
                + "\n" + _saveMatch.P2Name + ": " + P2 + "\n" + _saveMatch.P3Name + ": " + P3 + "\n" + _saveMatch.P4Name + ": " + P4 + "\nWINNER: " + Winner + "\n**********************\n";

            System.Diagnostics.Debug.WriteLine(text);

            await Common.FileHandler.WriteToFile(text, "PtP-SaveFile.txt");

        }

        public async void SaveMatchToTarget(Match m, string Winner, string Target)
        {

            if (m != null)
            {
                _saveMatch = m;
            }
            else
            {
                _saveMatch = new Match();
            }

            string P1 = "";
            foreach (Score s in P1Scores)
            {
                P1 = P1 + s.RoundScore + "|";
            }

            string P2 = "";
            foreach (Score s in P2Scores)
            {
                P2 = P2 + s.RoundScore + "|";
            }

            string P3 = "";
            foreach (Score s in P3Scores)
            {
                P3 = P3 + s.RoundScore + "|";
            }

            string P4 = "";
            foreach (Score s in P4Scores)
            {
                P4 = P4 + s.RoundScore + "|";
            }

            string text = _saveMatch.TimeStamp + "\n" + _saveMatch.OtherInfo + "\n--------------------\n" + _saveMatch.P1Name + ": " + P1
                + "\n" + _saveMatch.P2Name + ": " + P2 + "\n" + _saveMatch.P3Name + ": " + P3 + "\n" + _saveMatch.P4Name + ": " + P4 + "\nWINNER: " + Winner + "\n**********************\n";

            System.Diagnostics.Debug.WriteLine(text);
            switch (Target) { 

                case "onedrive":
                    //await Common.FileHandler.WriteToOnedrive(text);
                    break;
                default:
                    await Common.FileHandler.WriteToFile(text, "PtP-SaveFile.txt");
                    break;
            }
        }

        
    }
        
  }

