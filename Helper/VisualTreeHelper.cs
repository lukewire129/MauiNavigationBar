using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiNavigationBar.Helper
{
    public static class VisualTreeHelper
    {
        public static T GetParent<T>(this Element element) where T : Element
        {
            if (element is T)
                return element as T;
            else
            {
                if (element.Parent != null)
                    return element.Parent.GetParent<T> ();

                return default;
            }
        }
    }
}
