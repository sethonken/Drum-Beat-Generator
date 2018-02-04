﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace DrumBeatGen {
    public partial class Form1 : Form {
        Pattern pattern;
        bool restart; //used to end play thread when new pattern is created
        System.Timers.Timer timer;

        public Form1() {
            InitializeComponent();
            restart = false;
            timer = new System.Timers.Timer();
            timer.Elapsed += OnTimedEvent;
        }

        private void play() {
            while (pattern.getPlay()) {
                for (int i = 0; i < pattern.getGridSize(); i++) {
                    //set timer
                    timer.Enabled = true;

                    int tick = i / pattern.getTickMultiplier();

                    //check if paused/new pattern generated
                    while (pattern == null || !pattern.getPlay()) {
                        if (restart) {
                            //reset pattern bar location
                            this.panel6.Location = new System.Drawing.Point(26, 376);
                            this.Refresh();
                            return;
                        }
                    }
                    //move pattern bar location
                    this.panel6.Location = new System.Drawing.Point(26 + (int)((i + 1) / (1.0 * pattern.getGridSize()) * 252), 376);
                    this.Refresh();

                    //loop to keep with bpm
                    while (timer.Enabled) {
                        ;
                    }
                }

                //reset pattern bar location
                this.panel6.Location = new System.Drawing.Point(26, 376);
                this.Refresh();
            }
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e) {
            timer.Enabled = false;
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

            //update new timer
            if (pattern != null) {
                updateTimer();
            }
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

                //start beat
                if (restart) {
                    restart = false;
                    System.Threading.ThreadStart work = play;
                    System.Threading.Thread thread = new System.Threading.Thread(work);
                    thread.IsBackground = true;
                    thread.Start();
                }
            }
            else {
                this.button2.Text = "Play";
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            //generate new pattern
            pattern = null;
            pattern = new Pattern(this.comboBox1.Text, Int32.Parse(this.textBox3.Text), Int32.Parse(this.textBox5.Text), this.comboBox2.Text);

            //reset play button
            pattern.setPlay(false);
            this.button2.Text = "Play";
            restart = true;

            //set timer
            updateTimer();

            //draw snares on grid
            List<int> snare = pattern.getSnareList();
            foreach (int hit in snare) {
                PictureBox pictureBox = copyPictureBox(this.pictureBox2);
                pictureBox.Location = new System.Drawing.Point(pictureBox.Location.X + (int)((hit / (pattern.getGridSize() / (1.0 * pattern.getTickMultiplier()))) * 252), pictureBox.Location.Y);
                this.Controls.Add(pictureBox);
                pictureBox.BringToFront();
            }

            //draw kicks on grid
            List<int> kick = pattern.getKickList();
            foreach (int hit in kick) {
                PictureBox pictureBox = copyPictureBox(this.pictureBox3);
                pictureBox.Location = new System.Drawing.Point(pictureBox.Location.X + (int)((hit / (pattern.getGridSize() / (1.0 * pattern.getTickMultiplier()))) * 252), pictureBox.Location.Y);
                this.Controls.Add(pictureBox);
                pictureBox.BringToFront();
            }

            //bring pattern bar to front
            this.panel6.BringToFront();
        }

        private PictureBox copyPictureBox(PictureBox pb) {
            PictureBox pictureBox = new PictureBox();

            pictureBox.BackColor = pb.BackColor;
            pictureBox.Image = pb.Image;
            pictureBox.Location = pb.Location;
            pictureBox.Name = pb.Name;
            pictureBox.Size = pb.Size;
            pictureBox.SizeMode = pb.SizeMode;

            return pictureBox;
        }

        private void updateBPM(object sender, EventArgs e) {
            if (pattern != null) {
                this.pattern.setBPM(this.trackBar1.Value);
            }
        }

        private void updateTimer() {
            timer.Interval = ((8.0 * 60000.0 / pattern.getBPM() / pattern.getGridSize()));
        }
    }
}
