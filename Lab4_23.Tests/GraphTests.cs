using System.IO;
using Lab4_23;

namespace Lab4_23.Tests
{
    /// <summary>
    /// Тесты для класса Graph.
    /// Покрывают: загрузку из файла, BFS, DFS, достижимость, компоненты связности.
    /// </summary>
    [TestFixture]
    public class GraphTests
    {
        //  Вспомогательные методы создания графов

        /// <summary>
        /// Создаёт граф из строки (записывает во временный файл и загружает).
        /// </summary>
        private static Graph GraphFromText(string content)
        {
            string tmp = Path.GetTempFileName();
            File.WriteAllText(tmp, content);
            var g = new Graph();
            g.LoadFromFile(tmp);
            File.Delete(tmp);
            return g;
        }

        /// <summary>
        /// Простой связный граф: A-B-C-D (цепочка), плюс ребро B-D.
        ///
        ///  A --10-- B --20-- C
        ///           |        
        ///          30        
        ///           |        
        ///           D        
        /// </summary>
        private static Graph SimpleGraph() => GraphFromText(@"
VERTICES
A
B
C
D
EDGES
A;B;10
B;C;20
B;D;30
");

        /// <summary>
        /// Несвязный граф: компонента {A,B} и компонента {C,D}.
        /// </summary>
        private static Graph DisconnectedGraph() => GraphFromText(@"
VERTICES
A
B
C
D
EDGES
A;B;5
C;D;7
");

        /// <summary>
        /// Граф с одной вершиной без рёбер.
        /// </summary>
        private static Graph SingleVertexGraph() => GraphFromText(@"
VERTICES
X
EDGES
");

        //  Тесты загрузки

        [Test]
        public void LoadFromFile_CorrectVertexCount()
        {
            var g = SimpleGraph();
            Assert.That(g.Vertices.Count, Is.EqualTo(4));
        }

        [Test]
        public void LoadFromFile_VerticesContainExpectedNames()
        {
            var g = SimpleGraph();
            Assert.That(g.Vertices, Does.Contain("A"));
            Assert.That(g.Vertices, Does.Contain("B"));
            Assert.That(g.Vertices, Does.Contain("C"));
            Assert.That(g.Vertices, Does.Contain("D"));
        }

        [Test]
        public void LoadFromFile_NeighboursAreSymmetric()
        {
            // Неориентированный граф: если A->B, то и B->A
            var g = SimpleGraph();
            var neighborsA = g.GetNeighbors("A").Select(n => n.neighbor).ToList();
            var neighborsB = g.GetNeighbors("B").Select(n => n.neighbor).ToList();

            Assert.That(neighborsA, Does.Contain("B"));
            Assert.That(neighborsB, Does.Contain("A"));
        }

        [Test]
        public void LoadFromFile_WeightsAreCorrect()
        {
            var g = SimpleGraph();
            var ab = g.GetNeighbors("A").First(n => n.neighbor == "B");
            Assert.That(ab.weight, Is.EqualTo(10));
        }

        [Test]
        public void LoadFromFile_CommentsAndEmptyLinesIgnored()
        {
            var g = GraphFromText(@"
# это комментарий
VERTICES
# ещё комментарий
Node1
Node2

EDGES
# ребро
Node1;Node2;99
");
            Assert.That(g.Vertices.Count, Is.EqualTo(2));
            Assert.That(g.GetNeighbors("Node1").First().weight, Is.EqualTo(99));
        }

        [Test]
        public void LoadFromFile_SingleVertex_NoEdges()
        {
            var g = SingleVertexGraph();
            Assert.That(g.Vertices.Count, Is.EqualTo(1));
            Assert.That(g.GetNeighbors("X").Count, Is.EqualTo(0));
        }

        //  Тесты BFS

        [Test]
        public void BFS_StartsFromCorrectVertex()
        {
            var g = SimpleGraph();
            var order = g.BFS("A");
            Assert.That(order[0], Is.EqualTo("A"));
        }

        [Test]
        public void BFS_VisitsAllReachableVertices()
        {
            var g = SimpleGraph();
            var order = g.BFS("A");
            Assert.That(order.Count, Is.EqualTo(4));
            Assert.That(order, Does.Contain("A"));
            Assert.That(order, Does.Contain("B"));
            Assert.That(order, Does.Contain("C"));
            Assert.That(order, Does.Contain("D"));
        }

        [Test]
        public void BFS_BreadthFirstOrder_AIsFirst_BIsSecond()
        {
            // A -> B (сосед A), затем C и D (соседи B)
            var g = SimpleGraph();
            var order = g.BFS("A");
            Assert.That(order.IndexOf("B"), Is.LessThan(order.IndexOf("C")));
            Assert.That(order.IndexOf("B"), Is.LessThan(order.IndexOf("D")));
        }

        [Test]
        public void BFS_DisconnectedGraph_OnlyReachableComponent()
        {
            var g = DisconnectedGraph();
            var order = g.BFS("A");
            Assert.That(order, Does.Contain("A"));
            Assert.That(order, Does.Contain("B"));
            Assert.That(order, Does.Not.Contain("C"));
            Assert.That(order, Does.Not.Contain("D"));
        }

        [Test]
        public void BFS_UnknownVertex_ReturnsEmpty()
        {
            var g = SimpleGraph();
            var order = g.BFS("Z");
            Assert.That(order, Is.Empty);
        }

        [Test]
        public void BFS_SingleVertex_ReturnsSelf()
        {
            var g = SingleVertexGraph();
            var order = g.BFS("X");
            Assert.That(order.Count, Is.EqualTo(1));
            Assert.That(order[0], Is.EqualTo("X"));
        }

        //  Тесты DFS

        [Test]
        public void DFS_StartsFromCorrectVertex()
        {
            var g = SimpleGraph();
            var order = g.DFS("A");
            Assert.That(order[0], Is.EqualTo("A"));
        }

        [Test]
        public void DFS_VisitsAllReachableVertices()
        {
            var g = SimpleGraph();
            var order = g.DFS("A");
            Assert.That(order.Count, Is.EqualTo(4));
        }

        [Test]
        public void DFS_NoRepeatedVertices()
        {
            var g = SimpleGraph();
            var order = g.DFS("A");
            Assert.That(order.Distinct().Count(), Is.EqualTo(order.Count));
        }

        [Test]
        public void DFS_DisconnectedGraph_OnlyReachableComponent()
        {
            var g = DisconnectedGraph();
            var order = g.DFS("C");
            Assert.That(order, Does.Contain("C"));
            Assert.That(order, Does.Contain("D"));
            Assert.That(order, Does.Not.Contain("A"));
            Assert.That(order, Does.Not.Contain("B"));
        }

        [Test]
        public void DFS_UnknownVertex_ReturnsEmpty()
        {
            var g = SimpleGraph();
            var order = g.DFS("Z");
            Assert.That(order, Is.Empty);
        }

        [Test]
        public void DFS_SingleVertex_ReturnsSelf()
        {
            var g = SingleVertexGraph();
            var order = g.DFS("X");
            Assert.That(order.Count, Is.EqualTo(1));
            Assert.That(order[0], Is.EqualTo("X"));
        }

        //  Тесты достижимости (IsReachable)

        [Test]
        public void IsReachable_DirectNeighbour_ReturnsTrue()
        {
            var g = SimpleGraph();
            var (reachable, _) = g.IsReachable("A", "B");
            Assert.That(reachable, Is.True);
        }

        [Test]
        public void IsReachable_IndirectVertex_ReturnsTrue()
        {
            var g = SimpleGraph();
            var (reachable, _) = g.IsReachable("A", "C");
            Assert.That(reachable, Is.True);
        }

        [Test]
        public void IsReachable_SameVertex_ReturnsTrue()
        {
            var g = SimpleGraph();
            var (reachable, path) = g.IsReachable("A", "A");
            Assert.That(reachable, Is.True);
            Assert.That(path.Count, Is.EqualTo(1));
            Assert.That(path[0], Is.EqualTo("A"));
        }

        [Test]
        public void IsReachable_DifferentComponent_ReturnsFalse()
        {
            var g = DisconnectedGraph();
            var (reachable, _) = g.IsReachable("A", "C");
            Assert.That(reachable, Is.False);
        }

        [Test]
        public void IsReachable_PathStartsWithSource()
        {
            var g = SimpleGraph();
            var (_, path) = g.IsReachable("A", "D");
            Assert.That(path.First(), Is.EqualTo("A"));
        }

        [Test]
        public void IsReachable_PathEndsWithTarget()
        {
            var g = SimpleGraph();
            var (_, path) = g.IsReachable("A", "D");
            Assert.That(path.Last(), Is.EqualTo("D"));
        }

        [Test]
        public void IsReachable_PathIsContiguous()
        {
            // Каждый следующий узел пути должен быть соседом предыдущего
            var g = SimpleGraph();
            var (_, path) = g.IsReachable("A", "C");
            for (int i = 0; i < path.Count - 1; i++)
            {
                var neighbors = g.GetNeighbors(path[i]).Select(n => n.neighbor);
                Assert.That(neighbors, Does.Contain(path[i + 1]),
                    $"Вершина {path[i + 1]} не является соседом {path[i]}");
            }
        }

        [Test]
        public void IsReachable_UnknownSource_ReturnsFalse()
        {
            var g = SimpleGraph();
            var (reachable, _) = g.IsReachable("Z", "A");
            Assert.That(reachable, Is.False);
        }

        [Test]
        public void IsReachable_UnknownTarget_ReturnsFalse()
        {
            var g = SimpleGraph();
            var (reachable, _) = g.IsReachable("A", "Z");
            Assert.That(reachable, Is.False);
        }

        //  Тесты компонент связности

        [Test]
        public void GetConnectedComponents_ConnectedGraph_OneComponent()
        {
            var g = SimpleGraph();
            var components = g.GetConnectedComponents();
            Assert.That(components.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetConnectedComponents_ConnectedGraph_AllVerticesInComponent()
        {
            var g = SimpleGraph();
            var all = g.GetConnectedComponents().SelectMany(c => c).ToList();
            Assert.That(all.Count, Is.EqualTo(4));
        }

        [Test]
        public void GetConnectedComponents_DisconnectedGraph_TwoComponents()
        {
            var g = DisconnectedGraph();
            var components = g.GetConnectedComponents();
            Assert.That(components.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetConnectedComponents_DisconnectedGraph_CorrectSizes()
        {
            var g = DisconnectedGraph();
            var sizes = g.GetConnectedComponents().Select(c => c.Count).OrderBy(x => x).ToList();
            Assert.That(sizes, Is.EqualTo(new List<int> { 2, 2 }));
        }

        [Test]
        public void GetConnectedComponents_AllVerticesCovered()
        {
            var g = DisconnectedGraph();
            var all = g.GetConnectedComponents().SelectMany(c => c).ToList();
            Assert.That(all.Count, Is.EqualTo(g.Vertices.Count));
        }

        [Test]
        public void GetConnectedComponents_NoVertexInTwoComponents()
        {
            var g = DisconnectedGraph();
            var all = g.GetConnectedComponents().SelectMany(c => c).ToList();
            // Нет дублей
            Assert.That(all.Distinct().Count(), Is.EqualTo(all.Count));
        }

        [Test]
        public void GetConnectedComponents_SingleVertex_OneComponent()
        {
            var g = SingleVertexGraph();
            var components = g.GetConnectedComponents();
            Assert.That(components.Count, Is.EqualTo(1));
            Assert.That(components[0][0], Is.EqualTo("X"));
        }

        [Test]
        public void GetConnectedComponents_ThreeComponents()
        {
            var g = GraphFromText(@"
VERTICES
A
B
C
D
E
F
EDGES
A;B;1
C;D;2
E;F;3
");
            Assert.That(g.GetConnectedComponents().Count, Is.EqualTo(3));
        }

        //  Тест на реальном файле энергосистемы

        [Test]
        public void RealFile_LoadsCorrectly()
        {
            // Ищем файл рядом с тестовой сборкой
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "energy_graph.txt");
            if (!File.Exists(path))
                Assert.Ignore("Файл energy_graph.txt не найден рядом со сборкой — пропускаем.");

            var g = new Graph();
            g.LoadFromFile(path);

            Assert.That(g.Vertices.Count, Is.GreaterThanOrEqualTo(15),
                "Должно быть минимум 15 вершин по условию ЛР");

            int edgeCount = g.Vertices.Sum(v => g.GetNeighbors(v).Count) / 2;
            Assert.That(edgeCount, Is.GreaterThanOrEqualTo(20),
                "Должно быть минимум 20 рёбер по условию ЛР");
        }

        [Test]
        public void RealFile_GraphIsConnected()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "energy_graph.txt");
            if (!File.Exists(path))
                Assert.Ignore("Файл energy_graph.txt не найден рядом со сборкой — пропускаем.");

            var g = new Graph();
            g.LoadFromFile(path);

            var components = g.GetConnectedComponents();
            Assert.That(components.Count, Is.EqualTo(1),
                "Граф энергосистемы должен быть связным");
        }
        //  ЛР5 — Тесты алгоритма Дейкстры

        /// <summary>
        /// Взвешенный граф для тестов Дейкстры:
        ///
        ///  A --1-- B --4-- D
        ///  |       |
        ///  2       2
        ///  |       |
        ///  C --1-- E
        ///
        /// Кратчайшие расстояния от A:
        ///   A=0, B=1, C=2, E=3, D=5
        /// </summary>
        private static Graph WeightedGraph() => GraphFromText(@"
VERTICES
A
B
C
D
E
EDGES
A;B;1
A;C;2
B;D;4
B;E;2
C;E;1
");

        /// <summary>
        /// Линейная цепочка A-B-C-D с разными весами.
        /// A --5-- B --1-- C --10-- D
        /// Кратчайший путь A→D = 16 (единственный).
        /// </summary>
        private static Graph LinearGraph() => GraphFromText(@"
VERTICES
A
B
C
D
EDGES
A;B;5
B;C;1
C;D;10
");

        //  Тесты Dijkstra — расстояния

        [Test]
        public void Dijkstra_SourceDistanceIsZero()
        {
            var g = WeightedGraph();
            var (dist, _) = g.Dijkstra("A");
            Assert.That(dist["A"], Is.EqualTo(0));
        }

        [Test]
        public void Dijkstra_DirectNeighbourDistance()
        {
            // A--1--B: расстояние до B = 1
            var g = WeightedGraph();
            var (dist, _) = g.Dijkstra("A");
            Assert.That(dist["B"], Is.EqualTo(1));
        }

        [Test]
        public void Dijkstra_ShortestPathThroughIntermediate()
        {
            // A→C = 2, A→B→E = 3, A→C→E = 3 — оба варианта дают 3
            var g = WeightedGraph();
            var (dist, _) = g.Dijkstra("A");
            Assert.That(dist["E"], Is.EqualTo(3));
        }

        [Test]
        public void Dijkstra_LongerDirectVsShortIndirect()
        {
            // A→B→D = 5, прямого A→D нет — расстояние = 5
            var g = WeightedGraph();
            var (dist, _) = g.Dijkstra("A");
            Assert.That(dist["D"], Is.EqualTo(5));
        }

        [Test]
        public void Dijkstra_AllVerticesHaveDistance()
        {
            // Граф связный — все вершины должны получить конечное расстояние
            var g = WeightedGraph();
            var (dist, _) = g.Dijkstra("A");
            foreach (string v in g.Vertices)
                Assert.That(dist[v], Is.Not.EqualTo(int.MaxValue),
                    $"Вершина {v} должна быть достижима из A");
        }

        [Test]
        public void Dijkstra_UnreachableVertex_MaxValue()
        {
            // Несвязный граф: C и D недостижимы из A
            var g = DisconnectedGraph();
            var (dist, _) = g.Dijkstra("A");
            Assert.That(dist["C"], Is.EqualTo(int.MaxValue));
            Assert.That(dist["D"], Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void Dijkstra_LinearChain_CorrectDistances()
        {
            // A--5--B--1--C--10--D: от A: B=5, C=6, D=16
            var g = LinearGraph();
            var (dist, _) = g.Dijkstra("A");
            Assert.That(dist["B"], Is.EqualTo(5));
            Assert.That(dist["C"], Is.EqualTo(6));
            Assert.That(dist["D"], Is.EqualTo(16));
        }

        [Test]
        public void Dijkstra_SymmetricResult()
        {
            // Неориентированный граф: dist(A→B) == dist(B→A)
            var g = WeightedGraph();
            var (distFromA, _) = g.Dijkstra("A");
            var (distFromB, _) = g.Dijkstra("B");
            Assert.That(distFromA["B"], Is.EqualTo(distFromB["A"]));
        }

        [Test]
        public void Dijkstra_SingleVertex_ZeroDistance()
        {
            var g = SingleVertexGraph();
            var (dist, _) = g.Dijkstra("X");
            Assert.That(dist["X"], Is.EqualTo(0));
        }

        //  Тесты GetDijkstraPath — восстановление пути

        [Test]
        public void GetDijkstraPath_SameVertex_ReturnsSingleElement()
        {
            var g = WeightedGraph();
            var (_, prev) = g.Dijkstra("A");
            var path = g.GetDijkstraPath(prev, "A", "A");
            Assert.That(path.Count, Is.EqualTo(1));
            Assert.That(path[0], Is.EqualTo("A"));
        }

        [Test]
        public void GetDijkstraPath_DirectNeighbour_TwoElements()
        {
            var g = WeightedGraph();
            var (_, prev) = g.Dijkstra("A");
            var path = g.GetDijkstraPath(prev, "A", "B");
            Assert.That(path.Count, Is.EqualTo(2));
            Assert.That(path[0], Is.EqualTo("A"));
            Assert.That(path[^1], Is.EqualTo("B"));
        }

        [Test]
        public void GetDijkstraPath_StartsWithSource()
        {
            var g = WeightedGraph();
            var (_, prev) = g.Dijkstra("A");
            var path = g.GetDijkstraPath(prev, "A", "D");
            Assert.That(path.First(), Is.EqualTo("A"));
        }

        [Test]
        public void GetDijkstraPath_EndsWithTarget()
        {
            var g = WeightedGraph();
            var (_, prev) = g.Dijkstra("A");
            var path = g.GetDijkstraPath(prev, "A", "D");
            Assert.That(path.Last(), Is.EqualTo("D"));
        }

        [Test]
        public void GetDijkstraPath_IsContiguous()
        {
            // Каждый следующий узел пути должен быть соседом предыдущего
            var g = WeightedGraph();
            var (_, prev) = g.Dijkstra("A");
            var path = g.GetDijkstraPath(prev, "A", "D");
            for (int i = 0; i < path.Count - 1; i++)
            {
                var neighbors = g.GetNeighbors(path[i]).Select(n => n.neighbor);
                Assert.That(neighbors, Does.Contain(path[i + 1]),
                    $"{path[i + 1]} не является соседом {path[i]}");
            }
        }

        [Test]
        public void GetDijkstraPath_LinearChain_CorrectPath()
        {
            // A--5--B--1--C--10--D: единственный путь A→D
            var g = LinearGraph();
            var (_, prev) = g.Dijkstra("A");
            var path = g.GetDijkstraPath(prev, "A", "D");
            Assert.That(path, Is.EqualTo(new List<string> { "A", "B", "C", "D" }));
        }

        [Test]
        public void GetDijkstraPath_Unreachable_ReturnsEmpty()
        {
            // Несвязный граф: из A нельзя попасть в C
            var g = DisconnectedGraph();
            var (_, prev) = g.Dijkstra("A");
            var path = g.GetDijkstraPath(prev, "A", "C");
            Assert.That(path, Is.Empty);
        }

        [Test]
        public void GetDijkstraPath_PathCostMatchesDijkstra()
        {
            // Сумма весов рёбер пути должна совпадать с dist[target]
            var g = WeightedGraph();
            var (dist, prev) = g.Dijkstra("A");
            var path = g.GetDijkstraPath(prev, "A", "D");

            int pathCost = 0;
            for (int i = 0; i < path.Count - 1; i++)
            {
                pathCost += g.GetNeighbors(path[i])
                             .First(n => n.neighbor == path[i + 1]).weight;
            }

            Assert.That(pathCost, Is.EqualTo(dist["D"]));
        }

        //  Тест Дейкстры на реальном файле

        [Test]
        public void Dijkstra_RealFile_SourceDistanceIsZero()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "energy_graph.txt");
            if (!File.Exists(path))
                Assert.Ignore("Файл energy_graph.txt не найден — пропускаем.");

            var g = new Graph();
            g.LoadFromFile(path);

            var (dist, _) = g.Dijkstra("ЭС_Центральная");
            Assert.That(dist["ЭС_Центральная"], Is.EqualTo(0));
        }

        [Test]
        public void Dijkstra_RealFile_AllVerticesReachable()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "energy_graph.txt");
            if (!File.Exists(path))
                Assert.Ignore("Файл energy_graph.txt не найден — пропускаем.");

            var g = new Graph();
            g.LoadFromFile(path);

            var (dist, _) = g.Dijkstra("ЭС_Центральная");
            foreach (string v in g.Vertices)
                Assert.That(dist[v], Is.Not.EqualTo(int.MaxValue),
                    $"Вершина {v} должна быть достижима (граф связный)");
        }
    }
}