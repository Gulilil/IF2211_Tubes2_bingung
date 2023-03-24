using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Diagnostics;
using System.IO;
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
using Class;
using Method;
using System.Threading;

namespace TreasureMaze
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String mode;
        private Map map;
        private Class.Point[] solPaths;
        private double side;
        private string rootPath;
        private Class.Point[] listCurLoc;

        public MainWindow()
        {
            InitializeComponent();
            BFSButton.AddHandler(Ellipse.MouseLeftButtonDownEvent, new MouseButtonEventHandler(PickBFS));
            DFSButton.AddHandler(Ellipse.MouseLeftButtonDownEvent, new MouseButtonEventHandler(PickDFS));
            BFSTSPButton.AddHandler(Ellipse.MouseLeftButtonDownEvent, new MouseButtonEventHandler(PickBFSTSP));
            DFSTSPButton.AddHandler(Ellipse.MouseLeftButtonDownEvent, new MouseButtonEventHandler(PickDFSTSP));
            mode = "BFS";
            map = new Map();
            string name = Directory.GetCurrentDirectory();
            this.rootPath = Directory.GetParent((Directory.GetParent(name)).ToString()).ToString();
            BitmapImage imgSource = new BitmapImage(new Uri(this.rootPath + "\\assets\\head.png"));
            HeadLogo.Source = imgSource;
            imgSource = new BitmapImage(new Uri(this.rootPath + "\\assets\\startbtn.png"));
            StartButton.Source = imgSource;
            imgSource = new BitmapImage(new Uri(this.rootPath + "\\assets\\file.png"));
            FileButton.Source = imgSource;
            imgSource = new BitmapImage(new Uri(this.rootPath + "\\assets\\search.png"));
            SearchButton.Source = imgSource;
            imgSource = new BitmapImage(new Uri(this.rootPath + "\\assets\\route.png"));
            RouteButton.Source = imgSource;
        }

        private void ChooseFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select File";
            fileDialog.Filter = "Text File (*.txt)|*.txt";
            fileDialog.FilterIndex = 1;
            fileDialog.ShowDialog();
            var bc = new BrushConverter();
            if (fileDialog.FileName != "")
            {
                chooseFileButton.Background = (Brush)bc.ConvertFrom("#F8B400");
                String pathFileMap = fileDialog.FileName;
                map = new Map();
                map.ReadFileAtPath(pathFileMap);

                if (map.getValid())
                {

                    MapBuffer.ColumnDefinitions.Clear();
                    MapBuffer.RowDefinitions.Clear();

                    int col = map.getCol();
                    int row = map.getRow();
                    int side;
                    if (col > row)
                    {
                        side = 650 / col;
                    }
                    else
                    {
                        side = 650 / row;
                    }
                    MapBuffer.Width = side * col;
                    MapBuffer.Height = side * row;
                    MapBuffer.Background = (Brush)bc.ConvertFrom("#000000");
                    for (int i = 0; i < col; i++)
                    {
                        ColumnDefinition coldef = new ColumnDefinition();
                        coldef.Width = new GridLength(1, GridUnitType.Star);
                        MapBuffer.ColumnDefinitions.Add(coldef);
                    }

                    this.side = side;
                    for (int i = 0; i < row; i++)
                    {
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
                            if (map.getValueAtCoordinate(new Class.Point(i, j)) == 'R')
                            {
                                stack.Background = (Brush)bc.ConvertFrom("#FAEDCD");
                            }
                            else if (map.getValueAtCoordinate(new Class.Point(i, j)) == 'X')
                            {
                                stack.Background = (Brush)bc.ConvertFrom("#245953");
                            }
                            else if (map.getValueAtCoordinate(new Class.Point(i, j)) == 'K')
                            {
                                stack.Background = (Brush)bc.ConvertFrom("#EAE7B1");
                                stack.Opacity = 1;
                                Image image = new Image();
                                BitmapImage imgSource = new BitmapImage(new Uri(this.rootPath + "\\assets\\start.png"));
                                image.Source = imgSource;
                                image.Height = this.side;
                                image.Width = this.side;
                                Grid.SetRow(image, i);
                                Grid.SetColumn(image, j);
                                MapBuffer.Children.Add(image);
                            }
                            else
                            {
                                stack.Background = (Brush)bc.ConvertFrom("#FAEDCD");
                                Image image = new Image();
                                BitmapImage imgSource = new BitmapImage(new Uri(this.rootPath + "\\assets\\treasure.png"));
                                image.Source = imgSource;
                                image.Height = this.side;
                                image.Width = this.side;
                                Grid.SetRow(image, i);
                                Grid.SetColumn(image, j);
                                MapBuffer.Children.Add(image);
                            }

                        }
                    }
                }
                else
                {
                    // Invalid Map Handling
                    Debug.WriteLine("Invalid Map Detected.");
                    MapBuffer.ColumnDefinitions.Clear();
                    MapBuffer.RowDefinitions.Clear();

                    MapBuffer.Width = 650;
                    MapBuffer.Height = 650;

                    chooseFileButton.Background = Brushes.Red;
                    MapBuffer.Background = (Brush)bc.ConvertFrom("#EFCFD4");
                    this.solPaths = null;
                }
            } 
            else
            {
                Debug.WriteLine("Masukkan salah");
            }
            
        }

        private void PickBFS(object sender, MouseButtonEventArgs e)
        {
            var bc = new BrushConverter();
            BFSButton.Fill = (Brush)bc.ConvertFrom("#F8B400");
            DFSButton.Fill = Brushes.White;
            BFSTSPButton.Fill = Brushes.White;
            DFSTSPButton.Fill = Brushes.White;
            mode = "BFS";
        }

        private void PickDFS(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter(); 
            BFSButton.Fill = Brushes.White;
            DFSButton.Fill = (Brush)bc.ConvertFrom("#F8B400");
            BFSTSPButton.Fill = Brushes.White;
            DFSTSPButton.Fill = Brushes.White;
            mode = "DFS";
        }

        private void PickBFSTSP(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter(); 
            BFSButton.Fill = Brushes.White;
            DFSButton.Fill = Brushes.White;
            BFSTSPButton.Fill = (Brush)bc.ConvertFrom("#F8B400");
            DFSTSPButton.Fill = Brushes.White;
            mode = "BFSTSP";
        }

        private void PickDFSTSP(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter(); 
            BFSButton.Fill = Brushes.White;
            DFSButton.Fill = Brushes.White;
            BFSTSPButton.Fill = Brushes.White;
            DFSTSPButton.Fill = (Brush)bc.ConvertFrom("#F8B400");
            mode = "DFSTSP";
        }

        private void StartProcess(object sender, RoutedEventArgs e)
        {   
            if (this.map.getCol() != 0 && this.map.getRow() != 0)
            {
                if (mode == "BFS")
                {
                    BFS bfs = new BFS();
                    bfs.getSolution(this.map);
                    timeBuffer.Content =  "Execution Time: " + bfs.getExecutionTime().ToString() + " ms";
                    stepsBuffer.Content = "Steps: " + bfs.getSteps().ToString();
                    nodesBuffer.Content = "Nodes: " + bfs.getNodes().ToString();
                    routeBuffer.Content = "Route: " + bfs.generateSolutionRoutes();
                    this.map.resetMap();
                    solPaths = bfs.getSolPaths();
                    listCurLoc = bfs.getListCurLoc();
                } else if (mode == "DFS")
                {
                    DFS dfs = new DFS();
                    dfs.getSolution(this.map);
                    timeBuffer.Content =  "Execution Time: " + dfs.getExecutionTime().ToString() + " ms";
                    stepsBuffer.Content = "Steps: " + dfs.getSteps().ToString();
                    nodesBuffer.Content = "Nodes: " + dfs.getNodes().ToString();
                    routeBuffer.Content = "Route: " + dfs.generateSolutionRoutes();
                    this.map.resetMap();
                    solPaths = dfs.getSolPaths();
                    listCurLoc = dfs.getListCurLoc();
                } else if (mode == "DFSTSP")
                {
                    DFS dfs = new DFS();
                    dfs.setTSP(true);
                    dfs.getSolution(this.map);
                    timeBuffer.Content =  "Execution Time: " + dfs.getExecutionTime().ToString() + " ms";
                    stepsBuffer.Content = "Steps: " + dfs.getSteps().ToString();
                    nodesBuffer.Content = "Nodes: " + dfs.getNodes().ToString();
                    routeBuffer.Content = "Route: " + dfs.generateSolutionRoutes();
                    this.map.resetMap();
                    solPaths = dfs.getSolPaths();
                    listCurLoc = dfs.getListCurLoc();
                } else if (mode == "BFSTSP") 
                {
                    BFS bfs = new BFS();
                    bfs.setTSP(true);
                    bfs.getSolution(this.map);
                    timeBuffer.Content =  "Execution Time: " + bfs.getExecutionTime().ToString() + " ms";
                    stepsBuffer.Content = "Steps: " + bfs.getSteps().ToString();
                    nodesBuffer.Content = "Nodes: " + bfs.getNodes().ToString();
                    routeBuffer.Content = "Route: " + bfs.generateSolutionRoutes();
                    this.map.resetMap();
                    solPaths = bfs.getSolPaths();
                    listCurLoc = bfs.getListCurLoc();
                }
            } else
            {
                 chooseFileButton.Background = Brushes.Red;
            }
            
        }

        private async void PlayRoute(object sender, RoutedEventArgs e)
        {
            int number;
            var bc = new BrushConverter(); 
            IntervalBuffer.Background = (Brush)bc.ConvertFrom("#245953");
            bool isNumber = int.TryParse(IntervalBuffer.Text, out number);
            if (this.solPaths != null && isNumber == true && number >= 10 && number <= 1000)
            {
                for (int i = 0; i < solPaths.Length;i++)
                {
                    Image image = new Image();
                    BitmapImage imgSource = new BitmapImage(new Uri(this.rootPath + "\\assets\\programmer.png"));
                    image.Source = imgSource;
                    image.Height = this.side;
                    image.Width = this.side;
                    Grid.SetRow(image, solPaths[i].getRow());
                    Grid.SetColumn(image, solPaths[i].getCol());
                    MapBuffer.Children.Add(image);
                    await Task.Delay(number);
                    MapBuffer.Children.Remove(image);
                    StackPanel stack1 = new StackPanel();
                    Grid.SetRow(stack1, solPaths[i].getRow());
                    Grid.SetColumn(stack1, solPaths[i].getCol());
                    
                    
                    var elements = MapBuffer.Children.Cast<UIElement>().Where(el => Grid.GetColumn(el) == solPaths[i].getCol() && Grid.GetRow(el) == solPaths[i].getRow());
                    int stackidx = elements.Count() - 1;
                    
                    
                    if ((this.map.getValueAtCoordinate(solPaths[i].getRow(), solPaths[i].getCol()) == 'R' && stackidx > 0) || (this.map.getValueAtCoordinate(solPaths[i].getRow(), solPaths[i].getCol()) != 'R' && stackidx > 1))
                    {
                        double opacity = (elements.ElementAt(stackidx - 1)).Opacity;
                        if (opacity == 1) {
                            opacity = 0;
                        }
                        if (opacity + 0.05 < 2)
                        {
                            stack1.Background = (Brush)bc.ConvertFrom("#3E3B11");
                            stack1.Opacity = opacity + 0.06 ;
                        } 

                    } else
                    {
                        stack1.Background = (Brush)bc.ConvertFrom("#EAE7B1");
                    }
                    MapBuffer.Children.Add(stack1);
                    if (solPaths[i].getRow() == this.map.getStartLoc().getRow() && solPaths[i].getCol() == this.map.getStartLoc().getCol())
                    {
                        Image img = new Image();
                        BitmapImage source = new BitmapImage(new Uri(this.rootPath + "\\assets\\start.png"));
                        img.Source = source;
                        img.Height = this.side;
                        img.Width = this.side;
                        Grid.SetRow(img, solPaths[i].getRow());
                        Grid.SetColumn(img, solPaths[i].getCol());
                        MapBuffer.Children.Add(img);
                    }
                }
                MapBuffer.Children.Clear();
                for (int i = 0; i < this.map.getRow(); i++)
                {
                    for (int j = 0; j < this.map.getCol(); j++)
                    {
                        StackPanel stack = new StackPanel();
                        Grid.SetRow(stack, i);
                        Grid.SetColumn(stack, j);
                        MapBuffer.Children.Add(stack);
                        if (map.getValueAtCoordinate(new Class.Point(i, j)) == 'R')
                        {
                            stack.Background = (Brush)bc.ConvertFrom("#FAEDCD");
                        }
                        else if (map.getValueAtCoordinate(new Class.Point(i, j)) == 'X')
                        {
                            stack.Background = (Brush)bc.ConvertFrom("#245953");
                        }
                        else if (map.getValueAtCoordinate(new Class.Point(i, j)) == 'K')
                        {
                            stack.Background = (Brush)bc.ConvertFrom("#EAE7B1");
                            Image image = new Image();
                            BitmapImage imgSource = new BitmapImage(new Uri(this.rootPath + "\\assets\\start.png"));
                            image.Source = imgSource;
                            image.Height = this.side;
                            image.Width = this.side;
                            Grid.SetRow(image, i);
                            Grid.SetColumn(image, j);
                            MapBuffer.Children.Add(image);
                        }
                        else
                        {
                            stack.Background = (Brush)bc.ConvertFrom("#FAEDCD");
                            Image image = new Image();
                            BitmapImage imgSource = new BitmapImage(new Uri(this.rootPath + "\\assets\\treasure.png"));
                            image.Source = imgSource;
                            image.Height = this.side;
                            image.Width = this.side;
                            Grid.SetRow(image, i);
                            Grid.SetColumn(image, j);
                            MapBuffer.Children.Add(image);
                        }

                    }
                }

            } else 
            {
                if (this.solPaths != null)
                {
                    IntervalBuffer.Background = Brushes.Red;
                } else
                {
                    chooseFileButton.Background = Brushes.Red;
                }
                
            } 
        }

        private async void PlaySearch(object sender, RoutedEventArgs e)
        {
            int number;
            var bc = new BrushConverter();
            IntervalBuffer.Background = (Brush)bc.ConvertFrom("#245953");
            bool isNumber = int.TryParse(IntervalBuffer.Text, out number);
            if (this.listCurLoc != null && isNumber == true && number >= 10 && number <= 1000)
            {
                for (int i = 0; i < listCurLoc.Length; i++)
                {
                    // Image image = new Image();
                    // BitmapImage imgSource = new BitmapImage(new Uri(this.rootPath + "\\assets\\programmer.png"));
                    // image.Source = imgSource;
                    // image.Height = this.side;
                    // image.Width = this.side;
                    // Grid.SetRow(image, listCurLoc[i].getRow());
                    // Grid.SetColumn(image, listCurLoc[i].getCol());
                    // MapBuffer.Children.Add(image);
                    StackPanel stack2 = new StackPanel();
                    Grid.SetRow(stack2, listCurLoc[i].getRow());
                    Grid.SetColumn(stack2, listCurLoc[i].getCol());
                    stack2.Background = (Brush)bc.ConvertFrom("Blue");
                    MapBuffer.Children.Add(stack2);
                    await Task.Delay(number);
                    MapBuffer.Children.Remove(stack2);
                    // MapBuffer.Children.Remove(image);
                    StackPanel stack1 = new StackPanel();
                    Grid.SetRow(stack1, listCurLoc[i].getRow());
                    Grid.SetColumn(stack1, listCurLoc[i].getCol());


                    var elements = MapBuffer.Children.Cast<UIElement>().Where(el => Grid.GetColumn(el) == listCurLoc[i].getCol() && Grid.GetRow(el) == listCurLoc[i].getRow());
                    int stackidx = elements.Count() - 1;


                    if ((this.map.getValueAtCoordinate(listCurLoc[i].getRow(), listCurLoc[i].getCol()) == 'R' && stackidx > 0) || (this.map.getValueAtCoordinate(listCurLoc[i].getRow(), listCurLoc[i].getCol()) != 'R' && stackidx > 1))
                    {
                        double opacity = (elements.ElementAt(stackidx - 1)).Opacity;
                        if (opacity == 1)
                        {
                            opacity = 0;
                        }
                        if (opacity + 0.05 < 2)
                        {
                            stack1.Background = (Brush)bc.ConvertFrom("#3E3B11");
                            stack1.Opacity = opacity + 0.06;
                        }

                    }
                    else
                    {
                        stack1.Background = (Brush)bc.ConvertFrom("#EAE7B1");
                    }
                    MapBuffer.Children.Add(stack1);
                    if (listCurLoc[i].getRow() == this.map.getStartLoc().getRow() && listCurLoc[i].getCol() == this.map.getStartLoc().getCol())
                    {
                        Image img = new Image();
                        BitmapImage source = new BitmapImage(new Uri(this.rootPath + "\\assets\\start.png"));
                        img.Source = source;
                        img.Height = this.side;
                        img.Width = this.side;
                        Grid.SetRow(img, listCurLoc[i].getRow());
                        Grid.SetColumn(img, listCurLoc[i].getCol());
                        MapBuffer.Children.Add(img);
                    } else if (this.map.getValueAtCoordinate(listCurLoc[i].getRow(), listCurLoc[i].getCol()) == 'T')
                    {
                        Image img = new Image();
                        BitmapImage source = new BitmapImage(new Uri(this.rootPath + "\\assets\\treasure.png"));
                        img.Source = source;
                        img.Height = this.side;
                        img.Width = this.side;
                        Grid.SetRow(img, listCurLoc[i].getRow());
                        Grid.SetColumn(img, listCurLoc[i].getCol());
                        MapBuffer.Children.Add(img);
                    }
                }
                MapBuffer.Children.Clear();
                for (int i = 0; i < this.map.getRow(); i++)
                {
                    for (int j = 0; j < this.map.getCol(); j++)
                    {
                        StackPanel stack = new StackPanel();
                        Grid.SetRow(stack, i);
                        Grid.SetColumn(stack, j);
                        MapBuffer.Children.Add(stack);
                        if (map.getValueAtCoordinate(new Class.Point(i, j)) == 'R')
                        {
                            stack.Background = (Brush)bc.ConvertFrom("#FAEDCD");
                        }
                        else if (map.getValueAtCoordinate(new Class.Point(i, j)) == 'X')
                        {
                            stack.Background = (Brush)bc.ConvertFrom("#245953");
                        }
                        else if (map.getValueAtCoordinate(new Class.Point(i, j)) == 'K')
                        {
                            stack.Background = (Brush)bc.ConvertFrom("#EAE7B1");
                            Image image = new Image();
                            BitmapImage imgSource = new BitmapImage(new Uri(this.rootPath + "\\assets\\start.png"));
                            image.Source = imgSource;
                            image.Height = this.side;
                            image.Width = this.side;
                            Grid.SetRow(image, i);
                            Grid.SetColumn(image, j);
                            MapBuffer.Children.Add(image);
                        }
                        else
                        {
                            stack.Background = (Brush)bc.ConvertFrom("#FAEDCD");
                            Image image = new Image();
                            BitmapImage imgSource = new BitmapImage(new Uri(this.rootPath + "\\assets\\treasure.png"));
                            image.Source = imgSource;
                            image.Height = this.side;
                            image.Width = this.side;
                            Grid.SetRow(image, i);
                            Grid.SetColumn(image, j);
                            MapBuffer.Children.Add(image);
                        }

                    }
                }

            }
            else
            {
                if (this.listCurLoc != null)
                {
                    IntervalBuffer.Background = Brushes.Red;
                }
                else
                {
                    chooseFileButton.Background = Brushes.Red;
                }

            }

        }

    }
}
