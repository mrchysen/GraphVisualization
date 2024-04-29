namespace BitmapPractise.Graph;

public static class GraphReader
{
    /// <summary>
    /// IsOriented - ориентированный - если true то добавляется путь в обе стороны
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Graph ReadGraph(string path, bool IsOriented = false, bool IsWeighted = false)
    {
        List<Node> ans = new();

        if (File.Exists(path))
        {
            try
            {
                using (StreamReader sr = new(path))
                {
                    var data = sr.ReadLine().Split().Select(int.Parse).ToArray();
                    int n = data[0];
                    int m = data[1];
                    

                    for (int i = 0; i < n; i++)
                    {
                        ans.Add(new Node(i));
                    }

                    for (int i = 0; i < m; i++)
                    {
                        data = sr.ReadLine().Split().Select(int.Parse).ToArray();

                        int weight = 1;

                        if (IsWeighted)
                        {
                            weight = data[2];
                        }

                        ans[data[0] - 1].Add(new Edge(ans[data[1] - 1], weight));

                        if (!IsOriented)
                        {
                            ans[data[1] - 1].Add(new Edge(ans[data[0] - 1], weight));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error_reading: " + ex.Message);
            }
        }
        else
        {
            Console.WriteLine("info: No such file");
        }

        Graph graph = new Graph(ans, IsWeighted, IsOriented);

        return graph;
    }
}
