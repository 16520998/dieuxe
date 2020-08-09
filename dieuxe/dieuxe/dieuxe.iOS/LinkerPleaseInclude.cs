using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace dieuxe.iOS
{
    public class LinkerPleaseInclude
    {
        public void Include(UITabBarItem item)
        {
            var x = new System.ComponentModel.ReferenceConverter(typeof(void));
            item.BadgeColor = UIColor.Red;
            item.BadgeValue = "badge";
        }
    }
}