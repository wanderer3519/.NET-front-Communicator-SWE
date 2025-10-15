using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using CanvasDataModel;

namespace ViewModel
{
    public class CanvasViewModel
    {
        public enum DrawingMode { FreeHand, StraightLine, Rectangle, EllipseShape, TriangleShape }
        private DrawingMode currentMode = DrawingMode.FreeHand;
        public DrawingMode CurrentMode
        {
            get => currentMode;
            set => currentMode = value;
        }
        private List<Point> _trackedPoints = new(); // this is for tracking mouse movements
        public bool IsTracking = false;

        public ObservableCollection<IShape> Shapes = new();  // current canvas shapes
        private readonly StateManager _stateManager = new();     // current canvas shapes


        public void StartTracking(Point point)
        {
            IsTracking = true;
            if (CurrentMode == DrawingMode.FreeHand)
            {
                _trackedPoints.Clear();
                _trackedPoints.Add(point);
            }
            else if (CurrentMode == DrawingMode.StraightLine || CurrentMode == DrawingMode.Rectangle || CurrentMode == DrawingMode.EllipseShape || CurrentMode == DrawingMode.TriangleShape)
            {
                _trackedPoints.Clear();
                _trackedPoints.Add(point); // start point
                _trackedPoints.Add(point); // end point (will be updated on mouse move)
            }
        }
        public void TrackPoint(Point point)
        {
            if (IsTracking && _trackedPoints.Count > 0)
            {
                if (CurrentMode == DrawingMode.FreeHand)
                {
                    _trackedPoints.Add(point);
                }
                else if (CurrentMode == DrawingMode.StraightLine || CurrentMode == DrawingMode.Rectangle || CurrentMode == DrawingMode.EllipseShape || CurrentMode == DrawingMode.TriangleShape)
                {
                    _trackedPoints[1] = point; // update end point
                }
            }
        }

        public void StopTracking()
        {
            IsTracking = false;
            if (_trackedPoints.Count == 0) return;
            if (CurrentMode == DrawingMode.FreeHand)
            {
                if (_trackedPoints.Count == 0) return;
                var freehand = new FreeHand(_trackedPoints);
                Shapes.Add(freehand);
                _stateManager.AddState(new State(Shapes));
                return;

            }
            else if (CurrentMode == DrawingMode.StraightLine)
            {
                if (_trackedPoints.Count < 2) return;
                var line = new StraightLine(_trackedPoints);
                Shapes.Add(line);
                _stateManager.AddState(new State(Shapes));
                return;
            }
            else if (CurrentMode == DrawingMode.Rectangle)
            {
                if (_trackedPoints.Count < 2) return;
                var rectangle = new RectangleShape(_trackedPoints);
                Shapes.Add(rectangle);
                _stateManager.AddState(new State(Shapes));
                return;
            }
            else if (CurrentMode == DrawingMode.EllipseShape)
            {
                if (_trackedPoints.Count < 2) return;
                var ellipse = new EllipseShape(_trackedPoints);
                Shapes.Add(ellipse);
                _stateManager.AddState(new State(Shapes));
                return;
            }
            else if (CurrentMode == DrawingMode.TriangleShape)
            {
                if (_trackedPoints.Count < 2) return;
                var ellipse = new TriangleShape(_trackedPoints);
                Shapes.Add(ellipse);
                _stateManager.AddState(new State(Shapes));
                return;
            }
        }
        public IShape? CurrentPreviewShape
        {
            get
            {
                if (!IsTracking || _trackedPoints.Count < 2) return null;

                switch (CurrentMode)
                {
                    case DrawingMode.FreeHand:
                        return null; // or optionally a partial FreeHand preview
                    case DrawingMode.StraightLine:
                        return new StraightLine(_trackedPoints);
                    case DrawingMode.Rectangle:
                        return new RectangleShape(_trackedPoints);
                    case DrawingMode.EllipseShape:
                        return new EllipseShape(_trackedPoints);
                    case DrawingMode.TriangleShape:
                        return new TriangleShape(_trackedPoints);
                    default:
                        return null;
                }
            }
        }

        public void Undo()
        {
            var prev = _stateManager.Undo(); // StateManager is in Model
            if (prev != null)
            {
                Shapes.Clear();
                foreach (var s in prev.Shapes) Shapes.Add(s);
            }
        }

        public void Redo()
        {
            var next = _stateManager.Redo(); // StateManager is in Model
            if (next != null)
            {
                Shapes.Clear();
                foreach (var s in next.Shapes) Shapes.Add(s);
            }
        }
    }
}
