using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimensionaFormas
{
    public class Pontalete
    {
        private double diametro;
        private double altura;
        private Material material;
        private PainelLaje painelLaje;
        private PainelViga painelViga;
        private Concreto concreto;
        private Laje laje;
        private Viga viga;

        // Construtor 1
        public Pontalete()
        {
            diametro = 0;
            altura = 0;

            material = new Material();
            painelLaje = new PainelLaje();
            concreto = new Concreto();
            laje = new Laje();
            viga = new Viga();
            painelViga = new PainelViga();
        }

        // Construtor 2
        public Pontalete(double d, double a, Material mat, GuiaLaje guia, PainelLaje painelLaj, Concreto conc, Laje slab, Viga vig, PainelViga pan)
        {
            diametro = d;
            altura = a;

            material = mat;
            painelLaje = painelLaj;
            concreto = conc;
            laje = slab;
            viga = vig;
            painelViga = pan;
        }

        // Construtor 3
        public Pontalete(Pontalete p)
        {
            this.diametro = p.diametro;
            this.altura = p.altura;

            this.material = p.material;
            this.painelLaje = p.painelLaje;
            this.concreto = p.concreto;
            this.viga = p.viga;
            this.painelViga = p.painelViga;
      
        }

        // Metodo Indice de Esbeltez
        public double IndiceEsbeltez()
        {
            return altura / Math.Pow(MomentoInercia() / Area(), 1.0 / 2.0);   
        }

        // Metodo Define Esbeltez
        public string DefineEsbeltez()
        {
            if (IndiceEsbeltez() < 40.0)
            {
                return "A peça é curta.";
            }
            else
            {
                if (40.0 <= IndiceEsbeltez() && IndiceEsbeltez() <= 80.0)
                {
                    return "A peça é medianamente esbelta.";
                }
                else
                {
                    if (80.0 < IndiceEsbeltez() && IndiceEsbeltez() <= 140.0)
                    {
                        return "A peça é esbelta.";
                    }
                    else
                    {
                        return "Índice de esbeltez muito alto. Deve-se aumentar a seção da peça.";
                    }
                }
            }
        }

        // Metodo Medianamente Esbelta
        public string MedianamenteEsbelta()
        {
            double forcaNormal, tensaoNormal;
            double momento1D, momentoD, tensaoMomento;
            double distanciaCentroide;
            double excentricidadeD, excentricidadeI, excentricidadeA;
            double cargaEuler;

            forcaNormal = 1.44 * concreto.getDensidade() * laje.getAltura() * (laje.getLargura() / 2.00) * Convert.ToInt32(painelLaje.DistanciaGuias());
            tensaoNormal = forcaNormal / Area();
            excentricidadeA = Math.Max(altura / 300.00, diametro / 30.00);
            momento1D = 0;
            cargaEuler = (Math.Pow(Math.PI, 2.00) * material.moduloElasticidadeEfetivo() * MomentoInercia()) / Math.Pow(altura, 2.00);
            excentricidadeI = Math.Max(momento1D / forcaNormal, diametro / 30.00);
            excentricidadeD = (excentricidadeI + excentricidadeA) * (cargaEuler / (cargaEuler - forcaNormal));
            momentoD = forcaNormal * excentricidadeD;
            distanciaCentroide = diametro / 2.00; 
            tensaoMomento = momentoD * distanciaCentroide / MomentoInercia();

            if ((tensaoNormal + tensaoMomento) <= material.resistenciaCalculoCompressao())
            {
                return "O pontalete é estável!";
            }
            else
            {
                return "O pontalete não é estável! Deve-se aumentar a seção do mesmo ou escolher outro material!";
            }
        }

        // Metodo Esbelta
        public string Esbelta()
        {
            double forcaNormal, tensaoNormal, forcaNormalGK, forcaNormalQK;
            double momento1GD, momento1QD, momentoD, tensaoMomento;
            double distanciaCentroide, fluencia;
            double excentricidadeI, excentricidadeA, excentricidadeC, excentricidadeIG;
            double cargaEuler;

            forcaNormal = 1.44 * concreto.getDensidade() * laje.getAltura() * (laje.getLargura() / 2.00) * painelLaje.DistanciaGuias();
            tensaoNormal = forcaNormal / Area();
            cargaEuler = (Math.Pow(Math.PI, 2.00) * material.moduloElasticidadeEfetivo() * MomentoInercia()) / Math.Pow(altura, 2.00);
            momento1GD = 0;
            momento1QD = 0;
            excentricidadeI = Math.Max((momento1QD + momento1GD) / forcaNormal, diametro / 30.00);
            excentricidadeIG = momento1GD / forcaNormal;
            excentricidadeA = altura / 300.00;
            forcaNormalGK = forcaNormal / 1.44;
            forcaNormalQK = 0.10 * forcaNormalGK;
            fluencia = 0.80;

            excentricidadeC = (excentricidadeIG + excentricidadeA) * (Math.Exp((fluencia * (forcaNormalGK + (0.30 + 0.20) * forcaNormalQK)) /
                                                    (cargaEuler - (forcaNormalGK + (0.30 + 0.20)) * forcaNormalQK)) - 1);
            
            momentoD = forcaNormal * (excentricidadeI + excentricidadeA + excentricidadeC) * (cargaEuler / (cargaEuler - forcaNormal));
            distanciaCentroide = diametro / 2.00;
            tensaoMomento = momentoD * distanciaCentroide / MomentoInercia();

            if ((tensaoNormal + tensaoMomento) <= material.resistenciaCalculoCompressao())
            {
                return "O pontalete é estável!";
            }
            else
            {
                return "O pontalete não é estável! Deve-se aumentar a seção do mesmo ou escolher outro material!";
            }
        }

        // Metodo Area
        public double Area()
        {
            return Math.PI * Math.Pow(diametro / 2.0, 2.0);
        }

        // Metodo Momento de Inercia
        public double MomentoInercia()
        {
            return Math.PI * Math.Pow(diametro / 2.0, 4.0) / 4.0;
        }

        // Metodo Distancia entre Pontaletes de Vigas
        public double DistPontaleteViga()
        {
            if (diametro == 7.00)
            {
                if (altura <= 200)
                {
                    return 10.73 / (1.44 * concreto.getDensidade() * viga.getAltura() * viga.getLargura());
                }
                else
                {
                    if (altura <= 210)
                    {
                        return 9.96 / (1.44 * concreto.getDensidade() * viga.getAltura() * viga.getLargura());
                    }
                    else
                    {
                        if (altura <= 220)
                        {
                            return 9.27 / (1.44 * concreto.getDensidade() * viga.getAltura() * viga.getLargura());
                        }
                        else
                            return 0.00;
                    }
                }
            }
            else
            {
                if (diametro == 10.00)
                {
                    if (altura <= 220)
                    {
                        return 31.72 / (1.44 * concreto.getDensidade() * viga.getAltura() * viga.getLargura());
                    }
                    else
                    {
                        if (altura <= 230)
                        {
                            return 29.90 / (1.44 * concreto.getDensidade() * viga.getAltura() * viga.getLargura());
                        }
                        else
                        {
                            if (altura <= 240)
                            {
                                return 28.21 / (1.44 * concreto.getDensidade() * viga.getAltura() * viga.getLargura());
                            }
                            else
                            {
                                if (altura <= 250)
                                {
                                    return 26.64 / (1.44 * concreto.getDensidade() * viga.getAltura() * viga.getLargura());
                                }
                                else
                                {
                                    if (altura <= 260)
                                    {
                                        return 25.17 / (1.44 * concreto.getDensidade() * viga.getAltura() * viga.getLargura());
                                    }
                                    else
                                    {
                                        if (altura <= 270)
                                        {
                                            return 23.83 / (1.44 * concreto.getDensidade() * viga.getAltura() * viga.getLargura());
                                        }
                                        else
                                        {
                                            if (altura <= 280)
                                            {
                                                return 22.57 / (1.44 * concreto.getDensidade() * viga.getAltura() * viga.getLargura());
                                            }
                                            else
                                            {
                                                if (altura <= 290)
                                                {
                                                    return 21.41 / (1.44 * concreto.getDensidade() * viga.getAltura() * viga.getLargura());
                                                }
                                                else
                                                {
                                                    if (altura <= 300)
                                                    {
                                                        return 20.33 / (1.44 * concreto.getDensidade() * viga.getAltura() * viga.getLargura());
                                                    }
                                                    else
                                                        return 0.00;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return 0.00;
            }
        }

        // Metodos get
        public double getAltura()
        {
            return altura;
        }

        public double getDiametro()
        {
            return diametro;
        }

        // Metodos set
        public void setAltura(double alt)
        {
            altura = alt;
        }

        public void setDiametro(double diam)
        {
            diametro = diam;
        }

        public void setMaterial(Material mat)
        {
            material = mat;
        }

        public void setPainelLaje(PainelLaje pan)
        {
            painelLaje = pan;
        }

        public void setConcreto(Concreto conc)
        {
            concreto = conc;
        }

        public void setLaje(Laje laj)
        {
            laje = laj;
        }

        public void setViga(Viga vig)
        {
            viga = vig;
        }

        public void setPainelViga(PainelViga panV)
        {
            painelViga = panV;
        }

        // Metodo Medianamente Esbelta Viga
        public string MedianamenteEsbeltaViga()
        {
            double forcaNormal, tensaoNormal;
            double momento1D, momentoD, tensaoMomento;
            double distanciaCentroide;
            double excentricidadeD, excentricidadeI, excentricidadeA;
            double cargaEuler;

            forcaNormal = 1.44 * concreto.getDensidade() * viga.getAltura() * (viga.getLargura() / 2.00) * Convert.ToInt32(DistPontaleteViga());
            tensaoNormal = forcaNormal / Area();
            excentricidadeA = Math.Max(altura / 300.00, diametro / 30.00);
            momento1D = 0;
            cargaEuler = (Math.Pow(Math.PI, 2.00) * material.moduloElasticidadeEfetivo() * MomentoInercia()) / Math.Pow(altura, 2.00);
            excentricidadeI = Math.Max(momento1D / forcaNormal, diametro / 30.00);
            excentricidadeD = (excentricidadeI + excentricidadeA) * (cargaEuler / (cargaEuler - forcaNormal));
            momentoD = forcaNormal * excentricidadeD;
            distanciaCentroide = diametro / 2.00;
            tensaoMomento = momentoD * distanciaCentroide / MomentoInercia();

            if ((tensaoNormal + tensaoMomento) <= material.resistenciaCalculoCompressao())
            {
                return "O pontalete é estável!";
            }
            else
            {
                return "O pontalete não é estável! Deve-se aumentar a seção do mesmo ou escolher outro material!";
            }
        }

        // Metodo Esbelta
        public string EsbeltaViga()
        {
            double forcaNormal, tensaoNormal, forcaNormalGK, forcaNormalQK;
            double momento1GD, momento1QD, momentoD, tensaoMomento;
            double distanciaCentroide, fluencia;
            double excentricidadeI, excentricidadeA, excentricidadeC, excentricidadeIG;
            double cargaEuler;

            forcaNormal = 1.44 * concreto.getDensidade() * viga.getAltura() * (viga.getLargura() / 2.00) * Convert.ToInt32(DistPontaleteViga());
            tensaoNormal = forcaNormal / Area();
            cargaEuler = (Math.Pow(Math.PI, 2.00) * material.moduloElasticidadeEfetivo() * MomentoInercia()) / Math.Pow(altura, 2.00);
            momento1GD = 0;
            momento1QD = 0;
            excentricidadeI = Math.Max((momento1QD + momento1GD) / forcaNormal, diametro / 30.00);
            excentricidadeIG = momento1GD / forcaNormal;
            excentricidadeA = altura / 300.00;
            forcaNormalGK = forcaNormal / 1.44;
            forcaNormalQK = 0.10 * forcaNormalGK;
            fluencia = 0.80;

            excentricidadeC = (excentricidadeIG + excentricidadeA) * (Math.Exp((fluencia * (forcaNormalGK + (0.30 + 0.20) * forcaNormalQK)) /
                                                    (cargaEuler - (forcaNormalGK + (0.30 + 0.20)) * forcaNormalQK)) - 1);

            momentoD = forcaNormal * (excentricidadeI + excentricidadeA + excentricidadeC) * (cargaEuler / (cargaEuler - forcaNormal));
            distanciaCentroide = diametro / 2.00;
            tensaoMomento = momentoD * distanciaCentroide / MomentoInercia();

            if ((tensaoNormal + tensaoMomento) <= material.resistenciaCalculoCompressao())
            {
                return "O pontalete é estável!";
            }
            else
            {
                return "O pontalete não é estável! Deve-se aumentar a seção do mesmo ou escolher outro material!";
            }
        }
    }
}
