using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace QuanLyKhachSan.ViewModel
{
    public class Lib
    {

        public T FindParent<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);
            if (parent == null) return null;
            var parentT = parent as T;
            return parentT ?? FindParent<T>(parent);
        }
        public FrameworkElement GetWindowParent(Button p)
        {
            FrameworkElement parent = p;
            while (parent.Parent != null)
            {
                if (parent.Parent is UserControl)
                {
                    parent = parent.Parent as FrameworkElement;
                    break;
                }
                parent = parent.Parent as FrameworkElement;
            }
            return parent;
        }

        public T FindVisualChildByName<T>(DependencyObject parent, string name) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                string controlName = child.GetValue(Control.NameProperty) as string;

                if (controlName == name)
                {
                    return child as T;
                }
                else
                {
                    T result = FindVisualChildByName<T>(child, name);

                    if (result != null)
                        return result;
                }
            }
            return null;
        }

        public string VN(string p)
        {
            string res = "";
            int cnt = 0;
            for (int i = p.Length - 1; i >= 1; i--)
            {
                cnt++;
                res = p[i] + res;
                if (cnt == 3)
                {
                    res = "." + res;
                    cnt = 0;
                }
            }
            res = p[0] + res + " vnđ";
            return res;
        }
    }
}
