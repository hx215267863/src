using System;
using System.Windows;

namespace IFactory.UI.Controls
{
    public class GifImageExceptionRoutedEventArgs : RoutedEventArgs
    {
        public Exception ErrorException;

        public GifImageExceptionRoutedEventArgs(RoutedEvent routedEvent, object obj)
          : base(routedEvent, obj)
        {
        }
    }
}
