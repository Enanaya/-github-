using SQLite.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using 客户端.Models;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace 客户端.Resource
{
    public sealed partial class MyContentDialog : UserControl
    {
        public MyContentDialog()
        {
            this.InitializeComponent();
        }


        /// <summary>
        /// 标识 <see cref="Text"/> 的依赖项属性。
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(MyContentDialog), new PropertyMetadata(default(string)));

        /// <summary>
        /// 获取或设置
        /// </summary>
        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        //private

    }
}
