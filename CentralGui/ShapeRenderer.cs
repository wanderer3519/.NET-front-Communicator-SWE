using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CanvasDataModel
{
    public static class ShapeRenderer
    {
        public static void Render(Canvas canvas, IShape shape)
        {
            switch (shape.Type)
            {
                case ShapeType.FreeHand:
                    RenderFreeHand(canvas, (FreeHand)shape);
                    break;

                case ShapeType.StraightLine:
                    RenderStraightLine(canvas, (StraightLine)shape);
                    break;
                case ShapeType.Rectangle:
                    RenderRectangle(canvas, (RectangleShape)shape);
                    break;  
                case ShapeType.EllipseShape:
                    RenderEllipse(canvas, (EllipseShape)shape);
                    break;
                case ShapeType.Triangle:
                    RenderTriangle(canvas, (TriangleShape)shape);
                    break;
            }
        }

        private static void RenderStraightLine(Canvas canvas, StraightLine line)
        {
            Line uiLine = new Line
            {
                X1 = line.Points[0].X,
                Y1 = line.Points[0].Y,
                X2 = line.Points[1].X,
                Y2 = line.Points[1].Y,
                Stroke = Brushes.Red,
                StrokeThickness = 2
            };
            canvas.Children.Add(uiLine);
        }
        private static void RenderFreeHand(Canvas canvas, FreeHand freeHand)
        {
            for (int i = 1; i < freeHand.Points.Count; i++)
            {
                Line segment = new Line
                {
                    X1 = freeHand.Points[i - 1].X,
                    Y1 = freeHand.Points[i - 1].Y,
                    X2 = freeHand.Points[i].X,
                    Y2 = freeHand.Points[i].Y,
                    Stroke = Brushes.Red,
                    StrokeThickness = 2
                };
                canvas.Children.Add(segment);
            }
        }
        //[topleft, bottomright]
        private static void RenderRectangle(Canvas canvas, RectangleShape rectangle)
        {
            Line uLine = new Line
            {
                X1 = rectangle.Points[0].X,
                Y1 = rectangle.Points[0].Y,
                X2 = rectangle.Points[1].X,
                Y2 = rectangle.Points[0].Y,
                Stroke = Brushes.Red,
                StrokeThickness = 2
            };
            canvas.Children.Add(uLine);

            Line rLine = new Line
            {
                X1 = rectangle.Points[1].X,
                Y1 = rectangle.Points[0].Y,
                X2 = rectangle.Points[1].X,
                Y2 = rectangle.Points[1].Y,
                Stroke = Brushes.Red,
                StrokeThickness = 2
            };
            canvas.Children.Add(rLine);

            Line dLine = new Line
            {
                X1 = rectangle.Points[1].X,
                Y1 = rectangle.Points[1].Y,
                X2 = rectangle.Points[0].X,
                Y2 = rectangle.Points[1].Y,
                Stroke = Brushes.Red,
                StrokeThickness = 2
            };
            canvas.Children.Add(dLine);

            Line lLine = new Line
            {
                X1 = rectangle.Points[0].X,
                Y1 = rectangle.Points[1].Y,
                X2 = rectangle.Points[0].X,
                Y2 = rectangle.Points[0].Y,
                Stroke = Brushes.Red,
                StrokeThickness = 2
            };
            canvas.Children.Add(lLine);
        }
        public static void RenderEllipse(Canvas canvas, EllipseShape ellipse)
        {
            var topLeft = ellipse.Points[0];
            var bottomRight = ellipse.Points[1];

            double x = Math.Min(topLeft.X, bottomRight.X);
            double y = Math.Min(topLeft.Y, bottomRight.Y);
            double width = Math.Abs(bottomRight.X - topLeft.X);
            double height = Math.Abs(bottomRight.Y - topLeft.Y);

            Ellipse uiEllipse = new Ellipse
            {
                Width = width,
                Height = height,
                Stroke = Brushes.Red,
                StrokeThickness = 2
            };

            Canvas.SetLeft(uiEllipse, x);
            Canvas.SetTop(uiEllipse, y);

            canvas.Children.Add(uiEllipse);
        }
        public static void RenderTriangle(Canvas canvas, TriangleShape triangle)
        {
            Line lLine = new Line
            {
                X1 = triangle.Points[0].X,
                Y1 = triangle.Points[1].Y,
                X2 = (triangle.Points[1].X + triangle.Points[0].X)/2,
                Y2 = triangle.Points[0].Y,
                Stroke = Brushes.Red,
                StrokeThickness = 2
            };
            canvas.Children.Add(lLine);

            Line rLine = new Line
            {
                X1 = (triangle.Points[1].X + triangle.Points[0].X) / 2,
                Y1 = triangle.Points[0].Y,
                X2 = triangle.Points[1].X,
                Y2 = triangle.Points[1].Y,
                Stroke = Brushes.Red,
                StrokeThickness = 2
            };
            canvas.Children.Add(rLine);

            Line dLine = new Line
            {
                X1 = triangle.Points[1].X,
                Y1 = triangle.Points[1].Y,
                X2 = triangle.Points[0].X,
                Y2 = triangle.Points[1].Y,
                Stroke = Brushes.Red,
                StrokeThickness = 2
            };
            canvas.Children.Add(dLine);

        }
        public static void RenderAll(Canvas canvas, ObservableCollection<IShape> shapes)
        {
            canvas.Children.Clear();        // Clear existing drawings
            foreach (var shape in shapes)   // Draw each shape
                Render(canvas, shape);
        }
    }
}