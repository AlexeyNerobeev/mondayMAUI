using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp4.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp4.ViewModel
{
    partial class ContactsViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<NContact> _contacts = new ObservableCollection<NContact>();

        [ObservableProperty]
        private NContact _selectedContact;

        public ContactsViewModel()
         {
            Contacts.Add(new NContact("Иванов иван иванович", "+7 549 463 85 12", "ivanov@gmail.com"));
            Contacts.Add(new NContact("Петров Сергей иванович", "+7 549 123 12 85", "petrov@gmail.com"));
            Contacts.Add(new NContact("Федоров максим Анатольевич", "+7 549 643 12 75", "fedorov@gmail.com"));
            Contacts.Add(new NContact("Боженов Даниил Петрович", "+7 549 346 12 54", "bojenov@gmail.com"));
        }


        [RelayCommand]
        private void Update()
        {
            Contacts.Clear();

            Contacts.Add(new NContact("Иванов иван иванович", "+7 549 463 85 12", "ivanov@gmail.com"));
            Contacts.Add(new NContact("Петров Сергей иванович", "+7 549 123 12 85", "petrov@gmail.com"));
            Contacts.Add(new NContact("Федоров максим Анатольевич", "+7 549 643 12 75", "fedorov@gmail.com"));
            Contacts.Add(new NContact("Боженов Даниил Петрович", "+7 549 346 12 54", "bojenov@gmail.com"));


        }
    }
}
