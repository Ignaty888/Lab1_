using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace lab1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

        }

        int i = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            label8.Text = "";
            if (numericUpDown4.Value < numericUpDown5.Value)
            {
                label8.Text = "Макс значение не м.б. меньше мин значения!"; return; }
            int count, current = 0;
                count = (Convert.ToInt32(numericUpDown2.Value) -
               Convert.ToInt32(numericUpDown1.Value)) /
               Convert.ToInt32(numericUpDown3.Value) + 1;
                for (int n = Convert.ToInt32(numericUpDown1.Value); n <=
               Convert.ToInt32(numericUpDown2.Value); n +=
               Convert.ToInt32(numericUpDown3.Value))
                {
                   int[] vptr = new int[n];
                Random rand = new Random();
                    for (int j = 0; j < n; j++)
                    {
                        vptr[j] =
                       rand.Next(Convert.ToInt32(numericUpDown5.Value),
                       Convert.ToInt32(numericUpDown4.Value));
                    }
                    if (checkBox1.Checked)
                    {
                        dataGridView1.ColumnCount = n + 1;
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Cells[0].Value = "Исходный массив";
                    for (int j = 0; j < n; j++)
                        {
                            dataGridView1.Rows[i].Cells[j + 1].Value =
                       vptr[j];
                        }
                        i++;
                    }
                    sort(vptr, n);
                    current += 1;
                    progressBar1.Value = 100 * current / count;
                }
            }
            private void sort(int[] p, int n)
            {
                int k = 0, sr = 0, obm = 0, m = 0;
                for (int j = 0; j < n; j++)
                {
                    if (p[j] == 0) k++;
                    else p[j - k] = p[j];
                }
                n -= k;
                sr += n;
                if (n == 0)
                {
                    label8.Text = "В массиве одни нули"; return;
                }
                for (m = 0; m < n - 1; m++)
                    for (int j = m + 1; j < n; j++)
                    {
                        if (p[m] > 0 && p[j] > 0 && p[m] < p[j])
                        {
                            swap(ref p[m], ref p[j]); obm++;
                        }
                        if (p[m] < 0 && p[j] < 0 && p[m] > p[j])
                        {
                            swap(ref p[m], ref p[j]); obm++;
                        }
                        sr += 6;
                    }
                if (checkBox1.Checked)
                {
                    dataGridView1.AutoResizeColumns();
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = "Получен массив";
                for (int j = 0; j < n; j++)
                { dataGridView1.Rows[i].Cells[j + 1].Value = p[j]; }
                    i++;
                }
                if (Convert.ToInt32(numericUpDown1.Value) ==
               Convert.ToInt32(numericUpDown2.Value))
                {
                    label8.Text = "Количество сравнений=" +
               Convert.ToString(sr) + " Количество обменов=" + Convert.ToString(obm);
                }
                if (checkBox2.Checked)
                {
                    chart1.Series[0].Points.AddXY(n, sr);
                    chart2.Series[0].Points.AddXY(n, obm);
                }
            }


            void swap(ref int x, ref int y)
            { int z = x; x = y; y = z; }

            private void button2_Click(object sender, EventArgs e)
            {
                chart1.Series[0].Points.Clear();
                chart2.Series[0].Points.Clear();
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                i = 0;
                button2.Enabled = true;
            }
            int n, m;
            private void button3_Click(object sender, EventArgs e)
            {
                int i, j, k, q;
                button3.Enabled = false;
                if (numericUpDown7.Value < numericUpDown8.Value)
                {
                    label9.Text = "Макс значение не м.б. меньше мин значения!"; return; }
                n = Convert.ToInt32(numericUpDown6.Value);
                    m = Convert.ToInt32(numericUpDown6.Value);
                    int[,] ptr;
                    ptr = new int[m, n];
                    Random rand = new Random();
                    dataGridView2.AutoResizeColumns();
                    dataGridView2.ColumnCount = n;
                    for (i = 0; i < n; i++)
                    {
                        dataGridView2.Rows.Add();
                        for (j = 0; j < m; j++)
                        {
                        ptr[i, j] =rand.Next(Convert.ToInt32(numericUpDown8.Value),
                            Convert.ToInt32(numericUpDown7.Value));
                            dataGridView2.Rows[i].Cells[j].Value = ptr[i, j];
                        }
                    }
            // Обработка и вывод новой матрицы
            int[,] processedMatrix = ProcessMatrix(ptr);
            DisplayMatrix(dataGridView3, processedMatrix);



            /* q = 0;
             k = 0;
             if (ptr[n - 1, m - 1] < 0) k++;
             for (q = 0; q < n - 1; q++)

             {
                 if (ptr[q, m - 1] < 0)
                 {
                     k++;
                     for (i = q; i < n - 1; i++)
                     {
                         for (j = 0; j < m; j++) ptr[i, j] = ptr[i + 1,
                        j];

                     }
                     q--;
                 }
                 if (k + q + 1 == n) { break; }
             }
             if (k == n)
             {
                 label9.Text = "В матрице удалены все строки";
                 return;
             }
             if (k == 0)
             {
                 label9.Text = "В матрице нет удаленных строк";
                 return;
             }
             label9.Text = "В матрице удалено " + k + " строк(и)";
             for (j = 0; j < m; j++)
             {
                 ptr[n - k, j] = 0;
                 for (i = 0; i < n - k; i++)
                 {
                     ptr[n - k, j] += ptr[i, j];
                 }
             }
             dataGridView3.AutoResizeColumns();
             dataGridView3.ColumnCount = n;
             for (i = 0; i <= n - k; i++)
             {
                 dataGridView3.Rows.Add();
                 for (j = 0; j < m; j++)
                 {
                     dataGridView3.Rows[i].Cells[j].Value = ptr[i, j];
                 }
             }*/
            }

           private void button4_Click(object sender, EventArgs e)
           {
             dataGridView2.Rows.Clear();
             dataGridView2.Columns.Clear();
             dataGridView3.Rows.Clear();
             dataGridView3.Columns.Clear();
             button3.Enabled = true;
             label9.Text = "";
           }
        // Метод для обработки матрицы
        private int[,] ProcessMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            // Выбираем столбцы, которые нужно оставить
            bool[] keepColumn = new bool[cols];
            for (int j = 0; j < cols; j++)
            {
                keepColumn[j] = true;
                for (int i = j + 1; i < rows; i++)
                {
                    if (matrix[i, j] == 0)
                    {
                        keepColumn[j] = false;
                        break;
                    }
                }
            }

            // Подсчитываем количество столбцов, которые нужно оставить
            int newCols = keepColumn.Count(k => k);

            // Создаем новую матрицу с дополнительным столбцом для суммы строк
            int[,] newMatrix = new int[rows, newCols + 1];

            // Копируем элементы из старой матрицы в новую, исключая ненужные столбцы
            for (int i = 0; i < rows; i++)
            {
                int colIndex = 0;
                for (int j = 0; j < cols; j++)
                {
                    if (keepColumn[j])
                    {
                        newMatrix[i, colIndex] = matrix[i, j];
                        colIndex++;
                    }
                }
            }

            // Добавляем столбец с суммами строк
            for (int i = 0; i < rows; i++)
            {
                int rowSum = 0;
                for (int j = 0; j < newCols; j++)
                {
                    rowSum += newMatrix[i, j];
                }
                newMatrix[i, newCols] = rowSum;
            }

            return newMatrix;
        }

        // Метод для вывода матрицы в DataGridView
        private void DisplayMatrix(DataGridView dgv, int[,] matrix)
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            dgv.ColumnCount = cols;
            for (int i = 0; i < rows; i++)
            {
                dgv.Rows.Add();
                for (int j = 0; j < cols; j++)
                {
                    dgv.Rows[i].Cells[j].Value = matrix[i, j];
                }
            }
        }

    }
}




