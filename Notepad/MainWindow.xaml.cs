/*
* Filename      :  MainWindow.xaml.cs
* Project       :  PROG 2121 A-02 : WPF Application
* By            :  Paige Lam
* Date          :  October 1, 2021
* Description   :  This class file contains the main default properties, attributes, 
*                     behavior and methods for the notepad application
*/


using Microsoft.Win32;
using System.IO;
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


namespace Notepad
{
    /// <summary>
    /// The purpose of this class is to mofel the properties, attributes, and behaviours of a simple notepad
    /// </summary>
    public partial class MainWindow : Window
    {
        private string fileName;
        private string openedFilePath;


        /// <summary>
        /// It states the getter and setter for file name in a notepad
        /// </summary>
        public string Filename
        {
            get
            {
                return fileName;
            }
            set
            {

            }
        }


        /// <summary>
        /// It states the getter and setter for open file path in a notepad
        /// </summary>
        public string OpenedFilePath
        {
            get
            {
                return openedFilePath;
            }
            set
            {

            }
        }


        /// <summary>
        /// This is the initial constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        public MainWindow(string filename)
        {
            try
            {
                fileName = filename;
                this.Title = fileName;
            }
            catch (Exception ext)
            {
                // if no file name was found 
                MessageBox.Show("No File Name \n " + "Error Code :" + ext.Message, "Missing File name", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        /// <summary>
        /// It provide an overview of features that New feature under File
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void new_Click(object sender, RoutedEventArgs e)
        {
            // when textbox content length is 0
            if (text_area.Text.Length == 0)
            {
                MainWindow newWin = new MainWindow();
                newWin.Show();

                this.Close();
            }

            // when textbox content length is greater than 0
            else if (text_area.Text.Length != 0)
            {
                MessageBoxResult result = MessageBox.Show("Save changes to \"" + (fileName ?? "untitled") + "\"?", "Notepad", MessageBoxButton.YesNoCancel);

                // when user would like to save current work after trying to use New option
                if (result == MessageBoxResult.Yes)
                {
                    // save file in directory
                    SaveAs_File();

                    MainWindow newWin = new MainWindow();
                    newWin.Show();

                    this.Close();
                }

                // when user does not want to save current work after trying to use New option
                else if (result == MessageBoxResult.No)
                {
                    MainWindow newWin = new MainWindow();
                    newWin.Show();

                    this.Close();

                }
            }
        }


        //Function for saving new files 
        /// <summary>
        /// It provide an overview of features that Save As feature under File, including default file name, 
        /// default file extension, and default filtered file extension in the directory
        /// </summary>
        public void SaveAs_File()
        {
            SaveFileDialog save_dialog = new SaveFileDialog();

            save_dialog.FileName = "untitled";
            save_dialog.DefaultExt = ".txt";
            save_dialog.Filter = "Text Documents (.txt) |*.txt";

            save_dialog.ShowDialog();

            File.WriteAllText(save_dialog.FileName, text_area.Text);
        }


        //Function for saving already opened files or all other loaded files 
        /// <summary>
        /// It indicates the opened file path when save a file
        /// </summary>
        public void Save_File()
        {
            File.WriteAllText(openedFilePath, text_area.Text);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void open_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Save changes to \"" + (fileName ?? "untitled") + "\"?", "Notepad", MessageBoxButton.YesNo);

            /// when user would like to save current work after trying to use Open feature
            if (result == MessageBoxResult.Yes)
            {
                // save file in directory
                SaveAs_File();

                MainWindow newWin = new MainWindow();
                newWin.Show();

                this.Close();
            }

            // when user does not want to save current work after trying to use Open feature
            if (result == MessageBoxResult.No)
            {
                text_area.Text = ""; 

                OpenFileDialog open_dialog = new OpenFileDialog();

                // use Open feature under File
                if (open_dialog.ShowDialog() == true)
                {
                    text_area.Text = File.ReadAllText(open_dialog.FileName).ToString(); 

                    openedFilePath = System.IO.Path.GetFullPath(open_dialog.FileName);

                    this.Title = System.IO.Path.GetFileNameWithoutExtension(open_dialog.FileName);
                }

            }
        }


        /// <summary>
        /// Calling the SaveAs_File method to perform Save As feature under File
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void save_as_Click(object sender, RoutedEventArgs e)
        {
            SaveAs_File();
        }


        /// <summary>
        /// Close feature under File closes the notepad program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        /// <summary>
        /// About feature under Help opens a new window that contains information of the notepad application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void about_Click(object sender, RoutedEventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.Show();

        }


        /// <summary>
        /// It captures the changes inside the textbox in terms of character counts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void text_area_TextChanged(object sender, TextChangedEventArgs e)
        {
            CharacterCount.Content = text_area.Text.Length; 
        }
    }
}
