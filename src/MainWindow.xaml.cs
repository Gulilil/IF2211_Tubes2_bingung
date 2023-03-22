using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Diagnostics;
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
        private String mode;
        private Map map;

        public MainWindow()
        {
            InitializeComponent();
            BFSButton.AddHandler(Ellipse.MouseLeftButtonDownEvent, new MouseButtonEventHandler(PickBFS));
            DFSButton.AddHandler(Ellipse.MouseLeftButtonDownEvent, new MouseButtonEventHandler(PickDFS));
            BFSTSPButton.AddHandler(Ellipse.MouseLeftButtonDownEvent, new MouseButtonEventHandler(PickBFSTSP));
            DFSTSPButton.AddHandler(Ellipse.MouseLeftButtonDownEvent, new MouseButtonEventHandler(PickDFSTSP));
            mode = "BFS";
            map = new Map();
        }

        private void ChooseFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select File";
            fileDialog.Filter = "Text File (*.txt)|*.txt";
            fileDialog.FilterIndex = 1;
            fileDialog.ShowDialog();
            if (fileDialog.FileName != "")
            {
                String pathFileMap = fileDialog.FileName;
                map.ReadFileAtPath(pathFileMap);
                
                MapBuffer.ColumnDefinitions.Clear();
                MapBuffer.RowDefinitions.Clear();
                
                int col = map.getCol();
                int row = map.getRow();
                int side;
                if (col > row)
                {
                    side = 650/col;
                } else
                {
                    side = 650/row;
                }
                MapBuffer.Width = side * col;
                MapBuffer.Height = side * row;
                var bc = new BrushConverter();
                MapBuffer.Background = (Brush)bc.ConvertFrom("#000000");
                for (int i = 0; i < col; i++) {
                    ColumnDefinition coldef = new ColumnDefinition();
                    coldef.Width = new GridLength(1, GridUnitType.Star);
                    MapBuffer.ColumnDefinitions.Add(coldef);
                }
                
                for (int i = 0; i < row; i++) {
                    RowDefinition rowdef = new RowDefinition();
                    rowdef.Height = new GridLength(1, GridUnitType.Star);
                    MapBuffer.RowDefinitions.Add(rowdef);
                }
                
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        StackPanel stack = new StackPanel();
                        Grid.SetRow(stack, i);
                        Grid.SetColumn(stack, j);
                        MapBuffer.Children.Add(stack);
                        if (map.getValueAtCoordinate(new src.Point(i, j)) == 'R')
                        {
                            stack.Background = (Brush)bc.ConvertFrom("#FAEDCD");
                        } else if (map.getValueAtCoordinate(new src.Point(i, j)) == 'X')
                        {
                            stack.Background = (Brush)bc.ConvertFrom("#245953");
                        } else if (map.getValueAtCoordinate(new src.Point(i, j)) == 'K')
                        {
                            stack.Background = (Brush)bc.ConvertFrom("#19A7CE");
                        } else
                        {
                            stack.Background = (Brush)bc.ConvertFrom("#F7C04A");
                        }
                      
                    }
                }
            } else
            {
                Debug.WriteLine("Masukkan salah");
            }
            
        }

        private void PickBFS(object sender, MouseButtonEventArgs e)
        {
            var bc = new BrushConverter();
            BFSButton.Fill = (Brush)bc.ConvertFrom("#94AF9F");
            DFSButton.Fill = Brushes.White;
            BFSTSPButton.Fill = Brushes.White;
            DFSTSPButton.Fill = Brushes.White;
            mode = "BFS";
        }

        private void PickDFS(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter(); 
            BFSButton.Fill = Brushes.White;
            DFSButton.Fill = (Brush)bc.ConvertFrom("#94AF9F");
            BFSTSPButton.Fill = Brushes.White;
            DFSTSPButton.Fill = Brushes.White;
            mode = "DFS";
        }

        private void PickBFSTSP(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter(); 
            BFSButton.Fill = Brushes.White;
            DFSButton.Fill = Brushes.White;
            BFSTSPButton.Fill = (Brush)bc.ConvertFrom("#94AF9F");
            DFSTSPButton.Fill = Brushes.White;
            mode = "BFSTSP";
        }

        private void PickDFSTSP(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter(); 
            BFSButton.Fill = Brushes.White;
            DFSButton.Fill = Brushes.White;
            BFSTSPButton.Fill = Brushes.White;
            DFSTSPButton.Fill = (Brush)bc.ConvertFrom("#94AF9F");
            mode = "DFSTSP";
        }

        private void StartProcess(object sender, RoutedEventArgs e)
        {
            if (mode == "BFS")
            {
                BFS bfs = new BFS();
                bfs.getSolution(this.map);
                timeBuffer.Content =  "Execution Time: " + bfs.getExecutionTime().ToString();
                stepsBuffer.Content = "Steps: " + bfs.getSteps().ToString();
                nodesBuffer.Content = "Nodes: " + bfs.getNodes().ToString();
                routeBuffer.Content = "Route: " + bfs.generateSolutionRoutes();
                this.map.resetMap();
            } else if (mode == "DFS")
            {
                DFS dfs = new DFS();
                dfs.getSolution(this.map);
                timeBuffer.Content =  "Execution Time: " + dfs.getExecutionTime().ToString();
                stepsBuffer.Content = "Steps: " + dfs.getSteps().ToString();
                nodesBuffer.Content = "Nodes: " + dfs.getNodes().ToString();
                routeBuffer.Content = "Route: " + dfs.generateSolutionRoutes();
                this.map.resetMap();
            } else if (mode == "DFSTSP")
            {
                DFS dfs = new DFS();
                dfs.setTSP(true);
                dfs.getSolution(this.map);
                timeBuffer.Content =  "Execution Time: " + dfs.getExecutionTime().ToString();
                stepsBuffer.Content = "Steps: " + dfs.getSteps().ToString();
                nodesBuffer.Content = "Nodes: " + dfs.getNodes().ToString();
                routeBuffer.Content = "Route: " + dfs.generateSolutionRoutes();
                this.map.resetMap();
            } else
            {
                BFS bfs = new BFS();
                bfs.setTSP(true);
                bfs.getSolution(this.map);
                timeBuffer.Content =  "Execution Time: " + bfs.getExecutionTime().ToString();
                stepsBuffer.Content = "Steps: " + bfs.getSteps().ToString();
                nodesBuffer.Content = "Nodes: " + bfs.getNodes().ToString();
                routeBuffer.Content = "Route: " + bfs.generateSolutionRoutes();
                this.map.resetMap();
            }
        }
    }
}
