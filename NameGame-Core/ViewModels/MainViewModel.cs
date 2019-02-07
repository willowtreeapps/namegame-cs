using System;
using System.Runtime.CompilerServices;
using WillowTree.NameGame.Core.Models;
using WillowTree.NameGame.Core.Services;

namespace WillowTree.NameGame.Core.ViewModels
{
    public class MainViewModel
    {
        private NameGameService _service;

		public MainViewModel()
        {
            _service = new NameGameService();
        }
    }
}
