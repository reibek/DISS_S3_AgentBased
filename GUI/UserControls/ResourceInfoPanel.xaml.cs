using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for ResourceInfoPanel.xaml
    /// </summary>
    public partial class ResourceInfoPanel : UserControl
    {
        public static readonly DependencyProperty LabelDependency = DependencyProperty.Register("Label", typeof(string), typeof(ResourceInfoPanel));
        public string Label
        {
            get => (string)GetValue(LabelDependency);
            set => SetValue(LabelDependency, value);
        }

        public static readonly DependencyProperty QuCountDependency = DependencyProperty.Register("QuCount", typeof(string), typeof(ResourceInfoPanel));
        public string QuCount
        {
            get => (string)GetValue(QuCountDependency);
            set => SetValue(QuCountDependency, value);
        }

        public static readonly DependencyProperty QuAvgDependency = DependencyProperty.Register("QuAvg", typeof(string), typeof(ResourceInfoPanel));
        public string QuAvg
        {
            get => (string)GetValue(QuAvgDependency);
            set => SetValue(QuAvgDependency, value);
        }

        public static readonly DependencyProperty ResBusyDependency = DependencyProperty.Register("ResBusy", typeof(string), typeof(ResourceInfoPanel));
        public string ResBusy
        {
            get => (string)GetValue(ResBusyDependency);
            set => SetValue(ResBusyDependency, value);
        }

        public static readonly DependencyProperty ResCountDependency = DependencyProperty.Register("ResCount", typeof(string), typeof(ResourceInfoPanel));
        public string ResCount
        {
            get => (string)GetValue(ResCountDependency);
            set => SetValue(ResCountDependency, value);
        }

        public static readonly DependencyProperty ResUtilDependency = DependencyProperty.Register("ResUtil", typeof(string), typeof(ResourceInfoPanel));
        public string ResUtil
        {
            get => (string)GetValue(ResUtilDependency);
            set => SetValue(ResUtilDependency, value);
        }

        public ResourceInfoPanel()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
