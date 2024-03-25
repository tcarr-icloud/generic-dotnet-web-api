namespace webapi
{
    public class UsersApi : IDisposable
    {
        private readonly DatabaseApiContext _context;
        private GenericRepository<User> _userRepository;
        private GenericRepository<Company> _companyRepository;
        private GenericRepository<Workspace> _workspaceRepository;

        public UsersApi(DatabaseApiContext context)
        {
            this._context = context;
        }

        public GenericRepository<User> UserRepository
        {
            get
            {
                this._userRepository ??= new GenericRepository<User>(_context);
                return _userRepository;
            }
        }

        public GenericRepository<Company> CompanyRepository
        {
            get
            {
                this._companyRepository ??= new GenericRepository<Company>(_context);
                return _companyRepository;
            }
        }

        public GenericRepository<Workspace> WorkspaceRepository
        {
            get
            {
                this._workspaceRepository ??= new GenericRepository<Workspace>(_context);
                return _workspaceRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}