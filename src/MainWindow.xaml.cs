using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime;
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
using Microsoft.Win32;
using src;

namespace TreasureMaze
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String pathFileMap;
        private String mode;
        private Map map;

        public MainWindow()
        {
            InitializeComponent();
            BFS.AddHandler(Ellipse.MouseLeftButtonDownEvent, new MouseButtonEventHandler(PickBFS));
            DFS.AddHandler(Ellipse.MouseLeftButtonDownEvent, new MouseButtonEventHandler(PickDFS));
            BFSTSP.AddHandler(Ellipse.MouseLeftButtonDownEvent, new MouseButtonEventHandler(PickBFSTSP));
            DFSTSP.AddHandler(Ellipse.MouseLeftButtonDownEvent, new MouseButtonEventHandler(PickDFSTSP));
            pathFileMap = "";
            mode = "BFS";
        }

        private void ChooseFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select File";
            fileDialog.InitialDirectory = @"C:\";
            fileDialog.Filter = "Text File (*.txt)|*.txt";
            fileDialog.FilterIndex = 1;
            fileDialog.ShowDialog();
            if (fileDialog.FileName != "")
            {

            }
        }

        private void PickBFS(object sender, MouseButtonEventArgs e)
        {
            var bc = new BrushConverter();
            BFS.Fill = (Brush)bc.ConvertFrom("#94AF9F");
            DFS.Fill = Brushes.White;
            BFSTSP.Fill = Brushes.White;
            DFSTSP.Fill = Brushes.White;
            mode = "BFS";
        }

        private void PickDFS(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter(); 
            BFS.Fill = Brushes.White;
            DFS.Fill = (Brush)bc.ConvertFrom("#94AF9F");
            BFSTSP.Fill = Brushes.White;
            DFSTSP.Fill = Brushes.White;
            mode = "DFS";
        }

        private void PickBFSTSP(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter(); 
            BFS.Fill = Brushes.White;
            DFS.Fill = Brushes.White;
            BFSTSP.Fill = (Brush)bc.ConvertFrom("#94AF9F");
            DFSTSP.Fill = Brushes.White;
            mode = "BFSTSP";
        }

        private void PickDFSTSP(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter(); 
            BFS.Fill = Brushes.White;
            DFS.Fill = Brushes.White;
            BFSTSP.Fill = Brushes.White;
            DFSTSP.Fill = (Brush)bc.ConvertFrom("#94AF9F");
            mode = "DFSTSP";
        }

        private void StartProcess(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
