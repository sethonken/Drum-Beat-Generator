using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrumBeatGen {
    public class Pattern {
        bool play;
        string timeSignature;
        int bpm;
        int complexity;
        string drumKit;

        public Pattern(string timeSignature, int bpm, int complexity, string drumKit) {
            this.play = false;
            this.timeSignature = timeSignature;
            this.bpm = bpm;
            this.complexity = complexity;
            this.drumKit = drumKit;
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
    }
}
