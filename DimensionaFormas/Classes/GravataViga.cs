using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DimensionaFormas
{
    public class GravataViga// : Gravata
    {
        private double comprimento;
        private double espessura;
        private double largura;

        private Concreto concreto;
        private Material material;
        private PainelViga painel;
        private Viga viga;

        private double espessura1, espessura2, largura1, largura2, largura3;
        private string tipo1, tipo2, tipo;
        double cisalhamento, flecha, flexao, painelVertical, momento;

        // Construtor 1
        public GravataViga()
        {
            comprimento = 0;
            espessura = 0;
            largura = 0;

            concreto = new Concreto();
            material = new Material();
            painel = new PainelViga();
            viga = new Viga();
        }

        // Construtor 2
        public GravataViga(GravataViga grav)
        {
            this.comprimento = grav.comprimento;
            this.espessura = grav.espessura;
            this.largura = grav.largura;

            this.concreto = grav.concreto;
            this.material = grav.material;
            this.painel = grav.painel;
            this.viga = grav.viga;
        }

        // Construtor 3
        public GravataViga(double a, double e, double b, Concreto conc, Material mat, PainelViga pan, Viga vig)
        {
            comprimento = a;
            espessura = e;
            largura = b;

            concreto = conc;
            material = mat;
            painel = pan;
            viga = vig;
        }

        // Metodo Dimensoes da Gravata
        public void DimensoesGravata()
        {
            cisalhamento = (1.08 * concreto.getDensidade() * viga.getAltura() * Convert.ToInt32(painel.DistanciaGravatas())
                                        * comprimento) / material.resistenciaCalculoCisalhamento();

            largura3 = Math.Pow(cisalhamento, 1.0 / 2.0);

            flexao = 1.08 * concreto.getDensidade() * Convert.ToInt32(painel.DistanciaGravatas()) * viga.getAltura()
                * Math.Pow(comprimento, 2.0) / material.resistenciaCalculoCompressao();
            
            momento = (1.44 * concreto.getDensidade() * viga.getAltura() * Convert.ToInt32(painel.DistanciaGravatas()) * Math.Pow(viga.getAltura(), 2.00)) / 42.67;
            painelVertical = 6 * momento / material.resistenciaCalculoCompressao();

            EscolheDimensaoFlexao(Math.Max(flexao, painelVertical));

            flecha = 56.78 * concreto.getDensidade() * viga.getLargura() * viga.getAltura() * Math.Pow(comprimento, 3.0) / material.moduloElasticidadeEfetivo();

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

        public double getPainelVertical()
        {
            return painelVertical;
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

        public void setViga(Viga vig)
        {
            viga = vig;
        }

        public void setPainelViga(PainelViga pan)
        {
            painel = pan;
        }
    }
}