using System.Collections.Generic;
using System.Drawing;
using System.Windows;

namespace CanvasDataModel
{
    public class FreeHand : IShape
    {
        public ShapeType Type => ShapeType.FreeHand;
        public List<Point> Points { get; } = new();

        public FreeHand(List<Point> points)
        {
            Points.AddRange(points);
        }
    }
}