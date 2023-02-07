using BespokeFusion;
using System.Windows.Media;
using System.Windows;

namespace ViewModels.Utilites.CustomMessageBox
{
    /// <summary>
    /// <see cref="DarkMessageBox"/> is a static class that creates <see cref="CustomMaterialMessageBox"/> with dark theme
    ///</summary>
    public static class DarkMessageBox
    {
        private static SolidColorBrush DarkPurpleBrush;
        private static SolidColorBrush DarkBrush;

        /// <summary>
        /// Initializes a new instance of the <see cref="DarkMessageBox"/> class.
        /// </summary>
        static DarkMessageBox()
        {
            DarkPurpleBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF673AB7"));
            DarkBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF303030"));
        }

        /// <summary>
        /// Shows a window according to given values with ok button.
        /// </summary>
        /// <param name="text">Text is to be shown.</param>
        /// <param name="brush"><see cref="Brush"/> for corners of window.</param>
        public static void Show(string text, Brush brush = null)
        {
            var msg = new CustomMaterialMessageBox()
            {
                TxtMessage = { Text = text, Foreground = Brushes.White },
                TxtTitle = { Text = "Info", Foreground = Brushes.White },
                BtnOk = { Content = "Ok", Background = DarkPurpleBrush },
                BtnCancel = { Visibility = Visibility.Collapsed },
                BtnCopyMessage = { Visibility = Visibility.Collapsed },
                MainContentControl = { Background = DarkBrush },
                TitleBackgroundPanel = { Background = DarkPurpleBrush },
                BorderBrush = brush != null ? brush : Brushes.Red
            };
            msg.Show();
        }

        /// <summary>
        /// Shows a window according to given values with ok button.
        /// </summary>
        /// <param name="text">Text is to be shown.</param>
        /// <param name="title">Title is to be shown.</param>
        /// <param name="brush"><see cref="Brush"/> for corners of window.</param>
        public static void Show(string text, string title, Brush brush = null)
        {
            var msg = new CustomMaterialMessageBox()
            {
                TxtMessage = { Text = text, Foreground = Brushes.White },
                TxtTitle = { Text = title, Foreground = Brushes.White },
                BtnOk = { Content = "Ok", Background = DarkPurpleBrush },
                BtnCancel = { Visibility = Visibility.Collapsed },
                BtnCopyMessage = { Visibility = Visibility.Collapsed },
                MainContentControl = { Background = DarkBrush },
                TitleBackgroundPanel = { Background = DarkPurpleBrush },
                BorderBrush = brush != null ? brush : Brushes.Red
            };
            msg.BtnOk.Content = "Ok";
            msg.TxtMessage.Text = text;
            msg.TxtTitle.Text = title;
            msg.Show();
        }

        /// <summary>
        /// Shows a window according to given values with Yes and No button.
        /// </summary>
        /// <param name="text">Text is to be shown.</param>
        /// <param name="title">Title is to be shown.</param>
        /// <param name="brush"><see cref="Brush"/> for corners of window.</param>
        public static MessageBoxResult ShowResult(string text, string title, Brush brush = null)
        {
            var msg = new CustomMaterialMessageBox()
            {
                TxtMessage = { Text = text, Foreground = Brushes.White },
                TxtTitle = { Text = title, Foreground = Brushes.White },
                BtnOk = { Content = "Yes", Background = DarkPurpleBrush },
                BtnCancel = { Content = "No", Background = DarkPurpleBrush },
                BtnCopyMessage = { Visibility = Visibility.Collapsed },
                MainContentControl = { Background = DarkBrush },
                TitleBackgroundPanel = { Background = DarkPurpleBrush },
                BorderBrush = brush != null ? brush : Brushes.Red
            };
            msg.Show();
            return msg.Result;
        }
    }
}
