using WinFormsApp1.Clases;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Paso 0: Condición de vacío y negativos
            if (textBox1.Text.Equals("") || textBox2.Text.Equals("") || textBox3.Text.Equals("") || textBox4.Text.Equals("") || textBox5.Text.Equals(""))
            {
                MessageBox.Show("Los números tienen que ser MAYOR que cero, NO VACÍOS");
                return;
            }


            // Paso 1: Inicialización de parámetros
            int numeroIteraciones = Convert.ToInt32(textBox1.Text);
            int totalPaneles = Convert.ToInt32(textBox2.Text);
            int minPanelesFuncionando = Convert.ToInt32(textBox3.Text);
            int rangoInferior = Convert.ToInt32(textBox4.Text);
            int rangoSuperior = Convert.ToInt32(textBox5.Text);

            if (numeroIteraciones < 0 || totalPaneles < 0 || minPanelesFuncionando < 0 || rangoInferior < 0 || rangoSuperior < 0 )
            {
                MessageBox.Show("Los números tienen que ser MAYOR que cero");
                return;
            }
            if (totalPaneles < minPanelesFuncionando )
            {
                MessageBox.Show("Numero en los panles incorrectos");
                return;
            }

            // Crear una instancia del algoritmo de Monte Carlo
            MonteCarlo monteCarlo = new MonteCarlo();

            // Ejecutar la simulación
            var resultados = monteCarlo.RealizarSimulacion(totalPaneles, minPanelesFuncionando, rangoInferior, rangoSuperior, numeroIteraciones);

            // Configurar el DataGridView
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("NumeroExperimento", "N° Experimento");

            // Crear columnas para cada panel
            for (int i = 1; i <= totalPaneles; i++)
            {
                dataGridView1.Columns.Add($"Panel{i}", $"Panel {i}");
            }
            dataGridView1.Columns.Add("TiempoEstimadoFalla", "Tiempo Estimado de Falla");

            // Agregar los resultados al DataGridView
            foreach (var resultado in resultados)
            {
                var fila = new DataGridViewRow();
                fila.CreateCells(dataGridView1);

                fila.Cells[0].Value = resultado.NumeroExperimento;
                for (int i = 0; i < totalPaneles; i++)
                {
                    fila.Cells[i + 1].Value = resultado.TiemposPaneles[i];
                }
                fila.Cells[totalPaneles + 1].Value = resultado.TiempoEstimadoFalla;

                dataGridView1.Rows.Add(fila);
            }

            // Calcular el valor estimado promedio al final de la simulación
            int valorEstimadoPromedio = (int)resultados.Average(r => r.TiempoEstimadoFalla);
            var resumenFila = new DataGridViewRow();
            resumenFila.CreateCells(dataGridView1);
            resumenFila.Cells[0].Value = "Promedio";
            resumenFila.Cells[totalPaneles + 1].Value = valorEstimadoPromedio;
            dataGridView1.Rows.Add(resumenFila);
            


        }

        public void llenarGrid(List<int> lista)
        {
            // Paso 0: Indicas el número de columnas
            string numeroColumna1 = "1";
            string numeroColumna2 = "2";

            // Paso 1: Determinas la cantidad de columnas
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add(numeroColumna1, "Id");
            dataGridView1.Columns.Add(numeroColumna2, "Valor");

            // Paso 2: Recorres el grid para cada fila y llenas los valores aleatorios
            for (int i = 0; i < lista.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[Int32.Parse(numeroColumna1) - 1].Value = (i + 1).ToString();
                dataGridView1.Rows[i].Cells[Int32.Parse(numeroColumna2) - 1].Value = lista[i].ToString();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void DescargaExcel(DataGridView data)
        {
            // Paso 0: Instalar complemento de Excel
            Microsoft.Office.Interop.Excel.Application exportarExcel = new Microsoft.Office.Interop.Excel.Application();
            exportarExcel.Application.Workbooks.Add(true);
            int indiceColumna = 0;

            // Paso 1: Construyes columnas y los nombres de las "cabeceras"
            foreach (DataGridViewColumn columna in data.Columns)
            {
                indiceColumna++;
                exportarExcel.Cells[1, indiceColumna] = columna.HeaderText;
            }

            // Paso 2: Construyes filas y llenas valores
            int indiceFilas = 0;
            foreach (DataGridViewRow fila in data.Rows)
            {
                indiceFilas++;
                indiceColumna = 0;
                foreach (DataGridViewColumn columna in data.Columns)
                {
                    indiceColumna++;
                    exportarExcel.Cells[indiceFilas + 1, indiceColumna] = fila.Cells[columna.Name].Value;
                }
            }

            // Paso 3: Visibilidad
            exportarExcel.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DescargaExcel(dataGridView1);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}

