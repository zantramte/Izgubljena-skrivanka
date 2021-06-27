using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace Izgubljena_skrivanka
{
    public partial class Form1 : Form
    {
        WindowsMediaPlayer Igraj = new WindowsMediaPlayer();

        public void Uredi_zadeve()
        {
            Zima.Uredi_stvari();
            label1.Text = "Kje je: " + Zima.Iskana_crka;

            for (int indeks = 1; indeks < Controls.Count; indeks++)
            {
                if (Controls[indeks] is Button)
                {
                    Zima.Trenutna_cifra = Zima.N.Next(Zima.Crke.Count);
                    Controls[indeks].Text = Zima.Crke[Zima.Trenutna_cifra];
                    Zima.Crke.RemoveAt(Zima.Trenutna_cifra);
                    Controls[indeks].Visible = true;
                    Controls[indeks].ForeColor = Color.RoyalBlue;
                    Controls[indeks].Location = new Point(Zima.X[indeks - 1], Zima.Y[indeks - 1]);
                }
            }
        }

        public Form1()
        {
            InitializeComponent();

            Cursor = new Cursor(Application.StartupPath + "\\Miska\\snezak.ico");

            Obvestilo.ShowBalloonTip(1000, "Snežak sporoča", "ŽIVJO! Pomagaj mi poiskati izgubljene črke!", ToolTipIcon.Info);
            
            foreach (Control Kontrola in Controls)
            {
                if (Kontrola is Button)
                {
                    Zima.X.Add(Kontrola.Location.X);
                    Zima.Y.Add(Kontrola.Location.Y);
                    ControlExtension.Draggable(Kontrola, true);
                }
            }

            Uredi_zadeve();
        }

        private void Iskanje_Click(object sender, EventArgs e)
        {
            Button Moja = (Button)sender;
            Zima.Moja_crka = Moja.Text;

            if (((Moja.Location.Y > 94) && (Moja.Location.Y < 189)) && ((Moja.Location.X > 483) && (Moja.Location.X < 590)))
            {
                Moja.Location = new Point(526, 132);
                Moja.ForeColor = Color.White;

                if (Zima.Preveri_crki())
                {
                    Igraj.URL = "najdeno.wav";
                    MessageBox.Show("HURAAA! Moja črka " + Zima.Iskana_crka + " je najdena!", "Snežak sporoča", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Uredi_zadeve();
                }

                else
                {
                    Zima.Poskusi--;
                    Igraj.URL = "lose.wav";

                    switch (Zima.Poskusi)
                    {
                        case 4:
                        case 3:
                            MessageBox.Show("Ne, črke " + Moja.Text + " ne iščeva! Iščiva naprej, imaš še " + Zima.Poskusi + " poskuse!", "Snežak sporoča", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Moja.Visible = false;
                            break;

                        case 2:
                            MessageBox.Show("Ne, črke " + Moja.Text + " ne iščeva! Iščiva naprej, imaš še " + Zima.Poskusi + " poskusa!", "Snežak sporoča", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Moja.Visible = false;
                            break;

                        case 1:
                            MessageBox.Show("Ne, črke " + Moja.Text + " ne iščeva! Iščiva naprej, imaš še " + Zima.Poskusi + " poskus!", "Snežak sporoča", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Moja.Visible = false;
                            break;

                        default:
                            Igraj.URL = "konec.wav";
                            DialogResult Dialog = MessageBox.Show("KONEC IGRE! Želiš ponovno igro?", "Snežak sprašuje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (Dialog == DialogResult.Yes)
                            {
                                Uredi_zadeve();
                            }

                            else
                            {
                                MessageBox.Show("Se še vidiva! Adijo!", "Snežak sporoča", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Application.Exit();
                            }
                            break;
                    }
                }
            }
        }

        private void Iskanje_MouseEnter(object sender, EventArgs e)
        {
            Igraj.URL = "hover.wav";
            Button Moja = (Button)sender;
            Moja.Size = new Size(Moja.Width + 2, Moja.Height + 2);
        }

        private void Iskanje_MouseLeave(object sender, EventArgs e)
        {
            Button Moja = (Button)sender;
            Moja.Size = new Size(Moja.Width - 2, Moja.Height - 2);
        }
    }
}
