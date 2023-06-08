using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib
{
    public class LoginState
    {
        public bool isLoggedIn { get; set; }
        public matkaajaDTO? loggedUser { get; set; }

        public event Action? OnChange;



        public void SetLogin(bool login, matkaajaDTO user)
        {
            isLoggedIn = login;
            loggedUser = user;
            NotifyStateChanged();
        }

        private void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }
    }
}
