using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using WillowTree.NameGame.Core.Models;
using WillowTree.NameGame.Core.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using System.IO;

namespace WillowTree.NameGame.Core.ViewModels
{
    public class MainViewModel : MvxViewModel, INotifyPropertyChanged
    {
        private INameGameService _service;
        private Random random = new Random();

        public MainViewModel(INameGameService service)
        {
            _service = service;

            AddPeople();
        }        

        /// <summary>
        /// Sets the Person objects that displayed within the MainView
        /// </summary>
        public async void AddPeople()
        {
            Person loading = new Person {
                Name = "loading...",
                PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/b/b1/Loading_icon.gif"
            };
            Person1 = loading;
            Person2 = loading;
            Person3 = loading;
            Person4 = loading;
            Person5 = loading;
            Person6 = loading;          
            
            Person whoIsPerson = new Person();
            List<Person> people = _service.GetPeople().GetAwaiter().GetResult();

            // Populate group of people to display
            whoIsPerson = people[random.Next(people.Count)];
            WhoIs = whoIsPerson;

            List<Person> displayPeople = new List<Person>();

            displayPeople.Add(whoIsPerson);

            while (displayPeople.Count < 6)
            {
                Person randomPerson = people[random.Next(people.Count)];
                if (!displayPeople.Contains(randomPerson))
                {
                    displayPeople.Add(randomPerson);
                }
            }

            // Populate objects that bind to MainView.xaml
            Person1 = displayPeople[getIndexToRemove(displayPeople)];
            displayPeople.Remove(Person1);
            await Task.Delay(random.Next(1000, 2000));

            Person2 = displayPeople[getIndexToRemove(displayPeople)];
            displayPeople.Remove(Person2);
            await Task.Delay(random.Next(1000, 2000));

            Person3 = displayPeople[getIndexToRemove(displayPeople)];
            displayPeople.Remove(Person3);
            await Task.Delay(random.Next(1000, 2000));

            Person4 = displayPeople[getIndexToRemove(displayPeople)];
            displayPeople.Remove(Person4);
            await Task.Delay(random.Next(1000, 2000));

            Person5 = displayPeople[getIndexToRemove(displayPeople)];
            displayPeople.Remove(Person5);
            await Task.Delay(random.Next(1000, 2000));
                        
            Person6 = displayPeople[getIndexToRemove(displayPeople)];
            displayPeople.Remove(Person6);
            await Task.Delay(random.Next(1000, 2000));
        }

        /// <summary>
        /// Return a random index for an List<Person>
        /// </summary>                
        private int getIndexToRemove(List<Person> peopleList)
        {
            int index;
            index  = random.Next(peopleList.Count);
            return index;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies the view when property changes
        /// </summary>        
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        Person _WhoIs;
        Person _Person1;
        Person _Person2;
        Person _Person3;
        Person _Person4;
        Person _Person5;
        Person _Person6;
       
        public Person WhoIs
        {
            get
            {
                return _WhoIs;
            }
            set
            {
                if (_WhoIs != value)
                {
                    _WhoIs = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Person Person1
        {
            get
            {
                return _Person1;
            }
            set
            {
                if (_Person1 != value)
                {
                    _Person1 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Person Person2
        {
            get
            {
                return _Person2;
            }
            set
            {
                if (_Person2 != value)
                {
                    _Person2 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Person Person3
        {
            get
            {
                return _Person3;
            }
            set
            {
                if (_Person3 != value)
                {
                    _Person3 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Person Person4
        {
            get
            {
                return _Person4;
            }
            set
            {
                if (_Person4 != value)
                {
                    _Person4 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Person Person5
        {
            get
            {
                return _Person5;
            }
            set
            {
                if (_Person5 != value)
                {
                    _Person5 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Person Person6
        {
            get
            {
                return _Person6;
            }
            set
            {
                if (_Person6 != value)
                {
                    _Person6 = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
