using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;
using System;
using System.IO;
using Avalonia.Media;
using System.Numerics;
using System.Collections.Generic;

namespace SharpUp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void BackUp(object source,  RoutedEventArgs args)
    {
        List<string> fToIgnore = new List<string>();
        List<string> dToIgnore = new List<string>();

        if(Directory.Exists(WhatBkUp.Text) && Directory.Exists(WhereBkUp.Text))
        {
            string src = WhatBkUp.Text;
            string dest = WhereBkUp.Text;
            WarnText.Text = "";

            string? ftoIgStr = FToIgString.Text;
            string? dtoIgStr = DToIgString.Text;

            if(!string.IsNullOrEmpty(ftoIgStr))
            {
                string[] fIgArr = ftoIgStr.Split(' ');

                foreach(var i in fIgArr)
                {
                    fToIgnore.Add(i);
                }
            }
            if(!string.IsNullOrEmpty(dtoIgStr))
            {
                string[] dIgArr = dtoIgStr.Split(' ');

                foreach(var i in dIgArr)
                {
                    dToIgnore.Add(i);
                }
            }

            BackUp bk = new BackUp(src, dest, fToIgnore, dToIgnore);

        }
        else
        {
            WarnText.Text = "Please enter valid paths";
        }
    }
}