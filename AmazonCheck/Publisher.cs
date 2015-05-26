using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmazonCheck
{
    class Publisher
    {
        public delegate void VisitDelegate(String strUrl);

        public event VisitDelegate SetVisitEvent;

        public void Visit(String strUrl)
        {
            if (SetVisitEvent != null) 
            {
                SetVisitEvent(strUrl); 
            }
        }
    }
}


