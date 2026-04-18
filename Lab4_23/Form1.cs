using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Lab4_23
{
    public partial class Form1 : Form
    {
        private Graph _graph = new Graph();
        private bool _graphLoaded = false;

        public Form1()
        {
            InitializeComponent();
        }

        //  Обработчики кнопок

        private void btnLoad_Click(object sender, EventArgs e)
        {
            // Сначала ищем файл рядом с exe
            string defaultFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "energy_graph.txt");
            if (!File.Exists(defaultFile))
                defaultFile = Path.Combine(Directory.GetCurrentDirectory(), "energy_graph.txt");

            if (File.Exists(defaultFile))
            {
                LoadGraph(defaultFile);
            }
            else
            {
                using var dlg = new OpenFileDialog
                {
                    Title = "Выберите файл графа",
                    Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
                };
                if (dlg.ShowDialog() == DialogResult.OK)
                    LoadGraph(dlg.FileName);
            }
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            if (!CheckLoaded()) return;
            ShowGraphInfo();
        }

        private void btnBfs_Click(object sender, EventArgs e)
        {
            if (!CheckLoaded()) return;
            string start = cmbStart.SelectedItem?.ToString() ?? "";
            if (string.IsNullOrEmpty(start)) { Warn("Выберите начальную вершину."); return; }
            RunBFS(start);
        }

        private void btnDfs_Click(object sender, EventArgs e)
        {
            if (!CheckLoaded()) return;
            string start = cmbStart.SelectedItem?.ToString() ?? "";
            if (string.IsNullOrEmpty(start)) { Warn("Выберите начальную вершину."); return; }
            RunDFS(start);
        }

        private void btnReach_Click(object sender, EventArgs e)
        {
            if (!CheckLoaded()) return;
            string start = cmbStart.SelectedItem?.ToString() ?? "";
            string end = cmbEnd.SelectedItem?.ToString() ?? "";
            if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
            { Warn("Выберите начальную и конечную вершины."); return; }
            RunReachability(start, end);
        }

        private void btnComp_Click(object sender, EventArgs e)
        {
            if (!CheckLoaded()) return;
            RunComponents();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtOutput.Clear();
        }

        //  Загрузка графа

        private void LoadGraph(string path)
        {
            try
            {
                _graph.LoadFromFile(path);
                _graphLoaded = true;

                cmbStart.Items.Clear();
                cmbEnd.Items.Clear();
                foreach (string v in _graph.Vertices)
                {
                    cmbStart.Items.Add(v);
                    cmbEnd.Items.Add(v);
                }
                if (cmbStart.Items.Count > 0) cmbStart.SelectedIndex = 0;
                if (cmbEnd.Items.Count > 1) cmbEnd.SelectedIndex = 1;

                lblStatus.Text = $"Граф загружен: {_graph.Vertices.Count} вершин | Файл: {Path.GetFileName(path)}";
                lblStatus.ForeColor = Color.FromArgb(39, 174, 96);

                AppendLine(Color.FromArgb(80, 200, 120),
                    $"Граф успешно загружен из файла: {path}");
                AppendLine(Color.FromArgb(200, 200, 200),
                    $"   Вершин: {_graph.Vertices.Count}");
                AppendLine(Color.FromArgb(200, 200, 200),
                    $"   Вершины: {string.Join(", ", _graph.Vertices)}");
                AppendLine(Color.Empty, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки файла:\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //  Информация о графе

        private void ShowGraphInfo()
        {
            AppendHeader("ИНФОРМАЦИЯ О ГРАФЕ ЭНЕРГОСИСТЕМЫ");
            AppendLine(Color.FromArgb(200, 200, 200),
                $"Количество вершин: {_graph.Vertices.Count}");

            int edgeCount = 0;
            foreach (string v in _graph.Vertices)
                edgeCount += _graph.GetNeighbors(v).Count;
            edgeCount /= 2;

            AppendLine(Color.FromArgb(200, 200, 200), $"Количество рёбер: {edgeCount}");
            AppendLine(Color.Empty, "");
            AppendLine(Color.FromArgb(255, 200, 100), "Список смежности:");

            foreach (string v in _graph.Vertices)
            {
                var neighbors = _graph.GetNeighbors(v);
                string neighborStr = neighbors.Count > 0
                    ? string.Join(", ", neighbors.Select(n => $"{n.neighbor} ({n.weight} МВт)"))
                    : "(нет связей)";
                AppendLine(Color.FromArgb(180, 220, 255), $"  {v}");
                AppendLine(Color.FromArgb(160, 160, 160), $"    -> {neighborStr}");
            }
            AppendLine(Color.Empty, "");
        }

        //  BFS

        private void RunBFS(string start)
        {
            AppendHeader($"BFS — ОБХОД В ШИРИНУ от «{start}»");
            AppendLine(Color.FromArgb(160, 160, 160),
                "Алгоритм: обходит граф уровень за уровнем, используя очередь (Queue).");
            AppendLine(Color.Empty, "");

            var order = _graph.BFS(start);

            AppendLine(Color.FromArgb(255, 200, 100), $"Порядок посещения ({order.Count} вершин):");
            for (int i = 0; i < order.Count; i++)
                AppendLine(Color.FromArgb(80, 200, 120), $"  {i + 1,2}. {order[i]}");

            var notVisited = _graph.Vertices.Except(order).ToList();
            AppendLine(Color.Empty, "");
            if (notVisited.Count > 0)
                AppendLine(Color.FromArgb(255, 100, 100),
                    $"Недостижимые вершины ({notVisited.Count}): {string.Join(", ", notVisited)}");
            else
                AppendLine(Color.FromArgb(80, 200, 120),
                    "Все вершины достижимы из данной стартовой вершины.");
            AppendLine(Color.Empty, "");
        }

        //  DFS

        private void RunDFS(string start)
        {
            AppendHeader($"DFS — ОБХОД В ГЛУБИНУ от «{start}»");
            AppendLine(Color.FromArgb(160, 160, 160),
                "Алгоритм: уходит как можно глубже по одной ветке, используя стек (Stack).");
            AppendLine(Color.Empty, "");

            var order = _graph.DFS(start);

            AppendLine(Color.FromArgb(255, 200, 100), $"Порядок посещения ({order.Count} вершин):");
            for (int i = 0; i < order.Count; i++)
                AppendLine(Color.FromArgb(100, 200, 255), $"  {i + 1,2}. {order[i]}");

            var notVisited = _graph.Vertices.Except(order).ToList();
            AppendLine(Color.Empty, "");
            if (notVisited.Count > 0)
                AppendLine(Color.FromArgb(255, 100, 100),
                    $"Недостижимые вершины ({notVisited.Count}): {string.Join(", ", notVisited)}");
            else
                AppendLine(Color.FromArgb(80, 200, 120),
                    "Все вершины достижимы из данной стартовой вершины.");
            AppendLine(Color.Empty, "");
        }

        //  Достижимость

        private void RunReachability(string source, string target)
        {
            AppendHeader($"ДОСТИЖИМОСТЬ: «{source}» -> «{target}»");
            AppendLine(Color.FromArgb(160, 160, 160),
                "Метод: BFS с восстановлением пути через массив предков.");
            AppendLine(Color.Empty, "");

            var (reachable, path) = _graph.IsReachable(source, target);

            if (reachable)
            {
                AppendLine(Color.FromArgb(80, 200, 120),
                    $"Вершина «{target}» ДОСТИЖИМА из «{source}».");
                AppendLine(Color.Empty, "");
                AppendLine(Color.FromArgb(255, 200, 100),
                    $"Путь ({path.Count - 1} переход(ов)):");
                AppendLine(Color.FromArgb(180, 220, 255),
                    "  " + string.Join(" -> ", path));
            }
            else
            {
                AppendLine(Color.FromArgb(255, 100, 100),
                    $"Вершина «{target}» НЕДОСТИЖИМА из «{source}».");
                AppendLine(Color.FromArgb(160, 160, 160),
                    "Вершины находятся в разных компонентах связности.");
            }
            AppendLine(Color.Empty, "");
        }

        //  Компоненты связности

        private void RunComponents()
        {
            AppendHeader("КОМПОНЕНТЫ СВЯЗНОСТИ ГРАФА");
            AppendLine(Color.FromArgb(160, 160, 160),
                "Метод: последовательный BFS для всех непосещённых вершин.");
            AppendLine(Color.Empty, "");

            var components = _graph.GetConnectedComponents();

            AppendLine(Color.FromArgb(255, 200, 100), $"Найдено компонент: {components.Count}");
            AppendLine(Color.Empty, "");

            for (int i = 0; i < components.Count; i++)
            {
                var comp = components[i];
                Color c = i == 0
                    ? Color.FromArgb(80, 200, 120)
                    : Color.FromArgb(255, 150, 80);
                AppendLine(c, $"  Компонента {i + 1} ({comp.Count} вершин):");
                AppendLine(Color.FromArgb(180, 220, 255), "    " + string.Join(", ", comp));
            }

            AppendLine(Color.Empty, "");
            if (components.Count == 1)
                AppendLine(Color.FromArgb(80, 200, 120),
                    "Граф связный — все узлы энергосистемы соединены.");
            else
                AppendLine(Color.FromArgb(255, 100, 100),
                    $"Граф несвязный — {components.Count} изолированных сегмента энергосети.");
            AppendLine(Color.Empty, "");
        }

        //  Вспомогательные методы вывода
        private void AppendHeader(string text)
        {
            string sep = new string('=', 30);
            AppendLine(Color.FromArgb(255, 200, 50), $"\n{sep} {text} {sep}");
        }

        private void AppendLine(Color color, string text)
        {
            txtOutput.SelectionStart = txtOutput.TextLength;
            txtOutput.SelectionLength = 0;
            txtOutput.SelectionColor = color != Color.Empty
                ? color
                : Color.FromArgb(220, 220, 220);
            txtOutput.AppendText(text + "\n");
            txtOutput.ScrollToCaret();
        }

        private bool CheckLoaded()
        {
            if (_graphLoaded) return true;
            Warn("Сначала загрузите граф (кнопка «Загрузить граф»).");
            return false;
        }

        private void Warn(string msg)
        {
            AppendLine(Color.FromArgb(255, 150, 50), $"[!] {msg}");
        }
    }
}
