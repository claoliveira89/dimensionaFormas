using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DimensionaFormas
{
    public class PainelLaje : Painel
    {
        private Material material;
        private Concreto concreto;
        private Laje laje;
        private double comprimento;
        private double espessura;
        private double largura;
        private double flexao = 0;
        private double flecha = 0;
        private double cisalhamento = 0;

        // Construtor 1
        public PainelLaje()
        {
            comprimento = 0;
            espessura = 0;
            largura = 0;

            concreto = new Concreto();
            material = new Material();
            laje = new Laje();
        }

        // Construtor 2
        public PainelLaje(PainelLaje pan)
        {
            this.comprimento = pan.comprimento;
            this.espessura = pan.espessura;
            this.largura = pan.largura;

            this.material = pan.material;
            this.concreto = pan.concreto;
            this.laje = pan.laje;
        }

        // Construtor 3
        public PainelLaje(double a, double e, double b, Concreto conc, Material mat, Laje laj)
        {
            comprimento = a;
            espessura = e;
            largura = b;

            concreto = conc;
            material = mat;
            laje = laj;
        }

        // Metodo Distancia entre Guias
        public double DistanciaGuias()
        {
            double d1, d2, d3;

            d1 = Flexao(material.resistenciaCalculoCompressao(), concreto.getDensidade(), laje.getAltura());
            d2 = Flecha(material.moduloElasticidadeEfetivo(), concreto.getDensidade(), laje.getAltura());
            d3 = Cisalhamento(material.resistenciaCalculoCisalhamento(), concreto.getDensidade(), laje.getAltura());

            return Math.Min(Math.Min(d2, d3), d1);
        }

        // Metodo Tensoes Normais
        public double Flexao(double resistenciaCompressao, double densidade, double alturaLaje)
        {
            flexao = Math.Sqrt((resistenciaCompressao * 1.00 * Math.Pow(espessura, 2.00))/(1.08 * densidade * alturaLaje * 1.00));

            return flexao;
        }

        // Metodo Flecha
        public double Flecha(double moduloElasticidade, double densidade, double alturaLaje)
        {
            flecha = Math.Pow((16 * moduloElasticidade * 1.00 * Math.Pow(espessura, 3.00)) / (1275.00 * densidade * alturaLaje * 1.00), 1.0/3.0);

            return flecha;
        }

        // Metodo Cisalhamento
        public double Cisalhamento(double resistenciaCisalhamento, double densidade, double alturaLaje)
        {
            cisalhamento = (resistenciaCisalhamento * 1.00 * espessura) / (1.08 * densidade * alturaLaje * 1.00);

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

        public void setLaje(Laje laj)
        {
            laje = laj;
        }
    }
}
