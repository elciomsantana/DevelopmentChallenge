/******************************************************************************************************************/
/******* ¿Qué pasa si debemos soportar un nuevo idioma para los reportes, o agregar más formas geométricas? *******/
/******************************************************************************************************************/

/*
 * TODO: 
 * Refactorizar la clase para respetar principios de la programación orientada a objetos.
 * Implementar la forma Trapecio/Rectangulo. 
 * Agregar el idioma Italiano (o el deseado) al reporte.
 * Se agradece la inclusión de nuevos tests unitarios para validar el comportamiento de la nueva funcionalidad agregada (los tests deben pasar correctamente al entregar la solución, incluso los actuales.)
 * Una vez finalizado, hay que subir el código a un repo GIT y ofrecernos la URL para que podamos utilizar la nueva versión :).
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.Json;
using NCalc;

namespace DevelopmentChallenge.Data.Classes
{
    public class FormaDefinition
    {
        public string Nome { get; set; }
        public string AreaFormula { get; set; }
        public string PerimetroFormula { get; set; }
        public List<string> Parametros { get; set; }
    }

    public class FormaGeometrica
    {
        public const string FILE_PATH_JSON = "shapes.json";
        public string Nome { get; }

        public Dictionary<string, decimal> Parametros { get; }

        private static Dictionary<string, FormaDefinition> _formas;
       
        private static ResourceManager _resourceManager;

        static FormaGeometrica()
        {
            // Carrega JSON de Fómulas de Formas Geométricas
            var json = File.ReadAllText(FILE_PATH_JSON);
            var lista = JsonSerializer.Deserialize<List<FormaDefinition>>(json);
           
            _formas = lista.ToDictionary(f => f.Nome, f => f);

            // Carrega o resource manager para vários idiomas
            _resourceManager = new ResourceManager("DevelopmentChallenge.Data.Properties.Resources", typeof(FormaGeometrica).Assembly);
        }

        public FormaGeometrica(string nome, Dictionary<string, decimal> parametros)
        {
            Nome = nome;
            Parametros = parametros;
        }


        public decimal CalcularArea()
        {
            var def = _formas[Nome];
            return FormulaEvaluator.Evaluate(def.AreaFormula, Parametros);
        }

        public decimal CalcularPerimetro()
        {
            var def = _formas[Nome];
            return FormulaEvaluator.Evaluate(def.PerimetroFormula, Parametros);
        }

        public static string Imprimir(List<FormaGeometrica> formas, string idioma)
        {
            var sb = new StringBuilder();
            var culture = new CultureInfo(idioma);

            if (!formas.Any())
            {
                sb.Append($"<h1>{_resourceManager.GetString("errorEmptyForms", culture)}</h1>");
            }
            else
            {
                sb.Append($"<h1>{_resourceManager.GetString("outputShapesReport", culture)}</h1>");

                var agrupadas = formas.GroupBy(f => f.Nome);
                decimal totalArea = 0, totalPerimetro = 0;
                int totalFormas = 0;

                foreach (var grupo in agrupadas)
                {
                    var quantidade = grupo.Count();
                    var area = grupo.Sum(f => f.CalcularArea());
                    var perimetro = grupo.Sum(f => f.CalcularPerimetro());
                    totalArea += area;
                    totalPerimetro += perimetro;
                    totalFormas += quantidade;

                    var nomeForma = _resourceManager.GetString(grupo.Key, culture) ?? grupo.Key;
                    sb.Append($"{quantidade} {nomeForma} | {_resourceManager.GetString("outputArea", culture)} {area:#.##} | {_resourceManager.GetString("outputPerimeter", culture)} {perimetro:#.##} <br/>");
                }

                sb.Append($"{_resourceManager.GetString("outputTotal", culture)}:<br/>");
                sb.Append($"{totalFormas} {_resourceManager.GetString("outputShapes", culture)} ");
                sb.Append($"{_resourceManager.GetString("outputPerimeter", culture)} {totalPerimetro:#.##} ");
                sb.Append($"{_resourceManager.GetString("outputArea", culture)} {totalArea:#.##}");
            }

            return sb.ToString();
        }
    }
}