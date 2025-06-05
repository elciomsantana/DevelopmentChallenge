using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentChallenge.Data.Classes
{
    public class FormulaEvaluator
    {
        public static decimal Evaluate(string formula, Dictionary<string, decimal> parametros)
        {
            var expr = new Expression(formula);


            // Implementação de Funções Extras
            expr.EvaluateFunction += (name, args) =>
            {
                if (name.Equals("PI", StringComparison.OrdinalIgnoreCase))
                    args.Result = Math.PI;
                else if (name.Equals("sqrt", StringComparison.OrdinalIgnoreCase))
                    args.Result = Math.Sqrt(Convert.ToDouble(args.Parameters[0].Evaluate()));
            };

            foreach (var p in parametros)
                expr.Parameters[p.Key] = p.Value;
                  
            try
            {
                var result = expr.Evaluate();
               

                if (result is decimal d) return d;
                if (result is double db) return Convert.ToDecimal(db);
                if (result is int i) return Convert.ToDecimal(i);
                if (result is long l) return Convert.ToDecimal(l);
                throw new InvalidCastException("O resultado da expressão não pôde ser convertido para decimal.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao avaliar a fórmula '{formula}': {ex.Message}", ex);
            }
        }
    }
}
