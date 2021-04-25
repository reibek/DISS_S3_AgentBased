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
    /// Interaction logic for LabeledInput.xaml
    /// </summary>
    public partial class LabeledInput : UserControl
    {
        public static readonly DependencyProperty LabelDependency = DependencyProperty.Register("Label", typeof(string), typeof(LabeledInput));
        public string Label
        {
            get => (string) GetValue(LabelDependency);
            set => SetValue(LabelDependency, value);
        }

        public LabeledInput()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
