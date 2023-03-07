namespace EnterpriseApp.Client
{

    public class AppState
    {
        public bool IsLoggedIn { get; set; }
        public string UserName { get; set; } = string.Empty;

        private string? filter;
        public string Filter
        {
            get => filter ?? string.Empty;
            set
            {
                filter = value;
                NotifyStateChanged();
            }
        }

        private string? filterValue;
        public string FilterValue
        {
            get => filterValue ?? string.Empty;
            set
            {
                filterValue = value;
                NotifyStateChanged();
            }
        }

        private string? company;
        public string Company
        {
            get => company ?? string.Empty;
            set
            {
                company = value;
                NotifyStateChanged();
            }
        }

        private string? estatus;
        public string Estatus
        {
            get => estatus ?? string.Empty;
            set
            {
                estatus = value;
                NotifyStateChanged();
            }
        }


        public void SetLogin(bool login, string user)
        {
            IsLoggedIn = login;
            UserName = user;
            NotifyStateChanged();
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

    }

}
