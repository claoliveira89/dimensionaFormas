using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace DimensionaFormas
{
    public partial class Form1 : Form
    {
        SqlConnection connection;
        string connectionString;

        Laje laje = new Laje();
        PainelLaje painelLaje = new PainelLaje();
        GuiaLaje guiaLaje = new GuiaLaje();

        Viga viga1 = new Viga();
        PainelViga painelViga1 = new PainelViga();
        GravataViga gravataViga1 = new GravataViga();

        Viga viga2 = new Viga();
        PainelViga painelViga2 = new PainelViga();
        GravataViga gravataViga2 = new GravataViga();

        Pilar pilar = new Pilar();
        PainelPilar painelPilar = new PainelPilar();
        GravataPilar gravataPilar = new GravataPilar();

        Concreto concreto = new Concreto();

        Coeficientes coeficienteMaterialPainel = new Coeficientes();
        Material materialForma = new Material();

        Material materialPont = new Material();
        Pontalete pontaleteLaje = new Pontalete();
        Pontalete pontaleteViga1 = new Pontalete();
        Pontalete pontaleteViga2 = new Pontalete();
        Coeficientes coeficienteMaterialPontalete = new Coeficientes();

        bool btnAplicarEstrutura = false;
        bool btnAplicarPainelDeFormas = false;
        bool btnCalcularMetodos = false;

        public Form1()
        {
            InitializeComponent();

            connectionString = ConfigurationManager.ConnectionStrings["DimensionaFormas.Properties.Settings.FormasConnectionString"].ConnectionString;
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        // Metodos TELA ESTRTURA
        private void btnAplicar1_Click(object sender, EventArgs e)
        {
            if (CamposOkEstrutura())
            {
                laje.setAltura(Convert.ToDouble(txtLajeAlt.Text));
                laje.setComprimento(Convert.ToDouble(txtLajeComp.Text));
                laje.setLargura(Convert.ToDouble(txtLajeLarg.Text));

                viga1.setAltura(Convert.ToDouble(txtViga1Alt.Text));
                viga1.setComprimento(Convert.ToDouble(txtViga1Comp.Text));
                viga1.setLargura(Convert.ToDouble(txtViga1Larg.Text));
                gravataViga1.setComprimento(Math.Round(2 * Convert.ToDouble(txtViga1Larg.Text) / 5.0) * 5);
                gravataViga1.setViga(viga1);

                viga2.setAltura(Convert.ToDouble(txtViga2Alt.Text));
                viga2.setComprimento(Convert.ToDouble(txtViga2Comp.Text));
                viga2.setLargura(Convert.ToDouble(txtViga2Larg.Text));
                gravataViga2.setComprimento(Math.Round(2 * Convert.ToDouble(txtViga2Larg.Text) / 5.0) * 5);
                gravataViga2.setViga(viga2);

                pilar.setAltura(Convert.ToDouble(txtPilarAlt.Text));
                pilar.setMaiorDimensao(Convert.ToDouble(txtPilarMaiorDim.Text));
                pilar.setMenorDimensao(Convert.ToDouble(txtPilarMenorDim.Text));
                gravataPilar.setComprimento(2 * Convert.ToDouble(txtPilarMaiorDim.Text));
                gravataPilar.setPilar(pilar);

                if (chkConcr.Checked)
                {
                    concreto.setDensidade(0.000025);
                }
                else
                {
                    concreto.setDensidade(Convert.ToDouble(txtDensiConcreto.Text));
                }

                painelLaje.setConcreto(concreto);
                guiaLaje.setConcreto(concreto);
                painelPilar.setConcreto(concreto);
                gravataPilar.setConcreto(concreto);
                painelViga1.setConcreto(concreto);
                gravataViga1.setConcreto(concreto);
                painelViga2.setConcreto(concreto);
                gravataViga2.setConcreto(concreto);

                btnAplicarEstrutura = true;
                tabControl1.SelectedTab = tabPage2;
            }
            else
            {
                MessageBox.Show("Preencha todas as informações corretamente!");
            }
        }

        private void chkConcr_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConcr.Checked)
            {
                txtDensiConcreto.Enabled = false;
            }
            else
            {
                txtDensiConcreto.Enabled = true;
            }
        }

        bool CamposOkEstrutura()
        {
           return ((FormatValid(txtLajeAlt.Text) && txtLajeAlt.Text != "") && (FormatValid(txtLajeComp.Text) && txtLajeComp.Text != "")
                && (FormatValid(txtLajeLarg.Text) && txtLajeLarg.Text != "") && (FormatValid(txtViga1Alt.Text) && txtViga1Alt.Text != "")
                && (FormatValid(txtViga1Comp.Text) && txtViga1Comp.Text != "") && (FormatValid(txtViga1Larg.Text) && txtViga1Larg.Text != "")
                && (FormatValid(txtViga2Alt.Text) && txtViga2Alt.Text != "") && (FormatValid(txtViga2Comp.Text) && txtViga2Comp.Text != "")
                && (FormatValid(txtViga2Larg.Text) && txtViga2Larg.Text != "") && (FormatValid(txtPilarAlt.Text) && txtPilarAlt.Text != "")
                && (FormatValid(txtPilarMaiorDim.Text) && txtPilarMaiorDim.Text != "") && (FormatValid(txtPilarMenorDim.Text) && txtPilarMenorDim.Text != "")
                && (chkConcr.Checked || (FormatValid(txtDensiConcreto.Text) && txtDensiConcreto.Text != "")));
        }

        // Metodos TELA PAINEL DAS FORMAS
        private void rdBtnDicot_CheckedChanged(object sender, EventArgs e)
        {
            SetVisibilidade(true);
            dataMaterial.SetBounds(11, 59, 519, 363);

            MostraLista("Dicotiledoneas", dataMaterial);
        }

        private void rdBtnConif_CheckedChanged(object sender, EventArgs e)
        {
            SetVisibilidade(true);
            dataMaterial.SetBounds(11, 59, 519, 363);

            MostraLista("Coniferas", dataMaterial);
        }

        private void rdBtnOutro_CheckedChanged(object sender, EventArgs e)
        {
            SetVisibilidade(false);
            dataMaterial.SetBounds(11, 220, 519, 202);

            MostraLista("Outros", dataMaterial);
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtBNovoNome.Clear();
            txtBNovofc0.Clear();
            txtBNovoft0.Clear();
            txtBNovofv.Clear();
            txtBNovoEc0.Clear();

            btnAddNovo.Enabled = true;
        }

        private void btnLimpar_Click()
        {
            txtBNovoNome.Clear();
            txtBNovofc0.Clear();
            txtBNovoft0.Clear();
            txtBNovofv.Clear();
            txtBNovoEc0.Clear();
        }

        private void btnAplicar2_Click(object sender, EventArgs e)
        {
            if (!btnAplicarEstrutura)
                MessageBox.Show("Aplique as modificações da aba de Estrutura antes de prosseguir.");

            else if (CamposOkPainelFormas())
            {
                coeficienteMaterialPainel.setKmod1(Convert.ToDouble(txtKmod1.Text));
                coeficienteMaterialPainel.setKmod2(Convert.ToDouble(txtKmod2.Text));
                coeficienteMaterialPainel.setKmod3(Convert.ToDouble(txtKmod3.Text));

                materialForma.setCoeficientes(coeficienteMaterialPainel);

                painelLaje.setComprimento(Convert.ToDouble(txtBPainelComp.Text));
                painelLaje.setEspessura(Convert.ToDouble(txtBPainelEspe.Text));
                painelLaje.setLargura(Convert.ToDouble(txtBPainelLarg.Text));
                painelLaje.setMaterial(materialForma);
                painelLaje.setConcreto(concreto);
                painelLaje.setLaje(laje);

                guiaLaje.setComprimento(Convert.ToDouble(txtBPainelComp.Text));
                guiaLaje.setEspessura(Convert.ToDouble(txtBPainelEspe.Text));
                guiaLaje.setAltura(Convert.ToDouble(txtBPainelLarg.Text));
                guiaLaje.setMaterial(materialForma);
                guiaLaje.setConcreto(concreto);
                guiaLaje.setLaje(laje);
                guiaLaje.setPainelLaje(painelLaje);

                painelPilar.setComprimento(Convert.ToDouble(txtBPainelComp.Text));
                painelPilar.setEspessura(Convert.ToDouble(txtBPainelEspe.Text));
                painelPilar.setLargura(Convert.ToDouble(txtBPainelLarg.Text));
                painelPilar.setMaterial(materialForma);
                painelPilar.setConcreto(concreto);
                painelPilar.setPilar(pilar);
                gravataPilar.setMaterial(materialForma);
                gravataPilar.setPainelPilar(painelPilar);

                painelViga1.setComprimento(Convert.ToDouble(txtBPainelComp.Text));
                painelViga1.setEspessura(Convert.ToDouble(txtBPainelEspe.Text));
                painelViga1.setLargura(Convert.ToDouble(txtBPainelLarg.Text));
                painelViga1.setMaterial(materialForma);
                painelViga1.setConcreto(concreto);
                painelViga1.setViga(viga1);
                gravataViga1.setMaterial(materialForma);
                gravataViga1.setPainelViga(painelViga1);

                painelViga2.setComprimento(Convert.ToDouble(txtBPainelComp.Text));
                painelViga2.setEspessura(Convert.ToDouble(txtBPainelEspe.Text));
                painelViga2.setLargura(Convert.ToDouble(txtBPainelLarg.Text));
                painelViga2.setMaterial(materialForma);
                painelViga2.setConcreto(concreto);
                painelViga2.setViga(viga2);
                gravataViga2.setMaterial(materialForma);
                gravataViga2.setPainelViga(painelViga2);

                btnAplicarPainelDeFormas = true;
                tabControl1.SelectedTab = tabPage3;
            }
            else
            {
                MessageBox.Show("Preencha todas as informações corretamente!");
            }
        }

        private void btnAddNovo_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO Outros VALUES (@Nome, @Comp, @Trac, @Cisa, @ModE)";

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();

                command.Parameters.AddWithValue("@Nome", txtBNovoNome.Text.Replace('.', ','));
                command.Parameters.AddWithValue("@Comp", txtBNovofc0.Text.Replace('.', ','));
                command.Parameters.AddWithValue("@Trac", txtBNovoft0.Text.Replace('.', ','));
                command.Parameters.AddWithValue("@Cisa", txtBNovofv.Text.Replace('.', ','));
                command.Parameters.AddWithValue("@ModE", txtBNovoEc0.Text.Replace('.', ','));

                command.ExecuteScalar();

                MostraLista("Outros", dataMaterial);
            }

            btnLimpar_Click();
        }

        private void txtLajeComp_TextChanged(object sender, EventArgs e)
        {
            txtViga1Comp.Text = txtLajeComp.Text;
        }

        private void txtLajeLarg_TextChanged(object sender, EventArgs e)
        {
            txtViga2Comp.Text = txtLajeLarg.Text;
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM Outros WHERE Nome = @Nome";

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();

                command.Parameters.AddWithValue("@Nome", txtBNovoNome.Text);

                command.ExecuteNonQuery();

                MostraLista("Outros", dataMaterial);
            }

            txtBNovoNome.Clear();
        }

        private void dataMaterial_CellContentClick(object sender, DataGridViewCellEventArgs e)

        {
            foreach (DataGridViewRow row in dataMaterial.SelectedRows)
            {
                materialForma.setNome(row.Cells[1].Value.ToString());
                materialForma.setResistenciaCompressao(Convert.ToDouble(row.Cells[2].Value));
                materialForma.setResistenciaTracao(Convert.ToDouble(row.Cells[3].Value));
                materialForma.setResistenciCisalhamento(Convert.ToDouble(row.Cells[4].Value));
                materialForma.setModuloElasticidade(Convert.ToDouble(row.Cells[5].Value));

                if (rdBtnOutro.Checked)
                {
                    txtBNovoNome.Text = row.Cells[1].Value.ToString();
                }
            }
        }

        private void chkCoef_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCoef.Checked)
            {
                txtKmod1.Enabled = false;
                txtKmod2.Enabled = false;
                txtKmod3.Enabled = false;

                txtKmod1.Text = "0,90";
                txtKmod2.Text = "1,00";
                txtKmod3.Text = "0,80";
            }
            else
            {
                txtKmod1.Enabled = true;
                txtKmod2.Enabled = true;
                txtKmod3.Enabled = true;

                txtKmod1.Clear();
                txtKmod2.Clear();
                txtKmod3.Clear();
            }
        }

        bool CamposOkPainelFormas()
        {
            return (materialForma.getResistenciaCompressao() != 0.00 && (chkCoefPont.Checked || ((FormatValid(txtKmod1.Text) && txtKmod1.Text != "")
                && (FormatValid(txtKmod2.Text) && txtKmod2.Text != "") && (FormatValid(txtKmod3.Text) && txtKmod3.Text != "")))
                && (FormatValid(txtBPainelComp.Text) && txtBPainelComp.Text != "") && (FormatValid(txtBPainelEspe.Text)
                && txtBPainelEspe.Text != "") && (FormatValid(txtBPainelLarg.Text) && txtBPainelLarg.Text != "")) ;
        }

        // Metodos TELA PONTALETES
        private void rdBtnDicoPont_CheckedChanged(object sender, EventArgs e)
        {
            MostraLista("Dicotiledoneas", dataPont);
        }

        private void rdBtnConiPont_CheckedChanged(object sender, EventArgs e)
        {
            MostraLista("Coniferas", dataPont);
        }

        private void chkCoefPont_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCoefPont.Checked)
            {
                txtKmod1Pont.Enabled = false;
                txtKmod2Pont.Enabled = false;
                txtKmod3Pont.Enabled = false;

                txtKmod1Pont.Text = "0,90";
                txtKmod2Pont.Text = "1,00";
                txtKmod3Pont.Text = "0,80";
            }
            else
            {
                txtKmod1Pont.Enabled = true;
                txtKmod2Pont.Enabled = true;
                txtKmod3Pont.Enabled = true;

                txtKmod1Pont.Clear();
                txtKmod2Pont.Clear();
                txtKmod3Pont.Clear();
            }
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (!btnAplicarEstrutura)
                MessageBox.Show("Aplique as modificações da aba de Estrutura antes de prosseguir.");
            else if (!btnAplicarPainelDeFormas)
                MessageBox.Show("Aplique as modificações da aba de Painel de Formas antes de prosseguir.");

            else if (CamposOkPontaletes())
            {
                coeficienteMaterialPontalete.setKmod1(Convert.ToDouble(txtKmod1Pont.Text));
                coeficienteMaterialPontalete.setKmod2(Convert.ToDouble(txtKmod2Pont.Text));
                coeficienteMaterialPontalete.setKmod3(Convert.ToDouble(txtKmod3Pont.Text));

                materialPont.setCoeficientes(coeficienteMaterialPontalete);
                pontaleteLaje.setMaterial(materialPont);
                pontaleteViga1.setMaterial(materialPont);
                pontaleteViga2.setMaterial(materialPont);

                if (rdBtnDiam7.Checked)
                {
                    pontaleteLaje.setDiametro(7.00);
                    pontaleteViga1.setDiametro(7.00);
                    pontaleteViga2.setDiametro(7.00);
                } 
                else
                {
                    pontaleteLaje.setDiametro(10.00);
                    pontaleteViga1.setDiametro(10.00);
                    pontaleteViga2.setDiametro(10.00);
                }

                pontaleteLaje.setAltura(pilar.getAltura() + viga1.getAltura() - laje.getAltura() - painelLaje.getEspessura());
                pontaleteLaje.setPainelLaje(painelLaje);
                pontaleteLaje.setConcreto(concreto);
                pontaleteLaje.setLaje(laje);

                pontaleteViga1.setAltura(pilar.getAltura() - painelViga1.getEspessura());
                pontaleteViga1.setPainelViga(painelViga1);
                pontaleteViga1.setConcreto(concreto);
                pontaleteViga1.setViga(viga1);

                pontaleteViga2.setAltura(pilar.getAltura() - painelLaje.getEspessura() + Math.Abs(viga1.getAltura() - viga2.getAltura()));
                pontaleteViga2.setPainelViga(painelViga2);
                pontaleteViga2.setConcreto(concreto);
                pontaleteViga2.setViga(viga2);
                
                FormaLaje();
                FormaPilares();
                FormaViga1();
                FormaViga2();
                PreencheRelatorioObservacoes();


                btnCalcularMetodos = true;
                tabControl1.SelectedTab = tabPage4;
            }
            else
            {
                MessageBox.Show("Preencha todas as informações corretamente!");
            }
        }

        private void dataPont_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in dataPont.SelectedRows)
            {
                materialPont.setNome(row.Cells[1].Value.ToString());
                materialPont.setResistenciaCompressao(Convert.ToDouble(row.Cells[2].Value));
                materialPont.setResistenciaTracao(Convert.ToDouble(row.Cells[3].Value));
                materialPont.setResistenciCisalhamento(Convert.ToDouble(row.Cells[4].Value));
                materialPont.setModuloElasticidade(Convert.ToDouble(row.Cells[5].Value));
            }
        }

        bool CamposOkPontaletes()
        {
            return (materialPont.getResistenciaCompressao() != 0.00 && (chkCoefPont.Checked || ((FormatValid(txtKmod1Pont.Text) && txtKmod1Pont.Text != "")
                && (FormatValid(txtKmod2Pont.Text) && txtKmod2Pont.Text != "") && (FormatValid(txtKmod3Pont.Text) && txtKmod3Pont.Text != "")))
                && (rdBtnDiam7.Checked || rdBtnDiam10.Checked)) ;
        }

        // Metodos TELA LAJE
        private void FormaLaje()
        {
            painelLaje.DistanciaGuias();
            txtBEspGuiaLajeFlexao.Text = Convert.ToString(string.Format("{0:0,0.00}", painelLaje.getFlexao()));
            txtBEspGuiaLajeFlecha.Text = Convert.ToString(string.Format("{0:0,0.00}", painelLaje.getFlecha()));
            txtBEspGuiaLajeCisa.Text = Convert.ToString(string.Format("{0:0,0.00}", painelLaje.getCisalhamento()));
            textBox28.Text = Convert.ToString(string.Format("{0:0,0.00}", Convert.ToInt32(painelLaje.DistanciaGuias())));

            guiaLaje.DistanciaPontaletes();
            txtBEspPontLajeFlexao.Text = Convert.ToString(string.Format("{0:0,0.00}", guiaLaje.getFlexao()));
            txtBEspPontLajeFlecha.Text = Convert.ToString(string.Format("{0:0,0.00}", guiaLaje.getFlecha()));
            txtBEspPontLajeCisa.Text = Convert.ToString(string.Format("{0:0,0.00}", guiaLaje.getCisalhamento()));
            textBox29.Text = Convert.ToString(string.Format("{0:0,0.00}", Convert.ToInt32(guiaLaje.DistanciaPontaletes())));

            txtBEstPontLambida.Text = Convert.ToString(string.Format("{0:0,0.00}", pontaleteLaje.IndiceEsbeltez()));
            lblEstPontLaje.Text = pontaleteLaje.DefineEsbeltez();

            if (pontaleteLaje.DefineEsbeltez() == "A peça é curta.")
            {
                label64.Text = "Nenhuma Verificação é necessária!";
            }
            else
            {
                if (pontaleteLaje.DefineEsbeltez() == "A peça é medianamente esbelta.")
                {
                    label64.Text = pontaleteLaje.MedianamenteEsbelta();
                }
                else
                {
                    if (pontaleteLaje.DefineEsbeltez() == "A peça é esbelta.")
                    {
                        label64.Text = pontaleteLaje.Esbelta();
                    }
                    else
                        label64.Text = "O pontalete ";
                }
            }

            PreencheRelatorioLaje();
        }

        // Metodos TELA PILARES
        private void FormaPilares()
        {
            painelPilar.DistanciaGravatas();
            textBox6.Text = Convert.ToString(string.Format("{0:0,0.00}", painelPilar.getFlexao()));
            textBox5.Text = Convert.ToString(string.Format("{0:0,0.00}", painelPilar.getFlecha()));
            textBox4.Text = Convert.ToString(string.Format("{0:0,0.00}", painelPilar.getCisalhamento()));
            textBox17.Text = Convert.ToString(string.Format("{0:0,0.00}", Convert.ToInt32(painelPilar.DistanciaGravatas())));

            gravataPilar.DimensaoFinal();
            textBox2.Text = Convert.ToString(string.Format("{0:0,0.00}", gravataPilar.getFlexao()));
            textBox3.Text = Convert.ToString(string.Format("{0:0,0.00}", gravataPilar.getFlecha()));
            textBox1.Text = Convert.ToString(string.Format("{0:0,0.00}", gravataPilar.getCisalhamento()));
            label92.Text = gravataPilar.DimensaoFinal();

            PreencheRelatorioPilares();
        }

        // Metodos TELA VIGAS
        private void FormaViga1()
        {
            painelViga1.DistanciaGravatas();
            textBox18.Text = Convert.ToString(string.Format("{0:0,0.00}", painelViga1.getFlexao()));
            textBox15.Text = Convert.ToString(string.Format("{0:0,0.00}", painelViga1.getFlecha()));
            textBox14.Text = Convert.ToString(string.Format("{0:0,0.00}", painelViga1.getCisalhamento()));
            textBox7.Text = Convert.ToString(string.Format("{0:0,0.00}", Convert.ToInt32(painelViga1.DistanciaGravatas())));

            gravataViga1.DimensaoFinal();
            textBox12.Text = Convert.ToString(string.Format("{0:0,0.00}", gravataViga1.getFlexao()));
            textBox13.Text = Convert.ToString(string.Format("{0:0,0.00}", gravataViga1.getPainelVertical()));
            textBox11.Text = Convert.ToString(string.Format("{0:0,0.00}", gravataViga1.getFlecha()));
            textBox10.Text = Convert.ToString(string.Format("{0:0,0.00}", gravataViga1.getCisalhamento()));
            label40.Text = gravataViga1.DimensaoFinal();

            textBox9.Text = Convert.ToString(string.Format("{0:0,0.00}", pontaleteViga1.DistPontaleteViga()));
            textBox8.Text = Convert.ToString(string.Format("{0:0,0.00}", pontaleteViga1.IndiceEsbeltez()));
            label41.Text = pontaleteViga1.DefineEsbeltez();

            if (pontaleteViga1.DefineEsbeltez() == "A peça é curta.")
            {
                label39.Text = "Nenhuma Verificação é necessária!";
            }
            else
            {
                if (pontaleteViga1.DefineEsbeltez() == "A peça é medianamente esbelta.")
                {
                    label39.Text = pontaleteViga1.MedianamenteEsbeltaViga();
                }
                else
                {
                    if (pontaleteViga1.DefineEsbeltez() == "A peça é esbelta.")
                    {
                        label39.Text = pontaleteViga1.EsbeltaViga();
                    }
                    else
                        label39.Text = "O pontalete ";
                }
            }

            PreencheRelatorioViga1();
        }

        private void FormaViga2()
        {
            painelViga2.DistanciaGravatas();
            textBox27.Text = Convert.ToString(string.Format("{0:0,0.00}", painelViga2.getFlexao()));
            textBox26.Text = Convert.ToString(string.Format("{0:0,0.00}", painelViga2.getFlecha()));
            textBox25.Text = Convert.ToString(string.Format("{0:0,0.00}", painelViga2.getCisalhamento()));
            textBox24.Text = Convert.ToString(string.Format("{0:0,0.00}", Convert.ToInt32(painelViga2.DistanciaGravatas())));

            gravataViga2.DimensaoFinal();
            textBox22.Text = Convert.ToString(string.Format("{0:0,0.00}", gravataViga2.getFlexao()));
            textBox23.Text = Convert.ToString(string.Format("{0:0,0.00}", gravataViga2.getPainelVertical()));
            textBox21.Text = Convert.ToString(string.Format("{0:0,0.00}", gravataViga2.getFlecha()));
            textBox20.Text = Convert.ToString(string.Format("{0:0,0.00}", gravataViga2.getCisalhamento()));
            label72.Text = gravataViga2.DimensaoFinal();

            textBox19.Text = Convert.ToString(string.Format("{0:0,0.00}", pontaleteViga2.DistPontaleteViga()));
            textBox16.Text = Convert.ToString(string.Format("{0:0,0.00}", pontaleteViga2.IndiceEsbeltez()));
            label66.Text = pontaleteViga2.DefineEsbeltez();

            if (pontaleteViga2.DefineEsbeltez() == "A peça é curta.")
            {
                label62.Text = "Nenhuma Verificação é necessária!";
            }
            else
            {
                if (pontaleteViga2.DefineEsbeltez() == "A peça é medianamente esbelta.")
                {
                    label62.Text = pontaleteViga2.MedianamenteEsbeltaViga();
                }
                else
                {
                    if (pontaleteViga2.DefineEsbeltez() == "A peça é esbelta.")
                    {
                        label62.Text = pontaleteViga2.EsbeltaViga();
                    }
                    else
                        label62.Text = "O pontalete ";
                }
            }

            PreencheRelatorioViga2();
        }

        // Metodos TELA RELATORIO FINAL
        private void PreencheRelatorioLaje()
        {
            label104.Text = "Quantidade: " + Convert.ToString(Convert.ToInt32((laje.getLargura() * laje.getComprimento()) / (painelLaje.getLargura() * painelLaje.getComprimento()))) + " painéis.";
            label106.Text = "Espaçamento: " + textBox28.Text + " cm.";
            label102.Text = "Quantidade: " + Convert.ToString(2 + Convert.ToInt32(laje.getComprimento() / Convert.ToDouble(textBox28.Text))) + " painéis.";
            label108.Text = "Espaçamento: " + textBox29.Text + " cm.";
            label107.Text = "Quantidade: " + Convert.ToString(1 + Convert.ToInt32(laje.getLargura() / Convert.ToDouble(textBox29.Text))) + " pontaletes.";
        }

        private void PreencheRelatorioPilares()
        {
            label135.Text = "Quantidade: " + Convert.ToString(Convert.ToInt32(4 * (2 * pilar.getAltura() * (pilar.getMaiorDimensao() + pilar.getMenorDimensao()) / (painelPilar.getComprimento() * painelPilar.getLargura())))) + " painéis.";
            label115.Text = "Dimensões: " + Convert.ToString(gravataPilar.getEspessura()) + " x " + Convert.ToString(gravataPilar.getLargura()) + " cm.";
            label113.Text = "Espaçamento: " + textBox17.Text + " cm.";
            label111.Text = "Quantidade: " + Convert.ToString(4 * (1 + Convert.ToInt32(pilar.getAltura() / Convert.ToDouble(textBox17.Text)))) + " peças.";
            label139.Text = "Tipo: " + gravataPilar.getTipo() + ".";
        }

        private void PreencheRelatorioViga1()
        {
            label132.Text = "Quantidade: " + Convert.ToString(Convert.ToInt32((viga1.getLargura() * viga1.getComprimento()) / (painelViga1.getLargura() * painelViga1.getComprimento()))) + " painéis.";
            label112.Text = "Quantidade: " + Convert.ToString(Convert.ToInt32((2 * viga1.getAltura() * viga1.getComprimento()) / (painelViga1.getLargura() * painelViga1.getComprimento()))) + " painéis.";
            label128.Text = "Dimensões: " + Convert.ToString(gravataViga1.getEspessura()) + " x " + Convert.ToString(gravataViga1.getLargura()) + " cm.";
            label127.Text = "Espaçamento: " + textBox7.Text + " cm.";
            label126.Text = "Quantidade: " + Convert.ToString(3 * (1 + Convert.ToInt32(viga1.getComprimento() / Convert.ToDouble(textBox7.Text)))) + " peças.";
            label140.Text = "Tipo: " + gravataViga1.getTipo() + ".";
            label116.Text = "Espaçamento: " + textBox9.Text + " cm.";
            label114.Text = "Quantidade: " + Convert.ToString(2 + Convert.ToInt32(viga1.getComprimento() / Convert.ToDouble(textBox9.Text))) + " pontaletes.";
        }

        private void PreencheRelatorioViga2()
        {
            label124.Text = "Quantidade: " + Convert.ToString(Convert.ToInt32((viga2.getLargura() * viga2.getComprimento()) / (painelViga2.getLargura() * painelViga2.getComprimento()))) + " painéis.";
            label121.Text = "Quantidade: " + Convert.ToString(Convert.ToInt32((2 * viga2.getAltura() * viga2.getComprimento()) / (painelViga2.getLargura() * painelViga2.getComprimento()))) + " painéis.";
            label137.Text = "Dimensões: " + Convert.ToString(gravataViga2.getEspessura()) + " x " + Convert.ToString(gravataViga2.getLargura()) + " cm.";
            label133.Text = "Espaçamento: " + textBox24.Text + " cm.";
            label125.Text = "Quantidade: " + Convert.ToString(3 * (1 + Convert.ToInt32(viga2.getComprimento() / Convert.ToDouble(textBox24.Text)))) + " peças.";
            label141.Text = "Tipo: " + gravataViga2.getTipo() + ".";
            label120.Text = "Espaçamento: " + textBox19.Text + " cm.";
            label118.Text = "Quantidade: " + Convert.ToString(2 + Convert.ToInt32(viga2.getComprimento() / Convert.ToDouble(textBox19.Text))) + " pontaletes.";
        }

        private void PreencheRelatorioObservacoes()
        {
            label138.Text = "- Os painéis laterais e de fundo da laje, pilares e vigas são constituídos de " + materialForma.getNome() + " de dimensões " + txtBPainelLarg.Text + " x " + txtBPainelComp.Text + " cm;\n\n"
                            + "- As gravatas dos pilares e vigas são constituídas de " + materialForma.getNome() + ";\n\n"
                            + "- Os pontaletes da laje e vigas são constituídos de " + materialPont.getNome() + ".\n\n";
        }

        // Metodos GERAIS
        private void MostraLista(string nomeLista, DataGridView dataGrid)
        {
            string query = "SELECT * FROM " + nomeLista;

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                DataTable materialTbl = new DataTable();
                adapter.Fill(materialTbl);

                dataGrid.DataSource = materialTbl;
                dataGrid.Columns[0].Visible = false;

            }
        }

        private void SetVisibilidade(bool valor)
        {

            lblNovoNome.Visible = !valor;
            txtBNovoNome.Visible = !valor;

            lblNovofc0.Visible = !valor;
            txtBNovofc0.Visible = !valor;
            lblUnidade13.Visible = !valor;

            lblNovoft0.Visible = !valor;
            txtBNovoft0.Visible = !valor;
            lblUnidade14.Visible = !valor;

            lblNovofv.Visible = !valor;
            txtBNovofv.Visible = !valor;
            lblUnidade15.Visible = !valor;

            lblNovoEc0.Visible = !valor;
            txtBNovoEc0.Visible = !valor;
            lblUnidade16.Visible = !valor;

            btnAddNovo.Visible = !valor;
            btnDeletar.Visible = !valor;
            btnLimpar.Visible = !valor;

            btnAddNovo.Enabled = true;
            btnLimpar_Click();

        }

        bool FormatValid(string format)
        {
            string allowableLetters = "0123456789,";

            foreach (char c in format)
            {
                if (!allowableLetters.Contains(c.ToString()))
                {
                    return false;
                }
            }

            return true;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (!btnCalcularMetodos)
            {
                MessageBox.Show("Clique no botão Calcular na aba Pontaletes antes de prosseguir.");
                return;
            }

            string[] lines = { label101.Text, "",
                                "--- Fôrmas da Laje ---", label103.Text, label104.Text, "", label105.Text, label106.Text, label102.Text, "", label109.Text, label108.Text, label107.Text,
                                "", "--- Fôrmas dos Pilares ---", label134.Text, label135.Text, "", label117.Text, label115.Text, label113.Text, label111.Text, label139.Text,
                                "", "--- Fôrmas da Viga 1 ---",  label130.Text, label132.Text, "", label110.Text, label112.Text, "", label129.Text, label128.Text, label127.Text, label126.Text, label140.Text, "", label131.Text, label116.Text, label114.Text,
                                "", "--- Fôrmas da Viga 2 ---", label122.Text, label124.Text, "", label119.Text, label121.Text, "", label136.Text, label137.Text, label133.Text, label125.Text, label141.Text, "", label123.Text, label120.Text, label118.Text,
                                "", "--- Observações:", label138.Text
            };
            
            string mydocpath =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            
            using (StreamWriter outputFile = new StreamWriter(mydocpath + @"\RelatorioFinal.txt"))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }
        }
    }
}


