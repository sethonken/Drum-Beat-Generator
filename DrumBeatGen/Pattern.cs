using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Image credits
 * -------------
 * Kit - http://www.clker.com/clipart-4464.html
 */

namespace DrumBeatGen {
    public class Pattern {
        //values from user input
        bool play;
        string timeSignature;
        int bpm;
        int complexity;
        string drumKit;

        //values for grid size
        int tickMultiplier;
        int measures;
        int noteLength;
        int gridSize;

        //values for note placements
        List<int> snare;
        List<int> kick;

        public Pattern(string timeSignature, int bpm, int complexity, string drumKit) {
            //values from user input
            this.play = false;
            this.timeSignature = timeSignature;
            this.bpm = bpm;
            this.complexity = complexity;
            this.drumKit = drumKit;

            //values for grid size
            tickMultiplier = 1; // divisions of one note
            measures = 2;
            noteLength = 16; // 1/n note length
            gridSize = tickMultiplier * measures * noteLength;

            //values for note placements
            snare = new List<int>();
            kick = new List<int>();
        }

        public bool getPlay() {
            return this.play;
        }

        public void setPlay(bool val) {
            this.play = val;
        }

        public string getTimeSignature() {
            return this.timeSignature;
        }

        public void setTimeSignature(string timeSignature) {
            this.timeSignature = timeSignature;
        }

        public int getBPM() {
            return this.bpm;
        }

        public void setBPM(int bpm) {
            this.bpm = bpm;
        }

        public int getComplexity() {
            return this.complexity;
        }

        public void setComplexity(int complexity) {
            this.complexity = complexity;
        }

        public string getDrumKit() {
            return this.drumKit;
        }

        public void setDrumKit(string drumKit) {
            this.drumKit = drumKit;
        }

        public int getTickMultiplier() {
            return this.tickMultiplier;
        }

        public int getGridSize() {
            return this.gridSize;
        }

        public List<int> getSnareList() {
            return snare;
        }

        public List<int> getKickList() {
            return kick;
        }
    }
}
