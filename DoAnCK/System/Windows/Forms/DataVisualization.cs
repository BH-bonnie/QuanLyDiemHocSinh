
namespace System.Windows.Forms
{
    internal class DataVisualization
    {
        internal class Charting
        {
            public static object SeriesChartType { get; internal set; }

            internal class Series : DevExpress.XtraCharts.Series
            {
                private string v;

                public Series(string v)
                {
                    this.v = v;
                }

                public object ChartType { get; internal set; }
            }
        }
    }
}