using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DimensionaFormas
{
    public class GuiaLaje// : ElemTransv
    {
        private double altura;
        private double comprimento;
        private double distancia;
        private double espessura;
        private Material material;
        private Concreto concreto;
        private Laje laje;
        private PainelLaje painelLaje;
        double cisalhamento = 0;
        double flexao = 0;
        double flecha = 0;

        // Construtor 1
        public GuiaLaje()
        {
            comprimento = 0;
            distancia = 0;
            espessura = 0;
            altura = 0;

            material = new Material();
            concreto = new Concreto();
            laje = new Laje();
            painelLaje = new PainelLaje();
        }

        // Construtor 2
        public GuiaLaje(double c, double d, double e, double a, Material mat, Concreto conc, Laje laj, PainelLaje pan)
        {
            comprimento = c;
            distancia = d;
            espessura = e;
            altura = a;

            material = mat;
            concreto = conc;
            laje = laj;
            painelLaje = pan;
        }

        // Construtor 3
        public GuiaLaje(GuiaLaje gl)
        {
            this.comprimento = gl.comprimento;
            this.distancia = gl.distancia;
            this.espessura = gl.espessura;
            this.altura = gl.altura;

            this.material = gl.material;
            this.concreto = gl.concreto;
            this.laje = gl.laje;
            this.painelLaje = gl.painelLaje;
        }

        // Metodo Distancia entre Pontaletes
        public double DistanciaPontaletes()
        {
            double d1, d2, d3;

            d1 = Flexao(material.resistenciaCalculoCompressao(), concreto.getDensidade(), laje.getAltura());
            d2 = Flecha(material.moduloElasticidadeEfetivo(), concreto.getDensidade(), laje.getAltura());
            d3 = Cisalhamento(material.resistenciaCalculoCisalhamento(), concreto.getDensidade(), laje.getAltura());

            return Math.Min(Math.Min(d2, d3), d1);
        }

        // Metodo Tensões Normais
        public double Flexao(double resistenciaCompressao, double densidade, double alturaLaje)
        {
            flexao = Math.Sqrt((resistenciaCompressao * espessura * Math.Pow(altura, 2.00)) /
                                        (1.08 * densidade * alturaLaje * Convert.ToInt32(painelLaje.DistanciaGuias())));

            return flexao;
        }

        // Metodo Flecha
        public double Flecha(double moduloElasticidade, double densidade, double alturaLaje)
        {
            flecha = Math.Pow((16 * moduloElasticidade * espessura * Math.Pow(altura, 3.00)) /
                                        (1275.00 * densidade * alturaLaje * Convert.ToInt32(painelLaje.DistanciaGuias())), 1.0 / 3.0);

            return flecha;
        }

        // Metodo Cisalhamento
        public double Cisalhamento(double resistenciaCisalhamento, double densidade, double alturaLaje)
        {
            cisalhamento = (resistenciaCisalhamento * espessura * altura) /
                                (1.08 * densidade * alturaLaje * Convert.ToInt32(painelLaje.DistanciaGuias()));

            return cisalhamento;
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

        public double getAltura()
        {
            return altura;
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

        // Metodos set
        public void setComprimento(double compri)
        {
            comprimento = compri;
        }

        public void setEspessura(double esp)
        {
            espessura = esp;
        }

        public void setAltura(double alt)
        {
            altura = alt;
        }

        public void setMaterial(Material mat)
        {
            material = mat;
        }

        public void setConcreto(Concreto conc)
        {
            concreto = conc;
        }

        public void setLaje(Laje laj)
        {
            laje = laj;
        }

        public void setPainelLaje(PainelLaje pan)
        {
            painelLaje = pan;
        }
    }
}