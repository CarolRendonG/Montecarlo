using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Clases
{
    public class MonteCarlo
    {
        public List<ResultadoMonteCarlo> RealizarSimulacion(int totalPaneles, int minPanelesFuncionando, int rangoInferior, int rangoSuperior, int numeroIteraciones)
        {
            List<ResultadoMonteCarlo> resultados = new List<ResultadoMonteCarlo>();
            Random random = new Random();

            for (int n = 0; n < numeroIteraciones; n++)
            {
                List<int> tiemposFalla = new List<int>();

                // Generar tiempos de vida para cada panel
                for (int i = 0; i < totalPaneles; i++)
                {
                    int tiempoVida = random.Next(rangoInferior, rangoSuperior + 1);
                    tiemposFalla.Add(tiempoVida);
                }

                // Ordenar tiempos de falla en orden ascendente
                tiemposFalla.Sort();

                // Calcular el tiempo cuando fallan suficientes paneles para que el satélite no funcione
                int tiempoDeFalla = tiemposFalla[totalPaneles - minPanelesFuncionando];

                // Agregar el resultado del experimento a la lista de resultados
                resultados.Add(new ResultadoMonteCarlo
                {
                    NumeroExperimento = n + 1,
                    TiemposPaneles = tiemposFalla,
                    TiempoEstimadoFalla = tiempoDeFalla
                });
            }

            return resultados;
        }
    }

    public class ResultadoMonteCarlo
    {
        public int NumeroExperimento { get; set; }
        public List<int> TiemposPaneles { get; set; }
        public int TiempoEstimadoFalla { get; set; }
    }
}
