using System;
using System.Windows.Forms;

namespace NOS_Kriptografija
{
    public partial class HomeWindow : Form
    {
        public HomeWindow()
        {
            InitializeComponent();
        }

        #region Digitalna omotnica

        #region Odaberi buttons - Digitalna Omotnica

        private void bOdaberi1_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = Program.Direktorij
            };
            dialog.ShowDialog();
            tbUlaznaDatotekaOmotnica.Text = dialog.SafeFileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = Program.Direktorij
            };
            dialog.ShowDialog();
            tbJavniKljucPrimatelja.Text = dialog.SafeFileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = Program.Direktorij
            };
            dialog.ShowDialog();
            tbDigitalnaOmotnicaIzrada.Text = dialog.SafeFileName;
        }


        private void button5_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = Program.Direktorij
            };
            dialog.ShowDialog();
            tbDigitalnaOmotnicaOtvaranje.Text = dialog.SafeFileName;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = Program.Direktorij
            };
            dialog.ShowDialog();
            tbTajniKljucPrimatelja.Text = dialog.SafeFileName;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = Program.Direktorij
            };
            dialog.ShowDialog();
            tbSadrzajDigitalneOmotnice.Text = dialog.SafeFileName;
        }

        #endregion

        #region Otvori buttons - Digitalna Omotnica

        private void button11_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.Direktorij + tbUlaznaDatotekaOmotnica.Text);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.Direktorij + tbJavniKljucPrimatelja.Text);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.Direktorij + tbDigitalnaOmotnicaIzrada.Text);
        }



        private void button14_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.Direktorij + tbDigitalnaOmotnicaOtvaranje.Text);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.Direktorij + tbTajniKljucPrimatelja.Text);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.Direktorij + tbSadrzajDigitalneOmotnice.Text);
        }

        #endregion

        #region Izrada - Otvaranje digitalne omotnice

        private void button4_Click(object sender, EventArgs e)
        {
            EncryptionMode mode;
            if (rbECB_Omotnica.Checked)
            {
                mode = EncryptionMode.ECB;
            }
            else if (rbCFB_Omotnica.Checked)
            {
                mode = EncryptionMode.CFB;
            }
            else if (rbOFB_Omotnica.Checked)
            {
                mode = EncryptionMode.OFB;
            }
            else
            {
                mode = EncryptionMode.CBC;
            }

            KeySize keySize;
            if (rbVelicina128.Checked)
            {
                keySize = KeySize._128;
            }
            else if (rbVelicina192.Checked)
            {
                keySize = KeySize._192;
            }
            else
            {
                keySize = KeySize._256;
            }

            var algorithm = rbAES.Checked ? SymetricAlgorithm.AES : SymetricAlgorithm.THREE_DES;
            DigitalEnvelope.CreateDigitalEnvelope(tbUlaznaDatotekaOmotnica.Text, tbJavniKljucPrimatelja.Text, tbDigitalnaOmotnicaIzrada.Text, mode, algorithm, keySize);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            EncryptionMode mode;
            if (rbECB_Omotnica.Checked)
            {
                mode = EncryptionMode.ECB;
            }
            else if (rbCFB_Omotnica.Checked)
            {
                mode = EncryptionMode.CFB;
            }
            else if (rbOFB_Omotnica.Checked)
            {
                mode = EncryptionMode.OFB;
            }
            else
            {
                mode = EncryptionMode.CBC;
            }

            var algorithm = rbAES_2.Checked ? SymetricAlgorithm.AES : SymetricAlgorithm.THREE_DES;
            DigitalEnvelope.OpenDigitalEnvelope(tbDigitalnaOmotnicaOtvaranje.Text, tbTajniKljucPrimatelja.Text, tbSadrzajDigitalneOmotnice.Text, mode, algorithm);
        }


        #endregion

        private void rbAES_CheckedChanged(object sender, EventArgs e)
        {
            if (rb3DES.Checked)
            {
                rbVelicina256.Visible = false;
                if (rbVelicina256.Checked)
                {
                    rbVelicina128.Checked = true;
                }
            }
            else
            {
                rbVelicina256.Visible = true;
            }
        }

        private void rbAES_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb3DES.Checked)
            {
                rbVelicina256.Visible = false;
                if (rbVelicina256.Checked)
                {
                    rbVelicina128.Checked = true;
                }
            }
            else
            {
                rbVelicina256.Visible = true;
            }
        }

        #endregion

        #region Digitalni potpis

        #region Odaberi buttons - Digitalni Potpis

        private void button21_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = Program.Direktorij
            };
            dialog.ShowDialog();
            tbUlaznaDatotekaPotpis.Text = dialog.SafeFileName;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = Program.Direktorij
            };
            dialog.ShowDialog();
            tbTajniKljucPosiljatelja.Text = dialog.SafeFileName;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = Program.Direktorij
            };
            dialog.ShowDialog();
            tbDigitalniPotpisIzrada.Text = dialog.SafeFileName;
        }


        private void button28_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = Program.Direktorij
            };
            dialog.ShowDialog();
            tbDigitalniPotpisProvjera.Text = dialog.SafeFileName;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = Program.Direktorij
            };
            dialog.ShowDialog();
            tbJavniKljucPosiljatelja.Text = dialog.SafeFileName;
        }

        #endregion

        #region Otvori buttons - Digitalni Potpis

        private void button17_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.Direktorij + tbUlaznaDatotekaPotpis.Text);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.Direktorij + tbTajniKljucPosiljatelja.Text);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.Direktorij + tbDigitalniPotpisIzrada.Text);
        }


        private void button24_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.Direktorij + tbDigitalniPotpisProvjera.Text);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.Direktorij + tbJavniKljucPosiljatelja.Text);
        }

        #endregion

        #region Izrada - Provjera digitalnog potpisa

        private void button18_Click(object sender, EventArgs e)
        {
            HashingMode mode;

            if (rbSHA_1_Potpis.Checked)
            {
                mode = HashingMode.SHA_1;
            }
            else if (rbSHA_2_256_Potpis.Checked)
            {
                mode = HashingMode.SHA_2_256;
            }
            else
            {
                mode = HashingMode.SHA_2_512;
            }

            DigitalSignature.CreateDigitalSignature_FromFile(tbUlaznaDatotekaPotpis.Text, tbTajniKljucPosiljatelja.Text, tbDigitalniPotpisIzrada.Text, mode);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            HashingMode mode;

            if (rbSHA_1_Potpis2.Checked)
            {
                mode = HashingMode.SHA_1;
            }
            else if (rbSHA_2_256_Potpis2.Checked)
            {
                mode = HashingMode.SHA_2_256;
            }
            else
            {
                mode = HashingMode.SHA_2_512;
            }
            DigitalSignature.CheckDigitalSignature_FromFile(tbUlaznaDatotekaPotpis.Text, tbDigitalniPotpisProvjera.Text, tbJavniKljucPosiljatelja.Text, tbProvjeraPotpisa, mode);
        }

        #endregion

        private void button22_Click_1(object sender, EventArgs e)
        {
            tbProvjeraPotpisa.Text = "";
        }

        #endregion

        #region Digitalni pečat

        #region Odaberi buttons - Digitalni Pečat

        private void button35_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = Program.Direktorij
            };
            dialog.ShowDialog();
            tbUlaznaDatotekaPecat.Text = dialog.SafeFileName;
        }

        private void button34_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = Program.Direktorij
            };
            dialog.ShowDialog();
            tbJavniKljucPrimatelja_Pecat.Text = dialog.SafeFileName;
        }

        private void button46_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = Program.Direktorij
            };
            dialog.ShowDialog();
            tbTajniKljucPosiljatelja_Pecat.Text = dialog.SafeFileName;
        }

        private void button44_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = Program.Direktorij
            };
            dialog.ShowDialog();
            tbDigitalnaOmotnicaIzrada_Pecat.Text = dialog.SafeFileName;
        }

        private void button33_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = Program.Direktorij
            };
            dialog.ShowDialog();
            tbDigitalniPotpisIzrada_Pecat.Text = dialog.SafeFileName;
        }


        private void button42_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = Program.Direktorij
            };
            dialog.ShowDialog();
            tbDobivenaPoruka.Text = dialog.SafeFileName;
        }

        private void button41_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = Program.Direktorij
            };
            dialog.ShowDialog();
            tbJavniKljucPosiljatelja_Pecat.Text = dialog.SafeFileName;
        }

        private void button48_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = Program.Direktorij
            };
            dialog.ShowDialog();
            tbTajniKljucPrimatelja_Pecat.Text = dialog.SafeFileName;
        }

        private void button50_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = Program.Direktorij
            };
            dialog.ShowDialog();
            tbDigitalnaOmotnicaOtvaranje_Pecat.Text = dialog.SafeFileName;
        }

        private void button52_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = Program.Direktorij
            };
            dialog.ShowDialog();
            tbDigitalniPotpisProvjera_Pecat.Text = dialog.SafeFileName;
        }

        #endregion

        #region Otvori buttons - Digitalni Pečat

        private void button31_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.Direktorij + tbUlaznaDatotekaPecat.Text);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.Direktorij + tbJavniKljucPrimatelja_Pecat.Text);
        }

        private void button45_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.Direktorij + tbTajniKljucPosiljatelja_Pecat.Text);
        }

        private void button43_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.Direktorij + tbDigitalnaOmotnicaIzrada_Pecat.Text);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.Direktorij + tbDigitalniPotpisIzrada_Pecat.Text);
        }


        private void button38_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.Direktorij + tbDobivenaPoruka.Text);
        }

        private void button37_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.Direktorij + tbJavniKljucPosiljatelja_Pecat.Text);
        }

        private void button47_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.Direktorij + tbTajniKljucPrimatelja_Pecat.Text);
        }

        private void button49_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.Direktorij + tbDigitalnaOmotnicaOtvaranje_Pecat.Text);
        }

        private void button51_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.Direktorij + tbDigitalniPotpisProvjera_Pecat.Text);
        }

        #endregion

        #region Izrada - Provjera digitalnog pečata

        private void button32_Click(object sender, EventArgs e)
        {
            EncryptionMode encryptionMode;
            if (rbECB_Pecat.Checked)
            {
                encryptionMode = EncryptionMode.ECB;
            }
            else if (rbCFB_Pecat.Checked)
            {
                encryptionMode = EncryptionMode.CFB;
            }
            else if (rbOFB_Pecat.Checked)
            {
                encryptionMode = EncryptionMode.OFB;
            }
            else
            {
                encryptionMode = EncryptionMode.CBC;
            }

            var hasningMode = HashingMode.SHA_1;

            if (rbSHA_1_Pecat.Checked)
            {
                hasningMode = HashingMode.SHA_1;
            }
            else if (rbSHA_2_256_Pecat.Checked)
            {
                hasningMode = HashingMode.SHA_2_256;
            }
            else if (rbSHA_2_256_Pecat.Checked)
            {
                hasningMode = HashingMode.SHA_2_512;
            }

            KeySize keySize;
            if (rbVelicina128_Pecat.Checked)
            {
                keySize = KeySize._128;
            }
            else if (rbVelicina192_Pecat.Checked)
            {
                keySize = KeySize._192;
            }
            else
            {
                keySize = KeySize._256;
            }

            var algorithm = rbAES_Pecat.Checked ? SymetricAlgorithm.AES : SymetricAlgorithm.THREE_DES;
            DigitalSeal.CreateDigitalSeal(tbUlaznaDatotekaPecat.Text, tbJavniKljucPrimatelja_Pecat.Text, tbTajniKljucPosiljatelja_Pecat.Text, tbDigitalnaOmotnicaIzrada_Pecat.Text, tbDigitalniPotpisIzrada_Pecat.Text, encryptionMode, hasningMode, algorithm, keySize);
        }

        private void button39_Click(object sender, EventArgs e)
        {
            EncryptionMode encryptionMode;
            if (rbECB_Pecat2.Checked)
            {
                encryptionMode = EncryptionMode.ECB;
            }
            else if (rbCFB_Pecat2.Checked)
            {
                encryptionMode = EncryptionMode.CFB;
            }
            else if (rbOFB_Pecat2.Checked)
            {
                encryptionMode = EncryptionMode.OFB;
            }
            else
            {
                encryptionMode = EncryptionMode.CBC;
            }

            var hasningMode = HashingMode.SHA_1;

            if (rbSHA_1_Pecat2.Checked)
            {
                hasningMode = HashingMode.SHA_1;
            }
            else if (rbSHA_2_256_Pecat2.Checked)
            {
                hasningMode = HashingMode.SHA_2_256;
            }
            else if (rbSHA_2_256_Pecat2.Checked)
            {
                hasningMode = HashingMode.SHA_2_512;
            }

            var algorithm = rbAES_Pecat2.Checked ? SymetricAlgorithm.AES : SymetricAlgorithm.THREE_DES;
            DigitalSeal.CheckDigitalSeal(tbDobivenaPoruka.Text, tbJavniKljucPosiljatelja_Pecat.Text, tbTajniKljucPrimatelja_Pecat.Text, tbDigitalnaOmotnicaOtvaranje_Pecat.Text, tbDigitalniPotpisProvjera_Pecat.Text, tbProvjeraPecata, encryptionMode, hasningMode, algorithm);
        }

        #endregion

        private void rbAES_Pecat_CheckedChanged(object sender, EventArgs e)
        {
            if (rb3DES_Pecat.Checked)
            {
                rbVelicina256_Pecat.Visible = false;
                if (rbVelicina256_Pecat.Checked)
                {
                    rbVelicina128_Pecat.Checked = true;
                }
            }
            else
            {
                rbVelicina256_Pecat.Visible = true;
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            tbProvjeraPecata.Text = "";
        }

        #endregion

    }
}
