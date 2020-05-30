using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DimensionaFormas
{
    public class Material
    {
        private double moduloElasticidade;
        private string nome;
        private double resistenciaCisalhamento;
        private double resistenciaCompressao;
        private double resistenciaTracao;
        
        private Coeficientes coeficientes;

       // Construtor 1
        public Material()
        {
            moduloElasticidade = 0;
            resistenciaCisalhamento = 0;
            resistenciaCompressao = 0;
            resistenciaTracao = 0;
            nome = "";

            coeficientes = new Coeficientes();
        }

        // Construtor 2
        public Material(string n, double E, double Ci, double Co, double T, Coeficientes coef)
        {
            moduloElasticidade = E;
            resistenciaCisalhamento = Ci;
            resistenciaCompressao = Co;
            resistenciaTracao = T;
            nome = n;

            coeficientes = coef;
        }

        // Construtor 3
        public Material(Material m)
        {
            this.moduloElasticidade = m.moduloElasticidade;
            this.resistenciaCisalhamento = m.resistenciaCisalhamento;
            this.resistenciaCompressao = m.resistenciaCompressao;
            this.resistenciaTracao = m.resistenciaTracao;
            this.nome = m.nome;

            this.coeficientes = m.coeficientes;
        }

        // Metodos
        public double resistenciaCalculoCisalhamento()
        {
            return 0.54 * coeficientes.calculaKmod() * (resistenciaCisalhamento / 10.0) / 1.8;
        }

        public double resistenciaCalculoCompressao()
        {
            return 0.7 * coeficientes.calculaKmod() * (resistenciaCompressao / 10.0) / 1.4;
        }

        public double resistenciaCalculoTracao()
        {
            return 0.7 * coeficientes.calculaKmod() * (resistenciaTracao / 10.0) / 1.8;
        }

        public double moduloElasticidadeEfetivo()
        {
            return coeficientes.calculaKmod() * (moduloElasticidade / 10.0);
        }

        // Metodos set
        public void setModuloElasticidade(double mod)
        {
            moduloElasticidade = mod;
        }

        public void setNome(string name)
        {
            nome = name;
        }

        public void setResistenciCisalhamento(double cisa)
        {
            resistenciaCisalhamento = cisa;
        }

        public void setResistenciaCompressao(double compre)
        {
            resistenciaCompressao = compre;
        }

        public void setResistenciaTracao(double trac)
        {
            resistenciaTracao = trac;
        }

        public void setCoeficientes(Coeficientes coef)
        {
            coeficientes = coef;
        }

        // Metodos get
        public double getModuloElasticidade()
        {
            return moduloElasticidade;
        }

        public string getNome()
        {
            return nome;
        }

        public double getResistenciaCisalhamento()
        {
            return resistenciaCisalhamento;
        }

        public double getResistenciaCompressao()
        {
            return resistenciaCompressao;
        }

        public double getResistenciaTracao()
        {
            return resistenciaTracao;
        }
    }
}