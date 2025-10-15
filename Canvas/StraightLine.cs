using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDataModel
{
    public class StraightLine : IShape
    {
        public ShapeType Type => ShapeType.StraightLine;
        public List<Point> Points { get; } = new();

        public StraightLine(List<Point> points)
        {
            Points.AddRange(points);
        }

    }
}
