using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimensionaFormas
{
    public class PainelPilar : Painel
    {
        private Material material;
        private Concreto concreto;
        private Pilar pilar;
        private double comprimento;
        private double espessura;
        private double largura;
        private double flexao = 0;
        private double flecha = 0;
        private double cisalhamento = 0;

        // Construtor 1
        public PainelPilar()
        {
            comprimento = 0;
            espessura = 0;
            largura = 0;

            concreto = new Concreto();
            material = new Material();
            pilar = new Pilar();
        }

        // Construtor 2
        public PainelPilar(PainelPilar pan)
        {
            this.comprimento = pan.comprimento;
            this.espessura = pan.espessura;
            this.largura = pan.largura;

            this.concreto = pan.concreto;
            this.material = pan.material;
            this.pilar = pan.pilar;
        }

        // Construtor 3
        public PainelPilar(double a, double e, double b, Concreto conc, Material mat, Pilar pill)
        {
            comprimento = a;
            espessura = e;
            largura = b;

            concreto = conc;
            material = mat;
            pilar = pill;
        }

        // Metodo Distancia entre Gravatas
        public double DistanciaGravatas()
        {
            double d1, d2, d3;

            d1 = Flexao(material.resistenciaCalculoCompressao(), concreto.getDensidade(), (pilar.getAltura() - espessura));
            d2 = Flecha(material.moduloElasticidadeEfetivo(), concreto.getDensidade(), (pilar.getAltura() - espessura));
            d3 = Cisalhamento(material.resistenciaCalculoCisalhamento(), concreto.getDensidade(), (pilar.getAltura() - espessura));

            return Math.Min(Math.Min(d2, d3), d1);
        }

        // Metodo Tensoes Normais
        public double Flexao(double resistenciaComprimento, double densidade, double alturaPilar)
        {
            flexao = Math.Sqrt((4 * resistenciaComprimento * 1.0 * Math.Pow(espessura, 2.00)) / (3.90 * densidade * alturaPilar * 1.00));

            return flexao;
        }

        // Metodo Flecha
        public double Flecha(double moduloElasticidade, double densidade, double alturaPilar)
        {
            flecha = Math.Pow((16 * moduloElasticidade * 1.0 * Math.Pow(espessura, 3.00)) / (1275.00 * densidade * alturaPilar * 1.00), 1.0 / 3.0);

            return flecha;
        }

        // Metodo Cisalhamento
        public double Cisalhamento(double resistenciaCisalhamento, double densidade, double alturaPilar)
        {
            cisalhamento = (4 * resistenciaCisalhamento * 1.0 * espessura) / (3.90 * densidade * alturaPilar * 1.00);

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

        public void setPilar(Pilar pil)
        {
            pilar = pil;
        }
    }
}
