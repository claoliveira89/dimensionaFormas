using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimensionaFormas
{
    public class PainelViga : Painel
    {
        private Material material;
        private Concreto concreto;
        private Viga viga;
        private double comprimento;
        private double espessura;
        private double largura;
        private double flexao = 0;
        private double flecha = 0;
        private double cisalhamento = 0;

        // Construtor 1
        public PainelViga()
        {
            comprimento = 0;
            espessura = 0;
            largura = 0;

            concreto = new Concreto();
            material = new Material();
            viga = new Viga();
        }

        // Construtor 2
        public PainelViga(PainelViga pan)
        {
            this.comprimento = pan.comprimento;
            this.espessura = pan.espessura;
            this.largura = pan.largura;

            this.concreto = pan.concreto;
            this.material = pan.material;
            this.viga = pan.viga;
        }

        // Construtor 3
        public PainelViga(double a, double e, double b, Concreto conc, Material mat, Viga vig)
        {
            comprimento = a;
            espessura = e;
            largura = b;

            concreto = conc;
            material = mat;
            viga = vig;
        }

        // Metodo Distancia entre Gravatas
        public double DistanciaGravatas()
        {
            double d1, d2, d3;

            d1 = Flexao(material.resistenciaCalculoCompressao(), concreto.getDensidade(), viga.getAltura());
            d2 = Flecha(material.moduloElasticidadeEfetivo(), concreto.getDensidade(), viga.getAltura());
            d3 = Cisalhamento(material.resistenciaCalculoCisalhamento(), concreto.getDensidade(), viga.getAltura());

            return Math.Min(Math.Min(d1, d3), d2);
        }

        // Metodo Tensoes Normais
        public double Flexao(double resistenciaCompressao, double densidade, double alturaViga)
        {
            flexao = Math.Sqrt((resistenciaCompressao * 1.0 * Math.Pow(espessura, 2.00)) / (1.08 * densidade * alturaViga * 1.00));

            return flexao;
        }

        // Metodo Flecha
        public double Flecha(double moduloElasticidade, double densidade, double alturaViga)
        {
            flecha = Math.Pow((16 * moduloElasticidade * 1.0 * Math.Pow(espessura, 3.00)) / (1275.00 * densidade * alturaViga * 1.00), 1.0 / 3.0);

            return flecha;
        }

        // Metodo Cisalhamento
        public double Cisalhamento(double resistenciaCisalhamento, double densidade, double alturaViga)
        {
            cisalhamento = (resistenciaCisalhamento * 1.0 * espessura) / (1.08 * densidade * alturaViga * 1.00);

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

        public void setViga(Viga vig)
        {
            viga = vig;
        }
    }
}
