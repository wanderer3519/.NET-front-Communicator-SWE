using CanvasDataModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using ViewModel;
using static ViewModel.CanvasViewModel;

namespace CentralGui
{
    public partial class MainWindow : Window
    {
        private readonly CanvasViewModel _vm = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _vm;

            Point? lastPoint = null;
            DrawArea.MouseLeftButtonDown += (s, e) =>
            {
                Point pos = e.GetPosition(DrawArea);
                _vm.StartTracking(new System.Drawing.Point((int)pos.X, (int)pos.Y));
                DrawArea.CaptureMouse();
                lastPoint = null;
            };



            DrawArea.MouseMove += (s, e) =>
            {
                if (_vm.IsTracking)
                {
                    if (_vm.CurrentMode == DrawingMode.FreeHand)
                    {
                        Point pos = e.GetPosition(DrawArea);
                        //_vm._trackedPoints.Add(pos);
                        _vm.TrackPoint(new System.Drawing.Point((int)pos.X, (int)pos.Y));

                        if (lastPoint != null)
                        {
                            Line segment = new Line
                            {
                                X1 = lastPoint.Value.X,
                                Y1 = lastPoint.Value.Y,
                                X2 = pos.X,
                                Y2 = pos.Y,
                                Stroke = Brushes.Red,
                                StrokeThickness = 2
                            };
                            DrawArea.Children.Add(segment);
                        }

                        lastPoint = pos;
                    }
                    else
                    {
                        Point pos = e.GetPosition(DrawArea);
                        _vm.TrackPoint(new System.Drawing.Point((int)pos.X, (int)pos.Y));

                        // Clear and redraw all shapes
                        //DrawArea.Children.Clear();
                        ShapeRenderer.RenderAll(DrawArea, _vm.Shapes);

                        // Draw the preview shape if any
                        var preview = _vm.CurrentPreviewShape;
                        if (preview != null)
                        {
                            ShapeRenderer.Render(DrawArea, preview);
                        }
                    }
                }
            };

            DrawArea.MouseLeftButtonUp += (s, e) =>
            {
                //IsTracking = false;
                //DrawArea.ReleaseMouseCapture();

                // create FreeHand object from points
                //var freehand = new FreeHand(new List<Point>(_trackedPoints));
                //Shapes.Add(freehand);

                // save full shapes array as a new state
                //_stateManager.AddState(new State(Shapes));

                //MessageBox.Show($"Added FreeHand with {_trackedPoints.Count} points.");

                _vm.StopTracking(); // adds FreeHand to Shapes

                DrawArea.ReleaseMouseCapture();
                RedrawCanvas();
            };

            // keyboard undo/redo
            this.KeyDown += (s, e) =>
            {
                if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.Z)
                {
                    _vm.Undo();
                }
                else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.Y)
                {
                    _vm.Redo();
                }
                //else if (e.Key == Key.L)
                //{
                //    _vm.CurrentMode = DrawingMode.StraightLine;
                //}
                //else if (e.Key == Key.F)
                //{
                //    _vm.CurrentMode = DrawingMode.FreeHand;
                //}
                //else if (e.Key == Key.R)
                //{
                //    _vm.CurrentMode = DrawingMode.Rectangle;
                //}
                //else if (e.Key == Key.E)
                //{
                //    _vm.CurrentMode = DrawingMode.EllipseShape;
                //}
                //else if (e.Key == Key.T)
                //{
                //    _vm.CurrentMode = DrawingMode.TriangleShape;
                //}
                ShapeRenderer.RenderAll(DrawArea, _vm.Shapes);
            };
        }

        private void RedrawCanvas()
        {
            ShapeRenderer.RenderAll(DrawArea, _vm.Shapes);
        }
        private void BtnFreehand_Click(object sender, RoutedEventArgs e)
        {
            _vm.CurrentMode = DrawingMode.FreeHand;
            UpdateToolButtons();
        }

        private void BtnLine_Click(object sender, RoutedEventArgs e)
        {
            _vm.CurrentMode = DrawingMode.StraightLine;
            UpdateToolButtons();
        }

        private void BtnRectangle_Click(object sender, RoutedEventArgs e)
        {
            _vm.CurrentMode = DrawingMode.Rectangle;
            UpdateToolButtons();
        }

        private void BtnTriangle_Click(object sender, RoutedEventArgs e)
        {
            _vm.CurrentMode = DrawingMode.TriangleShape;
            UpdateToolButtons();
        }

        private void BtnEllipse_Click(object sender, RoutedEventArgs e)
        {
            _vm.CurrentMode = DrawingMode.EllipseShape;
            UpdateToolButtons();
        }

        private void BtnUndo_Click(object sender, RoutedEventArgs e)
        {
            _vm.Undo();
            ShapeRenderer.RenderAll(DrawArea, _vm.Shapes);
        }

        private void BtnRedo_Click(object sender, RoutedEventArgs e)
        {
            _vm.Redo();
            ShapeRenderer.RenderAll(DrawArea, _vm.Shapes);
        }

        private void UpdateToolButtons()
        {
            // Reset all
            BtnFreehand.ClearValue(Button.BackgroundProperty);
            BtnFreehand.ClearValue(Button.BorderBrushProperty);
            BtnLine.ClearValue(Button.BackgroundProperty);
            BtnLine.ClearValue(Button.BorderBrushProperty);
            BtnRectangle.ClearValue(Button.BackgroundProperty);
            BtnRectangle.ClearValue(Button.BorderBrushProperty);
            BtnEllipse.ClearValue(Button.BackgroundProperty);
            BtnEllipse.ClearValue(Button.BorderBrushProperty);
            BtnTriangle.ClearValue(Button.BackgroundProperty);
            BtnTriangle.ClearValue(Button.BorderBrushProperty);
            BtnUndo.ClearValue(Button.BackgroundProperty);
            BtnUndo.ClearValue(Button.BorderBrushProperty);
            BtnRedo.ClearValue(Button.BackgroundProperty);
            BtnRedo.ClearValue(Button.BorderBrushProperty);

            // Highlight active
            Brush? selectedBrush = null;
            Brush? selectedBorder = null;
            try
            {
                selectedBrush = (Brush)FindResource("GlassyPressedBrush");
            }
            catch
            {
                selectedBrush = Brushes.LightSteelBlue;
            }

            selectedBorder = new SolidColorBrush(System.Windows.Media.Color.FromRgb(111, 166, 214)); // #6FA6D6

            if (_vm.CurrentMode == DrawingMode.FreeHand)
            {
                BtnFreehand.Background = selectedBrush;
                BtnFreehand.BorderBrush = selectedBorder;
            }
            else if (_vm.CurrentMode == DrawingMode.StraightLine)
            {
                BtnLine.Background = selectedBrush;
                BtnLine.BorderBrush = selectedBorder;
            }
            else if (_vm.CurrentMode == DrawingMode.Rectangle)
            {
                BtnRectangle.Background = selectedBrush;
                BtnRectangle.BorderBrush = selectedBorder;
            }
            else if (_vm.CurrentMode == DrawingMode.EllipseShape)
            {
                BtnEllipse.Background = selectedBrush;
                BtnEllipse.BorderBrush = selectedBorder;
            }
            else if (_vm.CurrentMode == DrawingMode.TriangleShape)
            {
                BtnTriangle.Background = selectedBrush;
                BtnTriangle.BorderBrush = selectedBorder;
            }
        }




    }
}
