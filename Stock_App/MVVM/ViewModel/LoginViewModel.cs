using Stock_App.Core;
using Stock_App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Stock_App.MVVM.ViewModel
{
    public class LoginViewModel : Core.ViewModel
    {
        public LoginViewModel() { }
        public bool CredentialCheck()
        {
            return true;
        }

        private string stuff;

        /*public void Test()
        {
            
            loginBtn.IsEnabled = false;
        }*/
    }

}
