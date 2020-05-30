using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DimensionaFormas
{
    public class GravataPilar : Gravata
    {
        private double comprimento;
        private double espessura = 0;
        private double largura = 0;

        private Concreto concreto;
        private Material material;
        private PainelPilar painel;
        private Pilar pilar;

        private double espessura1, espessura2, largura1, largura2, largura3;
        private string tipo1, tipo2, tipo;

        double cisalhamento = 0; double flecha = 0; double flexao = 0;

        // Construtor 1
        public GravataPilar()
        {
            comprimento = 0;
            espessura = 0;
            largura = 0;

            concreto = new Concreto();
            material = new Material();
            painel = new PainelPilar();
            pilar = new Pilar();
        }

        // Construtor 2
        public GravataPilar(GravataPilar gravata)
        {
            this.comprimento = gravata.comprimento;
            this.espessura = gravata.espessura;
            this.largura = gravata.largura;

            this.concreto = gravata.concreto;
            this.material = gravata.material;
            this.painel = gravata.painel;
            this.pilar = gravata.pilar;
        }

        // Construtor 3
        public GravataPilar(double a, double e, double b, Concreto conc, Material mat, PainelPilar pan, Pilar pill)
        {
            comprimento = a;
            espessura = e;
            largura = b;

            concreto = conc;
            material = mat;
            painel = pan;
            pilar = pill;
        }

        // Metodo Dimensoes da Gravata
        public void DimensoesGravata()
        {
            cisalhamento = (3.9 * concreto.getDensidade() * Convert.ToInt32(painel.DistanciaGravatas()) * (pilar.getAltura() - painel.getEspessura() - Convert.ToInt32(painel.DistanciaGravatas()))
                * (comprimento / 2.0)) / (4 * material.resistenciaCalculoCisalhamento());

            largura3 = Math.Pow(cisalhamento, 1.0 / 2.0);

            flexao = 11.7 * concreto.getDensidade() * Convert.ToInt32(painel.DistanciaGravatas()) * (pilar.getAltura() - painel.getEspessura() - Convert.ToInt32(painel.DistanciaGravatas()))
                * Math.Pow((comprimento / 2.0), 2.0) / (16.00 * material.resistenciaCalculoCompressao());

            EscolheDimensaoFlexao(flexao);

            flecha = 56.78 * concreto.getDensidade() * Convert.ToInt32(painel.DistanciaGravatas()) * (pilar.getAltura() - painel.getEspessura() - Convert.ToInt32(painel.DistanciaGravatas()))
                * Math.Pow((comprimento / 2.0), 3.0) / (material.moduloElasticidadeEfetivo());

            EscolheDimensaoFlecha(flecha);
        }

        // Metodo Escolhe Dimensao Final
        public string DimensaoFinal()
        {
            DimensoesGravata();

            if (espessura1 >= espessura2 && largura1 >= largura2 && largura1 >= largura3)
            {
                espessura = espessura1;
                largura = largura1;
                tipo = tipo1;

                return "A melhor opção é " + tipo1 + " de " + Convert.ToString(espessura1) + " x " + Convert.ToString(largura1) + " cm.";
            }
            else
            {
                if (espessura2 >= espessura1 && largura2 >= largura1 && largura2 >= largura3)
                {
                    espessura = espessura2;
                    largura = largura2;
                    tipo = tipo2;

                    return "A melhor opção é " + tipo2 + " de " + Convert.ToString(espessura2) + " x " + Convert.ToString(largura2) + " cm.";
                }
                else
                    return "Escolha outro material.";
            }
        }

        // Metodo de Escolher Dimensao das gravatas por Flexao
        public string EscolheDimensaoFlexao(double valor)
        {
            if (valor <= 180.00)
            {
                espessura1 = 5.00;
                largura1 = 6.00;
                tipo1 = "caibro";

                return "Caibro de 5.00 x 6.00 cm";
            }
            else
            {
                if (valor <= 245.00)
                {
                    espessura1 = 5.00;
                    largura1 = 7.00;
                    tipo1 = "caibro";

                    return "Caibro de 5.00 x 7.00 cm";
                }
                else
                {
                    if (valor <= 281.25)
                    {
                        espessura1 = 5.00;
                        largura1 = 7.50;
                        tipo1 = "caibro";

                        return "Caibro de 5.00 x 7.50 cm";
                    }
                    else
                    {
                        if (valor <= 384.00)
                        {
                            espessura1 = 6.00;
                            largura1 = 8.00;
                            tipo1 = "caibro";

                            return "Caibro de 6.00 x 8.00 cm";
                        }
                        else
                        {
                            if (valor <= 500.00)
                            {
                                espessura1 = 5.00;
                                largura1 = 10.00;
                                tipo1 = "caibro";

                                return "Caibro de 5.00 x 10.00 cm";
                            }
                            else
                            {
                                if (valor <= 864.00)
                                {
                                    espessura1 = 6.00;
                                    largura1 = 12.00;
                                    tipo1 = "vigota";

                                    return "Vigota de 6.00 x 12.00 cm";
                                }
                                else
                                {
                                    if (valor <= 1125.00)
                                    {
                                        espessura1 = 5.00;
                                        largura1 = 15.00;
                                        tipo1 = "vigota";

                                        return "Vigota de 5.00 x 15.00 cm";
                                    }
                                    else
                                    {
                                        if (valor <= 2048.00)
                                        {
                                            espessura1 = 8.00;
                                            largura1 = 16.00;
                                            tipo1 = "barrote";

                                            return "Barrote de 8.00 x 16.00 cm";
                                        }
                                        else
                                        {
                                            return "Escolha outro material.";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        // Metodo de Escolher Dimensao das gravatas por Flecha
        public string EscolheDimensaoFlecha(double valor)
        {
            if (valor <= 1080.00)
            {
                espessura2 = 5.00;
                largura2 = 6.00;
                tipo2 = "caibro";

                return "Caibro de 5.00 x 6.00 cm";
            }
            else
            {
                if (valor <= 1715.00)
                {
                    espessura2 = 5.00;
                    largura2 = 7.00;
                    tipo2 = "caibro";

                    return "Caibro de 5.00 x 7.00 cm";
                }
                else
                {
                    if (valor <= 2109.38)
                    {
                        espessura2 = 5.00;
                        largura2 = 7.50;
                        tipo2 = "caibro";

                        return "Caibro de 5.00 x 7.50 cm";
                    }
                    else
                    {
                        if (valor <= 3072.00)
                        {
                            espessura2 = 6.00;
                            largura2 = 8.00;
                            tipo2 = "caibro";

                            return "Caibro de 6.00 x 8.00 cm";
                        }
                        else
                        {
                            if (valor <= 5000.00)
                            {
                                espessura2 = 5.00;
                                largura2 = 10.00;
                                tipo2 = "caibro";

                                return "Caibro de 5.00 x 10.00 cm";
                            }
                            else
                            {
                                if (valor <= 10368.00)
                                {
                                    espessura2 = 6.00;
                                    largura2 = 12.00;
                                    tipo2 = "vigota";

                                    return "Vigota de 6.00 x 12.00 cm";
                                }
                                else
                                {
                                    if (valor <= 6875.00)
                                    {
                                        espessura2 = 5.00;
                                        largura2 = 15.00;
                                        tipo2 = "vigota";

                                        return "Vigota de 5.00 x 15.00 cm";
                                    }
                                    else
                                    {
                                        if (valor <= 32768.00)
                                        {
                                            espessura2 = 8.00;
                                            largura2 = 16.00;
                                            tipo2 = "barrote";

                                            return "Barrote de 8.00 x 16.00 cm";
                                        }
                                        else
                                        {
                                            return "Escolha outro material.";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        // Metodos get
        public double getEspessura()
        {
            return espessura;
        }

        public double getComprimento()
        {
            return comprimento;
        }

        public double getLargura()
        {
            return largura;
        }

        public double getFlexao()
        {
            return flexao;
        }

        public double getFlecha()
        {
            return flecha;
        }

        public double getCisalhamento()
        {
            return cisalhamento;
        }

        public string getTipo()
        {
            return tipo;
        }

        // Metodos set
        public void setComprimento(double compri)
        {
            comprimento = compri;
        }

        public void setEspessura(double esp)
        {
            espessura = esp;
        }

        public void setLargura(double larg)
        {
            largura = larg;
        }

        public void setMaterial(Material mat)
        {
            material = mat;
        }

        public void setConcreto(Concreto conc)
        {
            concreto = conc;
        }

        public void setPilar(Pilar pil)
        {
            pilar = pil;
        }

        public void setPainelPilar(PainelPilar pan)
        {
            painel = pan;
        }
    }
}