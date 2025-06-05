using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text.Json;
using DevelopmentChallenge.Data.Classes;
using Xunit;

namespace DevelopmentChallenge.Data.xUnit
{
    public class FormaGeometricaTests : IDisposable
    {
        private const string JsonFileName = "shapes.json";

        private readonly string _jsonBackup;

        public FormaGeometricaTests()
        {
            string _jsonPath = Path.Combine(Directory.GetCurrentDirectory(), JsonFileName);

            if (!File.Exists(_jsonPath))
            {
                _jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, JsonFileName);

                if (!File.Exists(_jsonPath))
                    throw new FileNotFoundException("JSON file with formulas not found!", JsonFileName);
            }


            _jsonBackup = File.ReadAllText(_jsonPath);

            ////JSON de teste
            //var formas = new List<FormaDefinition>
            //{
            //    new FormaDefinition
            //    {
            //        Nome = "Quadrado",
            //        AreaFormula = "lado * lado",
            //        PerimetroFormula = "4 * lado",
            //        Parametros = new List<string> { "lado" }
            //    },
            //    new FormaDefinition
            //    {
            //        Nome = "Círculo",
            //        AreaFormula = "PI() * raio * raio",
            //        PerimetroFormula = "2 * PI() * raio",
            //        Parametros = new List<string> { "raio" }
            //    }
            //};

            File.WriteAllText(JsonFileName, _jsonBackup);
        }

        [Fact]
        public void CalcularArea_Quadrado_DeveRetornarAreaCorreta()
        {
            var parametros = new Dictionary<string, decimal> { { "lado", 5 } };
            var forma = new FormaGeometrica("Quadrado", parametros);
            var area = forma.CalcularArea();
            Assert.Equal(25, area);
        }

        [Fact]
        public void CalcularPerimetro_Quadrado_DeveRetornarPerimetroCorreto()
        {
            var parametros = new Dictionary<string, decimal> { { "lado", 5 } };
            var forma = new FormaGeometrica("Quadrado", parametros);
            var perimetro = forma.CalcularPerimetro();
            Assert.Equal(20, perimetro);
        }

        [Fact]
        public void CalcularArea_Retangulo_DeveRetornarAreaCorreta()
        {
            var parametros = new Dictionary<string, decimal> { { "base", 5 }, { "altura", 2 } };
            var forma = new FormaGeometrica("Retângulo", parametros);
            var area = forma.CalcularArea();
            Assert.Equal(10, area);
        }

        [Fact]
        public void CalcularPerimetro_Retangulo_DeveRetornarPerimetroCorreto()
        {
            var parametros = new Dictionary<string, decimal> { { "base", 5 }, { "altura", 2 } };
            var forma = new FormaGeometrica("Retângulo", parametros);
            var perimetro = forma.CalcularPerimetro();
            Assert.Equal(14, perimetro);
        }


        [Fact]
        public void CalcularArea_Trapezio_DeveRetornarAreaCorreta()
        {
            var parametros = new Dictionary<string, decimal> { { "baseMaior", 5 }, { "baseMenor", 2 }, { "altura", 2 } };
            var forma = new FormaGeometrica("Trapézio", parametros);
            var area = forma.CalcularArea();
            Assert.Equal(7, area);
        }

        [Fact]
        public void CalcularPerimetro_Trapezio_DeveRetornarPerimetroCorreto()
        {
            var parametros = new Dictionary<string, decimal> { { "baseMaior", 5 }, { "baseMenor", 2 }, { "lado1", 2 }, { "lado2", 2 } };
            var forma = new FormaGeometrica("Trapézio", parametros);
            var perimetro = forma.CalcularPerimetro();
            Assert.Equal(11, perimetro);
        }


        [Fact]
        public void CalcularArea_Circulo_DeveRetornarAreaCorreta()
        {
            var parametros = new Dictionary<string, decimal> { { "raio", 2 } };
            var forma = new FormaGeometrica("Círculo", parametros);
            var area = forma.CalcularArea();
            Assert.Equal((decimal)Math.PI * 4, area, 5);
        }

        [Fact]
        public void CalcularPerimetro_Circulo_DeveRetornarPerimetroCorreto()
        {
            var parametros = new Dictionary<string, decimal> { { "raio", 2 } };
            var forma = new FormaGeometrica("Círculo", parametros);
            var perimetro = forma.CalcularPerimetro();
            Assert.Equal((decimal)(2 * Math.PI * 2), perimetro, 5);
        }


        /// <summary>
        /// Teste em português
        /// </summary>
        [Fact]
        public void Imprimir_ListaVazia_DeveRetornarMensagemVazia()
        {
            var resultado = FormaGeometrica.Imprimir(new List<FormaGeometrica>(), "pt");
            Assert.Contains("Lista de formas vazia!", resultado, StringComparison.OrdinalIgnoreCase);

        }

        /// <summary>
        /// Teste em espanhol
        /// </summary>
        [Fact]
        public void Imprimir_ListaVazia_DeveRetornarMensagemVacia()
        {
            var resultado = FormaGeometrica.Imprimir(new List<FormaGeometrica>(), "es");
            Assert.Contains("Lista vacía de formas!", resultado, StringComparison.OrdinalIgnoreCase);

        }
        /// <summary>
        /// Teste em inglês
        /// </summary>
        [Fact]
        public void Imprimir_ListaVazia_DeveRetornarMensagemEmpty()
        {
            var resultado = FormaGeometrica.Imprimir(new List<FormaGeometrica>(), "en");
            Assert.Contains("Empty list of shapes", resultado, StringComparison.OrdinalIgnoreCase);

        }

     
        /// <summary>
        /// Teste em italiano
        /// </summary>
        [Fact]
        public void Imprimir_ListaVazia_DeveRetornarMensagemVuoto()
        {
            var resultado = FormaGeometrica.Imprimir(new List<FormaGeometrica>(), "it");
            Assert.Contains("Elenco vuoto di forme!", resultado, StringComparison.OrdinalIgnoreCase);

        }

        /// <summary>
        /// Teste em português
        /// </summary>
        [Fact]
        public void Imprimir_ListaComFormas_DeveRetornarRelatorio()
        {
            var formas = new List<FormaGeometrica>
            {
                new FormaGeometrica("Quadrado", new Dictionary<string, decimal> { { "lado", 2 } }),
                new FormaGeometrica("Círculo", new Dictionary<string, decimal> { { "raio", 1 } })

            };

            var resultado = FormaGeometrica.Imprimir(formas, "pt");
            Assert.Contains("Relatório de Formas", resultado);
            Assert.Contains("Quadrado", resultado);
            Assert.Contains("Círculo", resultado);
            Assert.Contains("Área", resultado);
            Assert.Contains("Perímetro", resultado);
        }

        /// <summary>
        /// Teste em espanhol
        /// </summary>
        [Fact]
        public void Imprimir_ListaComFormas_DeveRetornarReporte()
        {
            var formas = new List<FormaGeometrica>
            {
                new FormaGeometrica("Quadrado", new Dictionary<string, decimal> { { "lado", 2 } }),
                new FormaGeometrica("Círculo", new Dictionary<string, decimal> { { "raio", 1 } })
            };
            var resultado = FormaGeometrica.Imprimir(formas, "es");
            Assert.Contains("Reporte de Formas", resultado);
            Assert.Contains("Cuadrado", resultado);
            Assert.Contains("Círculo", resultado);
            Assert.Contains("Area", resultado);
            Assert.Contains("Perimetro", resultado);
        }

        /// <summary>
        /// Teste em inglês
        /// </summary>
        [Fact]
        public void Imprimir_ListaComFormas_DeveRetornarReport()
        {
            var formas = new List<FormaGeometrica>
            {
                new FormaGeometrica("Quadrado", new Dictionary<string, decimal> { { "lado", 2 } }),
                new FormaGeometrica("Círculo", new Dictionary<string, decimal> { { "raio", 1 } })
            };
            var resultado = FormaGeometrica.Imprimir(formas, "en");
            Assert.Contains("Shapes report", resultado);
            Assert.Contains("Square", resultado);
            Assert.Contains("Circle", resultado);
            Assert.Contains("Area", resultado);
            Assert.Contains("Perimeter", resultado);
        }


        /// <summary>
        /// Teste em italiano
        /// </summary>
        [Fact]
        public void Imprimir_ListaComFormas_DeveRetornarReportforme()
        {
            var formas = new List<FormaGeometrica>
            {
                new FormaGeometrica("Quadrado", new Dictionary<string, decimal> { { "lado", 2 } }),
                new FormaGeometrica("Círculo", new Dictionary<string, decimal> { { "raio", 1 } })
            };
            var resultado = FormaGeometrica.Imprimir(formas, "it");
            Assert.Contains("Report forme", resultado);
            Assert.Contains("Quadrato", resultado);
            Assert.Contains("Cerchio", resultado);
            Assert.Contains("Area", resultado);
            Assert.Contains("Perimetro", resultado);
        }


        public void Dispose()
        {
            // Restaura JSON
            if (_jsonBackup != null)
                File.WriteAllText(JsonFileName, _jsonBackup);
            else if (File.Exists(JsonFileName))
                File.Delete(JsonFileName);
        }
    }
}