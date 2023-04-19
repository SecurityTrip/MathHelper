using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using SkiaSharp;
using SkiaSharp.Views.Avalonia;

namespace MathHelper;


public partial class MainWindow : Window
{
    
    public MainWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        FunctionTextBox = this.FindControl<TextBox>("FunctionTextBox");
        XMinTextBox = this.FindControl<TextBox>("XMinTextBox");
        XMaxTextBox = this.FindControl<TextBox>("XMaxTextBox");
        YMinTextBox = this.FindControl<TextBox>("YMinTextBox");
        YMaxTextBox = this.FindControl<TextBox>("YMaxTextBox");
    }

    private void OnPlotGraphButtonClick(object sender, RoutedEventArgs e)
    {
        string function = FunctionTextBox.Text;
        double xMin = double.Parse(XMinTextBox.Text);
        double xMax = double.Parse(XMaxTextBox.Text);
        double yMin = double.Parse(YMinTextBox.Text);
        double yMax = double.Parse(YMaxTextBox.Text);

        // Create a new SKCanvasView and add it to the window
        var canvasView = new SKCanvasView();
        canvasView.PaintSurface += (s, args) =>
        {
            // Get the SKCanvas from the args parameter
            SKCanvas canvas = args.Surface.Canvas;

            // Set up the coordinate system
            int width = args.Info.Width;
            int height = args.Info.Height;
            canvas.Scale(width / (float)(xMax - xMin), -height / (float)(yMax - yMin));
            canvas.Translate(-(float)xMin, -(float)yMax);

            // Create a new SKPath and add the graph of the function to it
            SKPath path = new SKPath();
            for (double x = xMin; x <= xMax; x += 0.1)
            {
                double y = EvaluateFunction(function, x);
                if (double.IsNaN(y))
                {
                    // Skip NaN values
                    continue;
                }
                SKPoint point = new SKPoint((float)x, (float)y);
                if (path.IsEmpty)
                {
                    path.MoveTo(point);
                }
                else
                {
                    path.LineTo(point);
                }
            }

            // Set up the paint and draw the path
            SKPaint paint = new SKPaint();
            paint.Color = SKColors.Blue;
            paint.StrokeWidth = 3;
            paint.IsAntialias = true;
            canvas.DrawPath(path, paint);
        };
        this.Content = canvasView;
    }

    private double EvaluateFunction(string function, double x)
    {
        // TODO: evaluate the function for the given value of x
        return 0;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
