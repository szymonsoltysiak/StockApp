using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_App.MVVM.ViewModel
{
    public class ProfileViewModel : Core.ViewModel
    {
        public string Path { get; set; }
        public ProfileViewModel()
        {
            var workingDirectory = Environment.CurrentDirectory;
            var path = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            Path = System.IO.Path.Join(path, "Images", "Meme.jpg");
        }
    }
}
