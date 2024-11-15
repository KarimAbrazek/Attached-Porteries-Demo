using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace WpfControls.CustomBehaviors.Behaviors
{
    public static class DataGridColumnValidate
    {

        #region DependencyProperties
        // Attached property to enable validation on the DataGrid
        public static readonly DependencyProperty EnableValidationProperty = DependencyProperty.RegisterAttached(
            "EnableValidation", typeof(bool), typeof(DataGridColumnValidate), new PropertyMetadata(false, OnEnableValidationChanged));

        // BackgroundColor attached property
        public static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.RegisterAttached(
            "BackgroundColor", typeof(Brush), typeof(DataGridColumnValidate), new PropertyMetadata(Brushes.Transparent));


        // ForegroundColor attached property
        public static readonly DependencyProperty ForegroundColorProperty = DependencyProperty.RegisterAttached(
            "ForegroundColor", typeof(Brush), typeof(DataGridColumnValidate), new PropertyMetadata(Brushes.Black));


        // LowerThreshold attached property
        public static readonly DependencyProperty LowerThresholdProperty = DependencyProperty.RegisterAttached(
            "LowerThreshold", typeof(double), typeof(DataGridColumnValidate), new PropertyMetadata(double.MinValue));


        // UpperThreshold attached property
        public static readonly DependencyProperty UpperThresholdProperty = DependencyProperty.RegisterAttached(
            "UpperThreshold", typeof(double), typeof(DataGridColumnValidate), new PropertyMetadata(double.MaxValue));

        #endregion

        #region Set Get 
        public static bool GetEnableValidation(DependencyObject obj) => (bool)obj.GetValue(EnableValidationProperty);
        public static void SetEnableValidation(DependencyObject obj, bool value) => obj.SetValue(EnableValidationProperty, value);

        public static double GetLowerThreshold(DependencyObject obj) => (double)obj.GetValue(LowerThresholdProperty);
        public static void SetLowerThreshold(DependencyObject obj, double value) => obj.SetValue(LowerThresholdProperty, value);




        public static double GetUpperThreshold(DependencyObject obj) => (double)obj.GetValue(UpperThresholdProperty);
        public static void SetUpperThreshold(DependencyObject obj, double value) => obj.SetValue(UpperThresholdProperty, value);


        public static Brush GetBackgroundColor(DependencyObject obj) => (Brush)obj.GetValue(BackgroundColorProperty);
        public static void SetBackgroundColor(DependencyObject obj, Brush value) => obj.SetValue(BackgroundColorProperty, value);


        public static Brush GetForegroundColor(DependencyObject obj) => (Brush)obj.GetValue(ForegroundColorProperty);
        public static void SetForegroundColor(DependencyObject obj, Brush value) => obj.SetValue(ForegroundColorProperty, value);

        #endregion


        #region Main methods
        private static void OnEnableValidationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DataGrid dataGrid && (bool)e.NewValue)
            {
                // Apply validation logic when DataGrid is loaded or DataContext changes or any event you need :)
                dataGrid.Loaded += (s, args) => ApplyValidationToAllCells(dataGrid);
                dataGrid.DataContextChanged += (s, args) => ApplyValidationToAllCells(dataGrid);
                dataGrid.MouseMove += (s, args) => ApplyValidationToAllCells(dataGrid);

            }
        }
        #endregion

        #region private Method

        // Apply validation to all cells in the DataGrid
        private static void ApplyValidationToAllCells(DataGrid dataGrid)
        {
            foreach (var row in dataGrid.Items)
            {
                foreach (var column in dataGrid.Columns)
                {
                    var cellContent = column.GetCellContent(row);
                    if (cellContent is FrameworkElement element)
                    {
                        ApplyValidationToCell(column, element);
                    }
                }
            }
        }

        // Validate the individual cell on edit or row edit
        private static void ApplyValidationToCell(DataGridColumn column, FrameworkElement cellElement)
        {
            if (cellElement is TextBlock textBlock && double.TryParse(textBlock.Text, out double cellValue))
            {
                double lowerThreshold = GetLowerThreshold(column);
                double upperThreshold = GetUpperThreshold(column);
                Brush backgroundColor = GetBackgroundColor(column);
                Brush foregroundColor = GetForegroundColor(column);

                // Apply background and foreground color if the value is out of bounds
                if (cellValue < lowerThreshold || cellValue > upperThreshold)
                {
                    DataGridCell cell = GetParentCell(cellElement);
                    if (cell != null)
                    {
                        cell.Background = backgroundColor;
                        cell.Foreground = foregroundColor;
                    }
                }
                else
                {
                    // Reset to default colors if the value is within bounds
                    DataGridCell cell = GetParentCell(cellElement);
                    if (cell != null)
                    {
                        cell.Background = Brushes.Transparent;
                        cell.Foreground = Brushes.Black;
                    }
                }
            }


        }

        private static DataGridCell GetParentCell(FrameworkElement element)
        {
            while (element != null && !(element is DataGridCell))
            {
                element = VisualTreeHelper.GetParent(element) as FrameworkElement;
            }
            return element as DataGridCell;
        }

        #endregion


    }
}
