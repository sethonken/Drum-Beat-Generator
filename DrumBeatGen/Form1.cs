using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrumBeatGen {
    public partial class Form1 : Form {
        Pattern pattern;

        public Form1() {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e) {
            this.textBox3.Text = this.trackBar1.Value.ToString();
        }

        private void textBox3_TextChanged(object sender, EventArgs e) {
            int newBPM;
            if (Int32.TryParse(this.textBox3.Text, out newBPM)) {
                if (newBPM < this.trackBar1.Minimum) {
                    newBPM = this.trackBar1.Minimum;
                }
                else if (newBPM > this.trackBar1.Maximum) {
                    newBPM = this.trackBar1.Maximum;
                }
            }
            else { //error in parse
                newBPM = this.trackBar1.Value;
            }

            //update the trackbar to reflect user input BPM
            this.trackBar1.Value = newBPM;
            this.textBox3.Text = this.trackBar1.Value.ToString();
        }

        private void textBox5_TextChanged(object sender, EventArgs e) {
            int newIntensity;
            if (Int32.TryParse(this.textBox5.Text, out newIntensity)) {
                if (newIntensity < this.trackBar2.Minimum) {
                    newIntensity = this.trackBar2.Minimum;
                }
                else if (newIntensity > this.trackBar2.Maximum) {
                    newIntensity = this.trackBar2.Maximum;
                }
            }
            else { //error in parse
                newIntensity = this.trackBar2.Value;
            }

            //update the trackbar to reflect user input BPM
            this.trackBar2.Value = newIntensity;
            this.textBox5.Text = this.trackBar2.Value.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e) {
            this.textBox5.Text = this.trackBar2.Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e) {
            //no pattern made yet, cannot play
            if (pattern == null) {
                return;
            }

            if (pattern.getPlay()) {
                pattern.setPlay(false);
            }
            else {
                pattern.setPlay(true);
            }

            //update button text
            if (pattern.getPlay()) {
                this.button2.Text = "Pause";
            }
            else {
                this.button2.Text = "Play";
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            pattern = null;
            pattern = new Pattern(this.comboBox1.Text, Int32.Parse(this.textBox3.Text), Int32.Parse(this.textBox5.Text), this.comboBox2.Text);

            //reset play button
            pattern.setPlay(false);
            this.button2.Text = "Play";
        }
    }
}
