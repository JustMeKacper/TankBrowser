using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankBrowser.MVVM.Model;

namespace TankBrowser.MVVM.View
{
    class Integrated : MainWindow
    {
        public void Reload()
        {
            base.reload(base.Index);    
        }
        public void ChangeName()
        {
            throw new NotImplementedException();
        }

        public void ChangePhoto()
        {
            throw new NotImplementedException();
        }
    }
}
