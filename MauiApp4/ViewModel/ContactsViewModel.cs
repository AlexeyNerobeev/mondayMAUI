using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp4.DTO;
using MauiApp4.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MauiApp4.ViewModel
{
#pragma warning disable
    partial class ContactsViewModel : ObservableObject
    {
        private readonly IApiService _apiService;

        [ObservableProperty]
        private ObservableCollection<ContactDto> _contacts = new ObservableCollection<ContactDto>();

        [ObservableProperty]
        private ContactDto _selectedContact;

        [ObservableProperty]
        private string _searchText;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool _isBusy;

        [ObservableProperty]
        private bool _isRefreshing;

        [ObservableProperty]
        private bool _isModalVisible;

        [ObservableProperty]
        private ContactDto _editingContact;

        public bool IsNotBusy => !IsBusy;

        public ContactsViewModel(IApiService apiService)
        {
            _apiService = apiService;
            _ = LoadContacts(); 
        }

        public ContactsViewModel()
        {
            if (Microsoft.Maui.Controls.DesignMode.IsDesignModeEnabled)
            {
                Contacts.Add(new ContactDto
                {
                    Id = 1,
                    FirstName = "Иван",
                    LastName = "Иванов",
                    Phone = "+79991234567",
                    Email = "ivan@example.com",
                    Address = "Москва"
                });
                Contacts.Add(new ContactDto
                {
                    Id = 2,
                    FirstName = "Петр",
                    LastName = "Петров",
                    Phone = "+79998765432",
                    Email = "petr@example.com",
                    Address = "Санкт-Петербург"
                });
            }
        }

        [RelayCommand]
        private async Task LoadContacts()
        {
            try
            {
                IsBusy = true;
                var contacts = await _apiService.GetContactsAsync(SearchText);

                Contacts.Clear();
                foreach (var contact in contacts)
                {
                    Contacts.Add(contact);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Ошибка",
                    $"Ошибка загрузки данных: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private void AddContact()
        {
            EditingContact = new ContactDto();
            IsModalVisible = true;
        }

        [RelayCommand]
        private void EditContact(ContactDto contact)
        {
            if (contact == null) return;

            EditingContact = new ContactDto
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Phone = contact.Phone,
                Email = contact.Email,
                Address = contact.Address
            };
            IsModalVisible = true;
        }

        [RelayCommand]
        private async Task DeleteContact(ContactDto contact)
        {
            if (contact == null) return;

            bool confirm = await Shell.Current.DisplayAlert(
                "Удаление контакта",
                $"Вы уверены, что хотите удалить контакт {contact.FullName}?",
                "Да", "Нет");

            if (confirm)
            {
                try
                {
                    IsBusy = true;
                    await _apiService.DeleteContactAsync(contact.Id);

                    Contacts.Remove(contact);
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Ошибка",
                        $"Ошибка удаления контакта: {ex.Message}", "OK");
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        [RelayCommand]
        private async Task SaveContact()
        {
            if (EditingContact == null) return;

            try
            {
                IsBusy = true;

                if (string.IsNullOrWhiteSpace(EditingContact.FirstName))
                {
                    await Shell.Current.DisplayAlert("Ошибка", "Введите имя", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(EditingContact.LastName))
                {
                    await Shell.Current.DisplayAlert("Ошибка", "Введите фамилию", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(EditingContact.Phone))
                {
                    await Shell.Current.DisplayAlert("Ошибка", "Введите телефон", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(EditingContact.Email))
                {
                    await Shell.Current.DisplayAlert("Ошибка", "Введите email", "OK");
                    return;
                }

                if (EditingContact.Id == 0) 
                {
                    var createDto = new CreateContactDto
                    {
                        FirstName = EditingContact.FirstName,
                        LastName = EditingContact.LastName,
                        Phone = EditingContact.Phone,
                        Email = EditingContact.Email,
                        Address = EditingContact.Address
                    };

                    var newContact = await _apiService.CreateContactAsync(createDto);

                    Contacts.Add(newContact);
                }
                else 
                {
                    var updateDto = new UpdateContactDto
                    {
                        FirstName = EditingContact.FirstName,
                        LastName = EditingContact.LastName,
                        Phone = EditingContact.Phone,
                        Email = EditingContact.Email,
                        Address = EditingContact.Address
                    };

                    await _apiService.UpdateContactAsync(EditingContact.Id, updateDto);

                    var existingContact = Contacts.FirstOrDefault(c => c.Id == EditingContact.Id);
                    if (existingContact != null)
                    {
                        existingContact.FirstName = EditingContact.FirstName;
                        existingContact.LastName = EditingContact.LastName;
                        existingContact.Phone = EditingContact.Phone;
                        existingContact.Email = EditingContact.Email;
                        existingContact.Address = EditingContact.Address;
                    }
                }

                CloseModal();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Ошибка",
                    $"Ошибка сохранения контакта: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task RefreshContacts()
        {
            try
            {
                IsRefreshing = true;
                await LoadContacts();
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        private async Task SearchContact()
        {
            await LoadContacts();
        }

        [RelayCommand]
        private void CloseModal()
        {
            IsModalVisible = false;
            EditingContact = null;
        }

        partial void OnSearchTextChanged(string value)
        {

        }
    }
}